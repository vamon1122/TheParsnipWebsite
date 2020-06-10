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
        int TaggedUserId;
        Page myPage;

        public void Initialise(User loggedInUser, MediaTag myMediaTag, Page page)
        {
            myPage = page;
            LoggedInUser = loggedInUser;
            MyMediaTag = myMediaTag;
            if (LoggedInUser.AccountType == "admin" || LoggedInUser.AccountType == "member")
                UploadDiv.Style.Clear();
        }

        public void Initialise(User loggedInUser, int taggedUserId, Page page)
        {
            myPage = page;
            LoggedInUser = loggedInUser;
            TaggedUserId = taggedUserId;
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
                        var myVideo = UploadVideo(LoggedInUser, MediaUpload, ThumbnailUpload);

                        if (MyMediaTag != null)
                        {
                            var mediaTagPair = new MediaTagPair(myVideo, MyMediaTag, LoggedInUser);
                            mediaTagPair.Insert();
                            HttpContext.Current.Response.Redirect($"edit_media?id={myVideo.Id}&tag={MyMediaTag.Id}", false);
                        }
                        else if (TaggedUserId != default)
                        {
                            var mediaUserPair = new MediaUserPair(myVideo, TaggedUserId, LoggedInUser);
                            mediaUserPair.Insert();
                            HttpContext.Current.Response.Redirect($"edit_media?id={myVideo.Id}&user={TaggedUserId}", false);
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect($"edit_media?id={myVideo.Id}", false);
                        }
                    }
                    else
                    {
                        string[] fileDir = MediaUpload.PostedFile.FileName.Split('\\');
                        string originalFileName = fileDir.Last();
                        string originalFileExtension = originalFileName.Split('.').Last();

                        if (ParsnipData.Media.Image.IsValidFileExtension(originalFileExtension))
                        {
                            var myImage = UploadImage(LoggedInUser, MediaUpload);

                            if (MyMediaTag != null)
                            {
                                var mediaTagPair = new MediaTagPair(myImage, MyMediaTag, LoggedInUser);
                                mediaTagPair.Insert();
                                HttpContext.Current.Response.Redirect($"edit_media?id={myImage.Id}&tag={MyMediaTag.Id}", false);
                            }
                            else if (TaggedUserId != default)
                            {
                                var mediaUserPair = new MediaUserPair(myImage, TaggedUserId, LoggedInUser);
                                mediaUserPair.Insert();
                                HttpContext.Current.Response.Redirect($"edit_media?id={myImage.Id}&user={TaggedUserId}", false);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect($"edit_media?id={myImage.Id}", false);
                            }
                        }
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

                if(TaggedUserId != default)
                {
                    var mediaUserPair = new MediaUserPair(myYoutube, TaggedUserId, LoggedInUser);
                    mediaUserPair.Insert();
                }
                
                if (MyMediaTag != null)
                    Response.Redirect($"edit_media?id={myYoutube.Id}&tag={MyMediaTag.Id}", false);
                else if (TaggedUserId != default)
                    Response.Redirect($"edit_media?id={myYoutube.Id}&user={TaggedUserId}", false);
                else
                    Response.Redirect($"edit_media?id={myYoutube.Id}", false);
            }
        }
        public static ParsnipData.Media.Image UploadImage(User uploader, FileUpload uploadControl)
        {
            try
            {
                ParsnipData.Media.Image myImage = new ParsnipData.Media.Image(uploader, uploadControl.PostedFile);
                myImage.Insert();
                return myImage;
            }
            catch (Exception ex)
            {
                var e = "Exception whilst uploading image: " + ex;
                new LogEntry(Log.Debug) { Text = e };
                Debug.WriteLine(e);
                HttpContext.Current.Response.Redirect("photos?error=video");
            }
            return null;
        }

        public static Video UploadVideo(User uploader, FileUpload videoUpload, FileUpload thumbnailUpload)
        {
            try
            {
                ParsnipData.Media.Video myVideo = new ParsnipData.Media.Video(uploader, videoUpload.PostedFile, thumbnailUpload.PostedFile);
                myVideo.Insert();
                return myVideo;
            }
            catch (Exception ex)
            {
                var e = "Exception whilst uploading video: " + ex;
                new LogEntry(Log.Debug) { Text = e };
                Debug.WriteLine(e);
                HttpContext.Current.Response.Redirect("photos?error=video");
            }
            return null;
        }
    }
}