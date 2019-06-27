using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class VideoControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public VideoControl()
        {

        }

        private ParsnipData.Media.Video _myVideo;
        public ParsnipData.Media.Video MyVideo
        {
            get { return _myVideo; }
            set
            {
                _myVideo = value;
                MyTitle.InnerHtml = MyVideo.Title;
                thumbnail.Src = "../../" + MyVideo.Thumbnail;

                Debug.WriteLine("Thumbnail = " + MyVideo.Thumbnail);
              
                VideoContainer.ID = _myVideo.Id.ToString();



                //MyEdit.HRef = string.Format("../../edit_image?imageid={0}", MyVideo.Id);
                MyShare.HRef = string.Format("../../video_player?videoid={0}", MyVideo.Id);
            }
        }
    }
}