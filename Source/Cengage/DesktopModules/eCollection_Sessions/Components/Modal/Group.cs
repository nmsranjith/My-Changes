using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using System.Text;
using DotNetNuke.Entities.Modules;
using System.Globalization;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Group" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Group
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Group : ContentItem, IComparable<Group>, IHydratable
    {
        private int _membersCount;
        public int MemberCount 
        { 
            get 
            { 
                return _membersCount; 
            } 
            set 
            { 
                _membersCount = value; 
            } 
        }
        private string _name;
        public string Name 
        { 
            get 
            { 
                return _name; 
            } 
            set 
            { 
                _name = value; 
            } 
        }
        public int GroupId { get; set; }
        private string _teacherName;
        public string TeacherName
        {
            get
            {
                return _teacherName;
            }
            set
            {
                _teacherName = value;
            }
        }
        private int _readingLevel;
        public int ReadingLevel
        {
            get
            {
                return _readingLevel;
            }
            set
            {
                _readingLevel = value;
            }
        }
        private string _StudentName;
        public string StudentNames
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }

        private int _UserId;
        public int UserID
        {

            get { return _UserId; }
            set { _UserId = value; }
        }
        private DateTime _recordStartTime;
        public DateTime RecordStartTime { get { return _recordStartTime; } set { _recordStartTime = value; } }

        private string _circledWord;
        public string CircledWord { get { return _circledWord; } set { _circledWord = value; } }
        private string _title;       
        public string Title { get { return _title; } set { _title = value; } }
        private string _ISBN;        
        public string ISBN { get { return _ISBN; } set { _ISBN = value; } }
        private string _bookImgName;        
        public string BookImgName { get { return _bookImgName; } set { _bookImgName = value; } }
        private string _sessionType;
        public string SessionType { get { return _sessionType; } set { _sessionType = value; } }
        private DateTime _sessionCreatedDate;        
        public DateTime SessionCreatedDate { get { return _sessionCreatedDate; } set { _sessionCreatedDate = value; } }
        private DateTime _bookOpenAt;
        public DateTime BookOpenAt { get { return _bookOpenAt; } set { _bookOpenAt = value; } }
        private string _booksOpenedMin;
        
        public string BooksOpenedMin { get { return _booksOpenedMin; } set { _booksOpenedMin = value; } }
        public char GroupType { get { return GROUP_TYPE; } set { GROUP_TYPE = value; } }
        //For checkbox
        private bool _checked;
        private char GROUP_TYPE;
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        public string NameToolTip
        {
            get;
            set;
        }

        public string ListType
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {                    
            MemberCount = Null.SetNullInteger(dr["MembersCount"]);
            NameToolTip = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(dr["Name"]) != "Z%-11-Z%" ? Null.SetNullString(dr["Name"]).ToLower() : "All Other Students");
            Name = NameToolTip.Length > 27 ? string.Concat(NameToolTip.Substring(0, 25), " ..") : NameToolTip;
            GroupId = Null.SetNullInteger(dr["GroupId"]);
            TeacherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(ConcatTeacherName(SessionController.Instance.GetTeacherName(GroupId))));
            ReadingLevel = Null.SetNullInteger(dr["ReadingLevel"]);
            ListType = Null.SetNullString(dr["ListType"]);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Group other)
        {
            return _name.CompareTo(other.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        private void FillReadingLevel(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    ReadingLevel = list[i];
                }
                else
                {
                    MinReadLevel = list[i];
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private string ConcatTeacherName(List<string> list)
        {
            StringBuilder teachername = new StringBuilder();
            try
            {

                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list.Count - 1 == 0)
                        {
                            teachername.Append(list[i].ToString());
                            return teachername.ToString();
                        }

                        if (i != (list.Count - 1))
                        {
                            teachername.Append(list[i].ToString());
                            teachername.Append(", ");
                        }
                        if (i == (list.Count - 1))
                        {
                            teachername.Remove(teachername.Length - 2, 1);
                            if (list.Count >= 3)
                            {
                                teachername.Append(" ...");
                            }
                            else
                            {
                                teachername.Append(" and ");
                                teachername.Append(list[i].ToString());
                            }
                        }
                    }
                    return teachername.ToString().TrimEnd('.', ' ').Length > 40 ? string.Concat(teachername.ToString().TrimEnd('.', ' ').Substring(0, 30), " ...") : teachername.ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static IComparer<Group> GroupsReadingLevelSorter
        {
            get { return new GroupsReadingLevelComparer(); }
        }

        // --------------------------------------------------------------------------------------------------------------------
        // <copyright file="GroupsReadingLevelComparer" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
        //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
        // </copyright>
        // <summary>
        //   Comparer class for Groups Reading Level
        // </summary>
        // ---------------------------------------------------------------------------------------------------------------------
        private class GroupsReadingLevelComparer : IComparer<Group>
        {
            #region IComparer<Sessions> Members
            /// <summary>
            /// 
            /// </summary>
            /// <param name="first"></param>
            /// <param name="second"></param>
            /// <returns></returns>
            public int Compare(Group first, Group second)
            {
                return first.ReadingLevel.CompareTo(second.ReadingLevel);
            }
            #endregion
        }

        public int WordCount { get; set; }

        public string PageName { get; set; }

        public string RecordFilePath { get; set; }


        public int SessionHistoryId { get; set; }

        public int MinReadLevel { get; set; }
    }
}