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
    public partial class Photos : MediaViewPage
    {
        private User myUser;
        static readonly MediaTag PhotosMediaTag = MediaTag.Select((int)Data.MediaTagIds.Photos);

        public Photos()
        {
            //Retrieves wrong album ID and overwrites
            //PhotosAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Data.OnMediaUnFocused();

            myUser = Account.SecurePage(this, Data.DeviceType);

            TagName.InnerText = $"#{PhotosMediaTag.Name}";
            TagDescription.InnerText = PhotosMediaTag.Description;

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