using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class MediaTagPairControl : System.Web.UI.UserControl
    {
        ParsnipData.Media.Media myMedia;
        MediaTagPair myPair;

        public ParsnipData.Media.Media MyMedia { get { return myMedia; } set { myMedia = value; } }
        public MediaTagPair MyPair { get { return myPair; } set { myPair = value;  DeleteButton.InnerText = value.MediaTag.Name;  } }

        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateDeleteButton();
        }
        private void GenerateDeleteButton()
        {
            Guid tempGuid = Guid.NewGuid();
            DeleteButton.Attributes.Add("onclick", $"document.getElementById('delete{tempGuid}').style.display='block'");



            /* 
             * This is an ugly fix to a problem which I was struggling to work out. The HTML which is generated below
             * is for the modal which contains the media share link. Originally, this was contained in the ascx page of
             * this user control, however, this did not work because the top modal was always triggered, regardless of 
             * which media item was clicked. To fix this, the id of the modal (and therfore data-target of the share 
             * button) had to be unique. So, I tried generating unique ids in javascript. However, js isn't executed in 
             * user-controls without some extra code which I didn't understand. So then I tried running the modal div 
             * at the server so that I could set the id from the code behind. However, for whatever reason, I 
             * discovered that the modal would never be triggered if it was run at the server. So finally, I came up 
             * with this. Just generating the modal HTML in a string and then inserting it into the user-control. Not 
             * pretty, but it works :P.
             */
            

            modalDiv.InnerHtml =
                "\n" +
                $"   <div class=\"w3-modal\" id=\"delete{tempGuid}\" onclick=\"void(0)\">\n" +
                "       <div class=\"w3-modal-content w3-display-middle modal-content\">\n" +
                "           <header class=\"w3-container w3-red\">\n" +
                "	            <h3>Confirm Delete</h3>\n" +
                "           </header>\n" +
                "           <div class=\"w3-container\">\n" +
                $"	            <p>Are you sure that you want to remove the tag \"{MyPair.MediaTag.Name}\" from the {myMedia.Type} called: \"{myMedia.Title}\"?</p>\n" + 
                "           </div>\n" +
                "           <div class=\"w3-margin-bottom\">\n" +
                $"              <a href=\"edit?removetag=true&id={myMedia.Id}&tag={MyPair.MediaTag.Id}\" style=\"color: inherit;text-decoration: none;\">\n" +
                "	                <button type=\"button\" class=\"w3-btn w3-red\" >REMOVE</button>\n" +
                "               </a>\n" +
                $"            <button type=\"button\" class=\"w3-btn w3-black\" onclick=\"document.getElementById('delete{tempGuid}').style.display='none'\">Cancel</button>\n" + 
                "           </div>\n" +
                "       </div>\n" +
                "   </div>\n";
        }
    }
}
