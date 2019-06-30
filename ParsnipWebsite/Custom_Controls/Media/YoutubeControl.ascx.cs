using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class YoutubeControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public YoutubeControl()
        {
            
        }

        private ParsnipData.Media.YoutubeVideo _myYoutubeVideo;
        public ParsnipData.Media.YoutubeVideo MyYoutubeVideo
        {
            get { return _myYoutubeVideo; }
            set
            {
                _myYoutubeVideo = value;
                MyTitle.InnerHtml = MyYoutubeVideo.Title;
                /*
                thumbnail.Src = "../../" + MyYoutubeVideo.Thumbnail;

                Debug.WriteLine("Thumbnail = " + MyYoutubeVideo.Thumbnail);
                */
                YoutubePlayer.Attributes.Add("data-id", MyYoutubeVideo.DataId);

                VideoContainer.ID = _myYoutubeVideo.Id.ToString();



                //MyEdit.HRef = string.Format("../../edit_image?imageid={0}", MyVideo.Id);
                MyShare.HRef = string.Format("../../video_player?data-id={0}", MyYoutubeVideo.DataId);
            }
        }
    }
}