<%@ WebHandler Language="C#" Class="SearchButton" %>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Instrumentation;
using Microsoft.ApplicationBlocks.Data;
using Cengage.eCommerce.ExceptionHandling;
using Cengage.eCommerce.Lib;
using System.IO;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Common;
using System.Web.SessionState;

public class SearchButton : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string ClassJson = Newtonsoft.Json.JsonConvert.SerializeObject(GetSearchInfo(context));
        context.Response.Write(ClassJson);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private static string[] stopWords = new string[] { };
    private static Func<string, int?> NWORDS;
    private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";

    private string GetSearchInfo(HttpContext context)
    {
        string input = string.Empty;
        NWORDS = train(words(GetDictionary()));
        if (context.Request.QueryString["searchtext"] != null)
        {
            input = context.Request.QueryString["searchtext"].ToLower().Trim().Replace("-", "");
            if (!string.IsNullOrEmpty(input))
            {
                string filename = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
                if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
                foreach (string word in stopWords)
                {
                    string regexp = @"(?i)\s?\b" + word + @"\b\s\+?";
                    input = Regex.Replace(input, regexp, " ");
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
                        match = correct(word);
                        corrected = corrected + " " + match;
                    }

                    if (input == corrected.ToLower().Trim())
                    {

                        TabController tab = new TabController();
                        TabInfo tabinfo = tab.GetTabByName("search", PortalSettings.Current.PortalId);
                        return "keywordCase" + "|" + string.Empty;
                    }
                    else
                    {

                        TabController tab = new TabController();
                        TabInfo tabinfo = tab.GetTabByName("search", PortalSettings.Current.PortalId);
                        return "correctedWordCase" + "|" + corrected.ToLower().Trim();

                    }
                }
                else
                {
                    TabController tab = new TabController();
                    TabInfo tabinfo = tab.GetTabByName("search", PortalSettings.Current.PortalId);
                    return "StopwordCase" + "|" + string.Empty;
                    //Response.Redirect(Globals.NavigateURL(tabinfo.TabID).ToLower() + "?q=" + strinput + "&dmean=Stopword" + "&division=" + ddlschooltypetop.SelectedItem.Text.ToLower(), true);

                }

            }
            else
            {

                TabController tab = new TabController();
                TabInfo tabinfo = tab.GetTabByName("search", PortalSettings.Current.PortalId);
                return string.Empty;
            }

        }
        return string.Empty;
    }

    /// <summary>
    /// Loading Dictionary
    /// </summary>
    /// <returns></returns>
    private string GetDictionary()
    {
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
            GetDicQuery = "SELECT SEARCH_WORD FROM CEN_NZ_DICTIONARY";
        else
            GetDicQuery = "SELECT SEARCH_WORD FROM CEN_AU_DICTIONARY";
        StringBuilder DictionaryWords = new StringBuilder();
        DnnLog.Trace("Connecting DB to load dictionary..");
        IDataReader DataReader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString, CommandType.Text, GetDicQuery);
        while (DataReader.Read())
        {
            DictionaryWords.Append(DataReader[0].ToString());
            DictionaryWords.Append("\r");
        }
        DnnLog.Trace("Succesfully Loaded Dictionary..");
        return DictionaryWords.ToString();
    }
    /// <summary>
    /// Matching with only alphabets
    /// </summary>
    /// <param name="text"></param>
    /// <returns>words as IEnumerable string</returns>
    private static IEnumerable<string> words(string text)
    {
        return Regex.Matches(text.ToLower(), "[a-z]+\\+")
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

}