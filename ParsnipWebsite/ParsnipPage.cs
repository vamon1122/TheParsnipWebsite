using System;
using System.Reflection;

namespace ParsnipWebsite
{
    public class ParsnipPage : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e) => Data.OnMediaUnFocused($"Pre-render");
    }
}