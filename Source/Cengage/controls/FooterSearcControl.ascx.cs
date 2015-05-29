/*
 
 *  Project Name        :   Cengage Ecommerce
 *  Module Name         :   Search Component
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   22-07-2013
 *  Date Modified       :   09-08-2013
  
 */

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
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Common;

public partial class controls_FooterSearcControl : System.Web.UI.UserControl
{

    //private static Func<string, int?> NWORDS;

    //private const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";

    /// <summary>
    /// Loading Dictionary
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                DnnLog.Info("State of both Division and Did you mean..");
                //ddlschooltypefooter.SelectedValue = (Session["Division"] != null) ? Session["Division"].ToString() : ddlschooltypefooter.SelectedValue;
                //if (ddlschooltypefooter.SelectedValue == "0")
                //{
                //    TextSearchFooterPrimary.Style.Add("display", "block");
                //    TextSearchFooter.Style.Add("display", "none");
                //    TextSearchFooterSecondary.Style.Add("display", "none");
                //    if (Request.QueryString["q"] != null)
                //        TextSearchFooterPrimary.Value = Request.QueryString["q"];
                //}
                //else if (ddlschooltypefooter.SelectedValue == "1")
                //{
                //    TextSearchFooterPrimary.Style.Add("display", "none");
                //    TextSearchFooter.Style.Add("display", "none");
                //    TextSearchFooterSecondary.Style.Add("display", "block");
                //    if (Request.QueryString["q"] != null)
                //        TextSearchFooterSecondary.Value = Request.QueryString["q"];
                //}
                //else
                //{
                //    TextSearchFooterPrimary.Style.Add("display", "none");
                //    TextSearchFooter.Style.Add("display", "block");
                //    TextSearchFooterSecondary.Style.Add("display", "none");
                //    if (Request.QueryString["q"] != null)
                //        TextSearchFooter.Value = Request.QueryString["q"];
                //}
            }
            DnnLog.Info("Loading Dictionary..");
            //NWORDS = train(words(GetDictionary()));
            if (Session["UserInfo"] != null)
            {
                DnnLog.Info("Assigning ApptagetSearchcode Session..");
                if (!HttpContext.Current.Request.Url.ToString().ToUpper().Contains(ConfigurationManager.AppSettings["DivisionPrimary"].ToString())
                    && !HttpContext.Current.Request.Url.ToString().ToUpper().Contains(ConfigurationManager.AppSettings["DivisionSecondary"].ToString()))
                {
                    DnnLog.Info("Assigning dropdown values of division..");
                    Visitor Visitor = Session["UserInfo"] as Visitor;
                    if (Visitor.CountryCode == "AU" && ddlschooltypefooter.SelectedItem.Text.ToUpper() == ConfigurationManager.AppSettings["DivisionPrimary"].ToString())
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString();
                    }
                    else if (Visitor.CountryCode == "NZ" && ddlschooltypefooter.SelectedItem.Text.ToUpper() == ConfigurationManager.AppSettings["DivisionPrimary"].ToString())
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["NZPRIAppTarget"].ToString();
                    }
                    else if (Visitor.CountryCode == "AU" && ddlschooltypefooter.SelectedItem.Text.ToUpper() == ConfigurationManager.AppSettings["DivisionSecondary"].ToString())
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUSECAppTarget"].ToString();
                    }
                    else if (Visitor.CountryCode == "NZ" && ddlschooltypefooter.SelectedItem.Text.ToUpper() == ConfigurationManager.AppSettings["DivisionSecondary"].ToString())
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["NZSECAppTarget"].ToString();
                    }
                    else if (Visitor.CountryCode == "AU" && ddlschooltypefooter.SelectedItem.Text.ToUpper() == ConfigurationManager.AppSettings["DivisionAll"].ToString())
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUAllAppTarget"].ToString();
                    }
                    else if (Visitor.CountryCode == "NZ" && ddlschooltypefooter.SelectedItem.Text.ToUpper() == ConfigurationManager.AppSettings["DivisionAll"].ToString())
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["NZAllAppTarget"].ToString();
                    }
                    else if (ddlschooltypefooter.SelectedItem.Text.ToUpper() == ConfigurationManager.AppSettings["DivisionAll"].ToString())
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUAllAppTarget"].ToString();
                    }
                    else if (ddlschooltypefooter.SelectedItem.Text.ToUpper() == ConfigurationManager.AppSettings["DivisionPrimary"].ToString())
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString();
                    }
                    else
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUSECAppTarget"].ToString();
                    }

                }
                else if (HttpContext.Current.Request.Url.ToString().ToUpper().Contains(ConfigurationManager.AppSettings["DivisionPrimary"].ToString()))
                {
                    DnnLog.Info("Assigning primary url of Domain..");
                    Visitor Visitor = Session["UserInfo"] as Visitor;
                    if (Visitor.CountryCode == "AU")
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString();
                    }
                    else if (Visitor.CountryCode == "NZ")
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["NZPRIAppTarget"].ToString();
                    }
                    else
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString();
                    }
                    //ddlschooltypefooter.SelectedValue = "0";
                    //ddlschooltypefooter.SelectedItem.Text = ConfigurationManager.AppSettings["DivisionPrimary"].ToString();

                }
                else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains(ConfigurationManager.AppSettings["DivisionSecondary"].ToString()))
                {
                    DnnLog.Info("Assigning secondary url of domain..");
                    Visitor Visitor = Session["UserInfo"] as Visitor;
                    if (Visitor.CountryCode == "AU")
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUSECAppTarget"].ToString();
                    }
                    else if (Visitor.CountryCode == "NZ")
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["NZSECAppTarget"].ToString();
                    }
                    else
                    {
                        Session["AppTragetSearchCode"] = ConfigurationManager.AppSettings["AUSECAppTarget"].ToString();
                    }
                    //ddlschooltypefooter.SelectedValue = "1";
                    //ddlschooltypefooter.SelectedItem.Text = ConfigurationManager.AppSettings["DivisionSecondary"].ToString();

                }
            }

        }
        catch (Exception Exception)
        {
            DnnLog.Error(Exception);
            DataAccessException.Instance.ExceptionMessage(Exception);
        }

    }
    /// <summary>
    /// Loading Dictionary
    /// </summary>
    /// <returns></returns>
    //private string GetDictionary()
    //{
    //    Visitor visitor = (Visitor)Session["UserInfo"];
    //    string GetDicQuery = string.Empty;
    //    if (visitor.CountryCode == "NZ")
    //        GetDicQuery = "SELECT SEARCH_WORD FROM CEN_NZ_DICTIONARY";
    //    else
    //        GetDicQuery = "SELECT SEARCH_WORD FROM CEN_AU_DICTIONARY";
    //    StringBuilder DictionaryWords = new StringBuilder();
    //    DnnLog.Trace("Connecting DB to load dictionary..");
    //    IDataReader DataReader = SqlHelper.ExecuteReader(ConfigurationManager.ConnectionStrings["SiteSqlServer"].ConnectionString, CommandType.Text, GetDicQuery);
    //    while (DataReader.Read())
    //    {
    //        DictionaryWords.Append(DataReader[0].ToString());
    //        DictionaryWords.Append("\r");
    //    }
    //    DnnLog.Trace("Succesfully Loaded Dictionary..");
    //    return DictionaryWords.ToString();
    //}

 
    //private static string[] stopWords = new string[] { };

    ///// <summary>
    ///// Once click on Search button, go for word correction
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void ProductFooterSearchButton_Click(object sender, EventArgs e)
    //{
    //    string AppTarget = "";
    //    string strinput = string.Empty;
    //    string input = string.Empty;
    //    try
    //    {
    //        if (TextSearchFooterPrimary.Value != string.Empty && TextSearchFooterPrimary.Value != "Enter your search here...")
    //        {
    //            strinput = TextSearchFooterPrimary.Value;
    //        }
    //        else if (TextSearchFooterSecondary.Value != string.Empty && TextSearchFooterSecondary.Value != "Enter your search here...")
    //        {
    //            strinput = TextSearchFooterSecondary.Value;
    //        }
    //        else if (TextSearchFooter.Value != string.Empty && TextSearchFooter.Value != "Enter your search here...")
    //        {
    //            strinput = TextSearchFooter.Value;
    //        }
    //        input = strinput.ToLower().Trim();
    //        DnnLog.Error("input footer field value is :" + input);
    //        if (!string.IsNullOrEmpty(input))
    //        {
    //            string filename = Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
    //            if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
    //            foreach (string word in stopWords)
    //            {
    //                string regexp = @"(?i)\s?\b" + word + @"\b\s?";
    //                input = Regex.Replace(input, regexp, " ");
    //            }

    //            if (Regex.IsMatch(input, @"^[\d-, ]+$"))
    //            {
    //                input = input.ToLower().Trim().Replace("-", "");
    //                input = input.Replace(" ", string.Empty).Trim();
    //            }
    //            else
    //            {
    //                input = input.Replace(",", " ");
    //                Regex r = new Regex("(?:[^a-z0-9- ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
    //                input = r.Replace(input, String.Empty);
    //                RegexOptions options = RegexOptions.None;
    //                Regex regex = new Regex(@"[ ]{2,}", options);
    //                input = regex.Replace(input, @" ");
    //                //string filename = Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
    //                //if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
    //                //foreach (string word in stopWords)
    //                //{
    //                //    string regexp = @"(?i)\s?\b" + word + @"\b\s?";
    //                //    input = Regex.Replace(input, regexp, " ");
    //                //}
    //            }
    //            input = input.Trim();


                             
    //            string corrected = string.Empty;
    //            string match = string.Empty;
    //            if (!string.IsNullOrEmpty(input) && input.ToLower() != "enter your search")
    //            {
    //                string[] words = input.Split(' ');
    //                List<string> list = HttpContext.Current.Cache[HttpContext.Current.Session.SessionID + "Auto"] as List<string>;
    //                int count = 0;
    //                foreach (string word in words)
    //                {
    //                    if (!list.Contains(word))
    //                    {
    //                        if (word.Length > 1)
    //                        {
    //                            count++;
    //                            match = correct(word);
    //                            corrected = corrected + " " + match;
    //                        }
    //                    }
    //                    if (count == 0)
    //                    {
    //                        corrected = corrected + " " + word;
    //                    }
    //                    count = 0;
    //                }

    //                if (input == corrected.ToLower().Trim())
    //                {
    //                    TabController tab = new TabController();
    //                    TabInfo tabinfo = tab.GetTabByName("search", PortalSettings.Current.PortalId);
    //                    Response.Redirect(Globals.NavigateURL(tabinfo.TabID).ToLower() + "?q=" + strinput.Trim()+ "&division=" + ddlschooltypefooter.SelectedItem.Text.ToLower(), true);
    //                }
    //                else
    //                {
                        
    //                    TabController tab = new TabController();
    //                    TabInfo tabinfo = tab.GetTabByName("search", PortalSettings.Current.PortalId);
    //                    Response.Redirect(Globals.NavigateURL(tabinfo.TabID).ToLower() + "?q=" + strinput + "&dmean=" + corrected.ToLower().Trim() + "&division=" + ddlschooltypefooter.SelectedItem.Text.ToLower(), true);
    //                    //Response.Redirect("productresults.aspx?search=" + TextSearchFooter.Value + "&dmean=" + corrected.ToLower().Trim(), true);
    //                    //Cache[HttpContext.Current.Session.SessionID] = new List<CengageSearchResult>();
    //                    // Response.Redirect("/productresults.aspx?'" + corrected + "'", false);
    //                }


    //            }
    //            else
    //            {
    //                TabController tab = new TabController();
    //                TabInfo tabinfo = tab.GetTabByName("search", PortalSettings.Current.PortalId);
    //                Response.Redirect(Globals.NavigateURL(tabinfo.TabID).ToLower() + "?q=" + strinput + "&dmean=Stopword" + "&division=" + ddlschooltypefooter.SelectedItem.Text.ToLower(), true);
    //                //Response.Redirect("productresults.aspx", false);
    //            }
    //            //}
    //            //// else directly go for product searching
    //            //else
    //            //{
    //            //    DnnLog.Trace("Did u mean disabled..");
    //            //    DidYouMean.Visible = false;
    //            //    DidUmeanSpan.Visible = false;
    //            //    Didyoumeanlabel.Style.Add("visibility", "hidden");
    //            //    DnnLog.Info("Calling GetSearchresults from productsearch click..");
    //            //    Cache[HttpContext.Current.Session.SessionID + "LeftFacet"] = GetFacetSearhResults(input, AppTarget);
    //            //    Cache[HttpContext.Current.Session.SessionID + "RightResults"] = GetSearhResults(input, AppTarget);
    //            //    Response.Redirect("/productresults.aspx?search=" + TextSearchFooter.Value + "", false);
    //            //}
    //        }
    //        else
    //        {
                
    //            TabController tab = new TabController();
    //            TabInfo tabinfo = tab.GetTabByName("search", PortalSettings.Current.PortalId);
    //            Response.Redirect(Globals.NavigateURL(tabinfo.TabID).ToLower()+"?q=" + strinput.Trim()+ "&division=" + ddlschooltypefooter.SelectedItem.Text.ToLower(), true);
    //            //Response.Redirect("productresults.aspx", false);
    //        }
    //    }
    //    catch (Exception Exception)
    //    {
    //        DnnLog.Error(Exception);
    //        DataAccessException.Instance.ExceptionMessage(Exception);
    //    }


    //}


    //protected void AutoSuggestionFooterButton_Click(object sender, EventArgs e)
    //{
    //    string strinput = string.Empty;
    //    string input = string.Empty;
    //    try
    //    {
    //        if (TextSearchFooterPrimary.Value != string.Empty && TextSearchFooterPrimary.Value != "Enter your search here...")
    //        {
    //            strinput = TextSearchFooterPrimary.Value;
    //        }
    //        else if (TextSearchFooterSecondary.Value != string.Empty && TextSearchFooterSecondary.Value != "Enter your search here...")
    //        {
    //            strinput = TextSearchFooterSecondary.Value;
    //        }
    //        else if (TextSearchFooter.Value != string.Empty && TextSearchFooter.Value != "Enter your search here...")
    //        {
    //            strinput = TextSearchFooter.Value;
    //        }
    //        input = strinput.ToLower().Trim().Replace("-", "");
    //        string filename = Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
    //        if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
    //        foreach (string word in stopWords)
    //        {
    //            string regexp = @"(?i)\s?\b" + word + @"\b\s?";
    //            input = Regex.Replace(input, regexp, " ");
    //        }
    //        input = input.ToLower().Trim();
    //        if (Regex.IsMatch(input, @"^[\d, ]+$"))
    //        {
    //            input = input.Replace(" ", string.Empty).Trim();
    //        }
    //        else
    //        {
    //            input = input.Replace(",", " ");
    //            Regex r = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
    //            input = r.Replace(input, String.Empty);
    //            RegexOptions options = RegexOptions.None;
    //            Regex regex = new Regex(@"[ ]{2,}", options);
    //            input = regex.Replace(input, @" ");
    //            //string filename = Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
    //            //if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
    //            //foreach (string word in stopWords)
    //            //{
    //            //    string regexp = @"(?i)\s?\b" + word + @"\b\s?";
    //            //    input = Regex.Replace(input, regexp, " ");
    //            //}
    //        }
    //        input = input.Trim();


            
    //        string corrected = string.Empty;
    //        string match = string.Empty;


    //        DnnLog.Info("Calling GetSearchresults from productsearch click..");
    //        //Cache[HttpContext.Current.Session.SessionID] = GetSearhResults(input, AppTarget);
            
    //        //Response.Redirect("productresults.aspx?search=" + TextSearch.Value + "", false);
    //        TabController tab = new TabController();
    //        TabInfo tabinfo = tab.GetTabByName("search", PortalSettings.Current.PortalId);
    //        //Response.Redirect(Globals.NavigateURL(tabinfo.TabID).ToLower() + "?q=" + TextSearchFooter.Value.Trim(), true);
    //    Response.Redirect(Globals.NavigateURL(tabinfo.TabID).ToLower() + "?q=" + strinput.Trim() + "&division=" + ddlschooltypefooter.SelectedItem.Text.ToLower(), true);



    //    }
    //    catch (Exception Exception)
    //    {
    //        DnnLog.Error(Exception);
    //        DataAccessException.Instance.ExceptionMessage(Exception);
    //    }



    //}


  

    //protected void turnOnDidUMean_Change(object sender, EventArgs e)
    //{
    //    Session["StatusOfDidUMean"] = turnOnDidUMean.Checked;
    //}
    ///// <summary>
    ///// Matching with only alphabets
    ///// </summary>
    ///// <param name="text"></param>
    ///// <returns>words as IEnumerable string</returns>
    //private static IEnumerable<string> words(string text)
    //{
    //    return Regex.Matches(text.ToLower(), "[a-z]+")
    //                .Cast<Match>()
    //                .Select(m => m.Value);
    //}

    ///// <summary>
    ///// goruping train of words
    ///// </summary>
    ///// <param name="features"></param>
    ///// <returns>words as funtion delegate</returns>
    //private static Func<string, int?> train(IEnumerable<string> features)
    //{
    //    var dict = features.GroupBy(f => f)
    //                       .ToDictionary(g => g.Key, g => g.Count());

    //    return f => dict.ContainsKey(f) ? dict[f] : (int?)null;
    //}
    ///// <summary>
    ///// All word correction happens here
    ///// </summary>
    ///// <param name="word"></param>
    ///// <returns>words as IEnumerable string</returns>
    //private static IEnumerable<string> edits1(string word)
    //{
    //    var splits = from i in Enumerable.Range(0, word.Length)
    //                 select new { a = word.Substring(0, i), b = word.Substring(i) };
    //    var deletes = from s in splits
    //                  where s.b != "" // we know it can't be null
    //                  select s.a + s.b.Substring(1);
    //    var transposes = from s in splits
    //                     where s.b.Length > 1
    //                     select s.a + s.b[1] + s.b[0] + s.b.Substring(2);
    //    var replaces = from s in splits
    //                   from c in ALPHABET
    //                   where s.b != ""
    //                   select s.a + c + s.b.Substring(1);
    //    var inserts = from s in splits
    //                  from c in ALPHABET
    //                  select s.a + c + s.b;

    //    return deletes
    //    .Union(transposes) // union translates into a set
    //    .Union(replaces)
    //    .Union(inserts);
    //}
    ///// <summary>
    ///// after edit the word, it will send current word
    ///// </summary>
    ///// <param name="word"></param>
    ///// <returns>words as IEnumerable string</returns>
    //private static IEnumerable<string> known_edits2(string word)
    //{
    //    return (from e1 in edits1(word)
    //            from e2 in edits1(e1)
    //            where NWORDS(e2) != null
    //            select e2)
    //           .Distinct();
    //}

    ///// <summary>
    ///// returns words after cross check with all words
    ///// </summary>
    ///// <param name="words"></param>
    ///// <returns>words as IEnumerable string</returns>
    //private static IEnumerable<string> known(IEnumerable<string> words)
    //{
    //    return words.Where(w => NWORDS(w) != null);
    //}
    ///// <summary>
    ///// finally returns corrected word
    ///// </summary>
    ///// <param name="word"></param>
    ///// <returns>word as string</returns>
    //private static string correct(string word)
    //{
    //    var candidates =
    //        new[] { known(new[] {word}),
    //                known(edits1(word)),
    //                known_edits2(word),
    //                new[] {word} }
    //              .First(s => s.Any());

    //    return candidates.OrderByDescending(c => NWORDS(c) ?? 1).First();
    //}
}