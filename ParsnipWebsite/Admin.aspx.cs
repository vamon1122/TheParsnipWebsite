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
using System.Reflection;
using System.Configuration;
using System.Web.Configuration;

namespace ParsnipWebsite
{
    public partial class Admin : System.Web.UI.Page
    {
        User myAccount;
        static readonly Log Debug = Log.Select(3);
        protected void Page_Load(object sender, EventArgs e)
        {
            myAccount = Account.SecurePage("admin", this, Data.DeviceType, "admin");
            Assembly parsnipWebsiteAssembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "bin\\ParsnipWebsite.dll");
            Version parsnipWebsiteVersion = parsnipWebsiteAssembly.GetName().Version;

            Assembly parsnipDataAssembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "bin\\ParsnipData.dll");
            Version parsnipDataVersion = parsnipDataAssembly.GetName().Version;

            Label_ParsnipWebsiteVersion.Text = "ParsnipWebsite v" + parsnipWebsiteVersion.ToString();
            Label_ParsnipDataVersion.Text = "ParsnipData v" + parsnipDataVersion.ToString();

            TextBox_MOTD.Text = ConfigurationManager.AppSettings["MOTD"];
        }

        protected void OpenLogsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("logs");
        }

        protected void NewUserButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("create-user");
        }

        protected void Button_UploadDataId_Click(object sender, EventArgs e)
        {
            Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            string newMOTD = Request.Form["TextBox_MOTD"];
            webConfigApp.AppSettings.Settings["MOTD"].Value = newMOTD;
            TextBox_MOTD.Text = newMOTD;
            webConfigApp.Save();

        }
    }
}