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
    public partial class Photos : System.Web.UI.Page
    {
        private User myUser;
        static readonly MediaTag PhotosMediaTag = new MediaTag(1);

        public Photos()
        {
            //Retrieves wrong album ID and overwrites
            //PhotosAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("photos", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("photos?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

            NewMenu.SelectedPage = PageIndex.Photos;
            NewMenu.LoggedInUser = myUser;
            NewMenu.Upload = true;
            UploadMediaControl.Initialise(myUser, PhotosMediaTag, this);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mediaControl in MediaControl.GetAlbumAsMediaControls(PhotosMediaTag))
            {
                DynamicMediaDiv.Controls.Add(mediaControl);
            }
        }
    }
}