using System;
using System.Collections.Generic;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;
using System.Text;
using DotNetNuke.Modules.eCollection_Students.Components.Controllers;
using System.Globalization;

namespace DotNetNuke.Modules.eCollection_Students.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Groups" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for groups
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Groups : ContentItem,IComparable<Groups>, IHydratable
    {
        private int _studentsCount;
        public int StudentsCount
        {
            get
            {
                return _studentsCount;
            }
            set
            {
                _studentsCount = value;
            }
        }
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
        private int _subsSk;
        public int SubsSk
        {
            get
            {
                return _subsSk;
            }
            set
            {
                _subsSk = value;
            }
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
            StudentsCount = Null.SetNullInteger(dr["StudentsCount"]);
            MemberCount = Null.SetNullInteger(dr["MembersCount"]);
            NameToolTip = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(dr["Name"]) != "Z%-11-Z%" ? Null.SetNullString(dr["Name"]).ToLower() : "All Other Students");
            Name = NameToolTip.Length > 27 ? string.Concat(NameToolTip.Substring(0, 25), " ..") : NameToolTip;
            GroupId = Null.SetNullInteger(dr["GroupId"]);
            SubsSk = Null.SetNullInteger(dr["SubsSk"]);
            TeacherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(ConcatTeacherName(StudentsController.Instance.GetTeacherName(GroupId))));
            ReadingLevel = Null.SetNullInteger(dr["ReadingLevel"]);
            ListType = Null.SetNullString(dr["ListType"]);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Groups other)
        {
            return _name.CompareTo(other.Name);
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
                                teachername.Append("...");
                            }
                            else
                            {
                                teachername.Append(" and ");
                                teachername.Append(list[i].ToString());
                            }
                        }
                    }
                    return teachername.ToString().TrimEnd('.', ' ').Length > 40 ? string.Concat(teachername.ToString().TrimEnd('.', ' ').Substring(0, 35), " ...") : teachername.ToString();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IComparer<Groups> GroupsReadingLevelSorter
        {
            get { return new GroupsReadingLevelComparer(); }
        }

        private class GroupsReadingLevelComparer : IComparer<Groups>
        {
            #region IComparer<Sessions> Members

            public int Compare(Groups first, Groups second)
            {
                return first.ReadingLevel.CompareTo(second.ReadingLevel);
            }
            #endregion
        }
    }
}