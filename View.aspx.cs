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

    public partial class View : System.Web.UI.Page
    {
        User myUser;
        static readonly Log DebugLog = Log.Select(3);
        ParsnipData.Media.Image myImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            //If there is an access token, get the token & it's data.
            //If there is no access token, check that the user is logged in.
            var accessToken = Request.QueryString["share"];

            if (Request.QueryString["share"] != null)
            {
                var myMediaShare = new MediaShare(new MediaShareId(Request.QueryString["share"].ToString()));
                try
                {
                    myMediaShare.Select();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (string.IsNullOrEmpty(myMediaShare.MediaId.ToString()))
                {
                    Debug.WriteLine("Media Id was empty");
                    new LogEntry(DebugLog) { text = string.Format("Someone tried to access access token {0}. Access was denied because the person who created this link has been suspended.", myMediaShare.Id) };
                    ShareUserSuspendedError.Visible = true;
                }
                else
                {
                    Debug.WriteLine("Media Id was not empty");

                    if (!IsPostBack)
                    {
                        myMediaShare.TimesUsed++;
                        myMediaShare.Update();
                    }

                    User createdBy = ParsnipData.Accounts.User.Select(myMediaShare.UserId);
                    myImage = ParsnipData.Media.Image.Select(myMediaShare.MediaId, myUser == null ? default : myUser.Id);

                    new LogEntry(DebugLog) { text = string.Format("{0}'s link to {1} got another hit! Now up to {2}", createdBy.FullName, myImage.Title, myMediaShare.TimesUsed) };
                }
            }
            else
            {
                if (Request.QueryString["id"] == null)
                    myUser = Account.SecurePage("view", this, Data.DeviceType);
                else
                    myUser = Account.SecurePage("view?id=" + Request.QueryString["id"], this, Data.DeviceType);

                if (Request.QueryString["id"] == null)
                    Response.Redirect("home");

                myImage = ParsnipData.Media.Image.Select(new MediaId(Request.QueryString["id"]), myUser.Id);
            }

            //Get the image which the user is trying to access, and display it on the screen.
            if (myImage == null || string.IsNullOrEmpty(myImage.Compressed))
            {
                //ShareLinkContainer.Visible = false;
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
                if (myImage.AlbumId == 0)
                {
                    Debug.WriteLine(string.Format("AlbumId {0} == {1}", myImage.AlbumId, default(int)));
                    //NotExistError.Visible = true;
                    Button_ViewAlbum.Visible = false;
                }
                else
                {
                    Debug.WriteLine(string.Format("AlbumId {0} != {1}", myImage.AlbumId, default(int)));

                    /*
                    ImageTitle.InnerText = myImage.Title;
                    Page.Title = myImage.Title;
                    ImagePreview.ImageUrl = myImage.Directory;   
                    */
                }

                ImageTitle.InnerText = myImage.Title;
                Page.Title = myImage.Title;
                ImagePreview.Src = myImage.Compressed;
                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:title\" content=\"{0}\" />", myImage.Title)));
                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:image\" content=\"{0}\" />", myImage.Compressed.Contains("https://lh3.googleusercontent.com") ? myImage.Compressed : string.Format("{0}/{1}", Request.Url.GetLeftPart(UriPartial.Authority), myImage.Compressed.Replace(" ", "%20")))));
                Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:type\" content=\"website\" />"));
                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:url\" content=\"{0}\" />", Request.Url.ToString())));
                Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:description\" content=\"{0}\" />", myImage.Description)));
                Page.Header.Controls.Add(new LiteralControl("<meta property=\"fb:app_id\" content=\"521313871968697\" />"));

                //If there was no access token, the user is trying to share the photo.
                //Generate a shareable link and display it on the screen.
                if (Request.QueryString["share"] == null)
                {
                    Button_ViewAlbum.Visible = false;

                    MediaShare myMediaShare;

                   
                        myMediaShare = myImage.MyMediaShare;
                    if(myMediaShare == null)
                    {
                        myMediaShare = new MediaShare(myImage.Id, myUser.Id);
                        myMediaShare.Insert();
                    }

                    //Gets URL without sub pages
                    //ShareLink.Value = Request.Url.GetLeftPart(UriPartial.Authority) + myMediaShare.ImageRedirect;
                }
                else
                {
                    //ShareLinkContainer.Visible = false;
                }
            }

        }

        protected void Button_ViewAlbum_Click(object sender, EventArgs e)
        {
            string redirect;
            switch (myImage.AlbumId.ToString().ToUpper())
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
                    Debug.WriteLine("Album was wrong! Album = " + myImage.AlbumId.ToString());
                    break;
            }

            Response.Redirect(redirect  + myImage.Id);
        }
    }
}