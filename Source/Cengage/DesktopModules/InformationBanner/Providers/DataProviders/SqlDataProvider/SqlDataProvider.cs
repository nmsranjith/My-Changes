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
using DotNetNuke.Modules.InformationBanner.Components.Common;

namespace DotNetNuke.Modules.InformationBanner.Data
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
        private const string ModuleQualifier = "InformationBanner_";

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
        /// <summary>
        /// Get All banner messages
        /// </summary>
        /// <param name="val"></param>
        /// <param name="currentDate"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override SqlDataReader GetAlerts(string val, string currentDate, string userLoginName)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GetAlerts, new SqlParameter("@INDEX", val), new SqlParameter("@CURRENTDATE", currentDate),new SqlParameter("@USER_LOGIN_NAME", userLoginName));
            }
            catch(Exception ex){ throw ex;}
        }

        /// <summary>
        /// Add a new banner message
        /// </summary>
        /// <param name="usersk"></param>
        /// <param name="ErrorMsg"></param>
        /// <param name="ErrorType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public override int AddAlerts(int usersk, string ErrorMsg, string ErrorType, string StartDate, string EndDate)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_AddAlerts, new SqlParameter("@USERSK", usersk), new SqlParameter("@ERROR_MESSAGE", ErrorMsg), new SqlParameter("@ERRORTYPE", ErrorType), new SqlParameter("@STARTdATE", StartDate), new SqlParameter("@ENDDATE", EndDate));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Delete a banner message
        /// </summary>
        /// <param name="AlertID"></param>
        /// <returns></returns>
        public override int DeleteAlerts(int AlertID)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_DeleteAlerts, new SqlParameter("@ALERTID", AlertID));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Update a banner message
        /// </summary>
        /// <param name="AlertID"></param>
        /// <param name="ErrorMsg"></param>
        /// <param name="ErrorType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public override int UpdateAlerts(int AlertID, string ErrorMsg, string ErrorType, string StartDate, string EndDate)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_UpdateAlerts, new SqlParameter("@ALERTID", AlertID), new SqlParameter("@ERROR_MESSAGE", ErrorMsg), new SqlParameter("@ERRORTYPE", ErrorType), new SqlParameter("@STARTdATE", StartDate), new SqlParameter("@ENDDATE", EndDate));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Delete a banner message for the paritcular user
        /// </summary>
        /// <param name="AlertID"></param>
        /// <param name="userLoginName"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public override int DeleteUserInfoID(int AlertID, string userLoginName, string pageUrl)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_UserDeleteAlerts, new SqlParameter("@ALERTID", AlertID), new SqlParameter("@UserLoginName", userLoginName), new SqlParameter("@PageUrl", pageUrl));
            }
            catch (Exception ex) { throw ex; }
        }
    }

}