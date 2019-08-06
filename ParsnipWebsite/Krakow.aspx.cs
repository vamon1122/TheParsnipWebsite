using System;
using ParsnipData.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Logs;
using ParsnipData.Media;
using System.Web.UI.HtmlControls;
using ParsnipData;
using System.Data.SqlClient;
using System.Diagnostics;
using ParsnipWebsite.Custom_Controls.Media;

namespace ParsnipWebsite
{
    public partial class Krakow : System.Web.UI.Page
    {
        private User myUser;
        Log DebugLog = new Log("debug");
        static readonly Album KrakowAlbum = new Album(new Guid("ff3127df-70b2-47ef-b77b-2e086d2ef370"));

        public Krakow()
        {
            KrakowAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage("krakow", this, Data.DeviceType, "member");

            if (IsPostBack && PhotoUpload.PostedFile != null)
            {
                new LogEntry(DebugLog) { text = "POSTBACK with krakow photo" };
                if (PhotoUpload.PostedFile.FileName.Length > 0)
                {
                    try
                    {
                        new LogEntry(DebugLog) { text = "Attempting to upload the krakow photo" };

                        string[] fileDir = PhotoUpload.PostedFile.FileName.Split('\\');
                        string myFileName = fileDir.Last();
                        string myFileExtension = myFileName.Split('.').Last().ToLower();

                        if (ParsnipData.Media.Image.IsValidFileExtension(myFileExtension))
                        {

                            string newDir = string.Format("Resources/Media/Images/Uploads/{0}{1}_{2}_{3}_{4}",
                                myUser.Forename, myUser.Surname, Guid.NewGuid(),
                                Parsnip.AdjustedTime.ToString("dd-MM-yyyy"), myFileName);

                            Debug.WriteLine("Newdir = " + newDir);
                            /*if (PhotoUpload.PostedFile.HasFile)
                            {*/
                            PhotoUpload.PostedFile.SaveAs(Server.MapPath("~/" + newDir));
                            ParsnipData.Media.Image temp = new ParsnipData.Media.Image(newDir, myUser, KrakowAlbum);
                            temp.Update();
                            Response.Redirect("edit_image?redirect=krakow&imageid=" + temp.Id);
                            //}
                        }
                        else
                        {
                            Response.Redirect("krakow?error=video");
                        }
                    }
                    catch (Exception err)
                    {

                        new LogEntry(DebugLog) { text = "There was an exception whilst uploading the krakow photo: " + err };
                    }
                }
            }

            if (myUser.AccountType == "admin" || myUser.AccountType == "member")
            {
                UploadDiv.Style.Clear();
            }
            Debug.WriteLine("Getting all krakow photos");
            List<ParsnipData.Media.Image> AllPhotos = KrakowAlbum.GetAllImages();
            Debug.WriteLine("Got all krakow photos");
            //new LogEntry(Debug) { text = "Got all krakow photos. There were {0} krakow photo(s) = " + AllPhotos.Count() };
            foreach (ParsnipData.Media.Image temp in AllPhotos)
            {
                var MyImageControl = (MediaControl)LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyImageControl.MyImage = temp;
                DynamicPhotosDiv.Controls.Add(MyImageControl);
            }


            if (Request.QueryString["imageid"] != null)
            {



            }

        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                new LogEntry(DebugLog) { text = "Attempting to upload the krakow photo image" };

                string newDir = string.Format("Resources/Media/Images/Uploads/{0}{1}_{2}",
                    myUser.Forename, myUser.Surname, PhotoUpload.FileName);

                if (PhotoUpload.HasFile)
                {

                    PhotoUpload.SaveAs(Server.MapPath("~/" + newDir));
                    ParsnipData.Media.Image temp = new ParsnipData.Media.Image(newDir, myUser, KrakowAlbum);
                    temp.Update();
                }
            }
            catch (Exception err)
            {

                new LogEntry(DebugLog) { text = "There was an exception whilst uploading the krakow photo image: " + err };
            }

        }


    }
}