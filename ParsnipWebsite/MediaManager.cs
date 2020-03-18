using System;
using ParsnipData.Accounts;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ParsnipData.Logs;
using ParsnipData.Media;
using ParsnipData;
using System.Diagnostics;
using ParsnipWebsite.Custom_Controls.Media;
using System.Collections.Generic;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;

namespace ParsnipWebsite
{
    public static class MediaManager
    {
        static readonly Log DebugLog = Log.Select(3);

        public static List<MediaControl> GetUserMediaAsMediaControls(int userId, int loggedInUserId)
        {
            var mediaControls = new List<MediaControl>();
            Page httpHandler = (Page)HttpContext.Current.Handler;

            foreach (ParsnipData.Media.Media temp in Media.SelectByUserId(userId, loggedInUserId))
            {
                MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                myMediaControl.MyMedia = temp;
                mediaControls.Add(myMediaControl);
            }

            return mediaControls;
        }

        public static List<MediaControl> GetAlbumAsMediaControls(MediaTag mediaTag)
        {
            var mediaControls = new List<MediaControl>();
            Page httpHandler = (Page)HttpContext.Current.Handler;
            int loggedInUserId = ParsnipData.Accounts.User.LogIn().Id;

            foreach (ParsnipData.Media.Media temp in mediaTag.GetAllMedia(loggedInUserId))
            {
                MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                myMediaControl.MyMedia = temp;
                mediaControls.Add(myMediaControl);
            }

            return mediaControls.OrderByDescending(mediaControl => mediaControl.MyMedia.DateTimeCaptured).ToList();
        }

        public static void UploadImage(User uploader, MediaTag mediaTag, FileUpload uploadControl)
        {
            try
            {
                ParsnipData.Media.Image myImage = new ParsnipData.Media.Image(uploader, mediaTag, uploadControl.PostedFile);
                myImage.Insert();
                myImage.Update();
                HttpContext.Current.Response.Redirect("edit_media?id=" + myImage.Id, false);
            }
            catch(Exception ex)
            {
                var e = "Exception whilst uploading image: " + ex;
                new LogEntry(DebugLog) { text = e };
                Debug.WriteLine(e);
                HttpContext.Current.Response.Redirect("photos?error=video");
            }
        }
    }
}