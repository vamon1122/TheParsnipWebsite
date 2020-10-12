using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using ParsnipData.Media;
using ParsnipData.Accounts;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class MediaControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public ParsnipData.Media.MediaTag MyMediaTag { get; set; }

        public ParsnipData.Media.MediaUserPair MyMediaUserPair { get; set; }

        ParsnipData.Media.Media _myMedia;
        public ParsnipData.Media.Media MyMedia
        {
            get
            {
                return _myMedia;
            }
            set {
                _myMedia = value;
                SetContainerWidth();
                MyTitle.InnerHtml = value.Title;
                MyTitle.ID = value.Id.ToString();
                MyEdit.HRef = $"../../edit_media?id={value.Id}";
                if(MyMediaTag != null && MyMediaTag.Id != default)
                {
                    MyEdit.HRef += $"&tag={MyMediaTag.Id}";
                }

                if (MyMediaUserPair != null && MyMediaUserPair.UserId != default)
                {
                    MyEdit.HRef += $"&user={MyMediaUserPair.UserId}";
                }

                if (value.Type == "image")
                {
                    if (value.XScale != default || value.YScale != default)
                    {
                        MyImageHolder.Style.Add("height", string.Format("{0}vmin", MyImageHolder.Width.Value * ((double)value.YScale / value.XScale)));
                        MyImageHolder.Style.Add("max-height", string.Format("{0}px", maxWidth * ((double)value.YScale / value.XScale)));
                    }

                    MyAnchorLink.HRef = string.Format("../../view?id={0}", value.Id);

                    MyImageHolder.Visible = true;
                    MyImageHolder.Style.Add("margin-bottom", "8px");
                    MyImageHolder.ImageUrl = value.Placeholder.Contains("http://") || value.Placeholder.Contains("https://") ? value.Placeholder : Request.Url.GetLeftPart(UriPartial.Authority) + "/" + value.Placeholder;
                    MyImageHolder.Attributes.Add("data-src", value.Compressed.Contains("http://") || value.Compressed.Contains("https://") ? value.Compressed : Request.Url.GetLeftPart(UriPartial.Authority) + "/" + value.Compressed);
                    MyImageHolder.Attributes.Add("data-srcset", value.Compressed.Contains("http://") || value.Compressed.Contains("https://") ? value.Compressed : Request.Url.GetLeftPart(UriPartial.Authority) + "/" + value.Compressed);
                    
                }
                else if(_myMedia.Type == "video" || _myMedia.Type == "youtube")
                {
                    if (value.XScale != default || value.YScale != default)
                    {
                        thumbnail.Style.Add("height", string.Format("{0}vmin", width * ((double)value.YScale / value.XScale)));
                        thumbnail.Style.Add("max-height", string.Format("{0}px", maxWidth * ((double)value.YScale / value.XScale)));
                    }

                    a_play_video.Visible = true;
                    a_play_video.HRef = string.Format("../../view?id={0}", value.Id);

                    if(_myMedia.Type == "video")
                    {
                        thumbnail.Src = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + value.Placeholder;
                        thumbnail.Attributes.Add("data-src", Request.Url.GetLeftPart(UriPartial.Authority) + "/" + value.Compressed);
                        thumbnail.Attributes.Add("data-srcset", Request.Url.GetLeftPart(UriPartial.Authority) + "/" + value.Compressed);
                    }
                    else //YoutubeVideo
                    {
                        thumbnail.Src = value.Placeholder.Contains("https://") ? value.Placeholder : $"{Request.Url.GetLeftPart(UriPartial.Authority)}/{value.Placeholder}";
                        thumbnail.Attributes.Add("data-src", value.Compressed.Contains("https://") ? value.Compressed : $"{Request.Url.GetLeftPart(UriPartial.Authority)}/{value.Compressed}");
                        thumbnail.Attributes.Add("data-srcset", value.Compressed.Contains("https://") ? value.Compressed : $"{Request.Url.GetLeftPart(UriPartial.Authority)}/{value.Compressed}");
                    }
                    
                }

                if(MyMedia.Status != null)
                {
                    if (MyMedia.Status.Equals(MediaStatus.Unprocessed))
                        MyTitle.InnerHtml += " ⚫ - Unprocessed";
                    else if (MyMedia.Status.Equals(MediaStatus.Processing))
                        MyTitle.InnerHtml += " 🔵 - Processing...";
                    else if (MyMedia.Status.Equals(MediaStatus.Error))
                        MyTitle.InnerHtml += " 🔴 - Error whilst processing";
                }

                GenerateShareButton();
            }

        }

        public string ShareLink
        {
            get
            {
                if(_anchorLink != null)
                {
                    return _anchorLink;
                }

                if (MyMedia.MyMediaShare == null || MyMedia.MyMediaShare.Id.ToString() == default)
                {
                    return "You must log in to share media";
                }
                else
                {
                    return Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" + MyMedia.MyMediaShare.Id;
                    /*
                    if(MyMedia.Type == "image")
                        return Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" + MyMedia.MyMediaShare.Id;
                    else
                        return Request.Url.GetLeftPart(UriPartial.Authority) + "/watch?share=" + MyMedia.MyMediaShare.Id;
                        */

                };
            }
        }

        public string AnchorLink { get { return _anchorLink; } set { _anchorLink = value; MyAnchorLink.HRef = value; } }
        private string _anchorLink;



        private ParsnipData.Media.Image _myImage;

        double width;
        double maxWidth = 480;

        private void SetContainerWidth()
        {
            width = 100;
            //width = Data.IsMobile ? 100 : 30;
            //min_width = Data.IsMobile ? 0 : 480;
            
            
            
            MediaContainer.Style.Add("width", string.Format("{0}vmin", width));
            MediaContainer.Style.Add("max-width", string.Format("{0}px", maxWidth));
            //MediaContainer.Style.Add("min-width", string.Format("{0}px", min_width));

        }

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

        public static List<MediaControl> GetMediaUserPairAsMediaControls(int mediaTagUserId)
        {
            var mediaControls = new List<MediaControl>();
            Page httpHandler = (Page)HttpContext.Current.Handler;
            int loggedInUserId = ParsnipData.Accounts.User.LogIn().Id;

            foreach (ParsnipData.Media.Media temp in MediaUserPair.GetAllMedia(mediaTagUserId, loggedInUserId))
            {
                MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                myMediaControl.MyMediaUserPair = new MediaUserPair() { UserId = mediaTagUserId, MediaId = temp.Id };
                myMediaControl.MyMedia = temp;
                mediaControls.Add(myMediaControl);
            }

            return mediaControls.OrderByDescending(mediaControl => mediaControl.MyMedia.DateTimeCaptured).ToList();
        }

        public static List<MediaControl> GetAlbumAsMediaControls(MediaTag mediaTag)
        {
            var mediaControls = new List<MediaControl>();
            Page httpHandler = (Page)HttpContext.Current.Handler;
            int loggedInUserId = ParsnipData.Accounts.User.LogIn().Id;

            foreach (ParsnipData.Media.Media temp in mediaTag.GetAllMedia(loggedInUserId))
            {
                MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                myMediaControl.MyMediaTag = mediaTag;
                myMediaControl.MyMedia = temp;
                mediaControls.Add(myMediaControl);
            }

            return mediaControls.OrderByDescending(mediaControl => mediaControl.MyMedia.DateTimeCaptured).ToList();
        }

        public static List<MediaControl> GetUserMediaAsMediaControls(int userId, int loggedInUserId)
        {
            var mediaControls = new List<MediaControl>();
            Page httpHandler = (Page)HttpContext.Current.Handler;

            foreach (ParsnipData.Media.Media temp in ParsnipData.Media.Media.SelectByUserId(userId, loggedInUserId))
            {
                MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                myMediaControl.MyMedia = temp;
                mediaControls.Add(myMediaControl);
            }

            return mediaControls;
        }
    }
}