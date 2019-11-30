using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipWebsite.Custom_Controls.Media;
using System.Diagnostics;
using ParsnipData.Media;

namespace ParsnipWebsite
{
    public partial class Videos : System.Web.UI.Page
    {
        private User myUser;
        static readonly MediaTag VideoMediaTag = new MediaTag(2);

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage("videos", this, Data.DeviceType);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaManager.GetAlbumAsMediaControls(VideoMediaTag))
            {
                links_div.Controls.Add(mc);
            }
        }

        protected void Button_UploadDataId_Click(object sender, EventArgs e)
        {
            var rawDataId = TextBox_UploadDataId.Text;
            var dataId = TextBox_UploadDataId.Text.Substring(rawDataId.Length - 11, 11);
            Youtube myYoutube = new Youtube(dataId, myUser, VideoMediaTag);
            myYoutube.Scrape();
            myYoutube.Insert();

            Response.Redirect($"edit_media?id={myYoutube.Id}");
        }
    }
}