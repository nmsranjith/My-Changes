using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SessionMembers" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for SessionMembers
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class SessionMembers :IComparable<SessionMembers>,IEquatable<SessionMembers>
    {
        public SessionMembers() { }
        // Session members
        public int SESSION_MEMBER_SK { get; set; }

        public int? CUST_SUBS_USER_SK { get; set; }

        public int? GRP_MEM_SK { get; set; }
        
        public int? ADDED_BY_CUST_SUBS_USER_SK { get; set; }

        public string MemberType { get; set; }

        public new Nullable<DateTime> Added_date { get; set; }

        private string _groupName = string.Empty;
        public string GroupName { get { return _groupName; } set { _groupName = value;} }

        private string _studentName = string.Empty;
        public string StudentName { get { return _studentName; } set { _studentName=value;} }

        private string _teacherName = string.Empty;
        public string TeacherName { get { return _teacherName; } set { _teacherName = value; } }

        public string Active { get; set; }

        public int CompareTo(SessionMembers other)
        {
            return _groupName.CompareTo(other.GroupName);
        }

        public static IComparer<SessionMembers> StudentNameSorter
        {
            get { return new StudentNameSorterComparer(); }
        }

        // --------------------------------------------------------------------------------------------------------------------
        // <copyright file="StudentNameSorterComparer" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
        //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
        // </copyright>
        // <summary>
        //    Comparer for Student Name Sorter
        // </summary>
        // ---------------------------------------------------------------------------------------------------------------------
        private class StudentNameSorterComparer : IComparer<SessionMembers>
        {
            #region IComparer<SessionMembers> Members
            /// <summary>
            /// 
            /// </summary>
            /// <param name="first"></param>
            /// <param name="second"></param>
            /// <returns></returns>
            public int Compare(SessionMembers first, SessionMembers second)
            {
                return first.StudentName.CompareTo(second.StudentName);
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SessionMembers other)
        {
            if (this.GRP_MEM_SK == other.GRP_MEM_SK)
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