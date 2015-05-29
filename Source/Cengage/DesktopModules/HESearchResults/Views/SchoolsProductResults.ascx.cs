using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Cengage.eCommerce.Lib;
using DotNetNuke.Modules.HESearchResults.Controls;
using DotNetNuke.Modules.HESearchResults.Components.Controller;
using System.Text.RegularExpressions;
using DotNetNuke.Modules.HESearchResults.Components;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Common.Utilities;
using System.IO;
using System.Configuration;
using DotNetNuke.Modules.HESearchResults.Components.Common;
using Cengage.Ecommerce.CengageServiceLibrary;
using DotNetNuke.Instrumentation;
using Cengage.Ecommerce.CengageServiceClient;
using DotNetNuke.Modules.HESearchResults.Components.Modal;

namespace DotNetNuke.Modules.HESearchResults.Views
{
    public partial class SchoolsProductResults : System.Web.UI.UserControl
    {
        #region Event Handlers



        Dictionary<string, List<Facets>> dicAttr = new Dictionary<string, List<Facets>>();
        DataSet CollQuoteModules;
        Visitor UserDetailInfo = null;

        private static string[] stopWords = new string[] { };
        private static Func<string, int?> NWORDS;
        private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
        protected List<ListInfo> CollListModules;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            DnnLog.Error("pageload starts");
            List<Facets> facetList = new List<Facets>();
            List<CengageSearchResult> searResults = new List<CengageSearchResult>();
            int ItemCount = int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"]);
            List<Cengage.Ecommerce.CengageServiceLibrary.Product> ProductPriceList = new List<Cengage.Ecommerce.CengageServiceLibrary.Product>();
            List<Cengage.Ecommerce.CengageServiceLibrary.Product> ProductAvailable = new List<Cengage.Ecommerce.CengageServiceLibrary.Product>();
            SearchController IECPR = new SearchController();
            string cmsproductCount = string.Empty;
            try
            {
                string queryKeyword = string.Empty;
                string cmsqueryKeyword = string.Empty;
                bool emptysearch = false;
                if (Request.QueryString["q"] != null)
                {
                    queryKeyword = GetCorrectText(Request.QueryString["q"]);
                    emptysearch = true;
                }
                DnnLog.Error("cms data preparation starts");
                string Division = (Request.QueryString["division"] != null) ? Request.QueryString["division"] : "all";
                DataSet cmsdata = new DataSet();
                UserDetailInfo = (Visitor)Session["UserInfo"];
                int Storesk = Convert.ToInt32(UserDetailInfo.StoreID);
                if (emptysearch == false)
                {
                    if (Request.Params["k_q"] != "")
                    {
                        queryKeyword = Request.Params["k_q"];
                        Division = "both";
                    }
                    if (Request.Params["k_q"] == "" && Request.Params["t_q"] == "" && Request.Params["a_q"] == "" && Request.Params["Sub_q"] == "" && Request.Params["fa_q"] == "" && Request.Params["fatv_q"] == "" && Request.Params["all_q"] == "" && Request.Params["et_q"] == "")
                    {
                        //queryKeyword = "";
                        Division = "both";
                    }
                    if (Request.Params["k_q"] == "" || Request.Params["all_q"] == "" || Request.Params["et_q"] == "")
                    {
                        //queryKeyword = "";
                        Division = "both";
                    }
                }
                if (queryKeyword.Contains('Ñ'))
                {
                    string[] querys = queryKeyword.Split('Ñ');
                    if (querys.Length > 1)
                    {
                        cmsqueryKeyword = querys[0];
                    }
                }
                else
                {

                    cmsqueryKeyword = queryKeyword;

                }
                DnnLog.Error("queryKeyword__" + cmsqueryKeyword);
                cmsdata = IECPR.SelectCms(Division, cmsqueryKeyword, int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"].ToString()), 1, Storesk);
                cmsproductCount = (cmsdata.Tables[1].Rows.Count > 0) ? cmsdata.Tables[1].Rows[0]["totalrows"].ToString() : "0";
                DnnLog.Error("queryKeyword__" + cmsqueryKeyword + "count__" + cmsproductCount + ":" + cmsdata.Tables[0].Rows.Count.ToString());
                Session["pagecount"] = cmsproductCount;
                cmsPagenum.Value = cmsproductCount;
                if ((Session["PageCount"]) != null)
                {
                    Hidpagecnt.Value = Session["PageCount"].ToString();
                }
                else
                {
                    Hidpagecnt.Value = "0";
                }
                if (Request.QueryString["cms"] == null)
                {
                    SetPagingDefaultscms(int.Parse(Hidpagecnt.Value), int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"].ToString())
                        , 0);
                }
                DnnLog.Error("cms data preparation completed");
                string[] query = queryKeyword.Split('Ñ');
                if (query.Length > 1) queryKeyword = query[1];
                DnnLog.Error("queryKeyword is :" + queryKeyword);
                if (queryKeyword == "DCDEFK")
                {
                    if (Hidpagecnt.Value == "0")
                    {
                        ProductPlace.Visible = true;
                        ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsNoResults.ascx"));
                        Label lblbannertxt = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                        lblbannertxt.Text = "No result found";
                        MainHold.Style.Add("display", "none");
                        return;
                    }
                    else
                    {
                        ProductPlace.Visible = true;
                        ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsNoResults.ascx"));
                        Label lblbannertxt = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                        lblbannertxt.Text = "No result found";
                        MainHold.Style.Add("display", "block");
                        Cmsplace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsCMSResults.ascx"));
                        return;
                    }
                }
                else if (queryKeyword == "DCDEFK-KCDEFD")
                {
                    SearchEngineModel searchEngineModel = new SearchEngineModel();
                    string division = (Request.QueryString["division"] != null) ? Request.QueryString["division"] : "both";
                    bool hasResults = searchEngineModel.CheckResultsForCorrectedWord(query[0], division, Storesk);
                    if (hasResults)
                    {
                        if (Hidpagecnt.Value == "0")
                        {
                            ProductPlace.Visible = true;
                            didumeanword.Value = query[0];
                            ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsNoResults.ascx"));
                            Label lblbannertxt = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                            lblbannertxt.Text = "No result found";
                            MainHold.Style.Add("display", "none");
                            return;
                        }
                        else
                        {
                            ProductPlace.Visible = true;
                            didumeanword.Value = query[0];
                            ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsNoResults.ascx"));
                            Label lblbannertxt = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                            lblbannertxt.Text = "No result found";
                            MainHold.Style.Add("display", "block");
                            Cmsplace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsCMSResults.ascx"));
                            return;
                        }
                    }
                    else
                    {
                        if (Hidpagecnt.Value == "0")
                        {
                            ProductPlace.Visible = true;
                            ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsNoResults.ascx"));
                            Label lblbannertxt = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                            lblbannertxt.Text = "No result found";
                            MainHold.Style.Add("display", "none");
                            return;
                        }
                        else
                        {
                            ProductPlace.Visible = true;
                            ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsNoResults.ascx"));
                            Label lblbannertxt = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                            lblbannertxt.Text = "No result found";
                            MainHold.Style.Add("display", "block");
                            Cmsplace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsCMSResults.ascx"));
                            return;
                        }
                    }
                }
                else
                {
                    string productCount = string.Empty;
                    DataTable AttributeStateTable = new DataTable();
                    AttributeStateTable.Columns.Add("ATTRIBUTE_NAME", typeof(string));
                    AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_SK", typeof(int));
                    AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE", typeof(string));
                    AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE_SK", typeof(int));
                    AttributeStateTable.Columns.Add("PROD_COUNT", typeof(int));
                    AttributeStateTable.Columns.Add("IS_MULTI_SELECT", typeof(char));
                    AttributeStateTable.Columns.Add("IS_CURRENT", typeof(char));
                    AttributeStateTable.Columns.Add("IS_SELECTED", typeof(char));
                    AttributeStateTable.Columns.Add("SEQNUM", typeof(int));

                    SearchEngine searchEngine = new SearchEngine();

                    //List<Facets> list = new List<Facets>();
                    SearchEngineModel searchEngineModel = new SearchEngineModel();
                    UserDetailInfo = (Visitor)Session["UserInfo"];

                    searchEngine.NumberOfResults = ItemCount;
                    searchEngine.PageNumber = (Request.QueryString["p"] != null) ? Convert.ToInt32(Request.QueryString["p"]) : 1;
                    searchEngine.SortOrder = (Request.QueryString["s"] != null) ? Request.QueryString["s"] : "R";
                    searchEngine.StoreSK = Convert.ToInt32(UserDetailInfo.StoreID);
                    searchEngine.CountryCode = UserDetailInfo.CountryCode;
                    searchEngine.Division = (Request.QueryString["division"] != null) ? Request.QueryString["division"] : "all";
                    searchEngine.SearchText = query[0];
                    searchEngine.ExactText = (Request.QueryString["q"] != null) ? Request.QueryString["q"] : string.Empty;
                    DnnLog.Error("SearchText is :" + query[0]);

                    searchEngine.Facets = (Request.QueryString["f"] != null) ? Request.QueryString["f"] : string.Empty;
                    if (string.IsNullOrEmpty(Request.QueryString["f"]))
                        Session["AttributeState"] = null;
                    searchEngine.AttributeStateTable = (Request.QueryString["f"] != null) ? ((Session["AttributeState"] != null) ? Session["AttributeState"] as DataTable : AttributeStateTable) : AttributeStateTable;
                    if (UserDetailInfo.CountryCode.ToUpper() == "NZ")
                        ((DotNetNuke.Framework.CDefault)this.Page).Title = "Search" + " " + searchEngine.SearchText + " " + "Page" + " " + searchEngine.PageNumber + " | " + (searchEngine.Division.ToLower() == "primary" ? "Primary" : "Secondary") + " | Cengage Learning - New Zealand";
                    else
                        ((DotNetNuke.Framework.CDefault)this.Page).Title = "Search" + " " + searchEngine.SearchText + " " + "Page" + " " + searchEngine.PageNumber + " | " + (searchEngine.Division.ToLower() == "primary" ? "Primary" : "Secondary") + " | Cengage Learning - Australia";

                    SqlDataReader dataReader = null;
                    // Checking Search type is Advance search or not
                    if (Request.QueryString["searchtype"] != null)
                    {
                        switch (Request.QueryString["searchtype"].ToLower())
                        {
                            case "advkey":
                                searchEngine.Division = "all";
                                searchEngine.AndAllwordsWeighate = (Request.QueryString["all_q"] != null) ? GetCorrectTextForAdv(Request.QueryString["all_q"]) : string.Empty;
                                searchEngine.AttributeValueWeighate = (Request.QueryString["fatv_q"] != null) ? GetCorrectTextForAdv(Request.QueryString["fatv_q"]) : string.Empty;
                                searchEngine.AttributeWeighate = (Request.QueryString["fa_q"] != null) ? GetCorrectTextForAdv(Request.QueryString["fa_q"]) : string.Empty;
                                searchEngine.AuthorWeighate = (Request.QueryString["a_q"] != null) ? GetCorrectTextForAdv(Request.QueryString["a_q"]) : string.Empty;
                                searchEngine.ExactphraseWeighate = (Request.QueryString["et_q"] != null) ? GetCorrectTextForAdv(Request.QueryString["et_q"]) : string.Empty;
                                searchEngine.KeyWordWeightage = (Request.QueryString["k_q"] != null) ? GetCorrectTextForAdv(Request.QueryString["k_q"]) : string.Empty;
                                searchEngine.SubjectWeighate = (Request.QueryString["Sub_q"] != null) ? GetCorrectTextForAdv(Request.QueryString["Sub_q"]) : string.Empty;
                                searchEngine.TitleWeighate = (Request.QueryString["t_q"] != null) ? GetCorrectTextForAdv(Request.QueryString["t_q"]) : string.Empty;
                                dataReader = searchEngineModel.GetAdvanceKeywordResults(searchEngine);
                                if (searchEngine.AndAllwordsWeighate == "" && searchEngine.AttributeValueWeighate == "" && searchEngine.AttributeWeighate == ""
                                    && searchEngine.AuthorWeighate == "" && searchEngine.ExactphraseWeighate == "" && searchEngine.KeyWordWeightage == ""
                                    && searchEngine.SubjectWeighate == "" && searchEngine.TitleWeighate == "")
                                {
                                    dataReader = searchEngineModel.GetComponentResults(searchEngine);
                                }
                                break;
                            case "advpri":
                                break;
                            case "advsec":
                                break;
                            case "advisbn":
                                searchEngine.Division = "all";
                                Hidpagecnt.Value = "0";
                                if (HttpContext.Current.Session.SessionID + "isbn" == HttpUtility.UrlDecode(Request.QueryString["keyisbn"]))
                                    searchEngine.SearchText = Cache[HttpUtility.UrlDecode(Request.QueryString["keyisbn"])] as string;
                                else
                                    searchEngine.SearchText = string.Empty;
                                DnnLog.Error("Search data preparation starts gnana");
                                dataReader = searchEngineModel.GetAdvanceIsbnResults(searchEngine);
                                DnnLog.Error("Search data preparation End");
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        DnnLog.Error("Search data preparation starts");
                        dataReader = searchEngineModel.GetComponentResults(searchEngine);
                        DnnLog.Error("Search data preparation completed");
                    }
                    if (dataReader.HasRows)
                    {
                        string Prdisbn_13 = string.Empty;
                        string PrdTitle = string.Empty;
                        string producttitle = string.Empty;
                        while (dataReader.Read())
                        {

                            CengageSearchResult search = new CengageSearchResult();
                            Product ProductPrice = new Product();
                            Product ProductAvail = new Product();

                            Prdisbn_13 = search.Isbn13 = Null.SetNullString(dataReader["ISBN_13"]);
                            search.Author = Null.SetNullString(dataReader["PREFERRED_NAME"]);
                            producttitle = search.Title = Null.SetNullString(dataReader["TITLE"]);
                            PrdTitle = search.Title = Server.HtmlEncode(Null.SetNullString(dataReader["TITLE"]));
                            search.ImagePath = ConfigurationManager.AppSettings["HTTP_IMAGE"].ToString() + Null.SetNullString(dataReader["IMAGE_FILE_NAME"]);
                            search.ProductSK = Null.SetNullInteger(dataReader["PRODUCT_SK"]);

                            search.Chkstatus = false;
                            search.AddStatus = false;
                            search.DiscountFlag = false;
                            search.RrpFlag = false;
                            search.DiscountVal = string.Empty;
                            search.StockAvail = "Available";
                            search.QtyVal = 0;
                            search.RRPPrice = 0;
                            search.DiscountPrice = 0;
                            search.AllowSale = "Y";
                            search.Exacturl = CengageSearchResult.getProductURL(search.Isbn13 + "/division=" + Division, producttitle);

                            ProductPrice.ISBN = Null.SetNullString(dataReader["ISBN_13"]);
                            ProductPrice.IsGSTIncluded = UserDetailInfo.GstApplicable == "Y" ? true : false;
                            ProductPrice.CurrencyCode = UserDetailInfo.CurrencyCode;
                            ProductPrice.ToCountry = UserDetailInfo.CountryCode;
                            ProductPrice.AccountNumber = UserDetailInfo.TradingAccountSK.ToString();
                            ProductAvail.ISBN = Null.SetNullString(dataReader["ISBN_13"]);
                            ProductAvail.IsGSTIncluded = UserDetailInfo.GstApplicable == "Y" ? true : false;
                            ProductAvail.CurrencyCode = UserDetailInfo.CurrencyCode;
                            ProductAvail.ToCountry = UserDetailInfo.CountryCode;
                            ProductAvail.AccountNumber = UserDetailInfo.TradingAccountSK.ToString();
                            searResults.Add(search);
                            ProductPriceList.Add(ProductPrice);
                            ProductAvailable.Add(ProductAvail);
                            HESearchResultsModuleBase.LogValues(string.Concat("Search Results TradingAccountSK -->", UserDetailInfo.TradingAccountSK, "\r\n Search Results CountryCode -->", UserDetailInfo.CountryCode));
                        }



                        dataReader.NextResult();
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                productCount = Null.SetNullString(dataReader["ProductCount"]);
                                PrdCount.InnerHtml = productCount;
                            }
                            if (Request.QueryString["f"] == null)
                            {
                                if ((int.Parse(productCount) == 1) && (int.Parse(cmsproductCount) == 0))
                                {
                                    string Prdurl;
                                    string pPrdisbn_13 = Prdisbn_13;
                                    string pPrdTitle = producttitle;
                                    Prdurl = CengageSearchResult.getProductURL(Prdisbn_13, producttitle.Trim());
                                    Response.Redirect(Prdurl + "/division=" + Division, false);
                                }

                            }
                        }


                        dataReader.NextResult();
                        if (dataReader.HasRows)
                        {
                            int Attrbutetype_sk = -1;
                            int prdcount = 0, count = 0;
                            string attrName = "STATE";
                            List<Facets> lst = new List<Facets>();
                            while (dataReader.Read())
                            {
                                Facets facets = new Facets();
                                facets.ATTRIBUTE_NAME = Server.HtmlEncode(Null.SetNullString(dataReader["ATTRIBUTE_NAME"]));
                                facets.ATTRIBUTE_TYPE_VALUE = Server.HtmlEncode(Null.SetNullString(dataReader["ATTRIBUTE_TYPE_VALUE"]));
                                facets.PROD_COUNT = Null.SetNullInteger(dataReader["ProductCount"]);
                                facets.IS_MULTI_SELECT = Null.SetNullString(dataReader["IS_MULTI_SELECT"]);
                                facets.ATTRIBUTE_TYPE_SK = Null.SetNullInteger(dataReader["ATTRIBUTE_TYPE_SK"]);
                                facets.ATTRIBUTE_TYPE_VALUE_SK = Null.SetNullInteger(dataReader["ATTRIBUTE_TYPE_VALUE_SK"]);
                                facets.SEQNUM = Null.SetNullInteger(dataReader["SEQNUM"]);
                                facets.IS_SELECTED = Null.SetNullString(dataReader["IS_SELECTED"]);
                                if (attrName == Null.SetNullString(dataReader["ATTRIBUTE_NAME"]))
                                {
                                    if (count == 0)
                                        prdcount = Null.SetNullInteger(dataReader["ProductCount"]);
                                    if (prdcount == Null.SetNullInteger(dataReader["ProductCount"]) && prdcount == int.Parse(productCount))
                                    {
                                        count++;
                                    }
                                }

                                if (Attrbutetype_sk != Null.SetNullInteger(dataReader["ATTRIBUTE_TYPE_SK"]))
                                {
                                    lst = new List<Facets>();
                                    dicAttr.Add(Null.SetNullInteger(dataReader["ATTRIBUTE_TYPE_SK"]).ToString() + "|" + Null.SetNullString(dataReader["ATTRIBUTE_NAME"]), lst);
                                }
                                lst.Add(facets);
                                Attrbutetype_sk = Null.SetNullInteger(dataReader["ATTRIBUTE_TYPE_SK"]);
                                prdcount = Null.SetNullInteger(dataReader["ProductCount"]);

                            }
                            if (count == Convert.ToInt32(ConfigurationManager.AppSettings["SateCount"].ToString()))
                            {
                                lst.RemoveAll(x => x.ATTRIBUTE_NAME == attrName);
                                var keysToRemove = dicAttr.Keys.Except(dicAttr.Keys.Where(x => x.Split('|')[1] != attrName)).ToList();

                                foreach (var key in keysToRemove)
                                    dicAttr.Remove(key);
                            }


                        }
                    }
                    else
                    {
                        if (Hidpagecnt.Value == "0")
                        {
                            ProductPlace.Visible = true;
                            ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsNoResults.ascx"));
                            Label lblbannertxt = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                            lblbannertxt.Text = "No result found";
                            MainHold.Style.Add("display", "none");
                            return;
                        }
                        else
                        {
                            ProductPlace.Visible = true;
                            ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsNoResults.ascx"));
                            Label lblbannertxt = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                            lblbannertxt.Text = "No result found";
                            MainHold.Style.Add("display", "block");
                            Cmsplace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsCMSResults.ascx"));
                            return;

                        }
                    }
                    dataReader.Close();
                    DnnLog.Error("Search data binding starts");
                    RightBind(searResults, ProductPriceList, ProductAvailable);
                    DnnLog.Error("Search data binding starts");
                    DnnLog.Error("Facet data binding starts");
                    LeftmainHeading.DataSource = dicAttr;
                    LeftmainHeading.DataBind();
                    DnnLog.Error("Facet data binding completed");
                    hdnProdCount.Value = productCount;
                    hdnItemCount.Value = ConfigurationManager.AppSettings["ITEMCOUNT"].ToString();

                    int startCount = 0, totalcount = 0;
                    if (Request.QueryString["p"] != null)
                    {
                        int totalpage = (int.Parse(productCount)) % int.Parse(hdnItemCount.Value) == 0 ? (int.Parse(productCount)) / int.Parse(hdnItemCount.Value) : (int.Parse(productCount)) / int.Parse(hdnItemCount.Value) + 1;
                        if (totalpage > int.Parse(Request.QueryString["p"]) + 4)
                        {
                            if (int.Parse(Request.QueryString["p"]) - 5 >= 0)
                                startCount = int.Parse(Request.QueryString["p"]) - 5;
                            else
                                startCount = 0;
                        }
                        else
                        {
                            if (totalpage >= 10)
                                startCount = int.Parse(Request.QueryString["p"]) - 9;
                            else
                                startCount = 0;
                        }
                    }
                    else
                    {
                        startCount = 0;
                    }

                    SetPagingDefaults(int.Parse(productCount), int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"].ToString())
                        , startCount);


                    //SetPagingDefaults(int.Parse(productCount), int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"].ToString())
                    //    , (Request.QueryString["p"] != null) ? (int.Parse(Request.QueryString["p"]) > 10) ? int.Parse(Request.QueryString["p"]) - 9 : 0 : 0);
                    UserIDHiddenField.Value = UserDetailInfo.UserID.ToString();
                    if (UserDetailInfo.UserID > 0)
                        CheckAddToList(UserDetailInfo.UserID);
                    BindListHeader(UserDetailInfo.UserID, "List");
                    BindQuoteHeader(UserDetailInfo.UserID);

                    if (Request.QueryString["cms"] != null)
                    {
                        Cmsplace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsCMSResults.ascx"));
                    }
                }
            }

            catch (Exception exc) //Module failed to load
            {
                //Exceptions.ProcessModuleLoadException(this, exc);
                DnnLog.Error("search error" + exc.Message);
                ProductPlace.Visible = true;
                ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsNoResults.ascx"));
                Label lblbannertxt = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                lblbannertxt.Text = "No result found";
                MainHold.Style.Add("display", "none");
            }
        }



        /// <summary>
        /// Product result getdictionary function
        /// </summary>
        /// <returns></returns>
        private string GetDictionary()
        {
            StringBuilder DictionaryWords = new StringBuilder();
            Visitor visitor = null;
            if (HttpContext.Current.Session["UserInfo"] != null)
            {
                visitor = (Visitor)HttpContext.Current.Session["UserInfo"];
            }
            else
            {
                visitor = new Visitor();
                visitor.CountryCode = "AU";
            }
            string GetDicQuery = string.Empty;

            if (visitor.CountryCode == "NZ")
            {

                GetDicQuery = "SELECT SEARCH_WORD FROM CEN_NZ_DICTIONARY";

                if (Cache["DICT_AU_WORDS"] == null)
                {
                    DnnLog.Trace("Connecting DB to load dictionary..");
                    IDataReader DataReader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString, CommandType.Text, GetDicQuery);
                    while (DataReader.Read())
                    {
                        DictionaryWords.Append(DataReader[0].ToString());
                        DictionaryWords.Append("\r");
                    }
                    if (DataReader != null)
                        DataReader = null;
                    Cache.Insert("DICT_AU_WORDS", DictionaryWords, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
                    DnnLog.Trace("Succesfully Loaded Dictionary..");
                }
                else
                {
                    DictionaryWords = ((StringBuilder)Cache["DICT_AU_WORDS"]);
                }

            }
            else
            {
                GetDicQuery = "SELECT SEARCH_WORD FROM CEN_AU_DICTIONARY";

                if (Cache["DICT_NZ_WORDS"] == null)
                {
                    DnnLog.Trace("Connecting DB to load dictionary..");
                    IDataReader DataReader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString, CommandType.Text, GetDicQuery);
                    while (DataReader.Read())
                    {
                        DictionaryWords.Append(DataReader[0].ToString());
                        DictionaryWords.Append("\r");
                    }
                    if (DataReader != null)
                        DataReader = null;
                    Cache.Insert("DICT_NZ_WORDS", DictionaryWords, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
                    DnnLog.Trace("Succesfully Loaded Dictionary..");
                }
                else
                {
                    DictionaryWords = ((StringBuilder)Cache["DICT_NZ_WORDS"]);
                }
            }

            return DictionaryWords.ToString();
        }
        /// <summary>
        /// Matching with only alphabets
        /// </summary>
        /// <param name="text"></param>
        /// <returns>words as IEnumerable string</returns>
        private static IEnumerable<string> words(string text)
        {
            return Regex.Matches(text.ToLower(), "[a-z]+")
                        .Cast<Match>()
                        .Select(m => m.Value);
        }

        /// <summary>
        /// goruping train of words
        /// </summary>
        /// <param name="features"></param>
        /// <returns>words as funtion delegate</returns>
        private static Func<string, int?> train(IEnumerable<string> features)
        {
            var dict = features.GroupBy(f => f)
                               .ToDictionary(g => g.Key, g => g.Count());

            return f => dict.ContainsKey(f) ? dict[f] : (int?)null;
        }
        /// <summary>
        /// All word correction happens here
        /// </summary>
        /// <param name="word"></param>
        /// <returns>words as IEnumerable string</returns>
        private static IEnumerable<string> edits1(string word)
        {
            var splits = from i in Enumerable.Range(0, word.Length)
                         select new { a = word.Substring(0, i), b = word.Substring(i) };
            var deletes = from s in splits
                          where s.b != "" // we know it can't be null
                          select s.a + s.b.Substring(1);
            var transposes = from s in splits
                             where s.b.Length > 1
                             select s.a + s.b[1] + s.b[0] + s.b.Substring(2);
            var replaces = from s in splits
                           from c in ALPHABET
                           where s.b != ""
                           select s.a + c + s.b.Substring(1);
            var inserts = from s in splits
                          from c in ALPHABET
                          select s.a + c + s.b;

            return deletes
            .Union(transposes) // union translates into a set
            .Union(replaces)
            .Union(inserts);
        }
        /// <summary>
        /// after edit the word, it will send current word
        /// </summary>
        /// <param name="word"></param>
        /// <returns>words as IEnumerable string</returns>
        private static IEnumerable<string> known_edits2(string word)
        {
            return (from e1 in edits1(word)
                    from e2 in edits1(e1)
                    where NWORDS(e2) != null
                    select e2)
                   .Distinct();
        }

        /// <summary>
        /// returns words after cross check with all words
        /// </summary>
        /// <param name="words"></param>
        /// <returns>words as IEnumerable string</returns>
        private static IEnumerable<string> known(IEnumerable<string> words)
        {
            return words.Where(w => NWORDS(w) != null);
        }
        /// <summary>
        /// finally returns corrected word
        /// </summary>
        /// <param name="word"></param>
        /// <returns>word as string</returns>
        private static string correct(string word)
        {
            var candidates =
                new[] { known(new[] {word}),
                    known(edits1(word)),
                    known_edits2(word),
                    new[] {word} }
                      .First(s => s.Any());

            return candidates.OrderByDescending(c => NWORDS(c) ?? 1).First();
        }

        /// <summary>
        /// Product result get correct text for adv function
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string GetCorrectTextForAdv(string inputText)
        {

            string input = inputText.ToLower().Trim().Replace("-", "");
            if (!string.IsNullOrEmpty(input))
            {
                string filename = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
                if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
                foreach (string word in stopWords)
                {
                    string regexp = @"(?i)\b" + word + @"\b";
                    if (input.Length > 2)
                        input = Regex.Replace(input, regexp, " ");
                    else if (input == "a")
                    {
                        input = Regex.Replace(input, regexp, " ");
                    }
                }
                input = input.ToLower().Trim().Replace("-", "");
                if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\d, ]+$"))
                {
                    input = input.Replace(" ", string.Empty).Trim();
                }
                else
                {
                    input = input.Replace(",", " ");
                    Regex r = new Regex("(?:[^a-z0-9+ ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                    input = r.Replace(input, String.Empty);
                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex(@"[ ]{2,}", options);
                    input = regex.Replace(input, @" ");

                }
                input = input.Trim();

                return input;

            }
            else
            {
                return string.Empty;
            }

        }

        /// <summary>
        /// product result getcorrect text function
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string GetCorrectText(string inputText)
        {
            string dic = GetDictionary();
            NWORDS = train(words(dic));
            string input = inputText.ToLower().Trim().Replace("-", "");
            if (!string.IsNullOrEmpty(input))
            {
                string filename = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
                if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
                foreach (string word in stopWords)
                {
                    string regexp = @"(?i)\b" + word + @"\b";
                    if (input.Length > 2)
                        input = Regex.Replace(input, regexp, " ");
                    else if (input == "a")
                    {
                        input = Regex.Replace(input, regexp, " ");
                    }
                }
                input = input.ToLower().Trim().Replace("-", "");
                if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\d, ]+$"))
                {
                    input = input.Replace(" ", string.Empty).Trim();
                }
                else
                {
                    input = input.Replace(",", " ");
                    Regex r = new Regex("(?:[^a-z0-9+ ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                    input = r.Replace(input, String.Empty);
                    RegexOptions options = RegexOptions.None;
                    Regex regex = new Regex(@"[ ]{2,}", options);
                    input = regex.Replace(input, @" ");

                }
                input = input.Trim();



                string corrected = string.Empty;
                string match = string.Empty;
                if (!string.IsNullOrEmpty(input) && input.ToLower() != "enter your search")
                {
                    string[] dicwords = input.Split(' ');
                    foreach (string word in dicwords)
                    {
                        bool isExists = Regex.IsMatch(dic.ToUpper(), @"\b" + word.ToUpper() + @"\b");
                        if (!isExists)
                        {
                            match = correct(word);
                            corrected = corrected + " " + match;
                        }
                        else
                        {
                            corrected = corrected + " " + word;
                        }
                    }

                    if (input == corrected.ToLower().Trim())
                    {
                        return input;
                    }
                    else
                    {
                        return corrected.ToLower().Trim() + "Ñ" + "DCDEFK-KCDEFD";
                    }
                }
                else
                {
                    return "DCDEFK";
                }

            }
            else
            {
                return string.Empty;
            }

        }


        /// <summary>
        /// product result set paging default function
        /// </summary>
        /// <param name="TotalNoOfRecords"></param>
        /// <param name="NoOfRecordsPerPage"></param>
        /// <param name="FirstValue"></param>
        private void SetPagingDefaults(int TotalNoOfRecords, int NoOfRecordsPerPage, int FirstValue)
        {
            Custompage.CreatePagingControl(TotalNoOfRecords, FirstValue);
            //Custompage.PageButtonStyle(FirstValue);
            if (TotalNoOfRecords > NoOfRecordsPerPage)
            {
                Custompage.DisplayPropertyForPage("block");
            }
            else
            {
                Custompage.DisplayPropertyForPage("none");
            }
        }

        private void SetPagingDefaultscms(int TotalNoOfRecords, int NoOfRecordsPerPage, int FirstValue)
        {
            Cmspageview.CreatePagingControl(TotalNoOfRecords, FirstValue);
            //Custompage.PageButtonStyle(FirstValue);
            if (TotalNoOfRecords > NoOfRecordsPerPage)
            {
                Cmspageview.DisplayPropertyForPage("block");
            }
            else
            {
                Cmspageview.DisplayPropertyForPage("none");
            }
        }



        /// <summary>
        /// Product result right bind function
        /// </summary>
        /// <param name="searResults"></param>
        /// <param name="ProductPriceList"></param>
        /// <param name="ProductAvailableList"></param>
        private void RightBind(List<CengageSearchResult> searResults, List<Product> ProductPriceList, List<Product> ProductAvailableList)
        {
            bool Isbmup = false;
            using (PriceSvcClient pricesvcclient = new PriceSvcClient())
            {
                ProductPriceList = pricesvcclient.GetProductPrice(ProductPriceList);
                ProductAvailableList = pricesvcclient.GetProductAvailablity(ProductAvailableList);
            }
            try
            {
                int i = 0;
                DnnLog.Error("web service call begin");
                searResults.ForEach(u =>
                {
                    Product ProdPrice = ProductPriceList[i];
                    Product ProdAvail = ProductAvailableList[i];
                    if (ProdPrice.UnitPriceActual < ProdPrice.RRP)
                    {
                        u.PriceVal = "$" + String.Format("{0:#,##0.00}", Convert.ToDecimal(ProdPrice.UnitPriceActual.ToString()));
                        u.DiscountFlag = true;
                        u.DiscountVal = "RRP $ " + ProdPrice.RRP + ", YOU SAVE $ " + String.Format("{0:#,##0.00}", (ProdPrice.RRP - ProdPrice.UnitPriceActual));
                        u.RrpFlag = false;
                    }
                    else
                    {
                        u.PriceVal = "$" + String.Format("{0:#,##0.00}", Convert.ToDecimal(ProdPrice.RRP.ToString()));
                        u.DiscountFlag = false;
                        u.RrpFlag = true;
                    }
                    u.StockAvail = ProdAvail.StockStatus.ToUpper();
                    DnnLog.Info("He search stock availability" + u.StockAvail);
                    u.AllowSale = ProdAvail.AllowSale.ToString();
                    DnnLog.Info("He search stock allowsale" + u.AllowSale);

                    u.IsCachedPrice = (ProdAvail.ePriceSource == PriceSource.CENGAGE_AS400) ? "N" : "Y";
                    if (u.IsCachedPrice == "N")
                    {
                        Isbmup = true;
                    }

                    if (UserDetailInfo.TradingAccountSK > 6)
                    {
                        u.Stockqty = int.Parse(ProdAvail.StockAvailableQuantity.ToString());
                        u.Stockavailable = "Available stock";
                        if (u.Stockqty > 0)
                        {
                            u.Stockqty = int.Parse(ProdAvail.StockAvailableQuantity.ToString());
                        }
                        else
                        {
                            u.Stockqty = 0;
                        }
                        if (u.IsCachedPrice == "Y")
                        {
                            u.Stockqty = int.Parse(ProdPrice.StockAvailableQuantity.ToString());
                            //Isbmup = true;
                            if (u.Stockqty > 0)
                            {
                                u.Stockqty = int.Parse(ProdPrice.StockAvailableQuantity.ToString());
                            }
                            else
                            {
                                u.Stockqty = 0;
                            }
                        }
                    }
                    else
                    {
                        u.Stockqty = null;
                        u.Stockavailable = "";
                    }
                    u.RRPPrice = ProdPrice.RRP;
                    u.DiscountPrice = ProdPrice.UnitPriceActual;
                    HESearchResultsModuleBase.LogValues(string.Concat("Search Results RRP Price -->", ProdPrice.RRP));
                    i = i + 1;
                });
                DnnLog.Error("web service call end");

                rdProducts.DataSource = searResults.Where(x => x.AllowSale == "Y");
                rdProducts.DataBind();
                if (!Isbmup)
                {
                    AddtoQuoteDiv.Disabled = true;
                    AddtoQuoteDiv.Style.Add("cursor", "no-drop");
                    AddtoQuote.Enabled = false;
                    AddtoQuote.Style.Add("cursor", "no-drop");
                }
            }
            catch (Exception ex)
            {
                DnnLog.Error("Webserive binding time error : " + ex.Message);
            }
        }

        /// <summary>
        /// Product result leftmain heading bind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LeftmainHeading_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Repeater leftSubHeading = (Repeater)e.Item.FindControl("leftSubHeading");
                Label MainTitle = (Label)e.Item.FindControl("MainTitle");
                leftSubHeading.DataSource = dicAttr[MainTitle.ToolTip + "|" + MainTitle.Text];
                leftSubHeading.DataBind();
            }
            catch (Exception ex)
            {
                //DnnError
            }

        }

        /// <summary>
        /// Product result bindlistheader
        /// </summary>
        /// <param name="userSk"></param>
        /// <param name="type"></param>
        private void BindListHeader(int? userSk, string type)
        {
            try
            {
                SearchController IECPR = new SearchController();
                CollListModules = new List<ListInfo>();
                CollListModules = IECPR.GetProduct(Constants.FLG_LISTQUOTEHEADER, "", userSk, "List");
                HtmlGenericControl lblCart = new HtmlGenericControl();
                lblCart = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("Utilities1").FindControl("WishList");
                if (CollListModules.Count != 0)
                    lblCart.InnerText = "(" + CollListModules.Count + ")";
                else
                    lblCart.InnerText = "";
                ListInfo LI = new ListInfo { Name = "Create New List", UserListQuoteSK = "0" };
                CollListModules.Add(LI);
                HeaderAddtoList.DataSource = CollListModules;
                ListInfo found = CollListModules.FirstOrDefault(cc => cc.Name.ToUpper().ToString() == "Shopping List".ToUpper());
                if (found == null)
                {
                    LI = new ListInfo { Name = "Shopping List", UserListQuoteSK = "-1" };
                    CollListModules.Insert(0, LI);
                }
                ListItem l = new ListItem("Create New List", "0", true);
                HeaderAddtoList.Items.Add(l);
                HeaderAddtoList.DataTextField = "NAME";
                HeaderAddtoList.DataValueField = "UserListQuoteSK";
                HeaderAddtoList.DataBind();
            }
            catch (Exception ex)
            {
                //DataAccessException.Instance.ExceptionMessage(ex);
            }
            finally
            {
            }
        }


        /// <summary>
        /// Product result check add to list function
        /// </summary>
        /// <param name="userSk"></param>
        private void CheckAddToList(int userSk)
        {
            if (Session["newCreatedListName"] != null && Session["newCreatedListItems"] != null)
            {
                string ListName = Session["newCreatedListName"].ToString();
                string listDetail = Session["newCreatedListItems"].ToString();
                string listSK = string.Empty;
                SearchController IECPR = new SearchController();
                CollListModules = IECPR.GetProduct(Constants.FLG_LISTQUOTEHEADER, "", userSk, "List");
                object found = CollListModules.FirstOrDefault(cc => cc.Name.ToUpper().ToString() == ListName.ToUpper());
                if (found != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "listNameExist", "ShowErrorMessage('List&nbsp;name&nbsp;already&nbsp;exist')", true);
                }
                else
                {
                    IECPR.AddListQuote(Constants.FLG_CREATELIST_QUOTE, userSk.ToString(), UserDetailInfo.TradingAccountSK.ToString(), ListName.ToString(), "List", ref listSK);
                    int returValue = 0;
                    if (listDetail.Length > 0)
                    {
                        listDetail = listDetail.Substring(0, (listDetail.Length - 1)) + ".";
                        ListInfo _objListparameter = new ListInfo { Status = listDetail, UserListQuoteSK = listSK, GenericFlag = "List" };
                        IECPR.AddListQuote(Constants.FLG_ADDTOLIST, userSk.ToString(), listDetail, listSK, "List", ref listSK);
                    }
                }
                addedLabel.Style.Add("display", "block");
                addtolstdiv1.Style.Add("display", "none");
                Session["newCreatedListName"] = null;
                Session["newCreatedListItems"] = null;
            }
            else if (Session["newCreatedListItems"] != null)
            {

                string listDetail = Session["newCreatedListItems"].ToString();
                string listSK = string.Empty;
                SearchController IECPR = new SearchController();
                CollListModules = IECPR.GetProduct(Constants.FLG_LISTQUOTEHEADER, "", userSk, "List");
                object found = CollListModules.FirstOrDefault(cc => cc.Name.ToUpper().ToString() == "Shopping List".ToUpper());
                if (found != null)
                {
                    listSK = ((ListInfo)found).UserListQuoteSK.ToString();
                }
                else
                {
                    IECPR.AddListQuote(Constants.FLG_CREATELIST_QUOTE, userSk.ToString(), UserDetailInfo.TradingAccountSK.ToString(), "Shopping List", "List", ref listSK);
                }
                int returValue = 0;
                if (listDetail.Length > 0)
                {
                    listDetail = listDetail.Substring(0, (listDetail.Length - 1)) + ".";
                    ListInfo _objListparameter = new ListInfo { Status = listDetail, UserListQuoteSK = listSK, GenericFlag = "List" };
                    IECPR.AddListQuote(Constants.FLG_ADDTOLIST, userSk.ToString(), listDetail, listSK, "List", ref listSK);
                }
                addedLabel.Style.Add("display", "block");
                addtolstdiv1.Style.Add("display", "none");
                Session["newCreatedListItems"] = null;
            }

        }

        /// <summary>
        /// Product result bind quote header
        /// </summary>
        /// <param name="userSk"></param>
        private void BindQuoteHeader(int userSk)
        {
            try
            {
                SearchController IECPR = new SearchController();
                string role = IECPR.GetUserRole(userSk).ToUpper();
                if (role == "REP" || role == "CS")
                {
                    AddtoQuote.Visible = true;
                    CollQuoteModules = IECPR.GetExistingQuotes(userSk, role, UserDetailInfo.TradingAccountSK);

                    AddtoQuote.DataTextField = "NAME";
                    AddtoQuote.DataValueField = "SK";
                    if (CollQuoteModules != null && CollQuoteModules.Tables.Count > 0)
                    {
                        AddtoQuote.DataSource = CollQuoteModules;
                        AddtoQuote.DataBind();
                    }
                    AddtoQuote.Items.Add(new ListItem("Create Quote", "0|0|0"));
                }
                else
                {
                    AddtoQuoteDiv.Style.Add("display", "none");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Product result lefsubheading function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void leftSubHeading_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //HiddenField hidden = (HiddenField)e.Item.FindControl("dummyhidden");
            CheckBox checkBox = (CheckBox)e.Item.FindControl("LeftMenuChk");
            HtmlImage img = (HtmlImage)e.Item.FindControl("unchkboxs");
            //checkBox.Checked = (hidden.Value == "N" || hidden.Value.Trim() == string.Empty) ? false : true;
            if (checkBox.Checked)
            {
                img.Src = "/Portals/0/Images/check.PNG";
            }
            else
            {
                img.Src = "/Portals/0/images/pagebk.png";
            }
        }
        #endregion
        bool Flagvalue;
        //  string FacetFirstvalue;
        string FacetActual;
        string Facetacttr;
        #region Optional Interfaces

        /// <summary>
        /// Product result module action
        /// </summary>




        /// <summary>
        /// product result btnpage click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnpages_click(object sender, EventArgs e)
        //{
        //    ProductPlace.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsCMSResults.ascx"));

        //}


        #endregion
    }
}