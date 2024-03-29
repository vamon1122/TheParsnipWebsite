﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Accounts;

namespace ParsnipWebsite
{
    public partial class Bios : ParsnipPage
    {
        private User myUser;
        protected void Page_Load(object sender, EventArgs e)
        {
            myUser = Account.SecurePage(this, Data.DeviceType);
            NewMenu.LoggedInUser = myUser;
            NewMenu.SelectedPage = PageIndex.Bios;
        }
    }
}