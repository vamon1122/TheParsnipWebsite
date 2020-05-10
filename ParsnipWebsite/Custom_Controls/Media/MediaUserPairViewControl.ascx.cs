using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class MediaUserPairViewControl : System.Web.UI.UserControl
    {
        ParsnipData.Media.Media myMedia;
        MediaUserPair myPair;

        public ParsnipData.Media.Media MyMedia { get { return myMedia; } set { myMedia = value; } }
        public MediaUserPair MyPair { get { return myPair; } set { myPair = value; ViewButton.InnerText = value.Name; } }

        public void UpdateLink()
        {
            string redirect = $"tag?userid={myPair.UserId}&focus={MyMedia.Id}";
            if (MyMedia != null && MyPair != null && myPair.UserId != default)
                ViewButtonLink.HRef = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{redirect}";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}