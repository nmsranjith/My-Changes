using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using DotNetNuke.Modules.eCollection_Groups.Components;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;

namespace DotNetNuke.Modules.eCollection_Groups
{
    /// <summary>
    /// Summary description for GroupsHandler
    /// </summary>
    public class GroupsHandler : eCollection_GroupsModuleBase, IHttpHandler, IRequiresSessionState 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context  )
        {
            try
            {
                GroupController groupController = GroupController.Instance;
                string type = string.Empty;
                if (context.Request.QueryString["Search"] != null)
                {
                    type = context.Request.QueryString["Search"].ToString().Trim();
                }
                switch (type)
                {

                    case "Class":
                        string classjson = Newtonsoft.Json.JsonConvert.SerializeObject(groupController.GetGroupName(ClassType, Context.Session["UserName"].ToString()));
                        context.Response.Write(classjson);
                        break;
                    case "Group":
                        string groupjson = Newtonsoft.Json.JsonConvert.SerializeObject(groupController.GetGroupName(GroupType, Context.Session["UserName"].ToString()));
                        context.Response.Write(groupjson);
                        break;
                    case "GetClassBySubs":
                        string classNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(groupController.GetClassListBySubscription(Context.Session["Subscription"].ToString(), Context.Session["UserName"].ToString()));
                        context.Response.Write(classNamejson);
                        break;
                    case "groupAutoComplete":
                        List<string> searchList = groupController.GetGroupName(Context.Session["UserName"].ToString(), int.Parse(Context.Session["Subscription"].ToString()));
                        string groupNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(searchList);
                        context.Response.Write(groupNamejson);
                        break;
                    case "studentAutoComplete":
                        List<string> stdList = groupController.GetStudentsName(int.Parse(Context.Session["Subscription"].ToString()), Context.Session["UserName"].ToString());
                        string studentNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(stdList);
                        context.Response.Write(studentNamejson);
                        break;
                    case "teacherAutoComplete":
                        List<string> tchrList = groupController.GetTeachersName(int.Parse(Context.Session["Subscription"].ToString()), Context.Session["UserName"].ToString());
                        string teacherNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(tchrList);
                        context.Response.Write(teacherNamejson);
                        break;
                    case "groupStudentAutoComplete":
                        List<string> grpStdList = groupController.GetStudentNameInGroup(Context.Session["EditGroupID"] != null ? int.Parse(Context.Session["EditGroupID"].ToString()) : int.Parse(Context.Session["EditClassID"].ToString()), Context.Session["UserName"].ToString());
                        string grpStdNamejson = Newtonsoft.Json.JsonConvert.SerializeObject(grpStdList);
                        context.Response.Write(grpStdNamejson);
                        break;
                    case "isbnSearch":
                        List<string> srhStdList = groupController.GetBooksISBN();
                        string isbnjson = Newtonsoft.Json.JsonConvert.SerializeObject(srhStdList);
                        context.Response.Write(isbnjson);
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