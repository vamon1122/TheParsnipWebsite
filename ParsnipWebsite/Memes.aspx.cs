using System;
using ParsnipData.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Logging;
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
        static readonly MediaTag MemesMediaTag = new MediaTag(3);

        public Memes()
        {
            //Retrieves wrong album ID and overwrites
            //MemesAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("memes", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("memes?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

            if (IsPostBack)
            {
                if (PhotoUpload.PostedFile.ContentLength > 0)
                    MediaManager.UploadImage(myUser, MemesMediaTag, PhotoUpload);

                if (PhotoUpload2.PostedFile.ContentLength > 0)
                    MediaManager.UploadImage(myUser, MemesMediaTag, PhotoUpload2);
            }

            if (myUser.AccountType == "admin" || myUser.AccountType == "member")
                UploadDiv.Style.Clear();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaManager.GetAlbumAsMediaControls(MemesMediaTag))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}