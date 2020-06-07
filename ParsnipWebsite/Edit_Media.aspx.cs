using System;
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
    public partial class Edit_Media : System.Web.UI.Page
    {
        User myUser;
        private ParsnipData.Media.Media MyImage;
        private Video MyVideo;
        private Youtube MyYoutubeVideo;
        private MediaShare myMediaShare;

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
            Login();
            
            GetMedia(); //CheckPermissions() is dependent on this for CreatedByUserId

            var tagParam = Request.QueryString["tag"];
            var userTagParam = Request.QueryString["user"];
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

                GetTags();

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
                if (Request.QueryString["id"] == null)
                    myUser = Account.SecurePage("edit_media", this, Data.DeviceType);
                else if (Request.QueryString["tag"] == null)
                    myUser = Account.SecurePage($"edit_media?id={Request.QueryString["id"]}", this, Data.DeviceType);
                else
                    myUser = Account.SecurePage($"edit_media?id={Request.QueryString["id"]}&tag={Request.QueryString["tag"]}", this, Data.DeviceType);

                if (Request.QueryString["removetag"] == "true")
                {
                    MediaTagPair.Delete(new MediaId(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["tag"]));
                    if (Request.QueryString["tag"] == null)
                        Response.Redirect($"edit_media?id={Request.QueryString["id"]}");
                    else
                        Response.Redirect($"edit_media?id={Request.QueryString["id"]}&tag={Request.QueryString["tag"]}");
                }
                else if (Request.QueryString["removeusertag"] == "true")
                {
                    MediaUserPair.Delete(new MediaId(Request.QueryString["id"]), Convert.ToInt32(Request.QueryString["userid"]));
                    if (Request.QueryString["userid"] == null)
                        Response.Redirect($"edit_media?id={Request.QueryString["id"]}");
                    else
                        Response.Redirect($"edit_media?id={Request.QueryString["id"]}&userid={Request.QueryString["userid"]}");
                }
            }

            void GetMedia()
            {
                MyYoutubeVideo = ParsnipData.Media.Youtube.Select(new MediaId(Request.QueryString["id"]), myUser.Id);
                if (MyYoutubeVideo == null)
                    MyVideo = ParsnipData.Media.Video.Select(new MediaId(Request.QueryString["id"]), myUser.Id);

                if (MyYoutubeVideo == null && MyVideo == null)
                    MyImage = ParsnipData.Media.Image.Select(new MediaId(Request.QueryString["id"]), myUser.Id);

                if (MyYoutubeVideo != null)
                {
                    MyYoutubeVideo = Youtube.Select(new MediaId(Request.QueryString["id"]), myUser.Id);
                    MediaShare myMediaShare = MyYoutubeVideo.MyMediaShare;
                    if (myMediaShare == null)
                    {
                        myMediaShare = new MediaShare(MyYoutubeVideo.Id, myUser.Id);
                        myMediaShare.Insert();
                    }
                    ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" +
                    myMediaShare.Id;
                    thumbnail.Src = MyYoutubeVideo.Compressed;
                    input_date_media_captured.Value = MyYoutubeVideo.DateTimeCaptured.ToString();
                    a_play_video.HRef = string.Format("../../view?id={0}", MyYoutubeVideo.Id);
                    a_play_video.Visible = true;
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
                    thumbnail.Src = MyVideo.Compressed;
                    input_date_media_captured.Value = MyVideo.DateTimeCaptured.ToString();
                    a_play_video.HRef = string.Format("../../view?id={0}", MyVideo.Id);
                    a_play_video.Visible = true;
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
                    ImagePreview.ImageUrl = MyImage.Compressed;
                    input_date_media_captured.Value = MyImage.DateTimeCaptured.ToString();
                    ImagePreview.Visible = true;
                    Page.Title = "Edit Image";
                }
                else
                {
                    Response.Redirect("home");
                }
            }

            void GetTags()
            {
                Page httpHandler = (Page)HttpContext.Current.Handler;
                foreach (MediaTagPair mediaTagPair in MyMedia.MediaTagPairs)
                {
                    MediaTagPairControl mediaTagPairControl = (MediaTagPairControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaTagPairControl.ascx");
                    mediaTagPairControl.MyMedia = MyMedia;
                    mediaTagPairControl.MyPair = mediaTagPair;
                    MediaTagContainer.Controls.Add(mediaTagPairControl);
                }
                foreach (MediaUserPair mediaUserPair in MyMedia.MediaUserPairs)
                {
                    MediaUserPairControl mediaUserPairControl = (MediaUserPairControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaUserPairControl.ascx");
                    mediaUserPairControl.MyMedia = MyMedia;
                    mediaUserPairControl.MyPair = mediaUserPair;
                    UserTagContainer.Controls.Add(mediaUserPairControl);
                }
            }

            void GetOriginalRedirect()
            {
                

                if (OriginalTag == null && tagParam == null && userTagParam == null && MyMedia.AlbumId == default && MyMedia.MediaTagPairs.Count == default)
                {
                    OriginalAlbumRedirect = "home?focus=" + MyMedia.Id.ToString();
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
                else
                {
                    if (userTagParam != null)
                        OriginalAlbumRedirect = $"tag?user={userTagParam}&focus={MyMedia.Id}";
                    else if (tagParam != null)
                        OriginalAlbumRedirect = $"tag?id={tagParam}&focus={MyMedia.Id}";
                    else
                        OriginalAlbumRedirect = $"home?focus={MyMedia.Id}";
                    //else if (MyMedia.MediaTagPairs.Count != default)
                    //    OriginalAlbumRedirect = $"tag?id={MyMedia.MediaTagPairs[0].MediaTag.Id}&focus={MyMedia.Id}";

                }
            }

            void PopulateTagDropDowns()
            {
                NewAlbumsDropDown.Items.Clear();
                NewAlbumsDropDown.Items.Add(new ListItem() { Value = "0", Text = "(No tag selected)" });
                foreach (MediaTag tempMediaTag in MediaTag.GetAllTags())
                {
                    NewAlbumsDropDown.Items.Add(new ListItem()
                    {
                        Value = Convert.ToString(tempMediaTag.Id),
                        Text = tempMediaTag.Name
                    });
                }

                DropDown_SelectUser.Items.Clear();
                foreach (User user in ParsnipData.Accounts.User.GetAllUsers())
                {
                    DropDown_SelectUser.Items.Add(new ListItem()
                    {
                        Value = Convert.ToString(user.Id),
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
                        text = string.Format("{0} tried to delete media called \"{1}\", but {2} was not allowed " +
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
                            text = string.Format("{0} started editing media called \"{1}\". {2} does not own the " +
                            "media but {3} is allowed since {3} is an {4}", myUser.FullName, MyMedia.Title,
                            myUser.SubjectiveGenderPronoun.First().ToString().ToUpper() +
                            myUser.SubjectiveGenderPronoun.Substring(1), myUser.SubjectiveGenderPronoun, accountType)
                        };
                    }
                    else
                    {
                        new LogEntry(Log.General)
                        {
                            text = string.Format("{0} attempted to edit media called \"{1}\" which {2} " +
                        "did not own. Access was DENIED!", myUser.FullName, MyMedia.Title, myUser.SubjectiveGenderPronoun)
                        };

                        Response.Redirect($"{OriginalAlbumRedirect}&error=P100");
                    }
                }
            }

            void DisplayMediaAttributes()
            {
                if (MyMedia.Title != null && !string.IsNullOrEmpty(MyMedia.Title) && !string.IsNullOrWhiteSpace(MyMedia.Title))
                {
                    InputTitleTwo.Text = MyMedia.Title;
                }

                if (myUser.AccountType == "admin")
                {
                    DateCapturedDiv.Visible = true;
                    btn_AdminDelete.Visible = true;
                }
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

                        MyMedia.Update();

                        if (myUser.Id.ToString() == MyMedia.CreatedById.ToString())
                        {
                            new LogEntry(Log.General)
                            {
                                text = string.Format("{0} saved changes to {1} media called \"{2}\"",
                                myUser.FullName, myUser.PosessivePronoun, MyMedia.Title)
                            };
                        }
                        else
                        {
                            string accountType = myUser.AccountType == "admin" ? "admin" : "approved media editor";

                            new LogEntry(Log.General)
                            {
                                text = string.Format("{0} saved changes to media called \"{1}\". {3} does not own " +
                                "the media but {2} is allowed since {2} is an {4}", myUser.FullName,
                                MyMedia.Title, myUser.SubjectiveGenderPronoun,
                                myUser.SubjectiveGenderPronoun.First().ToString().ToUpper() +
                                myUser.SubjectiveGenderPronoun.Substring(1), accountType)
                            };
                        }

                    }
                    else
                    {
                        changesWereSaved = false;
                        new LogEntry(Log.General)
                        {
                            text =
                            string.Format("{0} tried to save changes to media called \"{1}\" which {2} did not own. {3} is not " +
                            "an admin or an approved media editor so {4} changes were not saved",
                            myUser.FullName, MyMedia.Title, myUser.SubjectiveGenderPronoun,
                            myUser.SubjectiveGenderPronoun.First().ToString().ToUpper() +
                            myUser.SubjectiveGenderPronoun.Substring(1), myUser.PosessivePronoun)
                        };
                    }

                    string Redirect = MyMedia.AlbumId == default ? OriginalAlbumRedirect : $"{Request.Url.GetLeftPart(UriPartial.Authority)}/tag?id={MyMedia.AlbumId}&media={MyMedia.Id}";
                    if (changesWereSaved)
                    {
                        Response.Redirect(Redirect);
                    }
                    else
                    {
                        if (Redirect.Contains("?"))
                            Response.Redirect(Redirect + "&error=P100");
                        else
                            Response.Redirect(Redirect + "?error=P100");
                    }
                }
                catch
                {

                }
            }
        }

        protected void AddMediaTagPair_Click(object sender, EventArgs e)
        {
            int selectedTag = Convert.ToInt16(Request["NewAlbumsDropDown"]);
            if (selectedTag != default)
            {
                MediaTag myMediaTag = new MediaTag(selectedTag);

                MediaTagPair newMediaTagPair = new MediaTagPair(MyMedia, myMediaTag, myUser);
                newMediaTagPair.Insert();

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }

        protected void AddMediaUserPair_Click(object sender, EventArgs e)
        {
            int selectedUserId = Convert.ToInt16(Request["DropDown_SelectUser"]);
            if (selectedUserId != default)
            {
                int userId = selectedUserId;

                MediaUserPair newMediaUserPair = new MediaUserPair(MyMedia, selectedUserId, myUser);
                newMediaUserPair.Insert();

                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
        }
    }
}