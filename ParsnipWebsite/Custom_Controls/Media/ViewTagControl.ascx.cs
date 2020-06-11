using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Media;

namespace ParsnipWebsite.Custom_Controls.Media
{
    public partial class ViewTagControl : System.Web.UI.UserControl
    {
        ParsnipData.Media.Media myMedia;
        MediaUserPair myUserPair;
        MediaTagPair myTagPair;
        MediaTag _myTag;
        int _myUserId;
        User _myUser;

        public string Name { get {
                if (MyUserPair != null)
                    return MyUserPair.Name;

                if (MyUser != null)
                    return MyUser.Username;

                if (MyTag != null)
                    return MyTag.Name;

                return "";
            
            } }
        public MediaTagPair MyTagPair { get { return myTagPair; } set { myTagPair = value; MyTag = value.MediaTag; } }
        public MediaTag MyTag { get { return _myTag; } set { _myTag = value; ViewButton.InnerText = $"#{value.Name}"; } }

        public ParsnipData.Media.Media MyMedia { get { return myMedia; } set { myMedia = value; } }
        public MediaUserPair MyUserPair { get { return myUserPair; } set { MyUserId = value.UserId; myUserPair = value; ViewButton.InnerText = $"@{value.Name}"; } }

        public int MyUserId { get { return _myUserId; } set { _myUserId = value; } }

        public User MyUser { get { return _myUser; } set { ViewButton.InnerText = $"@{value.Username}"; _myUser = value; MyUserId = value.Id;  } }

        public void UpdateLink()
        {
            if (MyUserId != default)
            {
                string redirect = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/tag?user={MyUserId}";
                if (MyMedia != null)
                    ViewButtonLink.HRef = $"{redirect}&focus={MyMedia.Id}";
                else
                    ViewButtonLink.HRef = redirect;
            }
            else if(MyTag != null){
                string redirect = null;
                try
                {
                    var defaultRedirect = $"tag?id={MyTag.Id}";
                    if (MyTag == null)
                    {
                        redirect = defaultRedirect;
                    }
                    else
                    {
                        switch (MyTag.Id)
                        {
                            case (int)Data.MediaTagIds.Amsterdam:
                                redirect = $"amsterdam";
                                if (MyMedia != null && myMedia.Id != null)
                                    redirect += $"?focus={myMedia.Id}";
                                break;
                            case (int)Data.MediaTagIds.Krakow:
                                redirect = $"krakow";
                                if (MyMedia != null && myMedia.Id != null)
                                    redirect += $"?focus={myMedia.Id}";
                                break;
                            case (int)Data.MediaTagIds.Memes:
                                redirect = $"memes";
                                if (MyMedia != null && myMedia.Id != null)
                                    redirect += $"?focus={myMedia.Id}";
                                break;
                            case (int)Data.MediaTagIds.Photos:
                                redirect = $"photos";
                                if (MyMedia != null && myMedia.Id != null)
                                    redirect += $"?focus={myMedia.Id}";
                                break;
                            case (int)Data.MediaTagIds.Portugal:
                                redirect = $"portugal";
                                if (MyMedia != null && myMedia.Id != null)
                                    redirect += $"?focus={myMedia.Id}";
                                break;
                            case (int)Data.MediaTagIds.Videos:
                                redirect = $"videos";
                                if (MyMedia != null && myMedia.Id != null)
                                    redirect += $"?focus={myMedia.Id}";
                                break;
                            case default(int):
                                redirect = $"manage_media";
                                if (MyMedia != null && myMedia.Id != null)
                                    redirect += $"?id={MyMedia.Id}";
                                break;
                            default:
                                redirect = $"{defaultRedirect}";
                                if (MyMedia != null && myMedia.Id != null)
                                    redirect += $"&focus={MyMedia.Id}";
                                break;
                        }

                        

                    }
                }
                catch (Exception ex)
                {

                }

                if (!string.IsNullOrEmpty(redirect))
                    ViewButtonLink.HRef = $"{HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)}/{redirect}";
                else
                    ViewButtonLink.HRef = $"Error";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}