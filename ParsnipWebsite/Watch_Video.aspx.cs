using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Media;
using ParsnipData.Logs;
using System.Data.SqlClient;
using ParsnipData;
using System.Diagnostics;

namespace ParsnipWebsite
{
    public partial class Watch_Video : System.Web.UI.Page
    {
        User myUser;
        Log DebugLog = new Log("Debug");
        ParsnipData.Media.Video myVideo;
        ParsnipData.Media.YoutubeVideo myYoutubeVideo;
        AccessToken myAccessToken;
        public Media MyMedia
        {
            get
            {
                if (myVideo == null)
                    return myYoutubeVideo;
                else
                    return myVideo;

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["access_token"] == null)
            {
                Button_ViewAlbum.Visible = false;

                //We handle data-id since Youtube.js cannot access the id
                if (Request.QueryString["data-id"] == null)
                {
                    if (Request.QueryString["id"] == null)
                    {
                        Response.Redirect("videos");
                    }
                    else
                    {
                        myUser = Account.SecurePage("watch_video?id=" + Request.QueryString["id"], this,
                        Data.DeviceType);

                        if (Video.Exists(new Guid(Request.QueryString["id"])))
                        {
                            myVideo = new Video(new Guid(Request.QueryString["id"]));
                            myVideo.Select();

                            if (AccessToken.TokenExists(myUser.Id, myVideo.Id))
                            {
                                myAccessToken = AccessToken.GetToken(myUser.Id, myVideo.Id);
                            }
                            else
                            {
                                myAccessToken = new AccessToken(myUser.Id, myVideo.Id);
                                myAccessToken.Insert();
                            }
                        }
                        else
                        {
                            myYoutubeVideo = new YoutubeVideo(new Guid(Request.QueryString["id"]));
                            myYoutubeVideo.Select();

                            if (AccessToken.TokenExists(myUser.Id, myYoutubeVideo.Id))
                            {
                                myAccessToken = AccessToken.GetToken(myUser.Id, myYoutubeVideo.Id);
                            }
                            else
                            {
                                myAccessToken = new AccessToken(myUser.Id, myYoutubeVideo.Id);
                                myAccessToken.Insert();
                            }
                        }
                    }
                }
                else
                {
                    myUser = Account.SecurePage("watch_video?data-id=" + Request.QueryString["data-id"], this,
                        Data.DeviceType);

                    Debug.WriteLine("Getting youtube video with data-id = " + Request.QueryString["data-id"]);
                    myYoutubeVideo = new YoutubeVideo(Request.QueryString["data-id"]);
                    myYoutubeVideo.Select();

                    if (AccessToken.TokenExists(myUser.Id, myYoutubeVideo.Id))
                    {
                        myAccessToken = AccessToken.GetToken(myUser.Id, myYoutubeVideo.Id);
                    }
                    else
                    {
                        myAccessToken = new AccessToken(myUser.Id, myYoutubeVideo.Id);
                        myAccessToken.Insert();
                    }
                }
            }
            else
            {
                Debug.WriteLine("Getting access token with id = " + Request.QueryString["access_token"]);
                myAccessToken = new AccessToken(new Guid(Request.QueryString["access_token"]));
                myAccessToken.Select();

                if (myAccessToken.MediaId == Guid.Empty)
                {
                    Debug.WriteLine("Media Id was empty");
                    new LogEntry(DebugLog) { text = string.Format("Someone tried to access access token {0}. Access was denied because the person who created this link has been suspended.", myAccessToken.Id) };
                    ShareUserSuspendedError.Visible = true;
                }
                else
                {
                    myAccessToken.TimesUsed++;
                    myAccessToken.Update();

                    Debug.WriteLine("Access token media id = " + myAccessToken.MediaId);

                    if (Video.Exists(myAccessToken.MediaId))
                    {
                        Debug.WriteLine("Getting video with id = " + myAccessToken.MediaId);
                        myVideo = new Video(myAccessToken.MediaId);
                        myVideo.Select();
                    }
                    else
                    {
                        Debug.WriteLine("Getting youtube video with id = " + myAccessToken.MediaId);
                        myYoutubeVideo = new YoutubeVideo(myAccessToken.MediaId);
                        myYoutubeVideo.Select();
                    }
                }
            }
            
            Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:title\" content=\"{0}\" />", myVideo == null ? myYoutubeVideo.Title : myVideo.Title)));
            Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:image\" content=\"{0}\" />", myVideo == null ? string.Format("https://i.ytimg.com/vi/{0}/hqdefault.jpg", myYoutubeVideo.DataId) : string.Format("{0}/{1}", Request.Url.GetLeftPart(UriPartial.Authority), myVideo.Thumbnail.Replace(" ", "%20")))));
            Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:type\" content=\"website\" />"));
            Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:url\" content=\"{0}\" />", Request.Url.ToString())));
            Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:description\" content=\"{0}\" />", myVideo == null ? myYoutubeVideo.Description : myVideo.Description)));
            Page.Header.Controls.Add(new LiteralControl("<meta property=\"fb:app_id\" content=\"521313871968697\" />"));
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (myVideo == null && myYoutubeVideo == null)
            {
                Button_ViewAlbum.Visible = false;
            }
            else
            {
                if (myVideo == null)
                {
                    youtube_video_container.Visible = true;
                    VideoTitle.InnerText = myYoutubeVideo.Title;
                    Debug.WriteLine("Retrieved a data-id from Data.dll = " + myYoutubeVideo.DataId);
                    youtube_video.Attributes.Add("data-id", myYoutubeVideo.DataId);
                    MyEdit.HRef = string.Format("../../edit_media?id={0}", myYoutubeVideo.Id);
                }
                else
                {
                    if (!Data.IsMobile)
                        video_container.Attributes.Add("autoplay", "autoplay");

                    video_container.Poster = myVideo.Thumbnail;
                    video_container.Visible = true;
                    VideoTitle.InnerText = myVideo.Title;
                    VideoSource.Src = myVideo.Directory;
                    MyEdit.HRef = string.Format("../../edit_media?id={0}", myVideo.Id);
                }

                ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + "/watch_video?access_token=" +
                    myAccessToken.Id;
            }

            if (Request.QueryString["access_token"] == null)
            {
                new LogEntry(new Log("General")) { text = string.Format("{0} started watching video called \"{1}\"", myUser.FullName, MyMedia.Title) };
            }
            else
            {
                myUser = ParsnipData.Accounts.User.GetLoggedInUser();
                User sharedBy = new User(myAccessToken.UserId);
                sharedBy.Select();
                string personFullName = myUser == null ? "A stranger" : myUser.FullName;

                new LogEntry(new Log("General"))
                {
                    text = string.Format("{0} started watching video called \"{1}\" " +
                    "using {2}'s access token. This token has now been used {3} times!", personFullName, MyMedia.Title,
                    sharedBy.FullName, myAccessToken.TimesUsed)
                };
            }
        }

        protected void Button_ViewAlbum_Click(object sender, EventArgs e)
        {
            string redirect;

            string myAlbum = myVideo == null ? myYoutubeVideo.AlbumId.ToString().ToUpper() : myVideo.AlbumId.ToString().ToUpper();

            switch (myAlbum)
            {
                case "4B4E450A-2311-4400-AB66-9F7546F44F4E":
                    Debug.WriteLine("Redirecting to photos");
                    redirect = "~/photos?focus=";
                    break;
                case "5F15861A-689C-482A-8E31-2F13429C36E5":
                    Debug.WriteLine("Redirecting to memes");
                    redirect = "~/memes?focus=";
                    break;
                case "FF3127DF-70B2-47EF-B77B-2E086D2EF370":
                    Debug.WriteLine("Redirecting to Krakow");
                    redirect = "~/krakow?focus=";
                    break;
                case "73C436A1-893B-4418-8800-821823C18DFE":
                    Debug.WriteLine("Redirecting to Videos");
                    redirect = "~/videos?focus=";
                    break;
                default:
                    redirect = "photos?error=album_does_not_exist";
                    Debug.WriteLine("Album was wrong! Album = " + myAlbum);
                    break;
            }

            Response.Redirect(redirect + (myVideo == null ? myYoutubeVideo.Id.ToString().ToUpper() : myVideo.Id.ToString().ToUpper()));
        }
    }
}