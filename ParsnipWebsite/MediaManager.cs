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

                        string uploadsDir = string.Format("Resources/Media/Images/Uploads/");
                        string originalFileName = string.Format("{0}{1}_{2}_{3}_{4}",
                            uploader.Forename, uploader.Surname, Guid.NewGuid(),
                            Parsnip.AdjustedTime.ToString("dd-MM-yyyy"), myFileName);

                        string thumbnailFileName = originalFileName.Substring(0, originalFileName.LastIndexOf('.')) + ".jpg";

                        Debug.WriteLine("Newdir = " + uploadsDir);
                        uploadControl.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("~/" + uploadsDir + originalFileName));

                        Bitmap newBitMap = new System.Drawing.Bitmap(uploadControl.PostedFile.InputStream);
                        Bitmap thumbnail = ResizeBitmap(newBitMap, (int)(newBitMap.Width * 0.05), (int)(newBitMap.Height * 0.05));
                        thumbnail.Save(HttpContext.Current.Server.MapPath(uploadsDir + "5_percent_thumbnails/" + thumbnailFileName));
                        ParsnipData.Media.Image temp = new ParsnipData.Media.Image(uploadsDir + originalFileName, uploader, album);
                        temp.Placeholder = uploadsDir + "5_percent_thumbnails/" + thumbnailFileName;
                        temp.Update();
                        HttpContext.Current.Response.Redirect("edit_media?id=" + temp.Id);
                    }
                    else
                    {
                        //Really, this should redirect to the corresponding album page
                        HttpContext.Current.Response.Redirect("photos?error=video");
                    }
                }
                catch (Exception err)
                {
                    new LogEntry(DebugLog) { text = "There was an exception whilst uploading the photo: " + err };
                }
            }

            Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
            {
                Bitmap result = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.DrawImage(bmp, 0, 0, width, height);
                }

                return result;
            }
        }
    }
}