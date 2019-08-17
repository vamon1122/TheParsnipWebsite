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
        static readonly Album VideosAlbum = new Album(new Guid("73C436A1-893B-4418-8800-821823C18DFE"));

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage("videos", this, Data.DeviceType);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaManager.GetAlbumAsMediaControls(VideosAlbum))
            {
                links_div.Controls.Add(mc);
            }
        }
    }
}