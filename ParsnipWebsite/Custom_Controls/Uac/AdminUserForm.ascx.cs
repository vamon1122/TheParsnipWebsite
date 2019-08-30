﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParsnipData;
using System.Diagnostics;
using ParsnipData.Accounts;

namespace ParsnipWebsite.Custom_Controls.Uac
{
    internal static class PersistentData
    {
        internal static AdminUserForm myUserForm1;
        internal static User _dataSubject;
        internal static User DataSubject { get { return _dataSubject; } set { /*Debug.WriteLine(string.Format("dataSubject (id = \"{0}\") was set in UserForm", value.Id));*/ _dataSubject = value; myUserForm1.UpdateFields(); } }
    }


    public partial class AdminUserForm : System.Web.UI.UserControl
    {
        public User DataSubject { get { return PersistentData.DataSubject; } }

        public void UpdateDataSubject(Guid pId)
        {
            Debug.WriteLine("Searching for Id " + pId);
            User mySubject = new User(pId);
            mySubject.Select();
            Debug.WriteLine("Success? " + mySubject.FullName);
            PersistentData.DataSubject = mySubject;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public AdminUserForm()
        {
            PersistentData.myUserForm1 = this;
            if (PersistentData._dataSubject == null)
            {
                Debug.WriteLine("----------UserForm1 was initialised without a dataSubject");

                PersistentData._dataSubject = new User(Guid.Empty);
            }
            else
            {
                //Debug.WriteLine("----------_dataSubject was already initialised");
            }
        }

        public void UpdateDateCreated()
        {
            dateTimeCreated.Attributes.Add("placeholder", Parsnip.AdjustedTime.ToString("dd/MM/yyyy"));
        }

        public void UpdateFields()
        {
            Debug.WriteLine(string.Format("----------Userform fields are being updated. Name: {0} Id: {1}", PersistentData.DataSubject.FullName, PersistentData.DataSubject.Id));

            Debug.WriteLine(string.Format("----------{0} != {1}", PersistentData.DataSubject.Id.ToString(), Guid.Empty.ToString()));



            //Debug.WriteLine("----------UpdateForm()");
            //Debug.WriteLine("----------username = " + username.Text);
            //Debug.WriteLine("----------dataSubject.username = " + dataSubject.username);
            //Debug.WriteLine("----------dataSubject.id = " + dataSubject.id);

            TextBox_LastLoggedIn.Text = PersistentData.DataSubject.LastLogIn == DateTime.MinValue ? "Never" : PersistentData.DataSubject.LastLogIn.ToString();
            
            

            username.Text = PersistentData.DataSubject.Username;

            email.Text = PersistentData.DataSubject.Email;

            password.Text = PersistentData.DataSubject.Password;

            forename.Text = PersistentData.DataSubject.Forename;

            surname.Text = PersistentData.DataSubject.Surname;

            if (string.IsNullOrEmpty(PersistentData.DataSubject.GenderUpper))
                gender.Value = "Male";
            else
                gender.Value = PersistentData.DataSubject.GenderUpper;

            if (PersistentData.DataSubject.Dob.ToString("dd/MM/yyyy") != "01/01/0001")
                dobInput.Value = PersistentData.DataSubject.Dob.ToString("dd/MM/yyyy");
            else
                dobInput.Value = "";

            address1.Text = PersistentData.DataSubject.Address1;

            address2.Text = PersistentData.DataSubject.Address2;

            address3.Text = PersistentData.DataSubject.Address3;

            postCode.Text = PersistentData.DataSubject.PostCode;

            mobilePhone.Text = PersistentData.DataSubject.MobilePhone;

            homePhone.Text = PersistentData.DataSubject.HomePhone;

            workPhone.Text = PersistentData.DataSubject.WorkPhone;

            if (PersistentData.DataSubject.DateTimeCreated != null)
            {
                dateTimeCreated.Attributes.Remove("placeholder");
                dateTimeCreated.Attributes.Add("placeholder", PersistentData.DataSubject.DateTimeCreated.Date.ToString("dd/MM/yyyy"));
            }

            if (string.IsNullOrEmpty(PersistentData.DataSubject.AccountType))
                accountType.Value = "user";
            else
                accountType.Value = PersistentData.DataSubject.AccountType;

            if (string.IsNullOrEmpty(PersistentData.DataSubject.AccountType))
                accountStatus.Value = "active";
            else
                accountStatus.Value = PersistentData.DataSubject.AccountStatus;

            if (PersistentData.DataSubject.DateTimeCreated.ToString("dd/MM/yyyy") == "01/01/0001")
            {
                //Debug.WriteLine(string.Format("{0}'s datetimecreated {1} == 01/01/0001", dataSubject.fullName, dataSubject.dateTimeCreated.ToString("dd/MM/yyyy")));
                dateTimeCreated.Value = Parsnip.AdjustedTime.ToString("dd/MM/yyyy");
            }
            else
            {
                //Debug.WriteLine(string.Format("{0}'s dob {1} != 01/01/0001",dataSubject.fullName, dataSubject.dateTimeCreated.ToString("dd/MM/yyyy")));
                dateTimeCreated.Value = PersistentData.DataSubject.DateTimeCreated.ToString("dd/MM/yyyy");
            }

        }

        public void UpdateDataSubject()
        {
            if (PersistentData.DataSubject == null)
            {
                Debug.WriteLine("My dataSubject is null. Adding new dataSubject");
                PersistentData.DataSubject = new User("UpdateDataSubject (UserForm1)");
            }
            /*
            Debug.WriteLine(string.Format("username.Text = {0}", username.Text));
            Debug.WriteLine(string.Format("forename.Text = {0}", forename.Text));
            Debug.WriteLine(string.Format("mobilePhone.Text = {0}", mobilePhone.Text));
            Debug.WriteLine(string.Format("homePhone.Text = {0}", homePhone.Text));
            Debug.WriteLine(string.Format("workPhone.Text = {0}", workPhone.Text));
            */
            PersistentData.DataSubject.Username = username.Text;
            //Debug.WriteLine(string.Format("dataSubject.Username = username.Text ({0})", username.Text));

            PersistentData.DataSubject.Email = email.Text;
            PersistentData.DataSubject.Password = password.Text;
            PersistentData.DataSubject.Forename = forename.Text;
            PersistentData.DataSubject.Surname = surname.Text;
            PersistentData.DataSubject.GenderUpper = gender.Value.Substring(0, 1);
            //Debug.WriteLine("DOB = " + dobInput.Value);


            if (DateTime.TryParse(dobInput.Value, out DateTime result))
                PersistentData.DataSubject.Dob = Convert.ToDateTime(dobInput.Value);

            PersistentData.DataSubject.Address1 = address1.Text;
            PersistentData.DataSubject.Address2 = address2.Text;
            PersistentData.DataSubject.Address3 = address3.Text;
            PersistentData.DataSubject.PostCode = postCode.Text;
            PersistentData.DataSubject.MobilePhone = mobilePhone.Text;
            PersistentData.DataSubject.HomePhone = homePhone.Text;
            PersistentData.DataSubject.WorkPhone = workPhone.Text;
            PersistentData.DataSubject.DateTimeCreated = Parsnip.AdjustedTime;
            PersistentData.DataSubject.AccountType = accountType.Value;
            PersistentData.DataSubject.AccountStatus = accountStatus.Value;
            PersistentData.DataSubject.AccountType = accountType.Value;
        }
    }
    
}