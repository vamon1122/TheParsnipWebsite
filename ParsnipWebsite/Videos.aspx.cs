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
    public partial class Videos : MediaViewPage
    {
        private User myUser;
        static readonly MediaTag VideoMediaTag = MediaTag.Select((int)Data.MediaTagIds.Videos);

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage(this, Data.DeviceType);

            TagName.InnerText = $"#{VideoMediaTag.Name}";
            TagDescription.InnerText = VideoMediaTag.Description;

            NewMenu.SelectedPage = PageIndex.Videos;
            NewMenu.LoggedInUser = myUser;
            NewMenu.Upload = true;

            UploadMediaControl.Initialise(myUser, VideoMediaTag, this);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaControl.GetAlbumAsMediaControls(VideoMediaTag, this))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}