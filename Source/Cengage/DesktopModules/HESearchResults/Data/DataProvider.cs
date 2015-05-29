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
using DotNetNuke.Modules.HESearchResults.Components;
using DotNetNuke.Modules.HESearchResults.Components.Modal;
using DotNetNuke.Modules.HESearchResults.Controls;


namespace DotNetNuke.Modules.HESearchResults.Data
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
                const string assembly = "DotNetNuke.Modules.HESearchResults.Data.SqlDataprovider,HESearchResults";
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

        public abstract SqlDataReader GetProductResults(SearchParameters sParams);
        public abstract SqlDataReader GetGaleProductResults(SearchParameters sParams);
        public abstract SqlDataReader GetAdvanceSearchProductResults(SearchParameters sParams);
        public abstract SqlDataReader GetGaleAdvanceSearchProductResults(SearchParameters sParams);
        public abstract SqlDataReader GetCmsResults(SearchParameters sParams);
        public abstract SqlDataReader GetHEAdvanceIsbnResults(SearchParameters sParams);
        public abstract SqlDataReader GetGaleAdvanceIsbnResults(SearchParameters sParams);
        public abstract int SaveSearchAndFavorites(SearchParameters sParams);
        public abstract int UpdateSaveSearchAndFavorites(SearchParameters sParams);
        public abstract bool CheckForHEDidYouMeanWordExist(string word, string division, string country);

        //school ecommerce
        public abstract DataSet SelectCmsResult(string Division, string Keywords, int ItemCount, int PageNumber, int Storesk);
        public abstract string GetUserRole(int usersk);
        public abstract DataSet GetQuoteProducts(int userListQuoteSK);
        public abstract IDataReader GetProduct(string flag, string userListQuoteSK, int? userSk, string genericFlag);
        public abstract IDataReader GetFacetResultsAttributes(SearchEngine searchEngine);
        public abstract DataSet GetExistingQuotes(int usersk, string Role, int TradeSK);
        public abstract SqlDataReader GetComponentResults(SearchEngine searchEngine);
        public abstract SqlDataReader GetAdvanceIsbnResults(SearchEngine searchEngine);
        public abstract void EditQuoteDetails(QuoteInfo Quote);
        public abstract SqlDataReader GetAdvanceKeywordResults(SearchEngine searchEngine);
        public abstract DataSet SelectSearchStudProd(int PrdSk, string division, int store_sk, string country);
        public abstract DataSet SelectStdPrd(int PrdSk, string division);
        public abstract int AddListQuote(string flag, string _userSK, string _productSKQuantity, string _userListQuoteSK, string _genericFlag, ref string _output);
        public abstract bool CheckResultsForCorrectedWord(string word, string division, int storeSk);
        #endregion

    }

}