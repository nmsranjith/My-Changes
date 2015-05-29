using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Books.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SessionProducts" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for SessionProducts
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class SessionProducts
    {
        // Session Products
        public int SESSION_PRODUCT_SK { get; set; }

        public int? CUST_SUBS_ITEM_SK { get; set; }

        public new Nullable<DateTime> Books_AddedDate { get; set; }

        public int? ADDED_BY_CUST_SUBS_USER_SK { get; set; }

        public string AttributeTypeId { get; set; }

        public string AttributeTypeValue { get; set; }

        public string ImageFileName { get; set; }

        public string ImageType { get; set; }

        public string Title { get; set; }

        public string CopyRightYear { get; set; }

        public string NoOfPages { get; set; }

        public string PreferredName { get; set; }

        public string Active { get; set; }
    }
}