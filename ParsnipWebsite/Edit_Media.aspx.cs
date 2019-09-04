using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Media;
using ParsnipData.Logs;
using ParsnipData;
using System.Diagnostics;

namespace ParsnipWebsite
{
    public partial class Edit_Media : System.Web.UI.Page
    {
        User myUser;
        static readonly Log GeneralLog = new Log("General");
        private ParsnipData.Media.Image MyImage;
        private Video MyVideo;
        private YoutubeVideo MyYoutubeVideo;

        public Media MyMedia
        {
            get
            {
                if (MyImage != null)
                    return MyImage;

                if (MyVideo != null)
                    return MyVideo;

                if (MyYoutubeVideo != null)
                    return MyYoutubeVideo;

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

            //myUser = Uac.SecurePage("edit_media", this, Data.DeviceType);

            if (Request.QueryString["id"] != null)
            {
                Guid id = new Guid(Request.QueryString["id"]);
                if (ParsnipData.Media.Image.Exists(id))
                {
                    MyImage = new ParsnipData.Media.Image(new Guid(Request.QueryString["id"]));
                    Debug.WriteLine("Selecting image with id = " + id);
                    MyImage.Select();
                    ImagePreview.ImageUrl = MyImage.Directory;
                    input_date_media_captured.Value = MyImage.DateTimeMediaCreated.ToString();
                    ImagePreview.Visible = true;
                    Page.Title = "Edit Image";
                }
                else if (Video.Exists(id))
                {
                    MyVideo = new Video(new Guid(Request.QueryString["id"]));
                    Debug.WriteLine("Selecting video with id = " + id);
                    MyVideo.Select();
                    thumbnail.Src = MyVideo.Thumbnail;
                    input_date_media_captured.Value = MyVideo.DateTimeMediaCreated.ToString();
                    a_play_video.HRef = string.Format("../../watch_video?id={0}", MyVideo.Id);
                    a_play_video.Visible = true;
                    Page.Title = "Edit Video";
                }
                else if (YoutubeVideo.Exists(id))
                {
                    MyYoutubeVideo = new YoutubeVideo(new Guid(Request.QueryString["id"]));
                    Debug.WriteLine("Selecting youtube video with id = " + id);
                    MyYoutubeVideo.Select();
                    input_date_media_captured.Value = MyYoutubeVideo.DateTimeMediaCreated.ToString();
                    youtube_video.Attributes.Add("data-id", MyYoutubeVideo.DataId);
                    youtube_video_container.Visible = true;
                    Page.Title = "Edit Youtube Video";
                }
                else
                {
                    Debug.WriteLine ("There was no media. Redirecting to home");
                    Response.Redirect("home");
                }

                Debug.WriteLine("----------Media album = " + MyMedia.AlbumId);

                switch (MyMedia.AlbumId.ToString().ToUpper())
                {
                    case "4B4E450A-2311-4400-AB66-9F7546F44F4E":
                        OriginalAlbumRedirect = "photos?focus=" + MyMedia.Id.ToString();
                        break;
                    case "5F15861A-689C-482A-8E31-2F13429C36E5":
                        OriginalAlbumRedirect = "memes?focus=" + MyMedia.Id.ToString();
                        break;
                    case "FF3127DF-70B2-47EF-B77B-2E086D2EF370":
                        OriginalAlbumRedirect = "krakow?focus=" + MyMedia.Id.ToString();
                        break;
                    case "73C436A1-893B-4418-8800-821823C18DFE":
                        OriginalAlbumRedirect = "videos?focus=" + MyMedia.Id.ToString();
                        break;
                    case "00000000-0000-0000-0000-000000000000":
                        Debug.WriteLine("Album id is empty guid. Redirecting to manage_media");
                        OriginalAlbumRedirect = "manage_media?" + MyMedia.Id.ToString();
                        break;
                    default:
                        Debug.WriteLine(string.Format("The album id {0} != ff3127df-70b2-47ef-b77b-2e086d2ef370",
                            MyMedia.AlbumId));
                        OriginalAlbumRedirect = "home?error=nomediaalbum4";
                        break;
                }

                NewAlbumsDropDown.Items.Clear();
                if (myUser.AccountType == "admin")
                    NewAlbumsDropDown.Items.Add(new ListItem() { Value = Guid.Empty.ToString(), Text = "None" });
                foreach (Album tempAlbum in Album.GetAllAlbums())
                {
                    NewAlbumsDropDown.Items.Add(new ListItem()
                    {
                        Value = Convert.ToString(tempAlbum.Id),
                        Text = tempAlbum.Name
                    });
                }

                var AlbumIds = MyMedia.AlbumIds();
                int NumberOfAlbums = AlbumIds.Count();

                if (NumberOfAlbums > 0)
                {
                    Debug.WriteLine("First album guid = " + AlbumIds.First().ToString());
                    NewAlbumsDropDown.SelectedValue = AlbumIds.First().ToString();
                }
                else
                {
                    NewAlbumsDropDown.SelectedValue = Guid.Empty.ToString();
                }

                if (Request.QueryString["delete"] != null)
                {
                    //I am being deleted
                    Debug.WriteLine("Delete media clicked");

                    bool deleteSuccess;

                    if (myUser.AccountType == "admin")
                    {
                        MyMedia.Delete();
                        deleteSuccess = true;
                    }
                    else
                    {
                        new LogEntry(GeneralLog)
                        {
                            text = string.Format("{0} tried to delete media called \"{1}\", but {2} was not allowed " +
                            "because {2} is not an admin", myUser.FullName, MyMedia.Title, 
                            myUser.SubjectiveGenderPronoun)
                        };
                        deleteSuccess = false;
                    }

                    string Redirect;

                    switch (NewAlbumsDropDown.SelectedValue.ToString().ToUpper())
                    {
                        case "4B4E450A-2311-4400-AB66-9F7546F44F4E":
                            Redirect = "photos";
                            break;
                        case "5F15861A-689C-482A-8E31-2F13429C36E5":
                            Redirect = "memes";
                            break;
                        case "FF3127DF-70B2-47EF-B77B-2E086D2EF370":
                            Redirect = "krakow";
                            break;
                        case "73C436A1-893B-4418-8800-821823C18DFE":
                            Redirect = "videos";
                            break;
                        case "00000000-0000-0000-0000-000000000000":
                            Debug.WriteLine("No album selected. Must be none! Redirecting to manage photos...");
                            Redirect = "manage_media";
                            break;
                        default:
                            Redirect = "home?error=nomediaalbum2";
                            break;
                    }
                    if (deleteSuccess)
                    {
                        new LogEntry(GeneralLog) { text = string.Format("{0} deleted media called \"{1}\"", 
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
                    Debug.WriteLine("I am a postback!!!");
                    Debug.WriteLine("Delete media NOT clicked");


                    bool changesWereSaved;
                    try
                    {
                        if (myUser.AccountType == "admin" || myUser.AccountType == "media" || myUser.Id.ToString() == MyMedia.CreatedById.ToString())
                        {
                            changesWereSaved = true;
                            Debug.WriteLine("Getting title from request: " + Request["InputTitleTwo"].ToString());
                            MyMedia.Title = Request["InputTitleTwo"].ToString();

                            if (myUser.AccountType == "admin")
                            {
                                try
                                {
                                    MyMedia.DateTimeMediaCreated = Convert.ToDateTime(Request["input_date_media_captured"]);
                                }
                                catch (Exception ex)
                                {
                                    input_date_media_captured.Value = Request["input_date_media_captured"];
                                    input_date_media_captured.Attributes.Add("class", "form-control is-invalid login");
                                    InputTitleTwo.CssClass = "form-control is-invalid login";
                                    throw ex;
                                }
                            }

                            Debug.WriteLine("Getting album from request...");
                            string newAlbumId;
                            try
                            {
                                newAlbumId = Request["NewAlbumsDropDown"].ToString();
                            }
                            catch
                            {
                                Debug.WriteLine("There was no album!");
                                newAlbumId = null;
                            }

                            if (newAlbumId != null)
                            {
                                MyMedia.AlbumId = new Guid(newAlbumId);
                            }

                            MyMedia.Update();

                            if (myUser.Id.ToString() == MyMedia.CreatedById.ToString())
                            {
                                new LogEntry(GeneralLog)
                                {
                                    text = string.Format("{0} saved changes to {1} media called \"{2}\"",
                                    myUser.FullName, myUser.PosessivePronoun, MyMedia.Title)
                                };
                            }
                            else
                            {
                                string accountType = myUser.AccountType == "admin" ? "admin" : "approved media editor";

                                new LogEntry(GeneralLog)
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
                            new LogEntry(GeneralLog)
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

                        switch (MyMedia.AlbumId.ToString().ToUpper())
                        {
                            case "4B4E450A-2311-4400-AB66-9F7546F44F4E":
                                Redirect = "photos?focus=" + MyMedia.Id.ToString();
                                break;
                            case "5F15861A-689C-482A-8E31-2F13429C36E5":
                                Redirect = "memes?focus=" + MyMedia.Id.ToString();
                                break;
                            case "FF3127DF-70B2-47EF-B77B-2E086D2EF370":
                                Redirect = "krakow?focus=" + MyMedia.Id.ToString();
                                break;
                            case "73C436A1-893B-4418-8800-821823C18DFE":
                                Redirect = "videos?focus=" + MyMedia.Id.ToString();
                                break;
                            case "00000000-0000-0000-0000-000000000000":
                                Debug.WriteLine("New album id is empty. Redirecting to original album");
                                //Redirect = "manage_medias?id=" + MyMedia.Id.ToString();
                                Redirect = OriginalAlbumRedirect;
                                break;
                            default:
                                Debug.WriteLine(string.Format("The album id {0} != ff3127df-70b2-47ef-b77b-2e086d2ef370",
                                    MyMedia.AlbumId));
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
                        new LogEntry(GeneralLog)
                        {
                            text = string.Format("{0} started editing media called \"{1}\". {2} does not own the " +
                            "media but {3} is allowed since {3} is an {4}", myUser.FullName, MyMedia.Title, 
                            myUser.SubjectiveGenderPronoun.First().ToString().ToUpper() + 
                            myUser.SubjectiveGenderPronoun.Substring(1), myUser.SubjectiveGenderPronoun, accountType)
                        };
                    }
                    else
                    {
                        new LogEntry(GeneralLog)
                        {
                            text = string.Format("{0} attempted to edit media called \"{1}\" which {2} " +
                        "did not own. Access was DENIED!", myUser.FullName, MyMedia.Title, myUser.SubjectiveGenderPronoun)
                        };

                        Response.Redirect(OriginalAlbumRedirect + "&error=0");
                    }
                }
                Debug.WriteLine("Setting media directory to: " + MyMedia.Directory);
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