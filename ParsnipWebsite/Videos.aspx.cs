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
        static readonly MediaTag VideoMediaTag = MediaTag.Select((int)Data.MediaTagIds.Videos);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("videos", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("videos?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

            NewMenu.SelectedPage = PageIndex.Videos;
            NewMenu.LoggedInUser = myUser;
            NewMenu.Upload = true;
            NewMenu.Search = true;

            UploadMediaControl.Initialise(myUser, VideoMediaTag, this);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaControl.GetAlbumAsMediaControls(VideoMediaTag))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}