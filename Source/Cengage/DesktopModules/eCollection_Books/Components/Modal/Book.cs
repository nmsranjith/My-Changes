using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using System.Xml;

namespace DotNetNuke.Modules.eCollection_Books.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Book" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Book
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Book : ContentItem
    {
        public int PRODUCT_SK { set; get; }
        public int CUST_SUBS_ITEM_SK { set; get; }
        public string ATTRIBUTE_TYPE_ID { set; get; }
        public string ATTRIBUTE_TYPE_VALUE { set; get; }
        public string IMAGE_FILE_NAME { get; set; }
        public string IMAGE_TYPE { get; set; }
        public string Title { get; set; }
        public string COPYRIGHT_YEAR { get; set; }
        public string TEXTTYPE { get; set; }
        public string NO_OF_PAGES { get; set; }
        public string Author { get; set; }
        private bool _checked;
        private bool _alreadyAvailble;
        //Added by kalai
        private string _className;
        private string _checkImgpathName;
        private int _SubscriptionId;
        private string _colourLevel;
        private int _colourOrder;
        public int SubscriptionId
        {
            get { return _SubscriptionId; }
            set { _SubscriptionId = value; }
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
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        public string CheckImgPathName
        {
            get { return _checkImgpathName; }
            set { _checkImgpathName = value; }
        }
       
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        public string ColourLevel
        {
            get { return _colourLevel; }
            set { _colourLevel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            PRODUCT_SK = Convert.ToInt32(dr["PRODUCT_SK"]);
            CUST_SUBS_ITEM_SK = Convert.ToInt32(dr["CUST_SUBS_ITEM_SK"]);
            //ATTRIBUTE_TYPE_ID = dr["ATTRIBUTE_TYPE_ID"].ToString();
            //ATTRIBUTE_TYPE_VALUE = dr["ATTRIBUTE_TYPE_VALUE"].ToString();
            IMAGE_FILE_NAME = dr["IMAGE_FILE_NAME"].ToString();
            IMAGE_TYPE = dr["IMAGE_TYPE"].ToString();
            Title = dr["Title"].ToString();
            COPYRIGHT_YEAR = dr["COPYRIGHT_YEAR"].ToString();
            NO_OF_PAGES = dr["NO_OF_PAGES"].ToString();
            Author = dr["PREFERRED_NAME"].ToString();
            ColourLevel = dr["ColourLabel"].ToString();
            ReadingAge = dr["ReadingAge"].ToString();
            ReadingLevel = dr["ReadingLevel"].ToString();
            Checked = false;
            //Added By kalai;
            ClassName = string.Empty;
            CheckImgPathName = string.Empty;
        }

        public DateTime Grace_period { get; set; }

        public int SelectedBooks { get; set; }

        public int TotalBooks { get; set; }

        public string ReadingAge { get; set; }

        public string BookCount { get; set; }

        public string AttributeType { get; set; }

        public int BooksLeft
        {

            get { return (TotalBooks - SelectedBooks); }
        }

        public int DaysLeft
        {
            get { return Grace_period.Subtract(DateTime.Now.Date).Days; }
        }

        public bool isAfterGracePeriod7Days
        {
            get { return (DateTime.Now.Subtract(Grace_period.Date).Days > 7); }
        }

        public bool isAfterGracePeriod
        {
            get { return (DateTime.Now.Subtract(Grace_period.Date).Days > 0); }
        }

        public XmlDocument ProductsDoc
        {
            get;
            set;
        }

        public bool AlreadyAvailable { 
            
            get {
                return _alreadyAvailble
                    ;}
            set { _alreadyAvailble = value; }
        
        }       

        public string ReadingLevel { get; set; }

        public int ColourOrder
        {
            get { return _colourOrder; }
            set { _colourOrder = value; }
        }
        public string Selected { get; set; }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Book" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Book packs
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class BookPack 
    {
        public int BookPackSk { set; get; }
        public string BookPackName { set; get; }
        public string PackDescription { set; get; }
        public string UserName { get; set; }
        public int SubscriptionId { get; set; }
        public string ISBN { get; set; }
        public string UncheckedISBNs { get; set; }
        public string BookPackType { get; set; }

        public string SearchText { get; set; }
        public string ConditionText { get; set; }
        public string WeightText { get; set; }
    }
}