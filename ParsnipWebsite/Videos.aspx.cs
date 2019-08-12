using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipWebsite.Custom_Controls.Media;

namespace ParsnipWebsite
{
    public partial class Videos : System.Web.UI.Page
    {
        private User myUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage("videos", this, Data.DeviceType);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (ParsnipData.Media.Video video in ParsnipData.Media.Video.GetAllVideos())
            {

                var MyVideoControl = (MediaControl)LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyVideoControl.MyVideo = video;
                links_div.Controls.Add(MyVideoControl);
            }

            foreach (ParsnipData.Media.YoutubeVideo youtubeVideo in ParsnipData.Media.YoutubeVideo.GetAllYoutubeVideos())
            {
                var MyVideoControl = (MediaControl)LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyVideoControl.MyYoutubeVideo = youtubeVideo;
                links_div.Controls.Add(MyVideoControl);
            }
        }


    }
}