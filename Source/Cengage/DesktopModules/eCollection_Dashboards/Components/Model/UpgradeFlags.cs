using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;

namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="UpgradeFlags" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for UpgradeFlags
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class UpgradeFlags 
    {
        #region Class members

        public char TeacherFlag { get; set; }
        public char StudentFlag { get; set; }
        public char GroupsFlag { get; set; }
        public char BooksFlag { get; set; }
        public char UpgradeYearLevel { get; set; }
        public int CustSubsSk { get; set; }
        public string UserName { get; set; }
        public char FromMainScreen { get; set; }
        public string ActionType { get; set; }
        #endregion
         
    }
}