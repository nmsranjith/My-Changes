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
using DotNetNuke.Modules.eCollection_Books.Components.Common;
using System.Xml;
using System.Configuration;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;

namespace DotNetNuke.Modules.eCollection_Books.Data
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
        private const string ModuleQualifier = "eCollection_Books_";

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
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override IDataReader GetBooks(int subId, string loginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKS, new SqlParameter("@CUST_SUBS_SK", subId), new SqlParameter("@LoginName", loginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override IDataReader GetBooksGracePeriod(int subId, string loginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETGRACEPERIOD, new SqlParameter("@SubId", subId), new SqlParameter("@LoginName", loginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="years"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override IDataReader GetReadingAge(int subId,string years, string loginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETREADINGAGE, new SqlParameter("@SubId", subId), new SqlParameter("@Years", years), new SqlParameter("@LoginName", loginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="AttributeType"></param>
        /// <param name="AttributeValue"></param>
        /// <returns></returns>
        public override IDataReader GetReadingAgeBooks(int subId, string AttributeType, string AttributeValue)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETREADINGAGEBOOKS, new SqlParameter("@CUST_SUBS_SK", subId), new SqlParameter("@ATTRIBUTE_TYPE", AttributeType), new SqlParameter("@ATTRIBUTE_VALUE", AttributeValue));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="AttributeType"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override IDataReader GetReadingCategories(int subId, string AttributeType, string loginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETCATEGORIES, new SqlParameter("@SubId", subId), new SqlParameter("@ATTRIBUTE_TYPE", AttributeType), new SqlParameter("@LoginName", loginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AttributeType"></param>
        /// <returns></returns>
        public override IDataReader GetBooksByCategories(string AttributeType)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKSBYCATEGORIES, new SqlParameter("@ATTRIBUTE_TYPE", AttributeType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="AttributeType"></param>
        /// <param name="AttributeValue"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override IDataReader GetLevels(int subId, string AttributeType, string AttributeValue,string loginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETLEVELS, new SqlParameter("@SubId", subId), new SqlParameter("@ATTRIBUTE_TYPE", AttributeType), new SqlParameter("@COLOUR_VALUE", AttributeValue), new SqlParameter("@LoginName", loginName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="products"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override int AddBooksToSubscription(int subId, XmlDocument products, string loginName)
        {
            SqlConnection uploadConnection = new SqlConnection(_connectionString);
            uploadConnection.Open();
            SqlTransaction uploadTransaction = uploadConnection.BeginTransaction();
            try
            {
                int result = 0;
                result += int.Parse(SqlHelper.ExecuteScalar(uploadTransaction, Constants.SP_ADDSUBSCRIPTION
                            , new SqlParameter("@CUST_SUBS_SK", subId)
                           , new SqlParameter("@xmlDoc", products.InnerXml)
                           , new SqlParameter("@AddedDate", DateTime.Now)
                           , new SqlParameter("@UserCreated", loginName)
                           ).ToString());

                if (result == 1)
                {
                    uploadTransaction.Commit();
                    return result;
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
        /// <param name="subId"></param>
        /// <param name="products"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override int RemoveBooksfromSubscription(int subId, XmlDocument products, string loginName)
        {
            SqlConnection uploadConnection = new SqlConnection(_connectionString);
            uploadConnection.Open();
            SqlTransaction uploadTransaction = uploadConnection.BeginTransaction();
            try
            {
                int result = 0;
                result += int.Parse(SqlHelper.ExecuteScalar(uploadTransaction, Constants.SP_REMOVESUBSCRIPTION
                            , new SqlParameter("@CUST_SUBS_SK", subId)
                           , new SqlParameter("@xmlDoc", products.InnerXml)
                           , new SqlParameter("@UserCreated", loginName)
                           ).ToString());

                if (result == 1)
                {
                    uploadTransaction.Commit();
                    return result;
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
        /// <param name="subId"></param>
        /// <param name="fromReadLvl"></param>
        /// <param name="toReadLvl"></param>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        public override IDataReader  GetBooksByReadingLevel(int subId, int fromReadLvl, int toReadLvl,string LoginName)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKSBYREADLEVEL, new SqlParameter("@CUST_SUBS_SK", subId), new SqlParameter("@FromReadingLevel", fromReadLvl), new SqlParameter("@ToReadingLevel", toReadLvl), new SqlParameter("@UserCreated", LoginName));
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
        /// <param name="subId"></param>
        /// <param name="AttributeType"></param>
        /// <param name="AttributeValue"></param>
        /// <returns></returns>
        public override IDataReader GetBookListByReadingAge(int subId, string AttributeType, string AttributeValue)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKLISTBYREADINGAGE, new SqlParameter("@CUST_SUBS_SK", subId), new SqlParameter("@ATTRIBUTE_TYPE", AttributeType), new SqlParameter("@ATTRIBUTE_VALUE", AttributeValue));
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
        /// <param name="subId"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public override IDataReader IsBooksAdded(int subId, XmlDocument products)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_ISBOOKSADDED, new SqlParameter("@CUST_SUBS_SK", subId), new SqlParameter("@xmlDoc", products.InnerXml));
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
        /// 
        /// </summary>
        /// <param name="subId"></param>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public override int AutoAssignBooks(int subId, string loginName)
        {           
            try
            {
                int.Parse(SqlHelper.ExecuteNonQuery(ConnectionString, Constants.SP_AUTOASSIGNBOOKS
                            , new SqlParameter("@SubId", subId)
                           , new SqlParameter("@LoginName", loginName)
                           ).ToString());
                return 1;
            }
            catch (Exception ex)
            {               
                throw ex;
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Values"></param>
        /// <param name="Type"></param>
        /// <param name="SubId"></param>
        /// <returns></returns>
        public override IDataReader  GetSelectedBooksCount(string Values, string Type, int SubId)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETSELECTEDBOOKCOUNT
                            , new SqlParameter("@Values", Values)
                            , new SqlParameter("@Type", Type)
                            , new SqlParameter("@CustSubsSK", SubId)
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
        /// <param name="subId"></param>
        /// <returns></returns>
        public override IDataReader GetBooksCountForPageRedirection(int subId)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKSCOUNTFORREDIRECTION,                           
                            new SqlParameter("@SubSk", subId)
                           );
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="today"></param>
        public override void RunScheduleForGracePeriodNotification(DateTime today)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(ConnectionString, Constants.sp_GRACEPERIODNOTIFICATIONMAIL,
                                                new SqlParameter("@Date", today.ToString("yyyy-MM-dd"))
                                              );
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Get all book packs
        /// </summary>
        /// <param name="bookPack"></param>
        /// <returns></returns>
        public override SqlDataReader GetBookPacks(BookPack bookPack)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKPACK,                                                
                                                new SqlParameter("@subscriptionId", bookPack.SubscriptionId));
            }
            catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Get all the eBooks of a book pack
        /// </summary>
        /// <param name="bookPackID"></param>
        /// <returns></returns>
        public override SqlDataReader GetBookPackeBooks(int bookPackID,string option)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETBOOKPACKBOOKS,
                                                new SqlParameter("@BookPackID", bookPackID),
                                                new SqlParameter("@Option", option));
            }
            catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Get all the eBooks of a custom book pack
        /// </summary>
        /// <param name="bookPackID"></param>
        /// <returns></returns>
        public override SqlDataReader GetCustomPackeBooks(int CustSubSk, string option)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_GETCUSTOMPACKBOOKS,
                                                new SqlParameter("@CustSubSk", CustSubSk),
                                                new SqlParameter("@Option", option));
            }
            catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Search eBooks of a Book pack across title,Reading level,Reading age,Text type
        /// </summary>
        /// <param name="bookPackID"></param>
        /// <returns></returns>
        public override SqlDataReader SearchBookPackeBooks(int bookPackID, string searchText, string conditionText, string weightageText, string option)
        {
            try
            {
                //\"",searchText.Replace("\"",string.Empty),"\" WEIGHT(1), "
                if (conditionText==string.Empty && searchText.StartsWith("\"") && searchText.EndsWith("\""))
                    conditionText = "\"" + searchText.Replace("\"", string.Empty) + "\"";
                weightageText = string.Concat(string.Concat("ISABOUT (", weightageText.TrimEnd(',')).TrimEnd(','), ") ");
                eCollection_BooksModuleBase.LogValues(string.Concat("\r\n bookPackID-->", bookPackID, " \r\n searchText-->", searchText, " \r\n conditionText-->", conditionText, " \r\n weightageText-->", weightageText, " \r\n Option-->", option));                
                
                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_SEARCHBOOKPACKBOOKS,
                                                new SqlParameter("@SearchText", searchText),
                                                new SqlParameter("@BookPackSk", bookPackID),
                                                new SqlParameter("@ConditionText", conditionText),
                                                new SqlParameter("@weighttext", weightageText),
                                                new SqlParameter("@Option", option));
            }
            catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Search eBooks of a custom pack across title,Reading level,Reading age,Text type
        /// </summary>
        /// <param name="bookPackID"></param>
        /// <returns></returns>
        public override SqlDataReader SearchCustomPackeBooks(int CustSubSk, string searchText, string conditionText, string weightageText, string option)
        {
            try
            {
                if (conditionText == string.Empty && searchText.StartsWith("\"") && searchText.EndsWith("\""))
                    conditionText = "\""+searchText.Replace("\"", string.Empty)+"\"";
                weightageText = string.Concat(string.Concat("ISABOUT (", weightageText.TrimEnd(',')).TrimEnd(','), ") ");
                eCollection_BooksModuleBase.LogValues(string.Concat("\r\n CustSubSk-->", CustSubSk, " \r\n searchText-->", searchText, " \r\n conditionText-->", conditionText, " \r\n weightageText-->", weightageText, " \r\n Option-->", option));

                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_SEARCHCUSTOMPACKBOOKS,
                                                new SqlParameter("@SearchText", searchText),
                                                new SqlParameter("@CustSubSk", CustSubSk),
                                                new SqlParameter("@ConditionText", conditionText),
                                                new SqlParameter("@weighttext", weightageText),
                                                new SqlParameter("@Option", option));
            }
            catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Save custom Book Pack
        /// </summary>
        /// <param name="customBookPack"></param>
        /// <returns></returns>
        public override int SaveCustomBookPack(BookPack customBookPack)
        {
            try
            {
                eCollection_BooksModuleBase.LogValues(string.Concat(" \r\n custsubsk-->", customBookPack.SubscriptionId, " \r\n ISBN-->", customBookPack.ISBN, " \r\n UserName-->", customBookPack.UserName));
                return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_SAVECUSTOMBOOKPACK,
                                                new SqlParameter("@custsubsk", customBookPack.SubscriptionId),
                                                new SqlParameter("@isbns", customBookPack.ISBN),
                                                new SqlParameter("@uncheckedisbns", customBookPack.UncheckedISBNs),
                                                new SqlParameter("@username", customBookPack.UserName)));
            }
            catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Set book pack to a subscription
        /// </summary>
        /// <param name="customBookPack"></param>
        /// <returns></returns>
        public override int SetBookPack(BookPack customBookPack)
        {
            try
            {
                eCollection_BooksModuleBase.LogValues(string.Concat(" \r\n custsubsk-->", customBookPack.SubscriptionId, " \r\n bookpacksk-->", customBookPack.BookPackSk, " \r\n UserName-->", customBookPack.UserName));
                return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_SETBOOKPACK,
                                                new SqlParameter("@custsubsk", customBookPack.SubscriptionId),
                                                new SqlParameter("@bookpacksk", customBookPack.BookPackSk),
                                                new SqlParameter("@username", customBookPack.UserName)));
            }
            catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Get book pack status
        /// </summary>
        /// <param name="customBookPack"></param>
        /// <returns></returns>
        public override int GetBookPackStatus(int custsubsk,string userName)
        {
            try
            {              
                return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_GETBOOKPACKSTATUS,
                                                new SqlParameter("@custsubsk", custsubsk),
                                                new SqlParameter("@username", userName)));
            }
            catch (Exception ex) { throw; }
        }
        #endregion

    }

}