using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using System.Web.SessionState;

namespace DotNetNuke.Modules.eCollection_Sessions
{
    /// <summary>
    /// Summary description for SessionHandler
    /// </summary>
    public class SessionHandler : eCollection_SessionsModuleBase, IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                SessionController sessionController = SessionController.Instance;
                string type = string.Empty;

                if (context.Request.QueryString["SessionStatus"] != null)
                {
                    type = context.Request.QueryString["SessionStatus"].ToString().Trim();
                }
                switch (type)
                {

                    case "Active":
                        string ActiveSession = Newtonsoft.Json.JsonConvert.SerializeObject(ActiveSessions);
                        context.Response.Write(ActiveSession);
                        break;
                    case "Finished":
                        string FinishedSession = Newtonsoft.Json.JsonConvert.SerializeObject(FinishedSessions);
                        context.Response.Write(FinishedSession);
                        break;
                    case "Archived":
                        string ArchivedSession = Newtonsoft.Json.JsonConvert.SerializeObject(ArchivedSessions);
                        context.Response.Write(ArchivedSession);
                        break;
                    case "EndSession":
                        DateTime sessionExpiryDate = DateTime.ParseExact(context.Request.QueryString["EndDate"].ToString().Replace('-','/'), "dd/MM/yyyy", null);
                        SessionController _sessionController = SessionController.Instance;
                        if (context.Session["EditSelectedId"] != null)
                            _sessionController.UpdateSessionExpiryDateAlone(Convert.ToInt32(context.Session["EditSelectedId"].ToString()), sessionExpiryDate, context.Session["UserName"].ToString());
                        break;
                    case "GetClassBySubs":
                        if (context.Session["SelectedSubscriptionId"] != null)
                        {
                            string classNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(sessionController.GetClassListBySubscription(context.Session["Subscription"].ToString(), context.Session["UserName"].ToString()));
                            context.Response.Write(classNamejson);
                        }
                        break;
                    case "GetGroupsBySubs":
                        if (context.Session["SelectedSubscriptionId"] != null)
                        {
                            string classNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(sessionController.GetGroupListBySubscription(context.Session["Subscription"].ToString(), context.Session["UserName"].ToString()));
                            context.Response.Write(classNamejson);
                        }
                        break;
                    case "groupAutoComplete":

                        List<string> sessList = sessionController.GetAllLookUpNames(0, context.Session["UserName"].ToString(), int.Parse(context.Session["Subscription"].ToString()));
                        string sessionNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(sessList);
                        context.Response.Write(sessionNamejson);
                        break;
                    case "studentAutoComplete":
                        List<string> stdList = sessionController.GetLookupStudentsName(int.Parse(Context.Session["Subscription"].ToString()), context.Session["UserName"].ToString());
                        string studentNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(stdList);
                        context.Response.Write(studentNamejson);
                        break;
                    case "groupsAutoComplete":
                        List<string> searchList = sessionController.GetGroupName(context.Session["UserName"].ToString(), int.Parse(Context.Session["Subscription"].ToString()));
                        string groupNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(searchList);
                        context.Response.Write(groupNamejson);
                        break;
                    case "teacherAutoComplete":
                        List<string> teacherList = sessionController.GetTeachersLookUp(int.Parse(Context.Session["Subscription"].ToString()), context.Session["UserName"].ToString());
                        string teacherNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(teacherList);
                        context.Response.Write(teacherNamejson);
                        break;
                    case "booksAutoComplete":
                        List<string> bookList = sessionController.GetBooksLookUp(int.Parse(Context.Session["Subscription"].ToString()), context.Session["UserName"].ToString(), Context.Session["BooksCategories"].ToString());
                        string booksNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(bookList);
                        context.Response.Write(booksNamejson);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
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