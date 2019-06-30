﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;
using ParsnipData.Logs;

namespace ParsnipWebsite.Custom_Controls.Media_Api
{
    public partial class ImageControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private ParsnipData.Media.Image _myImage;
        public ParsnipData.Media.Image MyImage { get { return _myImage; } set
            {
                _myImage = value;
                MyTitle.InnerHtml = MyImage.Title;
                MyImageHolder.ImageUrl = "../../Resources/Media/Images/Web_Media/placeholder.gif";
                MyImageHolder.Attributes.Add("data-src", MyImage.Directory);
                MyImageHolder.Attributes.Add("data-srcset", MyImage.Directory);
                MyImageHolder.CssClass = "lazy";
                MyImageHolder.Style.Add("margin-bottom", "8px");
                MyImageContainer.ID = _myImage.Id.ToString();

                

                MyEdit.HRef = string.Format("../../edit_image?imageid={0}", MyImage.Id);
                MyShare.HRef = string.Format("../../view_image?imageid={0}", MyImage.Id);
            }
        }
    }

    
}