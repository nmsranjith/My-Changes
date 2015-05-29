
using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;
using System;
namespace DotNetNuke.Modules.SampleRequestForm.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SRFParameters" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Sample Request Form Parameters
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------    
    public class SRFParameters : ContentItem
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ContactType { get; set; }
        public string ContactValue { get; set; }
        public string Mobile { get; set; }
        public string Work { get; set; }
        public string Contact_Email { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string SecurityQuestion { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string SSOAuthKey { get; set; }
        public string Token { get; set; }
        public string[] ISBNS { get; set; }
        public string CoreIsbn { get; set; }
        public string SecondaryEmail { get; set; }
        public string BookInUse { get; set; }
        public string Enrolment { get; set; }
        public string HowYouKnow { get; set; }
        public string Institution { get; set; }
        public string Semester { get; set; }
        public string State { get; set; }
        public string StateText { get; set; }
        public string SubUrb { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }
        public char IsAddressExist { get; set; }

        public string Message { get; set; }
        public string ForTeaching { get; set; }
        public string AlreadyUsing { get; set; }
        public string NeedHelp { get; set; }

        public int TradingPartenerAccountSk { get; set; }
        public int StoreSk { get; set; }
        public int UserSk { get; set; }
        public decimal Price { get; set; }
        public string TypeOfPack { get; set; }
        public string UserAgent { get; set; }
        public string Currency { get; set; }
        public int AddressDetailSk { get; set; }

        public string ContactID { get; set; }

        #region Overridden Fill Method
        /// <summary>
        /// CBO Fill Method
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            try
            {
                UserSk = Null.SetNullInteger(dr["INSTRUCTOR_SK"]);
                Name = Null.SetNullString(dr["INSTRUCTOR_FIRST_NAME"]) +" "+ Null.SetNullString(dr["INSTRUCTOR_LAST_NAME"]);
                Email = Null.SetNullString(dr["INSTRUCTOR_EMAIL"]);
                SecondaryEmail = Null.SetNullString(dr["STUDENT_EMAIL"]);
                Contact_Email = Null.SetNullString(dr["CONTACT_EMAIL"]);
                Mobile = Null.SetNullString(dr["MOBILE"]);
                Work = Null.SetNullString(dr["WORK"]);
                BookInUse = Null.SetNullString(dr["BOOKS_IN_USE"]);
                CourseCode = Null.SetNullString(dr["COURSE_ID"]);
                CourseName = Null.SetNullString(dr["COURSE_NAME"]);
                Enrolment = Null.SetNullString(dr["ENROLEMENT"]);
                //HowYouKnow=Null.SetNullString(dr["HOW_KNOWN_ABOUT_SITE"]);
                Institution = Null.SetNullString(dr["INSTITUTION"]);
                Semester = Null.SetNullString(dr["SEMESTER"]);
                State = Null.SetNullString(dr["STATE"]);
                SubUrb = Null.SetNullString(dr["SUBURB"]);
                Address1 = Null.SetNullString(dr["ADDRESS_LINE1"]);
                Address2 = Null.SetNullString(dr["ADDRESS_LINE2"]);
                Address3 = Null.SetNullString(dr["ADDRESS_LINE3"]);
                Country = Null.SetNullString(dr["COUNTRY_CODE"]);
                PostalCode = Null.SetNullInteger(dr["POSTAL_CODE"]);
                TradingPartenerAccountSk = Null.SetNullInteger(dr["TRADING_PARTNER_ACCOUNT_SK"]);
                StoreSk = Null.SetNullInteger(dr["STORE_SK"]);
            }
            catch(Exception ex)
            {
                SampleRequestFormModuleBase.LogFileWrite(ex);
            }
        }
        #endregion
    }
    
    
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StateObjClass" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Thread Timer Object
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------    
    
    public class StateObjClass
    {
        // Used to hold parameters for calls to TimerTask. 
        public int SomeValue;
        public System.Threading.Timer TimerReference;
        public bool TimerCanceled;
    }
}