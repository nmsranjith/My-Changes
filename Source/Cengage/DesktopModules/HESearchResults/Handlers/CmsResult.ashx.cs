using System;
using System.Web;
using System.Data;
using System.Web.SessionState;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using DotNetNuke.Common;
using DotNetNuke.Modules.HESearchResults.Components.Controller;
using System.Collections.Generic;
using DotNetNuke.Instrumentation;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.Linq;

namespace DotNetNuke.Modules.HESearchResults.Handlers
{
    /// <summary>
    /// Summary description for CmsResult
    /// </summary>
    /// 
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Cmsresulthandeler" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Cmssearchresulthandler
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class CmsResult : IHttpHandler, IRequiresSessionState
    {
        private string SearchQuery;
        private static string[] stopWords = new string[] { };
        private static Func<string, int?> NWORDS;
        private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
        public void ProcessRequest(HttpContext context)
        {
            string ClassJson = string.Empty;
            HttpRequest Request = HttpContext.Current.Request;
            SearchQuery = GetCorrectText(HttpContext.Current.Server.HtmlEncode(Request.Params["q"]));
            SearchController IECPR = new SearchController();
            string Division = HttpContext.Current.Server.HtmlEncode(Request.Params["division"]);
            DataSet cmsdata = new DataSet();
            HttpSessionState Session = HttpContext.Current.Session;
            Visitor UserDetailInfo = (Visitor)Session["UserInfo"];
            int Storesk = Convert.ToInt32(UserDetailInfo.StoreID);
            string[] querys = SearchQuery.Split('Ñ');
            if (querys.Length > 1) SearchQuery = querys[0];
            cmsdata = IECPR.SelectCms(Division, SearchQuery, int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"].ToString()), 1, Storesk);
            cmsdata.Tables[0].Columns.Add(new DataColumn("Exacturl"));
            for (int i = 0; i <= cmsdata.Tables[0].Rows.Count - 1; i++)
            {
                cmsdata.Tables[0].Rows[i]["Exacturl"] = FormatURL(int.Parse(cmsdata.Tables[0].Rows[i]["tabid"].ToString()), "");
            }

            ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject(cmsdata.Tables[0]);
            context.Response.Write(ClassJson);
            
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
        /// Getinput text function
        /// </summary>
        /// <param name="SearchText"></param>
        /// <returns></returns>
        public string GetInputText(string SearchText)
        {
            string input = SearchText.Trim();
            HttpServerUtility Server = HttpContext.Current.Server;
            string filename = Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
            if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
            foreach (string word in stopWords)
            {
                string regexp = @"(?i)\s?\b" + word + @"\b\s?";
                input = Regex.Replace(input, regexp, " ");
            }

            if (Regex.IsMatch(input, @"^[\d-, ]+$"))
            {
                input = input.ToLower().Trim().Replace("-", "");
                input = input.ToLower().Trim().Replace("#", String.Empty);
                input = input.Replace(" ", string.Empty).Trim();
            }
            else
            {
                input = input.ToLower().Trim().Replace("#", String.Empty);
                input = input.Replace(",", " ");
                Regex r = new Regex("(?:[^a-z0-9-+ ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                input = r.Replace(input, String.Empty);
                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex(@"[ ]{2,}", options);
                input = regex.Replace(input, @" ");
            }
            input = input.Trim();
            return input;
        }

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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}