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
            new LogEntry(DebugLog) { text = "POSTBACK with image" };
            if (uploadControl.PostedFile.FileName.Length > 0)
            {
                try
                {
                    new LogEntry(DebugLog) { text = "Attempting to upload the photo" };

                    string[] fileDir = uploadControl.PostedFile.FileName.Split('\\');
                    string myFileName = fileDir.Last();
                    string myFileExtension = myFileName.Split('.').Last().ToLower();

                    if (ParsnipData.Media.Image.IsValidFileExtension(myFileExtension))
                    {

                        string newDir = string.Format("Resources/Media/Images/Uploads/{0}{1}_{2}_{3}_{4}",
                            uploader.Forename, uploader.Surname, Guid.NewGuid(),
                            Parsnip.AdjustedTime.ToString("dd-MM-yyyy"), myFileName);

                        Debug.WriteLine("Newdir = " + newDir);
                        uploadControl.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("~/" + newDir));
                        ParsnipData.Media.Image temp = new ParsnipData.Media.Image(newDir, uploader, album);
                        temp.Update();
                        HttpContext.Current.Response.Redirect("edit_media?redirect=photos&id=" + temp.Id);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("photos?error=video");
                    }
                }
                catch (Exception err)
                {
                    new LogEntry(DebugLog) { text = "There was an exception whilst uploading the photo: " + err };
                }
            }
        }
    }
}