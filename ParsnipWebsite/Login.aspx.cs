using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Cookies;
using ParsnipData.Logging;
using ParsnipData.Accounts;
using BenLog;
using System.Diagnostics;

namespace ParsnipWebsite
{
    public partial class Login : System.Web.UI.Page
    {
        private User myUser;
        private string Redirect;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["url"] != null)
            {
                Redirect = Request.QueryString["url"];
                Warning.Attributes.CssStyle.Add("display", "block");

            }
            else
            {
                Redirect = "home";
            }

            if (IsPostBack)
            {
                //I am postback! Login was probably clicked...
                var tempUsername = ""; 
                var tempPassword = "";

                try
                {
                    
                    
                }
                catch
                {

                }

                myUser = ParsnipData.Accounts.User.LogIn(Request["inputUsername"].ToString(), RememberPwd.Checked, Request["inputPwd"].ToString(), RememberPwd.Checked);
                if (myUser != null)
                {
                    new LogEntry(Log.LogInOut) { text = String.Format("{0} logged in from {1} {2}.", myUser.FullName, myUser.PosessivePronoun, Data.DeviceType) };
                    WriteCookie();
                    Response.Redirect(Redirect);
                }
            }
            myUser = new User("login");

            if (String.IsNullOrEmpty(inputUsername.Text) && String.IsNullOrWhiteSpace(inputUsername.Text))
            {
                if (ParsnipData.Accounts.User.LogIn() != null)
                {
                    WriteCookie();
                    Response.Redirect(Redirect);
                }
                else
                {
                    inputUsername.Text = myUser.Username;
                }
            }
        }

        private void WriteCookie()
        {
            Cookie.WritePerm("accountType", myUser.AccountType);
        }

        protected void ButLogIn_Click(object sender, EventArgs e)
        {
            
        }
    }
}