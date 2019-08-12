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
    public partial class View_Image : System.Web.UI.Page
    {
        User myUser;
        static readonly Log DebugLog = new Log("Debug");
        ParsnipData.Media.Image myImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            //If there is an access token, get the token & it's data.
            //If there is no access token, check that the user is logged in.
            if (Request.QueryString["access_token"] != null)
            {
                var myAccessToken = new AccessToken(new Guid(Request.QueryString["access_token"]));
                try
                {
                    myAccessToken.Select();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (myAccessToken.MediaId == Guid.Empty)
                {
                    Debug.WriteLine("Media Id was empty");
                    new LogEntry(DebugLog) { text = string.Format("Someone tried to access access token {0}. Access was denied because the person who created this link has been suspended.", myAccessToken.Id) };
                    ShareUserSuspendedError.Visible = true;
                }
                else
                {
                    Debug.WriteLine("Media Id was not empty");

                    if (!IsPostBack)
                    {
                        myAccessToken.TimesUsed++;
                        myAccessToken.Update();
                    }

                    User createdBy = new User(myAccessToken.UserId);
                    createdBy.Select();

                    myImage = new ParsnipData.Media.Image(myAccessToken.MediaId);
                    myImage.Select();

                    new LogEntry(DebugLog) { text = string.Format("{0}'s link to {1} got another hit! Now up to {2}", createdBy.FullName, myImage.Title, myAccessToken.TimesUsed) };
                }
            }
            else
            {
                if (Request.QueryString["id"] == null)
                    myUser = Account.SecurePage("view_image", this, Data.DeviceType);
                else
                    myUser = Account.SecurePage("view_image?id=" + Request.QueryString["id"], this, Data.DeviceType);

                if (Request.QueryString["id"] == null)
                    Response.Redirect("home");

                myImage = new ParsnipData.Media.Image(new Guid(Request.QueryString["id"]));
                myImage.Select();
            }

            //Get the image which the user is trying to access, and display it on the screen.
            if (myImage == null || string.IsNullOrEmpty(myImage.Directory))
            {
                ShareLinkContainer.Visible = false;
                Button_ViewAlbum.Visible = false;
                if(ShareUserSuspendedError.Visible == false)
                {
                    UploadUserSuspendedError.Visible = true;
                }
            }
            else
            {
                Debug.WriteLine(string.Format("AlbumId {0}", myImage.AlbumId));

                //If the image has been deleted, display a warning.
                //If the image has not been deleted, display the image.
                if (myImage.AlbumId == Guid.Empty)
                {
                    Debug.WriteLine(string.Format("AlbumId {0} == {1}", myImage.AlbumId, Guid.Empty));
                    //NotExistError.Visible = true;
                    Button_ViewAlbum.Visible = false;
                }
                else
                {
                    Debug.WriteLine(string.Format("AlbumId {0} != {1}", myImage.AlbumId, Guid.Empty));

                    /*
                    ImageTitle.InnerText = myImage.Title;
                    Page.Title = myImage.Title;
                    ImagePreview.ImageUrl = myImage.Directory;   
                    */
                }

                ImageTitle.InnerText = myImage.Title;
                Page.Title = myImage.Title;
                ImagePreview.ImageUrl = myImage.Directory;

                //If there was no access token, the user is trying to share the photo.
                //Generate a shareable link and display it on the screen.
                if (Request.QueryString["access_token"] == null)
                {
                    Button_ViewAlbum.Visible = false;

                    AccessToken myAccessToken;

                    if (AccessToken.TokenExists(myUser.Id, myImage.Id))
                    {
                        myAccessToken = AccessToken.GetToken(myUser.Id, myImage.Id);
                    }
                    else
                    {
                        myAccessToken = new AccessToken(myUser.Id, myImage.Id);
                        myAccessToken.Insert();
                    }

                    //Gets URL without sub pages
                    ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + myAccessToken.ImageRedirect;
                }
                else
                {
                    ShareLinkContainer.Visible = false;
                }
            }

        }

        protected void Button_ViewAlbum_Click(object sender, EventArgs e)
        {
            switch (myImage.AlbumId.ToString().ToUpper())
            {
                case "4B4E450A-2311-4400-AB66-9F7546F44F4E":
                    Debug.WriteLine("Redirecting to photos");
                    Response.Redirect("~/krakow?focus=" + myImage.Id);
                    break;
                case "5F15861A-689C-482A-8E31-2F13429C36E5":
                    Debug.WriteLine("Redirecting to memes");
                    Response.Redirect("~/memes?focus=" + myImage.Id);
                    break;
                case "FF3127DF-70B2-47EF-B77B-2E086D2EF370":
                    Debug.WriteLine("Redirecting to Krakow");
                    Response.Redirect("~/krakow?focus=" + myImage.Id);
                    break;
                default:
                    Debug.WriteLine("Album was wrong! Album = " + myImage.AlbumId.ToString());
                    break;
            }
        }
    }
}