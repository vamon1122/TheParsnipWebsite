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
        static readonly Log DebugLog = new Log("debug");

        public static List<MediaControl> GetUsersMediaAsMediaControls(Guid userId)
        {
            var mediaControls = new List<MediaControl>();
            Page httpHandler = (Page)HttpContext.Current.Handler;

            foreach (ParsnipData.Media.Image temp in Media.GetImagesByUserId(userId))
            {
                MediaControl MyImageControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyImageControl.MyImage = temp;
                mediaControls.Add(MyImageControl);
            }

            foreach (ParsnipData.Media.Video video in Media.GetVideosByUserId(userId))
            {

                var MyVideoControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyVideoControl.MyVideo = video;
                mediaControls.Add(MyVideoControl);
            }

            foreach (ParsnipData.Media.YoutubeVideo youtubeVideo in Media.GetYoutubeVideosByUserId(userId))
            {
                var MyVideoControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyVideoControl.MyYoutubeVideo = youtubeVideo;
                mediaControls.Add(MyVideoControl);
            }

            return mediaControls.OrderByDescending(mc => mc.DateTimeMediaCreated).ToList();
        }

        public static List<MediaControl> GetAlbumAsMediaControls(Album album)
        {
            var mediaControls = new List<MediaControl>();
            Page httpHandler = (Page)HttpContext.Current.Handler;
            Guid loggedInUserId = ParsnipData.Accounts.User.GetLoggedInUser().Id;

            foreach (ParsnipData.Media.Image temp in album.GetAllImages())
            {
                MediaControl MyImageControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyImageControl.MyImage = temp;
                mediaControls.Add(MyImageControl);
            }

            foreach (ParsnipData.Media.Video video in album.GetAllVideos())
            {
                var MyVideoControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyVideoControl.MyVideo = video;
                mediaControls.Add(MyVideoControl);
            }

            foreach (ParsnipData.Media.YoutubeVideo youtubeVideo in album.GetAllYoutubeVideos())
            {
                var MyVideoControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyVideoControl.MyYoutubeVideo = youtubeVideo;
                mediaControls.Add(MyVideoControl);
            }

            return mediaControls.OrderByDescending(mc => mc.DateTimeMediaCreated).ToList();
        }

        public static void UploadImage(User uploader, Album album, FileUpload uploadControl)
        {
            try
            {
                Debug.WriteLine("Generating image object");
                ParsnipData.Media.Image myImage = new ParsnipData.Media.Image(uploader, album, uploadControl.PostedFile);
                Debug.WriteLine("Updating image object");
                myImage.Update();
                Debug.WriteLine("Redirecting to edit_media?id=" + myImage.Id);
                HttpContext.Current.Response.Redirect("edit_media?id=" + myImage.Id, false);
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception whilst uploading image: " + ex);
                HttpContext.Current.Response.Redirect("photos?error=video");
            }
        }
    }
}