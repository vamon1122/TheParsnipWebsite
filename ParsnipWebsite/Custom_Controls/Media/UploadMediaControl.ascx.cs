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
        ParsnipData.Media.Media MyMedia;
        PageIndex RedirectTo;
        string Search;

        public string EditLink
        {
            get
            {
                if (MyMedia == null)
                    return string.Empty;

                var redirect = string.Empty;
                if (!string.IsNullOrEmpty(RedirectTo?.Value))
                    redirect += $"redirect={RedirectTo.Value}";
                else if (MyMediaTag != null)
                    redirect += $"tag={MyMediaTag.Id}";
                else if (TaggedUserId != default)
                    redirect += $"user={TaggedUserId}";
                else if (!string.IsNullOrEmpty(Search))
                    redirect += $"search={Search}";
                else if (myPage != null)
                    redirect += $"redirect={myPage.Request.Url.AbsolutePath.Substring(1, myPage.Request.Url.AbsolutePath.Length - 1)}";

                var editLink = $"edit?id={MyMedia.Id}";
                if (string.IsNullOrEmpty(redirect))
                    return editLink;
                else
                    return $"{editLink}&{redirect}";
            }
        }

        public void Initialise(User loggedInUser, MediaTag myMediaTag, Page page)
        {
            myPage = page;
            MyMediaTag = myMediaTag;
            Initialise(loggedInUser, false);
        }

        public void Initialise(User loggedInUser, int taggedUserId, Page page)
        {
            myPage = page;
            TaggedUserId = taggedUserId;
            Initialise(loggedInUser, false);
        }

        public void Initialise(User loggedInUser, Page page, bool showButton = true)
        {
            myPage = page;
            Initialise(loggedInUser, showButton);
        }

        public void Initialise(User loggedInUser, PageIndex redirectTo, bool showButton = true)
        {
            RedirectTo = redirectTo;
            Initialise(loggedInUser, showButton);
        }

        public void Initialise(User loggedInUser, string search, bool showButton = true)
        {
            Search = search;
            Initialise(loggedInUser, showButton);
        }

        private void Initialise(User loggedInUser, bool showButton)
        {
            LoggedInUser = loggedInUser;
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
                        MyMedia = myVideo;

                        if (MyMediaTag != null)
                        {
                            var mediaTagPair = new MediaTagPair(myVideo, MyMediaTag, LoggedInUser);
                            mediaTagPair.Insert();
                        }
                        else if (TaggedUserId != default)
                        {
                            var mediaUserPair = new MediaUserPair(myVideo, TaggedUserId, LoggedInUser);
                            mediaUserPair.Insert();
                        }

                        Response.Redirect(EditLink, false);
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
                            MyMedia = myImage;

                            if (MyMediaTag != null)
                            {
                                var mediaTagPair = new MediaTagPair(myImage, MyMediaTag, LoggedInUser);
                                mediaTagPair.Insert();
                            }
                            else if (TaggedUserId != default)
                            {
                                var mediaUserPair = new MediaUserPair(myImage, TaggedUserId, LoggedInUser);
                                mediaUserPair.Insert();
                            }

                            Response.Redirect(EditLink, false);
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
                    MyMedia = myYoutube;

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

                    Response.Redirect(EditLink, false);
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