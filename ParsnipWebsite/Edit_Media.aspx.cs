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
            //REQUIRED TO VIEW POSTBACK
            form1.Action = Request.RawUrl;

            if (Request.QueryString["id"] == null)
                myUser = Account.SecurePage("edit_media", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("edit_media?id=" + Request.QueryString["id"], this, Data.DeviceType);

            if (Request.QueryString["id"] != null)
            {
                string id = Request.QueryString["id"];

                MyYoutubeVideo = ParsnipData.Media.Youtube.Select(new MediaId(Request.QueryString["id"]), myUser.Id);
                if(MyYoutubeVideo == null)
                    MyVideo = ParsnipData.Media.Video.Select(new MediaId(Request.QueryString["id"]), myUser.Id);

                if(MyYoutubeVideo == null && MyVideo == null)
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
                    input_date_media_captured.Value = MyYoutubeVideo.DateTimeCaptured.ToString();
                    youtube_video.Attributes.Add("data-id", MyYoutubeVideo.DataId);
                    youtube_video_container.Visible = true;
                    Page.Title = "Edit Youtube Video";
                }
                else if (MyVideo != null)
                {
                    MyVideo = Video.Select(new MediaId(Request.QueryString["id"]), myUser.Id);
                    MediaShare myMediaShare = MyVideo.MyMediaShare;
                    if(myMediaShare == null)
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

                switch (MyMedia.AlbumId)
                {
                    case (int)MediaTag.Ids.Photos:
                        OriginalAlbumRedirect = "photos?focus=" + MyMedia.Id.ToString();
                        break;
                    case (int)MediaTag.Ids.Memes:
                        OriginalAlbumRedirect = "memes?focus=" + MyMedia.Id.ToString();
                        break;
                    case (int)MediaTag.Ids.Krakow:
                        OriginalAlbumRedirect = "krakow?focus=" + MyMedia.Id.ToString();
                        break;
                    case (int)MediaTag.Ids.Videos:
                        OriginalAlbumRedirect = "videos?focus=" + MyMedia.Id.ToString();
                        break;
                    case (int)MediaTag.Ids.Portugal:
                        OriginalAlbumRedirect = "portugal?focus=" + MyMedia.Id.ToString();
                        break;
                    case (int)MediaTag.Ids.Amsterdam:
                        OriginalAlbumRedirect = "amsterdam?focus=" + MyMedia.Id.ToString();
                        break;
                    case default(int):
                        Debug.WriteLine("Album id is empty. Redirecting to manage_media");
                        OriginalAlbumRedirect = "manage_media?" + MyMedia.Id.ToString();
                        break;
                    default:
                        Debug.WriteLine(string.Format("The album id {0} was not recognised!",
                            MyMedia.AlbumId));
                        OriginalAlbumRedirect = "home?error=nomediaalbum4";
                        break;
                }

                NewAlbumsDropDown.Items.Clear();
                if (myUser.AccountType == "admin")
                    NewAlbumsDropDown.Items.Add(new ListItem() { Value = "0", Text = "None" });
                foreach (MediaTag tempMediaTag in MediaTag.GetAllTags())
                {
                    NewAlbumsDropDown.Items.Add(new ListItem()
                    {
                        Value = Convert.ToString(tempMediaTag.Id),
                        Text = tempMediaTag.Name
                    });
                }

                var AlbumIds = MyMedia.SelectMediaTagIds();
                int NumberOfAlbums = AlbumIds.Count();

                if (NumberOfAlbums > 0)
                {
                    NewAlbumsDropDown.SelectedValue = AlbumIds.First().ToString();
                }
                else
                {
                    NewAlbumsDropDown.SelectedValue = "0";
                }

                if (Request.QueryString["delete"] != null)
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

                    string Redirect;

                    switch (Convert.ToInt16(NewAlbumsDropDown.SelectedValue))
                    {
                        case (int)MediaTag.Ids.Amsterdam:
                            Redirect = "amsterdam";
                            break;
                        case (int)MediaTag.Ids.Krakow:
                            Redirect = "krakow";
                            break;
                        case (int)MediaTag.Ids.Memes:
                            Redirect = "memes";
                            break;
                        case (int)MediaTag.Ids.Photos:
                            Redirect = "photos";
                            break;
                        case (int)MediaTag.Ids.Portugal:
                            Redirect = "portugal";
                            break;
                        case (int)MediaTag.Ids.Videos:
                            Redirect = "videos";
                            break;
                        case 0:
                            Debug.WriteLine("No album selected. Must be none! Redirecting to manage photos...");
                            Redirect = "manage_media";
                            break;
                        default:
                            Redirect = "home?error=nomediaalbum2";
                            break;
                    }
                    if (deleteSuccess)
                    {
                        new LogEntry(Log.General) { text = string.Format("{0} deleted media called \"{1}\"", 
                            myUser.FullName, MyMedia.Title) };
                        Response.Redirect(Redirect);
                    }
                    else
                    {
                        if (Redirect.Contains("?"))
                            Response.Redirect(Redirect + "&error=access");
                        else
                            Response.Redirect(Redirect + "?error=access");
                    }
                }

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

                            int newAlbumId = default;
                            try
                            {
                                newAlbumId = Convert.ToInt16(Request["NewAlbumsDropDown"]);
                            }
                            catch
                            {
                                Debug.WriteLine("There was no album!");
                            }

                            MyMedia.AlbumId = newAlbumId;

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
                                    "the media but {2} is allowed since {2} is an {4}" , myUser.FullName, 
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

                        string Redirect;

                        switch (MyMedia.AlbumId)
                        {
                            case (int)MediaTag.Ids.Photos:
                                Redirect = "photos?focus=" + MyMedia.Id.ToString();
                                break;
                            case (int)MediaTag.Ids.Memes:
                                Redirect = "memes?focus=" + MyMedia.Id.ToString();
                                break;
                            case (int)MediaTag.Ids.Krakow:
                                Redirect = "krakow?focus=" + MyMedia.Id.ToString();
                                break;
                            case (int)MediaTag.Ids.Videos:
                                Redirect = "videos?focus=" + MyMedia.Id.ToString();
                                break;
                            case (int)MediaTag.Ids.Portugal:
                                Redirect = "portugal?focus=" + MyMedia.Id.ToString();
                                break;
                            case (int)MediaTag.Ids.Amsterdam:
                                Redirect = "amsterdam?focus=" + MyMedia.Id.ToString();
                                break;
                            case default(int):
                                Debug.WriteLine("New album id is empty. Redirecting to original album");
                                //Redirect = "manage_medias?id=" + MyMedia.Id.ToString();
                                Redirect = OriginalAlbumRedirect;
                                break;
                            default:
                                Debug.WriteLine(string.Format("Edit_Media: The album id {0} was not recognised!",
                                    MyMedia.AlbumId.ToString().ToUpper()));
                                Redirect = "home?error=nomediaalbum3";
                                break;
                        }
                        if (changesWereSaved)
                        {
                            Response.Redirect(Redirect);
                        }
                        else
                        {
                            if (Redirect.Contains("?"))
                                Response.Redirect(Redirect + "&error=access");
                            else
                                Response.Redirect(Redirect + "?error=access");
                        }
                    }
                    catch
                    {

                    }
                }

                if (MyMedia.Title != null && !string.IsNullOrEmpty(MyMedia.Title) &&
                    !string.IsNullOrWhiteSpace(MyMedia.Title))
                {
                    Debug.WriteLine("Updating title from media object: " + MyMedia.Title);
                    InputTitleTwo.Text = MyMedia.Title;
                }

                if (myUser.AccountType == "admin")
                {
                    DateCapturedDiv.Visible = true;
                    btn_AdminDelete.Visible = true;
                }

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

                        Response.Redirect(OriginalAlbumRedirect + "&error=0");
                    }
                }
                Debug.WriteLine("Setting media directory to: " + MyMedia.Compressed);
            }
            else
            {
                Response.Redirect("home");
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Save button clicked. Saving changes...");
        }
    }
}