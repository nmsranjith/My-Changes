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
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Xml;


namespace DotNetNuke.Modules.eCollection_Dashboards.Data
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
                const string assembly = "DotNetNuke.Modules.eCollection_Dashboards.Data.SqlDataprovider,eCollection_Dashboards";
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

        public abstract DataTable GetSubscriptionsList(Users user);
        public abstract int AddSubscriptionName(Subscription subscription);
        public abstract IDataReader UserDetails(Users user);
        public abstract IDataReader SubscriptionDetails(int subsId);
        public abstract IDataReader GetPurchaseDetails(int subsId);
        public abstract IDataReader GetStartedDetails(int subsId);
        public abstract IDataReader GetAllDates(Subscription subscription);
        public abstract IDataReader GetDailyActivities(Subscription subscription);
        public abstract int VideoGetStartedClose(Users user);
        public abstract IDataReader GetAllBooksAdded(Subscription subscriptions);
        public abstract IDataReader GetUpgradedActivities(Subscription subscriptions);
        public abstract IDataReader GetErrorMessagesByModuleName(string Module_Name);
        public abstract int UploadTeacherProfiles(XmlDocument doc);
        public abstract DataSet  ValidateSubs(int subsid);
        public abstract bool UpgradeSubscription(UpgradeFlags Flags);
        public abstract bool MigrateRenewalSubscription(UpgradeFlags Flags);

        #region "After Renewal"
        public abstract int StartAfterRenewal(string UserCreated, string RenewalType, int NewCustSubs, int OldCustSubs);
        public abstract int GetRenewalLicenseCount(string UserCreated, int NewCustSubs, int OldCustSubs);
        public abstract int UpdateInActiveAndArchive(string UserCreated, int NewCustSubs, int OldCustSubs);
        public abstract string GetRenewalSubscriptionName(string UserCreated);
        public abstract int GetRenewalBookCount(string UserCreated, int NewCustSubs, int OldCustSubs);
        public abstract IDataReader GetAllRenewels(int subsid);//string UserCreated);

        #endregion

        #region Request Access
        public abstract DataTable GetCountries();
        #endregion

        #region App Data Usage
            public abstract DataTable GetAccountDetails();
            public abstract DataSet GetReportInfo(DateTime fromdate, DateTime todate, int accountsk, string reportType);
            public abstract DataSet GetAccountInformation(int Account_Sk, DateTime fromdate, DateTime todate);
        #endregion

        #region Exception Mail
            public abstract void SendExceptionMail(string body, string errorMessage, int SubsSk);
        #endregion

        #region PM Content Management
        public abstract int InsertAudioFiles(Audio audio);
        #endregion

        #region GetUserinfo
        public abstract string GetUserinfo(string guid);
        #endregion

        #endregion

    }

}