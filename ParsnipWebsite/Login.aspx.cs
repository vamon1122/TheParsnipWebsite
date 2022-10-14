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
using System.Web.Services;

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
                if (Redirect == "me")
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

        [WebMethod(EnableSession = true)]
        public static bool OnTryLogin(string username, string password, bool rememberPassword)
        {
            var Request = HttpContext.Current.Request;
            Debug.WriteLine("Logging In");

            var myUser = ParsnipData.Accounts.User.LogIn(username, rememberPassword, password, rememberPassword);

            if (myUser != null) Cookie.WritePerm("accountType", myUser.AccountType);

            return myUser != null;
        }
    }
}