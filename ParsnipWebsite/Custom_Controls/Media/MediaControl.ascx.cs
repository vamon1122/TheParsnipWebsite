using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class MediaControl : System.Web.UI.UserControl
    {
        public DateTime DateTimeMediaCreated {
            get
            {
                if (MyImage != null)
                    return MyImage.DateCreated;

                if (MyVideo != null)
                    return MyVideo.DateCreated;

                if (MyYoutubeVideo != null)
                    return MyYoutubeVideo.DateCreated;

                 return DateTime.MinValue;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region image
        private ParsnipData.Media.Image _myImage;
        public ParsnipData.Media.Image MyImage
        {
            get { return _myImage; }
            set
            {
                MyImageHolder.Visible = true;
                _myImage = value;
                MyTitle.InnerHtml = MyImage.Title;
                MyImageHolder.ImageUrl = "../../Resources/Media/Images/Web_Media/placeholder.gif";
                MyImageHolder.Attributes.Add("data-src", MyImage.Directory);
                MyImageHolder.Attributes.Add("data-srcset", MyImage.Directory);
                MyImageHolder.Style.Add("margin-bottom", "8px");
                MediaContainer.ID = _myImage.Id.ToString();



                MyEdit.HRef = string.Format("../../edit_media?id={0}", MyImage.Id);
                MyShare.HRef = string.Format("../../view_image?id={0}", MyImage.Id);
            }
        }
        #endregion

        #region video
        private ParsnipData.Media.Video _myVideo;
        public ParsnipData.Media.Video MyVideo
        {
            get { return _myVideo; }
            set
            {
                a_play_video.Visible = true;
                //MyEdit.Visible = false;
                _myVideo = value;
                MyTitle.InnerHtml = MyVideo.Title;
                thumbnail.Src = "../../Resources/Media/Images/Web_Media/placeholder.gif";
                thumbnail.Attributes.Add("data-src", MyVideo.Thumbnail);
                thumbnail.Attributes.Add("data-srcset", MyVideo.Thumbnail);
                //thumbnail.Src = "../../" + MyVideo.Thumbnail;

                Debug.WriteLine("Thumbnail = " + MyVideo.Thumbnail);

                MediaContainer.ID = _myVideo.Id.ToString();



                //MyEdit.HRef = string.Format("../../edit_media?id={0}", MyVideo.Id);
                a_play_video.HRef = string.Format("../../watch_video?id={0}", MyVideo.Id);
                MyShare.HRef = string.Format("../../watch_video?id={0}", MyVideo.Id);
                MyEdit.HRef = string.Format("../../edit_media?id={0}", MyVideo.Id);
            }
        }
        #endregion

        #region Youtube video
        private ParsnipData.Media.YoutubeVideo _myYoutubeVideo;
        public ParsnipData.Media.YoutubeVideo MyYoutubeVideo
        {
            get { return _myYoutubeVideo; }
            set
            {
                YoutubePlayer.Visible = true;
                //MyEdit.Visible = false;
                _myYoutubeVideo = value;
                MyTitle.InnerHtml = MyYoutubeVideo.Title;
                YoutubePlayer.Attributes.Add("data-id", MyYoutubeVideo.DataId);
                MediaContainer.ID = _myYoutubeVideo.Id.ToString();
                MyTitle.InnerText = MyYoutubeVideo.Title;
                MyShare.HRef = string.Format("../../watch_video?id={0}", MyYoutubeVideo.Id);
                MyEdit.HRef = string.Format("../../edit_media?id={0}", MyYoutubeVideo.Id);
            }
        }
        #endregion
    }
}