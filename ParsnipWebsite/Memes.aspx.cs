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
        static readonly MediaTag MemesMediaTag = MediaTag.Select((int)Data.MediaTagIds.Memes);

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage(this, Data.DeviceType);

            TagName.InnerText = $"#{MemesMediaTag.Name}";
            TagDescription.InnerText = MemesMediaTag.Description;

            NewMenu.SelectedPage = PageIndex.Memes;
            NewMenu.LoggedInUser = myUser;
            NewMenu.Upload = true;

            UploadMediaControl.Initialise(myUser, MemesMediaTag, this);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaControl.GetAlbumAsMediaControls(MemesMediaTag))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}