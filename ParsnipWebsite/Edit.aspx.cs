﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Media;
using ParsnipData.Logging;
using ParsnipData;
using System.Diagnostics;
using ParsnipWebsite.Custom_Controls.Media;
using System.Security.Cryptography.X509Certificates;

namespace ParsnipWebsite
{
    public partial class Edit_Media : ParsnipPage
    {
        User myUser;
        private ParsnipData.Media.Media MyImage;
        private Video myVideo;
        public Video MyVideo { get { return myVideo ?? MyYoutubeVideo; } set { myVideo = value; } }
        private Youtube MyYoutubeVideo;
        private MediaShare myMediaShare;
        private bool isNew;

        public Media MyMedia
        {
            get
            {
                if (MyYoutubeVideo != null)
                    return MyYoutubeVideo;

                if (MyVideo != null)
                    return MyVideo;

                if (MyImage != null)
                    return MyImage;

                return null;
            }
        }
        string OriginalAlbumRedirect;
        protected void Page_Load(object sender, EventArgs e)
        {
            var tagParam = Request.QueryString["tag"];
            var userTagParam = Request.QueryString["user"];
            var Search = Request.QueryString["search"];
            var redirect = Request.QueryString["redirect"];

            Login();

            RemoveTags();
            
            GetMedia(); //CheckPermissions() is dependent on this for CreatedByUserId

            NewMenu.LoggedInUser = myUser;
            NewMenu.HighlightButtonsForPage(PageIndex.Tag, "Edit");

            MediaTag OriginalTag = string.IsNullOrEmpty(tagParam) ? null : new MediaTag(Convert.ToInt32(tagParam));

            GetOriginalRedirect();

            CheckPermissions();

            string id = Request.QueryString["id"];
            
            if (!string.IsNullOrEmpty(id))
            {
                //REQUIRED TO VIEW POSTBACK
                form1.Action = Request.RawUrl;

                if (Request.QueryString["delete"] != null)
                {
                    DoDeleteMedia();

                    Response.Redirect(OriginalAlbumRedirect);
                }

                CheckForThumbnailUpload();

                if(!IsPostBack) InitialiseTags();

                PopulateTagDropDowns();

                DisplayMediaAttributes();

                if (!IsPostBack)
                    InputTitleTwo.Focus();
            }
            else
            {
                Response.Redirect("home");
            }

            void Login()
            {
                myUser = Account.SecurePage(this, Data.DeviceType);

                NewMenu.SelectedPage = PageIndex.EditMedia;
                NewMenu.LoggedInUser = myUser;
                NewMenu.Share = true;
            }

            void GetMedia()
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
                {
                    MyYoutubeVideo = ParsnipData.Media.Youtube.Select(new MediaId(Request.QueryString["id"]), myUser.Id);
                    if (MyYoutubeVideo == null)
                        MyVideo = ParsnipData.Media.Video.Select(new MediaId(Request.QueryString["id"]), myUser.Id);

                    if (MyYoutubeVideo == null && MyVideo == null)
                        MyImage = ParsnipData.Media.Image.Select(new MediaId(Request.QueryString["id"]), myUser.Id);
                }

                if (MyYoutubeVideo != null)
                {
                    isNew = Session[$"{MyYoutubeVideo.Id}_IsNew"] == null ? MyYoutubeVideo.IsNew : Convert.ToBoolean(Session[$"{MyYoutubeVideo.Id}_IsNew"]);
                    MediaShare myMediaShare = MyYoutubeVideo.MyMediaShare;
                    if (myMediaShare == null)
                    {
                        myMediaShare = new MediaShare(MyYoutubeVideo.Id, myUser.Id);
                        myMediaShare.Insert();
                    }
                    ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" +
                    myMediaShare.Id;
                    Page.Title = "Edit Youtube Video";
                }
                else if (MyVideo != null)
                {
                    MyVideo = Video.Select(new MediaId(Request.QueryString["id"]), myUser.Id);
                    MediaShare myMediaShare = MyVideo.MyMediaShare;
                    if (myMediaShare == null)
                    {
                        myMediaShare = new MediaShare(MyVideo.Id, myUser.Id);
                        myMediaShare.Insert();
                    }
                    ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" +
                    myMediaShare.Id;
                    Page.Title = "Edit Video";
                }
                else if (MyImage != null)
                {

                    myMediaShare = MyImage.MyMediaShare;
                    if (myMediaShare == null)
                    {
                        myMediaShare = new MediaShare(MyImage.Id, myUser.Id);
                        myMediaShare.Insert();
                    }
                    ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" +
                    myMediaShare.Id;
                    Page.Title = "Edit Image";
                }
                else
                {
                    Response.Redirect("myuploads");
                }
            }

            void InitialiseTags()
            {
                Page httpHandler = (Page)HttpContext.Current.Handler;
                var tagText = new List<Tuple<char, string>>(); 
                foreach (MediaTagPair mediaTagPair in MyMedia.MediaTagPairs)
                {
                    MediaTagPairControl mediaTagPairControl = (MediaTagPairControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaTagPairControl.ascx");
                    mediaTagPairControl.MyMedia = MyMedia;
                    mediaTagPairControl.MyPair = mediaTagPair;
                    mediaTagPairControl.redirect = $"{HttpContext.Current.Request.Url}&removetag={mediaTagPair.MediaTag.Id}";
                    tagText.Add(new Tuple<char, string>('#', mediaTagPair.MediaTag.Name));
                }
                foreach (MediaUserPair mediaUserPair in MyMedia.MediaUserPairs)
                {
                    MediaUserPairControl mediaUserPairControl = (MediaUserPairControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaUserPairControl.ascx");
                    mediaUserPairControl.MyMedia = MyMedia;
                    mediaUserPairControl.MyPair = mediaUserPair;
                    mediaUserPairControl.redirect = $"{HttpContext.Current.Request.Url}&removeusertag={mediaUserPair.UserId}";
                    tagText.Add(new Tuple<char, string>('@', mediaUserPair.Name));
                }
                foreach (var textTag in tagText.OrderBy(x => x.Item2)) TagText.Text += $"{textTag.Item1}{textTag.Item2} ";
            }

            void GetOriginalRedirect()
            {


                if (OriginalTag == null && tagParam == null && userTagParam == null && MyMedia.AlbumId == default && MyMedia.MediaTagPairs.Count == default && !string.IsNullOrEmpty(OriginalAlbumRedirect) && string.IsNullOrEmpty(redirect))
                {
                    OriginalAlbumRedirect = "myuploads?focus=" + MyMedia.Id.ToString();
                }
                else if (!string.IsNullOrEmpty(Search))
                {
                    OriginalAlbumRedirect = $"search?text={Search}";
                }
                else if (OriginalTag != null)
                {
                    switch (OriginalTag.Id)
                    {
                        case (int)Data.MediaTagIds.Photos:
                            OriginalAlbumRedirect = "photos?focus=" + MyMedia.Id.ToString();
                            break;
                        case (int)Data.MediaTagIds.Memes:
                            OriginalAlbumRedirect = "memes?focus=" + MyMedia.Id.ToString();
                            break;
                        case (int)Data.MediaTagIds.Krakow:
                            OriginalAlbumRedirect = "krakow?focus=" + MyMedia.Id.ToString();
                            break;
                        case (int)Data.MediaTagIds.Videos:
                            OriginalAlbumRedirect = "videos?focus=" + MyMedia.Id.ToString();
                            break;
                        case (int)Data.MediaTagIds.Portugal:
                            OriginalAlbumRedirect = "portugal?focus=" + MyMedia.Id.ToString();
                            break;
                        case (int)Data.MediaTagIds.Amsterdam:
                            OriginalAlbumRedirect = "amsterdam?focus=" + MyMedia.Id.ToString();
                            break;
                        case default(int):
                            Debug.WriteLine(string.Format("The album id {0} was not recognised!",
                                MyMedia.AlbumId));
                            OriginalAlbumRedirect = "home?error=nomediaalbum4";
                            break;
                        default:
                            OriginalAlbumRedirect = $"tag?id={OriginalTag.Id}&focus={MyMedia.Id}";
                            break;
                    }
                }
                else if (!string.IsNullOrEmpty(redirect))
                {
                    OriginalAlbumRedirect = redirect;
                }
                else
                {
                    if (userTagParam != null)
                        OriginalAlbumRedirect = $"tag?user={userTagParam}&focus={MyMedia.Id}";
                    else if (tagParam != null)
                        OriginalAlbumRedirect = $"tag?id={tagParam}&focus={MyMedia.Id}";
                    else
                        OriginalAlbumRedirect = $"myuploads?focus={MyMedia.Id}";
                    //else if (MyMedia.MediaTagPairs.Count != default)
                    //    OriginalAlbumRedirect = $"tag?id={MyMedia.MediaTagPairs[0].MediaTag.Id}&focus={MyMedia.Id}";

                }
            }

            void PopulateTagDropDowns()
            {
                NewAlbumsDropDown.Items.Clear();
                NewAlbumsDropDown.Items.Add(new ListItem() { Value = "", Text = "(No tag selected)" });
                foreach (MediaTag tempMediaTag in MediaTag.GetAllTags()) NewAlbumsDropDown.Items.Add(tempMediaTag.Name);

                DropDown_SelectUser.Items.Clear();
                DropDown_SelectUser.Items.Add(new ListItem() { Value = "", Text = "(No user selected)" });
                foreach (User user in ParsnipData.Accounts.User.GetAllUsers())
                {
                    DropDown_SelectUser.Items.Add(new ListItem()
                    {
                        Value = user.Username,
                        Text = user.FullName
                    });
                }
            }
        
            void DoDeleteMedia()
            {
                bool deleteSuccess;

                if (myUser.AccountType == "admin")
                {
                    MyMedia.Delete();
                    deleteSuccess = true;
                }
                else
                {
                    new LogEntry(Log.General)
                    {
                        Text = string.Format("{0} tried to delete media called \"{1}\", but {2} was not allowed " +
                        "because {2} is not an admin", myUser.FullName, MyMedia.Title,
                        myUser.SubjectiveGenderPronoun)
                    };
                    deleteSuccess = false;
                }
            }

            void CheckPermissions()
            {
                if (MyMedia.CreatedById.ToString() != myUser.Id.ToString())
                {

                    if (myUser.AccountType == "admin" || myUser.AccountType == "media")
                    {
                        string accountType = myUser.AccountType == "admin" ? "admin" : "approved media editor";
                        new LogEntry(Log.General)
                        {
                            Text = string.Format("{0} started editing media called \"{1}\". {2} does not own the " +
                            "media but {3} is allowed since {3} is an {4}", myUser.FullName, MyMedia.Title,
                            myUser.SubjectiveGenderPronoun.First().ToString().ToUpper() +
                            myUser.SubjectiveGenderPronoun.Substring(1), myUser.SubjectiveGenderPronoun, accountType)
                        };
                    }
                    else
                    {
                        new LogEntry(Log.General)
                        {
                            Text = string.Format("{0} attempted to edit media called \"{1}\" which {2} " +
                        "did not own. Access was DENIED!", myUser.FullName, MyMedia.Title, myUser.SubjectiveGenderPronoun)
                        };

                        Response.Redirect($"{OriginalAlbumRedirect}&alert=P100");
                    }
                }
            }

            void DisplayMediaAttributes()
            {
                if (string.IsNullOrEmpty(InputTitleTwo.Text)) InputTitleTwo.Text = MyMedia.Title;

                if (myUser.AccountType == "admin") btn_AdminDelete.Visible = true;

                if(string.IsNullOrEmpty(SearchTerms_Input.Text)) SearchTerms_Input.Text = MyMedia.SearchTerms;

                if(string.IsNullOrEmpty(input_date_media_captured.Value)) input_date_media_captured.Value = MyMedia.DateTimeCaptured.ToString();

                if (MyVideo?.Thumbnails?.Count() > 0)
                {
                    ThumbnailSelectorContainer.Visible = true;
                    if (ThumbnailSelector.Controls.Count == 1)
                    {
                        ThumbnailSelector.Controls.Clear();
                        foreach (var control in VideoThumbnailControl.GetVideoAsVideoThumbnailControls(MyVideo))
                        {
                            control.VideoThumbnailClick += new EventHandler(VideoThumbnail_ButtonClick);
                            ThumbnailSelector.Controls.Add(control);
                        }
                    }
                }
            }
        
            void CheckForThumbnailUpload()
            {
                if (MyVideo != null)
                {
                    if (ThumbnailUpload.PostedFile != null && ThumbnailUpload.PostedFile.ContentLength > 0)
                    {
                        var thumbnail = new VideoThumbnail(MyVideo, myUser, ThumbnailUpload.PostedFile);
                        thumbnail.Insert();
                        ViewState["ActiveThumbnail"] = thumbnail.DisplayOrder;
                        MyVideo.Thumbnails.Add(thumbnail);
                    }

                    if (MyVideo.Thumbnails.Count > 0 || !MyVideo.Status.Equals(MediaStatus.Complete))
                    {
                        ThumbnailUploadControl.Visible = true;

                        if(MyVideo.Thumbnails.Count == 0)
                        {
                            ThumbnailsAreProcessing.Visible = true;
                        }
                    }
                }
            }
        
            void RemoveTags()
            {
                var mediaId = new MediaId(Request.QueryString["id"]);
                if (Request.QueryString["removetag"] != null)
                {
                    MediaTagPair.Delete(mediaId, Convert.ToInt32(Request.QueryString["removetag"]));
                    Response.Redirect(UriWithoutParameters("removetag"));
                }
                if (Request.QueryString["removeusertag"] != null)
                {
                    MediaUserPair.Delete(mediaId, Convert.ToInt32(Request.QueryString["removeusertag"]));
                    Response.Redirect(UriWithoutParameters("removeusertag"));
                }

                string UriWithoutParameters(string tag)
                {
                    var uri = HttpContext.Current.Request.Url;
                    var newQueryString = HttpUtility.ParseQueryString(uri.Query);
                    newQueryString.Remove(tag);
                    return $"{uri.GetLeftPart(UriPartial.Path)}?{newQueryString}";
                }
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if(MyYoutubeVideo != null) 
                isNew = MyYoutubeVideo.IsNew;
            
            Session[$"{MyMedia.Id}_IsNew"] = isNew;

            foreach (var control in ThumbnailSelector.Controls)
            {
                VideoThumbnailControl videoThumbnailControl;
                try
                {
                    videoThumbnailControl = (VideoThumbnailControl)control;
                }
                catch (InvalidCastException)
                {
                    continue;
                }

                if(ViewState["ActiveThumbnail"] != null) videoThumbnailControl.IsActive = videoThumbnailControl.Thumbnail.DisplayOrder == (short)ViewState["ActiveThumbnail"];
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Save button clicked. Saving changes...");
            if (IsPostBack)
            {
                bool changesWereSaved;
                try
                {
                    if (myUser.AccountType == "admin" || myUser.AccountType == "media" || myUser.Id.ToString() == MyMedia.CreatedById.ToString())
                    {
                        changesWereSaved = true;
                        MyMedia.Title = Request["InputTitleTwo"].ToString();

                        if (myUser.AccountType == "admin")
                        {
                            try
                            {
                                MyMedia.DateTimeCaptured = Convert.ToDateTime(Request["input_date_media_captured"]);
                            }
                            catch (Exception ex)
                            {
                                input_date_media_captured.Value = Request["input_date_media_captured"];
                                input_date_media_captured.Attributes.Add("class", "form-control is-invalid login");
                                throw ex;
                            }
                        }

                        var searchTerms = Request["SearchTerms_Input"]?.ToString();
                            MyMedia.SearchTerms = string.IsNullOrEmpty(searchTerms) ? null : searchTerms.Trim();

                        var textTagsText = Request["TagText"]?.ToString();
                        var textTags = textTagsText.Replace("#","-#").Replace("@","-@").Split(new char[] { '-' }, int.MaxValue, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();

                        foreach(var tag in textTags)
                        {
                            if (MyMedia.MediaTagPairs.Exists(x => $"#{x.MediaTag.Name}" == tag) || 
                                MyMedia.MediaUserPairs.Exists(x => $"@{x.Name}" == tag)) continue;

                            if (tag.Contains('@'))
                                new MediaUserPair(MyMedia, tag.Replace("@", string.Empty), myUser).Insert();
                            else
                                new MediaTagPair(MyMedia, new MediaTag(tag.Replace("#", string.Empty)), myUser).Insert();
                        }

                        foreach (var tag in MyMedia.MediaTagPairs)
                        {
                            if (!textTags.Contains($"#{tag.MediaTag.Name}")) MediaTagPair.Delete(tag.MediaId, tag.MediaTag.Id);
                        }

                        foreach (var tag in MyMedia.MediaUserPairs)
                        {
                            if (!textTags.Contains($"@{tag.Name}")) MediaUserPair.Delete(tag.MediaId, tag.UserId);
                        }

                        //TODO - Combine into single method / proc
                        MyMedia.Update(isNew);
                        var activeThumbnail = ViewState["ActiveThumbnail"];
                        if (activeThumbnail != null) MyVideo?.Thumbnails.Single(x => x.DisplayOrder == (short)activeThumbnail).SetAsActive();
                    }
                    else
                    {
                        changesWereSaved = false;
                        new LogEntry(Log.General)
                        {
                            Text =
                            string.Format("{0} tried to save changes to media called \"{1}\" which {2} did not own. {3} is not " +
                            "an admin or an approved media editor so {4} changes were not saved",
                            myUser.FullName, MyMedia.Title, myUser.SubjectiveGenderPronoun,
                            myUser.SubjectiveGenderPronoun.First().ToString().ToUpper() +
                            myUser.SubjectiveGenderPronoun.Substring(1), myUser.PosessivePronoun)
                        };
                    }

                    string Redirect = $"{OriginalAlbumRedirect}{(OriginalAlbumRedirect.Contains('?') ? '&' : '?')}focus={MyMedia.Id}";
                    if (changesWereSaved)
                    {
                        Response.Redirect(Redirect);
                    }
                    else
                    {
                        if (Redirect.Contains("?"))
                            Response.Redirect(Redirect + "&alert=P100");
                        else
                            Response.Redirect(Redirect + "?alert=P100");
                    }
                }
                catch
                {

                }
            }
        }
        protected void VideoThumbnail_ButtonClick(object sender, EventArgs e)
        {
            ViewState["ActiveThumbnail"] = (short)sender;
        }

        protected void AddMediaTagPair_Click(object sender, EventArgs e)
        {
            var newAlbumsDropDown = Request["NewAlbumsDropDown"];
            if(!string.IsNullOrWhiteSpace(newAlbumsDropDown)) TagText.Text += $"#{newAlbumsDropDown} ";
        }

        protected void AddMediaUserPair_Click(object sender, EventArgs e)
        {
            var dropDown_SelectUser = Request["DropDown_SelectUser"];
            if (!string.IsNullOrWhiteSpace(dropDown_SelectUser)) TagText.Text += $"@{dropDown_SelectUser} ";
        }
    }
}