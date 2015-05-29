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

using System.Data;
using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using DotNetNuke.Modules.eCollection_Sessions.Components;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using System.Collections.Generic;


namespace DotNetNuke.Modules.eCollection_Sessions.Data
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// An abstract class for the data access layer
    /// 
    /// The abstract data provider provides the methods that a control data provider (sqldataprovider)
    /// must implement. You'll find two commented out examples in the Abstract methods region below.
    /// </summary>
    /// -----------------------------------------------------------------------------
    public abstract class DataProvider
    {

        #region Shared/Static Methods

        private static DataProvider provider;

        // return the provider
        public static DataProvider Instance()
        {
            if (provider == null)
            {
                const string assembly = "DotNetNuke.Modules.eCollection_Sessions.Data.SqlDataprovider,eCollection_Sessions";
                Type objectType = Type.GetType(assembly, true, true);

                provider = (DataProvider)Activator.CreateInstance(objectType);
                DataCache.SetCache(objectType.FullName, provider);
            }

            return provider;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Not returning class state information")]
        public static IDbConnection GetConnection()
        {
            const string providerType = "data";
            ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(providerType);

            Provider objProvider = ((Provider)_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);
            string _connectionString;
            if (!String.IsNullOrEmpty(objProvider.Attributes["connectionStringName"]) && !String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]]))
            {
                _connectionString = System.Configuration.ConfigurationManager.AppSettings[objProvider.Attributes["connectionStringName"]];
            }
            else
            {
                _connectionString = objProvider.Attributes["connectionString"];
            }

            IDbConnection newConnection = new System.Data.SqlClient.SqlConnection();
            newConnection.ConnectionString = _connectionString.ToString();
            newConnection.Open();
            return newConnection;
        }
                
        #endregion

        #region Abstract methods

        //public abstract IDataReader GetItems(int userId, int portalId);

        //public abstract IDataReader GetItem(int itemId);        

        public abstract IDataReader GetSessions(int sessionId);        
        public abstract IDataReader GetAllSessions(int userId,string loginName,int SubId);
        public abstract IDataReader GetSubscription(int userId, string loginName);
        public abstract IDataReader GetTeachers(int subId, string loginName);
        public abstract IDataReader GetAllSessions(int portalId, bool sortAsc);
        public abstract int AddSession(Sessions a);
        public abstract int UpdateSession(Sessions a);
        public abstract void DeleteSession(int SessionId, string loginName);
        public abstract void DeleteSessions(int moduleId);
        public abstract IDataReader GetGroups(MyEnums.GroupType groupType, string loginName, int subId);
        public abstract IDataReader GetTeacherName(int groupId);
        public abstract IDataReader GetBooks(int subId);
        public abstract IDataReader GetStudentsList(int subId,string loginName);
        public abstract int UpdateSessionExpiryDate(List<IDCollection> IDCollectionLists, string userLoginName);
        public abstract int UpdateSessionExpiryDateAlone(int sessionId, DateTime SessionExpiryDate, string userLoginName);
        public abstract int DeleteSession(List<IDCollection> IDCollectionLists,string loginName);
        public abstract IDataReader GetSessionOpenedStudents(int sessionId);
        public abstract IDataReader GetSessionUnOpenedStudents(int sessionId);
        public abstract IDataReader GetSessionMembers(int sessionId);
        public abstract IDataReader GetSessionProducts(int sessionId);
        public abstract IDataReader GetBookListBySearch(string searchString, int custSubId, string Attribute_Type);
        public abstract IDataReader GetReadingHistory(int sessionId,string MonthName);
        public abstract IDataReader GetRecordingHistory(int sessionId, string month, int studentId, int sessHistoryId);
        public abstract IDataReader GetReadingHistoryLastSeven(int sessionId);
        public abstract IDataReader GetRecordingHistoryLastSeven(int sessionId, int studentId, int sessHistoryId);
        public abstract IDataReader GetSessionHistoryBySessionMonthNames(int sessionId);
        public abstract IDataReader GetReadingLevel(int groupId);
        public abstract IDataReader GetStudentByGroup(int groupId);
        public abstract IDataReader GetClassListBySubscription(string CustomerSubSK, string userLoginName);
        public abstract IDataReader GetGroupsByGroupID(int groupId, char groupType, string userLoginName);
        public abstract IDataReader GetgroupListBySubscription(string CustomerSubSK, string userLoginName);
        //added by kalai
        public abstract IDataReader GetBooksByReadingLevel(int subId, int fromReadLvl, int toReadLvl, string LoginName);
        public abstract IDataReader GetErrorMessagesByModuleName(string Module_Name);

        public abstract IDataReader GetAllLookUpNames(int userId, string loginName, int SubId);
        public abstract IDataReader GetNamesForSearch(string searchString, int userId, string loginName, int SubId);

        public abstract IDataReader GetStudentLookupNames(int subsId, string userLoginName);
        public abstract IDataReader GetSearchStudentDetails(string searchString, string userLoginName, int custSubId);

        public abstract IDataReader GetSearchGroup(string searchString, string userLoginName, int custSubId);
        public abstract IDataReader GetGroupName(string userLoginName, int custSubId);
        public abstract IDataReader GetBookListByReadingLevel(int subId, int fromReadLvl, int toReadLvl, string LoginName);

        public abstract IDataReader GetSearchTeachers(string searchString, string userLoginName, int custSubId);

        public abstract IDataReader GetBooksCategories(int subsId,string Attribute_Type,  string userLoginName);

        public abstract IDataReader GetSearchGroup(string searchString, int custSubId, string Attribute_Type);

        #region Exception Mail
        public abstract void SendExceptionMail(string body, string errorMessage, int SubsSk);
        #endregion
        #endregion

    }

}