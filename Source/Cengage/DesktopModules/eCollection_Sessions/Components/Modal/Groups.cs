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
    public class Groups : ContentItem, IComparable<Groups>, IHydratable
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
        private DateTime _sessionCreatedDate;        
        public DateTime SessionCreatedDate { get { return _sessionCreatedDate; } set { _sessionCreatedDate = value; } }
        private DateTime _bookOpenAt;
        public DateTime BookOpenAt { get { return _bookOpenAt; } set { _bookOpenAt = value; } }
        private int _booksOpenedMin;
        
        public int BooksOpenedMin { get { return _booksOpenedMin; } set { _booksOpenedMin = value; } }
        
        public override void Fill(System.Data.IDataReader dr)
        {
            //Call the base classes fill method to populate base class properties
            SessionController sessionController = SessionController.Instance;
            MemberCount = Null.SetNullInteger(int.Parse(dr["MembersCount"].ToString()));
            Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(dr["NAME"].ToString()).ToLower());
            GroupId = int.Parse(dr["GROUP_SK"].ToString());
            TeacherName = Null.SetNullString(ConcatTeacherName(SessionController.Instance.GetTeacherName(GroupId)));
            FillReadingLevel(sessionController.GetReadingLevel(GroupId));
            //ReadingLevel = 1;
        }
        
        public int CompareTo(Groups other)
        {
            return _name.CompareTo(other.Name);
        }

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
                    return teachername.ToString();
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




        public int WordCount { get; set; }

        public string PageName { get; set; }

        public string RecordFilePath { get; set; }


        public int SessionHistoryId { get; set; }

        public int MinReadLevel { get; set; }
    }
}