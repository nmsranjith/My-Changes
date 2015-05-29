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
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using System.Data.SqlClient;
using System.Xml;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using System.Collections.Generic;
using System.Configuration;

namespace DotNetNuke.Modules.eCollection_Teachers.Data
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
        private const string ModuleQualifier = "eCollection_Teachers_";

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
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override int? CreateTeacherProfile(Teacher teacher)
            {
                return int.Parse(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_ADDTEACHERPROFILE
                           , new SqlParameter("@TeacherLoginName", teacher.UserLoginName)
                           , new SqlParameter("@FirstName", teacher.FirstName)
                           , new SqlParameter("@LastName", teacher.LastName)
                           , new SqlParameter("@Email", teacher.Email)
                           , new SqlParameter("@MailBody", teacher.MailBody)
                           , new SqlParameter("@InvitedDate", DateTime.Now)
                           , new SqlParameter("@SelectedCustSubSk", teacher.SubscriptionId)
                           , new SqlParameter("@EmailUrl", teacher.EmailUrl)).ToString());
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override DataTable UploadTeacherProfiles(Teacher teacher)
            {
                SqlConnection uploadConnection = new SqlConnection(_connectionString);
                uploadConnection.Open();
                SqlTransaction uploadTransaction = uploadConnection.BeginTransaction();
                try
                {
                    DataTable result = SqlHelper.ExecuteDataset(uploadTransaction, Constants.SP_USERSBULKUPLOAD
                               , new SqlParameter("@xmlDoc", teacher.TeachersDoc.InnerXml)
                               , new SqlParameter("@UserCreated", teacher.UserCreated)
                               , new SqlParameter("@SubscriptionId", teacher.SubscriptionId)
                               , new SqlParameter("@AddedDate", teacher.CreatedDate)
                               ).Tables[0];

                    if (result.Rows.Count>0)
                    {
                        uploadTransaction.Commit();
                        return result;
                    }
                    else if (result.Rows[0][0] == "DUPLICATES")
                    {                   
                        throw new Exception("Teacher's email ids must be unique.");
                    }
                    uploadTransaction.Rollback();
                    return null;
                }
                catch (Exception ex)
                {
                    uploadTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    uploadConnection.Close();
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="TeacherLoginName"></param>
            /// <returns></returns>
            public override IDataReader GetTeachersList(Teacher teacher)
            {
                try
                {
                    return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETTEACHERSLIST
                           , new SqlParameter("@TeacherLoginName", teacher.TeacherLoginName)
                           , new SqlParameter("@SubsId", teacher.SubscriptionId));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teachers"></param>
            /// <returns></returns>
            public override void DeleteTeachers(Teacher teachers)
            {
                try
                {
                    foreach (Teacher teacher in teachers.SelectedTeachersList)
                    {
                        SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_DELETETEACHERPROFILE
                               , new SqlParameter("@UserId", teacher.TeacherId)
                               , new SqlParameter("@UserModified", teachers.UserModified)
                               , new SqlParameter("@DateModified", teachers.DateModified)
                               , new SqlParameter("@CustSubsSk", teachers.SubscriptionId));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override IDataReader GetSubscriptionsList(Teacher teacher)
            {
                try
                {
                    return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETSUBSCRIPTIONSLIST
                           , new SqlParameter("@LoginName", teacher.UserLoginName)
                           , new SqlParameter("@Type", "SUBSCRIPTIONS")
                           );
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override IDataReader GetCustSubuserSk(Teacher teacher)
            {
                try
                {
                    return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETSUBSCRIPTIONSLIST
                           , new SqlParameter("@LoginName", teacher.UserLoginName)
                           , new SqlParameter("@Type", "SubUserSK")
                           );
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override IDataReader GetGroupNames(Teacher teacher)
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETALLSUBSGROUPS
                    , new SqlParameter("@GroupType", teacher.GrpType)
                    , new SqlParameter("@LoginName", teacher.UserLoginName)
                    , new SqlParameter("@SubsSK", teacher.SubscriptionId)
                    , new SqlParameter("@Type", teacher.ActionType)
                    );
            }        

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override int UpdateBookReadLevel(Teacher teacher)
            {
                return SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_UPDATEBOOKREADLEVEL
                          , new SqlParameter("@TeacherId", teacher.TeacherId)
                          , new SqlParameter("@BookReadLevelFrom", teacher.BookReadLevelFrom)
                          , new SqlParameter("@BookReadLevelUpto", teacher.BookReadLevelUpto)
                          , new SqlParameter("@UserModified", teacher.UserModified)
                          , new SqlParameter("@DateModified", DateTime.Now)
                          );
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="groupId"></param>
            /// <returns></returns>
            public override IDataReader GetTeacherName(int groupId)
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETTEACHERNAMES, new SqlParameter("@GroupId", groupId));
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacherList"></param>
            /// <returns></returns>
            public override int AddTeacherToGroup(List<Teacher> teacherList)
            {
                int result = 0;
                foreach (Teacher teacher in teacherList)
                {
                    result += int.Parse(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_ADDTEACHERTOGROUPS
                               , new SqlParameter("@GroupId", teacher.ClassId)
                               , new SqlParameter("@Active", teacher.Active)
                               , new SqlParameter("@UserModified", teacher.UserModified)
                               , new SqlParameter("@DateModified", teacher.DateModified)
                               , new SqlParameter("@CustomerSubUserSK", teacher.CustSubUserSk)
                               , new SqlParameter("@SelectedCustSubSk", teacher.SubscriptionId)
                               ).ToString());
                }
                return result;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override DataSet GetProfileDetails(Teacher teacher)
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETPROFILEDETAILS
                   , new SqlParameter("@TeacherId", teacher.TeacherId)
                   , new SqlParameter("@ActiveFlag", teacher.Active)
                   , new SqlParameter("@CustSubsSk", teacher.SubscriptionId) 
                   );
            }



            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override DataSet GetReadingHistory(Teacher teacher)
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETPREADINGHISTORY
                   , new SqlParameter("@TeacherId", teacher.TeacherId)                   
                   );
            }

            /************************** READING HISTORY START *********************************/
            /// <summary>
            /// Returns the student's reading history months
            /// </summary>
            /// <param name="student"></param>
            /// <returns></returns>
            public override IDataReader GetReadingHistoryMonths(Teacher teacher)
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETREADINGHISTORYMONTHS
                    , new SqlParameter("@UserSk", teacher.TeacherId)
                    , new SqlParameter("@CustSubsSk", teacher.SubscriptionId)
                    , new SqlParameter("@Type", teacher.ActionType)
                    );
            }

            /// <summary>
            /// Returns the student's reading history months
            /// </summary>
            /// <param name="student"></param>
            /// <returns></returns>
            public override IDataReader GetReadingHistories(Teacher teacher)
            {
                var a = SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETREADINGHISTORY
                    , new SqlParameter("@UserSk", teacher.TeacherId)
                    , new SqlParameter("@CustSubsSk", teacher.SubscriptionId)
                    , new SqlParameter("@Type", teacher.ActionType)
                    );
                return a;
            }

            /// <summary>
            /// Returns all the circled words of the Teacher
            /// </summary>
            /// <param name="student"></param>
            /// <returns></returns>
            public override IDataReader GetMyWords(Teacher teacher)
            {
                var a = SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETMYWORDS
                    , new SqlParameter("@UserSk", teacher.TeacherId)
                    , new SqlParameter("@CustSubsSk", teacher.SubscriptionId)
                    , new SqlParameter("@Type", teacher.ActionType)
                    );
                return a;
            }

            /// <summary>
            /// Returns all the recordings of the Teacher
            /// </summary>
            /// <param name="student"></param>
            /// <returns></returns>
            public override IDataReader GetMyRecordings(Teacher teacher)
            {
                var a = SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETMYRECORDINGS
                    , new SqlParameter("@UserSk", teacher.TeacherId)
                    , new SqlParameter("@CustSubsSk", teacher.SubscriptionId)
                    , new SqlParameter("@Type", teacher.ActionType)
                    );
                return a;
            }

            /// <summary>
            /// Returns all the recording files of the Teacher
            /// </summary>
            /// <param name="student"></param>
            /// <returns></returns>
            public override IDataReader GetMyRecordedFiles(Teacher teacher)
            {
                var a = SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETMYRECORDINGFILES
                    , new SqlParameter("@UserSk", teacher.TeacherId)
                    , new SqlParameter("@CustSubsSk", teacher.SubscriptionId)
                    );
                return a;
            }

            /// <summary>
            /// Returns the number of words circled by the Teacher in each reading session 
            /// </summary>
            /// <param name="student"></param>
            /// <returns></returns>
            public override IDataReader GetMyRHWordsCount(Teacher teacher)
            {
                var a = SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETMYRHWORDSCOUNT
                    , new SqlParameter("@UserSk", teacher.TeacherId)
                    , new SqlParameter("@CustSubsSk", teacher.SubscriptionId)
                    );
                return a;
            }

            /// <summary>
            /// Returns the total number of words circled by the Teacher / total number of recordings done in overall.
            /// </summary>
            /// <param name="student"></param>
            /// <returns></returns>
            public override int GetMyWordsCount_MyRecordingsCount(Teacher teacher)
            {
                var a = Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_GETMYWORDSCOUNT_MYRECORDINGSCOUNT
                    , new SqlParameter("@UserSk", teacher.TeacherId)
                    , new SqlParameter("@CustSubsSk", teacher.SubscriptionId)
                    , new SqlParameter("@Type", teacher.ActionType)
                    ));
                return a;
            }


            /************************** READING HISTORY END *********************************/
            
           
            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override int ClearFromiPad(Teacher teacher)
            {
                return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_CLEARFROMIPAD
                , new SqlParameter("@UserId", teacher.TeacherId)
                , new SqlParameter("@ClearStatus", teacher.ActionType)
                , new SqlParameter("@FromDate", teacher.DateModified)
                , new SqlParameter("@Todate", DateTime.Now.AddDays(-9))
                , new SqlParameter("@Month", teacher.MonthId)
                , new SqlParameter("@CustSubsSk", teacher.SubscriptionId)).ToString());
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="teacher"></param>
            /// <returns></returns>
            public override IDataReader CheckClearedFromiPad(Teacher teacher)
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_ISCLEAREDFLAGCHECK
               , new SqlParameter("@UserId", teacher.TeacherId)
               , new SqlParameter("@CurrentDate", DateTime.Now)
               , new SqlParameter("@CustSubsSk", teacher.SubscriptionId));
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