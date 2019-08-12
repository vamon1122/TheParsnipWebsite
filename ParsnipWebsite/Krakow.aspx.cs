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
        static readonly Log DebugLog = new Log("debug");
        static readonly Album KrakowAlbum = new Album(new Guid("ff3127df-70b2-47ef-b77b-2e086d2ef370"));

        public Krakow()
        {
            KrakowAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["imageid"] == null)
                myUser = Account.SecurePage("krakow", this, Data.DeviceType, "member");
            else
                myUser = Account.SecurePage("krakow?imageid=" + Request.QueryString["imageid"], this, Data.DeviceType, "member");

            if (IsPostBack && PhotoUpload.PostedFile != null)
            {
                MediaManager.UploadImage(myUser, KrakowAlbum, PhotoUpload);
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
        }
    }
}