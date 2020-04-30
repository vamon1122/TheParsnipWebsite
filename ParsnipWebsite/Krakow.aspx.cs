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
    public partial class Krakow : System.Web.UI.Page
    {
        private User myUser;
        static readonly MediaTag KrakowMediaTag = new MediaTag(6);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("krakow", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("krakow?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

            UploadMediaControl.Initialise(myUser, KrakowMediaTag, this);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaControl.GetAlbumAsMediaControls(KrakowMediaTag))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}