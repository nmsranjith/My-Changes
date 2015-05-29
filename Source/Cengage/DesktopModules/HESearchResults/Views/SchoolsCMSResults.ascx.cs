using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using DotNetNuke.Common;
using DotNetNuke.Instrumentation;
using DotNetNuke.Modules.HESearchResults.Components.Controller;
using DotNetNuke.Services.Exceptions;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using System.Linq;

namespace DotNetNuke.Modules.HESearchResults.Views
{
    public partial class SchoolsCMSResults : System.Web.UI.UserControl
    {
        #region Event Handlers
        //List<CmsParam> CmsResult = new List<CmsParam>();
        private string SearchQuery;
        private string qstring;
        private string Division;
        private static string[] stopWords = new string[] { };
        private static Func<string, int?> NWORDS;
        private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
        SearchController Cmsresult = new SearchController();
        Visitor UserDetailInfo = null;

        /// <summary>
        /// Cms page result page init function
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }


        /// <summary>
        /// Cms page result initialize function
        /// </summary>
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
            try
            {
                DnnLog.Error("CMS call begin");
                UserDetailInfo = (Visitor)Session["UserInfo"];
                int Storesk = Convert.ToInt32(UserDetailInfo.StoreID);
                string productCount = string.Empty;

                int CmsPagenumber = 1;
                if (Request.Params["q"] != null)
                {
                    qstring = HttpContext.Current.Server.HtmlEncode(Request.Params["q"]);
                    SearchQuery = GetCorrectText(HttpContext.Current.Server.HtmlEncode(Request.Params["q"]));
                    Division = HttpContext.Current.Server.HtmlEncode(Request.Params["division"]);

                    if (qstring != string.Empty && SearchQuery == string.Empty)
                    {
                        Session["pagecount"] = "0";
                        return;
                    }
                }
                
                if (!string.IsNullOrEmpty(Request.Params["k_q"]))
                {
                    SearchQuery = Request.Params["k_q"];
                    Division = "both";
                }
                 if (Request.Params["k_q"] == "" && Request.Params["t_q"] == "" && Request.Params["a_q"] == "" && Request.Params["Sub_q"] == "" && Request.Params["fa_q"] == "" && Request.Params["fatv_q"] == "" && Request.Params["all_q"] == "" && Request.Params["et_q"] == "") {
                     SearchQuery = "";
                     Division = "both";
                }
                if (Request.Params["k_q"] == "" || Request.Params["all_q"] == "" || Request.Params["et_q"] == "") {
                    SearchQuery = "";
                    Division = "both";
                }
                if (Request.QueryString["cp"] != null)
                {
                    CmsPagenumber = int.Parse(HttpContext.Current.Server.HtmlEncode(Request.Params["cp"]));
                }
                else
                {
                    CmsPagenumber = 1;
                }
                DataSet cmsdata = new DataSet();
                cmsdata = Cmsresult.SelectCms(Division, SearchQuery, int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"].ToString()), CmsPagenumber, Storesk);
                productCount = (cmsdata.Tables[1].Rows.Count > 0) ? cmsdata.Tables[1].Rows[0]["totalrows"].ToString() : "0";
                Pagenum.Value = productCount;
                Session["pagecount"] = productCount;
                CmsPageResultRepeaterControl.DataSource = cmsdata.Tables[0];
                CmsPageResultRepeaterControl.DataBind();
                hdnProdCountcms.Value = productCount;
                hdnItemCountcms.Value = ConfigurationManager.AppSettings["ITEMCOUNT"].ToString();

                int startCount = 0, totalcount = 0;
                if (Request.QueryString["cp"] != null)
                {
                    int totalpage = (int.Parse(productCount)) % int.Parse(hdnItemCountcms.Value) == 0 ? (int.Parse(productCount)) / int.Parse(hdnItemCountcms.Value) : (int.Parse(productCount)) / int.Parse(hdnItemCountcms.Value) + 1;
                    if (totalpage > int.Parse(Request.QueryString["cp"]) + 4)
                    {
                        if (int.Parse(Request.QueryString["cp"]) - 5 >= 0)
                            startCount = int.Parse(Request.QueryString["cp"]) - 5;
                        else
                            startCount = 0;
                    }
                    else
                    {
                        if (totalpage >= 10)
                            startCount = int.Parse(Request.QueryString["cp"]) - 9;
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

                DnnLog.Error("CMS call end");
                //SetPagingDefaults(int.Parse(productCount), int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"].ToString()), (Request.QueryString["cp"] != null) ? (int.Parse(Request.QueryString["cp"]) > 10) ? int.Parse(Request.QueryString["cp"]) - 9 : 0 : 0);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }


        /// <summary>
        /// Cms page result set paging default function
        /// </summary>
        /// <param name="TotalNoOfRecords"></param>
        /// <param name="NoOfRecordsPerPage"></param>
        /// <param name="FirstValue"></param>
        private void SetPagingDefaults(int TotalNoOfRecords, int NoOfRecordsPerPage, int FirstValue)
        {
            Cmspage.CreatePagingControl(TotalNoOfRecords, FirstValue);
            //Cmspage.PageButtonStyle(FirstValue);
            if (TotalNoOfRecords > NoOfRecordsPerPage)
            {
                Cmspage.DisplayPropertyForPage("block");
            }
            else
            {
                Cmspage.DisplayPropertyForPage("none");
            }
        }

        #endregion

        /// <summary>
        /// Cms page result format url function
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        protected string FormatURL(int TabID, string Link)
        {
            string strURL;
            if (String.IsNullOrEmpty(Link))
            {
                strURL = Globals.NavigateURL(TabID);
            }
            else
            {
                strURL = Globals.NavigateURL(TabID, "", Link);
            }
            return strURL;
        }


        //public static string[] stopWords = new string[] { };
        /// <summary>
        /// Cms page result get input text validation function
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        //public string GetInputText(string SearchText)
        //{
        //    string input = SearchText.Trim();
        //    string filename = Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
        //    if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
        //    foreach (string word in stopWords)
        //    {
        //        string regexp = @"(?i)\s?\b" + word + @"\b\s?";
        //        input = Regex.Replace(input, regexp, " ");
        //    }

        //    if (Regex.IsMatch(input, @"^[\d-, ]+$"))
        //    {
        //        input = input.ToLower().Trim().Replace("-", "");
        //        input = input.ToLower().Trim().Replace("#", String.Empty);
        //        input = input.Replace(" ", string.Empty).Trim();
        //    }
        //    else
        //    {
        //        input = input.ToLower().Trim().Replace("#", String.Empty);
        //        input = input.Replace(",", " ");
        //        Regex r = new Regex("(?:[^a-z0-9-+ ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        //        input = r.Replace(input, String.Empty);
        //        RegexOptions options = RegexOptions.None;
        //        Regex regex = new Regex(@"[ ]{2,}", options);
        //        input = regex.Replace(input, @" ");
        //    }
        //    input = input.Trim();
        //    return input;
        //}



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

                if (HttpContext.Current.Cache["DICT_AU_WORDS"] == null)
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

                    HttpContext.Current.Cache.Insert("DICT_AU_WORDS", DictionaryWords, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
                    DnnLog.Trace("Succesfully Loaded Dictionary..");
                }
                else
                {
                    DictionaryWords = ((StringBuilder)HttpContext.Current.Cache["DICT_AU_WORDS"]);
                }

            }
            else
            {
                GetDicQuery = "SELECT SEARCH_WORD FROM CEN_AU_DICTIONARY";

                if (HttpContext.Current.Cache["DICT_NZ_WORDS"] == null)
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
                    HttpContext.Current.Cache.Insert("DICT_NZ_WORDS", DictionaryWords, null, DateTime.Now.AddHours(1), TimeSpan.Zero);
                    DnnLog.Trace("Succesfully Loaded Dictionary..");
                }
                else
                {
                    DictionaryWords = ((StringBuilder)HttpContext.Current.Cache["DICT_NZ_WORDS"]);
                }
            }

            return DictionaryWords.ToString();
        }

    }
}