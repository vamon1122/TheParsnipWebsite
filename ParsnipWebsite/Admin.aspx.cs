using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using System.Data.SqlClient;
using ParsnipData.Logs;
using ParsnipData;

namespace ParsnipWebsite
{
    public partial class Admin : System.Web.UI.Page
    {
        User myAccount;
        Log Debug = new Log("Debug");
        protected void Page_Load(object sender, EventArgs e)
        {
            myAccount = Account.SecurePage("admin", this, Data.DeviceType, "admin");
        }

        protected void OpenLogsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs");
        }

        protected void NewUserButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("create-user");
        }

        
    }
}