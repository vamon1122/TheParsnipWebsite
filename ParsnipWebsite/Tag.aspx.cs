﻿using System;
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
                var tagId = Convert.ToInt32(Request.QueryString["id"]);
                var userId = Convert.ToInt32(Request.QueryString["user"]);

                if (tagId != default)
                {
                    myTag = MediaTag.Select(tagId);
                    TagName.InnerText = myTag.Name;
                }

                if(userId != default)
                {
                    myTagUser = ParsnipData.Accounts.User.Select(userId);
                    TagName.InnerText = myTagUser.FullName;
                }

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
                Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:title\" content=\"{myTag.Name}\" />"));
                Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:description\" content=\"{myTag.Description}\" />"));
                //Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:image\" content=\"{Request.Url.GetLeftPart(UriPartial.Authority)}/Resources/Media/Images/Local/Dirt_On_You.jpg\" />"));
                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:alt\" content=\"{0}\" />", myTag.Description)));

                Page.Title = $"Tag: {myTag.Name}";
            }
            else
            {
                Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:title\" content=\"{myTagUser.FullName} was tagged in...\" />"));
                Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:description\" content=\"See pictures and videos which {myTagUser.FullName} has been tagged in!\" />"));
                //Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:image\" content=\"{Request.Url.GetLeftPart(UriPartial.Authority)}/Resources/Media/Images/Local/Dirt_On_You.jpg\" />"));
                Page.Header.Controls.Add(new LiteralControl(string.Format($"<meta property=\"og:alt\" content=\"See pictures and videos which {myTagUser.FullName} has been tagged in!\" />")));

                Page.Title = $"Tag: {myTagUser.Forename}";
            }

            //This will break youtube videos which have not had their thumbnail regenerated
            Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:type\" content=\"website\" />"));
            Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:url\" content=\"{0}\" />", Request.Url.ToString())));
            Page.Header.Controls.Add(new LiteralControl("<meta property=\"fb:app_id\" content=\"521313871968697\" />"));

            if (myTag != null)
                UploadMediaControl.Initialise(myUser, myTag, this);
            else
                UploadMediaControl.Initialise(myUser, myTagUser.Id, this);
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