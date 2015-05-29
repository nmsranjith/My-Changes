using System;
using System.Data.SqlClient;
using DotNetNuke.Modules.FeaturedSearch.Components.Modal;
using DotNetNuke.Modules.FeaturedSearch.Data;

namespace DotNetNuke.Modules.FeaturedSearch.Components.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="FeaturedSearchController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class FeaturedSearchController
    {
        public static readonly FeaturedSearchController studCtrl = new FeaturedSearchController();

        private FeaturedSearchController() { }

        public static FeaturedSearchController Instance
        {
            get { return studCtrl; }
            set { }
        }
 
        /// <summary>
        ///   Get all the saved featured searches
        /// </summary>
        /// <returns></returns>
        public SqlDataReader GetAllFeaturedSearches(FeaturedSearches featuredSearchItem)
        {
            try
            {
                return DataProvider.Instance().GetAllFeaturedSearches(featuredSearchItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get a featured search item
        /// </summary>
        /// <param name="featuredSearchItem"></param>
        /// <returns></returns>
        public SqlDataReader GetFeaturedSearchForEdit(FeaturedSearches featuredSearchItem)
        {
            try
            {
                return DataProvider.Instance().GetFeaturedSearchForEdit(featuredSearchItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Save a featured search
        /// </summary>
        /// <param name="featuredSearchItem"></param>
        /// <returns></returns>
        public int SaveFeaturedSearch(FeaturedSearches featuredSearchItem)
        {
            try
            {
                return DataProvider.Instance().SaveFeaturedSearch(featuredSearchItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete a featured search item
        /// </summary>
        /// <param name="featuredSearchItem"></param>
        /// <returns></returns>
        public int DeleteFeaturedSearch(FeaturedSearches featuredSearchItem)
        {
            try
            {
                return DataProvider.Instance().DeleteFeaturedSearch(featuredSearchItem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}