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
        static readonly Log DebugLog = Log.Select(3);
        static readonly MediaTag KrakowMediaTag = new MediaTag(6);

        public Krakow()
        {
            //Retrieves wrong album ID and overwrites
            //KrakowAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("krakow", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("krakow?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

            if (IsPostBack)
            {
                if (PhotoUpload.PostedFile.ContentLength > 0)
                    MediaManager.UploadImage(myUser, KrakowMediaTag, PhotoUpload);

                if (PhotoUpload2.PostedFile.ContentLength > 0)
                    MediaManager.UploadImage(myUser, KrakowMediaTag, PhotoUpload2);
            }

            if (myUser.AccountType == "admin" || myUser.AccountType == "member")
                UploadDiv.Style.Clear();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaManager.GetAlbumAsMediaControls(KrakowMediaTag))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}