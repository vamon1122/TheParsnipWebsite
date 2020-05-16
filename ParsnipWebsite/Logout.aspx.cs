using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Logging;

namespace ParsnipWebsite
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Data.DeviceType) || string.IsNullOrWhiteSpace(Data.DeviceType))
                Response.Redirect("get_device_info?url=logout");

            User myUser = new User("logout get name");
            ParsnipData.Accounts.User.LogIn();
            new LogEntry(Log.LogInOut) { text = String.Format("{0} logged out from {1} {2} device.", myUser.FullName, myUser.PosessivePronoun, Data.DeviceType) };
            ParsnipData.Accounts.User.LogOut();
            Response.Redirect("home");
        }
    }
}