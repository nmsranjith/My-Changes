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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.FeaturedSearch.Components.Controller;
using DotNetNuke.Modules.HESearchResults.Components.Controller;
using DotNetNuke.Modules.HESearchResults.Components.Modal;
using DotNetNuke.Modules.FeaturedSearch.Components.Modal;

namespace DotNetNuke.Modules.FeaturedSearch
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditFeaturedSearch class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from FeaturedSearchModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : FeaturedSearchModuleBase
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            SqlDataReader searchResult=null, reader= null;
            try
            {
                FModuleIdHdn.Value=this.ModuleId.ToString();
                if (!IsPostBack)
                {
                    Visitor UserDetailInfo = null;
                    UserDetailInfo = new Visitor();
                    //Check whether logged in user info available in session
                    if (Session["UserInfo"] != null)
                        UserDetailInfo = (Visitor)(Session["UserInfo"]);
                    DataTable AttributeStateTable = new DataTable();
                    AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE", typeof(string));
                    AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_SK", typeof(int));
                    AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE", typeof(string));
                    AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE_SK", typeof(int));
                    AttributeStateTable.Columns.Add("PROD_COUNT", typeof(int));
                    AttributeStateTable.Columns.Add("IS_CURRENT", typeof(char));
                    AttributeStateTable.Columns.Add("IS_SELECTED", typeof(char));
                    AttributeStateTable.Columns.Add("IS_PARENT", typeof(char));

                    DataTable showOnlyAttributeTable = new DataTable();
                    showOnlyAttributeTable.Columns.Add("SHOWONLY_FILTER_TYPE", typeof(string));

                    DataTable facetAttributeTable = new DataTable();
                    facetAttributeTable.Columns.Add("ATTRIBUTE_TYPE_SK", typeof(int));
                    facetAttributeTable.Columns.Add("ATTRIBUTE_TYPE_VALUE_SK", typeof(int));

                    SearchParameters sParams = new SearchParameters()
                    {
                        NumberOfResults = Null.SetNullInteger(ConfigurationManager.AppSettings["NO_OF_RECORDS_PER_PAGE"]),
                        SearchText = string.Empty,//query[0],
                        ExactText = string.Empty,
                        PageNumber = 1,
                        Division = "gale",
                        SortOrder = "R",
                        StoreSK = Null.SetNullInteger(UserDetailInfo.StoreID),
                        UserSk = UserDetailInfo.UserID,
                        ShowOnlyAttributesTable = showOnlyAttributeTable,
                        FacetAttributesTable = facetAttributeTable,
                        DisciplineType = string.Empty,
                        CategoryValue = string.Empty,
                        DisciplineValue = string.Empty,
                        SubjectValue = string.Empty,
                        Country = UserDetailInfo.CountryCode,
                        Condition = string.Empty,
                        Weightage = string.Empty,
                        FormsString = string.Empty,//ReturnWordsSearched(q, query[0])
                        AttributeStateTable = AttributeStateTable
                    };
                    searchResult = HESearchResultController.Instance.GetGaleProductResults(sParams);
                    searchResult.NextResult();
                    searchResult.NextResult();
                    searchResult.NextResult();
                    searchResult.NextResult();
                    searchResult.NextResult();
                    FSFacetDrpDwn.DataSource = searchResult;
                    FSFacetDrpDwn.DataBind();

                    CurrentSearchListRptr.DataSource = FeaturedSearchController.Instance.GetAllFeaturedSearches(new FeaturedSearches() { ModuleId = this.ModuleId });
                    CurrentSearchListRptr.DataBind();

                    if (Request.QueryString["fsearch"] != null)
                    {
                        reader = FeaturedSearchController.Instance.GetFeaturedSearchForEdit(new FeaturedSearches() { FeaturedSearchSk = Null.SetNullInteger(Request.QueryString["fsearch"]) });
                        while (reader.Read())
                        {
                            SearchNameTxt.Value = Null.SetNullString(reader["SEARCH_NAME"]);
                            AdvTitleTxt.Value = Null.SetNullString(reader["TITLE"]);
                            AdvAuthorTxt.Value = Null.SetNullString(reader["AUTHOR"]);
                            AdvSubjectTxt.Value = Null.SetNullString(reader["SUBJECT"]);
                            AdvFormatTxt.Value = Null.SetNullString(reader["FORMAT"]);
                            AdvLibraryTypeTxt.Value = Null.SetNullString(reader["LIBRARY_TYPE"]);
                            AdvOriginTxt.Value = Null.SetNullString(reader["ORIGIN"]);
                            AdvPublishedYearTxt.Value = Null.SetNullString(reader["PUBLISHED_YEAR"]);
                            AdvPublisherTxt.Value = Null.SetNullString(reader["PUBLISHER"]);
                            AdveBookPlatformTxt.Value = Null.SetNullString(reader["EBOOK_PLATFORM"]);
                            AdvAllWordsTxt.Value = Null.SetNullString(reader["ALL_WORDS"]);
                            AdvExactPhraseTxt.Value = Null.SetNullString(reader["EXACT_PHRASE"]);
                            AdvNoWordsTxt.Value = Null.SetNullString(reader["NONE_OF_THESE"]);
                        }
                        reader.Dispose();
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                LogFileWrite(exc);
            }
            finally
            {
                if(searchResult!=null)
                    searchResult.Dispose();               
            }
        }

        #endregion

    }

}