using ParsnipData.Accounts;
using ParsnipData.Logging;
using ParsnipData.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipWebsite.Custom_Controls.Media;

namespace ParsnipWebsite
{
    public partial class View_Image : System.Web.UI.Page
    {
        public User myUser { get; set; }

        MediaView mediaView;
        
        MediaShare myMediaShare;
        MediaShareId shareId;

        protected void Page_Load(object sender, EventArgs e)
        {
            //If there is an access token, get the token & it's data.
            //If there is no access token, check that the user is logged in.
            var mediaShareId = Request.QueryString["share"];

            mediaView = new MediaView();
            NewMenu.SelectedPage = PageIndex.View;
            NewMenu.Share = true;

            if (Request.QueryString["id"] == null)
            {
                if (Request.QueryString["share"] == null)
                {
                    Response.Redirect("home");
                }
                else
                {
                    myUser = ParsnipData.Accounts.User.LogIn();
                    NewMenu.LoggedInUser = myUser;
                    myMediaShare = new MediaShare(new MediaShareId(Request.QueryString["share"].ToString()));
                    shareId = myMediaShare.Id;
                    try
                    {
                        myMediaShare.Select();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    if (myMediaShare.MediaId == null)
                    {
                        if (Request.QueryString["alert"] == null)
                        {
                            new LogEntry(Log.Debug) { Text = $"Someone tried to access share {myMediaShare.Id}. Access was denied because the media was deleted or the person who created this link has been suspended." };
                            Response.Redirect($"view?share={mediaShareId}&alert=P101");
                        }
                        else
                        {
                            MediaContainer.Visible = false;
                            MediaTagContainer.Visible = false;
                        }
                    }
                    else
                    {
                        User createdBy = ParsnipData.Accounts.User.Select(myMediaShare.UserId);
                        var myUserId = myUser == null ? default : myUser.Id;
                        mediaView.Image = ParsnipData.Media.Image.Select(myMediaShare.MediaId, myUserId);
                        mediaView.Video = Video.Select(myMediaShare.MediaId, myUserId);
                        mediaView.YoutubeVideo = Youtube.Select(myMediaShare.MediaId, myUserId);

                        if(mediaView.Media != null)
                        {
                            myMediaShare.View(myUser);
                            mediaView.Media.ViewCount++;
                        }
                    }
                }
            }
            else
            {
                myUser = Account.SecurePage($"view?id={Request.QueryString["id"]}", this, Data.DeviceType, Request, mediaView);
                NewMenu.LoggedInUser = myUser;
                if (myUser != null)
                {
                    if (mediaView.Media != null)
                    {
                        if (mediaView.Media.MyMediaShare != null)
                        {
                            myMediaShare = mediaView.Media.MyMediaShare;

                            if (myMediaShare == null)
                            {
                                myMediaShare = new MediaShare(mediaView.Media.Id, myUser.Id);
                                myMediaShare.Insert();
                            }
                        }

                        mediaView.Media.View(myUser);
                    }
                }
            }

            if (mediaView.Media == null)
            {
                if (Request.QueryString["alert"] == null)
                {
                    Response.Redirect($"view?id={Request.QueryString["id"]}&alert=P101");
                }
                else
                {
                    MediaContainer.Visible = false;
                    MediaTagContainer.Visible = false;
                }
            }
            else
            {
                viewCount.InnerText = $"{mediaView.Media.ViewCount} view(s)";
                ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" +
                    myMediaShare.Id;
            }

            NewMenu.LoggedInUser = myUser;
            NewMenu.HighlightButtonsForPage(PageIndex.Tag, "View");

            //Get the image which the user is trying to access, and display it on the screen.
            if (mediaView.Media == null || string.IsNullOrEmpty(mediaView.Media.Compressed))
            {
                //ShareLinkContainer.Visible = false;
                Button_ViewAlbum.Visible = false;
                if (ShareUserSuspendedError.Visible == false)
                {
                    UploadUserSuspendedError.Visible = true;
                }
            }
            else
            {
                List<ViewTagControl> ViewTagControls = new List<ViewTagControl>();
                Page httpHandler = (Page)HttpContext.Current.Handler;
                foreach (MediaTagPair mediaTagPair in mediaView.Media.MediaTagPairs)
                {
                    ViewTagControl mediaTagPairViewControl = (ViewTagControl)httpHandler.LoadControl("~/Custom_Controls/Media/ViewTagControl.ascx");
                    mediaTagPairViewControl.MyMedia = mediaView.Media;
                    mediaTagPairViewControl.MyTagPair = mediaTagPair;
                    mediaTagPairViewControl.UpdateLink();
                    ViewTagControls.Add(mediaTagPairViewControl);
                }

                foreach (MediaUserPair mediaUserPair in mediaView.Media.MediaUserPairs)
                {
                    ViewTagControl mediaUserPairViewControl = (ViewTagControl)httpHandler.LoadControl("~/Custom_Controls/Media/ViewTagControl.ascx");
                    mediaUserPairViewControl.MyMedia = mediaView.Media;
                    mediaUserPairViewControl.MyUserPair = mediaUserPair;
                    mediaUserPairViewControl.UpdateLink();
                    ViewTagControls.Add(mediaUserPairViewControl);
                }

                foreach (ViewTagControl control in ViewTagControls.OrderBy(x => x.Name))
                {
                    MediaTagContainer.Controls.Add(control);
                }

                if (mediaView.Media.AlbumId == 0)
                    Button_ViewAlbum.Visible = false;

                ImageTitle.InnerText = mediaView.Media.Title;
                Page.Title = mediaView.Media.Title;

                if (mediaView.Media.Type == "image")
                    ImagePreview.Src = mediaView.Media.Compressed;

                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:title\" content=\"{0}\" />", mediaView.Media.Title)));
                //This will break youtube videos which have not had their thumbnail regenerated
                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:image\" content=\"{0}\" />", mediaView.Media.Compressed.Contains("https://lh3.googleusercontent.com") ? mediaView.Media.Compressed : string.Format("{0}/{1}", Request.Url.GetLeftPart(UriPartial.Authority), mediaView.Media.Compressed))));
                Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:type\" content=\"website\" />"));
                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:url\" content=\"{0}\" />", Request.Url.ToString())));
                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:description\" content=\"{0}\" />", mediaView.Media.Description)));
                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:alt\" content=\"{0}\" />", mediaView.Media.Alt)));
                Page.Header.Controls.Add(new LiteralControl("<meta property=\"fb:app_id\" content=\"521313871968697\" />"));

                if (Request.QueryString["share"] == null)
                {
                    Button_ViewAlbum.Visible = false;

                    MediaShare myMediaShare;


                    myMediaShare = mediaView.Media.MyMediaShare;
                    if (myMediaShare == null)
                    {
                        myMediaShare = new MediaShare(mediaView.Media.Id, myUser.Id);
                        myMediaShare.Insert();
                    }
                }
            }

            if(mediaView.Video != null)
            {
                if (mediaView.Video.VideoData.XScale != default && mediaView.Video.VideoData.YScale != default && mediaView.Video.IsPortrait())
                {
                    video_container.Attributes.Remove("class");
                    video_container.Attributes.Add("class", "media-viewer-portrait");

                    TitleContainer.Attributes.Remove("class");
                    TitleContainer.Attributes.Add("class", "media-viewer-title-portrait background-lightest");
                    //TitleContainer.Attributes.Add("class", "background-lightest");
                }
            }

            if (mediaView.Image != null)
            {
                if (mediaView.Image.IsPortrait())
                {
                    ImagePreview.Attributes.Remove("class");
                    ImagePreview.Attributes.Add("class", "media-viewer-portrait");

                    //MediaContainer.Attributes.Remove("class");
                    //MediaContainer.Attributes.Add("class", "media-viewer-portrait");

                    TitleContainer.Attributes.Remove("class");
                    TitleContainer.Attributes.Add("class", "media-viewer-title-portrait background-lightest");
                    //TitleContainer.Attributes.Add("class", "background-lightest");
                }
            }

        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            if(mediaView.Media == null)
                Button_ViewAlbum.Visible = false;

            if (mediaView.Video == null && mediaView.YoutubeVideo == null)
            {
                    youtube_video_container.Visible = false;
                    video_container.Visible = false;
                
            }
            else
            {
                if (mediaView.Video == null)
                {
                    youtube_video_container.Visible = true;
                    ImageTitle.InnerText = mediaView.YoutubeVideo.Title;
                    youtube_video.Attributes.Add("data-id", mediaView.YoutubeVideo.DataId);
                    ImagePreview.Visible = false;
                    video_container.Visible = false;
                }
                else
                {
                    if (!Data.IsMobile)
                        video_container.Attributes.Add("autoplay", "autoplay");

                    video_container.Poster = mediaView.Video.Compressed;
                    video_container.Visible = true;
                    ImageTitle.InnerText = mediaView.Video.Title;
                    VideoSource.Src = mediaView.Video.VideoData.VideoDir;
                    ImagePreview.Visible = false;
                    youtube_video_container.Visible = false;

                    if (mediaView.Video.Status != null)
                    {
                        if (mediaView.Video.Status.Equals(MediaStatus.Unprocessed))
                            unprocessed.Visible = true;
                        else if (mediaView.Video.Status.Equals(MediaStatus.Processing))
                            processing.Visible = true;
                        else if (mediaView.Video.Status.Equals(MediaStatus.Error))
                            error.Visible = true;
                    }
                }

                

                ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" +
                    myMediaShare.Id;
            }


            myUser = ParsnipData.Accounts.User.LogIn();
            string personFullName = myUser == null ? "A stranger" : myUser.FullName;

            NewMenu.LoggedInUser = myUser;

            if (Request.QueryString["share"] != null && string.IsNullOrEmpty(Request.QueryString["alert"]))
            {
                User sharedBy = ParsnipData.Accounts.User.Select(myMediaShare.UserId);

                new LogEntry(Log.General)
                {
                    Text = $"{personFullName} started watching video called \"{mediaView.Media.Title}\" " +
                    $"using {sharedBy.FullName}'s share link. This link has now been used {myMediaShare.TimesUsed} times!"
                };
            }
        }

        protected void Button_ViewAlbum_Click(object sender, EventArgs e)
        {
            string redirect;

            switch (mediaView.Media.AlbumId)
            {
                case (int)Data.MediaTagIds.Amsterdam:
                    redirect = "~/amsterdam?focus=";
                    break;
                case (int)Data.MediaTagIds.Krakow:
                    redirect = "~/krakow?focus=";
                    break;
                case (int)Data.MediaTagIds.Memes:
                    redirect = "~/memes?focus=";
                    break;
                case (int)Data.MediaTagIds.Photos:
                    redirect = "~/photos?focus=";
                    break;
                case (int)Data.MediaTagIds.Portugal:
                    redirect = "~/portugal?focus=";
                    break;
                case (int)Data.MediaTagIds.Videos:
                    redirect = "~/videos?focus=";
                    break;
                case default(int):
                    redirect = $"manage_media?id={mediaView.Media.Id}";
                    break;
                default:
                    redirect = $"tag?id={mediaView.Media.AlbumId}&focus={mediaView.Media.Id}";
                    break;
            }

            Response.Redirect(redirect + mediaView.Media.Id);
        }
    }
}



        
    
