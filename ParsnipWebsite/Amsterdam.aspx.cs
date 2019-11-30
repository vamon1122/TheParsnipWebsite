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
    public partial class Amsterdam : System.Web.UI.Page
    {
        private User myUser;
        static readonly Log DebugLog = Log.Select(3);
        static readonly MediaTag AmsterdamMediaTag = new MediaTag(4);

        public Amsterdam()
        {
            //Retrieves wrong album ID and overwrites
            //AmsterdamAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("amsterdam", this, Data.DeviceType, "media");
            else
                myUser = Account.SecurePage("amsterdam?focus=" + Request.QueryString["focus"], this, Data.DeviceType, "media");

            if (IsPostBack)
            {
                if (PhotoUpload.PostedFile.ContentLength > 0)
                    MediaManager.UploadImage(myUser, AmsterdamMediaTag, PhotoUpload);

                if (PhotoUpload2.PostedFile.ContentLength > 0)
                    MediaManager.UploadImage(myUser, AmsterdamMediaTag, PhotoUpload2);
            }

            if (myUser.AccountType == "admin" || myUser.AccountType == "member")
                UploadDiv.Style.Clear();
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaManager.GetAlbumAsMediaControls(AmsterdamMediaTag))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}