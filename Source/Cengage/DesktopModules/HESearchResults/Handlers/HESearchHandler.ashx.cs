using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using Cengage.eCommerce.Lib;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.HESearchResults.Components.Controller;
using DotNetNuke.Modules.HESearchResults.Components.Modal;
using System.Web.UI.WebControls;
using DotNetNuke.Instrumentation;
using System.Linq;
using System.Net;

namespace DotNetNuke.Modules.HESearchResults.Handlers
{
    /// <summary>
    /// Summary description for HESearchHandler
    /// </summary>
    public class HESearchHandler :  IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string cases = Null.SetNullString(context.Request.QueryString["section"]);
                Visitor visitor = new Visitor();
                if (context.Session["UserInfo"] != null)
                    visitor = (Visitor)(context.Session["UserInfo"]);
                switch (cases)
                {
                    case "favorite":
                        string json;
                        SearchParameters sParams = new SearchParameters();
                        int result = 0;                       
                        sParams.ActionType = "favorite";                        
                        sParams.ProductSk = context.Request.QueryString["prodId"];
                        sParams.Division = HttpContext.Current.Request.QueryString["division"];
                        if (context.Session["IsAuthenticated"] != null)
                        {
                            sParams.UserSk = visitor.UserID;
                            result = HESearchResultController.Instance.SaveSearchAndFavorites(sParams);
                        }
                        else
                        {
                            result = -1;
                            context.Session["SaveSearchFavorite"] = sParams;
                        }
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        context.Response.Write(json);
                        break;
                    case "removefavorite":
                        sParams = new SearchParameters()
                        {
                            ActionType = "favorite",
                            ProductSk = context.Request.QueryString["prodId"]
                        };
                        if (context.Session["IsAuthenticated"] != null)
                        {
                            sParams.UserSk = visitor.UserID;
                            result = HESearchResultController.Instance.UpdateSaveSearchAndFavorites(sParams);
                        }
                        else
                            result = -1;                        
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        context.Response.Write(json);
                        break;
                    case "checksavesearch":
                        result = 0;                       
                        if (context.Session["IsAuthenticated"] == null)
                            result = -1;
                        else { }
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                        context.Response.Write(json);
                        break;
                    case "savesearch":  
                        sParams=context.Session["SaveSearchFavorite"] as SearchParameters;
                        sParams.UserSk = visitor.UserID;
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(HESearchResultController.Instance.SaveSearchAndFavorites(sParams));
                        context.Session["SaveSearchFavorite"] = null;
                        context.Response.Write(json);
                        break;
                    case "setsearchvalue":
                        context.Response.ContentType = "text/plain";
                        sParams = Deserialize<SearchParameters>(context);
                        sParams.ActionType = "SAVESEARCH";
                        context.Session["SaveSearchFavorite"] = sParams;
                        LogHandlerExceptions(new Exception(sParams.ActionType + " " + sParams.SearchName));
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(sParams.SearchName);
                        context.Response.Write(json);
                        break;
                    case "getsearchname":
                        sParams = context.Session["SaveSearchFavorite"] as SearchParameters;
                        LogHandlerExceptions(new Exception(sParams.ActionType + " " + sParams.SearchName));
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(sParams.SearchName);
                        context.Response.Write(json);
                        break;
                    case "cms":
                        context.Session["cmscount" + HttpContext.Current.Session.SessionID] = null;                       
                            sParams = new SearchParameters()
                            {
                                SearchText = context.Request.QueryString["q"],
                                Division = context.Request.QueryString["division"],
                                NumberOfResults = Null.SetNullInteger(ConfigurationManager.AppSettings["NO_OF_RECORDS_PER_PAGE"]),
                                PageNumber = string.IsNullOrEmpty(context.Request.QueryString["cp"]) ? 1 : int.Parse(context.Request.QueryString["cp"]),
                                StoreSK = visitor != null ? Null.SetNullInteger(visitor.StoreID) : 1,
                                Country = visitor != null ? visitor.CountryCode : "AU"
                            };
                            var reader = HESearchResultController.Instance.GetSiteResults(sParams);
                            List<SearchParameters> cmsResults = new List<SearchParameters>();
                            while (reader.Read())
                            {
                                cmsResults.Add(new SearchParameters()
                                {
                                    TabUrl = Globals.NavigateURL(Null.SetNullInteger(reader["TabId"])),
                                    TabName = Null.SetNullString(reader["TabName"]),
                                    TabDescription = Null.SetNullString(reader["Description"])
                                });
                            }
                            reader.NextResult();
                            while (reader.Read())
                                context.Session["cmscount" + HttpContext.Current.Session.SessionID] = Null.SetNullString(reader["totalrows"]);
                          
                            DnnLog.Info("1) Ranjith-SEARCH-CMS Count=" + context.Session["cmscount" + HttpContext.Current.Session.SessionID].ToString());
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(cmsResults);
                            context.Response.Write(json);                       
                        break;
                    case "cmscount":
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(context.Session["cmscount" + HttpContext.Current.Session.SessionID].ToString());
                        DnnLog.Info("2) Ranjith-SEARCH-CMS Count=" + context.Session["cmscount" + HttpContext.Current.Session.SessionID].ToString());
                        context.Response.Write(json);
                        break;
                    case "taburl":
                        sParams = new SearchParameters();
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(Globals.NavigateURL(context.Request.QueryString["tabid"]));
                        context.Response.Write(json);
                        break;                     
                    case "multipleisbn":
                        string AppTarget = string.Empty, sessionID = HttpContext.Current.Session.SessionID;
                        if (visitor != null)
                        {
                            AppTarget = (visitor.CountryCode == "NZ") ? "NZALL" : "AUALL";
                        }
                        else
                        {
                            AppTarget = "AUALL";
                        }
                        StringBuilder isbns = new StringBuilder();
                        sParams = Deserialize<SearchParameters>(context);
                        string[] removeSpace = sParams.SearchText.Split(',');
                        string removeSpaceIsbn = string.Empty;
                        foreach (string isbn in removeSpace)
                        {
                            bool isTrue = false;
                            if (Regex.IsMatch(isbn, @"^[\d ]+$"))
                            {
                                removeSpaceIsbn = isbn.Replace(" ", string.Empty).Trim();
                                isTrue = true;
                            }
                            if (isTrue)
                            {
                                isbns.Append(removeSpaceIsbn + ",");
                            }
                            else
                            {
                                isbns.Append(isbn + ",");
                            }

                        }
                        context.Cache[sessionID + "isbn"] = isbns.ToString().Trim().Trim(',');
                        if (sParams.Division == "primary" || sParams.Division == "secondary")
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(string.Concat("/search?q=&searchtype=advisbn&division=", sParams.Division, "&keyisbn=", HttpUtility.HtmlEncode(sessionID), "isbn"));
                        else
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(string.Concat("/search?st=advisbn&division=", sParams.Division, "&keyisbn=", HttpUtility.HtmlEncode(sessionID), "isbn"));
                        context.Response.Write(json);
                        break;
                    case "getisbns":
                        string filePath = string.Empty;//Path.Combine(@"d:\", context.Request.QueryString["path"]);
                        //FileUpload a = new FileUpload();
                        //if (!File.Exists(filePath))
                       //     File.Delete(filePath);
                        //a.SaveAs(filePath);
                        if (context.Request.Files.Count > 0)
                        {
                            HttpFileCollection filesCollection = context.Request.Files;
                            foreach (string postedFileName in filesCollection)
                            {
                                HttpPostedFile postedFile = filesCollection[postedFileName];
                                filePath = context.Server.MapPath("~/Temp"+ postedFile.FileName);
                                postedFile.SaveAs(filePath);
                            }
                        }
                        
                        if (Path.GetExtension(filePath).Contains(".csv") || Path.GetExtension(filePath).Contains(".txt")
                        || Path.GetExtension(filePath).Contains(".xls") || Path.GetExtension(filePath).Contains(".xlsx"))
                        {
                            string isbnsValue = GetDataTableFromCsvOrXls(filePath);
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(isbnsValue.Replace("\r\n", string.Empty));
                           // if (filePath != string.Empty)
                            //    File.Delete(filePath);
                        }
                        else
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(string.Empty);
                        context.Response.Write(json);
                        break;
                    case "currentfacets":    
                        context.Response.ContentType = "text/plain";
                        context.Session["HECurrentFacetTable" + HttpContext.Current.Session.SessionID] = GetCurrentFacetTable(context);
                        context.Response.Write((context.Session["HECurrentFacetTable"+HttpContext.Current.Session.SessionID] as DataTable).Rows.Count);
                        break;
                    case "clearfacets":
                        context.Session["HECurrentFacetTable"+HttpContext.Current.Session.SessionID] = null;
                        context.Response.Write(0);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                LogHandlerExceptions(Ex);
            }
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
        /// product result handler ToDataTable function
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DataTable GetCurrentFacetTable(HttpContext context)// T is any generic type
        {
            try
            {
                DnnLog.Info("Current Facet Formation CALL-->" + DateTime.Now.ToString());
                List<Facets> CurrFacetsLst = Deserialize<List<Facets>>(context);
                DataTable AttributeStateTable = new DataTable();
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE", typeof(string));
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_SK", typeof(int));
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE", typeof(string));
                AttributeStateTable.Columns.Add("ATTRIBUTE_TYPE_VALUE_SK", typeof(int));
                AttributeStateTable.Columns.Add("PROD_COUNT", typeof(int));
                AttributeStateTable.Columns.Add("IS_CURRENT", typeof(char));
                AttributeStateTable.Columns.Add("IS_SELECTED", typeof(char));
                AttributeStateTable.Columns.Add("IS_PARENT", typeof(char));               
                List<Facets> parentFacetsLst = new List<Facets>();
                List<Facets> childFacetsLst = new List<Facets>();
                DnnLog.Info("Ranjith [Facet Header]-->  a.ATTRIBUTE_NAME  |  a.ATTRIBUTE_TYPE_SK  |  a.ATTRIBUTE_TYPE_VALUE  |  a.ATTRIBUTE_TYPE_VALUE_SK  |  a.PROD_COUNT  |  a.IS_CURRENT  |  a.IS_SELECTED  |  a.IS_PARENT");
                CurrFacetsLst.ForEach(a =>
                {
                    if (a.ATTRIBUTE_TYPE_VALUE_SK == 0)
                    {
                        if (!parentFacetsLst.Exists(c => c.ATTRIBUTE_TYPE_SK == a.ATTRIBUTE_TYPE_SK))
                        {
                            DnnLog.Info("Ranjith [Parent Facet]-->"+a.ATTRIBUTE_NAME + '|' + a.ATTRIBUTE_TYPE_SK + '|' + a.ATTRIBUTE_TYPE_VALUE + '|' + a.ATTRIBUTE_TYPE_VALUE_SK + '|' + a.PROD_COUNT + '|' + a.IS_CURRENT + '|' + a.IS_SELECTED + '|' + a.IS_PARENT);
                            AttributeStateTable.Rows.Add(WebUtility.HtmlDecode(a.ATTRIBUTE_NAME), a.ATTRIBUTE_TYPE_SK, WebUtility.HtmlDecode(a.ATTRIBUTE_TYPE_VALUE), a.ATTRIBUTE_TYPE_VALUE_SK, a.PROD_COUNT, a.IS_CURRENT, a.IS_SELECTED, a.IS_PARENT);
                            parentFacetsLst.Add(a);
                        }
                    }
                    else
                    {
                        DnnLog.Info("Ranjith [Child Facet]-->" + a.ATTRIBUTE_NAME + '|' + a.ATTRIBUTE_TYPE_SK + '|' + a.ATTRIBUTE_TYPE_VALUE + '|' + a.ATTRIBUTE_TYPE_VALUE_SK + '|' + a.PROD_COUNT + '|' + a.IS_CURRENT + '|' + a.IS_SELECTED + '|' + a.IS_PARENT);
                        AttributeStateTable.Rows.Add(WebUtility.HtmlDecode(a.ATTRIBUTE_NAME), a.ATTRIBUTE_TYPE_SK, WebUtility.HtmlDecode(a.ATTRIBUTE_TYPE_VALUE), a.ATTRIBUTE_TYPE_VALUE_SK, a.PROD_COUNT, a.IS_CURRENT, a.IS_SELECTED, a.IS_PARENT);
                        childFacetsLst.Add(a);
                    }
                });
              
                return AttributeStateTable;
            }
            catch (Exception ex)
            {
                LogHandlerExceptions(ex);                
            }
            return null;
        }      

        public string GetDataTableFromCsvOrXls(string Path)
        {
            string isbnstring = string.Empty;
            try
            {
                DataTable dtexcel = new DataTable();
                bool hasHeaders = false;
                string HDR = hasHeaders ? "Yes" : "No";
                string strConn;

                if (Path.Substring(Path.LastIndexOf('.')).ToLower() == ".xlsx" || Path.Substring(Path.LastIndexOf('.')).ToLower() == ".xls")
                {
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                    //DataTable HeaderColumn = ImportExcelXLS(Path);
                    OleDbConnection conn = new OleDbConnection(strConn);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    conn.Open();
                    DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    StringBuilder Isbns = new StringBuilder();
                    //Looping a first Sheet of Xl File
                    DataRow schemaRow = schemaTable.Rows[0];
                    string sheet = schemaRow["TABLE_NAME"].ToString();
                    cmd.CommandText = "SELECT top 1 * From [" + sheet + "]";
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable HeaderColumns = new DataTable();
                    da.SelectCommand = cmd;
                    HeaderColumns.Locale = CultureInfo.CurrentCulture;
                    da.Fill(HeaderColumns);
                    foreach (DataColumn column in HeaderColumns.Columns)
                    {
                        Isbns.Append(column.ColumnName + "," + Environment.NewLine);
                    }

                    if (!sheet.EndsWith("_"))
                    {
                        string query = "SELECT  * FROM [" + sheet + "] ";
                        OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                        dtexcel.Locale = CultureInfo.CurrentCulture;
                        daexcel.Fill(dtexcel);
                    }

                    conn.Close();



                    if (dtexcel != null && dtexcel.Rows.Count > 0)
                    {
                        foreach (DataRow Row in dtexcel.Rows)
                        {
                            Isbns.Append(Row[0].ToString() + "," + Environment.NewLine);
                        }
                    }
                    isbnstring = Isbns.ToString().TrimEnd(Environment.NewLine.ToCharArray()).TrimEnd(',');                    
                }
                else if (Path.Substring(Path.LastIndexOf('.')).ToLower() == ".csv" || Path.Substring(Path.LastIndexOf('.')).ToLower() == ".txt")
                {
                    DataTable DataTable = new DataTable();
                    String[] Values;

                    Values = File.ReadAllLines(Path);

                    string[] CsvRows = System.IO.File.ReadAllLines(Path);
                    string[] Headers = { "Isbn" };

                    // Adding columns name
                    //foreach (var Item in Headers)
                    //    DataTable.Columns.Add(new DataColumn(Item));
                    int count = 0;
                    string[] Fields = null;
                    StringBuilder Isbnstest = new StringBuilder();
                    foreach (string CsvRow in CsvRows)
                    {
                        Fields = CsvRow.Trim(',').Split(',');
                        foreach (string isbn in Fields)
                        {
                            Isbnstest.Append(isbn + "," + Environment.NewLine);
                            DataTable.Columns.Add(new DataColumn("ISBN" + count));
                            count++;
                        }
                    }

                    foreach (string CsvRow in CsvRows)
                    {

                        Fields = CsvRow.Trim(',').Split(',');

                        if (!string.IsNullOrEmpty(Fields[0].Trim()))
                        {
                            DataRow DataRow = DataTable.NewRow();
                            DataRow.ItemArray = Fields;
                            DataTable.Rows.Add(DataRow);
                        }
                    }
                    StringBuilder Isbns = new StringBuilder();
                    int rowcount = 0;
                    if (DataTable != null && DataTable.Rows.Count > 0)
                    {
                        foreach (DataRow Row in DataTable.Rows)
                        {
                            Isbns.Append(Row[rowcount].ToString() + "," + Environment.NewLine);
                            rowcount++;
                        }

                        isbnstring = Isbns.ToString().TrimEnd(Environment.NewLine.ToCharArray()).TrimEnd(',');
                    }
                    isbnstring = Isbnstest.ToString().TrimEnd(Environment.NewLine.ToCharArray()).TrimEnd(',').Trim('"').Replace('"', ' ');
                }
                return isbnstring;
            }
            catch (Exception ex)
            {
                LogHandlerExceptions(ex);
            }
            return isbnstring;
        }

        /// <summary>
        ///  Writes Exceptions to log file in user readable format
        /// </summary>
        /// <param name="e"></param>
        public static void LogHandlerExceptions(Exception e)
        {
            if (e.Source != "mscorlib")
            {
                FileStream fileStream = null;
                StreamWriter streamWriter = null;
                try
                {
                    string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "HE Handler Exceptions - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");

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
                    var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    streamWriter.WriteLine(string.Concat("\r\n-----------------------------------", DateTime.Now, "---------------------------------------------",
                       "\r\n Website: ", uri.Host,
                        "\r\n Type: ", e.GetType(),
                        "\r\n Source: ", e.Source, "\r\n Exception: ", e.Message, "\r\n Description: ", e.StackTrace, "\r\n-----------------------------------------------------------------------------------------------------------------"));

                    // TeacherController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
                }
                finally
                {
                    if (streamWriter != null) streamWriter.Close();
                    if (fileStream != null) fileStream.Close();
                }
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