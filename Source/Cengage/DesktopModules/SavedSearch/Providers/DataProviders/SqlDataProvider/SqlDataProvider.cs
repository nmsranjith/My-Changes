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
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Modules.SavedSearch.Components.Common;
using System.Configuration;
using System.Data;

namespace DotNetNuke.Modules.SavedSearch.Data
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
        private const string ModuleQualifier = "SavedSearch_";

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


        #endregion

        public override SqlDataReader GetSavedSearchName(string count,int usersk,string PageName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GetSavedName, new SqlParameter("@Count", count), new SqlParameter("@UserSK", usersk), new SqlParameter("@Division", PageName));
        }
        public override int DeleteSavedSearchName(int PROD_SAV_FAV_SK)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_DeleteSavedName, new SqlParameter("@PROD_SAV_FAV_SK", PROD_SAV_FAV_SK));
        }
        /// <summary>
        /// getting module information 
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns>IDataReader</returns>
        public override SqlDataReader GetModuleInformation(int moduleID)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "GET_COMERCE_CMS_HTMLTABLE", new SqlParameter("@MODULEID", moduleID));
        }
        /// <summary>
        /// saving module information
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="content"></param>
        /// <param name="Usersk"></param>
        /// <returns>bool</returns>
        public override bool InsertModuleInformation(int moduleID, string content, int Usersk)
        {
            int result = SqlHelper.ExecuteNonQuery(ConnectionString, "INSERT_COMERCE_CMS_HTMLTABLE", new SqlParameter("@CONTENT", content),
                new SqlParameter("@MODULEID", moduleID),
                new SqlParameter("@USERSK", Usersk));
            return result > 0 ? true : false;
        }
        /// <summary>
        /// update the module information
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="content"></param>
        /// <param name="Usersk"></param>
        /// <returns>bool</returns>
        public override bool UpdateModuleInformation(int moduleID, string content, int Usersk)
        {
            int result = SqlHelper.ExecuteNonQuery(ConnectionString, "UPDATE_COMERCE_CMS_HTMLTABLE", new SqlParameter("@CONTENT", content),
                new SqlParameter("@MODULEID", moduleID),
                new SqlParameter("@USERSK", Usersk));
            return result > 0 ? true : false;
        }
    }

}