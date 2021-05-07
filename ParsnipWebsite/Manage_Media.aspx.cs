using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Logging;
using System.Data.SqlClient;
using ParsnipData;
using ParsnipData.Media;
using System.Diagnostics;
using ParsnipWebsite.Custom_Controls.Media;

namespace ParsnipWebsite
{
    public partial class Manage_Media : System.Web.UI.Page
    {
        User myUser;
        int selectedUserId;

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage(this, Data.DeviceType, "admin");
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            UpdateUserList();

            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                new LogEntry(Log.Debug) { Text = "Manage_Media userId = " + Request.QueryString["id"].ToString() };
                selectedUserId = Convert.ToInt16(Request.QueryString["id"]);

                SelectUser.SelectedValue = selectedUserId.ToString();


                foreach (AdminMediaControl temp in AdminMediaControl.GetUserMediaAsAdminMediaControls(selectedUserId, myUser.Id))
                {
                    DisplayPhotosDiv.Controls.Add(temp);
                }
            }
            else
            {

                if (Request.QueryString["id"] == null)
                    Response.Redirect("manage_media?id=0");
            }
        }

        protected void BtnDeleteUploads_Click(object sender, EventArgs e)
        {
            //if(selectedUserId != 0)
            //MediaTag.DeleteCreatedByUser(selectedUserId);
            
            if((Convert.ToInt32(SelectUser.SelectedValue) != 0))
                MediaTag.DeleteCreatedByUser(Convert.ToInt32(SelectUser.SelectedValue));
            
        }

        void UpdateUserList()
        {
            var tempUsers = new List<User>();
            tempUsers.Add(new User()
            {
                Forename = "None",
                Surname = "Selected",
                Username = "No user selected"
            });

            tempUsers.AddRange(ParsnipData.Accounts.User.GetAllUsers());

            ListItem[] ListItems = new ListItem[tempUsers.Count];

            int i = 0;
            foreach (User temp in tempUsers)
            {
                ListItems[i] = new ListItem(String.Format("{0} ({1})", temp.FullName, temp.Username), temp.Id.ToString());
                i++;
            }
            SelectUser.Items.Clear();
            SelectUser.Items.AddRange(ListItems);

            SelectUser.SelectedValue = selectedUserId.ToString();
        }

        protected void SelectUser_Changed(object sender, EventArgs e)
        {
            Response.Redirect("manage_media?id=" + SelectUser.SelectedValue);
        }
    }
}