using System;
using ParsnipData.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Logging;
using ParsnipData.Media;
using System.Web.UI.HtmlControls;
using ParsnipData;
using System.Data.SqlClient;
using System.Diagnostics;
using ParsnipWebsite.Custom_Controls.Media;

namespace ParsnipWebsite
{
    public partial class View_Tag : System.Web.UI.Page
    {
        private User myUser;
        private MediaTag myTag;
        private User myTagUser;

        public View_Tag()
        {
            //Retrieves wrong album ID and overwrites
            //PhotosAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]) && string.IsNullOrEmpty(Request.QueryString["user"]))
            {
                Response.Redirect("home");
            }
            else
            {
                myTag = MediaTag.Select(Convert.ToInt32(Request.QueryString["id"]));
                myTagUser = ParsnipData.Accounts.User.Select(Convert.ToInt32(Request.QueryString["user"]));
                string focus = Request.QueryString["focus"];

                if (myTagUser == null)
                {
                    if (string.IsNullOrEmpty(focus))
                        myUser = Account.SecurePage($"tag?id={myTag.Id}", this, Data.DeviceType);
                    else
                        myUser = Account.SecurePage($"tag?id={myTag.Id}&{focus}", this, Data.DeviceType);
                }
                else
                {
                    if (string.IsNullOrEmpty(focus))
                        myUser = Account.SecurePage($"tag?user={myTagUser.Id}", this, Data.DeviceType);
                    else
                        myUser = Account.SecurePage($"tag?user={myTagUser.Id}&{focus}", this, Data.DeviceType);
                }
            }

            if (myTagUser == null)
            {
                Page.Title = $"Tag: {myTag.Name}";
            }
            else
            {
                Page.Title = $"Tag: {myTagUser.Forename}";
            }

            TagName.InnerText = string.IsNullOrEmpty(myTag.Name) ? myTagUser.FullName : myTag.Name;
            
            if(myTag != null)
                UploadMediaControl.Initialise(myUser, myTag, this);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if(myTagUser == null)
            {
                foreach (MediaControl mediaControl in MediaControl.GetAlbumAsMediaControls(myTag))
                {
                    DynamicMediaDiv.Controls.Add(mediaControl);
                }
            }
            else
            {
                foreach (MediaControl mediaControl in MediaControl.GetMediaUserPairAsMediaControls(myTagUser.Id))
                {
                    DynamicMediaDiv.Controls.Add(mediaControl);
                }
            }
        }
    }
}