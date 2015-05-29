using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Books" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Books
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Books : ContentItem
    {
        public int PRODUCT_SK { set; get; }
        public int CUST_SUBS_ITEM_SK {set;get;}
        public string ATTRIBUTE_TYPE_ID { set; get; }
        public string ATTRIBUTE_TYPE_VALUE { set; get; }
        public string IMAGE_FILE_NAME { get; set; }
        public string IMAGE_TYPE { get; set; }
        public string Title { get; set; }
        public string COPYRIGHT_YEAR { get; set; }
        public string NO_OF_PAGES { get; set; }
        public string TEXTTYPE { get; set; }
        public string Author { get; set; }
        private bool _checked;
        //Added by kalai
        private string _className;
        private string _checkImgpathName;
        public DateTime ADDED_DATE;
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

        public string ColourLevel { get; set; }

        public string ReadingAge { get; set; }

        public string ReadingLevel { get; set; }

       
    }
}