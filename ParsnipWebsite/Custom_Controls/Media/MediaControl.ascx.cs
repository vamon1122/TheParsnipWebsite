using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using ParsnipData.Media;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class MediaControl : System.Web.UI.UserControl
    {
        public DateTime DateTimeMediaCreated {
            get
            {
                if (MyImage != null)
                    return MyImage.DateTimeMediaCreated;

                if (MyVideo != null)
                    return MyVideo.DateTimeMediaCreated;

                if (MyYoutubeVideo != null)
                    return MyYoutubeVideo.DateTimeMediaCreated;

                 return DateTime.MinValue;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        

        #region image

        public ParsnipData.Media.Media MyMedia
        {
            get
            {
                if (MyImage != null)
                    return MyImage;
                else if (MyVideo != null)
                    return MyVideo;
                else return MyYoutubeVideo;
            }
        }

        public string ShareLink
        {
            get
            {
                if (MyMedia.MyAccessToken == null || MyMedia.MyAccessToken.Id.ToString() == Guid.Empty.ToString())
                {
                    return "You must log in to share media";
                }
                else
                {
                    if(MyImage == null)
                        return Request.Url.GetLeftPart(UriPartial.Authority) + "/watch_video?access_token=" + MyMedia.MyAccessToken.Id;
                    else
                        return Request.Url.GetLeftPart(UriPartial.Authority) + "/view_image?access_token=" + MyMedia.MyAccessToken.Id;
                };
            }
        }

        private ParsnipData.Media.Image _myImage;
        public ParsnipData.Media.Image MyImage
        {
            get { return _myImage; }
            set
            {
                MyImageHolder.Visible = true;
                _myImage = value;
                MyTitle.InnerHtml = MyImage.Title;
                Debug.WriteLine("Setting url");
                
                MyImageHolder.ImageUrl = MyImage.Placeholder.Contains("http://") || MyImage.Placeholder.Contains("https://") ? MyImage.Placeholder : Request.Url.GetLeftPart(UriPartial.Authority) + "/" + MyImage.Placeholder;
                Debug.WriteLine("Url = " + MyImageHolder.ImageUrl);

                MyImageHolder.Attributes.Add("data-src", MyImage.Directory);
                MyImageHolder.Attributes.Add("data-srcset", MyImage.Directory);
                

                MyImageHolder.Style.Add("margin-bottom", "8px");
                MediaContainer.ID = _myImage.Id.ToString();
                MyEdit.HRef = string.Format("../../edit_media?id={0}", MyImage.Id);

                GenerateShareButton();
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
                _myVideo = value;
                MyTitle.InnerHtml = MyVideo.Title;
                thumbnail.Src = MyVideo.Thumbnail.Placeholder;
                thumbnail.Attributes.Add("data-src", MyVideo.Thumbnail.Original);
                thumbnail.Attributes.Add("data-srcset", MyVideo.Thumbnail.Original);
                MediaContainer.ID = _myVideo.Id.ToString();
                a_play_video.HRef = string.Format("../../watch_video?id={0}", MyVideo.Id);
                MyEdit.HRef = string.Format("../../edit_media?id={0}", MyVideo.Id);

                GenerateShareButton();
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
                _myYoutubeVideo = value;
                MyTitle.InnerHtml = MyYoutubeVideo.Title;
                Debug.WriteLine("DataId = " + MyYoutubeVideo.DataId);
                YoutubePlayer.Attributes.Add("data-id", MyYoutubeVideo.DataId);
                MediaContainer.ID = _myYoutubeVideo.Id.ToString();
                MyTitle.InnerText = MyYoutubeVideo.Title;
                MyEdit.HRef = string.Format("../../edit_media?id={0}", MyYoutubeVideo.Id);

                GenerateShareButton();
            }
        }
        #endregion

        private void GenerateShareButton()
        {
            Guid tempGuid = Guid.NewGuid();
            ShareButton.Attributes.Add("data-target", "#share" + tempGuid);

            /* 
             * This is an ugly fix to a problem which I was struggling to work out. The HTML which is generated below
             * is for the modal which contains the media share link. Originally, this was contained in the ascx page of
             * this user control, however, this did not work because the top modal was always triggered, regardless of 
             * which media item was clicked. To fix this, the id of the modal (and therfore data-target of the share 
             * button) had to be unique. So, I tried generating unique ids in javascript. However, js isn't executed in 
             * user-controls without some extra code which I didn't understand. So then I tried running the modal div 
             * at the server so that I could set the id from the code behind. However, for whatever reason, I 
             * discovered that the modal would never be triggered if it was run at the server. So finally, I came up 
             * with this. Just generating the modal HTML in a string and then inserting it into the user-control. Not 
             * pretty, but it works :P.
             */
            modalDiv.InnerHtml = string.Format(
                "\n" +
                "   <div class=\"modal fade\" id=\"share{0}\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"shareMediaLink\" aria-hidden=\"true\">\n" +
                "       <div class=\"modal-dialog modal-dialog-centered\" role=\"document\">\n" +
                "           <div class=\"modal-content\" style=\"margin:0px; padding:0px\">\n" +
                "               <div class=\"input-group\" style=\"margin:0px; padding:0px\">\n" +
                "                   <div class=\"input-group-prepend\">" +
                "                       <span class=\"input-group-text\" id=\"inputGroup-sizing-default\">Share</span>\n" +
                "                  </div>\n" +
                "                  <input runat=\"server\" type=\"text\" id=\"ShareLink\" class=\"form-control\" onclick=\"this.setSelectionRange(0, this.value.length)\" value = \"{1}\" />\n" +
                "               </div>\n" +
                "           </div>\n" +
                "      </div>\n" +
                "   </div>\n",tempGuid, ShareLink);
        }
    }
}