using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;
using System.Text;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;
using System.Globalization;

namespace DotNetNuke.Modules.eCollection_Groups.Components
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Groups" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for groups
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Groups : ContentItem, IComparable<Groups>
    {
        private int? GROUP_SK;

        private DateTime DATE_MODIFIED;
        private int? GRP_MEM_SK;
        private int MembersCount;
        private int ADDED_BY_GRP_MEM_SK;
        private int? CUST_SUBS_USER_SK;
        private DateTime ADDED_DATE;
        private string StudentName;
        private string LAST_NAME;
        private int UserId;
        private string subsId;
        private int CustomerSubSK;
        private string Subscription;
        private bool _checked;

        //Common
        private int _memberID;
        public int MemberID { get { return _memberID; } set { _memberID = value; } }
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        //CreateGroup
        private int? CREATED_BY_CUST_SUBS_USER_SK;
        private int? CUST_SUBS_SK;
        private string NAME;
        private char GROUP_TYPE;
        private Nullable<DateTime> CREATED_DATE;
        private char ACTIVE;
        private DateTime DATE_CREATED;
        private string USER_CREATED;
        private string USER_LOGIN_NAME;
        //CreateGroup Prop
        public new int? CreatedByUserId { get { return CREATED_BY_CUST_SUBS_USER_SK; } set { CREATED_BY_CUST_SUBS_USER_SK = value; } }
        public new int? CustomerSubId { get { return CUST_SUBS_SK; } set { CUST_SUBS_SK = value; } }
        public string Name { get { return NAME; } set { NAME = value; } }
        public char GroupType { get { return GROUP_TYPE; } set { GROUP_TYPE = value; } }
        public new Nullable<DateTime> CreatedOnDate { get { return CREATED_DATE; } set { CREATED_DATE = value; } }
        public new char ActiveFlag { get { return ACTIVE; } set { ACTIVE = value; } }
        public new DateTime DateCreated { get { return DATE_CREATED; } set { DATE_CREATED = value; } }
        public new string UserCreated { get { return USER_CREATED; } set { USER_CREATED = value; } }
        public new string LoginName { get { return USER_LOGIN_NAME; } set { USER_LOGIN_NAME = value; } }
        //CreateGroup Prop

        //SubscriptionDropDown
        public string Subcriptions { get { return Subscription; } set { Subscription = value; } }
        public int CustomerSubscriptionId { get { return CustomerSubSK; } set { CustomerSubSK = value; } }
        //SubscriptionDropDown

        //GroupProfile
        private int _booksOpened;
        private int _independent;
        private int _guided;
        private int _myRecording;
        private int _myWords;
        private string _password;
        private string _emailAddress;
        private int _minStartReadFrom;
        private int _maxStartReadUpto;
        private int _minCurrReadFrom;
        private int _maxCurrReadUpto;
        private int _groupMemberId;
        //GroupProfileHistory
        private string _pageName;
        private string _recordFilePath;
        private DateTime _recordStartTime;
        private string _circledWord;
        private int _wordCount;
        private string _title;
        private string _ISBN;
        private string _bookImgName;
        private DateTime _sessionCreatedDate;
        private DateTime _bookOpenAt;
        private Int32  _booksOpenedMin;
        private int _sessionSK;
        private string _sessionName;
        private string _sessionType;
        private string _sessionNote;
        private string _bookWrapperName;
        private int _bookOpened;
        private int _bookUnOpened;
        private int _productId;
        //GroupProfile

        public int BooksOpened { get { return _booksOpened; } set { _booksOpened = value; } }
        public int Independent { get { return _independent; } set { _independent = value; } }
        public int Guided { get { return _guided; } set { _guided = value; } }
        public int MyRecording { get { return _myRecording; } set { _myRecording = value; } }
        public int MyWords { get { return _myWords; } set { _myWords = value; } }
        public string PassWord { get { return _password; } set { _password = value; } }
        public string EmailAddress { get { return _emailAddress; } set { _emailAddress = value; } }
        public int MinStartReadFrom { get { return _minStartReadFrom; } set { _minStartReadFrom = value; } }
        public int MaxStartReadUpto { get { return _maxStartReadUpto; } set { _maxStartReadUpto = value; } }
        public int MinCurrReadFrom { get { return _minCurrReadFrom; } set { _minCurrReadFrom = value; } }
        public int MaxCurrReadUpto { get { return _maxCurrReadUpto; } set { _maxCurrReadUpto = value; } }
        //GroupProfileHistory
        public string PageName { get { return _pageName; } set { _pageName = value; } }
        public string RecordFilePath { get { return _recordFilePath; } set { _recordFilePath = value; } }
        public DateTime RecordStartTime { get { return _recordStartTime; } set { _recordStartTime = value; } }
        public string CircledWord { get { return _circledWord; } set { _circledWord = value; } }
        public int WordCount { get { return _wordCount; } set { _wordCount = value; } }
        public string Title { get { return _title; } set { _title = value; } }
        public string ISBN { get { return _ISBN; } set { _ISBN = value; } }
        public string BookImgName { get { return _bookImgName; } set { _bookImgName = value; } }
        public DateTime SessionCreatedDate { get { return _sessionCreatedDate; } set { _sessionCreatedDate = value; } }
        public DateTime BookOpenAt { get { return _bookOpenAt; } set { _bookOpenAt = value; } }
        public Int32  BooksOpenedMin { get { return _booksOpenedMin; } set { _booksOpenedMin = value; } }
        public int GroupMemberID { get { return _groupMemberId; } set { _groupMemberId = value; } }
        public int SessionID { get { return _sessionSK; } set { _sessionSK = value; } }
        public string SessionName { get { return _sessionName; } set { _sessionName = value; } }
        public string SessionType { get { return _sessionType; } set { _sessionType = value; } }
        public string SessionNote { get { return _sessionNote; } set { _sessionNote = value; } }
        public string BookWrapperName { get { return _bookWrapperName; } set { _bookWrapperName = value; } }
        public int BookOpened { get { return _bookOpened; } set { _bookOpened = value; } }
        public int BookUnOpened { get { return _bookUnOpened; } set { _bookUnOpened = value; } }
        public int ProductID { get { return _productId; } set { _productId = value; } }

        public int GroupId { get; set; }


        public int MemberCount { get { return MembersCount; } set { MembersCount = value; } }




        public new int? LastModifiedByUserId { get; set; }



        public new Nullable<DateTime> LastModifiedOnDate { get; set; }

        public int? ModuleId { get; set; }

        public int? PortalId { get; set; }




        private string TFIRST_NAME;
        public string TeacherName
        {
            get
            {
                return TFIRST_NAME;
            }
            set
            {
                TFIRST_NAME = value;
            }
        }

        private int _maxReadLevel;
        private int _minReadLevel;
        private int _currentReadLevel;
        public int MaxReadLevel
        {
            get { return _maxReadLevel; }
            set { _maxReadLevel = value; }
        }
        public int CurrentReadLevel
        {
            get { return _currentReadLevel; }
            set { _currentReadLevel = value; }
        }
        public int MinReadLevel
        {
            get { return _minReadLevel; }
            set { _minReadLevel = value; }
        }
        private string _name;
        public string Names
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


        public UserInfo CreatedByUser
        {
            get
            {
                if (CreatedByUserID > 0)
                {
                    return UserController.GetUserById(PortalId.GetValueOrDefault(), CreatedByUserID);

                }
                return null;
            }
        }

        public UserInfo LastModifiedUser
        {
            get
            {
                if (LastModifiedByUserId > 0)
                {
                    return UserController.GetUserById(PortalId.GetValueOrDefault(), LastModifiedByUserId.GetValueOrDefault());
                }
                return null;
            }
        }
        public string StudentNames
        {
            get { return StudentName; }
            set { StudentName = value; }
        }
        public string UserLoginName
        {
            get;
            set;
        }
        public int UserID
        {

            get { return UserId; }
            set { UserId = value; }
        }
        public int SubscriptionId
        {
            get;
            set;
        }

        public string SubscriptionName
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }

        public char IsClearFromIpad
        {
            get;
            set;
        }

        public DateTime WordCircledAt
        {
            get;
            set;
        }
        public string GpType
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
            //Call the base classes fill method to populate base class properties
            GroupController groupController = GroupController.Instance;
            MemberCount = Null.SetNullInteger(int.Parse(dr["MembersCount"].ToString()));
            Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(dr["NAME"]).ToLower());
            GroupId = int.Parse(dr["GROUP_SK"].ToString());
            CustomerSubId = int.Parse(dr["CUST_SUBS_SK"].ToString());
            FillReadingLevel(groupController.GetReadingLevel(GroupId));
            TeacherName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["GrpMems"].ToString());//ConcatTeacherName(memberNameList).ToLower());
            GpType = dr["GpType"].ToString();
            GroupType = char.Parse(dr["GROUP_TYPE"].ToString());
            Checked = false;
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
                    MaxReadLevel = list[i];
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

                        if (i != (list.Count))
                        {
                            teachername.Append(list[i].ToString());
                            teachername.Append(", ");

                        }
                        if (i == (list.Count - 1))
                        {
                            teachername.Remove(teachername.Length - 2, 1);
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

        public List<IDCollection> TeachersList = new List<IDCollection>();

        public List<Students> StudentList = new List<Students>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Groups other)
        {
            return NAME.CompareTo(other.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IComparer<Groups> GroupsReadingLevelSorter
        {
            get { return new GroupsReadingLevelComparer(); }
        }

        // --------------------------------------------------------------------------------------------------------------------
        // <copyright file="GroupsReadingLevelComparer" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
        //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
        // </copyright>
        // <summary>
        //    Groups ReadingLevel Comparer class
        // </summary>
        // ---------------------------------------------------------------------------------------------------------------------
        private class GroupsReadingLevelComparer : IComparer<Groups>
        {
            #region IComparer<Groups> Members
            /// <summary>
            /// 
            /// </summary>
            /// <param name="first"></param>
            /// <param name="second"></param>
            /// <returns></returns>
            public int Compare(Groups first, Groups second)
            {
                return first.MaxReadLevel.CompareTo(second.MaxReadLevel);
            }
            #endregion
        }


    }
}