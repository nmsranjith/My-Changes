using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using System.Globalization;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Student" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Student
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Student : ContentItem, IComparable<Student>
    {
        public int CUST_SUBS_SK { get; set; }

        private string _studentName = string.Empty;
        //For checkbox
        private bool _checked;
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        public string StudentName { get{return _studentName;} set {_studentName = value;} }

        public int CurrentReadingLevel { get; set; }

        public string StudentLoginName { get; set; }

        #region Overridden Fill Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            CUST_SUBS_SK = int.Parse(dr["CUST_SUBS_USER_SK"].ToString());
            StudentName =  CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["USER_LOGIN_NAME"].ToString().ToLower());
            CurrentReadingLevel = Convert.ToInt32(dr["CURRENT_READING_LEVEL"]);
            StudentLoginName = dr["USERNAME"].ToString();
            Checked = false;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Student other)
        {
            return _studentName.CompareTo(other.StudentName);
        }

        public static IComparer<Student> SessionReadingLevelSorter
        {
            get { return new SessionReadingLevelComparer(); }
        }

        // --------------------------------------------------------------------------------------------------------------------
        // <copyright file="SessionReadingLevelComparer" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
        //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
        // </copyright>
        // <summary>
        //    Comparer for Session Reading Level
        // </summary>
        // ---------------------------------------------------------------------------------------------------------------------
        private class SessionReadingLevelComparer : IComparer<Student>
        {
            #region IComparer<Student> Members
            /// <summary>
            /// 
            /// </summary>
            /// <param name="first"></param>
            /// <param name="second"></param>
            /// <returns></returns>
            public int Compare(Student first, Student second)
            {
                return first.CurrentReadingLevel.CompareTo(second.CurrentReadingLevel);
            }
            #endregion
        }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Teacher" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //     DAO class for Teacher
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Teacher : ContentItem
    {
        public int TeacherId {get;set;}
        public string TeacherName {get;set;}

        #region Overridden Fill Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            TeacherId = int.Parse(dr["ID"].ToString());
            TeacherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["Value"].ToString().ToLower());           
        }
        #endregion
    }
        
}
