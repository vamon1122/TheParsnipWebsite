using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Logs;
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
        static readonly Log DebugLog = Log.Select(3);

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage("manage_media", this, Data.DeviceType, "admin");
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            UpdateUserList();

            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                new LogEntry(DebugLog) { text = "Manage_Media userId = " + Request.QueryString["id"].ToString() };
                selectedUserId = Convert.ToInt16(Request.QueryString["id"]);

                SelectUser.SelectedValue = selectedUserId.ToString();


                foreach (MediaControl temp in MediaManager.GetUserMediaAsMediaControls(selectedUserId, myUser.Id))
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