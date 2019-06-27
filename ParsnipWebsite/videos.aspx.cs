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
            myUser = Account.SecurePage("videos", this, Data.DeviceType, "member");

            
                
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            List<ParsnipData.Media.Video> allVideos = ParsnipData.Media.Video.GetAllVideos();

            foreach (ParsnipData.Media.Video video in allVideos)
            {

                var MyVideoControl = (VideoControl)LoadControl("~/Custom_Controls/Media/VideoControl.ascx");
                MyVideoControl.MyVideo = video;
                links_div.Controls.Add(MyVideoControl);
                //links_div.Controls.Add(new VideoControl() { MyVideo = video });
                /*
                links_div.InnerHtml += "<div class=\"center_div\"><div runat=\"server\" id=\"MyImageContainer\" class=\"meme\" style=\"background-color:#f2f2f2; display:inline-block; padding-top:8px; padding-bottom:8px\">";
                links_div.InnerHtml += string.Format("<h3>{0}</h3>", video.Title);
                links_div.InnerHtml += string.Format("<a href=\"{0}/video_player?videoid={1}\">", Request.Url.GetLeftPart(UriPartial.Authority), video.Id, video.Thumbnail);
                links_div.InnerHtml += "<div class=\"play-button-div\">";
                links_div.InnerHtml += string.Format("<img src=\"{2}\" style=\"width:100%\" />", Request.Url.GetLeftPart(UriPartial.Authority), video.Id, video.Thumbnail);
                links_div.InnerHtml += "<span class=\"play-button-icon\"><img src=\"Resources\\Media\\Images\\Web_Media\\play_button_2.png\" /></span>";
                links_div.InnerHtml += "</div></a></div></div><hr class=\"break\" />";
                */
            }
        }


    }
}