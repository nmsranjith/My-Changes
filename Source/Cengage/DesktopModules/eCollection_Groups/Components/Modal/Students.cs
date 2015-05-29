using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;
using System.Globalization;

namespace DotNetNuke.Modules.eCollection_Groups.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Students" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Students
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Students : ContentItem, IComparable<Students>, IEquatable<Students>
    {
        private int UserId;
        private string Name;
        private int _currentReadLevel;
        private string _studentLoginName;
        private int _readingLevel;
        private string _readingPercentage;
        private DateTime _dateCreated;
        private bool _checked;
        public string StudentNames
        {
            get { return Name; }
            set { Name = value; }
        }
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        public int UserID
        {

            get { return UserId; }
            set { UserId = value; }
        }

        public int CurrentReadLevel
        {
            get { return _currentReadLevel; }
            set { _currentReadLevel = value; }
        }
        public string StudentLoginName
        {
            get { return _studentLoginName; }
            set { _studentLoginName = value; }
        }
        public int ReadingLevel
        {
            get { return _readingLevel; }
            set { _readingLevel = value; }
        }
        public string ReadingPercentage
        {
            get { return _readingPercentage; }
            set { _readingPercentage = value; }
        }
        public DateTime CreatedDate
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }
        public int custSubUserSK
        {
            get;
            set;
        }

        public Students() { }
        public Students(int id, string text) { this.UserId = id; this.Name = text; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            GroupController groupController = GroupController.Instance;
            UserID = Null.SetNullInteger(int.Parse(dr["CustomerSubsUserSk"].ToString()));
            StudentNames = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(Null.SetNullString(dr["FirstName"].ToString()), " ", Null.SetNullString(dr["LastName"].ToString())).ToLower());
            CurrentReadLevel = Null.SetNullInteger(int.Parse(dr["CurrentReadingLevel"].ToString()));
            StudentLoginName = Null.SetNullString(dr["UserLoginName"].ToString()).ToLower();
            Checked = false;
        }

        public static IComparer<Students> StudentReadingLevelSorter
        {
            get { return new StudentReadingLevelComparer(); }
        }

        // --------------------------------------------------------------------------------------------------------------------
        // <copyright file="StudentReadingLevelComparer" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
        //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
        // </copyright>
        // <summary>
        //    Comparer class for Student Reading Level
        // </summary>
        // ---------------------------------------------------------------------------------------------------------------------
        private class StudentReadingLevelComparer : IComparer<Students>
        {
            #region IComparer<Groups> Members
            /// <summary>
            /// 
            /// </summary>
            /// <param name="first"></param>
            /// <param name="second"></param>
            /// <returns></returns>
            public int Compare(Students first, Students second)
            {
                return first.CurrentReadLevel.CompareTo(second.CurrentReadLevel);
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Students other)
        {
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Students other)
        {
            if (this.UserId == other.UserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}