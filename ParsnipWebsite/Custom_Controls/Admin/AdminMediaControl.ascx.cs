using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using ParsnipData.Media;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class AdminMediaControl : CommonMediaControl
    {
        protected override void SetMediaLogic(ParsnipData.Media.Media value)
        {
            SetContainerWidth(MediaContainer);
            MyTitle.InnerHtml = value.Title;
            MyTitle.ID = value.Id.ToString();
            MyEdit.HRef = $"../../edit?id={value.Id}";
            if (MyMediaTag != null && MyMediaTag.Id != default)
            {
                MyEdit.HRef += $"&tag={MyMediaTag.Id}";
            }

            if (MyMediaUserPair != null && MyMediaUserPair.UserId != default)
            {
                MyEdit.HRef += $"&user={MyMediaUserPair.UserId}";
            }

            if (MySearch != null)
            {
                MyEdit.HRef += $"&search={MySearch}";
            }

            if (Redirect != null)
            {
                MyEdit.HRef += $"&redirect={Redirect}?focus={MyMedia.Id}";
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
            else if (_myMedia.Type == "video" || _myMedia.Type == "youtube")
            {
                if (value.XScale != default || value.YScale != default)
                {
                    thumbnail.Style.Add("height", string.Format("{0}vmin", width * ((double)value.YScale / value.XScale)));
                    thumbnail.Style.Add("max-height", string.Format("{0}px", maxWidth * ((double)value.YScale / value.XScale)));
                }

                a_play_video.Visible = true;
                a_play_video.HRef = string.Format("../../view?id={0}", value.Id);

                if (_myMedia.Type == "video")
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

            if (MyMedia.Status != null)
            {
                if (MyMedia.Status.Equals(MediaStatus.Unprocessed))
                    MyTitle.InnerHtml += " ⚫ ";
                else if (MyMedia.Status.Equals(MediaStatus.Processing))
                    MyTitle.InnerHtml += " 🔵 ";
                else if (MyMedia.Status.Equals(MediaStatus.Error))
                    MyTitle.InnerHtml += " 🔴 ";
            }

            GenerateShareButton();
        }

        protected override void SetAnchorLink(string value)
        {
            MyAnchorLink.HRef = value;
        }

        protected override void GenerateShareButton()
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
                "   </div>\n", tempGuid, ShareLink);
        }

        public static List<AdminMediaControl> GetUserMediaAsAdminMediaControls(int userId, int loggedInUserId, PageIndex redirect)
        {
            var mediaControls = new List<AdminMediaControl>();
            Page httpHandler = (Page)HttpContext.Current.Handler;

            foreach (ParsnipData.Media.Media temp in ParsnipData.Media.Media.SelectByUserId(userId, loggedInUserId))
            {
                AdminMediaControl myAdminMediaControl = (AdminMediaControl)httpHandler.LoadControl("~/Custom_Controls/Admin/AdminMediaControl.ascx");
                myAdminMediaControl.Redirect = redirect.ToString();
                myAdminMediaControl.MyMedia = temp;
                mediaControls.Add(myAdminMediaControl);
            }

            return mediaControls;
        }
    }
}