using ParsnipData.Accounts;
using System;

namespace ParsnipWebsite
{
    public partial class Croatia : System.Web.UI.Page
    {
        private User myUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage(this, Data.DeviceType);
            NewMenu.LoggedInUser = myUser;
            NewMenu.SelectedPage = PageIndex.Croatia;
        }
    }
}
