using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Media;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class MediaUserPairControl : System.Web.UI.UserControl
    {
        ParsnipData.Media.Media myMedia;
        MediaUserPair myPair;

        public ParsnipData.Media.Media MyMedia { get { return myMedia; } set { myMedia = value; } }
        public MediaUserPair MyPair { get { return myPair; } set { myPair = value; DeleteButton.InnerText = value.Name; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateDeleteButton();
        }
        private void GenerateDeleteButton()
        {
            Guid tempGuid = Guid.NewGuid();
            DeleteButton.Attributes.Add("data-target", "#delete" + tempGuid);



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
                $"   <div class=\"modal fade\" id=\"delete{tempGuid}\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"deleteMediaTagPair\" aria-hidden=\"true\">\n" +
                "       <div class=\"modal-dialog modal-dialog-centered\" role=\"document\">\n" +
                "           <div class=\"modal-content\" style=\"margin:0px; padding:0px\">\n" +
                "               <div class=\"modal-header\">\n" +
                "	                <h5 class=\"modal-title\">Confirm Remove</ h5 >\n" +
                "	                <button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-label=\"Close\">\n" +
                "		                <span aria-hidden=\"true\">&times;</ span >\n" +
                "	                </button>\n" +
                "               </div>\n" +
                "               <div class=\"modal-body\">\n" +
                $"	                <p>Are you sure that you want to remove the tag \"{MyPair.Name}\" from the {myMedia.Type} called: \"{myMedia.Title}\"?</ p >\n" +
                "               </div>\n" +
                "               <div class=\"modal-footer\">\n" +
                $"                  <a href=\"Edit_Media?removeusertag=true&id={myMedia.Id}&userid={MyPair.UserId}\" style=\"color: inherit;text-decoration: none;\">\n" +
                "	                    <button type=\"button\" class=\"btn btn-danger\" >REMOVE</ button >\n" +
                "                  </a>\n" +
                "	                <button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">Cancel</ button >\n" +
                "              </div>\n" +
                "           </div>\n" +
                "      </div>\n" +
                "   </div>\n";
        }
    }
}