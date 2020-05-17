using ParsnipData.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;
using ParsnipData.Logging;

namespace ParsnipWebsite
{
    public partial class Me : System.Web.UI.Page
    {
        User myUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = ParsnipData.Accounts.User.LogIn();

            if (myUser != null)
                Response.Redirect($"tag?user={myUser.Id}");
            else
                Response.Redirect("login?url=me");
        }
    }
}