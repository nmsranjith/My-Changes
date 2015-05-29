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
using System.Collections;
using System;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using System.Collections.Generic;
using System.Data;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using System.IO;
using System.Configuration;
using System.Web;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Books
{

    /// <summary>
    /// This base class can be used to define custom properties for multiple controls. 
    /// An example module, DNNSimpleArticle (http://dnnsimplearticle.codeplex.com) uses this for an ArticleId
    /// 
    /// Because the class inherits from PortalModuleBase, properties like ModuleId, TabId, UserId, and others, 
    /// are accessible to your module's controls (that inherity from eCollection_BooksModuleBase
    /// 
    /// </summary>

    public class eCollection_BooksModuleBase : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected const string SessionsModule = "eCollection_Sessions";
        protected const string BooksModule = "eCollection Books";
        protected const string SessionParameter = "createsession";
        protected const string BOOKSELECTIONWIZARD = "BookSelectionWizard";
        protected const string SEESELECTEDBOOKS = "SeeSelectedBooks";
        protected const string DashboardsModule = "eCollection_Dashboards";
        protected const string BOOKLIST= "BookList";        

        public DataTable _subsList;
        BooksController _booksController = BooksController.Instance;
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

        public string SelectedReadingAge
        {
            get
            {
                if (Session["SelectedReadingAge"] == null)
                    Session["SelectedReadingAge"] = -1;

                return Session["SelectedReadingAge"].ToString();
            }
            set
            {
                Session["SelectedReadingAge"] = value;
            }
        }

        public string AttributeType
        {
            get
            {
                if (Session["AttributeType"] == null)
                    Session["AttributeType"] = -1;

                return Session["AttributeType"].ToString();
            }
            set
            {
                Session["AttributeType"] = value;
            }
        }

        public string AttributeValue
        {
            get
            {
                if (Session["AttributeValue"] == null)
                    Session["AttributeValue"] = -1;

                return Session["AttributeValue"].ToString();
            }
            set
            {
                Session["AttributeValue"] = value;
            }
        }

        public string TabName
        {
            get
            {
                if (Session["TabName"] == null)
                    Session["TabName"] = string.Empty;

                return Session["TabName"].ToString();
            }
            set
            {
                Session["TabName"] = value;
            }
        }

        protected List<Book> Products
        {
            get
            {
                try
                {
                    return _booksController.GetBooks(SelectedSubscriptionId, LoginName);
                }
                catch (Exception ex) //Module failed to load
                {                            
                    LogFileWrite(ex);
                }
                return null;
            }
        }

        protected Book GracePeriod
        {
            get
            {
                try
                {
                    return _booksController.GetBooksGracePeriod(SelectedSubscriptionId, LoginName);
                }
                catch (Exception ex) //Module failed to load
                {
                    LogFileWrite(ex);
                }
                return null;
            }
        }

        protected List<Book> ReadingAge
        {
            get
            {
                try
                {
                    return _booksController.GetReadingAge(SelectedSubscriptionId, SelectedReadingAge, LoginName);
                }
                catch (Exception ex) //Module failed to load
                {
                    LogFileWrite(ex);
                }
                return null;
            }
        }
        protected List<Book> ReadingAgeBooks
        {
            get
            {
                try
                {
                    return _booksController.GetReadingAgeBooks(SelectedSubscriptionId, AttributeType, AttributeValue);
                }
                catch (Exception ex) //Module failed to load
                {
                    LogFileWrite(ex);
                }
                return null;
            }
        }
        protected List<Book> Categories
        {
            get
            {
                try
                {
                    return _booksController.GetCategories(SelectedSubscriptionId, AttributeType, LoginName);
                }
                catch (Exception ex) //Module failed to load
                {
                    LogFileWrite(ex);
                }
                return null;
            }
        }

        protected List<Book> BooksByCategories
        {
            get
            {
                try
                {
                    return _booksController.GetBooksByCategories(AttributeType);
                }
                catch (Exception ex) //Module failed to load
                {
                    LogFileWrite(ex);
                }
                return null;
            }
        }

        protected List<Book> Levels
        {
            get
            {
                try
                {
                    return _booksController.GetLevels(SelectedSubscriptionId, AttributeType, AttributeValue, LoginName);
                }
                catch (Exception ex) //Module failed to load
                {
                    LogFileWrite(ex);
                }
                return null;
            }
        }

        protected bool BooksExceeded
        {
            get
            {
                if ((GracePeriod.TotalBooks - GracePeriod.SelectedBooks) <=0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productCount"></param>
        /// <returns></returns>
        protected bool isBooksExceeded(int productCount)
        {
            if ((GracePeriod.TotalBooks - GracePeriod.SelectedBooks) <= 0)
            {
                return true;
            }

            if (((GracePeriod.TotalBooks - GracePeriod.SelectedBooks) - productCount) < 0)
            {
                return true;
            }

            return false;
        }

        protected DataTable SubsList
        {
            get
            {
                try
                {
                    _subsList = DashboardController.Instance.GetSubscriptionsList(new Users() { UserLoginName = LoginName, Active = 'Y' });
                }
                catch (Exception ex) //Module failed to load
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

        protected List<DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Messages> SessionMessages
        {
            get
            {
                try
                {
                    return _booksController.GetErrorMessagesByModuleName(BooksModule);
                }
                catch (Exception ex) //Module failed to load
                {
                    LogFileWrite(ex);
                }
                return null;
            }
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
                DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Messages Error_Message = SessionMessages.Find(x => x.MessageCode.Equals(Message_Code));
                return Error_Message.MessageDesc;
            }
            catch (Exception ex) //Module failed to load
            {
                LogFileWrite(ex);
            }

            return string.Empty;
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

        /// <summary>
        ///  Logs the exception
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
                    BooksController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
                }
                finally
                {
                    if (streamWriter != null) streamWriter.Close();
                    if (fileStream != null) fileStream.Close();
                }
            }
        }

        /// <summary>
        ///  Writes values to log file for developer verification
        /// </summary>
        /// <param name="e"></param>
        public static void LogValues(string value)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "eCollection - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");

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
                    "\r\n ", value, "\r\n-----------------------------------------------------------------------------------------------------------------"));

                // TeacherController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }
    }
}
