using System;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Content;

namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Subscription" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Subscription
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Subscription :ContentItem
    {
        #region Class Members
        public int SubsId { get;set; }
        public string Name { get; set; }
        public string NewName { get; set; }
        public string AdminName { get; set; }
        public int TotalBooks { get; set; }
        public int BooksAdded { get; set; }
        public int DaysLeft { get; set; }
        public int TotalLicenses { get; set; }
        public int AvailableLicenses { get; set; }
        public DateTime StartedDate { get; set; }
        public string ActivityType { get; set; }
        public int ScrollCount { get; set; }
        public List<Subscription> SubscriptionList { get; set; }
        public List<Activity> ActivityList { get; set; }
        public decimal MaxTextLength { get; set; }
        public string DateCreated { get; set; }
        public string ActivityTypeName { get; set; }
        public int TeachersCount { get; set; }
        public int UsedLicences { get; set; }
        public int GraceDaysLeft { get; set; }
        public string AddEditBtnText { get; set; }
        public string SubscriptionType { get; set; }
        public string TitleText { get; set; }
        public string ClassName { get; set; }

        #endregion
        
        #region Overridden Fill Method
            /// <summary>
            /// 
            /// </summary>
            /// <param name="dr"></param>
            public override void Fill(System.Data.IDataReader dr)
            {
                BooksAdded = int.Parse(dr["TotalBooks"].ToString());
                TotalLicenses = Null.SetNullInteger(dr["TotalLicenses"]);
                AvailableLicenses = Null.SetNullInteger(dr["AvailableLicenses"]);
                AdminName = Null.SetNullString(dr["AdminName"]);
                StartedDate = Null.SetNullDateTime(dr["StartedDate"]);
                DaysLeft = Null.SetNullInteger(dr["DaysLeft"]);
                GraceDaysLeft =Null.SetNullInteger(dr["GraceDaysLeft"]);
            }
        #endregion          
    }
}