using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using DotNetNuke.Common.Utilities;
namespace DotNetNuke.Modules.eCollection_Teachers.Handlers
{
    /// <summary>
    /// Summary description for eCollectionHandler
    /// </summary>
    public class eCollectionHandler : eCollection_TeachersModuleBase, IHttpHandler, IRequiresSessionState 
    {
        /// <summary>
        /// /
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string page = context.Request.QueryString["autocomplete"].ToString();
                Teacher teachers = new Teacher();
                if (context.Session["UserName"] != null)
                {
                    teachers.TeacherLoginName = context.Session["UserName"].ToString();
                    teachers.Active = (char)MyEnums.Active.Yes;
                    teachers.SubscriptionId = int.Parse(context.Session["Subscription"].ToString());
                    switch (page)
                    {
                        case "teacherslist":
                            string json;
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(teacherController.GetAll(teachers));
                            context.Response.Write(json);
                            break;
                        case "teacherssearch":
                            string[] name = context.Request.QueryString["names"].Split(',');
                            List<Teacher> teacherList = new List<Teacher>();
                            var mainList = teacherController.GetAll(teachers);
                            foreach (string s in name)
                            {
                                var text = s.Trim().ToLower();
                                if (text != string.Empty)
                                {
                                    teacherList.AddRange(mainList.Where(x => x.DisplayName.ToLower().Contains(text)));
                                }
                            }
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(teacherList.Distinct());
                            context.Response.Write(json);
                            break;
                        case "paging":
                            var pageno = int.Parse(context.Request.QueryString["pageno"]);
                            var pgitmscnt = int.Parse(context.Request.QueryString["pgitmscnt"]);
                            List<Teacher> pageTeachers = new List<Teacher>();
                            mainList = teacherController.GetAll(teachers);
                            if (context.Request.QueryString["names"] != null & context.Request.QueryString["names"] != string.Empty)
                            {
                                name = context.Request.QueryString["names"].Split(',');
                                foreach (string s in name)
                                {
                                    var text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        pageTeachers.AddRange(mainList.Where(x => x.DisplayName.ToLower().Contains(text)));
                                    }
                                }

                                pageTeachers = pageTeachers.Distinct().ToList();
                                pageTeachers.Sort(Student => Student.FullName);

                            }
                            else
                            {
                                pageTeachers = mainList;
                            }
                            if (pageno == -1)
                            {
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(pageTeachers.Count);
                            }
                            else
                            {
                                pageTeachers = pageTeachers.GetRange((pageno - 1) * 10, Math.Min(10, (pageTeachers.Count - (pageno - 1) * 10)));
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(pageTeachers);
                            }
                            context.Response.Write(json);
                            break;
                        case "sorting":
                            var sortTeachers = new List<Teacher>();
                            string[] teacherIds = context.Request.QueryString["stuIds"].Split(' ');
                            var stu = teacherController.GetAll(teachers);
                            teacherIds = teacherIds.Where(s => s != string.Empty).ToArray();
                            foreach (string s in teacherIds)
                                sortTeachers.AddRange(stu.Where(x => s != string.Empty && x.TeacherId == int.Parse(s)).ToList());
                            sortTeachers.Sort(teacher => teacher.FullName);
                            if (context.Request.QueryString["sortDir"] == "desc")
                                sortTeachers.Reverse();
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(sortTeachers);
                            context.Response.Write(json);
                            break;
                        case "bookreadinglevel":
                            teachers = new Teacher();
                            teachers.TeacherId = int.Parse(context.Request.QueryString["Teacherid"]);
                            string[] levels = context.Request.QueryString["bookreadinglevel"].Split('-');
                            teachers.BookReadLevelFrom = int.Parse(levels[0]) + 1;
                            teachers.BookReadLevelUpto = int.Parse(levels[1]);
                            teachers.UserModified = context.Session["UserName"].ToString();
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(teacherController.UpdateBookReadLevel(teachers));
                            context.Response.Write(json);
                            break;
                        case "usergroups":
                            if (context.Request.QueryString["loginname"] != null)
                                teachers.UserLoginName = context.Request.QueryString["loginname"];
                            else
                                teachers.UserLoginName = context.Session["UserName"].ToString();
                            teachers.SubscriptionId = int.Parse(context.Session["Subscription"].ToString());
                            teachers.GrpCacheName = "GetGroupList";
                            teachers.ActionType = "FORTEACHERS";
                            teachers.GrpType = 'N';
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(teacherController.GetGroupNames(teachers));
                            context.Response.Write(json);
                            break;
                        case "groupsclasssearch":
                            teachers.UserLoginName = context.Session["UserName"].ToString();
                            teachers.SubscriptionId = int.Parse(context.Session["Subscription"].ToString());
                            teachers.GrpCacheName = "GetGroupList";
                            teachers.ActionType = "FORSUBS";
                            teachers.GrpType = 'N';
                            var grps = teacherController.GetGroupNames(teachers);
                            teachers.ActionType = "FORSUBS";
                            teachers.GrpType = 'C';
                            teachers.GrpCacheName = "GetClassList";
                            var cls = teacherController.GetGroupNames(teachers);
                            grps.AddRange(cls);
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(grps.Distinct());
                            context.Response.Write(json);
                            break;
                        case "getmessages":
                            var msg = string.Empty;
                            switch (context.Request.QueryString["msgcode"])
                            {
                                case "FIRSTNAME_MANDATORY":
                                    msg = GetMessage(Constants.FIRSTNAME_MANDATORY);
                                    break;
                                case "LASTNAME_MANDATORY":
                                    msg = GetMessage(Constants.LASTNAME_MANDATORY);
                                    break;
                                case "EMAIL_MANDATORY":
                                    msg = GetMessage(Constants.EMAIL_MANDATORY);
                                    break;
                                case "VALIDATE_FIRSTNAME":
                                    msg = GetMessage(Constants.VALIDATE_FIRSTNAME);
                                    break;
                                case "VALIDATE_LASTNAME":
                                    msg = GetMessage(Constants.VALIDATE_LASTNAME);
                                    break;
                                case "VALIDATE_EMAIL":
                                    msg = GetMessage(Constants.VALIDATE_EMAIL);
                                    break;
                                case "VALIDATE_BROWSETXTBX":
                                    msg = GetMessage(Constants.VALIDATE_BROWSETXTBX);
                                    break;
                                case "GROUPS_SELECTED":
                                    msg = GetMessage(Constants.GROUPS_SELECTED);
                                    break;
                            }
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                            context.Response.Write(json);
                            break;
                        case "clearfromiPad":
                            teachers.MonthId = DateTime.Now.Month;
                            switch (Null.SetNullString(context.Request.QueryString["duration"]))
                            {
                                case "today":
                                    teachers.ActionType = "WEEKDAYS";
                                    teachers.DateModified = DateTime.Now;
                                    break;
                                case "yesterday":
                                    teachers.ActionType = "WEEKDAYS";
                                    teachers.DateModified = DateTime.Now.AddDays(-1);
                                    break;
                                case "lastweek":
                                    teachers.ActionType = "LASTWEEK";
                                    teachers.DateModified = DateTime.Now.AddDays(-2);
                                    break;
                                case "restofthemonth":
                                    teachers.ActionType = "RESTOFMONTH";
                                    teachers.DateModified = DateTime.Now;
                                    break;
                                case "monthwise":
                                    teachers.ActionType = "MONTHWISE";
                                    teachers.DateModified = DateTime.Now;
                                    teachers.MonthId = Null.SetNullInteger(context.Request.QueryString["Month"]);
                                    break;
                                default: break;

                            }
                            teachers.TeacherId = Null.SetNullInteger(context.Request.QueryString["teacherId"]);
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(teacherController.ClearFromiPad(teachers));
                            context.Response.Write(json);
                            break;
                        case "checkclearfromiPad":
                            teachers.TeacherId = Null.SetNullInteger(context.Request.QueryString["teacherId"]);
                            var isClearedDetail = teacherController.CheckClearedFromiPad(teachers).Find(x => x.Text == context.Request.QueryString["Duration"]);
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(isClearedDetail != null ? isClearedDetail.Id : 0);
                            context.Response.Write(json);
                            break;
                        default:
                            var teacherNames = teacherController.GetAll(teachers);
                            string[] names = teacherNames.Select(e => string.Concat(e.FirstName, ' ', e.LastName)).ToArray();
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(names);
                            context.Response.Write(json);
                            break;
                    }
                }
                else
                {
                    context.Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
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