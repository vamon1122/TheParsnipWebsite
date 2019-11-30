using System;
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
        internal static User DataSubject { get { return _dataSubject; } set { /*Debug.WriteLine(string.Format("dataSubject (id = \"{0}\") was set in UserForm", value.Id));*/ _dataSubject = value; if (value != null) { myUserForm1.UpdateFields(); } } }
    }


    public partial class AdminUserForm : System.Web.UI.UserControl
    {
        public User DataSubject { get { return PersistentData.DataSubject; } }

        public void UpdateDataSubject(int pId)
        {
            User mySubject = User.Select(pId);
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
                PersistentData._dataSubject = new User();
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
                dateTimeCreated.Value = Parsnip.AdjustedTime.ToString("dd/MM/yyyy");
            else
                dateTimeCreated.Value = PersistentData.DataSubject.DateTimeCreated.ToString("dd/MM/yyyy");
        }

        public void UpdateDataSubject()
        {
            if (PersistentData.DataSubject == null)
            {
                Debug.WriteLine("My dataSubject is null. Adding new dataSubject");
                PersistentData.DataSubject = new User("UpdateDataSubject (UserForm1)");
            }
            PersistentData.DataSubject.Username = username.Text;
            PersistentData.DataSubject.Email = email.Text;
            PersistentData.DataSubject.Password = password.Text;
            PersistentData.DataSubject.Forename = forename.Text;
            PersistentData.DataSubject.Surname = surname.Text;
            PersistentData.DataSubject.GenderUpper = gender.Value.Substring(0, 1);
            
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