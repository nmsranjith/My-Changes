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
using DotNetNuke.Modules.eCollection_Groups.Components;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;
using System.Xml;
using System.Transactions;
using System.Configuration;

namespace DotNetNuke.Modules.eCollection_Groups.Data
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
        private const string ModuleQualifier = "eCollection_Groups_";

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
        /// <param name="groupType"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public override IDataReader GetGroupList(char groupType, string userLoginName, int custSubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETALLGROUPLIST, new SqlParameter("@GroupType", groupType), new SqlParameter("@UserLoginName", userLoginName), new SqlParameter("@CustSubId", custSubId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClassType"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public override IDataReader GetClassList(char ClassType, string userLoginName, int custSubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETALLGROUPLIST, new SqlParameter("@GroupType", ClassType), new SqlParameter("@UserLoginName", userLoginName), new SqlParameter("@CustSubId", custSubId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetGroupName(char groupType, string userLoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETALLGROUPLIST, new SqlParameter("@GroupType", groupType), new SqlParameter("@UserLoginName", userLoginName));
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
        /// <param name="a"></param>
        /// <returns></returns>
        public override int AddGroup(DotNetNuke.Modules.eCollection_Groups.Components.Groups a)
        {
            int firstTeacher = a.TeachersList[0].Id;
            int results = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDGROUP
                , new SqlParameter("@SelectedSubSk", a.CustomerSubId)
                , new SqlParameter("@Name", a.Name)
                , new SqlParameter("@GroupType", a.GroupType)
                , new SqlParameter("@CreatedOnDate", a.CreatedOnDate)
                , new SqlParameter("@ActiveFlag", a.ActiveFlag)
                , new SqlParameter("@DateCreated", a.DateCreated)
                , new SqlParameter("@UserLoginName", a.LoginName)
                , new SqlParameter("@SelectedUserID", firstTeacher)
                ));
            if (a.TeachersList != null)
            {
                a.TeachersList.RemoveAt(0);
                foreach (IDCollection teachercol in a.TeachersList)
                {
                    SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDGROUPMEMBER
                    , new SqlParameter("@groupSK", results) 
                    , new SqlParameter("@SelectedSubSk", a.CustomerSubId)
                    , new SqlParameter("@SelectedUserID", teachercol.Id)
                    , new SqlParameter("@ActiveFlag", a.ActiveFlag)
                    , new SqlParameter("@DateCreated", a.DateCreated)
                    , new SqlParameter("@UserLoginName", a.LoginName)
                    , new SqlParameter("@FirstTeacher", firstTeacher)
                    );
                }
            }
            if (a.StudentList != null)
            {
                foreach (Students studentcol in a.StudentList)
                {
                    SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDGROUPMEMBER
                    , new SqlParameter("@groupSK", results)
                    , new SqlParameter("@SelectedSubSk", a.CustomerSubId)
                    , new SqlParameter("@SelectedUserID", studentcol.UserID)
                    , new SqlParameter("@ActiveFlag", a.ActiveFlag)
                    , new SqlParameter("@DateCreated", a.DateCreated)
                    , new SqlParameter("@UserLoginName", a.LoginName)
                    , new SqlParameter("@FirstTeacher", firstTeacher)
                    );
                }
            }
            return results;
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
        /// <param name="groupName"></param>
        /// <param name="groupType"></param>
        /// <param name="customerSubId"></param>
        /// <returns></returns>
        public override bool ValidateGroupName(string groupName, char groupType, int customerSubId)
        {
            var res=Null.SetNullInteger((SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_GROUPNAMEVALIDATION, new SqlParameter("@GroupName", groupName), new SqlParameter("@GroupType", groupType), new SqlParameter("@CustomerSubID", customerSubId)).ToString()));
            return res != 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IDataReader GetGroupProfileByGroup(int groupId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETGROUPPROFILE, new SqlParameter("@GroupSK", groupId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IDataReader GetReadingLevelByGroup(int groupId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETREADINGLEVELBYGROUP, new SqlParameter("@GroupSK", groupId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IDataReader GetReadingGraphByGroup(int groupId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETPROFILEREADINGLEVEL, new SqlParameter("@GroupSK", groupId));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public override IDataReader GetReadingHistoryByGroup(int groupId, string month)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETPROFILEREADINGHISTORY, new SqlParameter("@GroupSK", groupId), new SqlParameter("@Month", month));
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
        /// <param name="month"></param>
        /// <returns></returns>
        public override IDataReader GetReadingSessionByGroup(int groupId, string month)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETPROFILEREADINGSESSION, new SqlParameter("@GroupSK", groupId), new SqlParameter("@Month", month));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="refDate"></param>
        /// <returns></returns>
        public override DataSet  GetReadingSessionByGroupAndSectionCat(int groupId, DateTime refDate)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETPROFILEREADINGSESSIONBYCAT, new SqlParameter("@GroupSK", groupId), new SqlParameter("@RefDate", refDate ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="currentReadLevel"></param>
        /// <returns></returns>
        public override IDataReader GetBookCount(int groupId, int currentReadLevel)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETBOOKCOUNT, new SqlParameter("@GroupSK", groupId), new SqlParameter("@CurrentReadLevel", currentReadLevel));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetSubscription(string userLoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSUBSCRIPTION, new SqlParameter("@UserLoginName", userLoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetStudentsBySubcription(int subsId, string userLoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSTUDENTBYSUBS, new SqlParameter("@SubsId", subsId), new SqlParameter("@TeacherLoginName", userLoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetTeachersbySubscription(int subsId, string userLoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETTEACHERBYSUBS, new SqlParameter("@SubscriptionId", subsId), new SqlParameter("@UserLoginName", userLoginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IDataReader GetTeachersByGroup(int groupId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETGROUPMEMBER, new SqlParameter("@GroupId", groupId), new SqlParameter("@Role", "Teacher"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public override IDataReader GetMembersByGroup(int groupId, string role)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETGROUPMEMBER, new SqlParameter("@GroupId", groupId), new SqlParameter("@Role", role));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public override DataTable GetGroupNameByGroupId(int groupId, string role)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSTUDENTBYGROUP, new SqlParameter("@GroupId", groupId)).Tables[0];
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
        /// <param name="groupId"></param>
        /// <param name="groupMemberId"></param>
        /// <param name="productId"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public override IDataReader GetRecordingHistoryByGroup(int groupId, int groupMemberId, int productId, string month)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETPROFILERECORDINGHISTORY, new SqlParameter("@GroupSK", groupId), new SqlParameter("@GroupMemberID", groupMemberId), new SqlParameter("@ProductSK", productId), new SqlParameter("@Month", month));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="groupMemberId"></param>
        /// <param name="productId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public override IDataReader GetLastSevenDaysRecordings(int groupId, int groupMemberId, int productId, DateTime fromDate, DateTime toDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETLASTSEVENDAYSRECORDING, new SqlParameter("@GroupSK", groupId), new SqlParameter("@GroupMemberID", groupMemberId), new SqlParameter("@ProductSK", productId), new SqlParameter("@FromDate", fromDate), new SqlParameter("@ToDate", toDate));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public override IDataReader GetLastSevenDaysReadings(int groupId, DateTime fromDate, DateTime toDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETLASTSEVENDAYSREADING, new SqlParameter("@GroupSK", groupId), new SqlParameter("@FromDate", fromDate), new SqlParameter("@ToDate", toDate));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        public override IDataReader GetWordsByGroup(int groupID)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETCIRCLEDWORDS, new SqlParameter("@GroupID", groupID));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="Teachercollection"></param>
        /// <param name="StudentCollection"></param>
        /// <param name="customerSubSK"></param>
        public override void UpdateMembers(Components.Groups groups, List<IDCollection> Teachercollection, List<Students> StudentCollection, int customerSubSK)
        {
            if (Teachercollection != null)
            {
                foreach (IDCollection membercollection in Teachercollection)
                {

                    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, Constants.SP_UPDATEGROUPMEMBERS
                   , new SqlParameter("@GroupId", groups.GroupId)
                   , new SqlParameter("@Active", 'N')
                   , new SqlParameter("@UserModified", groups.UserCreated)
                   , new SqlParameter("@DateModified", groups.DateCreated)
                   , new SqlParameter("@SelectedUserSk", membercollection.Id)
                   , new SqlParameter("@SelectedCustSubSk", customerSubSK)
                   );

                }
            }
            if (StudentCollection != null)
            {
                foreach (Students membercollection in StudentCollection)
                {

                    SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, Constants.SP_UPDATEGROUPMEMBERS
                   , new SqlParameter("@GroupId", groups.GroupId)
                   , new SqlParameter("@Active", 'N')
                   , new SqlParameter("@UserModified", groups.UserCreated)
                   , new SqlParameter("@DateModified", groups.DateCreated)
                   , new SqlParameter("@SelectedUserSk", membercollection.UserID)
                   , new SqlParameter("@SelectedCustSubSk", customerSubSK)
                   );

                }
            }
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, Constants.SP_UPDATEGROUP
              , new SqlParameter("@GroupId", groups.GroupId)
              , new SqlParameter("@CustSubsSK", groups.CustomerSubId)
              , new SqlParameter("@Name", groups.Name)
              , new SqlParameter("@GroupType", groups.GroupType)
              , new SqlParameter("@UserLoginName", groups.LoginName)
              , new SqlParameter("@DateModified", groups.DateCreated)
              );
           
            if (groups.TeachersList != null)
            {
                foreach (IDCollection membercollection in groups.TeachersList)
                {
                    int results = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDEDITEDGROUP
                        , new SqlParameter("@GroupId", groups.GroupId)
                        , new SqlParameter("@SelectedSubSk", groups.CustomerSubId)
                        , new SqlParameter("@SelectedUserID", membercollection.Id)
                        , new SqlParameter("@UserLoginName", groups.LoginName)
                        , new SqlParameter("@ActiveFlag", groups.ActiveFlag)
                        , new SqlParameter("@DateCreated", groups.DateCreated)
                        , new SqlParameter("@UserCreated", groups.UserCreated)
                        , new SqlParameter("@MemberStatus", "Teacher")
                        ));
                }
            }
            if (groups.StudentList != null)
            {
                foreach (Students membercollection in groups.StudentList)
                {
                    int results = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDEDITEDGROUP
                            , new SqlParameter("@GroupId", groups.GroupId)
                            , new SqlParameter("@SelectedSubSk", groups.CustomerSubId)
                            , new SqlParameter("@SelectedUserID", membercollection.UserID)
                            , new SqlParameter("@UserLoginName", groups.LoginName)
                            , new SqlParameter("@ActiveFlag", groups.ActiveFlag)
                            , new SqlParameter("@DateCreated", groups.DateCreated)
                            , new SqlParameter("@UserCreated", groups.UserCreated)
                            , new SqlParameter("@MemberStatus", "Student")
                            ));
                }
            }

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public override IDataReader GetMonthByGroup(int groupId, string type)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETMONTH, new SqlParameter("@GroupSK", groupId), new SqlParameter("@Type", type));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minReadLevel"></param>
        /// <param name="maxReadLevel"></param>
        /// <returns></returns>
        public override IDataReader GetColorByReadLevel(int minReadLevel, int maxReadLevel)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETCOLORBYREADLEVEL, new SqlParameter("@MinReadLevel", minReadLevel), new SqlParameter("@MaxReadLevel", maxReadLevel));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="groupId"></param>
        public override void DeleteGroup(Components.Groups groups, List<string> groupId)
        {
            foreach (string Id in groupId)
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, Constants.SP_DELETEGROUP
                    , new SqlParameter("@GroupId", Id.Trim())
                    , new SqlParameter("@Active", groups.ActiveFlag)
                    , new SqlParameter("@UserLoginName", groups.LoginName)
                    , new SqlParameter("@DateModified", groups.DateCreated)
                    );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stagingXmlDoc"></param>
        /// <param name="countCheckReq"></param>
        /// <param name="server"></param>
        /// <returns></returns>
        public override int AddIsbnDetails(XmlDocument stagingXmlDoc, int countCheckReq, string server)
        {
            try
            {
                int results;               
                results = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDSTAGING
                    , new SqlParameter("@StagingXml", stagingXmlDoc.InnerXml), new SqlParameter("@CountChekReq", countCheckReq), new SqlParameter("@server", server)));
                return results;
            }
            catch (Exception ex) { throw ex; }

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="publishingXmlDoc"></param>
        /// <returns></returns>
        public override int AddBooksInPublish(XmlDocument publishingXmlDoc)
        {
            try
            {
                int results = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_ADDPUBLISH
                            , new SqlParameter("@PublishXml", publishingXmlDoc.InnerXml)));
                return results;
            }
            catch (Exception ex) { throw ex; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDataReader GetBooksDetails()
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSTAGING);
            }
            catch (Exception ex) { throw ex; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public override IDataReader GetSearchBooksDetails(string searchText)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSEARCHSTAGING, new SqlParameter("@SearchString", searchText));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deleteXmlDoc"></param>
        /// <returns></returns>
        public override int DeleteBooksInStaging(XmlDocument deleteXmlDoc)
        {
            try
            {
                int results = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_DELETESTAGING
                            , new SqlParameter("@StagingDeleteXml", deleteXmlDoc.InnerXml)));
                return results;
            }
            catch (Exception ex) { throw ex; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbnXmlDoc"></param>
        /// <returns></returns>
        public override int CheckISBN(XmlDocument isbnXmlDoc)
        {
            try
            {
                int results = Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_CHECKISBN
                          , new SqlParameter("@isbnXml", isbnXmlDoc.InnerXml)));
                return results;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isbnXmlDoc"></param>
        /// <returns></returns>
        public override IDataReader GetIsbnDetails(XmlDocument isbnXmlDoc)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETISBNDETAILS, new SqlParameter("@isbnXml", isbnXmlDoc.InnerXml));
            }
            catch (Exception ex) { throw ex; }
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
        /// <param name="searchString"></param>
        /// <param name="userLoginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public override IDataReader GetSearchStudentDetails(string searchString, string userLoginName, int custSubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSTUDENTSEARCH, new SqlParameter("@SearchString", searchString), new SqlParameter("@UserLoginName", userLoginName), new SqlParameter("@CustSubId", custSubId));
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
        public override IDataReader GetSearchTeacherDetails(string searchString, string userLoginName, int custSubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETTEACHERSEARCH, new SqlParameter("@SearchString", searchString), new SqlParameter("@UserLoginName", userLoginName), new SqlParameter("@CustSubId", custSubId));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IDataReader GetSearchStudentByGroup(string searchString, int groupId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETSTUDENTSEARCHBYGROUP, new SqlParameter("@GroupId", groupId), new SqlParameter("@SearchString", searchString));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="month"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public override IDataReader GetReadingSessionWrapper(int groupId, string month, int sessionId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETREADINGSESSIONWRAPPER, new SqlParameter("@GroupSK", groupId), new SqlParameter("@Month", month), new SqlParameter("@SeessionSK", sessionId));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="sectionCat"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public override IDataReader GetReadingSessionWrapperBySectionCat(int groupId, string sectionCat, int sessionId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETREADINGSESSIONWRAPPERBYCAT, new SqlParameter("@GroupSK", groupId), new SqlParameter("@SelectionStr", sectionCat), new SqlParameter("@SeessionSK", sessionId));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="currentDate"></param>
        /// <param name="fromDate"></param>
        /// <param name="todate"></param>
        /// <param name="month"></param>
        /// <param name="updateStatus"></param>
        /// <returns></returns>
        public override int UpdateIsClearFromStatus(int groupId, DateTime? currentDate, DateTime? fromDate, DateTime? todate, string month, string updateStatus)
        {

            return SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, Constants.SP_UPDATEISCLEARFROMIPAD
           , new SqlParameter("@GroupSK", groupId)
           , new SqlParameter("@CurrentDate", currentDate)
           , new SqlParameter("@FromDate", fromDate)
           , new SqlParameter("@Todate", todate)
           , new SqlParameter("@month", month)
           , new SqlParameter("@UpdateStatus", updateStatus)
           );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="grfRefDate"></param>
        /// <returns></returns>
        public override IDataReader GetMinMaxReadingLevelByDate(int groupId, DateTime grfRefDate)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETMINMAXLEVEL, new SqlParameter("@GroupSK", groupId), new SqlParameter("@ReferenceDate", grfRefDate));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override int GetGroupOwnerID(int groupId)
        {
            return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETGROUPOWNERID, new SqlParameter("@GroupId", groupId)).ToString());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="custSubId"></param>
        /// <returns></returns>
        public override IDataReader GetLoginDetails(string loginName, int custSubId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETLOGINDETAILS, new SqlParameter("@UserLoginName", loginName), new SqlParameter("@CustSubId", custSubId));
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


        public override bool InsertNewePubBooks(DataTable newXmlIsbnsDt)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();

                SqlParameter attrtvp = new SqlParameter();
                attrtvp.ParameterName = "@EPUB_NEWISBN_TVP";
                attrtvp.Value = newXmlIsbnsDt;
                attrtvp.SqlDbType = SqlDbType.Structured;
                attrtvp.TypeName = "EPUB_NEWISBN_TVP";


                SqlCommand comm = new SqlCommand("ECOLL_INSERT_NEW_EPUB_ISBN", conn);
                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add(attrtvp);

                return comm.ExecuteNonQuery() > 0;
            }
            catch (Exception exception)
            {
                //throw exception;
                return false;
            }
        }

        public override string GetProductAndAuthorIds()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();

                SqlCommand comm = new SqlCommand("ECOLL_GET_CURRENT_PRODUCT_AND_AUTHOR_ID", conn);
                comm.CommandType = CommandType.StoredProcedure;
                SqlParameter identityparam = new SqlParameter("@OUTVALUES", SqlDbType.VarChar,100);
                identityparam.Direction = ParameterDirection.Output;
                comm.Parameters.Add(identityparam);
                comm.ExecuteNonQuery();
                string identities;
                if (identityparam.Value.ToString() != null)
                {
                    identities = comm.Parameters["@OUTVALUES"].Value.ToString();
                    return identities;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        #endregion
    }

}