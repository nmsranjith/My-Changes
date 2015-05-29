using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Users;
using DotNetNuke.Modules.eCollection_Sessions.Components;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using DotNetNuke.Entities.Modules;
using System.Globalization;
namespace DotNetNuke.Modules.eCollection_Sessions.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Sessions" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Sessions
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public sealed class Sessions : ContentItem, IComparable<Sessions>, IHydratable
    {
        public int? SessionId { get; set; }

        public int CUST_SUBS_SK { get; set; }

        public int? CREATED_BY_CUST_SUBS_USER_SK { get; set; }

        public int? ENDED_BY_CUST_SUBS_USER_SK { get; set; }               

        public string WorkType { get; set; }

        public MyEnums.SessionType SessionType { get; set; }             

        public DateTime SessionExpiryDate { get; set; }

        public string SessionStatus { get; set; }

        public string IsNameManualOverride { get; set; }

        public string Notes { get; set; }

        public string IsActive { get; set; }

        public new int? CreatedByUserId { get; set; }

        public string CreatedByUserName { get; set; }
        
        public new int? LastModifiedByUserId { get; set; }

        public new Nullable<DateTime> CreatedDate { get; set; }

        public new Nullable<DateTime> LastModifiedDate { get; set; }

        public List<SessionMembers> SessionMembers = new List<SessionMembers>();

        public List<SessionProducts> SessionProducts = new List<SessionProducts>();
       
        private string _name;
        private DateTime _sessionCreatedDate;
        private int _unOpened;

        public string Name
        {
            get 
            {
                return  _name; 
            }
            set { _name = value; }
        }
        
        public DateTime SessionCreatedDate 
        {             
            get {return _sessionCreatedDate; }
            set { _sessionCreatedDate = value;} 
        }

        public int UnOpened
        {
            get { return _unOpened; }
            set { _unOpened = value; }
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            //Call the base classes fill method to populate base class properties
            Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Null.SetNullString(dr["SessionName"]).ToLower());
            SessionCreatedDate = Null.SetNullDateTime(dr["CreatedDate"]);
            UnOpened = Null.SetNullInteger(dr["UnOpened"]);
            SessionExpiryDate = Null.SetNullDateTime(dr["ExpiryDate"]);
            SessionId = Convert.ToInt32(dr["SessionId"]);
            Notes = dr["Notes"] as string;
            SessionType = (MyEnums.SessionType)((dr["SessionType"].ToString() == "Guided") ? 1 : 2);
            SessionStatus = dr["SessionStatus"] as string;              
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Sessions other)
        {
            return _name.CompareTo(other.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        public static IComparer<Sessions> SessionCreatedDateSorter
        {
            get { return new SessionCreatedDateComparer(); }
        }

        // --------------------------------------------------------------------------------------------------------------------
        // <copyright file="SessionCreatedDateComparer" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
        //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
        // </copyright>
        // <summary>
        //   Comparer for Session Created Date
        // </summary>
        // ---------------------------------------------------------------------------------------------------------------------
        private class SessionCreatedDateComparer : IComparer<Sessions>
        {
            #region IComparer<Sessions> Members
            /// <summary>
            /// 
            /// </summary>
            /// <param name="first"></param>
            /// <param name="second"></param>
            /// <returns></returns>
            public int Compare(Sessions first, Sessions second)
            {
                return first.SessionCreatedDate.CompareTo(second.SessionCreatedDate);
            }
            #endregion
        }
    }
}