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
    public partial class Portugal : System.Web.UI.Page
    {
        private User myUser;
        static readonly MediaTag PortugalMediaTag = new MediaTag(5);

        public Portugal()
        {
            //Retrieves wrong album ID and overwrites
            //PortugalAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["focus"] == null)
                myUser = Account.SecurePage("portugal", this, Data.DeviceType);
            else
                myUser = Account.SecurePage("portugal?focus=" + Request.QueryString["focus"], this, Data.DeviceType);

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