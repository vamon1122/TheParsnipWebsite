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
    public class MediaViewPage : ParsnipPage
    {
        [WebMethod]
        public static void OnMediaCenterScreen(string containerId)
        {
            var timer = new Stopwatch();
            timer.Start();
            var thisViewId = Guid.NewGuid();
            var session = HttpContext.Current.Session;
            var splitContainerId = containerId.Split('_');
            var PageId = splitContainerId[2];
            session[$"{PageId}_CurrentViewId"] = thisViewId.ToString();
            if (splitContainerId.Length < 2 || splitContainerId.Last() == "thumbnail")
            {
                session[$"{PageId}_CurrentViewMediaId"] = null;
                Debug.WriteLine($"Video focused (Ignoring)");
                return;
            }
            StartImageViewTimer(thisViewId, PageId, $"{splitContainerId[3]}", ParsnipData.Accounts.User.LogIn());
            void StartImageViewTimer(Guid viewId, string pageId, string mediaId, User loggedInUser)
            {
                session[$"{pageId}_CurrentViewMediaId"] = mediaId;
                Debug.WriteLine($"Image focused ({mediaId} is an image. Starting timer...)");
                System.Timers.Timer minInsertViewTimer;
                var imageViewThreshold = TimeSpan.FromMilliseconds(Convert.ToInt16(ConfigurationManager.AppSettings["InsertImageViewAfterMilliseconds"]));
                minInsertViewTimer = new System.Timers.Timer(imageViewThreshold.TotalMilliseconds);
                minInsertViewTimer.Elapsed += (sender, e) => OnImageViewThresholdMet();
                minInsertViewTimer.AutoReset = false;
                minInsertViewTimer.Enabled = true;

                void OnImageViewThresholdMet()
                {
                    if (viewId.ToString() == session[$"{pageId}_CurrentViewId"]?.ToString())
                    {
                        System.Timers.Timer checkViewStillInFocus;
                        checkViewStillInFocus = new System.Timers.Timer(1);
                        checkViewStillInFocus.Elapsed += (sender, e) => OnViewStillInFocus();
                        checkViewStillInFocus.AutoReset = true;
                        checkViewStillInFocus.Enabled = true;

                        void OnViewStillInFocus()
                        {
                            if (viewId.ToString() != session[$"{pageId}_CurrentViewId"]?.ToString())
                            {
                                checkViewStillInFocus.Close();
                                timer.Stop();

                                TimeSpan timeTaken = timer.Elapsed;
                                var tempMedia = new Media() { Id = new MediaId(mediaId.Split('_').Last()) };
                                tempMedia.View(loggedInUser, true, imageViewThreshold, timer.Elapsed);
                                Debug.WriteLine($"View inserted ({mediaId} was viewed continuously for {timeTaken.TotalSeconds} seconds)");
                                return;
                            }
                        }
                    }
                    else Debug.WriteLine($"View NOT inserted ({mediaId} was NOT viewed continuously for {imageViewThreshold.TotalSeconds} seconds)");
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
                OnMediaCenterScreen("control_id_" + session["CurrentViewMediaId"].ToString());
            }
        }

        [WebMethod]
        public static void OnClose() => Debug.WriteLine("Tab closed");

        [WebMethod]
        public static void OnMenuOpenMediaNotReFocused() => Debug.WriteLine("Menu is open! Media not refocused.");

        [WebMethod]
        public static void OnMenuOpenMediaNotFocused() => Debug.WriteLine("Menu is open! Media not focused.");
    }
}
