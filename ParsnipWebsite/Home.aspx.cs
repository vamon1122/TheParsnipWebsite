using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Media;
using ParsnipData.Logs;
using ParsnipWebsite.Custom_Controls.Media;
using System.Configuration;

namespace ParsnipWebsite
{
    public partial class Home : System.Web.UI.Page
    {
        public static readonly Log DebugLog = Log.Select(3);
        private User myUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            MOTD_div.InnerHtml = ConfigurationManager.AppSettings["MOTD"];

            myUser = ParsnipData.Accounts.User.LogIn();
            if (string.IsNullOrEmpty(Data.DeviceType))
            {
                Response.Redirect("get_device_info?url=home");
            }

            new LogEntry(DebugLog) { text = string.Format("The home page was accessed by {0} from {1} {2} device.", 
                myUser == null ? "someone who was not logged in" : myUser.Forename, 
                myUser == null ? "their" : myUser.PosessivePronoun, Data.DeviceType) };

            WelcomeLabel.Text = 
                string.Format("Hiya {0} to the parsnip website!", myUser == null ?
                "stranger, welcome" : myUser.Forename + ", welcome back");

            int userId = myUser == null ? 0 : myUser.Id;
            Media latestVideo = Media.SelectLatestVideo(userId);
            var MyVideoControl = (MediaControl)Page.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");

           
                latestVideo.Title = "LATEST VIDEO: " + latestVideo.Title;
                MyVideoControl.MyMedia = latestVideo;

            LatestVideo.Controls.Add(MyVideoControl);
        }
    }
}