﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Media;
using ParsnipData.Logging;
using ParsnipWebsite.Custom_Controls.Media;
using System.Configuration;

namespace ParsnipWebsite
{
    public partial class Home : System.Web.UI.Page
    {
        private User myUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            MOTD_div.InnerHtml = ConfigurationManager.AppSettings["MOTD"];

            myUser = ParsnipData.Accounts.User.LogIn();

            Page httpHandler = (Page)HttpContext.Current.Handler;
            if (myUser == null)
                LoginNudge.Visible = true;
            else
            {
                UploadMediaControl.Initialise(myUser, this);
                var mediaControls = MediaControl.GetUserMediaAsMediaControls(myUser.Id, myUser.Id);
                if (mediaControls.Count != default)
                    UploadsPlaceholder.Visible = false;
                
                foreach (MediaControl mc in mediaControls)
                {
                    MyMediaContainer.Controls.Add(mc);
                }
            }
                

            if (string.IsNullOrEmpty(Data.DeviceType))
            {
                Response.Redirect("get_device_info?url=home");
            }

            new LogEntry(Log.Debug) { text = string.Format("The home page was accessed by {0} from {1} {2} device.", 
                myUser == null ? "someone who was not logged in" : myUser.Forename, 
                myUser == null ? "their" : myUser.PosessivePronoun, Data.DeviceType) };

            WelcomeLabel.Text = 
                string.Format("Hiya {0} to the parsnip website!", myUser == null ?
                "stranger, welcome" : myUser.Forename + ", welcome back");

            List<ViewTagControl> ViewTagControls = new List<ViewTagControl>();
            foreach (MediaTag mediaTag in MediaTag.GetAllTags())
            {
                ViewTagControl mediaTagPairViewControl = (ViewTagControl)httpHandler.LoadControl("~/Custom_Controls/Media/ViewTagControl.ascx");
                mediaTagPairViewControl.MyTag = mediaTag;
                mediaTagPairViewControl.UpdateLink();
                //MediaTagContainer.Controls.Add(mediaTagPairViewControl);
                ViewTagControls.Add(mediaTagPairViewControl);
            }

            foreach (ParsnipData.Accounts.User user in ParsnipData.Accounts.User.GetAllUsers())
            {
                ViewTagControl mediaUserPairViewControl = (ViewTagControl)httpHandler.LoadControl("~/Custom_Controls/Media/ViewTagControl.ascx");
                mediaUserPairViewControl.MyUser = user;
                mediaUserPairViewControl.UpdateLink();
                //MediaTagContainer.Controls.Add(mediaUserPairViewControl);
                ViewTagControls.Add(mediaUserPairViewControl);
            }

            foreach (ViewTagControl control in ViewTagControls.OrderBy(x => x.Name))
            {
                MediaTagContainer.Controls.Add(control);
            }


            var myImage = new ParsnipData.Media.Image();
            myImage.Id = MediaId.NewMediaId();
            myImage.Compressed = "Resources/Media/Images/Local/Dirt_On_You.jpg";
            myImage.Placeholder = "Resources/Media/Images/Local/Dirt_On_You.jpg";
            
            if (myUser != null)
                myImage.Title = $"Hey {myUser.Forename}, what DIRT do we have on YOU? 😜";
            else
                myImage.Title = $"What DIRT do we have on YOU? 😜";

            var MySeeYourselfControl = (MediaControl)Page.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
            MySeeYourselfControl.AnchorLink = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/me";
            MySeeYourselfControl.MyMedia = myImage;

            seeYourself.Controls.Add(MySeeYourselfControl);
            seeYourself.Visible = true;

            int userId = myUser == null ? 0 : myUser.Id;
            Media latestVideo = Media.SelectLatestVideo(userId);
            if(latestVideo != null)
            {
                var MyVideoControl = (MediaControl)Page.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");


                latestVideo.Title = "LATEST VIDEO: " + latestVideo.Title;
                MyVideoControl.MyMedia = latestVideo;

                LatestVideo.Controls.Add(MyVideoControl);
            }
            
        }
    }
}