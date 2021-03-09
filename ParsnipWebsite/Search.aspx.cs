using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;
using ParsnipData.Accounts;
using ParsnipWebsite.Custom_Controls.Media;

namespace ParsnipWebsite
{
    public partial class Search_Website : System.Web.UI.Page
    {
        private User myUser;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["text"] == null)
            {
                myUser = Account.SecurePage("search", this, Data.DeviceType);
                SearchMediaControl.Focus();
            }
            else
                myUser = Account.SecurePage("search?text=" + Request.QueryString["text"], this, Data.DeviceType);
            
            NewMenu.LoggedInUser = myUser;
            NewMenu.Upload = true;
            NewMenu.HighlightButtonsForPage(PageIndex.Search, "Search");

            UploadMediaControl.Initialise(myUser, this, false);

            var text = Request.QueryString["text"];
            if (!IsPostBack && text != null)
            {
                SearchMediaControl.TextBoxValue = text;
                DoSearch(text);
            }
        }

        private void DoSearch(string searchTerm)
        {
            var searchResults = Media.Search(searchTerm, myUser.Id);
            MediaTitle.InnerText = searchResults.Media.Count > 0 ? "Content found:" : "No content was found using those search terms :(";


            TagsTitle.InnerText = (searchResults.MediaTags.Count > 0 || searchResults.Users.Count > 0) ? "Tags found:" : "No tags were found using those search terms :(";

            Page httpHandler = (Page)HttpContext.Current.Handler;
            List<ViewTagControl> ViewTagControls = new List<ViewTagControl>();
            foreach (MediaTag mediaTag in searchResults.MediaTags)
            {
                ViewTagControl mediaTagPairViewControl = (ViewTagControl)httpHandler.LoadControl("~/Custom_Controls/Media/ViewTagControl.ascx");
                mediaTagPairViewControl.MyTag = mediaTag;
                mediaTagPairViewControl.UpdateLink();
                MediaTagContainer.Controls.Add(mediaTagPairViewControl);
                ViewTagControls.Add(mediaTagPairViewControl);
            }

            foreach (ParsnipData.Accounts.User user in searchResults.Users)
            {
                ViewTagControl mediaUserPairViewControl = (ViewTagControl)httpHandler.LoadControl("~/Custom_Controls/Media/ViewTagControl.ascx");
                mediaUserPairViewControl.MyUser = user;
                mediaUserPairViewControl.UpdateLink();
                MediaTagContainer.Controls.Add(mediaUserPairViewControl);
                ViewTagControls.Add(mediaUserPairViewControl);
            }

            foreach (ViewTagControl control in ViewTagControls.OrderBy(x => x.Name))
            {
                MediaTagContainer.Controls.Add(control);
            }

            foreach (var mediaControl in MediaControl.GetMediaSearchResultAsMediaControls(searchResults))
            {
                MediaContainer.Controls.Add(mediaControl);
            }
        }
    }
}