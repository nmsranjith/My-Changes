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
using DotNetNuke.Services.Localization;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using System.Linq;
using System.Data;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.IO;
using System.Configuration;
using DotNetNuke.Common.Utilities;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Sessions
{

    /// <summary>
    /// This base class can be used to define custom properties for multiple controls. 
    /// An example module, DNNSimpleArticle (http://dnnsimplearticle.codeplex.com) uses this for an ArticleId
    /// 
    /// Because the class inherits from PortalModuleBase, properties like ModuleId, TabId, UserId, and others, 
    /// are accessible to your module's controls (that inherity from eCollection_SessionsModuleBase
    /// 
    /// </summary>

    public class eCollection_SessionsModuleBase : DotNetNuke.Entities.Modules.PortalModuleBase
    {
       protected const string GroupsModule = "eCollection_Groups";
       protected const string SessionsModule = "eCollection Sessions";
        protected const string StudentsModule = "eCollection_Students";
        protected const string BooksModule = "eCollection_Books";
        protected const string TeachersModule = "eCollection_Teachers";
        protected const string DashboardsModule = "eCollection_Dashboards";

        protected const string SESSIONLIST = "sessions";
        protected const string SESSIONPROFILE = "sessionprofile";
        protected const string CREATESESSION = "createsession";
        protected const string ADDGROUPSTOSESSION = "addgroupstosession";
        protected const string ADDSTUDENTTOSESSION = "addstudenttosession";
        protected const string ADDTEACHERSTOSESSION = "addteacherstosession";
        protected const string ADDBOOKSTOSESSION = "addbookstosession";
        protected const string EDITSESSION = "editsession";

        public DataTable _subsList;
        private List<Sessions> _activeSessions;
        private List<Sessions> _allActiveSessions ;
        private List<Sessions> _finishedSessions;
        private List<Sessions> _allFinishedSessions;
        private List<Sessions> _archivedSessions;
        private List<Sessions> _allArchivedSessions;
        private int? _sessionId;
        private List<Student> _allStudents;
        private List<Student> _allOtherStudents;
        private List<Teacher> _allTeachersList;
        private List<Books> _books;
        private List<Group> _groupsList;


        private List<Sessions> _sessions = new List<Sessions>();
        SessionController _sessionController = SessionController.Instance;

        public int UserID = 0;        
        public char GroupType = 'N';
        public char ClassType = 'C';
        public int ConstRowCount
        {
            get
            {
                return int.Parse(Session["RowCount"].ToString());
            }
            set
            {
                Session["RowCount"] = value;
            }
        }
        public int? EditSessionId
        {
            get
            {                
                if (Session["EditSelectedId"] == null)
                {
                    return -1;
                }
                else
                {
                    return Session["EditSelectedId"] as int?;
                }                
            }
            protected set 
            {
                Session["EditSelectedId"] = value; 
            }
        }

        public int? CreateSessionId
        {
            get
            {
                if (Session["StartSessionId"] == null)
                {
                    return -1;
                }
                else
                {
                    return Session["StartSessionId"] as int?;
                }
            }
            protected set
            {
                Session["StartSessionId"] = value;
            }
        }

        public List<Sessions> SessionList
        {
            get
            {
                if (_sessions == null)
                {
                    if (Session["sessionList"] == null)
                    {
                        _sessions = SessionController.Instance.GetAll(RoleId, true);
                        Session["sessionList"] = _sessions;
                    }
                    else
                    {
                        _sessions = Session["sessionList"] as List<Sessions>;
                    }
                }
                return _sessions;
            }
            set { _sessions = value; }
        }

        private List<SessionMembers> _selectedGroups = new List<SessionMembers>();
        public List<SessionMembers> GroupsSelected
        {
            get
            {
                if (Session["SelectedGroups"] == null)
                    Session["SelectedGroups"] = new List<SessionMembers>();

                return (List<SessionMembers>)Session["SelectedGroups"];
            }
            set
            {
                Session["SelectedGroups"] = (List<SessionMembers>)value;
            }
        }

        private List<SessionProducts> _selectedProducts = new List<SessionProducts>();
        public List<SessionProducts> ProductsSelected
        {
            get
            {
                if (Session["SelectedProducts"] == null)
                    Session["SelectedProducts"] = new List<SessionProducts>();                
                return (List<SessionProducts>)Session["SelectedProducts"];
            }
            set
            {
                Session["SelectedProducts"] = (List<SessionProducts>)value;               
            }
        }
        

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

        public int RoleId
        {
            get
            {
                object value = Session["RoleId"];
                return value == null ? 0 : (int)value;
            }
            protected set { Session["RoldId"] = value; }
        }
        protected DataTable SubsList
        {
            get
            {
                _subsList = DashboardController.Instance.GetSubscriptionsList(new Users() { UserLoginName = LoginName, Active = 'Y' });
                return _subsList;
            }
            set
            {
                _subsList = value;
            }
        }    

        #region PortalModuleBase Overrides

        protected override void OnInit(EventArgs e)
        {
            
            Type baseType = GetType().BaseType;
            if (baseType != null)
                LocalResourceFile = Localization.GetResourceFile(this, baseType.Name + ".ascx");
            base.OnInit(e);
        }

        #endregion


        #region "Session List"

        protected List<Sessions> ActiveSessions
        {
            get
            {
                if (_activeSessions == null)
                {
                    _activeSessions = AllActiveSessions.FindAll(delegate(Sessions sessions) { return DateTime.Now.Date < sessions.SessionExpiryDate.Date; });
                }

                return _activeSessions;
            }
            set { _activeSessions = value; }
        }

        protected List<Sessions> FinishedSessions
        {
            get
            {
                if (_finishedSessions == null)
                {
                    _finishedSessions = AllFinishedSessions.FindAll(delegate(Sessions sessions) { return DateTime.Now.Date >= sessions.SessionExpiryDate.Date && !Helper.DiffInMonths(sessions.SessionExpiryDate.Date, DateTime.Now.Date); });
                }
                
                return _finishedSessions;
            }
            set { _finishedSessions = value; }
        }

        protected List<Sessions> ArchivedSessions
        {
            get
            {
                if (_archivedSessions == null)
                {
                    _archivedSessions = AllArchivedSessions.FindAll(delegate(Sessions sessions) { return Helper.DiffInMonths(sessions.SessionExpiryDate.Date, DateTime.Now.Date); });
                }

                return _archivedSessions;
            }
            set { _archivedSessions = value; }
        }



        protected List<Sessions> AllActiveSessions
        {
            get
            {
                try
                {
                    return _sessionController.GetAllActiveSessions(UserId, LoginName, SelectedSubscriptionId);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }  
        }


        protected List<Sessions> AllFinishedSessions
        {
            get
            {
                try
                {
                    return _sessionController.GetAllFinishedSessions(UserId, LoginName, SelectedSubscriptionId);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }   
        }

        protected List<Sessions> AllArchivedSessions
        {
            get
            {
                try
                {
                    return _sessionController.GetAllArchivedSessions(UserId, LoginName, SelectedSubscriptionId);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }            
        }

        protected List<IDCollection> Subscription
        {
            get
            {
                try
                {
                    return _sessionController.GetSubscription(UserId, LoginName);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }
        }


        protected List<IDCollection> Teachers
        {
            get
            {
                try
                {
                    return _sessionController.GetTeachers(SelectedSubscriptionId, LoginName);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }
        }

        protected List<Group> GroupsList
        {
            get
            {
                try
                {
                    _groupsList = _sessionController.GetGroups(MyEnums.GroupType.N, LoginName, SelectedSubscriptionId);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return _groupsList;
            }
        }

        protected List<Group> ClassesList
        {
            get
            {
                try
                {
                    return _sessionController.GetGroups(MyEnums.GroupType.C, LoginName, SelectedSubscriptionId);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }
        }

      
        protected List<Student> AllOtherStudents
        {
            get
            {
                try
                {
                    _allOtherStudents = _sessionController.GetStudentsList(SelectedSubscriptionId, LoginName);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return _allOtherStudents;
            }
            set { _allOtherStudents = value; }
        }

        protected List<Student> FullListStudents
        {
            get
            {
                try
                {
                    _allOtherStudents = _sessionController.GetStudentsFullList(SelectedSubscriptionId, LoginName);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return _allOtherStudents;
            }
            set { _allOtherStudents = value; }
        }

        protected List<Teacher> FullListTeachers
        {
            get
            {
                try
                {
                    _allTeachersList = _sessionController.GetAllTeachersList(SelectedSubscriptionId, LoginName);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return _allTeachersList;
            }
            set { _allTeachersList = value; }
        }
     

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
        public string[] SelectedID
        {
            get
            {
                if (Session["ClassSelectedID"] == null)
                {
                    return null;
                }
                else
                {
                    return Session["ClassSelectedID"] as string[];
                }
            }
            set
            {
                Session["ClassSelectedID"] = value;
            }
        }
        public int SelectedSubscriptionId
        {
            get
            {
                if (Session["Subscription"] == null)
                {
                    Session["Subscription"] = -1;
                }

                return Convert.ToInt32(Session["Subscription"]);
            }           
        }

        public string SessionNotes
        {
            get
            {
                if (Session["SessionNotes"] == null)
                    Session["SessionNotes"] = string.Empty;

                return (string)Session["SessionNotes"];
            }
            set
            {
                Session["SessionNotes"] = value;
            }
        }

        public string SessionType
        {
            get
            {
                if (Session["SessionType"] == null)
                    Session["SessionType"] = string.Empty;

                return (string)Session["SessionType"];
            }
            set
            {
                Session["SessionType"] = value;
            }
        }

        public string SessionName
        {
            get
            {
                if (Session["SessionName"] == null)
                    Session["SessionName"] = string.Empty;

                return (string)Session["SessionName"];
            }
            set
            {
                Session["SessionName"] = value;
            }
        }

        public DateTime SessionExpiryDate
        {
            get
            {
                if (Session["SessionExpiryDate"] == null)
                    Session["SessionExpiryDate"] = DateTime.MinValue;

                return (DateTime)Session["SessionExpiryDate"];
            }
            set
            {
                Session["SessionExpiryDate"] = value;
            }
        }

        public List<KeyValuePair<DateTime, string>> OpenedStudents
        {
            get
            {
                return _sessionController.GetSessionOpenedStudents(EditSessionId.Value);
            }
        }

        public List<string> UnOpenedStudents
        {
            get
            {
                return _sessionController.GetSessionUnOpenedStudents(EditSessionId.Value);
            }
        }

        protected List<Books> Products
        {
            get
            {
                return _sessionController.GetBooks(SelectedSubscriptionId);
            }
        }

        protected List<SessionProducts> SessionBooks
        {
            get
            {
                return _sessionController.GetSessionProducts(EditSessionId.Value);
            }
        }


        #endregion

        protected string GenerateSessionName(List<SessionMembers> Sessionmembers, DateTime CurrentDateTime,string newName)
        {
            try
            {
                if (newName == string.Empty)
                {
                    String SessionName = string.Empty;

                    IList<SessionMembers> groupList = Sessionmembers.Where(u => u.MemberType == "GROUP").OrderBy(u1 => u1.GroupName).ToList();

                    IList<SessionMembers> studentList = Sessionmembers.Where(u => (u.MemberType == "USER") && (u.StudentName.Trim().Length > 0)).OrderBy(u1 => u1.StudentName).ToList();


                    if (groupList.Count > 0)
                    {
                        if (groupList.Count == 1)
                        {
                            SessionName = groupList[0].GroupName + " | " + CurrentDateTime.ToString("dd/MM/yyyy");
                            return SessionName;
                        }
                        else
                        {
                            SessionName = groupList[0].GroupName + "..." + " | " + CurrentDateTime.ToString("dd/MM/yyyy");
                            return SessionName;
                        }
                    }

                    if (groupList.Count == 0 && studentList.Count > 0)
                    {
                        if (studentList.Count == 1)
                        {
                            SessionName = studentList[0].StudentName + " | " + CurrentDateTime.ToString("dd/MM/yyyy");
                            return SessionName;
                        }
                        else
                        {
                            SessionName = studentList[0].StudentName + "..." + " | " + CurrentDateTime.ToString("dd/MM/yyyy");
                            return SessionName;
                        }
                    }
                    return SessionName;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return newName;
        }


        protected List<DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Messages> SessionMessages
        {
            get
            {
                try
                {
                    return _sessionController.GetErrorMessagesByModuleName(SessionsModule);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }
        }

        protected string GetMessage(string Message_Code)
        {
            try
            {
                DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Messages Error_Message = SessionMessages.Find(x => x.MessageCode.Equals(Message_Code));
                return Error_Message.MessageDesc;
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return string.Empty;
        }

        protected List<string> TeacherNamesLookUp
        {
            get
            {
                try
                {
                    return _sessionController.GetTeachersLookUp(SelectedSubscriptionId, LoginName);
                }
                catch (Exception ex){LogFileWrite(ex);}
                return null;
            }
        }

        public string LoginName
        {
            get
            {
                if (Session["UserName"] == null)
                {
                    Session["UserName"] = string.Empty;
                }

                return Convert.ToString(Session["UserName"]);
            }
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
                  //  SessionController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
                }
                finally
                {
                    if (streamWriter != null) streamWriter.Close();
                    if (fileStream != null) fileStream.Close();
                }
            }
        }      
    }
}
