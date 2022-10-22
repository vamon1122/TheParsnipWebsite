using ParsnipData.Accounts;
using ParsnipData.Media;
using ParsnipWebsite.Custom_Controls.Media;
using System;
using System.Linq;

namespace ParsnipWebsite
{
    public partial class Latest : MediaViewPage
    {
        private User myUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage(this, Data.DeviceType);

            NewMenu.SelectedPage = PageIndex.Latest;
            NewMenu.LoggedInUser = myUser;
            NewMenu.Upload = true;

            UploadMediaControl.Initialise(myUser, this);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            var controls = LatestMediaControls.GetLatestMediaAsLatestMediaControls(Media.SelectLatestMedia(myUser.Id), "latest");
            if (controls.LastMinute.Count() > 0)
            {
                NoMediaContainer.Visible = false;
                LastMinuteAccordion.Visible = true;
                LastMinuteAccordion.Media = controls.LastMinute;
                LastMinuteAccordion.Text = "Last minute";
            }
            if (controls.LastHour.Count() > 0)
            {
                NoMediaContainer.Visible = false;
                LastHourAccordion.Visible = true;
                LastHourAccordion.Media = controls.LastHour;
                LastHourAccordion.Text = "Last hour";
            }
            if (controls.Today.Count() > 0)
            {
                NoMediaContainer.Visible = false;
                TodayAccordion.Visible = true;
                TodayAccordion.Media = controls.Today;
                TodayAccordion.Text = "Last 24 hours";
            }
            if (controls.Yesterday.Count() > 0)
            {
                NoMediaContainer.Visible = false;
                YesterdayAccordion.Visible = true;
                YesterdayAccordion.Media = controls.Yesterday;
                YesterdayAccordion.Text = "Last 48 Hours";
            }
            if (controls.LastWeek.Count() > 0)
            {
                NoMediaContainer.Visible = false;
                LastWeekAccordion.Visible = true;
                LastWeekAccordion.Media = controls.LastWeek;
                LastWeekAccordion.Text = "Last 7 days";
            }
            if (controls.LastMonth.Count() > 0)
            {
                NoMediaContainer.Visible = false;
                LastMonthAccordion.Visible = true;
                LastMonthAccordion.Media = controls.LastMonth;
                LastMonthAccordion.Text = "Last month";
            }
            if (controls.LastThreeMonths.Count() > 0)
            {
                NoMediaContainer.Visible = false;
                LastThreeMonthsAccordion.Visible = true;
                LastThreeMonthsAccordion.Media = controls.LastThreeMonths;
                LastThreeMonthsAccordion.Text = "Last three months";
            }
        }
    }
}
