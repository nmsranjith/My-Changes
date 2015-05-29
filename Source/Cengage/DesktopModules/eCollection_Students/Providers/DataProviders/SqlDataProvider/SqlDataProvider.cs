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
using System.Data.SqlClient;
using DotNetNuke.Modules.eCollection_Students.Components;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using System.Xml;
using System.Collections.Generic;
using System.Configuration;
namespace DotNetNuke.Modules.eCollection_Students.Data
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
        private const string ModuleQualifier = "eCollection_Students_";

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
        /// Get all students
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetStudentsList(Student student)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETCLASSMEMBERS //SP_GETSTUDENTSLIST
                       , new SqlParameter("@ClassId", 0)
                       //, new SqlParameter("@TeacherLoginName", student.TeacherLoginName)
                       , new SqlParameter("@Active", 'Y')
                       , new SqlParameter("@SubSk", student.SubscriptionId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all subscriptions
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override DataTable GetSubscriptionsList(Student student)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETSUBSCRIPTIONSLIST
                       , new SqlParameter("@LoginName", student.TeacherLoginName)
                       , new SqlParameter("@Active", student.Active)
                       ).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check whether student exist or not
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override int CheckExists(Student student)
        {

            try
            {
                return int.Parse(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_ADDSTUDENTPROFILE
                          , new SqlParameter("@UserLoginName", student.UserLoginName)
                       ).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check student username exists or not
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override string CheckUserNameExists(Student student)
        {
            try
            {
                return SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_CHECKUSERNAMEEXISTS
                          , new SqlParameter("@UserLoginName", student.UserLoginName)
                       ).ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Create Student Profile
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override int CreateStudentProfile(Student student)
        {
            try
            {
                return int.Parse(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_ADDSTUDENTPROFILE
                       , new SqlParameter("@UserDomain", student.UserDomain)
                       , new SqlParameter("@InternalUse", student.InternalUse)
                       , new SqlParameter("@FirstName", student.FirstName)
                       , new SqlParameter("@LastName", student.LastName)
                       , new SqlParameter("@Email", student.Email)
                       , new SqlParameter("@UserLoginName", student.UserLoginName)
                       , new SqlParameter("@Password", student.Password)
                       , new SqlParameter("@Gender", student.Gender)
                       , new SqlParameter("@DOB", student.DateofBirth)
                       , new SqlParameter("@Active", student.Active)
                       , new SqlParameter("@UserCreated", student.UserCreated)
                       , new SqlParameter("@Grade", student.Grade)
                       , new SqlParameter("@StartingReadingLevel", student.StartingReadinglevel)
                       , new SqlParameter("@CurrentReadingLevel", student.CurrentReadingLevel)
                       , new SqlParameter("@ESL", student.ESL)
                       , new SqlParameter("@ReadingRecovery", student.ReadingRecovery)
                       , new SqlParameter("@StartingReadingLevelFrom", student.StartingReadinglevelFrom)
                       , new SqlParameter("@StartingReadingLevelTo", student.StartingReadinglevelUpto)
                       , new SqlParameter("@CurrentReadingLevelFrom", student.CurrentReadinglevelFrom)
                       , new SqlParameter("@CurrentReadingLevelTo", student.CurrentReadinglevelUpto)
                       , new SqlParameter("@AddedDate", student.AddedDate)
                       , new SqlParameter("@Description", student.Description)
                       , new SqlParameter("@TeacherLoginName", student.TeacherLoginName)
                       , new SqlParameter("@PurchaseFlag", student.PurchaseFlag)
                       , new SqlParameter("@IsDefaultPartner", student.IsDefaultPartner)
                       , new SqlParameter("@CustomerSubcriptions_SK", student.SubscriptionId)
                       ).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Update student profile
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override int UpdateStudentDetails(Student student)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_UPDATESTUDENTPROFILE
                       , new SqlParameter("@StudentId", student.StudentId)
                       , new SqlParameter("@FirstName", student.FirstName)
                       , new SqlParameter("@LastName", student.LastName)
                       , new SqlParameter("@Email", student.Email)
                       , new SqlParameter("@Password", student.Password)
                       , new SqlParameter("@Gender", student.Gender)
                       , new SqlParameter("@DOB", student.DateofBirth)
                       , new SqlParameter("@Grade", student.Grade)
                       , new SqlParameter("@ESL", student.ESL)
                       , new SqlParameter("@ReadingRecovery", student.ReadingRecovery)
                       , new SqlParameter("@UserModified", student.UserModified)
                       , new SqlParameter("@DateModified", DateTime.Now)
                       );
        }

        /// <summary>
        ///  Get the student profile details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override DataSet GetProfileDetails(Student student)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETPROFILEDETAILS                   
                , new SqlParameter("@studentid", student.StudentId)  
                ,new SqlParameter("@ActiveFlag",student.Active)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId)    
                );
        }


        /// <summary>
        /// Returns Dataset related to reading History
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override DataSet GetStudentsReadingHistory(Student student)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETPREADINGHISTORY
                , new SqlParameter("@studentid", student.StudentId)                
                );
        }
        
        /// <summary>
        /// Get all class names
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetClassNames(Student student)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETALLSUBSGROUPS
                , new SqlParameter("@GroupType", "C")
                , new SqlParameter("@LoginName", student.TeacherLoginName)
                );
        }

        /// <summary>
        ///  Get all Group Names
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetGroupNames(Student student)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETALLSUBSGROUPS
                , new SqlParameter("@GroupType", student.GrpType)
                , new SqlParameter("@LoginName", student.UserLoginName)
                , new SqlParameter("@SubsSK", student.SubscriptionId)
                , new SqlParameter("@Type", student.ActionType)
                );
        }

        /// <summary>
        /// Get teacher Names of the given group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IDataReader GetTeacherName(int groupid)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETTEACHERNAMES, new SqlParameter("@GroupId", groupid));
        }

        /// <summary>
        /// Get all the members of the group
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetClassMembers(Student student)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETCLASSMEMBERS, new SqlParameter("@GroupId", student.ClassId), new SqlParameter("@Active", student.Active), new SqlParameter("@SubSk", student.SubscriptionId));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Create student profile using bulkupload
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public override DataTable UploadStudentProfiles(Student students)
        {

            SqlConnection uploadConnection = new SqlConnection(_connectionString);
            uploadConnection.Open();          
            SqlTransaction uploadTransaction = uploadConnection.BeginTransaction();
            try
            {
                var result = SqlHelper.ExecuteDataset(uploadTransaction, Constants.SP_USERSBULKUPLOAD
                           , new SqlParameter("@xmlDoc", students.StudentsDoc.InnerXml)
                           , new SqlParameter("@UserCreated", students.UserCreated)
                           , new SqlParameter("@SubscriptionId", students.SubscriptionId)
                           , new SqlParameter("@AddedDate", students.CreatedDate)
                           ).Tables[0];

                if (result.Rows.Count>0)
                {
                    uploadTransaction.Commit();
                    return result;
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
        /// Get all students Usernames
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public override DataSet GetAllUserNames(Student students)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_USERSBULKUPLOAD
                      , new SqlParameter("@Type", students.ActionType)
                           , new SqlParameter("@xmlDoc", students.StudentsDoc.InnerXml)
                           , new SqlParameter("@UserCreated", students.UserCreated)
                           , new SqlParameter("@SubscriptionId", students.SubscriptionId)
                           , new SqlParameter("@AddedDate", students.CreatedDate)
                       );
        }

        /// <summary>
        /// Update the student profile info
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override int UpdateStudentProfile(Student student)
        {
            return SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_UPDATESTUDENTPROFILEVALUES
                       , new SqlParameter("@studentid", student.StudentId)
                       , new SqlParameter("@ReadingRecovery", student.ReadingRecovery)
                       , new SqlParameter("@CurrentReadinglevel", student.CurrentReadingLevel)
                       , new SqlParameter("@CurrentReadinglevelFrom", student.CurrentReadinglevelFrom)
                       , new SqlParameter("@CurrentReadinglevelUpto", student.CurrentReadinglevelUpto)
                       , new SqlParameter("@UserModified", student.UserModified)
                       , new SqlParameter("@UpdateFlag", student.UpdateFlag)
                       );
        }

        /// <summary>
        /// Get all the details of the students
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public override DataTable GetStudentsDetails(Student students)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_PRINT_DELETESTUDENTSPROFILE, new SqlParameter("@UserId", students.StudentId), new SqlParameter("@XmlDoc", students.StudentsDoc.InnerXml), new SqlParameter("@Type", "DETAILS"), new SqlParameter("@UserModified", students.UserModified), new SqlParameter("@DateModified", students.DateModified), new SqlParameter("@CustSubsSk", students.SubscriptionId)).Tables[0];
        }

        /// <summary>
        ///  Delete student profile
        /// </summary>
        /// <param name="students"></param>
        public override void DeleteStudents(Student students)
        {
            foreach (Student stu in students.SelectedStudentsList)
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_PRINT_DELETESTUDENTSPROFILE
                       , new SqlParameter("@UserId", stu.StudentId)
                       , new SqlParameter("@XmlDoc", null)
                       , new SqlParameter("@FnType", "DELETE")
                       , new SqlParameter("@UserModified", students.UserModified)
                       , new SqlParameter("@DateModified", students.DateModified)
                       , new SqlParameter("@CustSubsSk", students.SubscriptionId));
            }
        }

        /// <summary>
        ///  Check the flag of Clear from iPad for all words
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader CheckClearedFromiPad(Student student)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_ISCLEAREDFLAGCHECK
                , new SqlParameter("@UserId", student.StudentId)
                , new SqlParameter("@CurrentDate", DateTime.Now)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId));
        }

        /// <summary>
        /// Clear words from iPad
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override  int ClearFromiPad(Student student)
        {   
            var a= SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_CLEARFROMIPAD
                , new SqlParameter("@UserId", student.StudentId)
                , new SqlParameter("@ClearStatus", student.ActionType)
                , new SqlParameter("@FromDate", student.DateModified)
                , new SqlParameter("@Todate", DateTime.Now.AddDays(-9))
                , new SqlParameter("@Month", student.MonthId)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId));
            return 1;
        }

        /************************** READING HISTORY START *********************************/
        /// <summary>
        /// Returns the student's reading history months
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetReadingHistoryMonths(Student student)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETREADINGHISTORYMONTHS
                , new SqlParameter("@UserSk", student.StudentId)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId)
                , new SqlParameter("@Type", student.ActionType)
                );
        }

        /// <summary>
        /// Returns the student's reading history months
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetReadingHistories(Student student)
        {
            var a= SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETREADINGHISTORY
                , new SqlParameter("@UserSk", student.StudentId)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId)
                , new SqlParameter("@Type", student.ActionType)
                );
            return a;
        }

        /// <summary>
        /// Returns all the circled words of the Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetMyWords(Student student)
        {
            var a = SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETMYWORDS
                , new SqlParameter("@UserSk", student.StudentId)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId)
                , new SqlParameter("@Type", student.ActionType)
                );
            return a;
        }

        /// <summary>
        /// Returns all the recordings of the Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetMyRecordings(Student student)
        {
            var a = SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETMYRECORDINGS
                , new SqlParameter("@UserSk", student.StudentId)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId)
                , new SqlParameter("@Type", student.ActionType)
                );
            return a;
        }

        /// <summary>
        /// Returns all the recording files of the Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetMyRecordedFiles(Student student)
        {
            var a = SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETMYRECORDINGFILES
                , new SqlParameter("@UserSk", student.StudentId)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId)
                );
            return a;
        }

        /// <summary>
        /// Returns the number of words circled by the Student in each reading session 
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetMyRHWordsCount(Student student)
        {
            var a = SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETMYRHWORDSCOUNT
                , new SqlParameter("@UserSk", student.StudentId)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId)
                );
            return a;
        }

        /// <summary>
        /// Returns the total number of words circled by the Student / total number of recordings done in overall.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override int GetMyWordsCount_MyRecordingsCount(Student student)
        {
            var a = Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_GETMYWORDSCOUNT_MYRECORDINGSCOUNT
                , new SqlParameter("@UserSk", student.StudentId)
                , new SqlParameter("@CustSubsSk", student.SubscriptionId)
                , new SqlParameter("@Type", student.ActionType)
                ));
            return a;
        }

        /************************** READING HISTORY END *********************************/
        /// <summary>
        /// Adds students to group or class
        /// </summary>
        /// <param name="studentList"></param>
        /// <returns></returns>
        public override int AddStudentToGroup(List<Student> studentList)
        {
            int result = 0;
            foreach (Student student in studentList)
            {
                result+= int.Parse(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_ADDSTUDENTTOGROUPS
                           , new SqlParameter("@GroupId", student.ClassId)
                           , new SqlParameter("@Active", student.Active)
                           , new SqlParameter("@UserModified", student.UserModified)
                           , new SqlParameter("@DateModified", student.DateModified)
                           , new SqlParameter("@CustomerSubUserSK", student.CustSubUserSk)
                           , new SqlParameter("@SelectedCustSubSk", student.SubscriptionId)
                           ).ToString());
            }
            return result;
        }

        /// <summary>
        ///  Get Reading level history of a student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override IDataReader GetReadingLevelHistory(Student student)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETREADINGLEVEL, new SqlParameter("@StudentId", student.StudentId));
        }

        /// <summary>
        /// Get all the error messages of the student module
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public override IDataReader GetErrorMessagesByModuleName(string moduleName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETERRORMESSAGES, new SqlParameter("@Module_Name", moduleName));
        }

        /// <summary>
        /// Get the min and max Reading level
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="grfrefdatefrom"></param>
        /// <param name="grfRefDateTo"></param>
        /// <returns></returns>
 		public override IDataReader GetMinMaxReadingLevelByDate(int groupId, DateTime grfRefDateFrom, DateTime grfRefDateTo)
        {
            return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETMINMAXREADLEVELBYDATE, new SqlParameter("@StudentID", groupId), new SqlParameter("@Datefrom", grfRefDateFrom), new SqlParameter("@Dateto", grfRefDateTo));
        }

        /// <summary>
        /// Get all unallocated students of the particular account / user group
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override SqlDataReader GetAllUnallocatedStudents(Student student)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETALLUNALLOCATEDSTUDENTS, new SqlParameter("@CUST_SUBS_SK", student.SubscriptionId), new SqlParameter("@LOGINUSERNAME", student.TeacherLoginName), new SqlParameter("@FINDENTRY", student.SearchText), new SqlParameter("@Start", student.Start), new SqlParameter("@End", student.End));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Update status of license allocation
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public override int UpdateStudentLicenseAllocation(Student student)
        {
            try
            {
                return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETUPDATESTUDENTLICENSEALLOCATION, new SqlParameter("@CustSubsSk", student.SubscriptionId), new SqlParameter("@StudentSk", student.StudentId), new SqlParameter("@CurrentState", student.ActionType), new SqlParameter("@TeacherName", student.TeacherLoginName)));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        ///  Get all Active subscriptions
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="subsSk"></param>
        /// <returns></returns>
        public override SqlDataReader GetAllActiveSubcriptions(string Loginname, int subsSk)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETACTIVESUBSCRIPTIONSLIST, new SqlParameter("@LoginName", Loginname), new SqlParameter("@CurrentSubsSk", subsSk), new SqlParameter("@Active", "Y"));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Switch student subscriptions
        /// </summary>
        /// <param name="loginname"></param>
        /// <param name="subsSk"></param>
        /// <returns></returns>
        public override int SwitchStudentSubcriptions(Student student)
        {
            try
            {
                return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_SWITCHSTUDENTSUBSCRIPTION, new SqlParameter("@StudentSk", student.StudentId), new SqlParameter("@NewSubsSk", student.NewSubsSk), new SqlParameter("@CurrentSubSSk", student.SubscriptionId), new SqlParameter("@TeacherName", student.TeacherLoginName)));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Inserts data into util email table to send email on an occurrence of any exception
        /// </summary>
        /// <returns>List of countries as Dataset</returns>
        public override void SendExceptionMail(string body, string errorMessage, int subsSk)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, Constants.sp_EXCEPTIONMAIL,
                                                new SqlParameter("@body", body),
                                                new SqlParameter("@ErrorMessage", errorMessage),
                                                new SqlParameter("@DateCreated", DateTime.Now),
                                                new SqlParameter("@toaddress", ConfigurationManager.AppSettings["ExceptionMailToAddress"]),
                                                new SqlParameter("@SubsSk", subsSk));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }       
        #endregion

    }

}