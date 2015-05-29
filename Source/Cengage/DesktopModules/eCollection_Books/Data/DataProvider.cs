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
using System.Xml;
using System.Data.SqlClient;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;


namespace DotNetNuke.Modules.eCollection_Books.Data
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
                const string assembly = "DotNetNuke.Modules.eCollection_Books.Data.SqlDataprovider,eCollection_Books";
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

        public abstract IDataReader GetBooks(int subId, string loginName);
        public abstract IDataReader GetBooksGracePeriod(int subId,string loginName);
        public abstract IDataReader GetReadingAge(int subId,string Years, string loginName);
        public abstract IDataReader GetReadingAgeBooks(int subId, string AttributeType, string AttributeValue);
        public abstract IDataReader GetReadingCategories(int subId, string AttributeType, string loginName);
        public abstract IDataReader GetLevels(int subId, string AttributeType, string AttributeValue, string loginName);
        public abstract int AddBooksToSubscription(int subId, XmlDocument products, string loginName);
        public abstract int RemoveBooksfromSubscription(int subId, XmlDocument products, string loginName);
        public abstract IDataReader GetBooksByCategories(string AttributeType);

        public abstract IDataReader GetBooksByReadingLevel(int subId, int fromReadLvl, int toReadLvl,string LoginName);
        public abstract IDataReader IsBooksAdded(int subId, XmlDocument products );
        public abstract IDataReader GetErrorMessagesByModuleName(string Module_Name);
        public abstract IDataReader GetBooksCategories(int subsId, string Attribute_Type, string userLoginName);
        public abstract IDataReader GetBookListByReadingLevel(int subId, int fromReadLvl, int toReadLvl, string LoginName);
        public abstract IDataReader GetSearchGroup(string searchString, int custSubId, string Attribute_Type);
        public abstract IDataReader GetBookListByReadingAge(int subId, string AttributeType, string AttributeValue);
        public abstract IDataReader GetBookListBySearch(string searchString, int custSubId, string Attribute_Type);
        public abstract int AutoAssignBooks(int subId, string loginName);
        public abstract IDataReader GetSelectedBooksCount(string Values, string Type, int SubId);
        public abstract IDataReader GetBooksCountForPageRedirection(int subId);
        public abstract void RunScheduleForGracePeriodNotification(DateTime today);
        public abstract SqlDataReader GetBookPacks(BookPack bookPack);
        public abstract SqlDataReader GetBookPackeBooks(int bookPackID, string option);
        public abstract SqlDataReader GetCustomPackeBooks(int CustSubSk, string option);
        public abstract SqlDataReader SearchBookPackeBooks(int bookPackID, string searchText, string conditionText, string weightageText,string option);
        public abstract SqlDataReader SearchCustomPackeBooks(int CustSubSk, string searchText, string conditionText, string weightageText, string option);
        public abstract int SaveCustomBookPack(BookPack customBookPack);
        public abstract int SetBookPack(BookPack customBookPack);
        public abstract int GetBookPackStatus(int custsubsk,string userName);
        #region Exception Mail
        public abstract void SendExceptionMail(string body, string errorMessage, int SubsSk);
        #endregion

        #endregion

    }

}