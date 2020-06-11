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
using ParsnipData.Media;

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

                if(Request.QueryString["url"] == "me")
                {
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:title\" content=\"What DIRT do we have on YOU? 😜\" />"));
                    Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:image\" content=\"{Request.Url.GetLeftPart(UriPartial.Authority)}/Resources/Media/Images/Local/Dirt_On_You.jpg\" />"));
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:type\" content=\"website\" />"));
                    Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:url\" content=\"{Request.Url}\" />"));
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:description\" content=\"See every piece of #TheParsnip content you've ever featured in!\" />"));
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:alt\" content=\"What DIRT do we have on YOU? 😜\" />"));
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"fb:app_id\" content=\"521313871968697\" />"));
                }
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
                    new LogEntry(Log.LogInOut) { Text = String.Format("{0} logged in from {1} {2}.", myUser.FullName, myUser.PosessivePronoun, Data.DeviceType) };
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