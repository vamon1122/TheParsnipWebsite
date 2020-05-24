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
        MediaTag myTag;

        public ParsnipData.Media.Media MyMedia { get { return myMedia; } set { myMedia = value; } }
        public MediaTagPair MyPair { get { return myPair; } set { myPair = value; MyTag = value.MediaTag; } }
        public MediaTag MyTag { get { return myTag; } set { myTag = value; ViewButton.InnerText = value.Name; } }

        public void UpdateLink()
        {
            string redirect = null;
            try
            {
                var defaultRedirect = $"tag?id={MyTag.Id}";
                if (myPair == null)
                {
                    redirect = defaultRedirect;
                }
                else
                {
                    switch (myPair.MediaTag.Id)
                    {
                        case (int)Data.MediaTagIds.Amsterdam:
                            redirect = $"amsterdam?focus={myMedia.Id}";
                            break;
                        case (int)Data.MediaTagIds.Krakow:
                            redirect = $"krakow?focus={myMedia.Id}";
                            break;
                        case (int)Data.MediaTagIds.Memes:
                            redirect = $"memes?focus={myMedia.Id}";
                            break;
                        case (int)Data.MediaTagIds.Photos:
                            redirect = $"photos?focus={myMedia.Id}";
                            break;
                        case (int)Data.MediaTagIds.Portugal:
                            redirect = $"portugal?focus={myMedia.Id}";
                            break;
                        case (int)Data.MediaTagIds.Videos:
                            redirect = $"videos?focus={myMedia.Id}";
                            break;
                        case default(int):
                            redirect = $"manage_media?id={MyMedia.Id}";
                            break;
                        default:
                            redirect = $"{defaultRedirect}&focus={MyMedia.Id}";
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                
            }

            if (!string.IsNullOrEmpty(redirect))
                ViewButtonLink.HRef = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{redirect}";
            else
                ViewButtonLink.HRef = $"Error";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}