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
    public partial class Edit_Image : System.Web.UI.Page
    {
        User myUser;
        static readonly Log DebugLog = new Log("Debug");
        ParsnipData.Media.Image MyImage;
        protected void Page_Load(object sender, EventArgs e)
        {
            //REQUIRED TO VIEW POSTBACK
            form1.Action = Request.RawUrl;

            if (Request.QueryString["imageid"] == null)
                myUser = Account.SecurePage("edit_image", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("edit_image?imageid=" + Request.QueryString["imageid"], this, Data.DeviceType);

            //myUser = Uac.SecurePage("edit_image", this, Data.DeviceType);

            if (Request.QueryString["imageid"] != null)
            {
                MyImage = new ParsnipData.Media.Image(new Guid(Request.QueryString["imageid"]));
                Debug.WriteLine("Selecting image with id = " + Request.QueryString["imageid"]);
                MyImage.Select();

                Debug.WriteLine("----------Image album = " + MyImage.AlbumId);

                NewAlbumsDropDown.Items.Clear();

                NewAlbumsDropDown.Items.Add(new ListItem() { Value = Guid.Empty.ToString(), Text = "None" });
                foreach (Album tempAlbum in Album.GetAllAlbums())
                {
                    NewAlbumsDropDown.Items.Add(new ListItem() { Value = Convert.ToString(tempAlbum.Id), Text = tempAlbum.Name });
                }

                var AlbumIds = MyImage.AlbumIds();
                int NumberOfAlbums = AlbumIds.Count();

                if (NumberOfAlbums > 0)
                {
                    new LogEntry(DebugLog) { text = "First album guid = " + AlbumIds.First().ToString() };
                    NewAlbumsDropDown.SelectedValue = AlbumIds.First().ToString();
                }
                else
                {
                    NewAlbumsDropDown.SelectedValue = Guid.Empty.ToString();
                }

                if (Request.QueryString["delete"] != null)
                {
                    //I am being deleted
                    new LogEntry(DebugLog) { text = "Delete image clicked" };
                    MyImage.Delete();

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
                        case "00000000-0000-0000-0000-000000000000":
                            Debug.WriteLine("No album selected. Must be none! Redirecting to manage photos...");
                            Redirect = "manage_photos";
                            break;
                        default:
                            Redirect = "home?error=noimagealbum2";
                            break;
                    }

                    Response.Redirect(Redirect);
                }

                if (IsPostBack)
                {
                    Debug.WriteLine("I am a postback!!!");
                    new LogEntry(DebugLog) { text = "Delete image NOT clicked" };
                    /*
                    new LogEntry(DebugLog) { text = "Posted back title3 = " + Request["InputTitleTwo"].ToString() };
                    new LogEntry(DebugLog) { text = "Posted back albumid3 = " + Request["NewAlbumsDropDown"].ToString() };
                    */

                    Debug.WriteLine("Getting title from request: " + Request["InputTitleTwo"].ToString());
                    MyImage.Title = Request["InputTitleTwo"].ToString();
                    
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
                        if (newAlbumId == Guid.Empty.ToString())
                        {
                            MyImage.AlbumId = Guid.Empty;
                            MyImage.RemoveFromAllAlbums();
                        }
                        else
                        {
                            MyImage.AlbumId = new Guid(newAlbumId);
                        }
                        MyImage.Update();
                    }

                    string Redirect;

                    switch (MyImage.AlbumId.ToString().ToUpper())
                    {
                        case "4B4E450A-2311-4400-AB66-9F7546F44F4E":
                            Redirect = "photos?imageid=" + MyImage.Id.ToString();
                            break;
                        case "5F15861A-689C-482A-8E31-2F13429C36E5":
                            Redirect = "memes?imageid=" + MyImage.Id.ToString();
                            break;
                        case "FF3127DF-70B2-47EF-B77B-2E086D2EF370":
                            Redirect = "krakow?imageid=" + MyImage.Id.ToString();
                            break;
                        case "00000000-0000-0000-0000-000000000000":
                            Debug.WriteLine("Album id is empty guid. Redirecting to manage_photos");
                            Redirect = "manage_photos?imageid=" + MyImage.Id.ToString();
                            break;
                        default:
                            //Debug.WriteLine(string.Format("The album id {0} != ff3127df-70b2-47ef-b77b-2e086d2ef370", MyImage.AlbumId));
                            Redirect = "home?error=noimagealbum3";
                            break;
                    }
                    Response.Redirect(Redirect);
                }

                if (MyImage.Title != null && !string.IsNullOrEmpty(MyImage.Title) && !string.IsNullOrWhiteSpace(MyImage.Title))
                {
                    Debug.WriteLine("Updating title from image object: " + MyImage.Title);
                    InputTitleTwo.Text = MyImage.Title;
                }

                if (myUser.AccountType == "admin")
                {
                    if (AlbumIds.Count() > 0)
                        btn_AdminDelete.Visible = true;

                    //DropDownDiv.Visible = true;
                }

                if (MyImage.CreatedById.ToString() != myUser.Id.ToString())
                {
                    new LogEntry(DebugLog) { text = string.Format("{0} attempted to edit an image which {1} did not own.", myUser.FullName, myUser.SubjectiveGenderPronoun) };
                    if (myUser.AccountType == "admin")
                    {

                        new LogEntry(DebugLog) { text = string.Format("{0} was allowed to edit the image anyway because {1} is an admin.", myUser.FullName, myUser.SubjectiveGenderPronoun) };
                    }
                    else
                    {
                        Response.Redirect("photos?error=0");
                    }
                }
                Debug.WriteLine("Setting image directory to: " + MyImage.Directory);
                    ImagePreview.ImageUrl = MyImage.Directory;
            }
            else
            {
                Response.Redirect("home");
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            new LogEntry(DebugLog) { text = "Save button clicked. Saving changes..." };
        }
    }
}