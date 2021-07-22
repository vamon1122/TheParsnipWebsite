using System;
using System.Collections.Generic;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class MediaAccordion : System.Web.UI.UserControl
    {
        public List<MediaControl> Media
        {
            set
            {
                foreach (var m in value)
                {
                    MediaContainer.Controls.Add(m);
                }
            }
        }

        public string Text { set => MyText.InnerText = value; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Expander.Attributes.Add("onclick", $"myFunction('{this.ID}_Accordion', '{this.ID}_IsExpandedIndicator')");
        }
    }
}
