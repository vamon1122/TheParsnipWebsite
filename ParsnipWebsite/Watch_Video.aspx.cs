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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["access_token"] == null)
            {
                Button_ViewAlbum.Visible = false;

                //We handle data-id since Youtube.js cannot access the video_id
                if (Request.QueryString["data-id"] == null)
                {
                    if (Request.QueryString["video_id"] == null)
                    {
                        Response.Redirect("videos");
                    }
                    else
                    {
                        myUser = Account.SecurePage("watch_video?video_id=" + Request.QueryString["video_id"], this,
                        Data.DeviceType);

                        if (Video.Exists(new Guid(Request.QueryString["video_id"])))
                        {
                            myVideo = new Video(new Guid(Request.QueryString["video_id"]));
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
                            myYoutubeVideo = new YoutubeVideo(new Guid(Request.QueryString["video_id"]));
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
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (myVideo == null && myYoutubeVideo == null)
            {
                ShareLinkContainer.Visible = false;
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
                }
                else
                {
                    video_container.Visible = true;
                    VideoTitle.InnerText = myVideo.Title;
                    VideoSource.Src = myVideo.Directory;
                }

                ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + "/watch_video?access_token=" +
                    myAccessToken.Id;
            }
        }

        protected void Button_ViewAlbum_Click(object sender, EventArgs e)
        {
            Response.Redirect("videos");
        }
    }
}