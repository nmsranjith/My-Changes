using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;

namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Renewel" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Renewel
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Renewel:ContentItem
    {
        public int NEW_CUST_SUBS_SK = 0;
        public int OLD_CUST_SUBS_SK = 0;
        public DateTime RENEWED_DATE = new DateTime();
        public string OLD_SUBS_NAME = string.Empty;
    }
}
