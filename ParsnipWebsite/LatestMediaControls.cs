using ParsnipData.Media;
using ParsnipWebsite.Custom_Controls.Media;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using static ParsnipWebsite.Enums;

namespace ParsnipWebsite
{
    public class LatestMediaControls
    {
        public List<MediaControl> LastMinute { get; set; }
        public List<MediaControl> LastHour { get; set; }
        public List<MediaControl> Today { get; set; }
        public List<MediaControl> Yesterday { get; set; }
        public List<MediaControl> LastWeek { get; set; }
        public List<MediaControl> LastMonth { get; set; }
        public List<MediaControl> LastThreeMonths { get; set; }

        public LatestMediaControls()
        {
            LastMinute = new List<MediaControl>();
            LastHour = new List<MediaControl>();
            Today = new List<MediaControl>();
            Yesterday = new List<MediaControl>();
            LastWeek = new List<MediaControl>();
            LastMonth = new List<MediaControl>();
            LastThreeMonths = new List<MediaControl>();
        }

        public static LatestMediaControls GetLatestMediaAsLatestMediaControls(List<Media> latestMedia, string redirect, ParsnipPage httpHandler)
        {
            var mediaControls = new LatestMediaControls();
            //ParsnipPage httpHandler = (ParsnipPage)HttpContext.Current.Handler;
            int loggedInUserId = ParsnipData.Accounts.User.LogIn().Id;

            var lastMinute = latestMedia.Where(x => x.DateTimeCreated > ParsnipData.Parsnip.AdjustedTime.AddMinutes(-1));
            var lastHour = latestMedia.Where(x => x.DateTimeCreated < ParsnipData.Parsnip.AdjustedTime.AddMinutes(-1) && x.DateTimeCreated > ParsnipData.Parsnip.AdjustedTime.AddHours(-1));
            var today = latestMedia.Where(x => x.DateTimeCreated < ParsnipData.Parsnip.AdjustedTime.AddHours(-1) && x.DateTimeCreated > ParsnipData.Parsnip.AdjustedTime.AddDays(-1));
            var yesterday = latestMedia.Where(x => x.DateTimeCreated < ParsnipData.Parsnip.AdjustedTime.AddDays(-1) && x.DateTimeCreated > ParsnipData.Parsnip.AdjustedTime.AddDays(-2));
            var lastWeek = latestMedia.Where(x => x.DateTimeCreated < ParsnipData.Parsnip.AdjustedTime.AddDays(-2) && x.DateTimeCreated > ParsnipData.Parsnip.AdjustedTime.AddDays(-7));
            var lastMonth = latestMedia.Where(x => x.DateTimeCreated < ParsnipData.Parsnip.AdjustedTime.AddDays(-7) && x.DateTimeCreated > ParsnipData.Parsnip.AdjustedTime.AddMonths(-1));
            var lastThreeMonths = latestMedia.Where(x => x.DateTimeCreated < ParsnipData.Parsnip.AdjustedTime.AddMonths(-1) && x.DateTimeCreated > ParsnipData.Parsnip.AdjustedTime.AddMonths(-3));

            if (lastMinute.Count() > 0)
            {
                foreach (var media in lastMinute)
                {
                    MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                    //myMediaControl.ID = $"{httpHandler.PageId}_{media.Id}";
                    myMediaControl.PageId = httpHandler.PageId;
                    myMediaControl.Redirect = redirect;
                    myMediaControl.MyMedia = media;
                    mediaControls.LastMinute.Add(myMediaControl);
                }
            }

            if (lastHour.Count() > 0)
            {
                foreach (var media in lastHour)
                {
                    MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                    //myMediaControl.ID = $"{httpHandler.PageId}_{media.Id}";
                    myMediaControl.PageId = httpHandler.PageId;
                    myMediaControl.Redirect = redirect;
                    myMediaControl.MyMedia = media;
                    mediaControls.LastHour.Add(myMediaControl);
                }
            }

            if (today.Count() > 0)
            {
                foreach (var media in today)
                {
                    MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                    //myMediaControl.ID = $"{httpHandler.PageId}_{media.Id}";
                    myMediaControl.PageId = httpHandler.PageId;
                    myMediaControl.Redirect = redirect;
                    myMediaControl.MyMedia = media;
                    mediaControls.Today.Add(myMediaControl);
                }
            }

            if (yesterday.Count() > 0)
            {
                foreach (var media in yesterday)
                {
                    MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                    //myMediaControl.ID = $"{httpHandler.PageId}_{media.Id}";
                    myMediaControl.PageId = httpHandler.PageId;
                    myMediaControl.Redirect = redirect;
                    myMediaControl.MyMedia = media;
                    mediaControls.Yesterday.Add(myMediaControl);
                }
            }

            if (lastWeek.Count() > 0)
            {
                foreach (var media in lastWeek)
                {
                    MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                    //myMediaControl.ID = $"{httpHandler.PageId}_{media.Id}";
                    myMediaControl.PageId = httpHandler.PageId;
                    myMediaControl.Redirect = redirect;
                    myMediaControl.MyMedia = media;
                    mediaControls.LastWeek.Add(myMediaControl);
                }
            }

            if (lastMonth.Count() > 0)
            {
                foreach (var media in lastMonth)
                {
                    MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                    //myMediaControl.ID = $"{httpHandler.PageId}_{media.Id}";
                    myMediaControl.PageId = httpHandler.PageId;
                    myMediaControl.Redirect = redirect;
                    myMediaControl.MyMedia = media;
                    mediaControls.LastMonth.Add(myMediaControl);
                }
            }

            if (lastThreeMonths.Count() > 0)
            {
                foreach (var media in lastThreeMonths)
                {
                    MediaControl myMediaControl = (MediaControl)httpHandler.LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                    //myMediaControl.ID = $"{httpHandler.PageId}_{media.Id}";
                    myMediaControl.PageId = httpHandler.PageId;
                    myMediaControl.Redirect = redirect;
                    myMediaControl.MyMedia = media;
                    mediaControls.LastThreeMonths.Add(myMediaControl);
                }
            }

            return mediaControls;
        }
    }
}
