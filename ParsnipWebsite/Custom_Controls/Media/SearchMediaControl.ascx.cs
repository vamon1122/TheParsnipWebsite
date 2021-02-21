using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class SearchMediaControl : System.Web.UI.UserControl
    {
        public string TextBoxValue { get { return TextBox_SearchNew.Value; } set { TextBox_SearchNew.Value = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox_SearchNew.Attributes.Add("placeholder", ConfigurationManager.AppSettings["SearchPlaceholder"]);
        }

        protected void Button_Search_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (string.IsNullOrEmpty(TextBoxValue))
                    Response.Redirect($"{Request.Url.GetLeftPart(UriPartial.Path)}?alert=P103");
                else
                    Response.Redirect($"search?text={TextBoxValue}");

            }
        }
    }
}