using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;
using ParsnipData.Logging;

namespace ParsnipWebsite
{
    public partial class Logs : System.Web.UI.Page
    {
        User myUser;
        int selectedLogId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("logs?id=0");

            myUser = Account.SecurePage("logs", this, Data.DeviceType, "admin");

            selectedLogId = Convert.ToInt16(Request.QueryString["id"]);

            List<LogEntry> LogEntries;

            if (selectedLogId == default)
                LogEntries = ParsnipData.Logging.Data.GetAllLogEntries().OrderByDescending(x => x.date).ToList();
            else
            {
                Log temp = Log.Select((Log.Ids)selectedLogId);
                LogEntries = temp.GetLogEntries().OrderByDescending(x => x.date).ToList();
            }

            foreach (LogEntry myEntry in LogEntries)
            {
                TableRow MyRow = new TableRow();
                MyRow.Attributes.Add("style", "word-wrap:break-word");
                MyRow.Cells.Add(new TableCell() { Text = myEntry.date.ToString(), CssClass = "date-cell" } );
                MyRow.Cells.Add(new TableCell() { Text = myEntry.text });
                LogTable.Rows.Add(MyRow);
            }

            EntryCount.Text = string.Format("{0} entries found", LogEntries.Count());
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("----------Page load complete!");

            if (Request.QueryString["action"] != null)
            {
                string action = Request.QueryString["action"];
                if (Request.QueryString["success"] != null)
                {
                    string success = Request.QueryString["success"];

                    if (success == "true")
                    {
                        SuccessText.Text = string.Format("<strong>Success</strong> All logs were successfully {0}d on the database!", action);
                        Success.Attributes.CssStyle.Add("display", "block");
                    }

                }

            }

            UpdateLogList();
            SelectLog.SelectedValue = selectedLogId.ToString();

            System.Diagnostics.Debug.WriteLine("Page_LoadComplete complete!");
        }

        protected void SelectLog_Changed(object sender, EventArgs e)
        {
            Response.Redirect("logs?id=" + SelectLog.SelectedValue);
        }

        protected void btnClearLogsConfirm_Click(object sender, EventArgs e)
        {
            ParsnipData.Logging.Data.ClearLogs();

            new LogEntry(Log.Default) { text = string.Format("Logs were cleared by {0}!", myUser.FullName) };

            Response.Redirect("logs?id=0&action=delete&success=true");
        }

        void UpdateLogList()
        {
            var logs = new List<Log>();
            logs.AddRange(Log.GetAllLogs());

            ListItem[] ListItems = new ListItem[logs.Count + 1];

            ListItems[0] = new ListItem("all", "0");

            int i = 1;
            foreach (Log temp in logs)
            {
                ListItems[i] = new ListItem(temp.Name, temp.Id.ToString());
                i++;
            }
            SelectLog.Items.Clear();
            SelectLog.Items.AddRange(ListItems);

            SelectLog.SelectedValue = selectedLogId.ToString();
        }
    }
}