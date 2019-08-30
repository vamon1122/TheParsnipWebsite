﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Media;
using ParsnipData.Logs;
using ParsnipWebsite.Custom_Controls.Media;

namespace ParsnipWebsite
{
    public partial class Home : System.Web.UI.Page
    {
        public static readonly Log DebugLog = new Log("Debug");
        private User myUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = ParsnipData.Accounts.User.GetLoggedInUser();
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

            Guid userId = myUser == null ? Guid.Empty : myUser.Id;
            Video latestVideo = Video.GetLatest(userId);
            latestVideo.Title = "LATEST VIDEO: " + latestVideo.Title;
            var MyVideoControl = (MediaControl)Page.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
            MyVideoControl.MyVideo = latestVideo;
            LatestVideo.Controls.Add(MyVideoControl);
        }
    }
}