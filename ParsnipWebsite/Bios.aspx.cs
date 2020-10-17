using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;

namespace ParsnipWebsite
{
    public partial class Bios : System.Web.UI.Page
    {
        private User myUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage("bios", this, Data.DeviceType);
            NewMenu.LoggedInUser = myUser;
            NewMenu.SelectedPage = PageIndex.Bios;
        }
    }
}