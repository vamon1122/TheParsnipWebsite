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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("memes", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("memes?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

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