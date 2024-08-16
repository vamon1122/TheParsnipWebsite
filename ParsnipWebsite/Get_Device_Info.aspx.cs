using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData;
using ParsnipData.Cookies;
using ParsnipData.Media;

namespace ParsnipWebsite
{
    public partial class Get_Device_Info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Writing sessionId cookie = " + Session.SessionID.ToString());
            Cookie.WriteSession("sessionId", Session.SessionID.ToString());

            var url = Request.QueryString["url"];
            

            if (url != null)
            {
                var uri = new Uri($"{Request.Url.GetLeftPart(UriPartial.Authority)}/{url}");
                int tagId = default;
                int userId = default;

                if (url.Contains("tag?id="))
                {
                    tagId = Convert.ToInt32(HttpUtility.ParseQueryString(uri.Query).Get("id"));
                }
                else if (url.Contains("tag?user="))
                {
                    Convert.ToInt32(HttpUtility.ParseQueryString(uri.Query).Get("user"));
                }
                else
                {
                    foreach (var id in (Data.MediaTagIds[])Enum.GetValues(typeof(Data.MediaTagIds)))
                    {
                        if (uri.ToString().Contains(id.ToString().ToLower()) || uri.ToString().Contains(id.ToString()))
                        {
                            tagId = (int)id;
                            break;
                        }
                    }
                }

                if (tagId != default)
                {
                    var myTag = MediaTag.Select(tagId);
                    if (myTag != null)
                    {
                        Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:title\" content=\"CLICK to 👀 #{myTag.Name} content!\" />"));
                        Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:description\" content=\"{myTag.Description}\" />"));
                        Page.Header.Controls.Add(new LiteralControl(string.Format("<meta property=\"og:alt\" content=\"{0}\" />", myTag.Description)));

                        addOGTagsForNonMediaEntity();

                        Page.Title = $"Tag: {myTag.Name}";
                    }
                } 
                else if (Request.QueryString["url"].Contains("tag?user="))
                {
                    
                    var myTagUser = ParsnipData.Accounts.User.Select(userId);
                    if (myTagUser != null)
                    {
                        Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:title\" content=\"@{myTagUser.Username} ({myTagUser.Forename}) was tagged in these 👀...\" />"));
                        Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:description\" content=\"See pictures and videos which {myTagUser.FullName} has been tagged in!\" />"));
                        Page.Header.Controls.Add(new LiteralControl(string.Format($"<meta property=\"og:alt\" content=\"See photos and videos which @{myTagUser.Username} ({myTagUser.FullName}) has been tagged in!\" />")));

                        addOGTagsForNonMediaEntity();

                        Page.Title = $"Tag: {myTagUser.Forename}";
                    }
                }
                else if (Request.QueryString["url"].Contains("view?share="))
                {
                    var myShare = new MediaShare(new MediaShareId(Request.QueryString["url"].Split('&')[0].Split('=').Last()));
                    myShare.Select();
                    var myMedia = Media.Select(myShare.MediaId);
                    Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:title\" content=\"{myMedia.Title}\" />"));
                    Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:image\" content=\"https://test.theparsnip.co.uk/{myMedia.Compressed}\" />"));
                    Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:url\" content=\"https://test.theparsnip.co.uk/view?share={myShare.Id}\" />"));
                    var tagsString = string.IsNullOrEmpty(myMedia.Description) ? myMedia.GetAtsAndTagsAsString() : myMedia.Description;
                        Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:description\" content=\"{tagsString}\" />"));
                }
            }
            Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:type\" content=\"website\" />"));
            Page.Header.Controls.Add(new LiteralControl("<meta property=\"fb:app_id\" content=\"521313871968697\" />"));

            

            void addOGTagsForNonMediaEntity()
            {
                Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:image\" content=\"{Request.Url.GetLeftPart(UriPartial.Authority)}/Resources/Media/Images/Web_Media/Lock.jpg\" />"));
                Page.Header.Controls.Add(new LiteralControl($"<meta property=\"og:url\" content=\"{Request.Url}\" />"));
                Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:image:width\" content=\"853\" />"));
                Page.Header.Controls.Add(new LiteralControl("<meta property=\"og:image:height\" content=\"480\" />"));
            }
        }
    }
}