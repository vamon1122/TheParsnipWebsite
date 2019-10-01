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
                    string originalFileName = fileDir.Last();
                    string originalFileExtension = "." + originalFileName.Split('.').Last();

                    if (ParsnipData.Media.Image.IsValidFileExtension(originalFileExtension.Substring(1, originalFileExtension.Length - 1).ToLower()))
                    {

                        string uploadsDir = string.Format("Resources/Media/Images/Uploads/");

                        
                        string generatedFileName = string.Format("{0}{1}_{2}_{3}_{4}",
                            uploader.Forename, uploader.Surname,
                            Parsnip.AdjustedTime.ToString("dd-MM-yyyy_HH.mm.ss"), originalFileName.Substring(0, originalFileName.LastIndexOf('.')), Guid.NewGuid());

                        

                        Debug.WriteLine("Original image saved as = " + uploadsDir);
                        uploadControl.PostedFile.SaveAs(HttpContext.Current.Server.MapPath("~/" + uploadsDir + "Originals/" + generatedFileName + originalFileExtension));

                        Bitmap original = new System.Drawing.Bitmap(uploadControl.PostedFile.InputStream);

                        //One of the numbers must be a double in order for the result to be double
                        Bitmap thumbnail = ResizeBitmap(original, (int)100, (int)(original.Height * (100d / original.Width)));


                        if (original.PropertyIdList.Contains(0x112)) //0x112 = Orientation
                        {
                            var prop = original.GetPropertyItem(0x112);
                            if (prop.Type == 3 && prop.Len == 2)
                            {
                                UInt16 orientationExif = BitConverter.ToUInt16(original.GetPropertyItem(0x112).Value, 0);
                                if (orientationExif == 8)
                                {
                                    thumbnail.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                }
                                else if (orientationExif == 3)
                                {
                                    thumbnail.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                }
                                else if (orientationExif == 6)
                                {
                                    thumbnail.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                }
                            }
                        }


                        //Change quality
                        //https://docs.microsoft.com/en-us/dotnet/api/system.drawing.image.save?view=netframework-4.8

                        ImageCodecInfo myImageCodecInfo;
                        Encoder myEncoder;
                        EncoderParameter myEncoderParameter;
                        EncoderParameters myEncoderParameters;
                        string newFileExtension = ".jpg";

                        // Get an ImageCodecInfo object that represents the JPEG codec.
                        myImageCodecInfo = GetEncoderInfo("image/jpeg");

                        // Create an Encoder object based on the GUID
                        // for the Quality parameter category.
                        myEncoder = Encoder.Quality;
                        // Create an EncoderParameters object.
                        // An EncoderParameters object has an array of EncoderParameter
                        // objects. In this case, there is only one

                        // EncoderParameter object in the array.
                        myEncoderParameters = new EncoderParameters(1);

                        myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        original.Save(HttpContext.Current.Server.MapPath(uploadsDir + generatedFileName + newFileExtension), myImageCodecInfo, myEncoderParameters);

                        myEncoderParameter = new EncoderParameter(myEncoder, 25L);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        thumbnail.Save(HttpContext.Current.Server.MapPath(uploadsDir + "Thumbnails/" + generatedFileName + newFileExtension), myImageCodecInfo, myEncoderParameters);

                    

                        ParsnipData.Media.Image temp = new ParsnipData.Media.Image(uploadsDir + generatedFileName + newFileExtension, uploader, album);

                        //FULL (Use 50 compression)
                        //Thumbnail W100 / 25 compression

                        temp.Placeholder = uploadsDir + "Thumbnails/" + generatedFileName + newFileExtension;
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
                Debug.WriteLine("Width = " + width);
                Debug.WriteLine("Height = " + height);

                Bitmap result = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.DrawImage(bmp, 0, 0, width, height);
                }

                return result;
            }


            ImageCodecInfo GetEncoderInfo(String mimeType)
            {
                int j;
                ImageCodecInfo[] encoders;
                encoders = ImageCodecInfo.GetImageEncoders();
                for (j = 0; j < encoders.Length; ++j)
                {
                    if (encoders[j].MimeType == mimeType)
                        return encoders[j];
                }
                return null;
            }
        }
    }
}