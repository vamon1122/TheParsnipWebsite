using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using System.Data.SqlClient;
using System.Diagnostics;
using ParsnipData.Logs;

namespace ParsnipWebsite
{
    public partial class Users : System.Web.UI.Page
    {
        User myUser;
        static Guid selectedUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["userId"] == null)
                Response.Redirect("users?userId=" + Guid.Empty.ToString());

            myUser = Account.SecurePage("users", this, Data.DeviceType, "admin");

            selectedUserId = new Guid(Request.QueryString["userId"]);

            if (selectedUserId.ToString() == Guid.Empty.ToString())
            {
                Button_Action.Text = "Insert";
                Button_Delete.Visible = false;
            }
            else
            {
                Button_Action.Text = "Update";
                Button_Delete.Visible = true;
            }
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            Debug.WriteLine("----------Page load complete!");

            if (Request.QueryString["action"] != null)
            {
                string action = Request.QueryString["action"];
                if (Request.QueryString["success"] != null)
                {
                    string success = Request.QueryString["success"];

                    if (success == "true")
                    {
                        SuccessText.Text = string.Format("<strong>Success</strong> User was successfully {0}d on " +
                            "the database!", action);

                        Success.Attributes.CssStyle.Add("display", "block");
                    }
                }
            }

            UserForm.UpdateDateCreated();
            UpdateUserList();
            if (selectedUserId.ToString() != Guid.Empty.ToString())
                selectUser.SelectedValue = selectedUserId.ToString();

            UserForm.UpdateDataSubject(selectedUserId);

            System.Diagnostics.Debug.WriteLine("Page_LoadComplete complete!");
        }

        void UpdateUserList()
        {
            var tempUsers = new List<User>();
            tempUsers.Add(new User(Guid.Empty) { Forename = "New", Surname = "User", Username = "Create a new user" });
            tempUsers.AddRange(ParsnipData.Accounts.User.GetAllUsers());

            ListItem[] ListItems = new ListItem[tempUsers.Count];

            int i = 0;
            foreach (User temp in tempUsers)
            {
                ListItems[i] = new ListItem(String.Format("{0} ({1})", temp.FullName, temp.Username), 
                    temp.Id.ToString());

                i++;
            }
            selectUser.Items.Clear();
            selectUser.Items.AddRange(ListItems);

            selectUser.SelectedValue = selectedUserId.ToString();
        }

        protected void SelectUser_Changed(object sender, EventArgs e)
        {
            Response.Redirect("users?userId=" + selectUser.SelectedValue);
        }

        protected void Button_Action_Click(object sender, EventArgs e)
        {
            string rememberSelectedValue = selectUser.SelectedValue;
            Debug.WriteLine("BEN!!!1 " + UserForm.DataSubject.Id.ToString());
            string temp = string.Format("{0} button was clicked. Selected user id = {1}", Button_Action.Text, 
                rememberSelectedValue);

            //Debug.WriteLine(temp);
            new LogEntry(Log.Default) { text = temp };

            UserForm.UpdateDataSubject();

            string actionPast = UserForm.DataSubject.ExistsOnDb() ? "edited" : "created";
            string actionPresent = UserForm.DataSubject.ExistsOnDb() ? "edit" : "create";

            if (UserForm.DataSubject.Validate())
            {
                if (UserForm.DataSubject.Update())
                {
                    new LogEntry(Log.Default) { text = String.Format("{0} {1} an account for {2} via the UserForm", 
                        myUser.FullName, actionPast, UserForm.DataSubject.FullName) };

                    Response.Redirect(string.Format("users?userId={0}&action=update&success=true", UserForm.DataSubject.Id.ToString()));
                }

                else
                {
                    new LogEntry(Log.Default) { text = String.Format("{0} tried to {1} an account for {2} via the UserForm, but there was an error whilst updating the database", myUser.FullName, actionPresent, UserForm.DataSubject.FullName) };
                    ErrorText.Text = string.Format("<strong>Database Error</strong> There was an error whilst updating {0} on the database.", UserForm.DataSubject.FullName);
                }


            }
            else
            {
                Debug.WriteLine("User failed to validate!");
                new LogEntry(Log.Default)
                {
                    text = String.Format("{0} attempted to {1} an account for {2} via the UserForm, but {3} was not " +
                    "validated successfully.", myUser.FullName, actionPresent, UserForm.DataSubject.FullName, 
                    UserForm.DataSubject.SubjectiveGenderPronoun)
                };

                Error.Attributes.CssStyle.Add("display", "block");

                string ValidationInfo = string.Format("<strong>Validation Error</strong> {0} could not be updated " +
                    "because {1} failed to validate: ", UserForm.DataSubject.FullName, 
                    UserForm.DataSubject.SubjectiveGenderPronoun);

                foreach (string error in UserForm.DataSubject.ValidationErrors)
                {
                    ValidationInfo += error + ", ";
                }

                ValidationInfo = ValidationInfo.Substring(0, ValidationInfo.Length - 2);
                ValidationInfo += ".";

                ErrorText.Text = ValidationInfo;
            }
        }

        protected void Button_DeleteConfirm_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Delete was confirmed");
            string temp = string.Format("Delete button was clicked. Selected user id = {0}", selectUser.SelectedValue);
            //Debug.WriteLine(temp);
            new LogEntry(Log.Default) { text = temp };

            bool success;

            if (UserForm.DataSubject.ExistsOnDb())
            {
                success = UserForm.DataSubject.Delete();
            }
            else
                success = false;

            Response.Redirect(string.Format("users?userId={0}&action=delete&success={1}", Guid.Empty.ToString(), success));
        }

        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Delete was clicked");
        }
    }
}