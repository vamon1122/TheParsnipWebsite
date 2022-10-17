using ParsnipData.Accounts;
using ParsnipData.Media;
using System.Configuration;
using System.Diagnostics;
using System.Web.Services;
using System.Web;
using System;
using System.Linq;

namespace ParsnipWebsite
{
    public class MediaViewPage : System.Web.UI.Page
    {
        [WebMethod]
        public static void OnMediaCenterScreen(string containerId)
        {
            var thisViewId = Guid.NewGuid();
            var session = HttpContext.Current.Session;
            session["CurrentViewId"] = thisViewId.ToString();
            var splitContainerId = containerId.Split('_');
            if (splitContainerId.Length < 2 || splitContainerId.Last() == "thumbnail")
            {
                session["CurrentViewMediaId"] = null;
                Debug.WriteLine($"Video focused (Ignoring)");
                return;
            }
            StartImageViewTimer(thisViewId, new MediaId(splitContainerId.Last()), ParsnipData.Accounts.User.LogIn());
            void StartImageViewTimer(Guid viewId, MediaId mediaId, User loggedInUser)
            {
                session["CurrentViewMediaId"] = mediaId;
                Debug.WriteLine($"Image focused ({mediaId} is an image. Starting timer...)");
                System.Timers.Timer insertViewTimer;
                insertViewTimer = new System.Timers.Timer(Convert.ToInt16(ConfigurationManager.AppSettings["InsertImageViewAfterMilliseconds"]));
                insertViewTimer.Elapsed += (sender, e) => OnImageViewTimerComplete();
                insertViewTimer.AutoReset = false;
                insertViewTimer.Enabled = true;

                void OnImageViewTimerComplete()
                {
                    if (viewId.ToString() == session["CurrentViewId"]?.ToString())
                    {
                        var tempMedia = new Media() { Id = mediaId };
                        tempMedia.View(loggedInUser);
                        Debug.WriteLine($"View inserted ({mediaId} was viwed continuously for 2 seconds)");
                        return;
                    }
                    Debug.WriteLine($"View NOT inserted ({mediaId} was NOT viewed continuously for 2 seconds)");
                }
            }
        }

        [WebMethod]
        public static void OnMediaUnFocused()
        {
            HttpContext.Current.Session["CurrentViewId"] = null;
            Debug.WriteLine($"Media was un-focused. View cancelled");
        }

        [WebMethod]
        public static void OnMediaReFocused()
        {
            var session = HttpContext.Current.Session;
            //session["CurrentViewId"] = Guid.NewGuid();
            if(session["CurrentViewMediaId"] == null)
            {
                Debug.WriteLine($"There was no media to re-focus");
            }
            else
            {
                Debug.WriteLine($"Media was Re-focused. View re-started");
                OnMediaCenterScreen("control_" + session["CurrentViewMediaId"].ToString());
            }
        }
    }
}
