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

        public void Initialise(User loggedInUser, Page page, bool showButton = true)
        {
            myPage = page;
            LoggedInUser = loggedInUser;
            MyMediaTag = null;
            if (LoggedInUser.AccountType == "admin" || LoggedInUser.AccountType == "member")
                UploadDiv.Style.Clear();

            UploadDiv.Visible = showButton;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (MediaUpload.PostedFile.ContentLength > 0)
                {
                    if (Video.IsValidFileExtension(MediaUpload.PostedFile.FileName.Split('.').Last()))
                    {
                        LogUpload("video");
                        var myVideo = UploadVideo(LoggedInUser, MediaUpload);

                        if (MyMediaTag != null)
                        {
                            var mediaTagPair = new MediaTagPair(myVideo, MyMediaTag, LoggedInUser);
                            mediaTagPair.Insert();
                            HttpContext.Current.Response.Redirect($"edit?id={myVideo.Id}&tag={MyMediaTag.Id}", false);
                        }
                        else if (TaggedUserId != default)
                        {
                            var mediaUserPair = new MediaUserPair(myVideo, TaggedUserId, LoggedInUser);
                            mediaUserPair.Insert();
                            HttpContext.Current.Response.Redirect($"edit?id={myVideo.Id}&user={TaggedUserId}", false);
                        }
                        else
                        {
                            HttpContext.Current.Response.Redirect($"edit?id={myVideo.Id}", false);
                        }
                    }
                    else
                    {
                        string[] fileDir = MediaUpload.PostedFile.FileName.Split('\\');
                        string originalFileName = fileDir.Last();
                        string originalFileExtension = originalFileName.Split('.').Last();

                        if (ParsnipData.Media.Image.IsValidFileExtension(originalFileExtension))
                        {
                            LogUpload("image");
                            var myImage = UploadImage(LoggedInUser, MediaUpload);

                            if (MyMediaTag != null)
                            {
                                var mediaTagPair = new MediaTagPair(myImage, MyMediaTag, LoggedInUser);
                                mediaTagPair.Insert();
                                HttpContext.Current.Response.Redirect($"edit?id={myImage.Id}&tag={MyMediaTag.Id}", false);
                            }
                            else if (TaggedUserId != default)
                            {
                                var mediaUserPair = new MediaUserPair(myImage, TaggedUserId, LoggedInUser);
                                mediaUserPair.Insert();
                                HttpContext.Current.Response.Redirect($"edit?id={myImage.Id}&user={TaggedUserId}", false);
                            }
                            else
                            {
                                HttpContext.Current.Response.Redirect($"edit?id={myImage.Id}", false);
                            }
                        }
                        else
                        {
                            new LogEntry(Log.General) { Text = $"{LoggedInUser.FullName} tried to upload an invalid file: {originalFileName}" };
                        }
                    }
                }
            }
        }

        protected void Button_UploadDataId_Click(object sender, EventArgs e)
        {
            if (LoggedInUser == null)
            {
                new LogEntry(Log.General) { Text = $"Someone, who was not logged in, tried upload a youtube video!" };
                Response.Redirect($"login?alert=P102", false);
            }
            else
            {
                LogUpload("youtube");
                var rawDataId = TextBox_UploadDataId.Text;
                var dataId = Youtube.ParseDataId(TextBox_UploadDataId.Text);
                if (dataId == null)
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

                    if (TaggedUserId != default)
                    {
                        var mediaUserPair = new MediaUserPair(myYoutube, TaggedUserId, LoggedInUser);
                        mediaUserPair.Insert();
                    }

                    if (MyMediaTag != null)
                        Response.Redirect($"edit?id={myYoutube.Id}&tag={MyMediaTag.Id}", false);
                    else if (TaggedUserId != default)
                        Response.Redirect($"edit?id={myYoutube.Id}&user={TaggedUserId}", false);
                    else
                        Response.Redirect($"edit?id={myYoutube.Id}", false);
                }
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

        public static Video UploadVideo(User uploader, FileUpload videoUpload)
        {
            try
            {
                ParsnipData.Media.Video myVideo = new ParsnipData.Media.Video(uploader, videoUpload.PostedFile);
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

        public void LogUpload(string type)
        {
            var url = Page.Request.Url.PathAndQuery.Substring(1);
            var a = $"<a href=\"{url}\">{url}</a>";
            var urlNoParams = url.Split('?')[0];
            var aNoParams = $"<a href=\"{urlNoParams}\">{urlNoParams}</a>";
            var myCheck = !string.IsNullOrEmpty(MyMediaTag?.Name) || url != urlNoParams;
            new LogEntry(Log.Test) { Text = $"{LoggedInUser.FullName} uploaded {(type == "image" ? "an" : "a")} {type}{(type == "youtube" ? " video" : string.Empty)} from {(MyMediaTag?.Name?.ToLower() == urlNoParams ? urlNoParams : (string.IsNullOrEmpty(MyMediaTag?.Name) ? $"the {(url == urlNoParams  ? a : urlNoParams)} page" : $"#{MyMediaTag.Name}"))}{(myCheck ? $" ({a})" : string.Empty)}!" };
            new LogEntry(Log.Access) { Text = $"{LoggedInUser.FullName} uploaded {(type == "image" ? "an" : "a")} {type}{(type == "youtube" ? " video" : string.Empty)} from {(string.IsNullOrEmpty(MyMediaTag?.Name) || MyMediaTag.Name.ToLower() == urlNoParams ? $"the {(url == urlNoParams ? a : urlNoParams)} page" : $"#{MyMediaTag.Name}")}{(string.IsNullOrEmpty(MyMediaTag?.Name) || url == urlNoParams ? string.Empty : $" ({a})")}!" };
        }
    }
}