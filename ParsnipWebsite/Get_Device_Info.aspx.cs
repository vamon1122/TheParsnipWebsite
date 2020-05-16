using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Cookies;

namespace ParsnipWebsite
{
    public partial class Get_Device_Info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["url"] != null)
            {
                if (Request.QueryString["url"] == "me")
                {
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:title\" content=\"What DIRT do we have on YOU? 😜\" />"));
                    Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:image\" content=\"{Request.Url.GetLeftPart(UriPartial.Authority)}/Resources/Media/Images/Local/Dirt_On_You.png\" />"));
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:type\" content=\"website\" />"));
                    Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:url\" content=\"{Request.Url.ToString()}\" />"));
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:description\" content=\"What DIRT do we have on YOU? 😜\" />"));
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:alt\" content=\"What DIRT do we have on YOU? 😜\" />"));
                    Page.Header.Controls.Add(new LiteralControl("<meta property=\"fb:app_id\" content=\"521313871968697\" />"));
                }
            }
            System.Diagnostics.Debug.WriteLine("Writing sessionId cookie = " + Session.SessionID.ToString());
            Cookie.WriteSession("sessionId", Session.SessionID.ToString());
        }
    }
}