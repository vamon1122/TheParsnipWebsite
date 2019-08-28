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
                //MyShare.HRef = string.Format("../../view_image?id={0}", MyImage.Id);

                

                Guid tempGuid = Guid.NewGuid();
                //ShareButton.Attributes.Add("data-target", "#ct109_share" + tempGuid);
                //exampleModalCenter.ID = "share" + tempGuid;

                string shareLink;

                Debug.WriteLine("Getting share link for media item " + MyImage.Title);
                if (value.MyAccessToken == null || value.MyAccessToken.Id.ToString() == Guid.Empty.ToString())
                {
                    shareLink = "You must log in to share images";
                }
                else
                {
                    shareLink = Request.Url.GetLeftPart(UriPartial.Authority) + "/view_image?access_token=" +
                        value.MyAccessToken.Id;

                    Debug.WriteLine("Share link value = " + shareLink);
                }
                

                modalDiv.InnerHtml = "<div class='modal fade' id='share" + tempGuid + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalCenterTitle' aria-hidden='true'><div class='modal-dialog modal-dialog-centered' role='document'><div class='modal-content' style='margin:0px; padding:0px'><div runat='server' id='ShareLinkContainer' class='input-group' style='margin:0px; padding:0px'><div class='input-group-prepend'><span class='input-group-text' id='inputGroup-sizing-default'>Link</span></div><input runat='server' type='text' id='ShareLink' class='form-control' onclick='this.setSelectionRange(0, this.value.length)' value = '" + shareLink + "' /></div></div></div></div>";
                ShareButton.Attributes.Add("data-target", "#share" + tempGuid);
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
                thumbnail.Src = "../../Resources/Media/Images/Web_Media/placeholder.gif";
                thumbnail.Attributes.Add("data-src", MyVideo.Thumbnail);
                thumbnail.Attributes.Add("data-srcset", MyVideo.Thumbnail);

                Debug.WriteLine("Thumbnail = " + MyVideo.Thumbnail);

                MediaContainer.ID = _myVideo.Id.ToString();

                a_play_video.HRef = string.Format("../../watch_video?id={0}", MyVideo.Id);
                //MyShare.HRef = string.Format("../../watch_video?id={0}", MyVideo.Id);
                MyEdit.HRef = string.Format("../../edit_media?id={0}", MyVideo.Id);




                //ShareButton.Attributes.Add("data-target", "#share" + tempGuid);
                //exampleModalCenter.ID = "ctshare" + tempGuid;

                string shareLink;
                Debug.WriteLine("Getting share link for media item " + MyVideo.Title);
                if (value.MyAccessToken == null || value.MyAccessToken.Id.ToString() == Guid.Empty.ToString())
                {
                    shareLink = "You must log in to share videos";
                }
                else
                {
                    shareLink = Request.Url.GetLeftPart(UriPartial.Authority) + "/watch_video?access_token=" +
                        value.MyAccessToken.Id;

                    Debug.WriteLine("Share link value = " + shareLink);
                }
                

                Guid tempGuid = Guid.NewGuid();
                modalDiv.InnerHtml = "<div class='modal fade' id='share" + tempGuid + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalCenterTitle' aria-hidden='true'><div class='modal-dialog modal-dialog-centered' role='document'><div class='modal-content' style='margin:0px; padding:0px'><div runat='server' id='ShareLinkContainer' class='input-group' style='margin:0px; padding:0px'><div class='input-group-prepend'><span class='input-group-text' id='inputGroup-sizing-default'>Link</span></div><input runat='server' type='text' id='ShareLink' class='form-control' onclick='this.setSelectionRange(0, this.value.length)' value = '" + shareLink +"' /></div></div></div></div>";
                ShareButton.Attributes.Add("data-target", "#share" + tempGuid);
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
                //MyShare.HRef = string.Format("../../watch_video?id={0}", MyYoutubeVideo.Id);
                MyEdit.HRef = string.Format("../../edit_media?id={0}", MyYoutubeVideo.Id);

                Guid tempGuid = Guid.NewGuid();


                string shareLink;

                Debug.WriteLine("Getting share link for media item " + MyYoutubeVideo.Title);
                if (value.MyAccessToken == null || value.MyAccessToken.Id.ToString() == Guid.Empty.ToString())
                {
                    shareLink = "You must log in to share youtube videos";
                }
                else
                {
                    shareLink = Request.Url.GetLeftPart(UriPartial.Authority) + "/watch_video?access_token=" +
                        value.MyAccessToken.Id;

                    Debug.WriteLine("Share link value = " + shareLink);
                }
                
                modalDiv.InnerHtml = "<div class='modal fade' id='share" + tempGuid + "' tabindex='-1' role='dialog' aria-labelledby='exampleModalCenterTitle' aria-hidden='true'><div class='modal-dialog modal-dialog-centered' role='document'><div class='modal-content' style='margin:0px; padding:0px'><div runat='server' id='ShareLinkContainer' class='input-group' style='margin:0px; padding:0px'><div class='input-group-prepend'><span class='input-group-text' id='inputGroup-sizing-default'>Link</span></div><input runat='server' type='text' id='ShareLink' class='form-control' onclick='this.setSelectionRange(0, this.value.length)' value = '" + shareLink + "' /></div></div></div></div>";
                ShareButton.Attributes.Add("data-target", "#share" + tempGuid);

            }
        }
        #endregion
    }
}