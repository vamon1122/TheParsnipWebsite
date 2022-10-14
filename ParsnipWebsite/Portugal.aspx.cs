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
    public partial class Portugal : MediaViewPage
    {
        private User myUser;
        static readonly MediaTag PortugalMediaTag = MediaTag.Select((int)Data.MediaTagIds.Portugal);

        public Portugal()
        {
            //Retrieves wrong album ID and overwrites
            //PortugalAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage(this, Data.DeviceType);

            TagName.InnerText = $"#{PortugalMediaTag.Name}";
            TagDescription.InnerText = PortugalMediaTag.Description;

            NewMenu.SelectedPage = PageIndex.Portugal;
            NewMenu.LoggedInUser = myUser;
            NewMenu.Upload = true;

            UploadMediaControl.Initialise(myUser, PortugalMediaTag, this);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaControl.GetAlbumAsMediaControls(PortugalMediaTag))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}