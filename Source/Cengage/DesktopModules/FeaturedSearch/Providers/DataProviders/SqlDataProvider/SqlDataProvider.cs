/*
' Copyright (c) 2012 DotNetNuke Corporation
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using DotNetNuke.Modules.FeaturedSearch.Components.Modal;
using Microsoft.ApplicationBlocks.Data;

namespace DotNetNuke.Modules.FeaturedSearch.Data
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract DataProvider class
    /// 
    /// This concreted data provider class provides the implementation of the abstract methods 
    /// from data dataprovider.cs
    /// 
    /// In most cases you will only modify the Public methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider : DataProvider
    {

        #region Private Members

        private const string ProviderType = "data";
        private const string ModuleQualifier = "FeaturedSearch_";

        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;
        private readonly string _providerPath;
        private readonly string _objectQualifier;
        private readonly string _databaseOwner;

        #endregion

        #region Constructors

        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider
            Provider objProvider = (Provider)(_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);

            // Read the attributes for this provider

            //Get Connection string from web.config
            _connectionString = Config.GetConnectionString();

            if (string.IsNullOrEmpty(_connectionString))
            {
                // Use connection string specified in provider
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(_objectQualifier) && _objectQualifier.EndsWith("_", StringComparison.Ordinal) == false)
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(_databaseOwner) && _databaseOwner.EndsWith(".", StringComparison.Ordinal) == false)
            {
                _databaseOwner += ".";
            }

        }

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string ProviderPath
        {
            get
            {
                return _providerPath;
            }
        }

        public string ObjectQualifier
        {
            get
            {
                return _objectQualifier;
            }
        }

        public string DatabaseOwner
        {
            get
            {
                return _databaseOwner;
            }
        }

        private string NamePrefix
        {
            get { return DatabaseOwner + ObjectQualifier + ModuleQualifier; }
        }

        #endregion

        #region Private Methods

        private static object GetNull(object Field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value);
        }

        #endregion

        #region Public Methods

        //public override IDataReader GetItem(int itemId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItem", itemId);
        //}

        //public override IDataReader GetItems(int userId, int portalId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItemsForUser", userId, portalId);
        //}

        /// <summary>
        /// Get all saved featured search items
        /// </summary>
        /// <returns></returns>
        public override SqlDataReader GetAllFeaturedSearches(FeaturedSearches featuredSearchItem)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, "ECOMM_GALE_ALL_FEATURED_SEARCHES_GET"
                    , new SqlParameter("@ModuleId", featuredSearchItem.ModuleId));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Get a featured search item
        /// </summary>
        /// <param name="featuredSearchItem"></param>
        /// <returns></returns>
        public override SqlDataReader GetFeaturedSearchForEdit(FeaturedSearches featuredSearchItem)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, "ECOMM_GALE_FEATURED_SEARCH_GET"
                    , new SqlParameter("@FeaturedSearchSk", featuredSearchItem.FeaturedSearchSk));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Save a featured search
        /// </summary>
        /// <param name="featuredSearchItem"></param>
        /// <returns></returns>
        public override int SaveFeaturedSearch(FeaturedSearches featuredSearchItem)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(ConnectionString, "ECOMM_GALE_FEATURED_SEARCH_SAVE"
                    , new SqlParameter("@FeaturedSearchSk", featuredSearchItem.FeaturedSearchSk)
                    , new SqlParameter("@SearchName", featuredSearchItem.SearchName)
                    , new SqlParameter("@CurrentUrl", featuredSearchItem.CurrentUrl)
                    , new SqlParameter("@Title", featuredSearchItem.Title)
                    , new SqlParameter("@Author", featuredSearchItem.Author)
                    , new SqlParameter("@Subject", featuredSearchItem.Subject)
                    , new SqlParameter("@Format", featuredSearchItem.Format)
                    , new SqlParameter("@LibraryType", featuredSearchItem.LibraryType)
                    , new SqlParameter("@Origin", featuredSearchItem.Origin)
                    , new SqlParameter("@PublishedYear", featuredSearchItem.PublishedYear)
                    , new SqlParameter("@Publisher", featuredSearchItem.Publisher)
                    , new SqlParameter("@EbookPlatform", featuredSearchItem.EbookPlatform)
                    , new SqlParameter("@AllWords", featuredSearchItem.AllWords)
                    , new SqlParameter("@ExactPhrase", featuredSearchItem.ExactPhrase)
                    , new SqlParameter("@NoneOfThese", featuredSearchItem.NoneOfThese)
                    , new SqlParameter("@ModuleId", featuredSearchItem.ModuleId));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Delete a featured search item
        /// </summary>
        /// <param name="featuredSearchItem"></param>
        /// <returns></returns>
        public override int DeleteFeaturedSearch(FeaturedSearches featuredSearchItem)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(ConnectionString, "ECOMM_GALE_FEATURED_SEARCH_DELETE"
                    , new SqlParameter("@FeaturedSearchSk", featuredSearchItem.FeaturedSearchSk));
            }
            catch (Exception ex) { throw ex; }
        }

        #endregion

    }

}