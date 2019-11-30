using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ParsnipData;
using ParsnipData.Accounts;
using System.Data;
using ParsnipData.Media;

namespace ParsnipWebsite
{
    public partial class Share_Stats : System.Web.UI.Page
    {
        User myUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage("share_stats", this, Data.DeviceType, "admin");
        }

        void Page_LoadComplete(object sender, EventArgs e)
        {
            DataTable allStats = MediaShare.GetStats();

            foreach(DataRow row in allStats.Rows)
            {
                string mediaRedirect;
                string shareRedirect;
                switch (row[6].ToString().Trim())
                {
                    case "image":
                        mediaRedirect = "view?id=";
                        shareRedirect = "view?share=";
                        break;
                    default:
                        mediaRedirect = "view?id=";
                        shareRedirect = "view?share=";
                        break;
                }
                var myRow = new TableRow();

                var titleCell = new TableCell();
                titleCell.Controls.Add(new LiteralControl(
                    string.Format("<a href={0}>{1}</a>", mediaRedirect + row[0].ToString(), row[1].ToString())));

                myRow.Cells.Add(titleCell);

                var userCell = new TableCell();
                userCell.Controls.Add(new LiteralControl(
                    string.Format("<a href={0}>{1}</a>", "manage_users?id=" + row[7].ToString(), row[3].ToString())));
                myRow.Cells.Add(userCell);

                var timesUsedCell = new TableCell();
                timesUsedCell.Controls.Add(new LiteralControl(
                    string.Format("<a href={0}>{1}</a>", shareRedirect + row[5].ToString(), row[4].ToString())));
                myRow.Cells.Add(timesUsedCell);


                Table_Stats.Rows.Add(myRow);
            }
            

        }
    }
}