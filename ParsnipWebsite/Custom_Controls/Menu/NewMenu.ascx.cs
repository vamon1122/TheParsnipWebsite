using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ParsnipWebsite.Custom_Controls.Menu
{
    public partial class NewMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private PageIndex _SelectedPage;
        private bool _LoggedIn;
        public PageIndex SelectedPage { get { return _SelectedPage; } set { HighlightButtonsForPage(value); _SelectedPage = value; } }
        private bool LoggedIn { get { return _LoggedIn; } set { 
            if(value == true)
                {
                    
                }
                _LoggedIn = value;
            } }

        private ParsnipData.Accounts.User _loggedInUser;
        public ParsnipData.Accounts.User LoggedInUser { get { return _loggedInUser; } set {
                
            if(value != null)
                {
                    Desktop_LogIn.Visible = false;
                    Mobile_LogIn.Visible = false;
                    Desktop_LogOut.Visible = true;
                    Mobile_LogOut.Visible = true;
                    CalcButtons();


                    _loggedInUser = value;
                }
            
            }}

        public bool Share
        {
            get { return Modal_Share.Visible; }
            set
            {
                Modal_Share.Visible = value;
                CalcButtons();
            }

        }
        public bool Upload
        {
            get { return Modal_Upload.Visible; }
            set
            {
                if(LoggedInUser != null && LoggedInUser.AccountType != "user")
                    Modal_Upload.Visible = value;

                CalcButtons();
            }

        }

        private void CalcButtons()
        {
            if (LoggedInUser != null)
            {
                if (Upload)
                {
                    if (LoggedInUser.AccountType == "admin")
                    {
                        Admin.Visible = true;
                        Modal_Upload.Visible = true;
                        right_content.Style.Remove("min-width");
                        right_content.Style.Add("min-width", "150px");
                    }
                    else if (LoggedInUser.AccountType != "user")
                    {
                        Modal_Upload.Visible = true;
                        right_content.Style.Remove("min-width");
                        right_content.Style.Add("min-width", "78px");
                    }
                }
                else if (Share)
                {
                    Modal_Share.Visible = true;
                    right_content.Style.Remove("min-width");

                    if (LoggedInUser.AccountType == "admin")
                    {
                        Admin.Visible = true;
                        right_content.Style.Add("min-width", "141px");
                    }
                    else
                    {
                        right_content.Style.Add("min-width", "69px");
                    }
                }
            }
        }

        public void HighlightButtonsForPage(PageIndex page, string buttonText)
        {
            if (string.IsNullOrEmpty(buttonText))
                throw new InvalidOperationException();

            var mainButton = GetMainButton(page);
            var mobileButton = GetMobileButton(page);

            HighlightButtons(mainButton, mobileButton, buttonText);

            
        }

        private HtmlAnchor GetMainButton(PageIndex page)
        {
            if (page.Equals(PageIndex.Home))
                return Desktop_Home;
            if (page.Equals(PageIndex.Photos))
                return Desktop_Photos;
            if (page.Equals(PageIndex.Videos))
                return Desktop_Videos;
            if (page.Equals(PageIndex.Memes))
                return Desktop_Memes;
            if (page.Equals(PageIndex.Bios))
                return Selected_Page;
            if (page.Equals(PageIndex.Krakow))
                return Selected_Page;
            if (page.Equals(PageIndex.Portugal))
                return Selected_Page;
            if (page.Equals(PageIndex.Amsterdam))
                return Selected_Page;
            if (page.Equals(PageIndex.EditMedia))
                return Selected_Page;
            if (page.Equals(PageIndex.Tag))
                return Selected_Page;
            if (page.Equals(PageIndex.MyUploads))
                return Selected_Page;
            if (page.Equals(PageIndex.View))
                return Selected_Page;

            return null;
        }

        private HtmlAnchor GetMobileButton(PageIndex page)
        {
            if (page.Equals(PageIndex.Home))
                return Mobile_Home;
            if (page.Equals(PageIndex.Photos))
                return Mobile_Photos;
            if (page.Equals(PageIndex.Videos))
                return Mobile_Videos;
            if (page.Equals(PageIndex.Memes))
                return Mobile_Memes;
            if (page.Equals(PageIndex.Bios) ||
            page.Equals(PageIndex.Krakow) ||
            page.Equals(PageIndex.Portugal) ||
            page.Equals(PageIndex.Amsterdam) ||
            page.Equals(PageIndex.EditMedia) ||
            page.Equals(PageIndex.Tag) ||
            page.Equals(PageIndex.MyUploads) ||
            page.Equals(PageIndex.View))
                return null;

            return null;
        }

        private void HighlightButtonsForPage(PageIndex page)
        {
            HighlightButtons(GetMainButton(page), GetMobileButton(page));
        }

        private void HighlightButtons(HtmlAnchor mainButton, HtmlAnchor mobileButton, string buttonText = null)
        {
            if (mobileButton != null)
                mobileButton.Visible = false;

            if (buttonText != null && mainButton != null)
                mainButton.InnerText = buttonText;

            if(mainButton == Selected_Page)
            {
                Selected_Page.Attributes.Remove("class");
                Selected_Page.Attributes.Add("class", "w3-bar-item w3-button w3-padding-large w3-white");
                Selected_Page.Visible = true;
            }
            else
            {
                mainButton.Attributes.Remove("class");
                mainButton.Attributes.Add("class", "w3-bar-item w3-button w3-padding-large w3-white w3-hide-small w3-hide-medium");
                Selected_Page.Attributes.Remove("class");
                Selected_Page.Attributes.Add("class", "w3-bar-item w3-button w3-padding-large w3-white w3-hide-large");
                Selected_Page.InnerHtml = mainButton.InnerHtml;
                mainButton.Visible = true;
                Selected_Page.Visible = true;
            }
        }
    }
}