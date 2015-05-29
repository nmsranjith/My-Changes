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
using DotNetNuke.Modules.HESearchResults.Components.Common;
using DotNetNuke.Modules.HESearchResults.Components.Modal;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Modules.HESearchResults.Controls;
using System.Configuration;
using System.IO;
using DotNetNuke.Modules.HESearchResults.Components;
using DotNetNuke.Instrumentation;

namespace DotNetNuke.Modules.HESearchResults.Data
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
        private const string ModuleQualifier = "HESearchResults_";

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

        #region HE SEARCH Methods
        /// <summary>
        /// For Product Search Results
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public override SqlDataReader GetProductResults(SearchParameters sParams)
        {
            try
            {
                SqlParameter attrtvp = new SqlParameter();
                attrtvp.ParameterName = "@ATTRIBUTETVP";
                attrtvp.Value = sParams.FacetAttributesTable;
                attrtvp.SqlDbType = SqlDbType.Structured;
                attrtvp.TypeName = "AttributeSearchTVP";

                SqlParameter attrShowOnlytvp = new SqlParameter();
                attrShowOnlytvp.ParameterName = "@SHOWONLYTVP";
                attrShowOnlytvp.Value = sParams.ShowOnlyAttributesTable;
                attrShowOnlytvp.SqlDbType = SqlDbType.Structured;
                attrShowOnlytvp.TypeName = "SHOWONLYTVP";

                SqlParameter attrStatetvp = new SqlParameter();
                attrStatetvp.ParameterName = "@ATTRIBUTE_UI_STATE";
                attrStatetvp.Value = sParams.AttributeStateTable;
                attrStatetvp.SqlDbType = SqlDbType.Structured;
                attrStatetvp.TypeName = "HE_ATTRIBUTE_UI_STATE_INDEXED";

                string Weightage = string.Concat(string.Concat("ISABOUT (", "\"", sParams.SearchText.Replace("\"", string.Empty) + "\"", " WEIGHT (1),", sParams.Weightage.TrimEnd(',')).TrimEnd(','), ") ");
                string Condition = string.Empty;
                HESearchResultsModuleBase.LogValues("@TITLE_WEIGHTAGE" + sParams.TitleWeightage + "@AUTHOR_WEIGHTAGE" + sParams.AuthorWeightage);
                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                LogSearchInputs(sParams, Weightage);
                SqlCommand comm = null;
                switch (sParams.Country.ToUpper())
                {
                    case "NZ":
                        comm = new SqlCommand("ECOMM_HE_NZ_PRODUCT_SEARCH_ATTRIBUTE_STATE_IMPROVEMENT", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                    default:
                        comm = new SqlCommand("ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE_IMPROVEMENT", conn);
                        //comm = new SqlCommand("ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE_TEST_MODIFIED",conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                }
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@USER_SK", sParams.UserSk));
                comm.Parameters.Add(new SqlParameter("@PAGE_NO", sParams.PageNumber));
                comm.Parameters.Add(new SqlParameter("@DIVISION", sParams.Division));
                comm.Parameters.Add(new SqlParameter("@STORESK", sParams.StoreSK));
                comm.Parameters.Add(new SqlParameter("@CONDITION", sParams.Condition));
                comm.Parameters.Add(new SqlParameter("@SEARCHTEXT", sParams.SearchText));
                comm.Parameters.Add(new SqlParameter("@EXACTSEARCHTEXT", sParams.ExactText));
                comm.Parameters.Add(new SqlParameter("@WEIGHTAGE", Weightage));
                comm.Parameters.Add(new SqlParameter("@NO_OF_RECORDS_PER_PAGE", sParams.NumberOfResults));
                comm.Parameters.Add(new SqlParameter("@SORT_ORDER", sParams.SortOrder));
                comm.Parameters.Add(attrtvp);
                comm.Parameters.Add(attrShowOnlytvp);
                comm.Parameters.Add(attrStatetvp);
                comm.Parameters.Add(new SqlParameter("@TYPEOFDISCIPLINE", sParams.DisciplineType));
                comm.Parameters.Add(new SqlParameter("@VALUEOFCATEGORY", sParams.CategoryValue));
                comm.Parameters.Add(new SqlParameter("@VALUEOFDISCIPLINE", sParams.DisciplineValue));
                comm.Parameters.Add(new SqlParameter("@VALUEOFSUBJECT", sParams.SubjectValue));
                comm.Parameters.Add(new SqlParameter("@COUNTRY", sParams.Country));
                comm.Parameters.Add(new SqlParameter("@FORMS", sParams.FormsString));
                comm.Parameters.Add(new SqlParameter("@TITLE_WEIGHTAGE", sParams.TitleWeightage));
                comm.Parameters.Add(new SqlParameter("@AUTHOR_WEIGHTAGE", sParams.AuthorWeightage));
                comm.Parameters.Add(new SqlParameter("@GstApplicable", sParams.GstFlag));
                return comm.ExecuteReader();//CommandBehavior.CloseConnection);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// For Product Search Results
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public override SqlDataReader GetGaleProductResults(SearchParameters sParams)
        {
            try
            {
                SqlParameter attrtvp = new SqlParameter();
                attrtvp.ParameterName = "@ATTRIBUTETVP";
                attrtvp.Value = sParams.FacetAttributesTable;
                attrtvp.SqlDbType = SqlDbType.Structured;
                attrtvp.TypeName = "AttributeSearchTVP";

                SqlParameter attrShowOnlytvp = new SqlParameter();
                attrShowOnlytvp.ParameterName = "@SHOWONLYTVP";
                attrShowOnlytvp.Value = sParams.ShowOnlyAttributesTable;
                attrShowOnlytvp.SqlDbType = SqlDbType.Structured;
                attrShowOnlytvp.TypeName = "SHOWONLYTVP";

                SqlParameter attrStatetvp = new SqlParameter();
                attrStatetvp.ParameterName = "@ATTRIBUTE_UI_STATE";
                attrStatetvp.Value = sParams.AttributeStateTable;
                attrStatetvp.SqlDbType = SqlDbType.Structured;
                attrStatetvp.TypeName = "HE_ATTRIBUTE_UI_STATE_INDEXED";

                SqlParameter GaleAreaTable = new SqlParameter();
                GaleAreaTable.ParameterName = "@AREATVP";
                GaleAreaTable.Value = sParams.GaleAreaTable;
                GaleAreaTable.SqlDbType = SqlDbType.Structured;
                GaleAreaTable.TypeName = "AREATVP";

                SqlParameter GaleDisciplineTable = new SqlParameter();
                GaleDisciplineTable.ParameterName = "@DISCIPLINETVP";
                GaleDisciplineTable.Value = sParams.GaleDisciplineTable;
                GaleDisciplineTable.SqlDbType = SqlDbType.Structured;
                GaleDisciplineTable.TypeName = "DISCIPLINETVP";

                string Weightage = string.Concat(string.Concat("ISABOUT (", "\"", sParams.SearchText.Replace("\"", string.Empty) + "\"", " WEIGHT (1),", sParams.Weightage.TrimEnd(',')).TrimEnd(','), ") ");
                string Condition = string.Empty;

                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                LogSearchInputs(sParams, Weightage);
                SqlCommand comm = null;
                if (Null.SetNullString(sParams.BwsFormat) == string.Empty)
                {
                    switch (sParams.Country.ToUpper())
                    {
                        case "NZ":
                            comm = new SqlCommand("ECOMM_NZ_GALE_PRODUCT_SEARCH_ATTRIBUTE_STATE_IMPROVEMENT", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                            break;
                        default:
                            comm = new SqlCommand("ECOMM_GALE_PRODUCT_SEARCH_ATTRIBUTE_STATE_IMPROVEMENT", conn);
                            //comm = new SqlCommand("ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE_TEST_MODIFIED",conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                            break;
                    }
                }
                else
                {
                    switch (sParams.Country.ToUpper())
                    {
                        case "NZ":
                            comm = new SqlCommand("ECOMM_NZ_GALE_FORMAT_PRODUCT_SEARCH_ATTRIBUTE_STATE_IMPROVEMENT", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                            break;
                        default:
                            comm = new SqlCommand("ECOMM_GALE_FORMAT_PRODUCT_SEARCH_ATTRIBUTE_STATE_IMPROVEMENT", conn);
                            //comm = new SqlCommand("ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE_TEST_MODIFIED",conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                            break;
                    }
                    comm.Parameters.Add(GaleAreaTable);
                    comm.Parameters.Add(GaleDisciplineTable);
                    comm.Parameters.Add(new SqlParameter("@FORMAT ", sParams.BwsFormat));
                }
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@USER_SK", sParams.UserSk));
                comm.Parameters.Add(new SqlParameter("@PAGE_NO", sParams.PageNumber));
                comm.Parameters.Add(new SqlParameter("@DIVISION", sParams.Division));
                comm.Parameters.Add(new SqlParameter("@CONDITION", sParams.Condition));
                comm.Parameters.Add(new SqlParameter("@SEARCHTEXT", sParams.SearchText));
                comm.Parameters.Add(new SqlParameter("@EXACTSEARCHTEXT", sParams.ExactText));
                comm.Parameters.Add(new SqlParameter("@WEIGHTAGE", Weightage));
                comm.Parameters.Add(new SqlParameter("@NO_OF_RECORDS_PER_PAGE", sParams.NumberOfResults));
                comm.Parameters.Add(new SqlParameter("@SORT_ORDER", sParams.SortOrder));
                comm.Parameters.Add(attrtvp);
                comm.Parameters.Add(attrShowOnlytvp);
                comm.Parameters.Add(attrStatetvp);
                comm.Parameters.Add(new SqlParameter("@TYPEOFDISCIPLINE", sParams.DisciplineType));
                comm.Parameters.Add(new SqlParameter("@VALUEOFCATEGORY", sParams.CategoryValue));
                comm.Parameters.Add(new SqlParameter("@VALUEOFDISCIPLINE", sParams.DisciplineValue));
                comm.Parameters.Add(new SqlParameter("@COUNTRY", sParams.Country));
                comm.Parameters.Add(new SqlParameter("@FORMS", sParams.FormsString));
                comm.Parameters.Add(new SqlParameter("@TITLE_WEIGHTAGE", sParams.TitleWeightage));
                comm.Parameters.Add(new SqlParameter("@AUTHOR_WEIGHTAGE", sParams.AuthorWeightage));

                comm.CommandTimeout = 60;
                DnnLog.Info("Before SQL CALL-->" + DateTime.Now.ToString());
                SqlDataReader reader = comm.ExecuteReader();
                DnnLog.Info("After SQL CALL-->" + DateTime.Now.ToString());
                return reader;//CommandBehavior.CloseConnection);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Product result formparameter function
        /// </summary>
        /// <param name="SearchText"></param>
        /// <param name="Condition"></param>
        /// <param name="Weightage"></param>
        private static void FormParameters(string[] WordsSearched, ref string Condition, ref string Weightage)
        {
            try
            {
                var a = 0;
                foreach (string searchtext in WordsSearched)
                {
                    var text = searchtext.ToUpper().Replace("\"", string.Empty);
                    if (text.Contains(" AND ") || text.Contains(" AND NOT "))
                    {
                        foreach (string containsearch in text.Split(new string[] { " AND " }, StringSplitOptions.None))
                        {
                            if (containsearch.Trim() != "NOT" && !string.IsNullOrEmpty(containsearch))
                            {
                                Weightage = Weightage + "\"" + containsearch + "\"" + " WEIGHT (0.8)" + ",";

                            }

                            Condition = Condition + "\"" + containsearch + "\"" + " AND ";
                        }
                        a = 1;
                    }
                    else
                    {
                        a = 2;
                        if (text != "NOT" && !string.IsNullOrEmpty(text))
                        {
                            Weightage = Weightage + "\"" + text + "\"" + " WEIGHT (0.8)" + ",";

                        }
                        Condition = Condition + "\"" + text + "\"" + " OR ";
                        //       Condition = Condition.Remove(Condition.Length - 4);
                    }
                }
                switch (a)
                {
                    case 1:
                        Condition = Condition.Remove(Condition.Length - 5);
                        break;
                    default:
                        Condition = Condition.Remove(Condition.Length - 4);
                        break;
                }
                Weightage = Weightage.TrimEnd(',') + ") ";

            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        ///  Check for the existence of did you mean word for HE,VPG and Gale
        /// </summary>
        /// <param name="word"></param>
        /// <param name="division"></param>
        /// <param name="storeSk"></param>
        /// <returns></returns>
        public override bool CheckForHEDidYouMeanWordExist(string word, string division, string country)
        {
            try
            {
                return ((Int32)SqlHelper.ExecuteScalar(ConnectionString, "ECOMM_HE_CHECK_RESULTS_IS_EXISTS",
                    new SqlParameter("@WORD", word)
                    , new SqlParameter("@DIVISION", division)
                    , new SqlParameter("@COUNTRY", country)) > 0);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// For Advance Search Keyword results
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        /// 
        public override SqlDataReader GetAdvanceSearchProductResults(SearchParameters sParams)
        {
            SqlCommand comm = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlParameter attrtvp = new SqlParameter();
                attrtvp.ParameterName = "@TESTATTRIBUTETVP";
                attrtvp.Value = sParams.FacetAttributesTable;
                attrtvp.SqlDbType = SqlDbType.Structured;
                attrtvp.TypeName = "AttributeSearchTVP";

                SqlParameter attrShowOnlytvp = new SqlParameter();
                attrShowOnlytvp.ParameterName = "@SHOWONLYTVP";
                attrShowOnlytvp.Value = sParams.ShowOnlyAttributesTable;
                attrShowOnlytvp.SqlDbType = SqlDbType.Structured;
                attrShowOnlytvp.TypeName = "SHOWONLYTVP";

                SqlParameter advAttributeSearchTVP = new SqlParameter();
                advAttributeSearchTVP.ParameterName = "@ADVATTRIBUTETVP";
                advAttributeSearchTVP.Value = sParams.AdvFacets;
                advAttributeSearchTVP.SqlDbType = SqlDbType.Structured;
                advAttributeSearchTVP.TypeName = "AdvAttributeSearchTVP";

                SqlParameter attrStatetvp = new SqlParameter();
                attrStatetvp.ParameterName = "@ATTRIBUTE_UI_STATE";
                attrStatetvp.Value = sParams.AttributeStateTable;
                attrStatetvp.SqlDbType = SqlDbType.Structured;
                attrStatetvp.TypeName = "HE_ATTRIBUTE_UI_STATE_INDEXED";

                switch (sParams.Country.ToUpper())
                {
                    case "NZ":
                        comm = new SqlCommand("ECOMM_HE_NZ_ADV_KEYWORD_SEARCH_DYNAMIC", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                    default:
                        comm = new SqlCommand("ECOMM_HE_AU_ADV_KEYWORD_SEARCH_DYNAMIC", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                }
                //SqlCommand comm = new SqlCommand("ECOMM_HE_ADV_KEYWORD_SEARCH", conn);
                AdvanceLogSearchInputs(sParams);
                comm.CommandType = CommandType.StoredProcedure;
                using (comm)
                {
                    comm.Parameters.Add(new SqlParameter("@PAGE_NO", sParams.PageNumber));
                    comm.Parameters.Add(new SqlParameter("@DIVISION", sParams.Division));
                    comm.Parameters.Add(new SqlParameter("@NO_OF_RECORDS_PER_PAGE", sParams.NumberOfResults));
                    comm.Parameters.Add(new SqlParameter("@SORT_ORDER", sParams.SortOrder));
                    comm.Parameters.Add(attrtvp);
                    comm.Parameters.Add(attrShowOnlytvp);
                    comm.Parameters.Add(advAttributeSearchTVP);
                    comm.Parameters.Add(attrStatetvp);
                    comm.Parameters.Add(new SqlParameter("@TYPEOFDISCIPLINE", sParams.DisciplineType));
                    comm.Parameters.Add(new SqlParameter("@VALUEOFCATEGORY", sParams.CategoryValue));
                    comm.Parameters.Add(new SqlParameter("@VALUEOFDISCIPLINE", sParams.DisciplineValue));
                    comm.Parameters.Add(new SqlParameter("@VALUEOFSUBJECT", sParams.SubjectValue));
                    comm.Parameters.Add(new SqlParameter("@TITLEWEIGHTAGE", sParams.TitleWeightage));
                    comm.Parameters.Add(new SqlParameter("@AUTHORWEIGHTAGE", sParams.AuthorWeightage));
                    comm.Parameters.Add(new SqlParameter("@SUBJECTWEIGHTAGE", sParams.SubjectWeightage));
                    comm.Parameters.Add(new SqlParameter("@ANDALLWORDSWEIGHATE", sParams.AndAllwordsWeighate));
                    comm.Parameters.Add(new SqlParameter("@EXACTPHRASEWEIGHATE", sParams.ExactphraseWeightage));
                    comm.Parameters.Add(new SqlParameter("@NONEOFTHESEWORDSWEIGHATE ", sParams.NonephraseWeightage));
                    comm.Parameters.Add(new SqlParameter("@USER_SK", sParams.UserSk));
                    comm.Parameters.Add(new SqlParameter("@EXACT_TITLE", sParams.ExactTitle));
                    comm.Parameters.Add(new SqlParameter("@EXACT_AUTHOR", sParams.ExactAuthor));
                    comm.Parameters.Add(new SqlParameter("@COUNTRY", sParams.Country));
                    comm.Parameters.Add(new SqlParameter("@GstApplicable", sParams.GstFlag));
                    comm.CommandTimeout = 60;
                    return comm.ExecuteReader();
                }
                //CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //comm.Connection.Close();
                // comm.Dispose();
            }
        }

        /// <summary>
        /// For Advance Search Keyword results
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        /// 
        public override SqlDataReader GetGaleAdvanceSearchProductResults(SearchParameters sParams)
        {
            SqlCommand comm = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlParameter attrtvp = new SqlParameter();
                attrtvp.ParameterName = "@TESTATTRIBUTETVP";
                attrtvp.Value = sParams.FacetAttributesTable;
                attrtvp.SqlDbType = SqlDbType.Structured;
                attrtvp.TypeName = "AttributeSearchTVP";

                SqlParameter attrShowOnlytvp = new SqlParameter();
                attrShowOnlytvp.ParameterName = "@SHOWONLYTVP";
                attrShowOnlytvp.Value = sParams.ShowOnlyAttributesTable;
                attrShowOnlytvp.SqlDbType = SqlDbType.Structured;
                attrShowOnlytvp.TypeName = "SHOWONLYTVP";

                SqlParameter advAttributeSearchTVP = new SqlParameter();
                advAttributeSearchTVP.ParameterName = "@ADVATTRIBUTETVP";
                advAttributeSearchTVP.Value = sParams.AdvFacets;
                advAttributeSearchTVP.SqlDbType = SqlDbType.Structured;
                advAttributeSearchTVP.TypeName = "AdvAttributeSearchTVP";

                SqlParameter attrStatetvp = new SqlParameter();
                attrStatetvp.ParameterName = "@ATTRIBUTE_UI_STATE";
                attrStatetvp.Value = sParams.AttributeStateTable;
                attrStatetvp.SqlDbType = SqlDbType.Structured;
                attrStatetvp.TypeName = "HE_ATTRIBUTE_UI_STATE_INDEXED";

                switch (sParams.Country.ToUpper())
                {
                    case "NZ":
                        comm = new SqlCommand("ECOMM_GALE_NZ_ADV_KEYWORD_SEARCH_DYNAMIC", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                    default:
                        comm = new SqlCommand("ECOMM_GALE_AU_ADV_KEYWORD_SEARCH_DYNAMIC", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                }
                //SqlCommand comm = new SqlCommand("ECOMM_HE_ADV_KEYWORD_SEARCH", conn);
                AdvanceLogSearchInputs(sParams);
                comm.CommandType = CommandType.StoredProcedure;
                using (comm)
                {
                    comm.Parameters.Add(new SqlParameter("@PAGE_NO", sParams.PageNumber));
                    comm.Parameters.Add(new SqlParameter("@NO_OF_RECORDS_PER_PAGE", sParams.NumberOfResults));
                    comm.Parameters.Add(new SqlParameter("@SORT_ORDER", sParams.SortOrder));
                    comm.Parameters.Add(attrtvp);
                    comm.Parameters.Add(attrShowOnlytvp);
                    comm.Parameters.Add(advAttributeSearchTVP);
                    comm.Parameters.Add(attrStatetvp);
                    comm.Parameters.Add(new SqlParameter("@TYPEOFDISCIPLINE", sParams.DisciplineType));
                    comm.Parameters.Add(new SqlParameter("@VALUEOFCATEGORY", sParams.CategoryValue));
                    comm.Parameters.Add(new SqlParameter("@VALUEOFDISCIPLINE", sParams.DisciplineValue));
                    comm.Parameters.Add(new SqlParameter("@TITLEWEIGHTAGE", sParams.TitleWeightage));
                    comm.Parameters.Add(new SqlParameter("@AUTHORWEIGHTAGE", sParams.AuthorWeightage));
                    comm.Parameters.Add(new SqlParameter("@SUBJECTWEIGHTAGE", sParams.SubjectWeightage));
                    comm.Parameters.Add(new SqlParameter("@ANDALLWORDSWEIGHATE", sParams.AndAllwordsWeighate));
                    comm.Parameters.Add(new SqlParameter("@EXACTPHRASEWEIGHATE", sParams.ExactphraseWeightage));
                    comm.Parameters.Add(new SqlParameter("@NONEOFTHESEWORDSWEIGHATE ", sParams.NonephraseWeightage));
                    comm.Parameters.Add(new SqlParameter("@USER_SK", sParams.UserSk));
                    comm.Parameters.Add(new SqlParameter("@EXACT_TITLE", sParams.ExactTitle));
                    comm.Parameters.Add(new SqlParameter("@EXACT_AUTHOR", sParams.ExactAuthor));
                    comm.Parameters.Add(new SqlParameter("@COUNTRY", sParams.Country));
                    comm.CommandTimeout = 60;
                    return comm.ExecuteReader();
                }
                //CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //comm.Connection.Close();
                // comm.Dispose();
            }
        }


        /// <summary>
        /// For Advance Multi isbns results
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public override SqlDataReader GetHEAdvanceIsbnResults(SearchParameters sParams)
        {
            try
            {
                SqlParameter attrTVP = new SqlParameter();
                attrTVP.ParameterName = "@ATTRIBUTETVP";
                attrTVP.Value = sParams.FacetAttributesTable;
                attrTVP.SqlDbType = SqlDbType.Structured;
                attrTVP.TypeName = "AttributeSearchTVP";
                SqlParameter showOnlyAttrTVP = new SqlParameter();
                showOnlyAttrTVP.ParameterName = "@SHOWONLYTVP";
                showOnlyAttrTVP.Value = sParams.ShowOnlyAttributesTable;
                showOnlyAttrTVP.SqlDbType = SqlDbType.Structured;
                showOnlyAttrTVP.TypeName = "SHOWONLYTVP";

                SqlParameter attrStatetvp = new SqlParameter();
                attrStatetvp.ParameterName = "@ATTRIBUTE_UI_STATE";
                attrStatetvp.Value = sParams.AttributeStateTable;
                attrStatetvp.SqlDbType = SqlDbType.Structured;
                attrStatetvp.TypeName = "HE_ATTRIBUTE_UI_STATE_INDEXED";

                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand cmd = null;
                switch (sParams.Country.ToUpper())
                {
                    case "NZ":
                        cmd = new SqlCommand("ECOMM_NZ_HE_MUTLI_ISBN_SEARCH_MIDIFIED", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                    default:
                        cmd = new SqlCommand("ECOMM_AU_HE_MUTLI_ISBN_SEARCH_MIDIFIED", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                }
                // LogSearchInputs(sParams,"");
                using (cmd)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PAGE_NO", sParams.PageNumber));
                    cmd.Parameters.Add(new SqlParameter("@DIVISION", sParams.Division));
                    cmd.Parameters.Add(new SqlParameter("@STORESK", sParams.StoreSK));
                    cmd.Parameters.Add(new SqlParameter("@NO_OF_RECORDS_PER_PAGE", sParams.NumberOfResults));
                    cmd.Parameters.Add(new SqlParameter("@SEARCHTEXT", sParams.SearchText));
                    cmd.Parameters.Add(new SqlParameter("@SORT_ORDER", sParams.SortOrder));
                    cmd.Parameters.Add(new SqlParameter("@TYPEOFDISCIPLINE", sParams.DisciplineType));
                    cmd.Parameters.Add(new SqlParameter("@VALUEOFCATEGORY", sParams.CategoryValue));
                    cmd.Parameters.Add(new SqlParameter("@VALUEOFDISCIPLINE", sParams.DisciplineValue));
                    cmd.Parameters.Add(new SqlParameter("@VALUEOFSUBJECT", sParams.SubjectValue));
                    cmd.Parameters.Add(new SqlParameter("@USER_SK", sParams.UserSk));
                    cmd.Parameters.Add(new SqlParameter("@COUNTRY", sParams.Country));
                    cmd.Parameters.Add(new SqlParameter("@GstApplicable", sParams.GstFlag));
                    cmd.Parameters.Add(attrTVP);
                    cmd.Parameters.Add(showOnlyAttrTVP);
                    cmd.Parameters.Add(attrStatetvp);
                    return cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// For Advance Multi isbns results
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public override SqlDataReader GetGaleAdvanceIsbnResults(SearchParameters sParams)
        {
            try
            {
                SqlParameter attrTVP = new SqlParameter();
                attrTVP.ParameterName = "@ATTRIBUTETVP";
                attrTVP.Value = sParams.FacetAttributesTable;
                attrTVP.SqlDbType = SqlDbType.Structured;
                attrTVP.TypeName = "AttributeSearchTVP";
                SqlParameter showOnlyAttrTVP = new SqlParameter();
                showOnlyAttrTVP.ParameterName = "@SHOWONLYTVP";
                showOnlyAttrTVP.Value = sParams.ShowOnlyAttributesTable;
                showOnlyAttrTVP.SqlDbType = SqlDbType.Structured;
                showOnlyAttrTVP.TypeName = "SHOWONLYTVP";

                SqlParameter attrStatetvp = new SqlParameter();
                attrStatetvp.ParameterName = "@ATTRIBUTE_UI_STATE";
                attrStatetvp.Value = sParams.AttributeStateTable;
                attrStatetvp.SqlDbType = SqlDbType.Structured;
                attrStatetvp.TypeName = "HE_ATTRIBUTE_UI_STATE_INDEXED";

                SqlConnection conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand cmd = null;
                switch (sParams.Country.ToUpper())
                {
                    case "NZ":
                        cmd = new SqlCommand("ECOMM_NZ_GALE_MUTLI_ISBN_SEARCH_MIDIFIED", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                    default:
                        cmd = new SqlCommand("ECOMM_AU_GALE_MUTLI_ISBN_SEARCH_MIDIFIED", conn);//"ECOMM_HE_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
                        break;
                }
                // LogSearchInputs(sParams,"");
                using (cmd)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PAGE_NO", sParams.PageNumber));
                    cmd.Parameters.Add(new SqlParameter("@NO_OF_RECORDS_PER_PAGE", sParams.NumberOfResults));
                    cmd.Parameters.Add(new SqlParameter("@SEARCHTEXT", sParams.SearchText));
                    cmd.Parameters.Add(new SqlParameter("@SORT_ORDER", sParams.SortOrder));
                    cmd.Parameters.Add(new SqlParameter("@TYPEOFDISCIPLINE", sParams.DisciplineType));
                    cmd.Parameters.Add(new SqlParameter("@VALUEOFCATEGORY", sParams.CategoryValue));
                    cmd.Parameters.Add(new SqlParameter("@VALUEOFDISCIPLINE", sParams.DisciplineValue));
                    cmd.Parameters.Add(new SqlParameter("@USER_SK", sParams.UserSk));
                    cmd.Parameters.Add(new SqlParameter("@COUNTRY", sParams.Country));
                    cmd.Parameters.Add(attrTVP);
                    cmd.Parameters.Add(showOnlyAttrTVP);
                    cmd.Parameters.Add(attrStatetvp);
                    return cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cms page result selectcmsresult function
        /// </summary>
        /// <param name="Division"></param>
        /// <param name="Keywords"></param>
        /// <param name="ItemCount"></param>
        /// <param name="PageNumber"></param>
        /// <param name="Storesk"></param>
        /// <returns></returns>
        public override SqlDataReader GetCmsResults(SearchParameters sParams)//DataSet
        {
            try
            {
                string KeyWordWeightage = string.Empty;
                if (!string.IsNullOrEmpty(sParams.SearchText))
                {
                    KeyWordWeightage = "ISABOUT (" + "\"" + sParams.SearchText + "\"" + " WEIGHT (1),";
                    foreach (string keyword in sParams.SearchText.Split(' '))
                    {
                        var word = keyword.Trim();
                        if (keyword != string.Empty)
                            KeyWordWeightage = KeyWordWeightage + "\"" + word.Trim(',') + "\"" + " WEIGHT (0.8)" + ",";
                    }
                    KeyWordWeightage = KeyWordWeightage.TrimEnd(',') + ") ";
                }

                return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_HECMSPAGERESULT,
                    new SqlParameter("@DIVISION", sParams.Division),
                    new SqlParameter("@KEYWORD", sParams.SearchText),
                    new SqlParameter("@KEYWORD_WEIGHTAGE", KeyWordWeightage),
                    new SqlParameter("@NO_OF_RECORDS_PER_PAGE", sParams.NumberOfResults),
                    new SqlParameter("@PAGE_NO", sParams.PageNumber),
                    new SqlParameter("@COUNTRY", sParams.Country));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To save the Search and favorite products of the user
        /// </summary>
        /// <param name="sParams"></param>
        /// <returns></returns>
        public override int SaveSearchAndFavorites(SearchParameters sParams)
        {
            try
            {
                return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_SAVESEARCHANDFAVORTIES,
                    new SqlParameter("@TYPE", sParams.ActionType),
                    new SqlParameter("@USER_SK", sParams.UserSk),
                    new SqlParameter("@PRODUCT_SK", sParams.ProductSk),
                    new SqlParameter("@URL_SEARCH", sParams.CurrentUrl),
                    new SqlParameter("@KEYWORDS_STORED", sParams.KeyWords),
                    new SqlParameter("@SEARCH_NAME", sParams.SearchName),
                    new SqlParameter("@DIVISION", sParams.Division)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To remove/update the Search and favorite products of the user
        /// </summary>
        /// <param name="sParams"></param>
        /// <returns></returns>
        public override int UpdateSaveSearchAndFavorites(SearchParameters sParams)
        {
            try
            {
                return Null.SetNullInteger(SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_UPDATESAVESEARCHANDFAVORTIES,
                    new SqlParameter("@USER_SK", sParams.UserSk),
                    new SqlParameter("@PRODUCT_SK", sParams.ProductSk),
                    new SqlParameter("@APP", "SearchResults"),
                    new SqlParameter("@TYPE", sParams.ActionType),
                    new SqlParameter("@SEARCH_NAME", sParams.SearchName),
                    new SqlParameter("@URL_SEARCH", sParams.CurrentUrl)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LogSearchInputs(SearchParameters sParams, string Weightage)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "HE Search Inputs - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(string.Concat("\r\n-----------------------------------", DateTime.Now, "---------------------------------------------",
                "\r\n @DIVISION: ", sParams.Division,
                "\r\n @STORESK: ", sParams.StoreSK,
                "\r\n @NO_OF_RECORDS_PER_PAGE: ", sParams.NumberOfResults,
                "\r\n @SORT_ORDER: ", sParams.SortOrder,
                "\r\n @FacetAttributesTable: ", sParams.FacetAttributesTable.Rows.Count,
                "\r\n @ShowOnlyAttributesTable: ", sParams.ShowOnlyAttributesTable.Rows.Count,
                "\r\n @TYPEOFDISCIPLINE: ", sParams.DisciplineType,
                "\r\n @VALUEOFCATEGORY: ", sParams.CategoryValue,
                "\r\n @VALUEOFDISCIPLINE: ", sParams.DisciplineValue,
                "\r\n @VALUEOFSUBJECT: ", sParams.SubjectValue,
                "\r\n @COUNTRY: ", sParams.Country,
                "\r\n @CONDITION: ", sParams.Condition,
                "\r\n @SEARCHTEXT: ", sParams.SearchText,
                "\r\n @EXACTSEARCHTEXT: ", sParams.ExactText,
                "\r\n @WEIGHTAGE: ", Weightage,
                "\r\n @FORMS: ", sParams.FormsString,
                "\r\n @CurrFacetAttributesTable: ", sParams.AttributeStateTable.Rows.Count,
                    //"\r\n @TITLEWEIGHTAGE: ", sParams.TitleWeighate,
                    //"\r\n @AUTHORWEIGHTAGE: ", sParams.AuthorWeighate,
                    //"\r\n @KEYWORDWEIGHTAGE: ", sParams.KeyWordWeightage,
                    //"\r\n @SUBJECTWEIGHTAGE: ", sParams.SubjectWeighate,
                    //"\r\n @ANDALLWORDSWEIGHATE: ", sParams.AndAllwordsWeighate,
                    //"\r\n @EXACTPHRASEWEIGHATE: ", sParams.ExactphraseWeighate,
                    //"\r\n @NONEOFTHESEWORDSWEIGHATE: ", sParams.NonephraseWeighate,
                    //"\r\n @ATTRIBUTEWEIGHTAGE: ", sParams.AttributeWeighate,
                    //"\r\n @ATTRIBUTEVALUEWEIGHTAGE: ", sParams.AttributeValueWeighate,
                "\r\n @USER_SK: ", sParams.UserSk,
                "\r\n-----------------------------------------------------------------------------------------------------------------"));
                // TeacherController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }
        private void AdvanceLogSearchInputs(SearchParameters sParams)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "HE Advance Search Inputs - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(string.Concat("\r\n-----------------------------------", DateTime.Now, "---------------------------------------------",
                "\r\n @DIVISION: ", sParams.Division,
                "\r\n @NO_OF_RECORDS_PER_PAGE: ", sParams.NumberOfResults,
                "\r\n @SORT_ORDER: ", sParams.SortOrder,
                 "\r\n @ADVATTRIBUTETVP: ", sParams.AdvFacets.Rows.Count,
                "\r\n @TYPEOFDISCIPLINE: ", sParams.DisciplineType,
                "\r\n @VALUEOFCATEGORY: ", sParams.CategoryValue,
                "\r\n @VALUEOFDISCIPLINE: ", sParams.DisciplineValue,
                "\r\n @VALUEOFSUBJECT: ", sParams.SubjectValue,
                "\r\n @SEARCHTEXT: ", sParams.SearchText,
                "\r\n @EXACTSEARCHTEXT: ", sParams.ExactText,
                "\r\n @TITLEWEIGHTAGE: ", sParams.TitleWeightage,
                "\r\n @AUTHORWEIGHTAGE: ", sParams.AuthorWeightage,
                "\r\n @SUBJECTWEIGHTAGE: ", sParams.SubjectWeightage,
                "\r\n @ANDALLWORDSWEIGHATE: ", sParams.AndAllwordsWeighate,
                "\r\n @EXACTPHRASEWEIGHATE: ", sParams.ExactphraseWeightage,
                "\r\n @NONEOFTHESEWORDSWEIGHATE: ", sParams.NonephraseWeightage,
                "\r\n @USER_SK: ", sParams.UserSk,
                "\r\n @EXACT_TITLE: ", sParams.ExactTitle,
                "\r\n @EXACT_AUTHOR: ", sParams.ExactAuthor,
                "\r\n-----------------------------------------------------------------------------------------------------------------"));
                // TeacherController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }
        #endregion


        #region School Search Methods
        /// <summary>
        /// Product result formparameter function
        /// </summary>
        /// <param name="SearchText"></param>
        /// <param name="Condition"></param>
        /// <param name="Weightage"></param>
        private static void FormParameters(string SearchText, ref string Condition, ref string Weightage, ref string tileWeightage, ref string authorWeightage)
        {
            if (SearchText.ToUpper().Contains(" AND ") || SearchText.ToUpper().Contains(" AND NOT "))
            {

                foreach (string containsearch in SearchText.ToUpper().Split(new string[] { " AND " }, StringSplitOptions.None))
                {
                    foreach (string formsstring in containsearch.Trim().Split(' '))
                    {
                        if (formsstring.ToUpper().Trim() != "NOT" && !string.IsNullOrEmpty(formsstring))
                        {
                            //Stemming = Stemming + "\"" + formsstring + "\"" + ",";
                            Weightage = Weightage + formsstring + " WEIGHT (1)" + ",";
                            tileWeightage = tileWeightage + "\"" + containsearch + "\"" + " WEIGHT (0.9)" + ",";
                            authorWeightage = authorWeightage + "\"" + containsearch + "\"" + " WEIGHT (0.7)" + ",";
                        }
                    }

                    Condition = Condition + "\"" + containsearch + "\"" + " AND ";

                }
                Condition = Condition.Remove(Condition.Length - 5);
            }
            else
            {
                foreach (string containsearch in SearchText.ToUpper().Split(new string[] { " " }, StringSplitOptions.None))
                {
                    foreach (string formsstring in containsearch.Trim().Split(' '))
                    {
                        if (formsstring.ToUpper().Trim() != "NOT" && !string.IsNullOrEmpty(formsstring))
                        {
                            //Stemming = Stemming + "\"" + formsstring + "\"" + ",";
                            Weightage = Weightage + formsstring + " WEIGHT (1)" + ",";
                            tileWeightage = tileWeightage + "\"" + formsstring + "\"" + " WEIGHT (0.9)" + ",";
                            authorWeightage = authorWeightage + "\"" + formsstring + "\"" + " WEIGHT (0.7)" + ",";
                        }
                    }

                    Condition = Condition + "\"" + containsearch + "\"" + " OR ";
                }
                Condition = Condition.Remove(Condition.Length - 4);
            }
            //Stemming = Stemming.TrimEnd(',') + ") ";
            Weightage = Weightage.TrimEnd(',') + ") ";
            tileWeightage = tileWeightage.TrimEnd(',') + ") ";
            authorWeightage = authorWeightage.TrimEnd(',') + ") ";
        }

        /// <summary>
        /// For Main Search Results
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public override SqlDataReader GetComponentResults(SearchEngine searchEngine)
        {
            DataTable TestAttributedataTable = new DataTable();
            TestAttributedataTable.Columns.Add("Attribute_Type_SK", typeof(int));
            TestAttributedataTable.Columns.Add("Attribute_Type_Value_SK", typeof(int));



            if (!string.IsNullOrEmpty(searchEngine.Facets))
            {
                string[] facets = searchEngine.Facets.Split(':');
                foreach (string facet in facets)
                {
                    string[] simFacets = facet.Split('_');
                    if (simFacets.Length == 1)
                    {
                        simFacets = new string[2];
                        simFacets[0] = facet;
                        simFacets[1] = "0";
                    }
                    string[] innerFacets = simFacets[1].Split(',');
                    foreach (string innerFacet in innerFacets)
                    {
                        TestAttributedataTable.Rows.Add(Convert.ToInt32(simFacets[0]), Convert.ToInt32(innerFacet));
                    }

                }
            }

            string Weightage = "ISABOUT (" + "\"" + searchEngine.SearchText + "\"" + " WEIGHT (1),";
            string titleWeightage = "ISABOUT (" + "\"" + searchEngine.SearchText + "\"" + " WEIGHT (1),";
            string authorWeightage = "ISABOUT (" + "\"" + searchEngine.SearchText + "\"" + " WEIGHT (0.8),";
            string Condition = string.Empty;

            // if user enters any product information , will perform search based on it

            FormParameters(searchEngine.SearchText, ref Condition, ref Weightage, ref titleWeightage, ref authorWeightage);
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlParameter pageNoParam = new SqlParameter("@PAGE_NO", searchEngine.PageNumber);
            SqlParameter divisionParam = new SqlParameter("@DIVISION", searchEngine.Division);
            SqlParameter storeParam = new SqlParameter("@STORESK", searchEngine.StoreSK);
            SqlParameter conditionParam = new SqlParameter("@CONDITION", Condition);
            SqlParameter searchParam = new SqlParameter("@SEARCHTEXT", searchEngine.SearchText);
            SqlParameter exactSearchParam = new SqlParameter("@EXACTSEARCHTEXT", searchEngine.ExactText);
            SqlParameter weightageParam = new SqlParameter("@WEIGHTAGE", Weightage);
            SqlParameter no_Of_Records = new SqlParameter("@NO_OF_RECORDS_PER_PAGE", searchEngine.NumberOfResults);
            SqlParameter sortParam = new SqlParameter("@SORT_ORDER", searchEngine.SortOrder);
            SqlParameter country_code = new SqlParameter("@COUNTRY_CODE", searchEngine.CountryCode);
            SqlParameter titleweightageParam = new SqlParameter("@TITLE_WEIGHTAGE", titleWeightage);
            SqlParameter authorweightageParam = new SqlParameter("@AUTHOR_WEIGHTAGE", authorWeightage);
            SqlParameter attrtvp = new SqlParameter();
            attrtvp.ParameterName = "@TESTATTRIBUTETVP";
            attrtvp.Value = TestAttributedataTable;
            attrtvp.SqlDbType = SqlDbType.Structured;
            attrtvp.TypeName = "AttributeSearchTVP";

            SqlParameter attrStatetvp = new SqlParameter();
            attrStatetvp.ParameterName = "@ATTRIBUTE_UI_STATE";
            attrStatetvp.Value = searchEngine.AttributeStateTable;
            attrStatetvp.SqlDbType = SqlDbType.Structured;
            attrStatetvp.TypeName = "ATTRIBUTE_UI_STATE";
            HESearchResultsModuleBase.LogValues("@schoolsTITLE_WEIGHTAGE" + titleWeightage + "@schoolsAUTHOR_WEIGHTAGE" + authorWeightage);
            SqlCommand comm = new SqlCommand("ECOMM_PRODUCT_SEARCH_ATTRIBUTE_STATE", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(pageNoParam);
            comm.Parameters.Add(divisionParam);
            comm.Parameters.Add(storeParam);
            comm.Parameters.Add(conditionParam);
            comm.Parameters.Add(searchParam);
            comm.Parameters.Add(exactSearchParam);
            comm.Parameters.Add(weightageParam);
            comm.Parameters.Add(no_Of_Records);
            comm.Parameters.Add(sortParam);
            comm.Parameters.Add(country_code);
            comm.Parameters.Add(titleweightageParam);
            comm.Parameters.Add(authorweightageParam);
            comm.Parameters.Add(attrtvp);
            comm.Parameters.Add(attrStatetvp);
            return comm.ExecuteReader(CommandBehavior.CloseConnection);

        }

        /// <summary>
        /// Product result selectsearchprod function
        /// </summary>
        /// <param name="PrdSk"></param>
        /// <param name="division"></param>
        /// <param name="store_sk"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public override DataSet SelectSearchStudProd(int PrdSk, string division, int store_sk, string country)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, "ECOMM_STUDENT_PRODUCT_ATTRIBUTES"
                    , new SqlParameter("@PRODUCT_SK", PrdSk), new SqlParameter("@DIVISION", division), new SqlParameter("@STORESK", store_sk), new SqlParameter("@COUNTRY_CODE", country));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Product result addlistquote function
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="_userSK"></param>
        /// <param name="_productSKQuantity"></param>
        /// <param name="_userListQuoteSK"></param>
        /// <param name="_genericFlag"></param>
        /// <param name="_output"></param>
        /// <returns></returns>
        public override int AddListQuote(string flag, string _userSK, string _productSKQuantity, string _userListQuoteSK, string _genericFlag, ref string _output)
        {
            System.Data.SqlClient.SqlParameter[] sqlp = new System.Data.SqlClient.SqlParameter[6];

            sqlp[0] = new SqlParameter("@FLAG", flag);
            sqlp[1] = new SqlParameter("@USERSK", _userSK); ;
            sqlp[2] = new SqlParameter("@PRDSK_QUANTITY", _productSKQuantity);
            sqlp[3] = new SqlParameter("@USR_LIST_QUOTE_SK", _userListQuoteSK);
            sqlp[4] = new SqlParameter("@GENERICFLAG", _genericFlag);
            sqlp[5] = new SqlParameter("@RETURN", _output);
            sqlp[5].Size = 50;
            sqlp[5].SqlDbType = SqlDbType.VarChar;
            sqlp[5].Direction = ParameterDirection.Output;

            int rv = SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, Constants.SP_INSERTLIST_QUOTE, sqlp);
            _output = (string)sqlp[5].Value;
            return rv;
        }

        /// <summary>
        /// product result select prd function
        /// </summary>
        /// <param name="PrdSk"></param>
        /// <param name="division"></param>
        /// <returns></returns>
        public override DataSet SelectStdPrd(int PrdSk, string division)
        {
            return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_PRDSTD
            , new SqlParameter("@product_sk", PrdSk),
            new SqlParameter("@division", division));
        }

        /// <summary>
        /// Product result edit quote details
        /// </summary>
        /// <param name="Quote"></param>
        public override void EditQuoteDetails(QuoteInfo Quote)
        {
            try
            {
                SqlParameter outputparam = new SqlParameter("@LIST_QUOTE_SK", Quote.LIST_QUOTE_SK) { Direction = ParameterDirection.InputOutput };
                SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, Constants.SP_EDITQUOTEDETAILS,
                    new SqlParameter() { ParameterName = "@QUOTEDETAILS", Value = Quote.ProductDetails, SqlDbType = SqlDbType.Structured },
                    new SqlParameter("@ACTION", Quote.ACTION),
                    new SqlParameter("@USER_SK", Quote.USER_SK),
                    new SqlParameter("@USER_CREATED", Quote.USER_CREATED),
                    new SqlParameter("@TRADING_PARTNER_ACCOUNT_SK", Quote.TRADING_PARTNER_ACCOUNT_SK),
                    new SqlParameter("@QUOTE_NAME", Quote.QUOTE_NAME),
                    new SqlParameter("@QUOTE_VALID_DAYS", Quote.QUOTE_VALID_DAYS),
                    new SqlParameter("@CURRENCY", Quote.CURRENCY),
                    new SqlParameter("@SHIPPING_COST_STD", Quote.SHIPPING_COST_STD),
                    new SqlParameter("@GST_APPLICABLE", Quote.GST_APPLICABLE),
                    outputparam,
                    new SqlParameter("@FREESHIPPING_ELIGIBLE_PRICE", Quote.FreeShippingEligiblePrice)
                    );
                Quote.LIST_QUOTE_SK = Convert.ToInt32(outputparam.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// For Advance Search Keyword results
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public override SqlDataReader GetAdvanceKeywordResults(SearchEngine searchEngine)
        {
            DataTable TestAttributedataTable = new DataTable();
            TestAttributedataTable.Columns.Add("Attribute_Type_SK", typeof(int));
            TestAttributedataTable.Columns.Add("Attribute_Type_Value_SK", typeof(int));



            if (!string.IsNullOrEmpty(searchEngine.Facets))
            {
                string[] facets = searchEngine.Facets.Split(':');
                foreach (string facet in facets)
                {
                    string[] simFacets = facet.Split('_');
                    if (simFacets.Length == 1)
                    {
                        simFacets = new string[2];
                        simFacets[0] = facet;
                        simFacets[1] = "0";
                    }
                    string[] innerFacets = simFacets[1].Split(',');
                    foreach (string innerFacet in innerFacets)
                    {
                        TestAttributedataTable.Rows.Add(Convert.ToInt32(simFacets[0]), Convert.ToInt32(innerFacet));
                    }

                }
            }
            string KeyWordWeightage = string.Empty;
            string TitleWeighate = string.Empty;
            string AuthorWeighate = string.Empty;
            string SubjectWeighate = string.Empty;
            string AndAllwordsWeighate = string.Empty;
            string ExactphraseWeighate = string.Empty;
            string AttributeWeighate = string.Empty;
            string AttributeValueWeighate = string.Empty;
            string Searchtext = string.Empty;
            if (!string.IsNullOrEmpty(searchEngine.KeyWordWeightage))
            {
                KeyWordWeightage = "ISABOUT (" + "\"" + searchEngine.KeyWordWeightage + "\"" + " WEIGHT (1),";
                foreach (string keyword in searchEngine.KeyWordWeightage.Split(' '))
                {
                    if (keyword.Trim() != string.Empty)
                        KeyWordWeightage = KeyWordWeightage + "\"" + keyword.Trim() + "\"" + " WEIGHT (0.8)" + ",";
                }
                KeyWordWeightage = KeyWordWeightage.TrimEnd(',') + ") ";
            }

            if (!string.IsNullOrEmpty(searchEngine.TitleWeighate))
            {

                TitleWeighate = "ISABOUT (" + "\"" + searchEngine.TitleWeighate + "\"" + " WEIGHT (1),";
                foreach (string title in searchEngine.TitleWeighate.Split(' '))
                {
                    if (title.Trim() != string.Empty)
                        TitleWeighate = TitleWeighate + "\"" + title.Trim() + "\"" + " WEIGHT (0.8)" + ",";
                }
                TitleWeighate = TitleWeighate.TrimEnd(',') + ") ";
                Searchtext = searchEngine.TitleWeighate;
            }

            if (!string.IsNullOrEmpty(searchEngine.AuthorWeighate))
            {

                AuthorWeighate = "ISABOUT (" + "\"" + searchEngine.AuthorWeighate + "\"" + " WEIGHT (1),";
                foreach (string author in searchEngine.AuthorWeighate.Split(' '))
                {
                    if (author.Trim() != string.Empty)
                        AuthorWeighate = AuthorWeighate + "\"" + author.Trim() + "\"" + " WEIGHT (0.8)" + ",";
                }
                AuthorWeighate = AuthorWeighate.TrimEnd(',') + ") ";
            }

            if (!string.IsNullOrEmpty(searchEngine.SubjectWeighate))
            {
                SubjectWeighate = "ISABOUT (" + "\"" + searchEngine.SubjectWeighate + "\"" + " WEIGHT (1),";
                foreach (string subject in searchEngine.SubjectWeighate.Split(' '))
                {
                    if (subject.Trim() != string.Empty)
                        SubjectWeighate = SubjectWeighate + "\"" + subject.Trim() + "\"" + " WEIGHT (0.8)" + ",";
                }
                SubjectWeighate = SubjectWeighate.TrimEnd(',') + ") ";
            }

            if (!string.IsNullOrEmpty(searchEngine.AndAllwordsWeighate))
            {
                AndAllwordsWeighate = "ISABOUT (" + "\"" + searchEngine.AndAllwordsWeighate + "\"" + " WEIGHT (1),";
                foreach (string item in searchEngine.AndAllwordsWeighate.Split(' '))
                {
                    if (item.Trim() != string.Empty)
                        AndAllwordsWeighate = AndAllwordsWeighate + "\"" + item.Trim() + "\"" + " WEIGHT (0.8)" + ",";
                }
                AndAllwordsWeighate = AndAllwordsWeighate.TrimEnd(',') + ") ";
            }

            if (!string.IsNullOrEmpty(searchEngine.ExactphraseWeighate))
            {
                ExactphraseWeighate = "\"" + searchEngine.ExactphraseWeighate + "\"";
            }

            if (!string.IsNullOrEmpty(searchEngine.AttributeWeighate) && !string.IsNullOrEmpty(searchEngine.AttributeValueWeighate))
            {
                AttributeWeighate = searchEngine.AttributeWeighate.Trim(',');
                AttributeValueWeighate = searchEngine.AttributeValueWeighate.Trim(',');
            }
            else
            {
                AttributeWeighate = "DCDEFKANDKCDEFD";
                AttributeValueWeighate = "DCDEFKANDKCDEFD";
            }
            if (string.IsNullOrEmpty(KeyWordWeightage))
                KeyWordWeightage = "DCDEFKANDKCDEFD";
            if (string.IsNullOrEmpty(TitleWeighate))
                TitleWeighate = "DCDEFKANDKCDEFD";
            if (string.IsNullOrEmpty(AuthorWeighate))
                AuthorWeighate = "DCDEFKANDKCDEFD";
            if (string.IsNullOrEmpty(SubjectWeighate))
                SubjectWeighate = "DCDEFKANDKCDEFD";
            if (string.IsNullOrEmpty(AndAllwordsWeighate))
                AndAllwordsWeighate = "DCDEFKANDKCDEFD";
            if (string.IsNullOrEmpty(ExactphraseWeighate))
                ExactphraseWeighate = "DCDEFKANDKCDEFD";


            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlParameter pageNoParam = new SqlParameter("@PAGE_NO", searchEngine.PageNumber);
            SqlParameter divisionParam = new SqlParameter("@DIVISION", searchEngine.Division);
            SqlParameter storeParam = new SqlParameter("@STORESK", searchEngine.StoreSK);
            SqlParameter searchParam = new SqlParameter("@SEARCHTEXT", Searchtext);
            SqlParameter no_Of_Records = new SqlParameter("@NO_OF_RECORDS_PER_PAGE", searchEngine.NumberOfResults);
            SqlParameter sortParam = new SqlParameter("@SORT_ORDER", searchEngine.SortOrder);
            SqlParameter country_code = new SqlParameter("@COUNTRY_CODE", searchEngine.CountryCode);
            SqlParameter KeyWord_Weightage = new SqlParameter("@KEYWORDWEIGHTAGE", KeyWordWeightage);
            SqlParameter Title_Weighate = new SqlParameter("@TITLEWEIGHTAGE", TitleWeighate);
            SqlParameter Author_Weighate = new SqlParameter("@AUTHORWEIGHTAGE", AuthorWeighate);
            SqlParameter Subject_Weighate = new SqlParameter("@SUBJECTWEIGHTAGE", SubjectWeighate);
            SqlParameter AndAllwords_Weighate = new SqlParameter("@ANDALLWORDSWEIGHATE", AndAllwordsWeighate);
            SqlParameter Exactphrase_Weighate = new SqlParameter("@EXACTPHRASEWEIGHATE", ExactphraseWeighate);
            SqlParameter Attribute_Weighate = new SqlParameter("@ATTRIBUTEWEIGHTAGE", AttributeWeighate);
            SqlParameter AttributeValue_Weighate = new SqlParameter("@ATTRIBUTEVALUEWEIGHTAGE", AttributeValueWeighate);


            SqlParameter attrtvp = new SqlParameter();
            attrtvp.ParameterName = "@TESTATTRIBUTETVP";
            attrtvp.Value = TestAttributedataTable;
            attrtvp.SqlDbType = SqlDbType.Structured;
            attrtvp.TypeName = "AttributeSearchTVP";

            SqlParameter attrStatetvp = new SqlParameter();
            attrStatetvp.ParameterName = "@ATTRIBUTE_UI_STATE";
            attrStatetvp.Value = searchEngine.AttributeStateTable;
            attrStatetvp.SqlDbType = SqlDbType.Structured;
            attrStatetvp.TypeName = "ATTRIBUTE_UI_STATE";

            SqlCommand comm = new SqlCommand("ECOMM_ADV_KEYWORD_SEARCH", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(pageNoParam);
            comm.Parameters.Add(divisionParam);
            comm.Parameters.Add(storeParam);
            comm.Parameters.Add(searchParam);
            comm.Parameters.Add(no_Of_Records);
            comm.Parameters.Add(sortParam);
            comm.Parameters.Add(country_code);
            comm.Parameters.Add(attrtvp);
            comm.Parameters.Add(attrStatetvp);
            comm.Parameters.Add(KeyWord_Weightage);
            comm.Parameters.Add(Title_Weighate);
            comm.Parameters.Add(Author_Weighate);
            comm.Parameters.Add(Subject_Weighate);
            comm.Parameters.Add(AndAllwords_Weighate);
            comm.Parameters.Add(Exactphrase_Weighate);
            comm.Parameters.Add(Attribute_Weighate);
            comm.Parameters.Add(AttributeValue_Weighate);
            return comm.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// product result getexistingquote function
        /// </summary>
        /// <param name="UserSk"></param>
        /// <param name="Role"></param>
        /// <param name="TradeSK"></param>
        /// <returns></returns>
        public override DataSet GetExistingQuotes(int UserSk, string Role, int TradeSK)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETEXISTINGQUOTES
                    , new SqlParameter("@USERSK", UserSk), new SqlParameter("@Role", Role), new SqlParameter("@TRADING_PARTNER_SK", TradeSK));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Product result getfacetresultattributes
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public override IDataReader GetFacetResultsAttributes(SearchEngine searchEngine)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Product result getproduct function
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="userListQuoteSK"></param>
        /// <param name="userSK"></param>
        /// <param name="genericFlag"></param>
        /// <returns></returns>
        public override IDataReader GetProduct(string flag, string userListQuoteSK, int? userSK, string genericFlag)
        {
            return SqlHelper.ExecuteReader(ConnectionString, Constants.SP_LISTDETAIL, flag, userListQuoteSK, userSK, genericFlag);
        }

        /// <summary>
        /// Product result getquoteproducts function
        /// </summary>
        /// <param name="userListQuoteSK"></param>
        /// <returns></returns>
        public override DataSet GetQuoteProducts(int userListQuoteSK)
        {
            try
            {
                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_GETQUOTEPRODUCTS
                   , new SqlParameter("@LIST_QUOTE_SK", userListQuoteSK));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// product result getuser role function
        /// </summary>
        /// <param name="usersk"></param>
        /// <returns></returns>
        public override string GetUserRole(int usersk)
        {
            try
            {
                object Role = SqlHelper.ExecuteScalar(ConnectionString, Constants.SP_GETUSERROLE
                , new SqlParameter("@userSK", usersk));
                if (Role != null)
                    return Role.ToString();
                else
                    return "";

            }
            catch (Exception)
            {
                throw;
            }
        }
        public override bool CheckResultsForCorrectedWord(string word, string division, int storeSk)
        {
            return ((Int32)SqlHelper.ExecuteScalar(ConnectionString, "ECOMM_CHECK_RESULTS_IS_EXISTS",
                new SqlParameter("@WORD", word)
                , new SqlParameter("@DIVISION", division)
                , new SqlParameter("@STORESK", storeSk)) > 0);
        }
        /// <summary>
        /// Cms page result selectcmsresult function
        /// </summary>
        /// <param name="Division"></param>
        /// <param name="Keywords"></param>
        /// <param name="ItemCount"></param>
        /// <param name="PageNumber"></param>
        /// <param name="Storesk"></param>
        /// <returns></returns>
        public override DataSet SelectCmsResult(string Division, string Keywords, int ItemCount, int PageNumber, int Storesk)
        {
            string KeyWordWeightage = string.Empty;
            try
            {

                if (!string.IsNullOrEmpty(Keywords))
                {
                    KeyWordWeightage = "ISABOUT (" + "\"" + Keywords + "\"" + " WEIGHT (1),";
                    foreach (string keyword in Keywords.Split(' '))
                    {
                        if (keyword.Trim() != string.Empty)
                            KeyWordWeightage = KeyWordWeightage + "\"" + keyword.Trim() + "\"" + " WEIGHT (0.8)" + ",";
                    }
                    KeyWordWeightage = KeyWordWeightage.TrimEnd(',') + ") ";
                }

                return SqlHelper.ExecuteDataset(ConnectionString, Constants.SP_CMSPAGERESULT,
                    new SqlParameter("@DIVISION", Division),
                    new SqlParameter("@KEYWORD", Keywords),
                    new SqlParameter("@KEYWORD_WEIGHTAGE", KeyWordWeightage),
                    new SqlParameter("@NO_OF_RECORDS_PER_PAGE", ItemCount),
                    new SqlParameter("@PAGE_NO", PageNumber),
                    new SqlParameter("@STORESK", Storesk));
            }
            catch (Exception ex) { throw ex; }

        }

        /// <summary>
        /// For Advance Multi isbns results
        /// </summary>
        /// <param name="searchEngine"></param>
        /// <returns></returns>
        public override SqlDataReader GetAdvanceIsbnResults(SearchEngine searchEngine)
        {
            DataTable TestAttributedataTable = new DataTable();
            TestAttributedataTable.Columns.Add("Attribute_Type_SK", typeof(int));
            TestAttributedataTable.Columns.Add("Attribute_Type_Value_SK", typeof(int));



            if (!string.IsNullOrEmpty(searchEngine.Facets))
            {
                string[] facets = searchEngine.Facets.Split(':');
                foreach (string facet in facets)
                {
                    string[] simFacets = facet.Split('_');
                    if (simFacets.Length == 1)
                    {
                        simFacets = new string[2];
                        simFacets[0] = facet;
                        simFacets[1] = "0";
                    }
                    string[] innerFacets = simFacets[1].Split(',');
                    foreach (string innerFacet in innerFacets)
                    {
                        TestAttributedataTable.Rows.Add(Convert.ToInt32(simFacets[0]), Convert.ToInt32(innerFacet));
                    }

                }
            }

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlParameter pageNoParam = new SqlParameter("@PAGE_NO", searchEngine.PageNumber);
            SqlParameter divisionParam = new SqlParameter("@DIVISION", searchEngine.Division);
            SqlParameter storeParam = new SqlParameter("@STORESK", searchEngine.StoreSK);
            SqlParameter searchParam = new SqlParameter("@SEARCHTEXT", searchEngine.SearchText);
            SqlParameter no_Of_Records = new SqlParameter("@NO_OF_RECORDS_PER_PAGE", searchEngine.NumberOfResults);
            SqlParameter sortParam = new SqlParameter("@SORT_ORDER", searchEngine.SortOrder);
            SqlParameter country_code = new SqlParameter("@COUNTRY_CODE", searchEngine.CountryCode);
            SqlParameter attrtvp = new SqlParameter();
            attrtvp.ParameterName = "@TESTATTRIBUTETVP";
            attrtvp.Value = TestAttributedataTable;
            attrtvp.SqlDbType = SqlDbType.Structured;
            attrtvp.TypeName = "AttributeSearchTVP";

            SqlParameter attrStatetvp = new SqlParameter();
            attrStatetvp.ParameterName = "@ATTRIBUTE_UI_STATE";
            attrStatetvp.Value = searchEngine.AttributeStateTable;
            attrStatetvp.SqlDbType = SqlDbType.Structured;
            attrStatetvp.TypeName = "ATTRIBUTE_UI_STATE";

            SqlCommand comm = new SqlCommand("ECOMM_MUTLI_ISBN_SEARCH_MIDIFIED", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add(pageNoParam);
            comm.Parameters.Add(divisionParam);
            comm.Parameters.Add(storeParam);
            comm.Parameters.Add(searchParam);
            comm.Parameters.Add(no_Of_Records);
            comm.Parameters.Add(sortParam);
            comm.Parameters.Add(country_code);
            comm.Parameters.Add(attrtvp);
            comm.Parameters.Add(attrStatetvp);
            return comm.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion


        #endregion

    }

}