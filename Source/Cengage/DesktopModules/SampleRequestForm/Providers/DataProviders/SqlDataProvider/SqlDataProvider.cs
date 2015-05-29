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
using DotNetNuke.Modules.SampleRequestForm.Components.Modal;
using Microsoft.ApplicationBlocks.Data;

namespace DotNetNuke.Modules.SampleRequestForm.Data
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
        private const string ModuleQualifier = "SampleRequestForm_";

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
        /// Send mail to sales representative
        /// </summary>
        /// <param name="toAddress"></param>
        /// <param name="mailBody"></param>
        /// <param name="mailSubject"></param>
        /// <returns></returns>
        public override int SRFSendMail(string toAddress, string mailBody, string mailSubject)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(ConnectionString, "ECOMM_SRF_SEND_EMAIL_SALES_REP"
                                                  , new SqlParameter("@toAddress", toAddress)
                                                  , new SqlParameter("@mailBody", mailBody)
                                                  , new SqlParameter("@mailSubject", mailSubject));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Save Instructor Info
        /// </summary>
        /// <param name="user"></param>
        /// <returns>returns integer</returns>
        public override int SaveSSOInstructorInfo(SRFParameters user)
        {
            try
            {
              /*  SampleRequestFormModuleBase.LogValues("USER_SK-->" + user.UserSk
               + " COURSE_NAME-->" + user.CourseName
               + " COURSE_ID-->" + user.CourseCode
               + " SEMESTER-->" + user.Semester
               + " ENROLEMENT-->" + user.Enrolment
               + " BOOKS_IN_USE-->" + user.BookInUse
               + " SUBURB-->" + user.SubUrb
               + " STATE-->" + user.State
               + " INSTITUTION-->" + user.Institution
               + " MOBILE-->" + user.Mobile
               + " WORK-->" + user.Contact_Email
               + " CONTACT_EMAIL-->" + user.Name
               + " ADDRESSLINE1-->" + user.Address1
               + " ADDRESSLINE2-->" + user.Address2
               + " ADDRESSLINE3-->" + user.Address3
               + " POSTALCODE-->" + user.PostalCode
               + " COUNTRY_CODE-->" + user.Country
               + " HOWYOUKNOW-->" + user.HowYouKnow);*/

               return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, "ECOMM_INSERT_INSTRUCTOR_DETAILS"
                                                , new SqlParameter("@USER_SK", user.UserSk)
                                                , new SqlParameter("@COURSE_NAME", user.CourseName)
                                                , new SqlParameter("@COURSE_ID", user.CourseCode)
                                                , new SqlParameter("@SEMESTER", user.Semester)
                                                , new SqlParameter("@ENROLEMENT", user.Enrolment)
                                                , new SqlParameter("@BOOKS_IN_USE", user.BookInUse)
                                                , new SqlParameter("@STATE", user.State)
                                                , new SqlParameter("@SUBURB", user.SubUrb)
                                                , new SqlParameter("@INSTITUTION", user.Institution)
                                                , new SqlParameter("@MOBILE", user.Mobile)
                                                , new SqlParameter("@WORK", user.Work)
                                                , new SqlParameter("@CONTACT_EMAIL", user.Contact_Email)
                                                , new SqlParameter("@NAME", user.Name)
                                                , new SqlParameter("@ADDRESSLINE1", user.Address1)
                                                , new SqlParameter("@ADDRESSLINE2", user.Address2)
                                                , new SqlParameter("@ADDRESSLINE3", user.Address3)
                                                , new SqlParameter("@POSTALCODE", user.PostalCode)
                                                , new SqlParameter("@COUNTRY_CODE", user.Country)
                                                , new SqlParameter("@HOWYOUKNOW", user.HowYouKnow)
                                                , new SqlParameter("@STATETEXT", user.StateText)
                                                , new SqlParameter("@IS_ADDRESS_EXISTS", user.IsAddressExist)));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        ///  Get Instructor Info
        /// </summary>
        /// <param name="UserLoginName"></param>
        /// <returns></returns>
        public override IDataReader GetInstructorInfo(string UserLoginName)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, "[ECOMM_GET_INSTRUCTOR_DETAILS]"
                                                  , new SqlParameter("@USERLOGINNAME", UserLoginName));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string InsertOrders(SRFParameters sRFParameters, DataTable orderDt, string typeOfSale, string FormatType)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            SqlParameter orderTvp = null;
            try
            {
                con = new SqlConnection(ConnectionString);
                orderTvp = new SqlParameter();
                orderTvp.ParameterName = "@ORDER_DETAIL_TVP";
                orderTvp.Value = orderDt;
                orderTvp.SqlDbType = SqlDbType.Structured;
                orderTvp.TypeName = "ORDER_DETAIL_TVP";
                con.Open();
                com = new SqlCommand("ECOMM_INSERT_SAMPLE_ORDERS", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add(new SqlParameter("@TradingPartenerAccountSk", sRFParameters.TradingPartenerAccountSk));
                com.Parameters.Add(new SqlParameter("@StoreSk", sRFParameters.StoreSk));
                com.Parameters.Add(new SqlParameter("@UserSk", sRFParameters.UserSk));
                com.Parameters.Add(new SqlParameter("@Currency", sRFParameters.Currency));
                com.Parameters.Add(new SqlParameter("@Price", sRFParameters.Price));
                com.Parameters.Add(new SqlParameter("@Email", sRFParameters.Email));
                com.Parameters.Add(new SqlParameter("@AddressDetailSk", sRFParameters.AddressDetailSk));
                com.Parameters.Add(new SqlParameter("@TypeOfPack", typeOfSale));
                com.Parameters.Add(new SqlParameter("@UserAgent", sRFParameters.UserAgent));
                com.Parameters.Add(new SqlParameter("@FormatType", FormatType));
                com.Parameters.Add(orderTvp);
                SqlParameter OutPutParameter = new SqlParameter("@new_identity", SqlDbType.VarChar,20);
                OutPutParameter.Direction = ParameterDirection.Output;
                com.Parameters.Add(OutPutParameter);
                com.ExecuteNonQuery();
               // SampleRequestFormModuleBase.LogValues("ORDER NUMBER-->"+Null.SetNullString(OutPutParameter.Value));
                return Null.SetNullString(OutPutParameter.Value);

            }
            catch (Exception ex)
            {
               SampleRequestFormModuleBase.LogFileWrite(ex);
               throw ex;               
            }
        }
        #endregion

    }

}