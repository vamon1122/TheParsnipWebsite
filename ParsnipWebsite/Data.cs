using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using ParsnipData.Cookies;

namespace ParsnipWebsite
{
    public static class Data
    {
        public enum MediaTagIds
        {
            Photos = 1,
            Videos = 2,
            Memes = 3,
            Amsterdam = 4,
            Portugal = 5,
            Krakow = 6
        }
        public static bool IsMobile { get { return Convert.ToBoolean(Cookie.Read("isMobile")); } }

        public static string DeviceType
        {
            get
            {
                string deviceType = Cookie.Read("deviceType");
                switch (deviceType)
                {
                    case "Android":
                        deviceType += " device";
                        break;
                    case "webOS":
                        deviceType += " device";
                        break;
                    case "BlackBerry":
                        deviceType += " device";
                        break;
                    case "Windows":
                        deviceType += " device";
                        break;
                    case "MacOS":
                        deviceType += " device";
                        break;
                    case "UNIX":
                        deviceType += " device";
                        break;
                    case "Linux":
                        deviceType += " device";
                        break;
                    default:
                        break;
                }

                return deviceType;
            }
        }
        public static string DeviceLatitude { get { return Cookie.Read("deviceLatitude"); } }
        public static string DeviceLongitude { get { return Cookie.Read("deviceLongitude"); } }
        public static string SessionId { get { return Cookie.Read("sessionId"); } }
        public static void OnMediaUnFocused(string reason, string bodyId = null)
        {
            HttpContext.Current.Session["CurrentViewId"] = null;
            if (bodyId != null)
            {
                HttpContext.Current.Session[$"{bodyId}_CurrentUnfocusedViewMediaId"] = HttpContext.Current.Session[$"{bodyId}_CurrentViewMediaId"] ?? HttpContext.Current.Session[$"{bodyId}_CurrentUnfocusedViewMediaId"];
                HttpContext.Current.Session[$"{bodyId}_CurrentViewMediaId"] = null;
                HttpContext.Current.Session[$"{bodyId}_CurrentViewId"] = null;
            }
            Debug.WriteLine($"Media focus was cleared ({reason})");
        }
    }
}