using System;
using System.Web.UI.HtmlControls;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public abstract class CommonMediaControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected ParsnipData.Media.Media _myMedia;
        public ParsnipData.Media.Media MyMedia
        {
            get
            {
                return _myMedia;
            }
            set
            {
                _myMedia = value;

                SetMediaLogic(value);
            }

        }

        public string Redirect { get; set; }

        protected abstract void SetMediaLogic(ParsnipData.Media.Media value);
        protected abstract void SetAnchorLink(string value);

        public string AnchorLink { get { return _anchorLink; } set { _anchorLink = value; SetAnchorLink(value); } }
        private string _anchorLink;

        public string ShareLink
        {
            get
            {
                if (_anchorLink != null)
                {
                    return _anchorLink;
                }

                if (MyMedia.MyMediaShare == null || MyMedia.MyMediaShare.Id.ToString() == default)
                {
                    return "You must log in to share media";
                }
                else
                {
                    return Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" + MyMedia.MyMediaShare.Id;
                    /*
                    if(MyMedia.Type == "image")
                        return Request.Url.GetLeftPart(UriPartial.Authority) + "/view?share=" + MyMedia.MyMediaShare.Id;
                    else
                        return Request.Url.GetLeftPart(UriPartial.Authority) + "/watch?share=" + MyMedia.MyMediaShare.Id;
                        */

                };
            }
        }

        protected double width = 100;
        protected double maxWidth = 480;
        protected void SetContainerWidth(HtmlGenericControl MediaContainer)
        {
            

            //width = Data.IsMobile ? 100 : 30;
            //min_width = Data.IsMobile ? 0 : 480;



            MediaContainer.Style.Add("width", string.Format("{0}vmin", width));
            MediaContainer.Style.Add("max-width", string.Format("{0}px", maxWidth));
            //MediaContainer.Style.Add("min-width", string.Format("{0}px", min_width));

        }

        public ParsnipData.Media.MediaTag MyMediaTag { get; set; }

        public ParsnipData.Media.MediaUserPair MyMediaUserPair { get; set; }

        public string MySearch { get; set; }

        protected abstract void GenerateShareButton();
    }
}