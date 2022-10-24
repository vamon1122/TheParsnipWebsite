using System;
using System.Reflection;

namespace ParsnipWebsite
{
    public class ParsnipPage : System.Web.UI.Page
    {
        private Guid _pageId;

        public ParsnipPage()
        {
            _pageId = Guid.NewGuid();
        }
        protected void Page_PreRender(object sender, EventArgs e) => Data.OnMediaUnFocused($"Pre-render ({_pageId})");
    }
}