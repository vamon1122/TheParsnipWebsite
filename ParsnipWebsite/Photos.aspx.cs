﻿using System;
using ParsnipData.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData.Logs;
using ParsnipData.Media;
using System.Web.UI.HtmlControls;
using ParsnipData;
using System.Data.SqlClient;
using System.Diagnostics;
using ParsnipWebsite.Custom_Controls.Media;

namespace ParsnipWebsite
{
    public partial class Photos : System.Web.UI.Page
    {
        private User myUser;
        static readonly Log DebugLog = new Log("debug");
        static readonly Album PhotosAlbum = new Album(new Guid("4b4e450a-2311-4400-ab66-9f7546f44f4e"));

        public Photos()
        {
            PhotosAlbum.Select();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["imageid"] == null)
                myUser = Account.SecurePage("photos", this, Data.DeviceType, "member");
            else
                myUser = Account.SecurePage("photos?imageid=" + Request.QueryString["imageid"], this, Data.DeviceType, "member");

            if (IsPostBack && PhotoUpload.PostedFile != null)
            {
                MediaManager.UploadImage(myUser, PhotosAlbum, PhotoUpload);
            }

            if (myUser.AccountType == "admin" || myUser.AccountType == "member")
            {
                UploadDiv.Style.Clear();
            }
            Debug.WriteLine("Getting all photos");
            List<ParsnipData.Media.Image> AllPhotos = PhotosAlbum.GetAllImages();
            Debug.WriteLine("Got all photos");
            //new LogEntry(Debug) { text = "Got all photos. There were {0} photo(s) = " + AllPhotos.Count() };
            foreach (ParsnipData.Media.Image temp in AllPhotos)
            {
                var MyImageControl = (MediaControl)LoadControl("~/Custom_Controls/Media/MediaControl.ascx");
                MyImageControl.MyImage = temp;
                DynamicPhotosDiv.Controls.Add(MyImageControl);
            }
        }
    }
}