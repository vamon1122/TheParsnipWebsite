using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;
using ParsnipData.Accounts;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class UploadMediaControl : System.Web.UI.UserControl
    {
        User LoggedInUser;
        ParsnipData.Media.MediaTag MyMediaTag;
        Page myPage;

        public void Initialise(User loggedInUser, MediaTag myMediaTag, Page page)
        {
            myPage = page;
            LoggedInUser = loggedInUser;
            MyMediaTag = myMediaTag;
            if (LoggedInUser.AccountType == "admin" || LoggedInUser.AccountType == "member")
                UploadDiv.Style.Clear();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (MediaUpload.PostedFile.ContentLength > 0)
                    MediaManager.UploadImage(LoggedInUser, MyMediaTag, MediaUpload);
            }
        }

        protected void Button_UploadDataId_Click(object sender, EventArgs e)
        {
            var rawDataId = TextBox_UploadDataId.Text;
            var dataId = TextBox_UploadDataId.Text.Substring(rawDataId.Length - 11, 11);
            Youtube myYoutube = new Youtube(dataId, LoggedInUser, MyMediaTag);
            myYoutube.Scrape();
            myYoutube.Insert();

            Response.Redirect($"edit_media?id={myYoutube.Id}&tag={MyMediaTag.Id}");
        }
    }
}