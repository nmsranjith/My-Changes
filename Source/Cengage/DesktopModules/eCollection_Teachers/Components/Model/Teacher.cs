using System;
using DotNetNuke.Entities.Content;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using DotNetNuke.Modules.eCollection_Teachers.Components.Controller;
using System.Xml.Linq;
using System.Xml;

namespace DotNetNuke.Modules.eCollection_Teachers.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Teacher" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Teacher
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Teacher : ContentItem
    {
        #region Property Methods
        public int TeacherId
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string DisplayName
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string UserLoginName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
        public int BookReadLevelFrom
        {
            get;
            set;
        }
        public int BookReadLevelUpto
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public DateTime InvitedDate
        {
            get;
            set;
        }
        public int? AddedBy
        {
            get;
            set;
        }
        public string TeacherLoginName
        {
            get;
            set;
        }
        public char Active
        {
            get;
            set;
        }
        public DateTime CreatedDate
        {
            get;
            set;
        }
        public string UserCreated
        {
            get;
            set;
        }
        public Nullable<DateTime> DateModified
        {
            get;
            set;
        }
        public string UserModified
        {
            get;
            set;
        }
        public char AcceptInvitation
        {
            get;
            set;
        }
        public XDocument TeachersDelDoc
        {
            get;
            set;
        }
        public XmlDocument TeachersDoc
        {
            get;
            set;
        }
        public string ActionType
        {
            get;
            set;
        }
        public string FullNameFlag
        {
            get;
            set;
        }
        public int CustSubUserSk
        {
            get;
            set;
        }
        public string ProcName
        {
            get;
            set;
        }
        public char GrpType
        {
            get;
            set;
        }
        public string GrpCacheName
        {
            get;
            set;
        }
        public int SubscriptionId
        {
            get;
            set;
        }
        public string UpdateFlag
        {
            get;
            set;
        }
        public List<Teacher> SelectedTeachersList
        {
            get;
            set;
        }
        public List<IDCollection> SubscriptionList
        {
            get;
            set;
        }
        public List<IDCollection> CustSubUserSkList
        {
            get;
            set;
        }
        public int ClassId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }
        public string ImageUrl
        {
            get;
            set;
        }

        public string BookOpenedDate
        {
            get;
            set;
        }

        public string BookOpenedTime
        {
            get;
            set;
        }
        public DateTime OpenedDate
        {
            get;
            set;
        }        
        public int Minutes
        {
            get;
            set;
        }
        public int Seconds
        {
            get;
            set;
        }
        public int ProductId
        {
            get;
            set;
        }
        public string Word
        {
            get;
            set;
        }
        public string WordCount
        {
            get;
            set;
        }
        public string SubscriptionName
        {
            get;
            set;
        }
        public string BookID
        {
            get;
            set;
        }

        public string PageName
        {
            get;
            set;
        }
        public string RecPath
        {
            get;
            set;
        }
        public int MonthId
        {
            get;
            set;
        }
        public string profilelink
        {
            get;
            set;
        }
        public string SessionType
        {
            get;
            set;
        }
        public string EmailUrl
        {
            get;
            set;
        }
        public string MailBody
        {
            get;
            set;
        }
        #endregion


        #region Overridden Fill Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            TeacherId = int.Parse(dr["TeacherId"].ToString());
            FirstName = dr["FirstName"].ToString();
            LastName = dr["LastName"].ToString();
            UserLoginName = dr["UserLoginName"].ToString();
            DisplayName = dr["DisplayName"].ToString();
            FullName = dr["FullName"].ToString();
            CustSubUserSk = int.Parse(dr["CustomerSubsUserSk"].ToString());
            profilelink = eCollection_TeachersModuleBase.ProfileUrl+UserLoginName;
        }
        #endregion
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Helper" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //   Helper class
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public static class Helper
    {
        /// <summary>
        /// 
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
}