using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using System.Web.SessionState;
using DotNetNuke.Modules.eCollection_Sessions;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Script.Serialization;

namespace DotNetNuke.Modules.eCollection_Books
{
    /// <summary>
    /// Summary description for BooksHandler
    /// </summary>
    public class BooksHandler : eCollection_BooksModuleBase, IHttpHandler, IRequiresSessionState
    {
        private static string[] stopWords = new string[] { };
        private string condition = string.Empty, weightage = string.Empty;
        private int correctedCount = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //LogFileWrite(new Exception(context.Request.QueryString["values"] + " and " + context.Request.QueryString["type"]));
                BooksController booksController = BooksController.Instance;
                string type = string.Empty;
                string Login_Name = Null.SetNullString(Context.Session["UserName"]);
                if (context.Request.QueryString["BooksStatus"] != null)
                {
                    type = Null.SetNullString(context.Request.QueryString["BooksStatus"]).Trim();
                }
                switch (type)
                {
                    case "booksAutoComplete":
                        List<string> bookList = booksController.GetBooksLookUp(int.Parse(Context.Session["Subscription"].ToString()), Login_Name, Context.Session["BooksCategories"].ToString());
                        string booksNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(bookList);
                        context.Response.Write(booksNamejson);
                        break;
                    case "bookscount":
                        //LogFileWrite(new Exception(context.Request.QueryString["values"] + " and " + context.Request.QueryString["type"]));
                        int[] BooksCount = booksController.GetSelectedBooksCount(context.Request.QueryString["values"], context.Request.QueryString["type"], int.Parse(Context.Session["Subscription"].ToString()));
                        if (BooksCount.Length > 0)
                        {
                            string booksCount = Newtonsoft.Json.JsonConvert.SerializeObject(BooksCount[0] + "," + BooksCount[1]);
                            context.Response.Write(booksCount);
                        }
                        
                        break;
                    case "contents":
                       var bpBooks=booksController.GetBookPackeBooks(Null.SetNullInteger(context.Request.QueryString["packsk"]),"level");
                       List<Book> bookPackBooks = new List<Book>();
                       while (bpBooks.Read())
                       {
                           bookPackBooks.Add(new Book() { Title = Null.SetNullString(bpBooks["TITLE"]), ReadingLevel = Null.SetNullString(bpBooks["READING_LEVEL"]), ReadingAge = Null.SetNullString(bpBooks["READING_AGE"]), TEXTTYPE = Null.SetNullString(bpBooks["TEXT_TYPE"]), IMAGE_FILE_NAME = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], Null.SetNullString(bpBooks["IMAGE_FILE_NAME"])) });
                       }
                       string json = Newtonsoft.Json.JsonConvert.SerializeObject(bookPackBooks);
                       context.Response.Write(json);  
                        break;
                    case "custom":
                       bpBooks = booksController.GetCustomPackeBooks(Null.SetNullInteger(Context.Session["Subscription"]), "level");
                       bookPackBooks = new List<Book>();
                       while (bpBooks.Read())
                       {
                           bookPackBooks.Add(new Book() { PRODUCT_SK = Null.SetNullInteger(bpBooks["PRODUCT_SK"]), Title = Null.SetNullString(bpBooks["TITLE"]), ReadingLevel = Null.SetNullString(bpBooks["READING_LEVEL"]), ReadingAge = Null.SetNullString(bpBooks["READING_AGE"]), TEXTTYPE = Null.SetNullString(bpBooks["TEXT_TYPE"]), IMAGE_FILE_NAME = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], Null.SetNullString(bpBooks["IMAGE_FILE_NAME"])), Selected = Null.SetNullString(bpBooks["SELECTED"]) });
                       }
                       json = Newtonsoft.Json.JsonConvert.SerializeObject(bookPackBooks);
                       context.Response.Write(json);  
                        break;
                    case "contentsearch":
                       string searchtext =context.Request.QueryString["q"],option=context.Request.QueryString["opt"];
                        bool cnCheck=false;
                       if (!string.IsNullOrEmpty(searchtext))
                       {
                           searchtext = searchtext.ToLower();
                           if (searchtext.Contains(" and ") || searchtext.Contains(" or ") || searchtext.Contains(" not ") || searchtext.StartsWith("and ") || searchtext.StartsWith("or ") || searchtext.StartsWith("not "))
                               cnCheck = true;                           
                           searchtext = GetCorrectText(searchtext, cnCheck);
                           bpBooks = booksController.SearchBookPackeBooks(Null.SetNullInteger(context.Request.QueryString["packsk"]), searchtext, condition, weightage,(!string.IsNullOrEmpty(option)?option:"level"));
                       }
                       else
                           bpBooks = booksController.GetBookPackeBooks(Null.SetNullInteger(context.Request.QueryString["packsk"]), (!string.IsNullOrEmpty(option) ? option : "level"));
                       bookPackBooks = new List<Book>();
                       while (bpBooks.Read())
                       {
                           bookPackBooks.Add(new Book() { Title = Null.SetNullString(bpBooks["TITLE"]), ReadingLevel = Null.SetNullString(bpBooks["READING_LEVEL"]), ReadingAge = Null.SetNullString(bpBooks["READING_AGE"]), TEXTTYPE = Null.SetNullString(bpBooks["TEXT_TYPE"]), IMAGE_FILE_NAME = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], Null.SetNullString(bpBooks["IMAGE_FILE_NAME"])) });
                       }
                       json = Newtonsoft.Json.JsonConvert.SerializeObject(bookPackBooks);
                       context.Response.Write(json);
                       break;
                    case "customsearch":
                       searchtext = context.Request.QueryString["q"];
                       option = context.Request.QueryString["opt"];
                       cnCheck = false;
                       if (!string.IsNullOrEmpty(searchtext))
                       {
                           searchtext = searchtext.ToLower();
                           if (searchtext.Contains(" and ") || searchtext.Contains(" or ") || searchtext.Contains(" not ") || searchtext.StartsWith("and ") || searchtext.StartsWith("or ") || searchtext.StartsWith("not "))
                               cnCheck = true;
                           searchtext = GetCorrectText(searchtext, cnCheck);
                           bpBooks = booksController.SearchCustomPackeBooks(Null.SetNullInteger(Context.Session["Subscription"]), searchtext, condition, weightage, (!string.IsNullOrEmpty(option) ? option : "level"));
                       }
                       else
                           bpBooks = booksController.GetCustomPackeBooks(Null.SetNullInteger(Context.Session["Subscription"]), (!string.IsNullOrEmpty(option) ? option : "level"));
                       bookPackBooks = new List<Book>();
                       while (bpBooks.Read())
                       {
                           bookPackBooks.Add(new Book() { PRODUCT_SK = Null.SetNullInteger(bpBooks["PRODUCT_SK"]), Title = Null.SetNullString(bpBooks["TITLE"]), ReadingLevel = Null.SetNullString(bpBooks["READING_LEVEL"]), ReadingAge = Null.SetNullString(bpBooks["READING_AGE"]), TEXTTYPE = Null.SetNullString(bpBooks["TEXT_TYPE"]), IMAGE_FILE_NAME = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], Null.SetNullString(bpBooks["IMAGE_FILE_NAME"])), Selected = Null.SetNullString(bpBooks["SELECTED"]) });
                       }
                       json = Newtonsoft.Json.JsonConvert.SerializeObject(bookPackBooks);
                       context.Response.Write(json);
                       break;
                    case "savecustom":
                        context.Response.ContentType = "text/plain";
                        BookPack bookPack = Deserialize<BookPack>(context);
                        bookPack.UserName = Login_Name;
                        bookPack.SubscriptionId = Null.SetNullInteger(Context.Session["Subscription"]);
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(booksController.SaveCustomBookPack(bookPack));
                        context.Response.Write(json);
                        break;
                    case "setbookpack":
                        bookPack = new BookPack();
                        bookPack.UserName = Login_Name;
                        bookPack.SubscriptionId = Null.SetNullInteger(Context.Session["Subscription"]);
                        bookPack.BookPackSk = Null.SetNullInteger(context.Request.QueryString["packsk"]);
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(booksController.SetBookPack(bookPack));
                        context.Response.Write(json);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// This function will take httpcontext object and will read the input stream
        /// It will use the built in JavascriptSerializer framework to deserialize object based given T object type value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public T Deserialize<T>(HttpContext context)
        {
            //read the json string
            string jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
            //cast to specified objectType
            var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);

            //return the object
            return obj;
        }

        /// <summary>
        /// product result getcorrect text function
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private string GetCorrectText(string inputText, bool cnCheck)
        {
            try
            {
                string input = inputText.ToLower().Trim().Replace("-", " ").Trim(':');
                //
                if (!string.IsNullOrEmpty(input))
                {                    
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


                    return GetCorrectedInputString(inputArray, input, cnCheck);//,string type)
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
        private string GetCorrectedInputString(List<string> mc, string input, bool cnCheck)//,string type)
        {
            try
            {
                string filename = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings.Get("STOPFILEpath"));
                if (File.Exists(filename)) stopWords = File.ReadAllLines(filename);
                int fc = 0, mCount = 0, stopCount = 0, fmcnt = 0;
                Regex r = new Regex("[^a-z0-9+#.'*\" ]", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                string c = string.Empty, d = string.Empty,  formsSubString = string.Empty, inputWord = string.Empty;
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
                                
                                        corrected = corrected + " " + a;

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
                                                weightage = string.Concat(weightage,  formsSubString, " WEIGHT (1),").ToUpper();
                                            }
                                            else
                                            {
                                                weightage = !weightage.Contains(string.Concat(formsSubString, " WEIGHT (1)").ToUpper()) ? string.Concat(weightage,  formsSubString, " WEIGHT (1),").ToUpper() : string.Concat(weightage, corrected, " WEIGHT (1),").ToUpper();
                                            }
                                            fc = 0;
                                            break;
                                        default:
                                            if (mc.Count == 1 || mCount == mc.Count)
                                            {                                                
                                                weightage = string.Concat(weightage,corrected, " WEIGHT (1),").ToUpper();
                                            }
                                            else
                                            {
                                                if (input.ToLower().Trim().EndsWith(" and") || input.ToLower().Trim().EndsWith(" or") || input.ToLower().EndsWith(" not"))
                                                    weightage = weightage == string.Empty ? string.Concat( corrected, " WEIGHT (1),").ToUpper() : string.Concat(weightage, corrected, " WEIGHT (1),").ToUpper();
                                                weightage = string.Concat(weightage,  formsSubString, " WEIGHT (1),").ToUpper();
                                                //forms = string.Concat(forms, "\"", formsSubString, "\",");                                            
                                            }
                                            fc++;
                                            break;
                                    }
                                }
                                else
                                {

                                   
                                }
                                 //LogValues(string.Concat("cnCheck-->", cnCheck));
                                // if user enters any product information , will perform search based on it
                                if (cnCheck)
                                {
                                   // LogValues(string.Concat("mCount-->", mCount, " \r\n mc.Count-->", mc.Count));
                                    switch (d)
                                    {
                                        case "and":
                                            condition = condition == string.Empty ? corrected.ToUpper() : string.Concat(condition, " ", d," ",  corrected).ToUpper();
                                            corrected = condition == string.Empty ? corrected : (inputWord == string.Empty ? corrected : string.Concat(d, " ", corrected));
                                            break;
                                        case "or":
                                            condition = condition == string.Empty ? corrected.ToUpper() : string.Concat(condition, " ", d," ",  corrected).ToUpper();
                                            corrected = condition == string.Empty ? corrected : (inputWord == string.Empty ? corrected : string.Concat(d, " ", corrected));
                                            break;
                                        case "not":
                                            //condition = condition == string.Empty ? string.Concat("\"", corrected, "\"").ToUpper() : string.Concat(condition, " AND NOT \"", corrected, "\"").ToUpper();
                                           // LogValues(string.Concat("mCount-->" , mCount , " \r\n mc.Count-->",mc.Count));
                                            if (mCount == mc.Count)
                                                condition = condition == string.Empty ? string.Concat("5TPESIJNARIVAP AND NOT ", corrected).ToUpper() : string.Concat(condition, " AND NOT ", corrected).ToUpper();
                                            else
                                                condition = condition == string.Empty ? corrected.ToUpper() : string.Concat(condition, " AND NOT ", corrected).ToUpper();
                                            corrected = condition == string.Empty ? corrected : string.Concat(d, " ", corrected);
                                            break;
                                        default:
                                            condition = condition == string.Empty ? corrected.ToUpper() : string.Concat(condition, " OR ", corrected).ToUpper();
                                            break;
                                    }
                                }
                                d = string.Empty;

                                if (a.Contains("\""))
                                    corrected = string.Concat('"', corrected, '"');
                                else { }

                                inputWord = string.Concat(inputWord, ' ', corrected);
                            }
                            else
                            {                                
                                fmcnt++;
                                stopCount++;
                            }
                        }
                        else
                        {
                            d = c;
                            stopCount++;                           
                        }
                    }
                }

                if (stopCount == mc.Count)
                {
                    condition = "5TPESIJNARIVAP";                   
                    weightage = string.Concat(weightage, "\"5TPESIJNARIVAP\" WEIGHT (1),").ToUpper();
                    return "5TPESIJNARIVAP";
                }
                else
                {                    
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}