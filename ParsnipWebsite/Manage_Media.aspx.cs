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
        Guid selectedUserId;
        static readonly Log DebugLog = new Log("Debug");

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage("manage_media", this, Data.DeviceType, "admin");
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            UpdateUserList();

            if (Request.QueryString["userId"] != null && Request.QueryString["userId"].ToString() != "")
            {
                new LogEntry(DebugLog) { text = "Manage_Media userId = " + Request.QueryString["userId"].ToString() };
                selectedUserId = new Guid(Request.QueryString["userId"].ToString());

                SelectUser.SelectedValue = selectedUserId.ToString();

                Debug.WriteLine("---------- posted back with id = " + selectedUserId);

                List<ParsnipData.Media.Image> MyPhotos = ParsnipData.Media.Image.GetImagesByUser(selectedUserId);
                //new LogEntry(Debug) { text = "Got all photos. There were {0} photo(s) = " + AllPhotos.Count() };
                foreach (MediaControl temp in MediaManager.GetUsersMediaAsMediaControls(selectedUserId))
                {
                    DisplayPhotosDiv.Controls.Add(temp);
                }
            }
            else
            {
                Debug.WriteLine("---------- not a postback");

                if (Request.QueryString["userId"] == null)
                    Response.Redirect("manage_media?userId=" + Guid.Empty.ToString());
            }
        }

        protected void BtnDeleteUploads_Click(object sender, EventArgs e)
        {
            selectedUserId = new Guid(Request.QueryString["userId"].ToString());
            ParsnipData.Media.Image.DeleteMediaTagPairsByUserId(selectedUserId);
            ParsnipData.Media.Video.DeleteMediaTagPairsByUserId(selectedUserId);
            ParsnipData.Media.YoutubeVideo.DeleteMediaTagPairsByUserId(selectedUserId);
            new LogEntry(DebugLog)
            {
                text = "Successfully deleted photos uploaded media createdbyid = " +
                selectedUserId
            };
        }

        void UpdateUserList()
        {
            var tempUsers = new List<User>();
            tempUsers.Add(new User(Guid.Empty)
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
            Response.Redirect("manage_media?userId=" + SelectUser.SelectedValue);
        }
    }
}