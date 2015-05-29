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
using DotNetNuke.Modules.eCollection_Groups.Components;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;
using System.Xml;

namespace DotNetNuke.Modules.eCollection_Groups.Data
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
                const string assembly = "DotNetNuke.Modules.eCollection_Groups.Data.SqlDataprovider,eCollection_Groups";
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
        public abstract IDataReader GetGroupList(char groupType, string userLoginName,int CustSubID);
        public abstract IDataReader GetClassList(char ClassType, string userLoginName, int CustSubID);
        public abstract IDataReader GetClassListBySubscription(string CustomerSubSK, string userLoginName);
        public abstract IDataReader GetGroupName(char grouptype, string userLoginName);
        public abstract IDataReader GetGroupsByGroupID(int groupId, char groupType, string userLoginName);
        public abstract IDataReader GetStudentByGroup(int groupId);
        public abstract IDataReader GetTeachersByGroup(int groupId);
        public abstract IDataReader GetMembersByGroup(int groupId, string role);
        public abstract IDataReader GetGroupProfileByGroup(int groupId);
        public abstract IDataReader GetReadingGraphByGroup(int groupId);
        public abstract IDataReader GetReadingHistoryByGroup(int groupId, string month);
        public abstract IDataReader GetMinMaxReadingLevelByDate(int groupId, DateTime grfRefDate);
        public abstract IDataReader GetReadingSessionByGroup(int groupId, string month);
        public abstract DataSet GetReadingSessionByGroupAndSectionCat(int groupId, DateTime refDate);
        public abstract void UpdateMembers(Components.Groups groups, List<IDCollection> Teachercollection, List<Students> StudentCollection, int customerSubSK);
        public abstract IDataReader GetRecordingHistoryByGroup(int groupId, int groupMemberId, int productId, string month);
        public abstract IDataReader GetSubscription(string userLoginName);
        public abstract IDataReader GetStudentsBySubcription(int subsId, string userLoginName);
        public abstract IDataReader GetTeachersbySubscription(int subsId, string userLoginName);
        public abstract IDataReader GetReadingLevel(int groupId);
        public abstract int AddGroup(DotNetNuke.Modules.eCollection_Groups.Components.Groups a);
        public abstract void DeleteGroup(Components.Groups group, List<string> groupId);
        public abstract bool ValidateGroupName(string groupName, char groupType, int customerSubId);
        public abstract IDataReader GetColorByReadLevel(int minReadLevel, int maxReadLevel);
        public abstract IDataReader GetWordsByGroup(int groupID);
        public abstract IDataReader GetMonthByGroup(int groupId, string type);
        public abstract IDataReader GetLastSevenDaysReadings(int groupId, DateTime fromDate, DateTime toDate);
        public abstract IDataReader GetLastSevenDaysRecordings(int groupId, int groupMemberId,int productID, DateTime fromDate, DateTime toDate);
        public abstract DataTable GetGroupNameByGroupId(int groupId, string role);
        public abstract IDataReader GetReadingLevelByGroup(int groupId);
        public abstract IDataReader GetBookCount(int groupId, int currentReadLevel);
        public abstract int AddIsbnDetails(XmlDocument stagingXmlDoc,int countCheckReq, string server);
        public abstract IDataReader GetBooksDetails();
        public abstract IDataReader GetSearchBooksDetails(string searchText);
        public abstract int AddBooksInPublish(XmlDocument publishingXmlDoc);
        public abstract int DeleteBooksInStaging(XmlDocument deleteXmlDoc);
        public abstract int CheckISBN(XmlDocument isbnXmlDoc);
        public abstract IDataReader GetIsbnDetails(XmlDocument isbnXmlDoc);
        public abstract IDataReader GetErrorMessagesByModuleName(string Module_Name);
        public abstract IDataReader GetReadingSessionWrapper(int groupId, string month, int sessionId);
        public abstract IDataReader GetReadingSessionWrapperBySectionCat(int groupId, string sectionCat, int sessionId);
        public abstract int UpdateIsClearFromStatus(int groupId, DateTime? currentDate, DateTime? fromDate, DateTime? todate, string month, string updateStatus);
        //search

        public abstract IDataReader GetSearchGroup(string searchString, string userLoginName, int custSubId);
        public abstract IDataReader GetSearchStudentDetails(string searchString, string userLoginName, int custSubId);
        public abstract IDataReader GetGroupName(string userLoginName, int custSubId);
        public abstract IDataReader GetSearchTeacherDetails(string searchString, string userLoginName, int custSubId);
        public abstract IDataReader GetSearchStudentByGroup(string searchString, int groupId);

        public abstract int GetGroupOwnerID(int groupId);

        //temp
        public abstract IDataReader GetLoginDetails(string loginName, int custSubId);

        #region Exception Mail
        public abstract void SendExceptionMail(string body, string errorMessage, int SubsSk);
        #endregion
        #endregion


        public abstract bool InsertNewePubBooks(DataTable newXmlIsbnsDt);

        public abstract string GetProductAndAuthorIds();
        
    }

}