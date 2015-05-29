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

using System.Collections;
using DotNetNuke.Entities.Modules;
using System;
using DotNetNuke.UI.UserControls;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DotNetNuke.Common;
using System.Text;
using System.Net.Mail;
using System.IO;
using DotNetNuke.Common.Utilities;
using System.Net;
using System.Data.OleDb;
using System.Linq;
using System.Xml;
using System.Web;
namespace DotNetNuke.Modules.eCollection_Dashboards
{

    /// <summary>
    /// This base class can be used to define custom properties for multiple controls. 
    /// An example module, DNNSimpleArticle (http://dnnsimplearticle.codeplex.com) uses this for an ArticleId
    /// 
    /// Because the class inherits from PortalModuleBase, properties like ModuleId, TabId, UserId, and others, 
    /// are accessible to your module's controls (that inherity from eCollection_DashboardsModuleBase
    /// 
    /// </summary>

    public class eCollection_DashboardsModuleBase : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected const string StudentsModule = "eCollection_Students";
        protected const string TeachersModule = "eCollection_Teachers";
        protected const string GroupsModule = "eCollection_Groups";
        protected const string SessionsModule = "eCollection_Sessions";
        protected const string BooksModule = "eCollection_Books";
        protected const string DashboardsModule = "eCollection_Dashboards";
        protected const string eCollectionLogin = "eCollection_Login";
        public const string CommonModule = "eCollection Common";


        protected const string STUDENTPROFILE = "profile";
        protected const string TEACHERPROFILE = "profile";

        protected const string DASHBOARD = "dashboard";
        protected const string CENGAGESTAGING = "cengagestaging";
        protected const string REQUESTACCESS = "requestaccess";
        protected const string APPDATACOLLECTION = "ecollectionreport";
        protected const string FEEDBACK = "feedback";
        protected const string LOGOUT = "logout";
        protected const string UPGRADESTEPS = "upgradesteps";
        protected const string UPGRADESTEPTWO = "upgradesteptwo";
        protected const string UPGRADESTEPONE = "upgradestepone";
        protected const string ASIANUSERSETUP = "asia";
        #region Private Members
        private Users _user = new Users();
        private DataTable _subsList = new DataTable();
        #endregion

        #region Protected Numbers
        protected DashboardController _dashboardController = DashboardController.Instance;
        protected string LoginName
        {
            get
            {
                return Null.SetNullString(Session["UserName"]);
            }
            set
            {
                Session["UserName"] = value;
            }
        }

        protected Users UserDetail
        {
            get
            {
                try
                {
                    _user = _dashboardController.UserDetails(new Users() { UserLoginName = LoginName, SubscriptionId = int.Parse(Session["Subscription"].ToString()) });
                }
                catch (Exception ex)
                {
                    LogFileWrite(ex);
                }
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        protected Users StaffDetail
        {
            get
            {
                try
                {
                    _user = _dashboardController.UserDetails(new Users() { UserLoginName = LoginName, SubscriptionId = 0 });
                }
                catch (Exception ex)
                {
                    LogFileWrite(ex);
                }
                return _user;
            }
            set
            {
                _user = value;
            }
        }

        protected DataTable SubsList
        {
            get
            {
                try
                {
                    _subsList = _dashboardController.GetSubscriptionsList(new Users() { UserLoginName = LoginName, Active = 'Y' });
                }
                catch (Exception ex)
                {
                    LogFileWrite(ex);
                }
                return _subsList;
            }
            set
            {
                _subsList = value;
            }
        }
        #endregion


        protected int GetTabID(string ModuleName)
        {
            int modID = 0;
            DotNetNuke.Entities.Modules.ModuleController mc = new ModuleController();
            ArrayList existMods = mc.GetModulesByDefinition(this.PortalId, ModuleName);
            foreach (DotNetNuke.Entities.Modules.ModuleInfo mi in existMods)
            {
                if (!mi.IsDeleted)
                {
                    modID = mi.TabID;
                }
            }
            int iProfileTabId = Convert.ToInt32(modID);
            return iProfileTabId;
        }

        public int RenewalLicenseCount
        {
            get
            {
                try
                {
                    return _dashboardController.GetRenewalLicenseCount(LoginName, NEW_CUST_SUBS_SK, OLD_CUST_SUBS_SK);
                }
                catch (Exception ex)
                {
                    LogFileWrite(ex);
                }
                return -1;
            }
        }

        public string renewalSubscriptionName
        {

            get
            {
                try
                {
                    return _dashboardController.GetRenewalSubscriptionName(LoginName);
                }
                catch (Exception ex)
                {
                    LogFileWrite(ex);
                }
                return string.Empty;
            }
        }

        public int RenewalBookCount
        {
            get
            {
                try
                {
                    return _dashboardController.GetRenewalBookCount(LoginName, NEW_CUST_SUBS_SK, OLD_CUST_SUBS_SK);
                }
                catch (Exception ex)
                {
                    LogFileWrite(ex);
                }
                return -1;
            }
        }

        public List<Renewel> Allrenewels
        {
            get
            {
                try
                {
                    return _dashboardController.GetAllrenewels(Null.SetNullInteger(Session["Subscription"]));//LoginName);
                }
                catch (Exception ex)
                {
                    LogFileWrite(ex);
                }
                return null;
            }
        }

        public int NEW_CUST_SUBS_SK
        {
            get
            {
                if (Session["NEW_CUST_SUBS_SK"] == null)
                    Session["NEW_CUST_SUBS_SK"] = 0;

                return (int)Session["NEW_CUST_SUBS_SK"];
            }
            set
            {
                Session["NEW_CUST_SUBS_SK"] = value;
            }
        }

        public int OLD_CUST_SUBS_SK
        {
            get
            {
                if (Session["OLD_CUST_SUBS_SK"] == null)
                    Session["OLD_CUST_SUBS_SK"] = 0;

                return (int)Session["OLD_CUST_SUBS_SK"];
            }
            set
            {
                Session["OLD_CUST_SUBS_SK"] = value;
            }
        }
        public int[] subValidation
        {
            get
            {
                int custSub = Convert.ToInt32(Session["Subscription"].ToString());
                return _dashboardController.ValidateSubs(custSub);
            }
        }
        public string OLD_CUST_SUBS_SK_NAME
        {
            get
            {
                if (Session["OLD_CUST_SUBS_SK_NAME"] == null)
                    Session["OLD_CUST_SUBS_SK_NAME"] = string.Empty;

                return (string)Session["OLD_CUST_SUBS_SK_NAME"];
            }
            set
            {
                Session["OLD_CUST_SUBS_SK_NAME"] = value;
            }
        }

        public int RENEWELCOUNT
        {
            get
            {
                if (Session["RENEWELCOUNT"] == null)
                    Session["RENEWELCOUNT"] = 0;

                return (int)Session["RENEWELCOUNT"];
            }
            set
            {
                Session["RENEWELCOUNT"] = value;
            }
        }

        protected List<Messages> UploadMessages
        {
            get
            {
                return _dashboardController.GetErrorMessagesByModuleName(TeachersModule);
            }
        }
        protected string GetMessage(string Message_Code)
        {
            try
            {
                Messages Error_Message = UploadMessages.Find(x => x.MessageCode.Equals(Message_Code));

                return Error_Message.MessageDesc;
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return string.Empty;
        }

        public static void LogFileWrite(Exception e)
        {
            if (e.Source != "mscorlib")
            {
                FileStream fileStream = null;
                StreamWriter streamWriter = null;
                try
                {
                    string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "Exceptions - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");
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
                       "\r\n Logged-in UserName: ", HttpContext.Current.Session["UserName"].ToString(),
                        "\r\n Type: ", e.GetType(),
                        "\r\n Source: ", e.Source, "\r\n Exception: ", e.Message, "\r\n Description: ", e.StackTrace, "\r\n-----------------------------------------------------------------------------------------------------------------"));
                    string body = string.Concat("<html",
                     "<body>",
                         "<h4>-----------------------------------", DateTime.Now, "--------------------------------------------- </h4>",
                         "<h4> Website: <b>", uri.Host, "</b></h4>",
                         "<h4>Type: ", e.GetType(), "</h4><br /><h4>Source: ", e.Source, "</h4><br /><h4> Exception: ",
                           e.Message, "</h4><br /><h4> Description: ", e.StackTrace, "</h4><br /><h4>-----------------------------------------------------------------------------------------------------------------</h4><br />",
                     "</body>",
                 "</html>");                   
                    DashboardController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
                }
                finally
                {
                    if (streamWriter != null) streamWriter.Close();
                    if (fileStream != null) fileStream.Close();
                }
            }
        }

        private static string GetIP()
        {
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();

            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            return addr[addr.Length - 1].ToString();

        }


        /// <summary>
        /// Insert all path,word and context audio file details of a book
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void WriteAudioPath(List<string> FileFullNames,string isbn)
        {
            //LogFileWrite(new Exception("Audio Insertion Starts : " + isbn));
            int i = 0;
            foreach (string fileName in FileFullNames)
            {
                i++;
                try
                {
                    //Import Excel into Data table 
                    DataTable audioFiles = ImportExcelXLS(fileName);

                    //Get all the affected records 
                    IEnumerable<DataRow> query1 = from row in audioFiles.AsEnumerable()
                                                  select row;
                    //Check whether excel file is not empty
                    if (query1.Count() > 0)
                    {
                        audioFiles = query1.CopyToDataTable();
                        // Construct the xml from datatable
                        StringBuilder str = new StringBuilder();
                        StringWriter writer = new StringWriter(str);
                        audioFiles.TableName = "AudioFiles";
                        audioFiles.WriteXml(writer, true);

                        // create a audio object for upload
                        Audio insertAudio = new Audio()
                        {
                            ISBN = isbn,
                            AudioXml = new XmlDocument() { InnerXml = str.ToString() },
                            FileType = i == 1 ? "Context" : (i == 2 ? "Page" : "Word")
                        };
                        DashboardController.Instance.InsertAudioFiles(insertAudio);
                        //LogFileWrite(new Exception("Audio Insertion Over : " + isbn));
                    }
                    else
                    {
                        LogFileWrite(new Exception("Empty Audio Excel File : " + fileName));
                        return;
                    }

                }
                catch (Exception ex)
                {
                    LogFileWrite(ex);
                }               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="hasHeaders"></param>
        /// <returns></returns>
        public static DataTable ImportExcelXLS(string FileName)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=0\"";
            DataTable outputTable = null;
            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    DataTable schemaTable = conn.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    foreach (DataRow schemaRow in schemaTable.Rows)
                    {
                        string sheet = schemaRow["TABLE_NAME"].ToString();
                        outputTable = new DataTable(sheet);
                        if (!sheet.EndsWith("_"))
                        {
                            try
                            {
                                OleDbCommand cmd = new OleDbCommand("SELECT TOP 2000 * FROM [" + sheet + "] ", conn);
                                cmd.CommandType = CommandType.Text;
                                new OleDbDataAdapter(cmd).Fill(outputTable);
                                if(outputTable.Rows.Count>0)
                                    break;
                            }
                            catch (Exception ex)
                            {
                                LogFileWrite(new Exception(string.Concat(ex.Message, string.Format(" Sheet:{0} .File:F{1}", sheet, FileName))));
                            }
                        }
                    }

                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return outputTable;
        }

    }
}
