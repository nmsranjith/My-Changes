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
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using DotNetNuke.Modules.eCollection_Sessions.Components;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using System.Linq;
using DotNetNuke.Modules.eCollection_Sessions.Components.ExceptionHandling;
using System.Collections.Generic;
using System.Configuration;

namespace DotNetNuke.Modules.eCollection_Sessions.Data
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
        private const string ModuleQualifier = "eCollection_Sessions_";

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public override int AddSession(Sessions a)
        {
            SqlParameter returnValue = new SqlParameter("returnValue", System.Data.SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;

            a.SessionId =  Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDSESSION
                , new SqlParameter("@SessionSk", a.SessionId)
                , new SqlParameter("@CUST_SUBS_SK", a.CUST_SUBS_SK)
                , new SqlParameter("@CREATED_BY_CUST_SUBS_USER_SK", a.CREATED_BY_CUST_SUBS_USER_SK.GetValueOrDefault())
                , new SqlParameter("@ENDED_BY_CUST_SUBS_USER_SK", a.ENDED_BY_CUST_SUBS_USER_SK.GetValueOrDefault())
                , new SqlParameter("@SessionName", a.Name)
                , new SqlParameter("@WorkType", a.WorkType)
                , new SqlParameter("@SessionType", a.SessionType.ToString())
                , new SqlParameter("@SessionCreatedDate", a.SessionCreatedDate)
                , new SqlParameter("@SessionExpiryDate", a.SessionExpiryDate)
                , new SqlParameter("@SessionStatus", a.SessionStatus)
                , new SqlParameter("@IsNameOverride", a.IsNameManualOverride)
                , new SqlParameter("@Notes", a.Notes)
                , new SqlParameter("@Active", a.IsActive)
                , new SqlParameter("@UserCreated", a.CreatedByUserName)
                , new SqlParameter("@UserModified", a.LastModifiedByUserID)
                , returnValue   
                ));

            Int32 rowsaffected = Convert.ToInt32(returnValue.Value);
            if (rowsaffected == -1)
            {
                throw new SessionValidationException("Session name already exists. Please specify a unique session name", MyEnums.CrudState.Insert);
            }

            foreach (SessionMembers sessions in a.SessionMembers)
            {
                SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDSESSIONMEMBERS
                      , new SqlParameter("@SESSION_MEMBER_SK", sessions.SESSION_MEMBER_SK)
                      , new SqlParameter("@SESSION_SK", a.SessionId)
                      , new SqlParameter("@CUST_SUBS_USER_SK", sessions.CUST_SUBS_USER_SK)
                      , new SqlParameter("@GRP_MEM_SK", sessions.GRP_MEM_SK)
                      , new SqlParameter("@ADDED_BY_CUST_SUBS_USER_SK", sessions.ADDED_BY_CUST_SUBS_USER_SK)
                      , new SqlParameter("@ADDEDDATE", sessions.Added_date)
                      , new SqlParameter("@MEMBERTYPE", sessions.MemberType)
                      , new SqlParameter("@Active", sessions.Active)
                      , new SqlParameter("@UserCreated", a.CreatedByUserName)
                      , new SqlParameter("@UserModified", a.LastModifiedByUserID)
                      );
                SqlParameter A = new SqlParameter();                
            }

            foreach (SessionProducts sessions in a.SessionProducts)
            {
                SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDSESSIONPRODUCTS
                  , new SqlParameter("@SESSION_PRODUCT_SK", sessions.SESSION_PRODUCT_SK)
                  , new SqlParameter("@SESSION_SK", a.SessionId)
                  , new SqlParameter("@CUST_SUBS_ITEM_SK", sessions.CUST_SUBS_ITEM_SK)
                  , new SqlParameter("@ADDED_BY_CUST_SUBS_USER_SK", sessions.ADDED_BY_CUST_SUBS_USER_SK)
                  , new SqlParameter("@ADDEDDATE", sessions.Books_AddedDate)
                  , new SqlParameter("@Active", sessions.Active)
                  , new SqlParameter("@UserCreated", a.CreatedByUserName)
                  , new SqlParameter("@UserModified", a.LastModifiedByUserID)
                  );
            }

            return (int)a.SessionId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sbc_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            //MessageBox.Show("Number of records affected : " + e.RowsCopied.ToString());    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public override IDataReader GetSessions(int sessionId)
        {
            return SqlHelper.ExecuteReader(ConnectionString,Constants.SP_GETSESSIONLIST, new SqlParameter("@SessionId", sessionId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <param name="SubId"></param>
        /// <returns></returns>
        public override IDataReader GetAllSessions(int userId, string loginName,int SubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETSESSIONLIST, new SqlParameter("@UserId", userId), new SqlParameter("@LoginName", loginName), new SqlParameter("@SubId", SubId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portalId"></param>
        /// <param name="sortAsc"></param>
        /// <returns></returns>
        public override IDataReader GetAllSessions(int portalId, bool sortAsc)
        {
            //return SqlHelper.ExecuteReader(ConnectionString,Constants.SP_GETALLSESSION, new SqlParameter("@PortalId", portalId), new SqlParameter("@sortAsc", sortAsc));
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public override int UpdateSession(Sessions a)
        {
            SqlParameter returnValue = new SqlParameter("returnValue", System.Data.SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;

             Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDSESSION
                , new SqlParameter("@SessionSk", a.SessionId)
                , new SqlParameter("@CUST_SUBS_SK", a.CUST_SUBS_SK)
                , new SqlParameter("@CREATED_BY_CUST_SUBS_USER_SK", a.CREATED_BY_CUST_SUBS_USER_SK.GetValueOrDefault())
                , new SqlParameter("@ENDED_BY_CUST_SUBS_USER_SK", a.ENDED_BY_CUST_SUBS_USER_SK.GetValueOrDefault())
                , new SqlParameter("@SessionName", a.Name)
                , new SqlParameter("@WorkType", a.WorkType)
                , new SqlParameter("@SessionType", a.SessionType.ToString())
                , new SqlParameter("@SessionCreatedDate", a.SessionCreatedDate)
                , new SqlParameter("@SessionExpiryDate", a.SessionExpiryDate)
                , new SqlParameter("@SessionStatus", a.SessionStatus)
                , new SqlParameter("@IsNameOverride", a.IsNameManualOverride)
                , new SqlParameter("@Notes", a.Notes)
                , new SqlParameter("@Active", a.IsActive)
                , new SqlParameter("@UserCreated", a.CreatedByUserName)
                , new SqlParameter("@UserModified", a.LastModifiedByUserID)
                , returnValue
                ));

             Int32 rowsaffected = Convert.ToInt32(returnValue.Value);
             if (rowsaffected == -1)
             {
                 throw new SessionValidationException("Session name already exists. Please specify a unique session name", MyEnums.CrudState.Insert);
             }

            foreach (SessionMembers sessions in a.SessionMembers)
            {
               SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_UPDATESESSIONMEMBERS
                    , new SqlParameter("@SESSION_MEMBER_SK", sessions.SESSION_MEMBER_SK)
                    , new SqlParameter("@SESSION_SK", a.SessionId)
                    , new SqlParameter("@CUST_SUBS_USER_SK", sessions.CUST_SUBS_USER_SK)
                    , new SqlParameter("@GRP_MEM_SK", sessions.GRP_MEM_SK)
                    , new SqlParameter("@ADDED_BY_CUST_SUBS_USER_SK", sessions.ADDED_BY_CUST_SUBS_USER_SK)
                    , new SqlParameter("@ADDEDDATE", sessions.Added_date)
                    , new SqlParameter("@MEMBERTYPE", sessions.MemberType)
                    , new SqlParameter("@Active", sessions.Active)
                    , new SqlParameter("@UserCreated", a.CreatedByUserName)
                    , new SqlParameter("@UserModified", a.LastModifiedByUserID)
                    );
                SqlParameter A = new SqlParameter();
            }

            foreach (SessionProducts sessions in a.SessionProducts)
            {
                SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDSESSIONPRODUCTS
                  , new SqlParameter("@SESSION_PRODUCT_SK", sessions.SESSION_PRODUCT_SK)
                  , new SqlParameter("@SESSION_SK", a.SessionId)
                  , new SqlParameter("@CUST_SUBS_ITEM_SK", sessions.CUST_SUBS_ITEM_SK)
                  , new SqlParameter("@ADDED_BY_CUST_SUBS_USER_SK", sessions.ADDED_BY_CUST_SUBS_USER_SK)
                  , new SqlParameter("@ADDEDDATE", sessions.Books_AddedDate)
                  , new SqlParameter("@Active", sessions.Active)
                  , new SqlParameter("@UserCreated", a.CreatedByUserName)
                  , new SqlParameter("@UserModified", a.LastModifiedByUserID)
                  );
            }
            return (int)a.SessionId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="loginName"></param>
        public override void DeleteSession(int sessionId, string loginName)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, Constants.SP_DELETESESSION, new SqlParameter("@SessionId", sessionId), new SqlParameter("@LoginName", loginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="fromReadLvl"></param>
        /// <param name="toReadLvl"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public override IDataReader GetBookListByReadingLevel(int subId, int fromReadLvl, int toReadLvl, string LoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKLISTBYREADLEVEL, new SqlParameter("@CUST_SUBS_SK", subId), new SqlParameter("@FromReadingLevel", fromReadLvl), new SqlParameter("@ToReadingLevel", toReadLvl), new SqlParameter("@UserCreated", LoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        public override void DeleteSessions(int moduleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, Constants.SP_DELETESESSION, new SqlParameter("@ModuleId", moduleId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override IDataReader GetSubscription(int userId, string loginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETSUBSCRIPTION, new SqlParameter("@UserId", userId), new SqlParameter("@LoginName", loginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override IDataReader GetTeachers(int subId, string loginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETTEACHERSLIST, new SqlParameter("@SubId", subId), new SqlParameter("@LoginName", loginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="loginName"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        public override IDataReader GetGroups(MyEnums.GroupType groupType, string loginName, int subId)
        {
            //return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETGROUPSLIST, new SqlParameter("@GroupType", groupType), new SqlParameter("@UserLoginName", loginName), new SqlParameter("@SubsSK", subId));
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETGROUPSLIST
                    , new SqlParameter("@GroupType", groupType)
                    , new SqlParameter("@LoginName", loginName)
                    , new SqlParameter("@SubsSK", subId)
                    , new SqlParameter("@Type", "FORSUBS")
                    );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IDataReader GetTeacherName(int groupId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETTEACHERNAME, new SqlParameter("@GroupId", groupId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <returns></returns>
        public override IDataReader GetBooks(int subId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKS, new SqlParameter("@CUST_SUBS_SK", subId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override IDataReader GetStudentsList(int subId,string loginName)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETSTUDENTSLIST
                       , new SqlParameter("@SubId", subId)
                       , new SqlParameter("@UserLoginName", loginName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDCollectionLists"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public override int UpdateSessionExpiryDate(List<IDCollection> IDCollectionLists,string LoginName)
        {
            foreach (IDCollection IDCollectionList in IDCollectionLists)
            {
                try
                {
                    if (IDCollectionList.Id != 0)
                    {
                        Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_UPDATEEXPIRYDATE
                       , new SqlParameter("@SESSION_SK", IDCollectionList.Id)
                       , new SqlParameter("@SessionExpiryDate", DateTime.Now)
                       , new SqlParameter("@UserCreated", LoginName)
                       ));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IDCollectionLists"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public override int DeleteSession(List<IDCollection> IDCollectionLists,string LoginName)
        {
            foreach (IDCollection IDCollectionList in IDCollectionLists)
            {
                try
                {
                    if (IDCollectionList.Id != 0)
                    {
                        Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_DELETESESSION
                       , new SqlParameter("@SESSION_SK", IDCollectionList.Id)
                      , new SqlParameter("@loginName", LoginName)
                       ));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public override IDataReader GetSessionOpenedStudents(int sessionId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_SESSIONOPENEDSTUDENTS, new SqlParameter("@sessionSk", sessionId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public override IDataReader GetSessionUnOpenedStudents(int sessionId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_SESSIONUNOPENEDSTUDENTS, new SqlParameter("@sessionSk", sessionId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public override IDataReader GetSessionMembers(int sessionId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_SESSIONMEMBERSGET, new SqlParameter("@sessionSk", sessionId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public override IDataReader GetSessionProducts(int sessionId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_SESSIONPRODUCTGET, new SqlParameter("@sessionSk", sessionId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="custSubId"></param>
        /// <param name="Attribute_Type"></param>
        /// <returns></returns>
        public override IDataReader GetBookListBySearch(string searchString, int custSubId, string Attribute_Type)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKLISTBYSEARCH, new SqlParameter("@SearchString", searchString), new SqlParameter("@CUST_SUBS_SK", custSubId), new SqlParameter("@Attribute_type", Attribute_Type));
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="SessionExpiryDate"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override int UpdateSessionExpiryDateAlone(int sessionId, DateTime SessionExpiryDate, string userLoginName)
        {            
            try
            {
                if (sessionId != 0)
                {
                    Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_UPDATEEXPIRYDATEALONE
                    , new SqlParameter("@SESSION_SK", sessionId)
                    , new SqlParameter("@SessionExpiryDate", SessionExpiryDate)
                    , new SqlParameter("@DateModified", DateTime.Now)
                    , new SqlParameter("@UserCreated", userLoginName)


                    ));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="MonthName"></param>
        /// <returns></returns>
        public override IDataReader GetReadingHistory(int sessionId,string MonthName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETREADINGHISTORY, new SqlParameter("@SessionSK", sessionId), new SqlParameter("@Month", MonthName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="month"></param>
        /// <param name="studentId"></param>
        /// <param name="sessHistoryId"></param>
        /// <returns></returns>
        public override IDataReader GetRecordingHistory(int sessionId, string month, int studentId, int sessHistoryId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETRECORDINGHISTORYGET, new SqlParameter("@SessionSK", sessionId), new SqlParameter("@Month", month), new SqlParameter("@CUST_SUBS_USER_SK", studentId), new SqlParameter("@SessionHistoryId", sessHistoryId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public override IDataReader GetReadingHistoryLastSeven(int sessionId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETREADINGHISTORYLASTSEVEN, new SqlParameter("@SessionSK", sessionId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="studentId"></param>
        /// <param name="sessHistoryId"></param>
        /// <returns></returns>
        public override IDataReader GetRecordingHistoryLastSeven(int sessionId, int studentId, int sessHistoryId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETRECORDINGHISTORYLASTSEVEN, new SqlParameter("@SessionSK", sessionId), new SqlParameter("@CUST_SUBS_USER_SK", studentId), new SqlParameter("@SessionHistoryId", sessHistoryId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public override IDataReader GetSessionHistoryBySessionMonthNames(int sessionId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.sp_GETREADINGHISTORYBYSESSIONMONTHS, new SqlParameter("@SessionSK", sessionId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IDataReader GetReadingLevel(int groupId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETPROFILEREADINGLEVEL, new SqlParameter("@GroupSK", groupId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IDataReader GetStudentByGroup(int groupId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSTUDENTBYGROUP, new SqlParameter("@GroupId", groupId));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerSubSK"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetClassListBySubscription(string CustomerSubSK, string userLoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETCLASSBYSUBS, new SqlParameter("@CustomerSubSk", CustomerSubSK), new SqlParameter("@UserLoginName", userLoginName));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupType"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetGroupsByGroupID(int groupId, char groupType, string userLoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GROUPSEARCH, new SqlParameter("@GroupId", groupId), new SqlParameter("@GroupType", groupType), new SqlParameter("@UserLoginName", userLoginName));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerSubSK"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetgroupListBySubscription(string CustomerSubSK, string userLoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETGROUPBYSUBS, new SqlParameter("@CustomerSubSk", CustomerSubSK), new SqlParameter("@UserLoginName", userLoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="fromReadLvl"></param>
        /// <param name="toReadLvl"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public override IDataReader GetBooksByReadingLevel(int subId, int fromReadLvl, int toReadLvl,string LoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKSBYREADLEVEL, new SqlParameter("@CUST_SUBS_SK", subId), new SqlParameter("@FromReadingLevel", fromReadLvl), new SqlParameter("@ToReadingLevel", toReadLvl), new SqlParameter("@UserCreated", LoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Module_Name"></param>
        /// <returns></returns>
        public override IDataReader GetErrorMessagesByModuleName(string Module_Name)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETERRORMESSAGES, new SqlParameter("@Module_Name", Module_Name));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <param name="SubId"></param>
        /// <returns></returns>
        public override IDataReader GetAllLookUpNames(int userId, string loginName,int SubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETLOOKUPNAMES, new SqlParameter("@UserId", userId), new SqlParameter("@LoginName", loginName), new SqlParameter("@CustSubId", SubId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userId"></param>
        /// <param name="loginName"></param>
        /// <param name="SubId"></param>
        /// <returns></returns>
        public override IDataReader GetNamesForSearch(string searchString, int userId, string loginName, int SubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETNAMESFORSEARCH, new SqlParameter("@SearchString", searchString), new SqlParameter("@UserId", userId), new SqlParameter("@LoginName", loginName), new SqlParameter("@SubsId", SubId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetStudentLookupNames(int subsId, string userLoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSTUDENTBYSUBS, new SqlParameter("@SubsId", subsId), new SqlParameter("@TeacherLoginName", userLoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public override IDataReader GetSearchStudentDetails(string searchString, string userLoginName, int custSubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSTUDENTNAMESFORSEARCH, new SqlParameter("@SearchString", searchString), new SqlParameter("@UserLoginName", userLoginName), new SqlParameter("@SubId", custSubId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public override IDataReader GetSearchGroup(string searchString, string userLoginName, int custSubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETGROUPSEARCH, new SqlParameter("@SearchString", searchString), new SqlParameter("@UserLoginName", userLoginName), new SqlParameter("@CustSubId", custSubId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public override IDataReader GetGroupName(string userLoginName, int custSubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETGROUPNAME, new SqlParameter("@UserLoginName", userLoginName), new SqlParameter("@CustSubId", custSubId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public override IDataReader GetSearchTeachers(string searchString, string userLoginName, int custSubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETTEACHERSSEARCH, new SqlParameter("@SearchString", searchString), new SqlParameter("@LoginName", userLoginName), new SqlParameter("@SubId", custSubId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <param name="Attribute_Type"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetBooksCategories(int subsId, string Attribute_Type, string userLoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETBOOKSCATEGORIES, new SqlParameter("@SubId", subsId), new SqlParameter("@ATTRIBUTE_TYPE", Attribute_Type), new SqlParameter("@LoginName", userLoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="custSubId"></param>
        /// <param name="Attribute_Type"></param>
        /// <returns></returns>
        public override IDataReader GetSearchGroup(string searchString, int custSubId, string Attribute_Type)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETBOOKSSEARCH, new SqlParameter("@SearchString", searchString), new SqlParameter("@CUST_SUBS_SK", custSubId), new SqlParameter("@Attribute_type", Attribute_Type));
        }

        /// <summary>
        /// Inserts data into util email table to send email on an occurrence of any exception
        /// </summary>
        /// <returns>List of countries as Dataset</returns>
        public override void SendExceptionMail(string body, string errorMessage, int SubsSk)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, Constants.sp_EXCEPTIONMAIL,
                                                new SqlParameter("@body", body),
                                                new SqlParameter("@ErrorMessage", errorMessage),
                                                new SqlParameter("@DateCreated", DateTime.Now),
                                                new SqlParameter("@toaddress", ConfigurationManager.AppSettings["ExceptionMailToAddress"]),
                                                new SqlParameter("@SubsSk", SubsSk));
            }
            catch (Exception exception)
            {
                //throw exception;
            }
        }       

        #endregion
    }

}