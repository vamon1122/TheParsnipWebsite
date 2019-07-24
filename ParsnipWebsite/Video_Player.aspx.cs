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
    public partial class Video_Player : System.Web.UI.Page
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

                if (Request.QueryString["data-id"] == null)
                {
                    if (Request.QueryString["videoid"] == null)
                    {
                        Response.Redirect("videos");
                    }
                    else
                    {
                        myUser = Account.SecurePage("video_player?videoid=" + Request.QueryString["videoid"], this,
                        Data.DeviceType);

                        if (Video.Exists(new Guid(Request.QueryString["videoid"])))
                        {
                            myVideo = new Video(new Guid(Request.QueryString["videoid"]));
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
                            myYoutubeVideo = new YoutubeVideo(new Guid(Request.QueryString["videoid"]));
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
                    myUser = Account.SecurePage("video_player?data-id=" + Request.QueryString["data-id"], this, 
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

        void Page_LoadComplete(object sender, EventArgs e)
        {
            if(myVideo == null)
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

            ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + "/video_player?access_token=" + 
                myAccessToken.Id;
        }

        protected void Button_ViewAlbum_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/videos");
        }
    }
}