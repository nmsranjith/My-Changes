
using DotNetNuke.Entities.Content;
using System;
namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Users" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Users
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Users : ContentItem
    {
        #region Class Members
            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string UserRole { get; set; }
            public string UserLoginName { get; set; }
            public char VideoFlag { get; set; }
            public char GetStartedFlag { get; set; }
            public DateTime AddedDate { get; set; }
            public int SubscriptionId { get; set; }
            public char Active { get; set; }
        #endregion
        
        #region Overridden Fill Method
            /// <summary>
            /// 
            /// </summary>
            /// <param name="dr"></param>
            public override void Fill(System.Data.IDataReader dr)
            {
                UserId = int.Parse(dr["UserId"].ToString());
                FirstName = dr["FirstName"].ToString();
                LastName = dr["LastName"].ToString();
                UserRole = dr["UserRole"].ToString();
                VideoFlag = dr["VideoFlag"].ToString() == string.Empty ? 'Y' : char.Parse(dr["VideoFlag"].ToString());
                GetStartedFlag = dr["GetStartedFlag"].ToString() == string.Empty ? 'Y' : char.Parse(dr["GetStartedFlag"].ToString()); 
            }
        #endregion           
    }
}