using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class MediaTagPairViewControl : System.Web.UI.UserControl
    {
        ParsnipData.Media.Media myMedia;
        MediaTagPair myPair;

        public ParsnipData.Media.Media MyMedia { get { return myMedia; } set { myMedia = value; UpdateLink(); } }
        public MediaTagPair MyPair { get { return myPair; } set { myPair = value; ViewButton.InnerText = value.MediaTag.Name; UpdateLink(); } }

        private void UpdateLink()
        {
            if(MyMedia != null && MyPair != null && myPair.MediaTag != null)
                ViewButtonLink.HRef = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{MyPair.MediaTag.Name.ToLower()}?focus={myMedia.Id}";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}