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
                var milliseconds = Convert.ToInt16(ConfigurationManager.AppSettings["InsertImageViewAfterMilliseconds"]);
                insertViewTimer = new System.Timers.Timer(milliseconds);
                insertViewTimer.Elapsed += (sender, e) => OnImageViewTimerComplete();
                insertViewTimer.AutoReset = false;
                insertViewTimer.Enabled = true;

                void OnImageViewTimerComplete()
                {
                    if (viewId.ToString() == session["CurrentViewId"]?.ToString())
                    {
                        var tempMedia = new Media() { Id = mediaId };
                        tempMedia.View(loggedInUser, true, milliseconds);
                        session["CurrentViewMediaId"] = null;
                        session["CurrentViewId"] = null;
                        Debug.WriteLine($"View inserted ({mediaId} was viwed continuously for 2 seconds)");
                        return;
                    }
                    Debug.WriteLine($"View NOT inserted ({mediaId} was NOT viewed continuously for 2 seconds)");
                }
            }
        }

        [WebMethod]
        public static void OnMediaUnFocused() => Data.OnMediaUnFocused();

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
                Debug.WriteLine($"Refocusing media...");
                OnMediaCenterScreen("control_" + session["CurrentViewMediaId"].ToString());
            }
        }

        [WebMethod]
        public static void OnClose() => Debug.WriteLine("Tab closed");
    }
}
