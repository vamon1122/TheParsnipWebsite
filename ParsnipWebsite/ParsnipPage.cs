using System;

namespace ParsnipWebsite
{
    public class ParsnipPage : System.Web.UI.Page
    {
        public readonly Guid PageId;

        public ParsnipPage()
        {
            PageId = Guid.NewGuid();
        }

        protected void Page_PreRender(object sender, EventArgs e) => Data.OnMediaUnFocused();
    }
}