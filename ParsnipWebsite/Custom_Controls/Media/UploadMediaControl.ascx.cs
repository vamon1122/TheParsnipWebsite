using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;
using ParsnipData.Accounts;
using System.Diagnostics;
using ParsnipData.Logging;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class UploadMediaControl : System.Web.UI.UserControl
    {
        User LoggedInUser;
        ParsnipData.Media.MediaTag MyMediaTag;
        Page myPage;

        public void Initialise(User loggedInUser, MediaTag myMediaTag, Page page)
        {
            myPage = page;
            LoggedInUser = loggedInUser;
            MyMediaTag = myMediaTag;
            if (LoggedInUser.AccountType == "admin" || LoggedInUser.AccountType == "member")
                UploadDiv.Style.Clear();
        }

        public void Initialise(User loggedInUser, Page page)
        {
            myPage = page;
            LoggedInUser = loggedInUser;
            MyMediaTag = null;
            if (LoggedInUser.AccountType == "admin" || LoggedInUser.AccountType == "member")
                UploadDiv.Style.Clear();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Session["MediaUpload"] == null && MediaUpload.HasFile)
                {
                    Session["MediaUpload"] = MediaUpload;
                }
                else if (Session["MediaUpload"] != null && !MediaUpload.HasFile)
                {
                    MediaUpload = (FileUpload)Session["MediaUpload"];
                }
                else if (MediaUpload.HasFile)
                {
                    Session["MediaUpload"] = MediaUpload;
                }

                
                
                if (MediaUpload.PostedFile.ContentLength > 0)
                {
                    if (ThumbnailUpload.PostedFile.ContentLength > 0)
                    {
                        UploadVideo(LoggedInUser, MediaUpload, ThumbnailUpload, MyMediaTag);
                    }
                    else
                    {
                        string[] fileDir = MediaUpload.PostedFile.FileName.Split('\\');
                        string originalFileName = fileDir.Last();
                        string originalFileExtension = originalFileName.Split('.').Last();

                        if (ParsnipData.Media.Image.IsValidFileExtension(originalFileExtension))
                            UploadImage(LoggedInUser, MediaUpload, MyMediaTag);
                        else if (Video.IsValidFileExtension(originalFileExtension))
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openUploadThumbnail();", true);
                        }
                    }
                }
            }
        }

        protected void Button_UploadDataId_Click(object sender, EventArgs e)
        {
            var rawDataId = TextBox_UploadDataId.Text;
            var dataId = Youtube.ParseDataId(TextBox_UploadDataId.Text);
            if(dataId == null)
            {
                YoutubeError.Visible = true;
            }
            else
            {
                Youtube myYoutube = new Youtube(dataId, LoggedInUser);
                myYoutube.Scrape();
                myYoutube.Insert();

                if (MyMediaTag != null)
                {
                    var mediaTagPair = new MediaTagPair(myYoutube, MyMediaTag, LoggedInUser);
                    mediaTagPair.Insert();
                }
                
                if (MyMediaTag == null)
                    Response.Redirect($"edit_media?id={myYoutube.Id}", false);
                else
                    Response.Redirect($"edit_media?id={myYoutube.Id}&tag={MyMediaTag.Id}", false);
            }
        }
        public static void UploadImage(User uploader, FileUpload uploadControl, MediaTag mediaTag)
        {
            try
            {
                ParsnipData.Media.Image myImage = new ParsnipData.Media.Image(uploader, uploadControl.PostedFile);
                myImage.Insert();

                if(mediaTag != null)
                {
                    var mediaTagPair = new MediaTagPair(myImage, mediaTag, uploader);
                    mediaTagPair.Insert();
                }

                if (mediaTag == null)
                    HttpContext.Current.Response.Redirect($"edit_media?id={myImage.Id}", false);
                else
                    HttpContext.Current.Response.Redirect($"edit_media?id={myImage.Id}&tag={mediaTag.Id}", false);
            }
            catch (Exception ex)
            {
                var e = "Exception whilst uploading image: " + ex;
                new LogEntry(Log.Debug) { Text = e };
                Debug.WriteLine(e);
                HttpContext.Current.Response.Redirect("photos?error=video");
            }
        }

        public static void UploadVideo(User uploader, FileUpload videoUpload, FileUpload thumbnailUpload, MediaTag mediaTag)
        {
            try
            {
                ParsnipData.Media.Video myVideo = new ParsnipData.Media.Video(uploader, videoUpload.PostedFile, thumbnailUpload.PostedFile);
                myVideo.Insert();

                if (mediaTag != null)
                {
                    var mediaTagPair = new MediaTagPair(myVideo, mediaTag, uploader);
                    mediaTagPair.Insert();
                }

                if (mediaTag == null)
                    HttpContext.Current.Response.Redirect($"edit_media?id={myVideo.Id}", false);
                else
                    HttpContext.Current.Response.Redirect($"edit_media?id={myVideo.Id}&tag={mediaTag.Id}", false);
            }
            catch (Exception ex)
            {
                var e = "Exception whilst uploading video: " + ex;
                new LogEntry(Log.Debug) { Text = e };
                Debug.WriteLine(e);
                HttpContext.Current.Response.Redirect("photos?error=video");
            }
        }
    }
}