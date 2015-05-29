using DotNetNuke.Entities.Content;
using System;
using System.Globalization;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="DateCollection" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for DateCollection
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class DateCollection : ContentItem
    {       
        private string _text = string.Empty;
        public string AddedBy
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        private string _date;
        public string DateCreated
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }
        public string ActivityType { get; set; }
        public string Prefix { get; set; }
        public DateTime AddedDate { get; set; }
        
        #region Overridden Fill Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            AddedBy = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["AddedBy"].ToString()==string.Empty?"Admin":dr["AddedBy"].ToString());
            DateCreated = string.Format("{0:dd}{1} {0:MMMM yyyy}", Null.SetNullDateTime(dr["DateCreated"]),
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 1) ? "st" :
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 21) ? "st" :
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 2) ? "nd" :
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 22) ? "nd" :
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 3) ? "rd" :
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 23) ? "rd" :
                (Null.SetNullDateTime(dr["DateCreated"]).Day == 31) ? "st" : "th");
            AddedDate = Null.SetNullDateTime(dr["DateCreated"]);
            ActivityType = dr["ActivityType"].ToString();
            Prefix = dr["Prefix"].ToString();
        }
        #endregion
    }
}