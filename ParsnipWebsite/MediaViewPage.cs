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
            new LogEntry(Log.Access, session.SessionID) { Text = $"JS: {loggedInUser.FullName} {(tabClosed ? "closed a tab after scrolling" : "scrolled")} an{(isUntitled ? " untitled " : " ")}image ({mediaId}) for {duration} secs{(isUntitled ? string.Empty : $": {tempMedia.Title}")} (<a href=\"{baseUrl}view?id={mediaId}\">view?id={mediaId}</a>)" };
        }
    }
}
