using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class VideoThumbnailControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public event EventHandler VideoThumbnailClick;

        private VideoThumbnail _thumbnail;
        public VideoThumbnail Thumbnail { get { return _thumbnail; } set { thumbnailButton.ImageUrl = $"../../{value.Placeholder}"; IsActive = value.Active; _thumbnail = value; } }
        public bool IsActive { set { if (value) { thumbnailButton.CssClass = "video-thumbnail video-thumbnail-selected"; } else { thumbnailButton.CssClass = "video-thumbnail"; }; } }
        public static List<VideoThumbnailControl> GetVideoAsVideoThumbnailControls(Video video)
        {
            var videoThumbnailControls = new List<VideoThumbnailControl>();
            Page httpHandler = (Page)HttpContext.Current.Handler;

            foreach (VideoThumbnail temp in video.Thumbnails)
            {
                VideoThumbnailControl myVideoThumbnailControl = (VideoThumbnailControl)httpHandler.LoadControl("~/Custom_Controls/Media/VideoThumbnailControl.ascx");
                myVideoThumbnailControl.Thumbnail = temp;
                videoThumbnailControls.Add(myVideoThumbnailControl);
            }

            return videoThumbnailControls.OrderByDescending(v => v.Thumbnail.DisplayOrder).ToList();
        }

        protected void thumbnailButton_Click(object sender, ImageClickEventArgs e)
        {
            VideoThumbnailClick(Thumbnail.DisplayOrder, e);
        }
    }
}