using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DotNetNuke.Entities.Content;

namespace DotNetNuke.Modules.HESearchResults.Components
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Searchmodifiedmodal" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Search result module modal class
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    [Serializable]
    public class ListInfo : IDisposable
    {
        # region Constructor
        public ListInfo() { }
        #endregion

        # region Public Property
        public string Flag
        {
            get;
            set;
        }
        public int Productsk
        {
            get;
            set;
        }
        public string ImageFileName
        {
            get;
            set;
        }
        public int ProductID
        {
            get;
            set;
        }
        public string ISBN13
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public int Quantity
        {
            get;
            set;
        }
        public string ISBNS
        {
            get;
            set;
        }
        public string GenericFlag
        {
            get;
            set;
        }
        public string UserListQuoteSK
        {
            get;
            set;
        }
        public int UserSK
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string IS_FAVOURITE
        {
            get;
            set;
        }
        public string ListType
        {
            get;
            set;
        }
        public int ProductCount
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
        public string QUOTE_EXPIRES_ON
        {
            get;
            set;
        }
        public int QUOTE_REF_NUMBER
        {
            get;
            set;
        }

        public string AppTargetCode
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string Suburb
        {
            get;
            set;
        }
        public string Subjects
        {
            get;
            set;
        }
        public string First_Name
        {
            get;
            set;
        }
        public string Last_Name
        {
            get;
            set;
        }



        #endregion

        public void Dispose()
        {
            GC.Collect();
        }
    }

    [Serializable]
    public class QuoteInfo : IDisposable
    {
        # region Constructor
        public QuoteInfo()
        {
            ProductDetails = new DataTable();
            ProductDetails.Columns.Add(new DataColumn("PRODUCT_SK", typeof(int)));
            ProductDetails.Columns.Add(new DataColumn("QTY", typeof(int)));
            ProductDetails.Columns.Add(new DataColumn("DISCOUNT_PRICE", typeof(decimal)));
            ProductDetails.Columns.Add(new DataColumn("RRP", typeof(decimal)));
        }
        #endregion

        # region Public Property
        public string ACTION
        {
            get;
            set;
        }
        public int USER_SK
        {
            get;
            set;
        }
        public string USER_CREATED
        {
            get;
            set;
        }
        public int TRADING_PARTNER_ACCOUNT_SK
        {
            get;
            set;
        }
        public string TradingPartnerAccNo { get; set; }
        public string QUOTE_NAME
        {
            get;
            set;
        }
        public int QUOTE_VALID_DAYS
        {
            get;
            set;
        }
        public string CURRENCY
        {
            get;
            set;
        }
        public decimal SHIPPING_COST_STD
        {
            get;
            set;
        }
        public decimal FreeShippingEligiblePrice
        {
            get;
            set;
        }
        public string GST_APPLICABLE
        {
            get;
            set;
        }
        public int LIST_QUOTE_SK
        {
            get;
            set;
        }

        public DataTable ProductDetails
        {
            get;
            set;
        }


        #endregion

        public void Dispose()
        {
            GC.Collect();
        }
    }


    public class SearchResults : ContentItem
    {
        public string searchval
        {
            get;
            set;
        }
        public string isbn
        {
            get;
            set;
        }
        public string Bookimg
        {
            get;
            set;
        }
        public string authname
        {
            get;
            set;
        }
        public string student
        {
            get;
            set;
        }
        public string Product
        {
            get;
            set;
        }
        public string description
        {
            get;
            set;
        }
        public string LeftMainMenu
        {
            get;
            set;
        }
        public string LeftSubMenu
        {
            get;
            set;
        }



    }
}