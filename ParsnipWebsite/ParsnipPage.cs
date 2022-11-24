using System.Diagnostics;
using System.Web.Services;

namespace ParsnipWebsite
{
    public class ParsnipPage : System.Web.UI.Page
    {
        [WebMethod]
        public static void OnMediaUnFocused(string feedback, string bodyId) => Debug.WriteLine($"{feedback}, however this page does not contain media. Not doing un-focus");

        [WebMethod]
        public static bool OnMediaReFocused(string bodyId, string feedback)
        {
            Debug.WriteLine($"{feedback}, however this page does not contain media. Not doing re-focus");
            return false;
        }
    }
}
