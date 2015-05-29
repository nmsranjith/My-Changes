using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.HESearchResults.Components.Modal;
using DotNetNuke.Modules.HESearchResults.Data;

namespace DotNetNuke.Modules.HESearchResults.Components.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="HESearchResultController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    
    public sealed class HESearchResultController
    {
        public static readonly HESearchResultController studCtrl = new HESearchResultController();

        private HESearchResultController() { }

        public static HESearchResultController Instance
        {
            get { return studCtrl; }
            set { }
        }
        
        private static readonly List<string> CacheKeys = new List<string>();

        /// <summary>
        ///  Get the Product Search results
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public SqlDataReader GetProductResults(SearchParameters sParams)
        {
            try
            {
                return DataProvider.Instance().GetProductResults(sParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Get the Product Search results for Gale
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public SqlDataReader GetGaleProductResults(SearchParameters sParams)
        {
            try
            {
                return DataProvider.Instance().GetGaleProductResults(sParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Get the Advance Search Product results
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public SqlDataReader GetAdvanceSearchProductResults(SearchParameters sParams)
        {
            try
            {
                return DataProvider.Instance().GetAdvanceSearchProductResults(sParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///  Get the Advance Search Product results for Gale
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public SqlDataReader GetGaleAdvanceSearchProductResults(SearchParameters sParams)
        {
            try
            {
                return DataProvider.Instance().GetGaleAdvanceSearchProductResults(sParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Check for the existence of did you mean word for HE,VPG and Gale
        /// </summary>
        /// <param name="word"></param>
        /// <param name="division"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public bool CheckForHEDidYouMeanWordExist(string word, string division, string country)
        {
            try
            {
                return DataProvider.Instance().CheckForHEDidYouMeanWordExist(word, division,country);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Get the CMS Site Search results
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public SqlDataReader GetSiteResults(SearchParameters sParams)
        {
            try
            {
                return DataProvider.Instance().GetCmsResults(sParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Get the Multiple ISBN Advance Search Product results
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public SqlDataReader GetHEAdvanceIsbnResults(SearchParameters sParams)
        {
            try
            {
                return DataProvider.Instance().GetHEAdvanceIsbnResults(sParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///  Get the Multiple ISBN Advance Search Product results for Gale
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public SqlDataReader GetGaleAdvanceIsbnResults(SearchParameters sParams)
        {
            try
            {
                return DataProvider.Instance().GetGaleAdvanceIsbnResults(sParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Saves the user's Favorites and Search inputs
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int SaveSearchAndFavorites(SearchParameters sParams)
        {
            try
            {
                return DataProvider.Instance().SaveSearchAndFavorites(sParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the user's Favorites and Save Search values
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int UpdateSaveSearchAndFavorites(SearchParameters sParams)
        {
            try
            {
                return DataProvider.Instance().UpdateSaveSearchAndFavorites(sParams);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Clear all the Caches
        /// </summary>
        public void ClearAllCache()
        {
            foreach (string cacheKey in CacheKeys)
                DataCache.RemoveCache(cacheKey);
        }     
    }
}