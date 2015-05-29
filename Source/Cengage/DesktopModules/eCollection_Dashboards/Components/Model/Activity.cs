using System;
using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;
using System.Globalization;

namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Activity" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Activity
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Activity : ContentItem
    {
        #region Class Members
        public string DateCreated { get; set; }
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public int UsedLicenses { get; set; }
        public int TotalBooks { get; set; }
        public double MaxLength { get; set; }
        public string TitleString { get; set; }
        public string AddedBy { get; set; }
        public string ClassName { get; set; }
        #endregion

        #region Overridden Fill Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            //DateTime date;
            DateCreated = string.Format("{0:dd}{1} {0:MMMM yyyy}", Null.SetNullDateTime(dr["DateCreated"]), 
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 1) ? "st" :
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 21) ? "st" : 
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 2) ? "nd" :
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 22) ? "nd" : 
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 3) ? "rd" :
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 23) ? "rd" : 
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 31) ? "st" : "th");
            ActivityType = dr["ActivityType"].ToString();
            // DateTime.TryParse(Null.SetNullString(dr["DateCreated"]), out date) ? string.Format("{0:dd}{1} {0:MMMM yyyy}", date, (date.Day == (1 | 21 | 31)) ? "st" : (date.Day == (2 | 22)) ? "nd" : (date.Day == (3 | 23)) ? "rd" : "th") : string.Empty;
            TitleString = dr["Name"].ToString();
            Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(TitleString.Length > 10 ? string.Concat(TitleString.Substring(0, 9), " ..") : TitleString);
            AddedBy = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["AddedBy"].ToString() == string.Empty ? "Admin" : dr["AddedBy"].ToString());            
            ClassName = ActivityType == "STUDENTS ADDED" ? "DashBoard_Items_Students" :
                      ActivityType == "TEACHERS ADDED" ? "DashBoard_Items_Teachers" :
                      ActivityType == "GROUPS CREATED" || ActivityType == "GROUPS ADDED" ? "DashBoard_Items_CrtGroups" :
                      ActivityType == "SESSIONS CREATED" || ActivityType == "SESSIONS ENDED" || ActivityType == "SESSIONS ADDED" ? "DashBoard_Items_AddSessions" :
                      ActivityType == "PM ECOLLECTION UPGRADED" ? "UpgradedDetailsDiv_BooksCount" :
                      ActivityType == "BOOKS SELECTED" ? "DashBoard_Items_books" : string.Empty;
        }
        #endregion           
    }
}