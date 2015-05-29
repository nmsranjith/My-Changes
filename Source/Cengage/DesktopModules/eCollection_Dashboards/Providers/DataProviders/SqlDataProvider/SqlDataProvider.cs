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
using DotNetNuke.Modules.eCollection_Dashboards.Components.Common;
using System.Data.SqlClient;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Configuration;
using System.Xml;

namespace DotNetNuke.Modules.eCollection_Dashboards.Data
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
        private const string ModuleQualifier = "eCollection_Dashboards_";

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
        /// Get logged in user details
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public override IDataReader UserDetails(Users user)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETUSERDETAIL, new SqlParameter("@LoginName", user.UserLoginName), new SqlParameter("@subsId", user.SubscriptionId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               // GetConnection().Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override DataTable GetSubscriptionsList(Users user)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETSUBSCRIPTIONSLIST
                       , new SqlParameter("@LoginName", user.UserLoginName)
                       , new SqlParameter("@Active",user.Active)
                       ).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public override int AddSubscriptionName(Subscription subscription)
        {
            try
            {
                return int.Parse(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_ADDSUBSCRIPTIONAME
                       , new SqlParameter("@SubsSk", subscription.SubsId)
                       , new SqlParameter("@Name", subscription.Name)
                       , new SqlParameter("@UserModified", subscription.AdminName)
                       , new SqlParameter("@DateModified", DateTime.Now)
                       ).ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <returns></returns>
        public override IDataReader SubscriptionDetails(int subsId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETSUBSCRIPTIONDETAIL, new SqlParameter("@subsId", subsId), new SqlParameter("@Type", "SUBSCRIPTION"), new SqlParameter("@CurrentDate", DateTime.Now));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <returns></returns>
        public override IDataReader GetPurchaseDetails(int subsId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETSUBSCRIPTIONDETAIL, new SqlParameter("@subsId", subsId), new SqlParameter("@Type", "PURCHASE"), new SqlParameter("@CurrentDate", DateTime.Now));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subsId"></param>
        /// <returns></returns>
        public override IDataReader GetStartedDetails(int subsId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETSUBSCRIPTIONDETAIL, new SqlParameter("@subsId", subsId), new SqlParameter("@Type", "GETSTARTED"), new SqlParameter("@CurrentDate", DateTime.Now));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public override IDataReader GetAllDates(Subscription subscription)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETALLACTIVITYDATES, new SqlParameter("@subsId", subscription.SubsId), new SqlParameter("@LoginName", subscription.AdminName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        public override IDataReader GetDailyActivities(Subscription subscription)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETALLACTIVITIES, new SqlParameter("@subsId", subscription.SubsId), new SqlParameter("@LoginName", subscription.AdminName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override int VideoGetStartedClose(Users user)
        {
            return int.Parse(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_VIDEOGETSTARTEDUPDATEFLAG, new SqlParameter("@LoginName", user.UserLoginName), new SqlParameter("@VideoFlag", user.VideoFlag), new SqlParameter("@GetStartedFlag", user.GetStartedFlag),new SqlParameter("@SubsId", user.SubscriptionId)).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriptions"></param>
        /// <returns></returns>
        public override IDataReader GetUpgradedActivities(Subscription subscription)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETALLACTIVITIES, new SqlParameter("@subsId", subscription.SubsId), new SqlParameter("@LoginName", subscription.AdminName), new SqlParameter("@ActivityType", subscription.ActivityType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriptions"></param>
        /// <returns></returns>
        public override IDataReader GetAllBooksAdded(Subscription subscription)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETALLACTIVITIES, new SqlParameter("@subsId", subscription.SubsId), new SqlParameter("@LoginName", subscription.AdminName), new SqlParameter("@ActivityType", subscription.ActivityType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserCreated"></param>
        /// <param name="RenewalType"></param>
        /// <param name="NewCustSubs"></param>
        /// <param name="OldCustSubs"></param>
        /// <returns></returns>
        public override int StartAfterRenewal(string UserCreated, string RenewalType, int NewCustSubs, int OldCustSubs)
        {
            SqlParameter returnValue = new SqlParameter("returnValue", System.Data.SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;

            Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_AFTERRENEWAL
                , new SqlParameter("@USER_CREATED", UserCreated)
                , new SqlParameter("@RENEW_TYPE", RenewalType)
                , new SqlParameter("@NEW_CUST_SUBS_SK", NewCustSubs)
                , new SqlParameter("@OLD_CUST_SUBS", OldCustSubs)
                , returnValue
                ));

            Int32 rowsaffected = Convert.ToInt32(returnValue.Value);
            if (rowsaffected == -1)
            {
                throw new Exception();
            }

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="NewCustSubs"></param>
        /// <param name="OldCustSubs"></param>
        /// <returns></returns>
        public override int GetRenewalLicenseCount(string loginName, int NewCustSubs, int OldCustSubs)
        {
            SqlParameter returnValue = new SqlParameter("returnValue", System.Data.SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;

            Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.SP_GETRENEWALLICENSECOUNT
                , new SqlParameter("@USER_CREATED", loginName)
                  , new SqlParameter("@NEW_CUST_SUBS_SK", NewCustSubs)
                , new SqlParameter("@OLD_CUST_SUBS", OldCustSubs)
                , returnValue
                ));

            Int32 rowsaffected = Convert.ToInt32(returnValue.Value);

            return rowsaffected;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="NewCustSubs"></param>
        /// <param name="OldCustSubs"></param>
        /// <returns></returns>
        public override int UpdateInActiveAndArchive(string loginName, int NewCustSubs, int OldCustSubs)
        {
            SqlParameter returnValue = new SqlParameter("returnValue", System.Data.SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;

            Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.sp_UPDATEINACTIVEANDARCHIVE
                , new SqlParameter("@USER_CREATED", loginName)
                    , new SqlParameter("@NEW_CUST_SUBS_SK", NewCustSubs)
                , new SqlParameter("@OLD_CUST_SUBS", OldCustSubs)
                , returnValue
                ));

            Int32 rowsaffected = Convert.ToInt32(returnValue.Value);

            return rowsaffected;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserCreated"></param>
        /// <returns></returns>
        public override string GetRenewalSubscriptionName(string UserCreated)
        {
            SqlParameter returnValue = new SqlParameter("SUBS_ID", System.Data.SqlDbType.VarChar, 60);
            returnValue.Direction = ParameterDirection.Output;

            SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.sp_GETRENEWALSUBSCRIPTIONNNAME
                , new SqlParameter("@USER_CREATED", UserCreated)
                , returnValue
                );

            return returnValue.Value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserCreated"></param>
        /// <param name="NewCustSubs"></param>
        /// <param name="OldCustSubs"></param>
        /// <returns></returns>
        public override int GetRenewalBookCount(string UserCreated, int NewCustSubs, int OldCustSubs)
        {
            SqlParameter returnValue = new SqlParameter("returnValue", System.Data.SqlDbType.Int);
            returnValue.Direction = ParameterDirection.ReturnValue;

            SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, Constants.sp_GETRENEWALBOOKCOUNT
                , new SqlParameter("@USER_CREATED", UserCreated)
                     , new SqlParameter("@NEW_CUST_SUBS_SK", NewCustSubs)
                , new SqlParameter("@OLD_CUST_SUBS", OldCustSubs)
                , returnValue
                );

            Int32 rowsaffected = Convert.ToInt32(returnValue.Value);

            return rowsaffected;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserCreated"></param>
        /// <returns></returns>
        public override IDataReader GetAllRenewels(int subsid)//string UserCreated)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.sp_GETALLRENEWELS, new SqlParameter("@CustSubsSK",subsid));//"@USER_CREATED", UserCreated));
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
                throw exception;
            }
        }

        /// <summary>
        /// Method to get List Of Countries
        /// </summary>
        /// <returns>List of countries as Dataset</returns>
        public override DataTable GetCountries()
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, "spGetCountries").Tables[0];
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Method to get Account Details
        /// </summary>
        /// <returns>Account Details as Dataset</returns>
        public override DataTable GetAccountDetails()
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETACCOUNTINFO).Tables[0];
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Method to get Account Details
        /// </summary>
        /// <returns>Account Details as Dataset</returns>
        /// 
        public override DataSet GetReportInfo(DateTime fromdate, DateTime todate, int accountsk, string reportType)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETREPORTINFO, new SqlParameter("@FromDate", fromdate), new SqlParameter("@ToDate", todate), new SqlParameter("@ACCOUNTSK", accountsk), new SqlParameter("@ReportType", reportType));
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Account_Sk"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public override DataSet GetAccountInformation(int Account_Sk, DateTime fromdate, DateTime todate)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETACCOUNTDETAILS, new SqlParameter("@Account_Sk", Account_Sk), new SqlParameter("@FromDate", fromdate), new SqlParameter("@ToDate", todate));
            }
            catch (Exception exception)
            {
                throw exception;
            }
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
        /// <param name="teacher"></param>
        /// <returns></returns>
        public override int UploadTeacherProfiles(XmlDocument doc)
        {
            SqlConnection uploadConnection = new SqlConnection(_connectionString);
            uploadConnection.Open();
            SqlTransaction uploadTransaction = uploadConnection.BeginTransaction();
            try
            {
                int result = Null.SetNullInteger(SqlHelper.ExecuteScalar(uploadTransaction, Constants.SP_USERSBULKUPLOAD
                           , new SqlParameter("@xmlDoc", doc.InnerXml)
                           , new SqlParameter("@AddedDate", DateTime.Now)
                           ).ToString());

                if (result > 0)
                {
                    uploadTransaction.Commit();
                    return result;
                }
                else if (result == -222)
                {
                    throw new Exception("Teacher's email ids must be unique.");
                }
                uploadTransaction.Rollback();
                return 0;
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
        /// <param name="subs"></param>
        /// <returns></returns>
        public override DataSet  ValidateSubs(int subs)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_VALIDATESUBS,                     
                    new SqlParameter("@SubsId", subs));
                
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Migrate Renewal Subscription 
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public override bool MigrateRenewalSubscription(UpgradeFlags flags)
        {
            try
            {
                int result = Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_RENEWALDATAMIGRATION,
                    new SqlParameter("@CurrentCustSubSK", flags.CustSubsSk),
                    new SqlParameter("@MoveTeachers", flags.TeacherFlag),
                    new SqlParameter("@MoveStudents", flags.StudentFlag),
                    new SqlParameter("@MoveGroups", flags.GroupsFlag),
                    new SqlParameter("@MoveBooks", flags.BooksFlag),
                    new SqlParameter("@MoveOneYearUp", flags.UpgradeYearLevel),
                    new SqlParameter("@UserLoginName", flags.UserName),
                    new SqlParameter("@ActionType", flags.ActionType)
                     ));
                if (result == 1)
                    return true;
                else return false;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public override bool UpgradeSubscription(UpgradeFlags  flags)
        {
            try
            {
                int result = Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_UPGRADEDATMIGRATION,
                    new SqlParameter("@CurrentCustSubSK", flags.CustSubsSk),
                    new SqlParameter("@MoveTeachers", flags.TeacherFlag),
                    new SqlParameter("@MoveStudents", flags.StudentFlag),                    
                    new SqlParameter("@MoveGroups", flags.GroupsFlag),
                    new SqlParameter("@MoveBooks", flags.BooksFlag),
                    new SqlParameter("@MoveOneYearUp", flags.UpgradeYearLevel),
                    new SqlParameter("@UserLoginName", flags.UserName),
                    new SqlParameter("@ActionType", flags.ActionType)
                     ));
                if (result == 1)
                    return true;
                else return false;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="audio"></param>
        /// <returns></returns>
        public override int InsertAudioFiles(Audio audio)
        {
            try
            {
                return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_INSERTAUDIODETAILS, new SqlParameter("@AudioXml", audio.AudioXml.InnerXml),
                    new SqlParameter("@ISBN", audio.ISBN),
                    new SqlParameter("@FileType", audio.FileType)).ToString());
            }
            catch(Exception ex){throw ex;}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public override string GetUserinfo(string guid)
        {
            try
            {
                string results = string.Empty;
                IDataReader Records = SqlHelper.ExecuteReader(ConnectionString, CommandType.StoredProcedure, Constants.Ecomm_GetUserInfo, new SqlParameter("@Guid", guid));
                while (Records.Read())
                {
                    results = Records["SerializedKey"].ToString();
                }
                return results;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        #endregion


    }

}