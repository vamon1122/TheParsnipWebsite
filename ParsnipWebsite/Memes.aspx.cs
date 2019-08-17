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
    public partial class Memes : System.Web.UI.Page
    {
        private User myUser;
        static readonly Log DebugLog = new Log("debug");
        static readonly Album MemesAlbum = new Album(new Guid("5F15861A-689C-482A-8E31-2F13429C36E5"));

        public Memes()
        {
            MemesAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("memes", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("memes?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

            if (IsPostBack && PhotoUpload.PostedFile != null)
                MediaManager.UploadImage(myUser, MemesAlbum, PhotoUpload);

            if (myUser.AccountType == "admin" || myUser.AccountType == "member")
                UploadDiv.Style.Clear();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaManager.GetAlbumAsMediaControls(MemesAlbum))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}