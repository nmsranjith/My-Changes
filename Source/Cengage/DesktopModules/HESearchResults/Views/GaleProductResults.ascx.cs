using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using Cengage.eCommerce.Lib;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.HESearchResults.Components.Controller;
using DotNetNuke.Modules.HESearchResults.Components.Modal;
using Microsoft.ApplicationBlocks.Data;
using System.Web.UI.HtmlControls;
using DotNetNuke.Instrumentation;

namespace DotNetNuke.Modules.HESearchResults.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="GaleProductResults" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Provides the products result page for gale.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    
    public partial class GaleProductResults : HESearchResultsModuleBase
    {
        Dictionary<string, List<Facets>> dicAttr = new Dictionary<string, List<Facets>>();
        Dictionary<string, List<DisciplineCategorySubjects>> disCategoryAttr = new Dictionary<string, List<DisciplineCategorySubjects>>();
        Dictionary<string, List<DisciplineCategorySubjects>> disciplineAttr = new Dictionary<string, List<DisciplineCategorySubjects>>();
        DataSet CollQuoteModules;
        Visitor UserDetailInfo = null;
        private static string[] stopWords = new string[] { };
        private static Func<string, int?> NWORDS;
        private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz+#";
        private string imageUrl = string.Empty, condition = string.Empty, weightage = string.Empty, forms = "FORMSOF(INFLECTIONAL,",hf=string.Empty;
        private int correctedCount = 0;

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                imageUrl = Null.SetNullString(ConfigurationManager.AppSettings["BooksImgPath"]);
                string cases = Null.SetNullString(Request.QueryString["cms"]);
                switch (cases.ToLower())
                {
                    case "t":
                        CmsPgResPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/HECMSResults.ascx"));
                        break;
                    default:
                        break;
                }
                string filename = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
                if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);

                string q = Null.SetNullString(Request.QueryString["q"]), division = Null.SetNullString(Request.QueryString["division"]);
                int pNo = Null.SetNullInteger(Request.QueryString["p"]), cpNo = Null.SetNullInteger(Request.QueryString["cp"]);
                SearchTextHdn.Value = q;
                DivisionHdn.Value = division;
                ProductsPageNumberHdn.Value = pNo == 0 ? "1" : pNo.ToString();
                CMSPageNumberHdn.Value = cpNo == 0 ? "1" : cpNo.ToString();
                FillProductResults(q, division, pNo, cpNo);
                
                

            }
            catch (Exception exc) //Module failed to load
            {
                LogFileWrite(exc);
            }
        }

        /// <summary>
        /// Fills product results
        /// </summary>
        private void FillProductResults(string q, string division, int pNo, int cpNo)
        {
            SqlDataReader searchResult = null;
            try
            {
                DataTable facetsAttrTable = new DataTable(), showOnlyAttrTable = new DataTable();
                SearchParameters sParams = null;
                int itemsCount = 0, newItems = 0, publishedItems = 0, audienceCodeItems = 0;
                string queryKeyword = string.Empty, s = Request.QueryString["s"], dt = Request.QueryString["dt"], dc = Request.QueryString["dc"],
                           dv = Request.QueryString["dv"], ds = Request.QueryString["ds"], f = Request.QueryString["f"], df = Request.QueryString["df"],
                           sf = Request.QueryString["sf"], dCheck = Request.QueryString["dcheck"], bwf = Request.QueryString["format"];
                hf = Request.QueryString["hf"];
                // if (!stopWords.Contains(q))
                //  {
                dCheck = string.IsNullOrEmpty(dCheck) ? "n" : dCheck;

                UserDetailInfo = new Visitor();
                //Check whether logged in user info available in session
                if (Session["UserInfo"] != null)
                    UserDetailInfo = (Visitor)(Session["UserInfo"]);
                if (UserDetailInfo.CountryCode.ToUpper() == "NZ")
                    ((DotNetNuke.Framework.CDefault)this.Page).Title = "Search" + " " + q + " " + "Page" + " " + (pNo == 0 ? 1 : pNo) + " | Gale | Cengage Learning - New Zealand";
                else
                    ((DotNetNuke.Framework.CDefault)this.Page).Title = "Search" + " " + q + " " + "Page" + " " + (pNo == 0 ? 1 : pNo) + " | Gale | Cengage Learning - Australia";
           
                //check the correct word of the current search
                if (q != string.Empty)
                    queryKeyword = GetCorrectText(q);
                else
                    queryKeyword = string.Empty;
                //string[] query = queryKeyword.Split('Ñ');
                Session["corrected"] = null;
                if (correctedCount > 0)
                {
                    if (dCheck.ToLower() == "n")
                    {
                        Session["corrected"] = queryKeyword;//query[0];
                        LoadNoResult(f);
                        return;
                    }
                    else { }
                }
                DataTable AttributeStateTable = new DataTable();
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE", typeof(string));
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_SK", typeof(int));
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE", typeof(string));
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE_SK", typeof(int));
                AttributeStateTable.Columns.Add("PROD_COUNT", typeof(int));
                AttributeStateTable.Columns.Add("IS_CURRENT", typeof(char));
                AttributeStateTable.Columns.Add("IS_SELECTED", typeof(char));
                AttributeStateTable.Columns.Add("IS_PARENT", typeof(char));
                if (string.IsNullOrEmpty(f))
                    Session["HECurrentFacetTable" + HttpContext.Current.Session.SessionID] = null;
                NoOfResultsHdn.Value = ConfigurationManager.AppSettings["NO_OF_RECORDS_PER_PAGE"];

                DataTable BwGaleAreaTable = new DataTable();
                BwGaleAreaTable.Columns.Add("DISCIPLINE_CATEGORY_NAME_SK", typeof(int));
                BwGaleAreaTable.Columns.Add("DISCIPLINE_CATEGORY_NAME", typeof(string));
                BwGaleAreaTable.Columns.Add("PROD_COUNT", typeof(int));
                BwGaleAreaTable.Columns.Add("IS_SELECTED", typeof(char));

                DataTable BwGaleDisciplineTable = new DataTable();
                BwGaleDisciplineTable.Columns.Add("DISCIPLINE_CATEGORY_NAME_SK", typeof(int));
                BwGaleDisciplineTable.Columns.Add("DISCIPLINE_SK", typeof(int));
                BwGaleDisciplineTable.Columns.Add("DISCIPLINE_CATEGORY_NAME", typeof(string));
                BwGaleDisciplineTable.Columns.Add("DISCIPLINE", typeof(string));
                BwGaleDisciplineTable.Columns.Add("PROD_COUNT", typeof(int));
                BwGaleDisciplineTable.Columns.Add("IS_SELECTED", typeof(char));

                sParams = new SearchParameters()
                {
                    NumberOfResults = Null.SetNullInteger(NoOfResultsHdn.Value),
                    SearchText = queryKeyword,//query[0],
                    ExactText = q,
                    PageNumber = pNo == 0 ? 1 : pNo,
                    Division = division,
                    SortOrder = string.IsNullOrEmpty(s) ? "R" : s,
                    StoreSK = Null.SetNullInteger(UserDetailInfo.StoreID),
                    UserSk = UserDetailInfo.UserID,
                    ShowOnlyAttributesTable = GetShowOnlyAttributeTable(sf),
                    FacetAttributesTable = GetFacetAttributeTable(f),
                    DisciplineType = string.IsNullOrEmpty(dt) ? string.Empty : dt,
                    CategoryValue = string.IsNullOrEmpty(dc) ? string.Empty : dc,
                    DisciplineValue = string.IsNullOrEmpty(dv) ? string.Empty : dv,
                    SubjectValue = string.IsNullOrEmpty(ds) ? string.Empty : ds,
                    Country = UserDetailInfo.CountryCode,
                    Condition = condition,
                    Weightage = weightage,
                    FormsString = forms.TrimEnd(',') + ")",//ReturnWordsSearched(q, query[0])
                    AttributeStateTable = (f != null) ? ((Session["HECurrentFacetTable" + HttpContext.Current.Session.SessionID] != null) ? Session["HECurrentFacetTable" + HttpContext.Current.Session.SessionID] as DataTable : AttributeStateTable) : AttributeStateTable,
                    GaleAreaTable = (bwf != null) ? ((Session["GaleAreaTable" + HttpContext.Current.Session.SessionID] != null) ? Session["GaleAreaTable" + HttpContext.Current.Session.SessionID] as DataTable : BwGaleAreaTable) : BwGaleAreaTable,
                    GaleDisciplineTable = (bwf != null) ? ((Session["GaleDisciplineTable" + HttpContext.Current.Session.SessionID] != null) ? Session["GaleDisciplineTable" + HttpContext.Current.Session.SessionID] as DataTable : BwGaleDisciplineTable) : BwGaleDisciplineTable,
                    BwsFormat=(bwf != null) ?bwf:string.Empty
                };
                // }
                // Checking Search type is Advance search or not

                string st = Request.QueryString["st"];
                if (!string.IsNullOrEmpty(st))
                {
                    string all_q = Request.QueryString["allq"], n_q = Request.QueryString["nq"], fatv_q = Request.QueryString["fatvq"], fa_q = Request.QueryString["faq"], t_q = Request.QueryString["tq"],
                           a_q = Request.QueryString["aq"], et_q = Request.QueryString["etq"], k_q = Request.QueryString["kq"], sub_q = Request.QueryString["subq"],
                           fv = Request.QueryString["fv"], pv = Request.QueryString["pv"], pyv = Request.QueryString["pyv"], ov = Request.QueryString["ov"],
                           epf = Request.QueryString["epf"], flt = Request.QueryString["flt"];
                    switch (st.ToLower())
                    {
                        case "ad":
                            sParams.AndAllwordsWeighate = (!string.IsNullOrEmpty(all_q)) ? GetCorrectTextForAdvAll(all_q, "and") : string.Empty;
                            sParams.NonephraseWeightage = (!string.IsNullOrEmpty(n_q)) ? GetCorrectTextForAdvAll(n_q, "or") : string.Empty;
                            sParams.AuthorWeightage = (!string.IsNullOrEmpty(a_q)) ? GetCorrectTextForAdv(a_q, "or") : string.Empty;
                            sParams.ExactphraseWeightage = (!string.IsNullOrEmpty(et_q)) ? GetCorrectTextForAdvExact(et_q, "or") : string.Empty;
                            sParams.SubjectWeightage = (!string.IsNullOrEmpty(sub_q)) ? GetCorrectTextForAdvSubject(sub_q, "or") : string.Empty;
                            sParams.TitleWeightage = (!string.IsNullOrEmpty(t_q)) ? GetCorrectTextForAdv(t_q, "or") : string.Empty;
                            sParams.ExactTitle = (!string.IsNullOrEmpty(t_q)) ? t_q.Trim() : string.Empty;
                            sParams.ExactAuthor = (!string.IsNullOrEmpty(a_q)) ? a_q.Trim() : string.Empty;

                            fv = (!string.IsNullOrEmpty(fv)) ? fv : string.Empty;
                            pv = (!string.IsNullOrEmpty(pv)) ? pv : string.Empty;
                            pyv = (!string.IsNullOrEmpty(pyv)) ? pyv : string.Empty;
                            ov = (!string.IsNullOrEmpty(ov)) ? ov : string.Empty;
                            epf = (!string.IsNullOrEmpty(epf)) ? epf : string.Empty;
                            flt = (!string.IsNullOrEmpty(flt)) ? flt : string.Empty;
                            string[] advFacetArray = { fv, pv, pyv, ov,epf,flt };
                            sParams.AdvFacets = GetAdvFacetTable(advFacetArray);
                            searchResult = HESearchResultController.Instance.GetGaleAdvanceSearchProductResults(sParams);
                            break;
                        case "advisbn":
                            string keyisbn = HttpUtility.UrlDecode(Request.QueryString["keyisbn"]);
                            if (HttpContext.Current.Session.SessionID + "isbn" == keyisbn)
                                sParams.SearchText = Cache[keyisbn] as string;
                            else
                                sParams.SearchText = string.Empty;
                            searchResult = HESearchResultController.Instance.GetGaleAdvanceIsbnResults(sParams);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    sParams.AuthorWeightage = GetWeitageForText(sParams.SearchText, "0.7","0.8");// keep it config
                    sParams.TitleWeightage = GetWeitageForText(sParams.SearchText, "0.9","1");// keep it in config
                    searchResult = HESearchResultController.Instance.GetGaleProductResults(sParams);
                }


                if (searchResult != null && searchResult.HasRows)
                {
                    List<Products> productList = new List<Products>();
                    while (searchResult.Read())
                    {
                        productList.Add(new Products()
                        {
                            TITLE = Null.SetNullString(searchResult["TITLE"]),
                            ToolTip = WebUtility.HtmlEncode(Null.SetNullString(searchResult["TITLE"])),
                            IMAGE_FILE_NAME = string.Concat(imageUrl, "GR", Null.SetNullString(searchResult["IMAGE_FILE_NAME"])),
                            PREFERRED_NAME = Null.SetNullString(searchResult["PREFERRED_NAME"]),
                            ISBN_13 = Null.SetNullString(searchResult["ISBN_13"]),
                            PRODUCT_SK = Null.SetNullInteger(searchResult["PRODUCT_SK"]),
                            PUBLICATION_DATE = Null.SetNullString(searchResult["PUBLICATION_DATE"]),
                            PRINT_PRICE = Null.SetNullString(searchResult["PRINT_PRICE"]),
                            EBOOK = Null.SetNullString(searchResult["EBOOK"]),
                            ECHAPTER = Null.SetNullString(searchResult["ECHAPTER"]),
                            SUBPRODUCT_TYPE = Null.SetNullString(searchResult["SUBPRODUCT_TYPE"]),
                            NEW_EDITION = Null.SetNullString(searchResult["NEW_EDITION"]),
                            EDITION = Null.SetNullString(searchResult["EDITION"]),
                            PRODUCT_FORMAT = Null.SetNullString(searchResult["PRODUCT_FORMAT"]),
                            FAVOURITE_FLAG = Null.SetNullString(searchResult["FAVOURITE_FLAG"]),
                            DetailUrl = FormatURL(Null.SetNullString(searchResult["TITLE"]), Null.SetNullString(searchResult["ISBN_13"]), Null.SetNullString(searchResult["TLID"])),
                            CoverType = Null.SetNullString(searchResult["COVER_TYPE"])
                        });
                    }

                    //Retrieve the total product count of the current search
                    searchResult.NextResult();
                    while (searchResult.Read())
                        itemsCount = Null.SetNullInteger(searchResult["ProductCount"]);

                    if (itemsCount == 1 && Request.QueryString["f"] == null)
                    {
                        Response.Redirect(productList[0].DetailUrl);
                    }
                    else
                    {
                        ProductResultsRptr.DataSource = productList;
                        ProductResultsRptr.DataBind();
                    }
                    
                    //Retrieve the total new edition count of the current search
                    searchResult.NextResult();
                    while (searchResult.Read())
                        SNewHdn.Value = Null.SetNullString(searchResult["NEW_EDITION_COUNT"]);
                    //Retrieve the total products count having audience code for the current search
                    //searchResult.NextResult();
                    //while (searchResult.Read())
                    //    SAudienceHdn.Value = Null.SetNullString(searchResult["AUDIENCE_COUNT"]);
                    //Retrieve the total published products count for the current search
                    searchResult.NextResult();
                    while (searchResult.Read())
                        SPublishedHdn.Value = Null.SetNullString(searchResult["PUBLICATION_COUNT"]);
                    //Retrieve all attributes of the respective facets for the current search
                    searchResult.NextResult();

                    if (searchResult.HasRows)
                    {
                        var Attrbutetype_sk = string.Empty;
                        int prdcount = 0, count = 0;
                        List<Facets> lst = new List<Facets>();
                        while (searchResult.Read())
                        {
                            Facets facets = new Facets();
                            facets.ATTRIBUTE_NAME = (new StringBuilder(Null.SetNullString(searchResult["ATTRIBUTE_TYPE"])).Replace("&", "&amp;")).ToString();
                            facets.ATTRIBUTE_TYPE_VALUE = (new StringBuilder(Null.SetNullString(searchResult["ATTRIBUTE_TYPE_VALUE"])).Replace("&", "&amp;")).ToString();
                            facets.PROD_COUNT = Null.SetNullInteger(searchResult["ProductCount"]);
                            facets.ATTRIBUTE_TYPE_SK = Null.SetNullInteger(searchResult["ATTRIBUTE_TYPE_SK"]);
                            facets.ATTRIBUTE_TYPE_VALUE_SK = Null.SetNullInteger(searchResult["ATTRIBUTE_TYPE_VALUE_SK"]);
                            facets.IS_SELECTED = Null.SetNullString(searchResult["IS_SELECTED"]);
                            facets.ChildFacetTitle = (new StringBuilder(string.Concat(facets.ATTRIBUTE_TYPE_SK, "|", facets.ATTRIBUTE_TYPE_VALUE_SK, "|", facets.ATTRIBUTE_NAME, "|", facets.ATTRIBUTE_TYPE_VALUE, "|", facets.PROD_COUNT))).Replace("'", "").Replace("&", "&amp;").ToString();
                            string attrChk = string.Concat(facets.ATTRIBUTE_TYPE_SK, "|", facets.ATTRIBUTE_NAME);
                            if (!dicAttr.ContainsKey(attrChk))
                            {
                                lst = new List<Facets>();
                                dicAttr.Add(attrChk, lst);
                            }
                            lst.Add(facets);
                            Attrbutetype_sk = attrChk;
                            prdcount = facets.PROD_COUNT;
                        }
                    }

                    //Retrieve and bind all the respective facets for the current search
                    searchResult.NextResult();
                    if (searchResult.HasRows)
                    {
                        var Attrbutetype_sk = string.Empty;
                        int prdcount = 0, count = 0;
                        List<Facets> lst = new List<Facets>();
                        while (searchResult.Read())
                        {
                            Facets facets = new Facets();
                            facets.ATTRIBUTE_NAME = (new StringBuilder(Null.SetNullString(searchResult["ATTRIBUTE_TYPE"])).Replace("&", "&amp;")).ToString();
                            facets.ATTRIBUTE_TYPE_SK = Null.SetNullInteger(searchResult["ATTRIBUTE_TYPE_SK"]);
                            facets.PROD_COUNT = Null.SetNullInteger(searchResult["ProductCount"]);
                            facets.ParentFacetTitle = (new StringBuilder(string.Concat(facets.ATTRIBUTE_TYPE_SK, "|", facets.ATTRIBUTE_NAME))).Replace("'", "").Replace("&", "&amp;").ToString();
                            lst.Add(facets);
                        }
                        AdvAttrTypesDrpDwn0.DataSource = lst;
                        AdvAttrTypesDrpDwn0.DataBind();
                        FacetsRepeater.DataSource = lst;
                        FacetsRepeater.DataBind();
                    }
                    
                    //Set discipline category
                    searchResult.NextResult();
                    if (searchResult.HasRows)
                    {
                        string Attrbutetype_sk = string.Empty;
                        int prdcount = 0, count = 0;
                        List<DisciplineCategorySubjects> lst = new List<DisciplineCategorySubjects>();
                        while (searchResult.Read())
                        {
                            DisciplineCategorySubjects disAttr = new DisciplineCategorySubjects();
                            disAttr.Discipline_Category_Name_Sk = Null.SetNullInteger(searchResult["DISCIPLINE_CATEGORY_NAME_SK"]);
                            disAttr.Discipline_Sk = Null.SetNullInteger(searchResult["DISCIPLINE_SK"]);
                            disAttr.Discipline = Null.SetNullString(searchResult["DISCIPLINE"]);
                            disAttr.Products_Count = Null.SetNullInteger(searchResult["SUBCOUT"]);
                            var attrChk = disAttr.Discipline_Category_Name_Sk.ToString();
                            if (!disCategoryAttr.ContainsKey(attrChk))
                            {
                                lst = new List<DisciplineCategorySubjects>();
                                disCategoryAttr.Add(attrChk, lst);
                            }
                            lst.Add(disAttr);
                            Attrbutetype_sk = attrChk;
                            prdcount = disAttr.Products_Count;
                        }
                    }
                    List<DisciplineCategorySubjects> lstDiscpline = new List<DisciplineCategorySubjects>();
                    if (!string.IsNullOrEmpty(hf) && hf == "y")
                    {
                        disCategoryAttr=Session["GaleHierarchyChild" + HttpContext.Current.Session.SessionID] as Dictionary<string, List<DisciplineCategorySubjects>>;
                        lstDiscpline = Session["GaleHierarchy" + HttpContext.Current.Session.SessionID] as List<DisciplineCategorySubjects>;
                      
                        DisciplineCategoryRptr.DataSource=lstDiscpline;
                        DisciplineCategoryRptr.DataBind();
                    }
                    else
                    {
                        Session["GaleHierarchyChild" + HttpContext.Current.Session.SessionID] = disCategoryAttr;
                        searchResult.NextResult();  
                       
                        while (searchResult.Read())
                        {
                            DisciplineCategorySubjects disAttr = new DisciplineCategorySubjects();
                            disAttr.Discipline_Category_Name_Sk = Null.SetNullInteger(searchResult["DISCIPLINE_CATEGORY_NAME_SK"]);
                            disAttr.Discipline_Category_Name = Null.SetNullString(searchResult["Discipline_Category_Name"]);
                            disAttr.Products_Count = Null.SetNullInteger(searchResult["SUBCOUT"]);
                            var attrChk = disAttr.Discipline_Category_Name_Sk.ToString();                          
                          if(!lstDiscpline.Contains(disAttr))                          
                            lstDiscpline.Add(disAttr);
                        }
                        Session["GaleHierarchy" + HttpContext.Current.Session.SessionID] = lstDiscpline;                     
                        DisciplineCategoryRptr.DataSource = lstDiscpline;
                        DisciplineCategoryRptr.DataBind();
                    }

                    int totalpage = 0;
                    if (itemsCount <= 0)
                    {
                        LoadNoResult(f);
                        if (string.IsNullOrEmpty(Request.QueryString["cms"]))
                        {
                            CmsPgResPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/HECMSResults.ascx"));
                            LoadCMSHdn.Value = "Y";
                        }
                        else LoadCMSHdn.Value = "N";
                    }
                    else
                    {
                        int startCount = 0, totalcount = 0;
                        if (sParams.PageNumber != 0)
                        {
                            totalpage = Null.SetNullInteger(Math.Ceiling(Convert.ToDouble(Convert.ToDecimal(itemsCount) / sParams.NumberOfResults)));// itemsCount % sParams.NumberOfResults == 0 ? itemsCount / sParams.NumberOfResults : itemsCount / sParams.NumberOfResults + 1;
                            if (totalpage > sParams.PageNumber + 5)
                            {
                                if (sParams.PageNumber - 6 >= 0)
                                    startCount = sParams.PageNumber - 6;
                                else
                                    startCount = 0;
                            }
                            else
                            {
                                if (totalpage > 10 && totalpage - sParams.PageNumber <= 6)
                                    startCount = totalpage - 10;
                                else if (totalpage > 10)
                                    startCount = sParams.PageNumber - 10;
                                else
                                    startCount = 0;
                            }
                        }
                        else
                        {
                            startCount = 0;
                        }
                        ProdCurrentPgStartSizeLbl.Text = ((sParams.PageNumber - 1) * sParams.NumberOfResults + 1).ToString();
                        ProdCurrentPgEndSizeLbl.Text = Math.Min((sParams.PageNumber * sParams.NumberOfResults), itemsCount).ToString();
                        if (totalpage > 1)
                        {
                            SetPagingDefaults(itemsCount, sParams.NumberOfResults, startCount);
                            HEProductsPagers.Visible = true;
                        }
                        else
                            HEProductsPagers.Visible = false;
                        ProdTotalResultLbl.Text = itemsCount.ToString();
                        ProdDivisionLbl.Text = division;
                    }
                }
                else
                    LoadNoResult(f);
            }
            catch (Exception exc) { LogFileWrite(exc); }
            finally
            {
                searchResult.Dispose();
            }
        }

        /// <summary>
        /// Loads No result page
        /// </summary>
        private void LoadNoResult(string facetExists)
        {
            HEProductsPagers.Visible = false;
            ResultCountPlaceHldr.Visible = false;
            if (facetExists != null)
                NoResultPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/HENoResult.ascx"));
            else
            {
                SearchPlaceHolder.Visible = false;
                InitialNoResultpHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/HEInitialNoResult.ascx"));
                LoadCMSHdn.Value = "Y";
            }
        }

        /// <summary>
        /// Returns all the searched words
        /// </summary>
        /// <param name="qString"></param>
        /// <returns></returns>
        private string[] ReturnWordsSearched(string qString, string exactString)
        {
            try
            {
                if (qString.Contains('"'))
                {
                    string pattern = "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'";// "(""[^""]+""|\w+)\s*";
                    MatchCollection mc = Regex.Matches(qString.ToLower(), pattern);
                    string[] exactwords = exactString.Split(' ');
                    int index = exactwords.Length;
                    string[] wordsSearched = new string[mc.Count + index];
                    index = 0;
                    foreach (Match m in mc)
                    {
                        var a = m.Groups[0].Value.Trim().Replace("\"", string.Empty); ;
                        if (!stopWords.Contains(a) && !wordsSearched.Contains(a))
                            wordsSearched[index++] = a;
                        else
                            wordsSearched[index++] = string.Empty;
                    }
                    exactwords.CopyTo(wordsSearched, index);
                    return wordsSearched;
                }
                else
                {
                    return exactString.Split(' ');
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return null;
        }

        /// <summary>
        /// Returns all the selected facets as a datatable
        /// </summary>
        /// <param name="facetString"></param>
        /// <returns></returns>
        private DataTable GetFacetAttributeTable(string facetString)
        {
            DataTable AttributeStateTable = new DataTable();
            AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_SK", typeof(int));
            AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE_SK", typeof(int));
            if (!string.IsNullOrEmpty(facetString))
            {
                string[] attrTypes = facetString.Split(':'), attr = null, attrValues = null;
               
                if (attrTypes.Length > 0)
                {
                    foreach (string a in attrTypes)
                    {
                        
                        attr = a.Split('_');
                        attrValues = attr[1].Split(',');
                        foreach (string b in attrValues)
                            AttributeStateTable.Rows.Add(attr[0], b);
                    }
                }
            }
            else { }
            return AttributeStateTable;
        }

        /// <summary>
        /// Returns all the Show only search options as a datatable
        /// </summary>
        /// <param name="showOnlyString"></param>
        /// <returns></returns>
        private DataTable GetShowOnlyAttributeTable(string showOnlyString)
        {
            DataTable AttributeStateTable = new DataTable();
            AttributeStateTable.Columns.Add("SHOWONLY_FILTER_TYPE", typeof(string));
            if (!string.IsNullOrEmpty(showOnlyString))
            {
                string[] showOnlyAttrValues = showOnlyString.Split(',');
                if (showOnlyAttrValues.Length > 0)
                {
                    foreach (string a in showOnlyAttrValues)
                    {
                        switch (a)
                        {
                            case "1":
                                AttributeStateTable.Rows.Add("NEW");
                                break;
                            case "2":
                                AttributeStateTable.Rows.Add("AUDIENCE");
                                break;
                            default:
                                AttributeStateTable.Rows.Add("PUBLISHER");
                                break;
                        }

                    }
                }
            }
            else { }
            return AttributeStateTable;
        }

        /// <summary>
        /// Returns all the selected advanced search facets as a datatable
        /// </summary>
        /// <param name="facetString"></param>
        /// <returns></returns>
        private DataTable GetAdvFacetTable(string[] facetStringArray)
        {
            DataTable AttributeStateTable = new DataTable();
            AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_SK", typeof(int));
            AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE", typeof(string));
            foreach (string facetString in facetStringArray)
            {
                if (!string.IsNullOrEmpty(facetString))
                {
                    string[] attrTypes = facetString.Split(':'), attr = null, attrValues = null;
                    if (attrTypes.Length > 0)
                    {
                        string[] attrVal = attrTypes[1].Split(',');
                        foreach (string a in attrVal)
                            AttributeStateTable.Rows.Add(attrTypes[0], a.Replace("\"", string.Empty).Trim());
                    }
                    else { }
                }
                else { }
            }
            return AttributeStateTable;
        }

        /// <summary>
        /// Function to format book thumbnail path
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        [WebMethod]
        protected string FormatImageURL()
        {
            return imageUrl;
        }

        /// <summary>
        /// Product detail page's url format  function
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        [WebMethod]
        protected string FormatURL(string title, string ISBN,string TLId)
        {
            if(!string.IsNullOrEmpty(ISBN.Trim()))
                return CengageSearchResult.getHEProductURL(ISBN, title,Request.QueryString["division"]);
            else
                return CengageSearchResult.getGaleProductURL(TLId, title, Request.QueryString["division"]);
        }

        /// <summary>
        /// Function to construct html elements for products
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        [WebMethod]
        protected string BindHtml(string type, string value, string parentValue)
        {
            string html = string.Empty;
            switch (type)
            {               
                case "NEW_EDITION":
                    return "<div class=\"version\"><span class=\"NewFlag\">" + value + "</span></div>";
                    break;
                case "EDITION":
                    return "<div class=\"edition\"> | <strong>Edition </strong><small>" + value + "</small></div>";
                    break;
                case "PREFERRED_NAME":
                    return "<div class=\"author\" title='" + HttpUtility.HtmlEncode(value) + "'><strong>Author/s:</strong><span class=\"AvailabilityCheck\">" + value + "</span></div>";
                    break;
                case "PRINT_PRICE":
                    return "<div class=\"span3 padding-5\"><div class=\"txt\">" + parentValue + "</div><div class=\"amt\">$<span class=\"AvailabilityCheck1\">" + value + "</span></div></div>";
                    break; 
                case "CONTACT_US":
                    return "<div id='price"+parentValue+"' class=\"gale-price\"><span class=\"consortia\">Consortia pricing</span> <a href=\"/contactus/div/gale\" class=\"consortia-link\">contact us</a> </div>";
                    break;
                case"ISBN_13":
                    if(parentValue.Trim()==string.Empty)
                        return "<div class=\"emptyisbn\"><strong>ISBN </strong><small>" + value + "</small></div>";
                    else
                        return "<div class=\"emptyisbn\"> | <strong>ISBN </strong><small>"+value+"</small></div>";
                    break;
                case "PUBLICATION_DATE":
                    return "<strong>Published </strong><small>"+value+"</small>";
                    break;
                default:
                    break;
            }
            return imageUrl;
        }

        /// <summary>
        /// product result set paging default function
        /// </summary>
        /// <param name="TotalNoOfRecords"></param>
        /// <param name="NoOfRecordsPerPage"></param>
        /// <param name="FirstValue"></param>
        private void SetPagingDefaults(int TotalNoOfRecords, int NoOfRecordsPerPage, int FirstValue)
        {
            try
            {
                HEProductsPagers.CreatePagingControl(TotalNoOfRecords, FirstValue);
                //HEProductsPager.PageButtonStyle(FirstValue);
                if (TotalNoOfRecords > NoOfRecordsPerPage)
                {
                    HEProductsPagers.DisplayPropertyForPage("block");
                }
                else
                {
                    HEProductsPagers.DisplayPropertyForPage("none");
                }
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Facets Repeater Item Data bound event to bind all the respective facet attributes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FacetsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            try
            {
                HiddenField AttrTypeSkHdn = (HiddenField)e.Item.FindControl("AttrTypeSkHdn");
                Repeater FacetValuesRepeater = (Repeater)e.Item.FindControl("FacetValuesRepeater");
                HtmlGenericControl facetParentDiv = (HtmlGenericControl)e.Item.FindControl("SearchFacetParent");

                if (dicAttr[AttrTypeSkHdn.Value].Count == 1)
                {
                    Facets a = dicAttr[AttrTypeSkHdn.Value][0] as Facets;
                    //LogValues(a.ATTRIBUTE_TYPE_VALUE + " --> IS_SELECTED=" + a.IS_SELECTED);
                    if (Session["HECurrentFacetTable" + HttpContext.Current.Session.SessionID] != null)
                    {
                        if (a.IS_SELECTED != "Y")
                            facetParentDiv.Visible = false;
                        else
                        {
                            FacetValuesRepeater.DataSource = dicAttr[AttrTypeSkHdn.Value];
                            FacetValuesRepeater.DataBind();
                        }
                    }
                    else
                    {
                        string f = Null.SetNullString(Request.QueryString["f"]); int fcnt = 0;                       
                        if (f != string.Empty)
                        {
                            foreach (string fac in f.Split(':'))
                            {
                                string[] fc = fac.Split('_');
                                foreach (string fcs in fc[1].Split(','))
                                {
                                    if (string.Concat(a.ATTRIBUTE_TYPE_SK, '_', a.ATTRIBUTE_TYPE_VALUE_SK) == fc[0] + '_' + fcs)
                                    {
                                        fcnt++;
                                    }
                                }
                            }
                        }
                        if(fcnt==0)
                            facetParentDiv.Visible = false;
                        else
                        {
                            FacetValuesRepeater.DataSource = dicAttr[AttrTypeSkHdn.Value];
                            FacetValuesRepeater.DataBind();
                        }
                    }
                }
                else
                {
                    FacetValuesRepeater.DataSource = dicAttr[AttrTypeSkHdn.Value];
                    FacetValuesRepeater.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Discipline Category Repeater Item Databound event is to bind all the respective disciplines.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DisciplineCategoryRptr_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                HiddenField DisciplineCategorySkHdn = (HiddenField)e.Item.FindControl("DisciplineCategorySkHdn");
                Repeater DisciplineRptr = (Repeater)e.Item.FindControl("DisciplineRptr");
                DisciplineRptr.DataSource = disCategoryAttr[DisciplineCategorySkHdn.Value];
                DisciplineRptr.DataBind();
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
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
        /// product result getcorrect text function
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string GetCorrectText(string inputText)
        {
            try
            {
                string input = inputText.ToLower().Trim().Replace("-", " ").Trim(':');
                //
                if (!string.IsNullOrEmpty(input))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\d, ]+$"))
                        input = input.Replace(" ", string.Empty).Trim();
                    else { }
                    input = input.Replace(",", " ");

                    string pattern = "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'";// "(""[^""]+""|\w+)\s*";

                    List<string> inputArray = new List<string>();

                    if (input.Contains('\''))
                    {
                        string[] inputTxts = input.Split(' ');//CopyTo(inputArray, inputArray.Length);
                        int exCnt = 0;
                        string strArray = string.Empty;
                        foreach (string str in inputTxts)
                        {
                            if (!strArray.Contains(str + "¤"))
                            {
                                string.Concat(strArray, str, "¤");
                                inputArray.Add(str);
                            }
                        }
                    }
                    if (input.Split(' ').Length != inputArray.Count)
                    {
                        MatchCollection mc = Regex.Matches(input.ToLower(), pattern);
                        foreach (Match str in mc)
                        {
                            inputArray.Add(str.Groups[0].Value.Trim());
                        }
                    }


                    return GetCorrectedInputString(inputArray, input);//,string type)
                }
                else
                    return string.Empty;
            }
            catch (Exception ex) { LogFileWrite(ex); return string.Empty; }
        }

        /// <summary>
        /// Returns the corrected text for search with forms,weightage
        /// </summary>
        /// <param name="mc"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetCorrectedInputString(List<string> mc, string input)//,string type)
        {
            try
            {
                string dic = GetDictionary(), inputWord = string.Empty, regex = "[^a-z0-9 ]";
                NWORDS = train(words(dic));

                int fc = 0, mCount = 0, stopCount = 0, fmcnt = 0;
                Regex r = new Regex("[^a-z0-9+#'*\" ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                string c = string.Empty, d = string.Empty, formsString = string.Empty, formsSubString = string.Empty;
                foreach (string m in mc)
                {
                    string a = m.Trim(),// m.Groups[0].Value.Trim(), 
                        b = string.Empty, corrected = string.Empty, match = string.Empty;//.Replace("\"", string.Empty);
                    b = a = a.Contains("\"") ? a : r.Replace(a, string.Empty);
                    c = a.Trim().ToLower();
                    mCount++;

                    if (!string.IsNullOrEmpty(c))
                    {
                        if (c != "and" && c != "or" && c != "not")
                        {
                            if (!stopWords.Contains(c))
                            {
                                stopCount = 0; fmcnt = 0;
                                bool isExists = dic.ToUpper().Contains("\r" + b.ToUpper() + "\r");// Regex.IsMatch(dic.ToUpper(), @"\b" + br.ToUpper() + @"\b");
                                switch (!isExists)
                                {
                                    case true:
                                        b = b.Replace("\"", string.Empty).Trim();
                                        foreach (string ex in b.Split(' '))
                                        {
                                            if (ex.Length > 1 && !Regex.IsMatch(ex, regex, RegexOptions.IgnoreCase) && !stopWords.Contains(ex))
                                            {
                                                match = correct(ex);
                                                corrected = string.Concat(corrected, " ", match);
                                            }
                                            else
                                                corrected = string.Concat(corrected, " ", ex);
                                        }
                                        break;
                                    default:
                                        corrected = corrected + " " + a;
                                        break;
                                }
                                corrected = corrected.ToLower().Trim();
                                if (b.ToLower() != corrected)
                                    correctedCount++;
                                else { }
                                if (corrected.Contains("*"))
                                    formsSubString = string.Concat(corrected.Replace("*", ""));
                                else
                                    formsSubString = corrected;
                                if (d != "not")
                                {
                                    switch (fc)
                                    {
                                        case 1:
                                            if (d == "and")
                                            {
                                                formsString = string.Concat(formsString, " and ", formsSubString);
                                                forms = string.Concat(forms, "\"", formsString, "\",");
                                                weightage = string.Concat(weightage, "\"", formsString, "\" WEIGHT (1),").ToUpper();
                                            }
                                            else
                                            {
                                                forms = !forms.Contains("\"" + formsString + "\"") ? string.Concat(forms, " \"", formsString, "\",\"", formsSubString, "\",") : string.Concat(forms, " \"", formsSubString, "\",");
                                                weightage = !weightage.Contains(string.Concat("\"", formsString, "\" WEIGHT (1)").ToUpper()) ? string.Concat(weightage, "\"", formsString, "\" WEIGHT (1),\"", corrected, "\" WEIGHT (1),").ToUpper() : string.Concat(weightage, "\"", corrected, "\" WEIGHT (1),").ToUpper();
                                            }
                                            fc = 0;
                                            break;
                                        default:
                                            if (mc.Count == 1 || mCount == mc.Count)
                                            {
                                                forms = string.Concat(forms, "\"", formsSubString, "\",");
                                                weightage = string.Concat(weightage, "\"", corrected, "\" WEIGHT (1),").ToUpper();
                                            }
                                            else
                                            {
                                                if (input.ToLower().Trim().EndsWith(" and") || input.ToLower().Trim().EndsWith(" or") || input.ToLower().EndsWith(" not"))
                                                    weightage = weightage == string.Empty ? string.Concat("\"", corrected, "\" WEIGHT (1),").ToUpper() : string.Concat(weightage, "\"", corrected, "\" WEIGHT (1),").ToUpper();
                                                formsString = formsSubString;
                                                //forms = string.Concat(forms, "\"", formsSubString, "\",");                                            
                                            }
                                            fc++;
                                            break;
                                    }
                                }
                                else
                                {

                                    //else
                                    //{
                                    //    forms = string.Concat(forms, " \"5TPESIJNARIVAP\",");
                                    //    weightage = string.Concat(weightage, "\"5TPESIJNARIVAP\" WEIGHT (1),").ToUpper();
                                    //}
                                }

                                // if user enters any product information , will perform search based on it
                                switch (d)
                                {
                                    case "and":
                                        condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " ", d, " \"", corrected, "\"").ToUpper();
                                        corrected = condition == string.Empty ? corrected : (inputWord == string.Empty ? corrected : string.Concat(d, " ", corrected));
                                        break;
                                    case "or":
                                        condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " ", d, " \"", corrected, "\"").ToUpper();
                                        corrected = condition == string.Empty ? corrected : (inputWord == string.Empty ? corrected : string.Concat(d, " ", corrected));
                                        break;
                                    case "not":
                                        //condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " AND NOT \"", corrected, "\"").ToUpper();

                                        if (mCount == mc.Count)
                                            condition = condition == string.Empty ? string.Concat("\"5TPESIJNARIVAP\" AND NOT \"", corrected, "\"").ToUpper() : string.Concat(condition, " AND NOT \"", corrected, "\"").ToUpper();
                                        else
                                            condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " AND NOT \"", corrected, "\"").ToUpper();
                                        corrected = condition == string.Empty ? corrected : string.Concat(d, " ", corrected);
                                        break;
                                    default:
                                        condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " OR \"", corrected, "\"").ToUpper();
                                        break;
                                }
                                d = string.Empty;

                                if (a.Contains("\""))
                                    corrected = string.Concat('"', corrected, '"');
                                else { }

                                inputWord = string.Concat(inputWord, ' ', corrected);
                            }
                            else
                            {
                                if (fmcnt == 0)
                                    forms = string.Concat(forms, " \"", formsString, "\",");
                                fmcnt++;
                                stopCount++;
                            }
                        }
                        else
                        {
                            d = c;
                            stopCount++;
                            if (mCount == mc.Count && !forms.Contains(formsSubString))
                                forms = string.Concat(forms, "\"", formsSubString, "\",");
                        }
                    }
                }

                if (stopCount == mc.Count)
                {
                    condition = "5TPESIJNARIVAP";
                    forms = forms = string.Concat(forms, "\"5TPESIJNARIVAP\",");
                    weightage = string.Concat(weightage, "\"5TPESIJNARIVAP\" WEIGHT (1),").ToUpper();
                    return "5TPESIJNARIVAP";
                }
                else
                {
                    if (input.ToLower().Contains(" not ") || input.ToLower().StartsWith("not "))
                    {
                        forms = string.Concat("FORMSOF(INFLECTIONAL,\"5TPESIJNARIVAP\",");
                        weightage = string.Concat("\"5TPESIJNARIVAP\" WEIGHT (1),").ToUpper();
                    }
                    // if (fc == stopCount )//&& !weightage.Contains(formsString+"\" WEIGHT (1),"))
                    //     weightage = string.Concat(weightage, "\"", formsString, "\" WEIGHT (1),").ToUpper();
                    return inputWord.Trim();
                }
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
                return string.Empty;
            }
        }
        /*
        /// <summary>
        /// product result getcorrect text function
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string GetCorrectText(string inputText)
        {
            string dic = GetDictionary();
            NWORDS = train(words(dic));
            string input = inputText.ToLower().Trim().Replace("-", " ").Trim(':');
            //
            if (!string.IsNullOrEmpty(input))
            {
                string inputWord = string.Empty;

                if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\d, ]+$"))
                    input = input.Replace(" ", string.Empty).Trim();
                else { }
                input = input.Replace(",", " ");
                Regex r = new Regex("[^a-z0-9+#*\" ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                string regex = "[^a-z0-9 ]";
                string pattern = "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'", c = string.Empty, d = string.Empty, formsString = string.Empty, formsSubString = string.Empty;// "(""[^""]+""|\w+)\s*";
                MatchCollection mc = Regex.Matches(input.ToLower(), pattern);
                int fc = 0, mCount = 0, stopCount = 0;
                foreach (Match m in mc)
                {
                    string a = m.Groups[0].Value.Trim(), b = string.Empty, corrected = string.Empty, match = string.Empty;//.Replace("\"", string.Empty);
                    b = a = a.Contains("\"") ? a : r.Replace(a, string.Empty);
                    c = a.Trim().ToLower();
                    mCount++;
                    if (!string.IsNullOrEmpty(c))
                    {
                        if (c != "and" && c != "or" && c != "not")
                        {
                            if (!stopWords.Contains(c))
                            {
                                stopCount = 0;
                                bool isExists = dic.ToUpper().Contains("\r" + b.ToUpper() + "\r");// Regex.IsMatch(dic.ToUpper(), @"\b" + br.ToUpper() + @"\b");
                                switch (!isExists)
                                {
                                    case true:
                                        b = b.Replace("\"", string.Empty);
                                        foreach (string ex in b.Split(' '))
                                        {
                                            if (ex.Length > 1 && !Regex.IsMatch(ex, regex, RegexOptions.IgnoreCase) && !stopWords.Contains(ex))
                                            {
                                                match = correct(ex);
                                                corrected = string.Concat(corrected, " ", match);
                                            }
                                            else
                                                corrected = string.Concat(corrected, " ", ex);
                                        }
                                        break;
                                    default:
                                        corrected = corrected + " " + a;
                                        break;
                                }
                                corrected = corrected.ToLower().Trim();
                                if (b.ToLower() != corrected)
                                    correctedCount++;
                                else { }
                                if (corrected.Contains("*"))
                                    formsSubString = string.Concat(corrected.Replace("*", ""));
                                else
                                    formsSubString = corrected;
                                if (d != "not")
                                {
                                    switch (fc)
                                    {
                                        case 1:
                                            if (d == "and")
                                            {
                                                formsString = string.Concat(formsString, " and ", formsSubString);
                                                forms = string.Concat(forms, "\"", formsString, "\",");
                                                weightage = string.Concat(weightage, "\"", formsString, "\" WEIGHT (1),").ToUpper();
                                            }
                                            else
                                            {
                                                forms = !forms.Contains("\"" + formsString + "\"") ? string.Concat(forms, " \"", formsString, "\",\"", formsSubString, "\",") : string.Concat(forms, " \"", formsSubString, "\",");
                                                weightage = !weightage.Contains(string.Concat("\"", formsString, "\" WEIGHT (1)").ToUpper()) ? string.Concat(weightage, "\"", formsString, "\" WEIGHT (1),\"", corrected, "\" WEIGHT (1),").ToUpper() : string.Concat(weightage, "\"", corrected, "\" WEIGHT (1),").ToUpper();
                                            }
                                            fc = 0;
                                            break;
                                        default:
                                            if (mc.Count == 1 || mCount == mc.Count)
                                            {
                                                forms = string.Concat(forms, "\"", formsSubString, "\",");
                                                weightage = string.Concat(weightage, "\"", corrected, "\" WEIGHT (1),").ToUpper();
                                            }
                                            else
                                            {
                                                if (input.ToLower().Trim().EndsWith(" and") || input.ToLower().Trim().EndsWith(" or") || input.ToLower().EndsWith(" not"))
                                                    weightage = weightage == string.Empty ? string.Concat("\"", corrected, "\" WEIGHT (1),").ToUpper() : string.Concat(weightage, "\"", corrected, "\" WEIGHT (1),").ToUpper();
                                                formsString = formsSubString;
                                                //forms = string.Concat(forms, "\"", formsSubString, "\",");                                            
                                            }
                                            fc++;
                                            break;
                                    }
                                }
                                else
                                {

                                    //else
                                    //{
                                    //    forms = string.Concat(forms, " \"5TPESIJNARIVAP\",");
                                    //    weightage = string.Concat(weightage, "\"5TPESIJNARIVAP\" WEIGHT (1),").ToUpper();
                                    //}
                                }

                                // if user enters any product information , will perform search based on it
                                switch (d)
                                {
                                    case "and":
                                        condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " ", d, " \"", corrected, "\"").ToUpper();
                                        corrected = condition == string.Empty ? corrected : string.Concat(d, " ", corrected);
                                        break;
                                    case "or":
                                        condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " ", d, " \"", corrected, "\"").ToUpper();
                                        corrected = condition == string.Empty ? corrected : string.Concat(d, " ", corrected);
                                        break;
                                    case "not":
                                        //condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " AND NOT \"", corrected, "\"").ToUpper();

                                        if (mCount == mc.Count)
                                            condition = condition == string.Empty ? string.Concat("\"5TPESIJNARIVAP\" AND NOT \"", corrected, "\"").ToUpper() : string.Concat(condition, " AND NOT \"", corrected, "\"").ToUpper();
                                        else
                                            condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " AND NOT \"", corrected, "\"").ToUpper();
                                        corrected = condition == string.Empty ? corrected : string.Concat(d, " ", corrected);
                                        break;
                                    default:
                                        condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " OR \"", corrected, "\"").ToUpper();
                                        break;
                                }
                                d = string.Empty;

                                if (a.Contains("\""))
                                    corrected = string.Concat('"', corrected, '"');
                                else { }

                                inputWord = string.Concat(inputWord, ' ', corrected);
                            }
                            else
                            {
                                forms = string.Concat(forms, " \"", formsString, "\",");
                                stopCount++;
                            }
                        }
                        else
                        {
                            d = c;
                            stopCount++;
                            if (mCount == mc.Count)
                                forms = string.Concat(forms, "\"", formsSubString, "\",");
                        }
                    }
                }

                if (stopCount == mc.Count)
                {
                    condition = "5TPESIJNARIVAP";
                    forms = forms = string.Concat(forms, "\"5TPESIJNARIVAP\",");
                    weightage = string.Concat(weightage, "\"5TPESIJNARIVAP\" WEIGHT (1),").ToUpper();
                    return "5TPESIJNARIVAP";
                }
                else
                {
                    if (input.ToLower().Contains(" not ") || input.ToLower().StartsWith("not "))
                    {
                        forms = string.Concat("FORMSOF(INFLECTIONAL,\"5TPESIJNARIVAP\",");
                        weightage = string.Concat("\"5TPESIJNARIVAP\" WEIGHT (1),").ToUpper();
                    }
                    //if (fc == stopCount)
                   //     weightage = string.Concat(weightage, "\"", formsString, "\" WEIGHT (1),").ToUpper();
                    return inputWord.Trim();
                }

            }
            else
                return string.Empty;
        }

        */

        /// <summary>
        /// Product result getdictionary function
        /// </summary>
        /// <returns></returns>
        private string GetDictionary()
        {
            StringBuilder DictionaryWords = new StringBuilder();
            Visitor visitor = null;
            if (Session["UserInfo"] != null)
            {
                visitor = (Visitor)Session["UserInfo"];
            }
            else
            {
                visitor = new Visitor();
                visitor.CountryCode = "AU";
            }
            string GetDicQuery = string.Empty;


            GetDicQuery = "SELECT SEARCH_WORD FROM CEN_GALE_DICTIONARY";

            if (Cache["DICT_GALE_WORDS"] == null)
            {
                //DnnLog.Trace("Connecting DB to load dictionary..");
                IDataReader DataReader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString, CommandType.Text, GetDicQuery);
                while (DataReader.Read())
                {
                    DictionaryWords.Append(DataReader[0].ToString());
                    DictionaryWords.Append("\r");
                }
                if (DataReader != null)
                    DataReader = null;
                Cache.Insert("DICT_GALE_WORDS", DictionaryWords, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
                //DnnLog.Trace("Succesfully Loaded Dictionary..");
            }
            else
            {
                DictionaryWords = ((StringBuilder)Cache["DICT_GALE_WORDS"]);
            }

            return DictionaryWords.ToString();
        }

        /// <summary>
        /// Product result get weightage text for search - Title and Author 
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        private string GetWeitageForText(string inputText, string weight, string wholewordweight)
        {
            string input = inputText.ToLower().Trim().Replace("-", " ").Trim(':');
            if (!string.IsNullOrEmpty(input))
            {
                input = input.ToLower().Trim().Replace("-", " ");

                if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\d, ]+$"))
                    input = input.Replace(" ", string.Empty).Trim();
                else { }
                input = input.Replace(",", " ");

                List<string> inputArray = new List<string>();
                string pattern = "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'", d = string.Empty;
                if (input.Contains('\''))
                {
                    string[] inputTxts = input.Split(' ');//CopyTo(inputArray, inputArray.Length);
                    int exCnt = 0;
                    string strArray = string.Empty;
                    foreach (string str in inputTxts)
                    {
                        if (!strArray.Contains(str + "¤"))
                        {
                            string.Concat(strArray, str, "¤");
                            inputArray.Add(str);
                        }
                    }
                }
                if (input.Split(' ').Length != inputArray.Count)
                {
                    MatchCollection mc = Regex.Matches(input.ToLower(), pattern);
                    foreach (Match str in mc)
                    {
                        inputArray.Add(str.Groups[0].Value.Trim());
                    }
                }

                Regex r = new Regex("[^a-z0-9+#'*\" ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                string regex = "[^a-z0-9 ]";
                string inputWordWg = "ISABOUT ( \"" + input.Replace("\"", "") + "\"" + " WEIGHT (" + wholewordweight + "),";
                foreach (string m in inputArray)
                {
                    string a = m.Trim().Replace("\"", string.Empty), b = string.Empty, corrected = string.Empty, match = string.Empty;// m.Groups[0].Value.Trim()
                    a = r.Replace(a, string.Empty);
                    corrected = a.Trim().ToLower();
                    if (!stopWords.Contains(corrected))
                    {
                        if (corrected != "and" && corrected != "or" && corrected != "not")
                        {
                            // if user enters any product information , will perform search based on it
                            switch (d)
                            {
                                case "and":
                                    corrected = string.Concat(d, " ", corrected);
                                    break;
                                case "or":
                                    corrected = string.Concat(d, " ", corrected);
                                    break;
                                case "not":
                                    corrected = string.Concat(d, " ", corrected);
                                    break;
                                default:
                                    break;
                            }
                            d = string.Empty;
                        }
                        else
                            d = corrected;
                    }
                    else { }

                    inputWordWg = string.Concat(inputWordWg, "\"" + corrected + "\"" + " WEIGHT (" + weight + "),");
                }

                return inputWordWg.Trim().TrimEnd(',') + ")";
                //return inputWord.Trim();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Product result get correct text for advance search - Title and Author function
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string GetCorrectTextForAdv(string inputText, string type)
        {
            string input = inputText.ToLower().Trim().Replace("-", " ").Trim(':');
            if (!string.IsNullOrEmpty(input))
            {
                string inputWord = string.Empty;
                //string filename = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
                //if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
                //foreach (string word in stopWords)
                //{
                //    string regexp = @"(?i)\b" + word + @"\b";
                //    if (input.Length > 2 && !(input.Contains("a*") || !input.Contains("a+")))
                //        input = Regex.Replace(input, regexp, " ");
                //    else if (input == "a")
                //    {
                //        input = Regex.Replace(input, regexp, " ");
                //    }
                //}
                input = input.ToLower().Trim().Replace("-", " ");

                if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\d, ]+$"))
                    input = input.Replace(" ", string.Empty).Trim();
                else { }
                input = input.Replace(",", " ");

                List<string> inputArray = new List<string>();
                string pattern = "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'", d = string.Empty;
                if (input.Contains('\''))
                {
                    string[] inputTxts = input.Split(' ');//CopyTo(inputArray, inputArray.Length);
                    int exCnt = 0;
                    string strArray = string.Empty;
                    foreach (string str in inputTxts)
                    {
                        if (!strArray.Contains(str + "¤"))
                        {
                            string.Concat(strArray, str, "¤");
                            inputArray.Add(str);
                        }
                    }
                }
                if (input.Split(' ').Length != inputArray.Count)
                {
                    MatchCollection mc = Regex.Matches(input.ToLower(), pattern);
                    foreach (Match str in mc)
                    {
                        inputArray.Add(str.Groups[0].Value.Trim());
                    }
                }

                Regex r = new Regex("[^a-z0-9+#'*\" ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                string regex = "[^a-z0-9 ]";
                string inputWordWg = "ISABOUT ( \"" + input.Replace("\"", "") + "\"" + " WEIGHT (1),";   
                foreach (string m in inputArray)
                {
                    string a = m.Trim().Replace("\"", string.Empty), b = string.Empty, corrected = string.Empty, match = string.Empty;// m.Groups[0].Value.Trim()
                    a = r.Replace(a, string.Empty);
                    corrected = a.Trim().ToLower();
                    if (!stopWords.Contains(corrected))
                    {
                        if (corrected != "and" && corrected != "or" && corrected != "not")
                        {
                            // if user enters any product information , will perform search based on it
                            switch (d)
                            {
                                case "and":
                                    inputWord = string.Concat(inputWord, " ", d, " \"", corrected, "\"").ToUpper();
                                    corrected = string.Concat(d, " ", corrected);
                                    break;
                                case "or":
                                    inputWord = string.Concat(inputWord, " ", d, " \"", corrected, "\"").ToUpper();
                                    corrected = string.Concat(d, " ", corrected);
                                    break;
                                case "not":
                                    inputWord = string.Concat(inputWord, " AND NOT \"", corrected, "\"").ToUpper();
                                    corrected = string.Concat(d, " ", corrected);
                                    break;
                                default:
                                    if (type == "and")
                                        inputWord = inputWord == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(inputWord, " AND \"", corrected, "\"").ToUpper();
                                    else
                                        inputWord = inputWord == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(inputWord, " OR \"", corrected, "\"").ToUpper();
                                    break;
                            }
                            d = string.Empty;
                        }
                        else
                            d = corrected;
                    }
                    else { }
                    inputWordWg = string.Concat(inputWordWg, "\"" + corrected + "\"" + " WEIGHT (0.8),");
                }
                return inputWordWg.Trim().TrimEnd(',') + ")";
                //return inputWord.Trim();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Product result get correct text for advance search - Exact Words function
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string GetCorrectTextForAdvExact(string inputText, string type)
        {
            string input = inputText.ToLower().Trim().Replace("-", " ");
            if (!string.IsNullOrEmpty(input))
            {
                string inputWord = string.Empty;
                //string filename = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
                //if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
                //foreach (string word in stopWords)
                //{
                //    string regexp = @"(?i)\b" + word + @"\b";
                //    if (input.Length > 2 && !(input.Contains("a*") || !input.Contains("a+")))
                //        input = Regex.Replace(input, regexp, " ");
                //    else if (input == "a")
                //    {
                //        input = Regex.Replace(input, regexp, " ");
                //    }
                //}
                input = input.ToLower().Trim().Replace("-", " ").Trim(':');

                if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\d, ]+$"))
                    input = input.Replace(" ", string.Empty).Trim();
                else { }
                string[] inputArray = input.Split(',');

                //Regex r = new Regex("[^a-z0-9+#*\" ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                //string regex = "[^a-z0-9 ]";  
                foreach (string text in inputArray)
                {
                    if (!stopWords.Contains(text))
                    {
                        string a = text.Trim().Replace("\"", string.Empty), corrected = string.Empty, match = string.Empty;
                        //a = r.Replace(a, string.Empty);
                        corrected = a.Trim().ToLower();
                        if (type == "and")
                            inputWord = inputWord == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(inputWord, " AND \"", corrected, "\"").ToUpper();
                        else
                            inputWord = inputWord == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(inputWord, " OR \"", corrected, "\"").ToUpper();
                    }
                    else { }

                }
                return inputWord.Trim();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Product result get correct text for advance search - Subject function
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string GetCorrectTextForAdvSubject(string inputText, string type)
        {
            string input = inputText.ToLower().Trim().Replace("-", " ").Trim(':');
            if (!string.IsNullOrEmpty(input))
            {
                string inputWord = string.Empty;
                //string filename = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
                //if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
                //foreach (string word in stopWords)
                //{
                //    string regexp = @"(?i)\b" + word + @"\b";
                //    if (input.Length > 2 && !(input.Contains("a*") || !input.Contains("a+")))
                //        input = Regex.Replace(input, regexp, " ");
                //    else if (input == "a")
                //    {
                //        input = Regex.Replace(input, regexp, " ");
                //    }
                //}
                input = input.ToLower().Trim().Replace("-", " ");

                if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\d, ]+$"))
                    input = input.Replace(" ", string.Empty).Trim();
                else { }
                string[] inputArray = input.ToLower().Split(',');
                string pattern = "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'", d = string.Empty, newText = string.Empty;
                Regex r = new Regex("[^a-z0-9+#'*\" ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);

                foreach (string text in inputArray)
                {
                    inputWord = inputWord == string.Empty ? string.Concat("\"", text, "\"").ToUpper() : string.Concat(inputWord, " OR \"", text, "\"").ToUpper();
                    newText = r.Replace(text, " ");
                    List<string> inputArr = new List<string>();
                    // string pattern = "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'", d = string.Empty;
                    if (newText.Contains('\''))
                    {
                        string[] inputTxts = newText.Split(' ');//CopyTo(inputArray, inputArray.Length);
                        int exCnt = 0;
                        string strArray = string.Empty;
                        foreach (string str in inputTxts)
                        {
                            if (!strArray.Contains(str + "¤"))
                            {
                                string.Concat(strArray, str, "¤");
                                inputArr.Add(str);
                            }
                        }
                    }
                    if (newText.Split(' ').Length != inputArr.Count)
                    {
                        MatchCollection mc = Regex.Matches(newText, pattern);
                        foreach (Match str in mc)
                        {
                            inputArr.Add(str.Groups[0].Value.Trim());
                        }
                    }

                    // Regex r = new Regex("[^a-z0-9+#'*\" ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                    string regex = "[^a-z0-9 ]";

                    foreach (string m in inputArr)
                    {
                        string a = m.Trim().Replace("\"", string.Empty), corrected = string.Empty;//Groups[0].Value.
                        corrected = a.Trim().ToLower();
                        if (!stopWords.Contains(corrected))
                        {
                            if (corrected != "and" && corrected != "or" && corrected != "not")
                            {
                                // if user enters any product information , will perform search based on it
                                switch (d)
                                {
                                    case "and":
                                        inputWord = string.Concat(inputWord, " ", d, " \"", corrected, "\"").ToUpper();
                                        corrected = string.Concat(d, " ", corrected);
                                        break;
                                    case "or":
                                        inputWord = string.Concat(inputWord, " ", d, " \"", corrected, "\"").ToUpper();
                                        corrected = string.Concat(d, " ", corrected);
                                        break;
                                    case "not":
                                        inputWord = string.Concat(inputWord, " AND NOT \"", corrected, "\"").ToUpper();
                                        corrected = string.Concat(d, " ", corrected);
                                        break;
                                    default:
                                        inputWord = inputWord == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(inputWord, " OR \"", corrected, "\"").ToUpper();
                                        break;
                                }
                                d = string.Empty;
                            }
                            else
                                d = corrected;
                        }
                        else { }
                    }
                }
                return inputWord.Trim();
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Product result get correct text for advance search - All these words and None of these words function
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string GetCorrectTextForAdvAll(string inputText, string type)
        {
            string input = inputText.ToLower().Trim().Replace("-", " ");
            if (!string.IsNullOrEmpty(input))
            {
                string inputWord = string.Empty;
                //string filename = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
                //if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
                //foreach (string word in stopWords)
                //{
                //    string regexp = @"(?i)\b" + word + @"\b";
                //    if (input.Length > 2 && !(input.Contains("a*") || !input.Contains("a+")))
                //        input = Regex.Replace(input, regexp, " ");
                //    else if (input == "a")
                //    {
                //        input = Regex.Replace(input, regexp, " ");
                //    }
                //}
                input = input.ToLower().Trim().Replace("-", " ");

                if (System.Text.RegularExpressions.Regex.IsMatch(input, @"^[\d, ]+$"))
                    input = input.Replace(" ", string.Empty).Trim();
                else { }
                input = input.Replace(",", " ");
                string pattern = "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'", d = string.Empty;
                string[] inputArray = input.Split(',');
                foreach (string text in inputArray)
                {
                    List<string> inputArr = new List<string>();
                    // string pattern = "[^\\s\"']+|\"([^\"]*)\"|'([^']*)'", d = string.Empty;
                    if (text.Contains('\''))
                    {
                        string[] inputTxts = text.Split(' ');//CopyTo(inputArray, inputArray.Length);
                        int exCnt = 0;
                        string strArray = string.Empty;
                        foreach (string str in inputTxts)
                        {
                            if (!strArray.Contains(str + "¤"))
                            {
                                string.Concat(strArray, str, "¤");
                                inputArr.Add(str);
                            }
                        }
                    }
                    if (text.Split(' ').Length != inputArr.Count)
                    {
                        MatchCollection mc = Regex.Matches(text.ToLower(), pattern);
                        foreach (Match str in mc)
                        {
                            inputArr.Add(str.Groups[0].Value.Trim());
                        }
                    }

                    Regex r = new Regex("[^a-z0-9+#'*\" ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                    string regex = "[^a-z0-9 ]";

                    foreach (string m in inputArr)
                    {
                        string a = m.Trim().Replace("\"", string.Empty), b = string.Empty, corrected = string.Empty, match = string.Empty;// m.Groups[0].Value.Trim()
                        corrected = a.Trim().ToLower();
                        if (!stopWords.Contains(corrected))
                        {
                            if (corrected != "and" && corrected != "or" && corrected != "not")
                            {
                                // if user enters any product information , will perform search based on it
                                switch (d)
                                {
                                    case "and":
                                        inputWord = string.Concat(inputWord, " ", d, " \"", corrected, "\"").ToUpper();
                                        corrected = string.Concat(d, " ", corrected);
                                        break;
                                    case "or":
                                        inputWord = string.Concat(inputWord, " ", d, " \"", corrected, "\"").ToUpper();
                                        corrected = string.Concat(d, " ", corrected);
                                        break;
                                    case "not":
                                        inputWord = string.Concat(inputWord, " AND NOT \"", corrected, "\"").ToUpper();
                                        corrected = string.Concat(d, " ", corrected);
                                        break;
                                    default:
                                        if (type == "and")
                                            inputWord = inputWord == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(inputWord, " AND \"", corrected, "\"").ToUpper();
                                        else
                                            inputWord = inputWord == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(inputWord, " OR \"", corrected, "\"").ToUpper();
                                        break;
                                }
                                d = string.Empty;
                            }
                            else
                                d = corrected;
                        }
                        else { }
                    }
                }
                return inputWord.Trim();
            }
            else
                return string.Empty;
        }        
    }
}