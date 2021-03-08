using System;
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
    public partial class MyUploads : System.Web.UI.Page
    {
        private User myUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            Page httpHandler = (Page)HttpContext.Current.Handler;

            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("myuploads", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("myuploads?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

            NewMenu.LoggedInUser = myUser;
            NewMenu.Upload = true;
            NewMenu.HighlightButtonsForPage(PageIndex.MyUploads, "My Uploads");

            UploadMediaControl.Initialise(myUser, this);
                if (myUser.AccountType == "admin" || myUser.AccountType == "member" || myUser.AccountType == "media")
                {
                    uploadForm.Visible = true;
                }

                var mediaControls = MediaControl.GetUserMediaAsMediaControls(myUser.Id, myUser.Id);
                if (mediaControls.Count != default)
                    UploadPrompt.Visible = false;

                foreach (MediaControl mc in mediaControls)
                {
                    MyMediaContainer.Controls.Add(mc);
                }
            


            if (string.IsNullOrEmpty(Data.DeviceType))
            {
                Response.Redirect("get_device_info?url=home");
            }

            new LogEntry(Log.Debug)
            {
                Text = string.Format("The home page was accessed by {0} from {1} {2} device.",
                myUser == null ? "someone who was not logged in" : myUser.Forename,
                myUser == null ? "their" : myUser.PosessivePronoun, Data.DeviceType)
            };

            //WelcomeLabel.Text =
            //    string.Format("Hiya {0} to the parsnip website!", myUser == null ?
            //    "stranger, welcome" : myUser.Forename + ", welcome back");

            var myImage = new ParsnipData.Media.Image();
            myImage.Id = MediaId.NewMediaId();
            myImage.Compressed = "Resources/Media/Images/Local/Dirt_On_You.jpg";
            myImage.Placeholder = "Resources/Media/Images/Local/Dirt_On_You.jpg";

            if (myUser != null)
                myImage.Title = $"Hey {myUser.Forename}, what DIRT do we have on YOU? 😜";
            else
                myImage.Title = $"What DIRT do we have on YOU? 😜";

            var MySeeYourselfControl = (MediaControl)Page.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
            MySeeYourselfControl.MyMedia = myImage;
            MySeeYourselfControl.AnchorLink = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/me";

            //seeYourself.Controls.Add(MySeeYourselfControl);
            //seeYourself.Visible = true;

            int userId = myUser == null ? 0 : myUser.Id;
            Media latestVideo = Media.SelectLatestVideo(userId);
            if (latestVideo != null)
            {
                var MyVideoControl = (MediaControl)Page.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");


                latestVideo.Title = "LATEST VIDEO: " + latestVideo.Title;
                MyVideoControl.MyMedia = latestVideo;

                //LatestVideo.Controls.Add(MyVideoControl);
            }
        }
    }
}