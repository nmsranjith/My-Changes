using System;
using DotNetNuke.Entities.Content;
using System.Collections.Generic;
using System.Xml;
using DotNetNuke.Common.Utilities;
using System.Globalization;

namespace DotNetNuke.Modules.eCollection_Students.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Student" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Students
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Student : ContentItem
    {

        #region Property Methods
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserLoginName { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public char? Gender { get; set; }
        public string Email { get; set; }
        public string UserDomain { get; set; }
        public char InternalUse { get; set; }
        public Nullable<DateTime> DateofBirth { get; set; }
        public DateTime AddedDate { get; set; }
        public int? AddedBy { get; set; }
        public string Grade { get; set; }
        public int StartingReadinglevel { get; set; }
        public int? CurrentReadingLevel { get; set; }
        public char? ESL { get; set; }
        public char? ReadingRecovery { get; set; }
        public int StartingReadinglevelFrom { get; set; }
        public int StartingReadinglevelUpto { get; set; }
        public int CurrentReadinglevelFrom { get; set; }
        public int CurrentReadinglevelUpto { get; set; }
        public char Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserCreated { get; set; }
        public Nullable<DateTime> DateModified { get; set; }
        public string UserModified { get; set; }
        public char PurchaseFlag { get; set; }
        public char IsDefaultPartner { get; set; }
        public string Description { get; set; }
        public string TeacherLoginName { get; set; }
        public int SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public int LicenseQty { get; set; }
        public string UpdateFlag { get; set; }
        public int BooksOpened { get; set; }
        public int IndependentPercentage { get; set; }
        public int GuidedPercentage { get; set; }
        public int MyWordsCount { get; set; }
        public int RecordingsCount { get; set; }
        public int ClassId { get; set; }
        public XmlDocument StudentsDoc { get; set; }
        public string ActionType { get; set; }
        public string FullNameFlag { get; set; }
        public int CustSubUserSk { get; set; }
        public string ProcName { get; set; }
        public char GrpType { get; set; }
        public string GrpCacheName { get; set; }
        public int PercentRead { get; set; }
        public List<Student> SelectedStudentsList { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string BookOpenedDate { get; set; }
        public string BookOpenedTime { get; set; }
        public string PageName { get; set; }
        public string RecPath { get; set; }
        public DateTime OpenedDate { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public string Word { get; set; }
        public string WordCount { get; set; }
        public int ProductId { get; set; }
        public string BookID { get; set; }
        public int MonthId { get; set; }
        public string profilelink { get; set; }
        public string SessionType { get; set; }
        public int NewSubsSk { get; set; }
        public string SearchText { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        #endregion        
 
        #region Overridden Fill Method
            /// <summary>
            /// CBO Fill Method
            /// </summary>
            /// <param name="dr"></param>
            public override  void Fill(System.Data.IDataReader dr)
            {
                StudentId = Null.SetNullInteger(dr["StudentId"].ToString());
                FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(dr["FirstName"]));
                LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(dr["LastName"]));
                DisplayName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(dr["DisplayName"]));
                FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(dr["FullName"]));
                UserLoginName = dr["UserLoginName"].ToString();
                CurrentReadingLevel = int.Parse(dr["CurrentReadingLevel"].ToString());
                CustSubUserSk = int.Parse(dr["CustomerSubsUserSk"].ToString());
                SubscriptionId = int.Parse(dr["SubsSK"].ToString());
                CreatedDate = DateTime.Parse(dr["CreatedDate"].ToString());
                ClassId = Null.SetNullInteger(dr["ClassId"]);
                profilelink = eCollection_StudentsModuleBase.ProfileUrl + UserLoginName;
            }
        #endregion           
      

    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Helper" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Helper
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public static class Helper
    {
        /// <summary>
        ///  Helper class method to sort Linq items
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        public static void Sort<TSource, TValue>(this List<TSource> source,
          Func<TSource, TValue> selector)
        {
            var comparer = Comparer<TValue>.Default;
            source.Sort((x, y) => comparer.Compare(selector(x), selector(y)));
        }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="UnallocatedStudents" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for UnallocatedStudents
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------    
    public class UnallocatedStudent
    {
         public int CUST_SUBS_ALLOC_SK { get; set; }
         public int USER_SK { get; set; }
         public string USER_LOGIN_NAME { get; set; }
         public string EMAIL { get; set; }
         public string ALLOC_STATUS { get; set; }
         public int RN { get; set; }        					
    }
}