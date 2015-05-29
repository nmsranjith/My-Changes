using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Books.Data;
using System.Data;
using System.Xml;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.eCollection_Books.Components.Controllers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="BooksController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public sealed class BooksController
    {
         private static readonly BooksController instance = new BooksController();

         private static readonly List<string> CacheKeys = new List<string>();
      
        private BooksController() { }

        public static BooksController Instance
        {
            get
            {
                return instance;
            }
        }

        #region memberfunction
        /// <summary>
        /// 
        /// </summary>
        public void ClearAllCache()
        {
            foreach (string cacheKey in CacheKeys)
                DataCache.RemoveCache(cacheKey);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<Book> GetBooks(int subId, string loginName)
        {            
            List<Book> books = new List<Book>();
            books = CBO.FillCollection<Book>(DataProvider.Instance().GetBooks(subId, loginName));                          
            return books;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public Book GetBooksGracePeriod(int subId,string loginName)
        {            
            IDataReader dr = DataProvider.Instance().GetBooksGracePeriod(subId,loginName);
            Book book = new Book();
            try
            {
                if (dr.Read())
                {
                    if(dr["Grace_period"].ToString().Trim() != "")
                    book.Grace_period = Convert.ToDateTime(dr["Grace_period"].ToString());

                    book.SelectedBooks = Convert.ToInt32(dr["SelectedBooks"].ToString());
                    book.TotalBooks = Convert.ToInt32(dr["TotalBooks"].ToString());
                }
            }
            finally
            {
                
                CBO.CloseDataReader(dr, true);
            }
            return book;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="Years"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<Book> GetReadingAge(int subId, string Years, string loginName)
        {
            IDataReader dr = DataProvider.Instance().GetReadingAge(subId,Years, loginName);
            List<Book> books = new List<Book>();
            try
            {
                while (dr.Read())
                {
                    Book book = new Book();
                    book.ReadingAge = dr["ReadingAge"].ToString();
                    book.BookCount = dr["BookCount"].ToString();
                    books.Add(book);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return books;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="AttributeType"></param>
        /// <param name="AttributeValue"></param>
        /// <returns></returns>
        public List<Book> GetReadingAgeBooks(int subId,string AttributeType, string AttributeValue)
        {
            List<Book> books = new List<Book>();
            IDataReader dr = DataProvider.Instance().GetReadingAgeBooks(subId, AttributeType, AttributeValue);

            try
            {
                List<Book> templist= new List<Book>();
                while (dr.Read())
                {

                    Book book = new Book();
                    book.PRODUCT_SK = Convert.ToInt32(dr["PRODUCT_SK"]);
                    
                    book.IMAGE_FILE_NAME = dr["IMAGE_FILE_NAME"].ToString();
                    book.IMAGE_TYPE = dr["IMAGE_TYPE"].ToString();
                    book.Title = dr["Title"].ToString().Replace("eBook:", "");
                    book.COPYRIGHT_YEAR = dr["COPYRIGHT_YEAR"].ToString();
                    book.NO_OF_PAGES = Null.SetNullInteger(dr["NO_OF_PAGES"]) < 0 ? "0" : dr["NO_OF_PAGES"].ToString();
                    book.Author = dr["PREFERRED_NAME"].ToString();
                    book.ATTRIBUTE_TYPE_ID = dr["ATTRIBUTE_TYPE_ID"].ToString();
                    book.ATTRIBUTE_TYPE_VALUE = dr["ATTR_VALUE"].ToString();

                    book.Checked = false;
                    if (Convert.ToInt32(dr["Available"]) == 0)
                        book.AlreadyAvailable = true;
                    else
                        book.AlreadyAvailable = false;
                    book.ClassName = string.Empty;
                    book.CheckImgPathName = string.Empty;
                    templist.Add(book);
                }

                //


                List<Book> list = new List<Book>();

                int count = templist.Count;
                int[] booksList = GetBookListByAge(subId, AttributeType, AttributeValue);


                for (int i = 0; i < booksList.Length; i++)
                {
                    if (templist.Count > 0)
                    {
                        Book book1 = new Book();
                        book1.PRODUCT_SK = booksList[i];
                        int[] index = new int[templist.Count];
                        int count1 = 0;
                        foreach (Book booklist in templist)
                        {
                            if (booklist.PRODUCT_SK == booksList[i])
                            {
                                book1.IMAGE_FILE_NAME = booklist.IMAGE_FILE_NAME;
                                book1.IMAGE_TYPE = booklist.IMAGE_TYPE;
                                book1.Title = booklist.Title;
                                book1.COPYRIGHT_YEAR = booklist.COPYRIGHT_YEAR;
                                book1.NO_OF_PAGES = booklist.NO_OF_PAGES;
                                book1.Author = booklist.Author;
                                book1.Checked = booklist.Checked;
                                book1.ClassName = booklist.ClassName;
                                book1.AlreadyAvailable = booklist.AlreadyAvailable;
                                book1.CheckImgPathName = booklist.CheckImgPathName;
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "GUIDED READING LEVEL")
                                {
                                    book1.ReadingLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "COLOUR LEVEL")
                                {
                                    book1.ColourLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "READING AGE")
                                {
                                    book1.ReadingAge = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "TEXT TYPE")
                                {
                                    book1.TEXTTYPE = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                index[count1] = templist.IndexOf(booklist);
                            }

                        }
                        list.Add(book1);
                        while (count1 > 0)
                        {

                            for (int j = count1 - 1; j >= 0; j--)
                            {
                                templist.RemoveAt(index[j]);
                                count1--;
                            }
                        }


                    }
                }
                books = list;

            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return books ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="AttributeType"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<Book> GetCategories(int subId, string AttributeType, string loginName)
        {
            IDataReader dr = DataProvider.Instance().GetReadingCategories(subId, AttributeType, loginName);
            List<Book> books = new List<Book>();
            try
            {
                while (dr.Read())
                {
                    Book book = new Book();
                    book.AttributeType = dr["Type"].ToString();
                    book.BookCount = dr["BookCount"].ToString();
                    if (book.AttributeType == "Magenta")
                        book.ColourOrder = 1;
                    else if (book.AttributeType == "Red")
                        book.ColourOrder = 2;
                    else if (book.AttributeType == "Yellow")
                        book.ColourOrder = 3;
                    else if (book.AttributeType == "Blue")
                        book.ColourOrder = 4;
                    else if (book.AttributeType == "Green")
                        book.ColourOrder = 5;
                    else if (book.AttributeType == "Orange")
                        book.ColourOrder = 6;
                    else if (book.AttributeType == "Turquoise")
                        book.ColourOrder = 7;
                    else if (book.AttributeType == "Purple")
                        book.ColourOrder = 8;
                    else if (book.AttributeType == "Gold")
                        book.ColourOrder = 9;
                    else if (book.AttributeType == "Silver")
                        book.ColourOrder = 10;
                    
                    books.Add(book);
                }

            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return books;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AttributeType"></param>
        /// <returns></returns>
        public List<Book> GetBooksByCategories(string AttributeType)
        {
            IDataReader dr = DataProvider.Instance().GetBooksByCategories(AttributeType);
            List<Book> books = new List<Book>();
            try
            {
                while (dr.Read())
                {
                    Book book = new Book();
                    book.AttributeType = Null.SetNullString(dr["Type"]).Trim() == string.Empty ? "Primary subject" : Null.SetNullString(dr["Type"]).Trim();
                    book.BookCount = dr["BookCount"].ToString();
                    books.Add(book);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            if (AttributeType.ToUpper() == "PRIMARY SUBJECT")
            {
                int index=0 ;
                bool PrimarySubReq = true;
                foreach (Book booklist in books)
                {
                    if (booklist.AttributeType.ToLower() == "primary subject")
                    {
                        index = books.IndexOf(booklist);
                        if (books.Count > 1)
                        {
                            PrimarySubReq = false;
                            break;
                        }
                    }
                }
                if (PrimarySubReq == false)
                    books.RemoveAt(index);
            }
            return books;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="AttributeType"></param>
        /// <param name="AttributeValue"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public List<Book> GetLevels(int subId, string AttributeType, string AttributeValue, string loginName)
        {
            IDataReader dr = DataProvider.Instance().GetLevels(subId, AttributeType,AttributeValue, loginName);
            List<Book> books = new List<Book>();
            try
            {
                while (dr.Read())
                {
                    Book book = new Book();
                    book.AttributeType = dr["Type"].ToString();
                    book.BookCount = dr["BookCount"].ToString();
                    books.Add(book);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return books;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="products"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int AddBooksToSubscription(int subId, XmlDocument products, string loginName)
        {
            eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
            return DataProvider.Instance().AddBooksToSubscription(subId, products, loginName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="products"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int RemoveBooksFromSubscription(int subId, XmlDocument products, string loginName)
        {
            eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
            return DataProvider.Instance().RemoveBooksfromSubscription(subId, products, loginName);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="fromReadLvl"></param>
        /// <param name="toReadLvl"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public List<Book> GetBooksByReadingLevel(int subId, int fromReadLvl, int toReadLvl,string LoginName)
        {
            string cacheKey = string.Format("GetBooksByReadingLevel{0}", subId);
            List<Book> books = (List<Book>)DataCache.GetCache(cacheKey);
            if (books == null)
            {
                IDataReader dr = DataProvider.Instance().GetBooksByReadingLevel(subId, fromReadLvl, toReadLvl, LoginName);
                List<Book> templist = new List<Book>();
                try
                {
                    while (dr.Read())
                    {

                        Book book = new Book();
                        book.PRODUCT_SK = Convert.ToInt32(dr["PRODUCT_SK"]);
                        book.CUST_SUBS_ITEM_SK = Convert.ToInt32(dr["CUST_SUBS_ITEM_SK"]);
                        book.IMAGE_FILE_NAME = dr["IMAGE_FILE_NAME"].ToString();
                        book.IMAGE_TYPE = dr["IMAGE_TYPE"].ToString();
                        book.Title = dr["Title"].ToString().Replace("eBook:", "");
                        book.COPYRIGHT_YEAR = dr["COPYRIGHT_YEAR"].ToString();
                        book.NO_OF_PAGES = Null.SetNullInteger(dr["NO_OF_PAGES"]) < 0 ? "0" : dr["NO_OF_PAGES"].ToString();
                        book.Author = dr["PREFERRED_NAME"].ToString();
                        book.ATTRIBUTE_TYPE_ID = dr["ATTRIBUTE_TYPE_ID"].ToString();
                        book.ATTRIBUTE_TYPE_VALUE = dr["ATTR_VALUE"].ToString();
                        book.Checked = false;
                        book.ClassName = string.Empty;
                        book.CheckImgPathName = string.Empty;
                        templist.Add(book);
                    }

                    //


                    List<Book> list = new List<Book>();

                    int count = templist.Count;
                    int[] booksList = GetBookListByLevels(subId, fromReadLvl, toReadLvl, LoginName);


                    for (int i = 0; i < booksList.Length; i++)
                    {
                        if (templist.Count > 0)
                        {
                            Book book1 = new Book();
                            book1.PRODUCT_SK = booksList[i];
                            int[] index = new int[templist.Count];
                            int count1 = 0;
                            foreach (Book booklist in templist)
                            {
                                if (booklist.PRODUCT_SK == booksList[i])
                                {
                                    book1.CUST_SUBS_ITEM_SK = booklist.CUST_SUBS_ITEM_SK;
                                    book1.IMAGE_FILE_NAME = booklist.IMAGE_FILE_NAME;
                                    book1.IMAGE_TYPE = booklist.IMAGE_TYPE;
                                    book1.Title = booklist.Title;
                                    book1.COPYRIGHT_YEAR = booklist.COPYRIGHT_YEAR;
                                    book1.NO_OF_PAGES = booklist.NO_OF_PAGES;
                                    book1.Author = booklist.Author;
                                    book1.Checked = booklist.Checked;
                                    book1.ClassName = booklist.ClassName;
                                    book1.CheckImgPathName = booklist.CheckImgPathName;
                                    if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "GUIDED READING LEVEL")
                                    {
                                        book1.ReadingLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                    }
                                    if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "COLOUR LEVEL")
                                    {
                                        book1.ColourLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                    }
                                    if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "READING AGE")
                                    {
                                        book1.ReadingAge = booklist.ATTRIBUTE_TYPE_VALUE;
                                    }
                                    if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "TEXT TYPE")
                                    {
                                        book1.TEXTTYPE = booklist.ATTRIBUTE_TYPE_VALUE;
                                    }
                                    index[count1] = templist.IndexOf(booklist);
                                }

                            }
                            list.Add(book1);
                            while (count1 > 0)
                            {

                                for (int j = count1 - 1; j >= 0; j--)
                                {
                                    templist.RemoveAt(index[j]);
                                    count1--;
                                }
                            }


                        }
                    }

                    books = list;

                        //
                        DataCache.SetCache(cacheKey, list, new TimeSpan(0, 20, 0));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
                finally
                {
                    CBO.CloseDataReader(dr, true);
                }
            }
            return books;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public int IsBooksAdded(int subId, XmlDocument products)
        {
            IDataReader dr = DataProvider.Instance().IsBooksAdded(subId, products);
            int bookCount = 0;
            try
            {
                if (dr.Read())
                {                    
                    bookCount=  Convert.ToInt32(dr["PRODUCTCOUNT"].ToString());
                }
            }
            finally
            {

                CBO.CloseDataReader(dr, true);
            }
            return bookCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Module_Name"></param>
        /// <returns></returns>
        public List<Messages> GetErrorMessagesByModuleName(string Module_Name)
        {
            string cacheKey = string.Format("GetErrorMessagesByModuleName{0}", Module_Name);
            List<Messages> moduleMessages = (List<Messages>)DataCache.GetCache(cacheKey);
            if (moduleMessages == null)
            {
                moduleMessages = CBO.FillCollection<Messages>(DataProvider.Instance().GetErrorMessagesByModuleName(Module_Name));
                if (moduleMessages != null)
                {
                    DataCache.SetCache(cacheKey, moduleMessages);
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return moduleMessages;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <param name="Attribute_type"></param>
        /// <returns></returns>
        public List<string> GetBooksLookUp(int subId, string loginName, string Attribute_type)
        {
            string cacheKey = string.Format("GetBooksLookUp{0}", subId);
            IDataReader dr = DataProvider.Instance().GetBooksCategories(subId, Attribute_type, loginName);
            List<string> list = new List<string>();
            try
            {
                while (dr.Read())
                {
                    string groupName = dr["ATTRIBUTE_TYPE_VALUE"].ToString();
                    list.Add(groupName);
                }
                if (list != null)
                {
                    DataCache.SetCache(cacheKey, list, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="custSubId"></param>
        /// <param name="Attribute_Type"></param>
        public void GetSearchBook(string searchString, int custSubId, string Attribute_Type)
        {
            string cacheKey = string.Format("GetBooksByReadingLevel{0}", custSubId);
            DataCache.RemoveCache(cacheKey);
            List<Book> books = new List<Book>();
            IDataReader dr = DataProvider.Instance().GetSearchGroup(searchString, custSubId, Attribute_Type);
            List<Book> templist = new List<Book>();
            try
            {
                while (dr.Read())
                {

                    Book book = new Book();
                    book.PRODUCT_SK = Convert.ToInt32(dr["PRODUCT_SK"]);
                    book.CUST_SUBS_ITEM_SK = Convert.ToInt32(dr["CUST_SUBS_ITEM_SK"]);
                    book.IMAGE_FILE_NAME = dr["IMAGE_FILE_NAME"].ToString();
                    book.IMAGE_TYPE = dr["IMAGE_TYPE"].ToString();
                    book.Title = dr["Title"].ToString().Replace("eBook:", "");
                    book.COPYRIGHT_YEAR = dr["COPYRIGHT_YEAR"].ToString();
                    book.NO_OF_PAGES = Null.SetNullInteger(dr["NO_OF_PAGES"]) < 0 ? "0" : dr["NO_OF_PAGES"].ToString();
                    book.Author = dr["PREFERRED_NAME"].ToString();
                    book.ATTRIBUTE_TYPE_ID = dr["ATTRIBUTE_TYPE_ID"].ToString();
                    book.ATTRIBUTE_TYPE_VALUE = dr["ATTR_VALUE"].ToString();
                    book.Checked = false;
                    book.ClassName = string.Empty;
                    book.CheckImgPathName = string.Empty;
                    templist.Add(book);
                }

                //


                List<Book> list = new List<Book>();

                int count = templist.Count;
                int[] booksList = GetBookListBySearch(searchString, custSubId, Attribute_Type);


                for (int i = 0; i < booksList.Length; i++)
                {
                    if (templist.Count > 0)
                    {
                        Book book1 = new Book();
                        book1.PRODUCT_SK = booksList[i];
                        int[] index = new int[templist.Count];
                        int count1 = 0;
                        foreach (Book booklist in templist)
                        {
                            if (booklist.PRODUCT_SK == booksList[i])
                            {
                                book1.CUST_SUBS_ITEM_SK = booklist.CUST_SUBS_ITEM_SK;
                                book1.IMAGE_FILE_NAME = booklist.IMAGE_FILE_NAME;
                                book1.IMAGE_TYPE = booklist.IMAGE_TYPE;
                                book1.Title = booklist.Title;
                                book1.COPYRIGHT_YEAR = booklist.COPYRIGHT_YEAR;
                                book1.NO_OF_PAGES = booklist.NO_OF_PAGES;
                                book1.Author = booklist.Author;
                                book1.Checked = booklist.Checked;
                                book1.ClassName = booklist.ClassName;
                                book1.CheckImgPathName = booklist.CheckImgPathName;
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "GUIDED READING LEVEL")
                                {
                                    book1.ReadingLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "COLOUR LEVEL")
                                {
                                    book1.ColourLevel = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "READING AGE")
                                {
                                    book1.ReadingAge = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                if (booklist.ATTRIBUTE_TYPE_ID.ToUpper() == "TEXT TYPE")
                                {
                                    book1.TEXTTYPE = booklist.ATTRIBUTE_TYPE_VALUE;
                                }
                                index[count1] = templist.IndexOf(booklist);
                            }

                        }
                        list.Add(book1);
                        while (count1 > 0)
                        {

                            for (int j = count1 - 1; j >= 0; j--)
                            {
                                templist.RemoveAt(index[j]);
                                count1--;
                            }
                        }


                    }
                }


                books = list;
                if (books != null)
                {
                    DataCache.SetCache(cacheKey, books, TimeSpan.FromMinutes(20));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="fromReadLvl"></param>
        /// <param name="toReadLvl"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public int[] GetBookListByLevels(int subId, int fromReadLvl, int toReadLvl, string LoginName)
        {
            int[] bookList = null;
            string bookslist = "";
            IDataReader dr = DataProvider.Instance().GetBookListByReadingLevel(subId, fromReadLvl, toReadLvl, LoginName);
            try
            {
                while (dr.Read())
                {
                    bookslist = bookslist.Contains(dr["PRODUCT_SK"].ToString()) ? bookslist : bookslist + "/" + dr["PRODUCT_SK"].ToString();
                }
                String[] str = bookslist.Split('/');
                bookList = new int[str.Length - 1];
                for (int i = 1; i < str.Length; i++)
                {
                    bookList[i - 1] = Convert.ToInt32(str[i]);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return bookList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="custSubId"></param>
        /// <param name="Attribute_Type"></param>
        /// <returns></returns>
        public int[] GetBookListBySearch(string searchString, int custSubId, string Attribute_Type)
        {
            int[] bookList = null;
            string bookslist = "";
            IDataReader dr = DataProvider.Instance().GetBookListBySearch(searchString, custSubId, Attribute_Type);
            try
            {
                while (dr.Read())
                {
                    bookslist = bookslist.Contains(dr["PRODUCT_SK"].ToString()) ? bookslist : bookslist + "/" + dr["PRODUCT_SK"].ToString();
                }
                String[] str = bookslist.Split('/');
                bookList = new int[str.Length - 1];
                for (int i = 1; i < str.Length; i++)
                {
                    bookList[i - 1] = Convert.ToInt32(str[i]);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return bookList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="AttributeType"></param>
        /// <param name="AttributeValue"></param>
        /// <returns></returns>
        public int[] GetBookListByAge(int subId, string AttributeType, string AttributeValue)
        {
            int[] bookList = null;
            string bookslist = "";
            IDataReader dr = DataProvider.Instance().GetBookListByReadingAge(subId, AttributeType, AttributeValue);
            try
            {
                while (dr.Read())
                {
                    bookslist = bookslist.Contains(dr["PRODUCT_SK"].ToString()) ? bookslist : bookslist + "/" + dr["PRODUCT_SK"].ToString();
                }
                String[] str = bookslist.Split('/');
                bookList = new int[str.Length - 1];
                for (int i = 1; i < str.Length; i++)
                {
                    bookList[i - 1] = Convert.ToInt32(str[i]);
                }
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return bookList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Values"></param>
        /// <param name="Type"></param>
        /// <param name="SubId"></param>
        /// <returns></returns>
        public int[] GetSelectedBooksCount(string Values, string Type, int SubId)
        {
            string cacheKey = string.Format("Subs-{1}{2}ReadingLevels{0}", Values, SubId,Type);
            var selectedCount = (int[])DataCache.GetCache(cacheKey);
            if (selectedCount==null)
            {
                IDataReader dr=DataProvider.Instance().GetSelectedBooksCount(Values,Type,SubId);
                selectedCount = new int[2];
                while(dr.Read())
                {
                    selectedCount = new int[2];
                    selectedCount[0] = Convert.ToInt32(dr["BooksSelected"].ToString());
                    selectedCount[1] = Convert.ToInt32(dr["AlreadySelected"].ToString());
                }
                
                
                if (selectedCount != null && selectedCount.Length>0)
                {
                    DataCache.SetCache(cacheKey, selectedCount,TimeSpan.FromMinutes(5));
                    if (!CacheKeys.Contains(cacheKey))
                        CacheKeys.Add(cacheKey);
                }
            }
            return selectedCount;
        }
        
        #region EXCEPTION MAIL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="errorMessage"></param>
        /// <param name="SubsSk"></param>
        public void SendExceptionMail(string body, string errorMessage, int SubsSk)
        {
            DataProvider.Instance().SendExceptionMail(body, errorMessage, SubsSk);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="today"></param>
        public void RunScheduleForGracePeriodNotification(DateTime today)
        {
            DataProvider.Instance().RunScheduleForGracePeriodNotification(today);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public int AutoAssignBooks(int subId, string loginName)
        {
            return DataProvider.Instance().AutoAssignBooks(subId,  loginName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <returns></returns>
        public int[] GetBooksCountForPageRedirection(int subId)
        {
            int[] Counts = new int[3];
            IDataReader dr = DataProvider.Instance().GetBooksCountForPageRedirection(subId);
            try
            {
                while (dr.Read())
                {
                    Counts[0] = Null.SetNullInteger(dr["TotalCount"]);
                    Counts[1] = Null.SetNullInteger(dr["SubscriptionQty"]);
                    Counts[2] = Null.SetNullInteger(dr["UsedCount"]);
                }
               
            }
            finally
            {
                CBO.CloseDataReader(dr, true);
            }
            return Counts;
        }

        /// <summary>
        /// Get all book packs
        /// </summary>
        /// <param name="bookPack"></param>
        /// <returns></returns>
        public SqlDataReader GetBookPacks(BookPack bookPack)
        {
            try
            {
                return DataProvider.Instance().GetBookPacks(bookPack);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Get all the eBooks of a book pack
        /// </summary>
        /// <param name="bookPackID"></param>
        /// <returns></returns>
        public SqlDataReader GetBookPackeBooks(int bookPackID, string option)
        {
            try
            {
                return DataProvider.Instance().GetBookPackeBooks(bookPackID, option);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Get all the eBooks of a custom book pack
        /// </summary>
        /// <param name="bookPackID"></param>
        /// <returns></returns>
        public SqlDataReader GetCustomPackeBooks(int CustSubSk, string option)
        {
            try
            {
                return DataProvider.Instance().GetCustomPackeBooks(CustSubSk, option);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Search eBooks of a Book pack across title,Reading level,Reading age,Text type
        /// </summary>
        /// <param name="bookPackID"></param>
        /// <returns></returns>
        public SqlDataReader SearchBookPackeBooks(int bookPackID, string searchText, string conditionText, string weightageText,string option)
        {
            try
            {
                return DataProvider.Instance().SearchBookPackeBooks(bookPackID, searchText, conditionText, weightageText, option);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Search eBooks of a Custom pack across title,Reading level,Reading age,Text type
        /// </summary>
        /// <param name="bookPackID"></param>
        /// <returns></returns>
        public SqlDataReader SearchCustomPackeBooks(int CustSubSk, string searchText, string conditionText, string weightageText, string option)
        {
            try
            {
                return DataProvider.Instance().SearchCustomPackeBooks(CustSubSk, searchText, conditionText, weightageText, option);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Save custom Book Pack
        /// </summary>
        /// <param name="customBookPack"></param>
        /// <returns></returns>
        public int SaveCustomBookPack(BookPack customBookPack)
        {
            try
            {
                return DataProvider.Instance().SaveCustomBookPack(customBookPack);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Set book pack to a subscription
        /// </summary>
        /// <param name="customBookPack"></param>
        /// <returns></returns>
        public int SetBookPack(BookPack customBookPack)
        {
            try
            {
                return DataProvider.Instance().SetBookPack(customBookPack);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Get book pack status
        /// </summary>
        /// <param name="customBookPack"></param>
        /// <returns></returns>
        public int GetBookPackStatus(int custsubsk,string userName)
        {
            try
            {
                return DataProvider.Instance().GetBookPackStatus(custsubsk, userName);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}