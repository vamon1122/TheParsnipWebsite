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
    public partial class Amsterdam : System.Web.UI.Page
    {
        private User myUser;
        static readonly MediaTag AmsterdamMediaTag = new MediaTag(4);

        public Amsterdam()
        {
            //Retrieves wrong album ID and overwrites
            //AmsterdamAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("amsterdam", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("amsterdam?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

            NewMenu.SelectedPage = PageIndex.Amsterdam;
            NewMenu.LoggedInUser = myUser;
            NewMenu.Upload = true;
            NewMenu.Search = true;

            UploadMediaControl.Initialise(myUser, AmsterdamMediaTag, this);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            foreach (MediaControl mc in MediaControl.GetAlbumAsMediaControls(AmsterdamMediaTag))
            {
                DynamicMediaDiv.Controls.Add(mc);
            }
        }
    }
}