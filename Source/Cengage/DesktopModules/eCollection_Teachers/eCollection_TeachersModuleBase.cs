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

using DotNetNuke.Entities.Modules;
using System;
using System.Collections;
using DotNetNuke.Modules.eCollection_Teachers.Components.Controller;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Common;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Mail;
using DotNetNuke.Common.Utilities;
using System.Web;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
namespace DotNetNuke.Modules.eCollection_Teachers
{

    /// <summary>
    /// This base class can be used to define custom properties for multiple controls. 
    /// An example module, DNNSimpleArticle (http://dnnsimplearticle.codeplex.com) uses this for an ArticleId
    /// 
    /// Because the class inherits from PortalModuleBase, properties like ModuleId, TabId, UserId, and others, 
    /// are accessible to your module's controls (that inherity from eCollection_TeachersModuleBase
    /// 
    /// </summary>

    public class eCollection_TeachersModuleBase : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region Module Names
        protected const string GroupsModule = "eCollection_Groups";
        protected const string SessionsModule = "eCollection_Sessions";
        protected const string StudentsModule = "eCollection_Students";
        protected const string BooksModule = "eCollection_Books";
        protected const string TeachersModule = "eCollection_Teachers";
        protected const string DashboardsModule = "eCollection_Dashboards";
        protected const string ActiveModule = "eCollection Teachers";
        public const string CommonModule = "eCollection Common";
        #endregion

        #region Page Redirection Strings
        protected const string TEACHERPROFILE = "profile";
        protected const string CREATETEACHERPROFILE = "createprofile";
        protected const string TEACHERPROFILEBULKUPLOAD = "bulkupload";
        protected const string MYRECORDINGS = "recordings";
        protected const string MYWORDS = "words";
        protected const string GROUPPROFILE = "groupprofile";
        protected const string ADDTOGROUP = "addtogroup";
        protected const string CREATEREADINGSESSION = "createsession";
        #endregion

        #region Private Members
        private List<Teacher> _allTeachers;
        private List<Teacher> _allOtherTeachers;
        private DataTable _subsList = new DataTable();
        private List<Teacher> _selected = new List<Teacher>();
        #endregion

        #region Protected Members
        protected TeacherController teacherController = TeacherController.Instance;
        protected int TeacherId
        {
            get
            {
                object value = Session["TeacherId"] = 12;
                return value == null ? 0 : (int)value;
            }
            private set
            {
                Session["TeacherId"] = value;
            }
        }
        protected int TradPartnerAccId
        {
            get
            {
                object value = Session["TradPartnerAccId"] = 3;
                return value == null ? 0 : (int)value;
            }
            private set
            {
                Session["TradPartnerAccId"] = value;
            }
        }

        protected List<Teacher> AllTeachers
        {
            get
            {
                try
                {
                    if (_allTeachers == null)
                    {
                        _allTeachers = _allOtherTeachers = teacherController.GetAll(new Teacher() { TeacherLoginName = Session["UserName"].ToString(),
                                                                                                    Active = (char)MyEnums.Active.Yes,
                                                                                                    SubscriptionId = int.Parse(Session["Subscription"].ToString())
                                                                                         });
                    }
                }
                catch (Exception ex) { LogFileWrite(ex); }      
                return _allTeachers;
            }
            set { _allTeachers = value; }
        }

        protected string TeacherLoginName
        {
            get
            {
                if(Session["UserName"]==null)
                    Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));
                return Session["UserName"].ToString();
            }
            private set
            {
                Session["UserName"] = value;
            }
        }

        protected DataTable SubsList
        {
            get
            {
                _subsList = DashboardController.Instance.GetSubscriptionsList(new Users() { UserLoginName = TeacherLoginName, Active = 'Y' });
                return _subsList;
            }
            set
            {
                _subsList = value;
            }
        }
        protected List<DotNetNuke.Modules.eCollection_Teachers.Components.Model.Messages> TeachersMessages
        {
            get
            {
                return teacherController.GetErrorMessagesByModuleName(ActiveModule);
            }
        }
        #endregion

        #region Common Methods
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModuleName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected Teacher selectedTeacher()
        {
            try
            {
                return AllTeachers.Find(delegate(Teacher teacher) { return teacher.UserLoginName == Request.QueryString["username"].ToString(); });
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string encrypt(string text)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(toEncryptArray, 0, toEncryptArray.Length);
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message_Code"></param>
        /// <returns></returns>
        protected string GetMessage(string Message_Code)
        {
            try
            {
                DotNetNuke.Modules.eCollection_Teachers.Components.Model.Messages Error_Message = TeachersMessages.Find(x => x.MessageCode.Equals(Message_Code));

                return Error_Message.MessageDesc;
            }
            catch (Exception ex) { LogFileWrite(ex); }      
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string ProfileUrl
        {
            get
            {
                return Globals.NavigateURL(new eCollection_TeachersModuleBase().GetTabID(TeachersModule)) + "?pagename=" + TEACHERPROFILE +"&username=";
            }
            set
            {
                ProfileUrl = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected Teacher GetRecordingsDetails(DataRow dr)
        {
            Teacher teacher = new Teacher();
            try
            {
                teacher.PageName = string.Concat("Content Page ", dr["PageName"].ToString().Split('-')[1]);
                teacher.RecPath = dr["RecPath"].ToString();
                teacher.BookOpenedDate = dr["OpenedDate"].ToString();
            }
            catch (Exception ex) { LogFileWrite(ex); }      
            return teacher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected Teacher GetRHRecordingsDetails(DataRow dr)
        {
            Teacher teacher = new Teacher();
            try
            {
                teacher.BookID = dr["BookID"].ToString();
                teacher.BookOpenedDate = dr["OpenedDate"].ToString();
                teacher.PageName = string.Concat("Content Page ", dr["PageName"].ToString().Split('-')[1]);
                teacher.RecPath = dr["FilePath"].ToString().ToLower();
                teacher.BookOpenedTime = dr["TimeSpan"].ToString();
            }
            catch (Exception ex) { LogFileWrite(ex); }      
            return teacher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        protected List<TeacherRecordingFiles> SaveRecordFile(List<TeacherRecordingFiles> dataSource)
        {
            try
            {
                dataSource.ForEach(x =>
                {
                    if (x.RecPath != string.Empty)
                    {
                        string driveName = ConfigurationManager.AppSettings.Get("RecordingPath");
                        string text = System.IO.File.ReadAllText(Path.Combine(driveName, x.RecPath));

                        byte[] bytes = System.Convert.FromBase64String(text);
                        int indexoftxt = x.RecPath.LastIndexOf(".txt");
                        x.RecPath = string.Concat(x.RecPath.Substring(0, indexoftxt), x.RecPath.Substring(indexoftxt, 4).Replace(".txt", ".m4a"));
                        if (!(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecPath))))
                            File.WriteAllBytes(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecPath), bytes);
                    }
                });
            }
            catch (Exception ex) { LogFileWrite(ex); }      
            return dataSource;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        protected bool ValidateEmail(string emailaddress)
        {
            try
            {
                return Regex.IsMatch(emailaddress.Trim(), @"^(([^<>()[\]\\.,;:\s@\""]+"
                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                        + @"[a-zA-Z]{2,}))$", RegexOptions.IgnoreCase);
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
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
                   // TeacherController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
                }
                finally
                {
                    if (streamWriter != null) streamWriter.Close();
                    if (fileStream != null) fileStream.Close();
                }
            }
        }      
       #endregion
    }
}
