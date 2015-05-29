using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.HESearchResults.Data;

namespace DotNetNuke.Modules.HESearchResults.Components.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SearchController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Searchresult controller class
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class SearchController
    {
        /// <summary>
        /// product result getproduct function
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="userListQuoteSK"></param>
        /// <param name="userSK"></param>
        /// <param name="genericFlag"></param>
        /// <returns></returns>
        public List<ListInfo> GetProduct(string flag, string userListQuoteSK, int? userSK, string genericFlag)
        {
            return CBO.FillCollection<ListInfo>(DataProvider.Instance().GetProduct(flag, userListQuoteSK, userSK, genericFlag));
        }

        /// <summary>
        /// product result getquoteproducts function
        /// </summary>
        /// <param name="userListQuoteSK"></param>
        /// <returns></returns>
        public DataSet GetQuoteProducts(int userListQuoteSK)
        {
            return (DataProvider.Instance().GetQuoteProducts(userListQuoteSK));
        }

        /// <summary>
        /// product result editquotedetails function
        /// </summary>
        /// <param name="Quote"></param>
        public void EditQuoteDetails(QuoteInfo Quote)
        {
            DataProvider.Instance().EditQuoteDetails(Quote);
        }

        /// <summary>
        /// product result addlistquote function
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="_userSK"></param>
        /// <param name="_productSKQuantity"></param>
        /// <param name="_userListQuoteSK"></param>
        /// <param name="_genericFlag"></param>
        /// <param name="_output"></param>
        /// <returns></returns>
        public int AddListQuote(string flag, string _userSK, string _productSKQuantity, string _userListQuoteSK, string _genericFlag, ref string _output)
        {
            return (DataProvider.Instance().AddListQuote(flag, _userSK, _productSKQuantity, _userListQuoteSK, _genericFlag, ref _output));
        }


        /// <summary>
        /// product result select function
        /// </summary>
        /// <param name="PrdSk"></param>
        /// <param name="division"></param>
        /// <returns></returns>
        public DataSet Selectsub(int PrdSk, string division)
        {
            return DataProvider.Instance().SelectStdPrd(PrdSk, division);
        }

        /// <summary>
        /// product result selectsearchstud function
        /// </summary>
        /// <param name="PrdSk"></param>
        /// <param name="division"></param>
        /// <param name="store_sk"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public DataSet SelectSearchStudProd(int PrdSk, string division,int store_sk,string country)
        {
            return DataProvider.Instance().SelectSearchStudProd(PrdSk, division, store_sk, country);
        }

        /// <summary>
        /// product result getuserrole function
        /// </summary>
        /// <param name="usersk"></param>
        /// <returns></returns>
        public string GetUserRole(int usersk)
        {
            return (DataProvider.Instance().GetUserRole(usersk));
        }

        /// <summary>
        /// product result getexistingquotes function
        /// </summary>
        /// <param name="userSk"></param>
        /// <param name="Role"></param>
        /// <param name="TradeSK"></param>
        /// <returns></returns>
        public DataSet GetExistingQuotes(int userSk, string Role, int TradeSK)
        {
            return (DataProvider.Instance().GetExistingQuotes(userSk, Role, TradeSK));
        }

        /// <summary>
        /// Cms search result controller select cms function
        /// </summary>
        /// <param name="Division"></param>
        /// <param name="Keywords"></param>
        /// <param name="ItemCount"></param>
        /// <param name="PageNumber"></param>
        /// <param name="Storesk"></param>
        /// <returns></returns>
        public DataSet SelectCms(string Division, string Keywords, int ItemCount, int PageNumber, int Storesk)
        {
            return DataProvider.Instance().SelectCmsResult(Division, Keywords, ItemCount, PageNumber, Storesk);
        }

    }
}