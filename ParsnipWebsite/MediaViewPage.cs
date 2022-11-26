using ParsnipData.Accounts;
using ParsnipData.Media;
using System.Configuration;
using System.Diagnostics;
using System.Web.Services;
using System.Web;
using System;
using System.Linq;
using ParsnipData.Logging;

namespace ParsnipWebsite
{
    public class MediaViewPage : System.Web.UI.Page
    {
        private static TimeSpan ImageViewThreshold { get => TimeSpan.FromMilliseconds(Convert.ToInt16(ConfigurationManager.AppSettings["InsertImageViewAfterMilliseconds"])); }

        [WebMethod(EnableSession = true)]
        public static void WriteDebug(string someText) => Debug.WriteLine(someText);

        [WebMethod(EnableSession = true)]
        public static void OnMediaViewed(string mediaIdString, double duration, bool tabClosed)
        {
            var timeTaken = TimeSpan.FromMilliseconds(duration);
            if (timeTaken < ImageViewThreshold)
            {
                Debug.WriteLine("JS: FYI, this JS view is only missing because when JS recorded the view time (more accurately), it was less than the threshold");
                return;
            }


            if (string.IsNullOrEmpty(mediaIdString))
            {
                //TODO - Delete debugging if this solution is acceptable
                Debug.WriteLine($"JS: Media view was triggered {(tabClosed ? "on tab close" : string.Empty)} but ID was empty!");
                return;
            }

            var mediaId = new MediaId(mediaIdString);
            var session = HttpContext.Current.Session;
            var request = HttpContext.Current.Request;
            string baseUrl = request.Url.Scheme + "://" + request.Url.Authority + request.ApplicationPath.TrimEnd('/') + "/";
            var tempMedia = Media.Select(mediaId);
            if(tempMedia == null)
            {
                Debug.WriteLine($"JS: Media view was triggered {(tabClosed ? "on tab close" : string.Empty)} but no media was found with id '{mediaId}'");
                return;
            }
            var loggedInUser = ParsnipData.Accounts.User.LogIn();

            var isUntitled = string.IsNullOrEmpty(tempMedia.Title);
            new LogEntry(Log.Access, session.SessionID) { Text = $"JS: {loggedInUser.FullName} {(tabClosed ? "closed a tab after scrolling" : "scrolled")} an{(isUntitled ? " untitled " : " ")}image ({mediaId}) for {Math.Round(timeTaken.TotalSeconds, 0, MidpointRounding.AwayFromZero)} secs{(isUntitled ? string.Empty : $": {tempMedia.Title}")} (<a href=\"{baseUrl}view?id={mediaId}\">view?id={mediaId}</a>)" };
        }

        [WebMethod]
        public static void OnMediaCenterScreen(string containerId, string bodyId, bool tabInFocus)
        {
            var timer = new Stopwatch();
            timer.Start();
            var dateTimeViewStarted = ParsnipData.Parsnip.AdjustedTime;

            var thisViewId = Guid.NewGuid();
            var session = HttpContext.Current.Session;
            var request = HttpContext.Current.Request;
            string baseUrl = request.Url.Scheme + "://" + request.Url.Authority +  request.ApplicationPath.TrimEnd('/') + "/";
            session[$"{bodyId}_CurrentViewId"] = thisViewId.ToString();
            var splitContainerId = containerId.Split('_');
            if (splitContainerId.Contains("thumbnail"))
            {
                Debug.WriteLine($"Video focused (Ignoring)");
                session[$"{bodyId}_CurrentViewMediaId"] = null;
                session[$"{bodyId}_ThumbnailIsCurrentlyFocused"] = true;
                return;
            }

            session[$"{bodyId}_ThumbnailIsCurrentlyFocused"] = false;

            if (tabInFocus)
            {
                Debug.WriteLine("Tab is not in focus. View postponed");
                session[$"{bodyId}_CurrentUnfocusedViewMediaId"] = splitContainerId.Last();
                session[$"{bodyId}_CurrentViewMediaId"] = null;
                session[$"{bodyId}_CurrentViewId"] = null;
                return;
            }

            StartImageViewTimer(thisViewId, new MediaId(splitContainerId.Last()), ParsnipData.Accounts.User.LogIn());

            void StartImageViewTimer(Guid viewId, MediaId mediaId, User loggedInUser)
            {
                var currentView = HttpContext.Current?.Session[$"{bodyId}_CurrentViewId"] ?? "HttpContext was null";

                session[$"{bodyId}_CurrentViewMediaId"] = mediaId;
                //Debug.WriteLine($"C#: Image focused ({mediaId} is an image. Starting timer for view {currentView}...)");
                Debug.WriteLine($"C#: Doing view. ViewID = {currentView}");
                System.Timers.Timer minInsertViewTimer;
                var currentImageThreshold = ImageViewThreshold;
                minInsertViewTimer = new System.Timers.Timer(currentImageThreshold.TotalMilliseconds);
                minInsertViewTimer.Elapsed += (sender, e) => OnImageViewThresholdMet();
                minInsertViewTimer.AutoReset = false;
                minInsertViewTimer.Enabled = true;

                void OnImageViewThresholdMet()
                {
                    if (viewId.ToString() == session[$"{bodyId}_CurrentViewId"]?.ToString())
                    {
                        System.Timers.Timer checkViewStillInFocus;
                        checkViewStillInFocus = new System.Timers.Timer(1);
                        checkViewStillInFocus.Elapsed += (sender, e) => OnViewStillInFocus();
                        checkViewStillInFocus.AutoReset = true;
                        checkViewStillInFocus.Enabled = true;

                        void OnViewStillInFocus()
                        {
                            if (viewId.ToString() != session[$"{bodyId}_CurrentViewId"]?.ToString())
                            {
                                timer.Stop();
                                checkViewStillInFocus.Close();

                                TimeSpan timeTaken = timer.Elapsed;
                                var tempMedia = Media.Select(mediaId);
                                tempMedia.View(loggedInUser, true, dateTimeViewStarted, currentImageThreshold, timer.Elapsed);
                                var isUntitled = string.IsNullOrEmpty(tempMedia.Title);
                                new LogEntry(Log.Access, session.SessionID) { Text = $"{loggedInUser.FullName} scrolled an{(isUntitled ? " untitled " : " ")}image for {Math.Round(timeTaken.TotalSeconds, 0, MidpointRounding.AwayFromZero)} secs{(isUntitled ? string.Empty : $": {tempMedia.Title}")} (<a href=\"{baseUrl}view?id={mediaId}\">view?id={mediaId}</a>)" };
                                Debug.WriteLine($"C#: The view which was just inserted was {currentView}");
                                return;
                            }
                        }
                    }
                }
            }
        }

        [WebMethod]
        public static void OnMediaUnFocused(string feedback, string bodyId) { var currentView = HttpContext.Current.Session[$"{bodyId}_CurrentViewId"]; Debug.WriteLine($"C# Blur. Current view is {currentView}"); Data.OnMediaUnFocused(feedback, bodyId); }

        [WebMethod(EnableSession = true)]
        public static bool OnMediaReFocused(string bodyId, string feedback)
        {
            var session = HttpContext.Current.Session;
            var loggedInUser = ParsnipData.Accounts.User.LogIn();

            if (loggedInUser == null)
            { 
                new LogEntry(Log.Access, session.SessionID) { Text = $"Someone returned to a media page after they had logged out. Refresh is required... ({feedback})" };
                return true;
            }

            if (session[$"{bodyId}_CurrentUnfocusedViewMediaId"] == null)
            {
                if (Convert.ToBoolean(session[$"{bodyId}_PageHasHadFocusInTheCurrentSession"]))
                {
                    Debug.WriteLine($"{loggedInUser.FullName} returned to a page which had focus in the current session, but there was no media to re-focus");
                    return false;
                }

                Debug.WriteLine($"{loggedInUser.FullName} returned to a page which never had focus in the current session. Refresh is required... ({feedback})");
                return true;
            }

            Debug.WriteLine($"Refocusing media... ({feedback})");
            OnMediaCenterScreen("control_" + session[$"{bodyId}_CurrentUnfocusedViewMediaId"].ToString(), bodyId, false);
            session[$"{bodyId}_CurrentUnfocusedViewMediaId"] = null;
            session[$"{bodyId}_PageHasHadFocusInTheCurrentSession"] = true;
            return false;
        }

        [WebMethod(EnableSession = true)]
        public static void OnMenuOpenMediaNotReFocused() => Debug.WriteLine("Media was not re-focused becuase the menu is open");
    }
}
