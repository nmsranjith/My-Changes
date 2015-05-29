using System.Web;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using System.Web.SessionState;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Students.Handlers
{
    /// <summary>
    /// Summary description for eCollectionHandler
    /// </summary>
    public class eCollectionHandler : eCollection_StudentsModuleBase, IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string page = context.Request.QueryString["autocomplete"].ToString();
                Student student = new Student();
                if (context.Session["UserName"] != null)
                {
                    student.TeacherLoginName = context.Session["UserName"].ToString();
                    student.Active = (char)MyEnums.Active.Yes;
                    student.SubscriptionId = int.Parse(context.Session["Subscription"].ToString());
                    switch (page)
                    {

                        case "studentsList":
                            string json;
                            switch (context.Request.QueryString["listType"].ToString())
                            {
                                default:
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.GetAll(student));
                                    context.Response.Write(json);
                                    break;
                                case "classmembers":
                                    if (context.Request.QueryString["classmembers"] != string.Empty)
                                    {
                                        student.ClassId = int.Parse(context.Request.QueryString["classmembers"]);
                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.GetClassMembers(student));
                                        context.Response.Write(json);
                                    }
                                    else
                                    {
                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.GetAll(student));
                                        context.Response.Write(json);
                                    }
                                    break;
                                case "studentssearch":
                                    string[] name = context.Request.QueryString["names"].Split(',');
                                    List<Student> stuList = new List<Student>();
                                    var mainList = studentController.GetAll(student);
                                    foreach (string s in name)
                                    {
                                        var text = s.Trim().ToLower();
                                        stuList.AddRange(mainList.Where(x => x.DisplayName.ToLower().Contains(text)));
                                    }
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(stuList.Distinct());
                                    context.Response.Write(json);
                                    break;
                            }
                            break;
                        case "fetchusername":
                            student.UserLoginName = context.Request.QueryString["names"];
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.CheckUserNameExists(student));
                            context.Response.Write(json);
                            break;
                        case "classmembers":
                            student.ClassId = int.Parse(context.Request.QueryString["classId"]);
                            var sortClassMembers = studentController.GetClassMembers(student);
                            switch (context.Request.QueryString["sortExp"])
                            {
                                case "ReadingLevel":
                                    sortClassMembers.Sort(Student => Student.CurrentReadingLevel);
                                    if (context.Request.QueryString["sortDir"] == "asc")
                                        sortClassMembers.Reverse();
                                    break;
                                default:
                                    sortClassMembers.Sort(Student => Student.FullName);
                                    if (context.Request.QueryString["sortDir"] == "desc")
                                        sortClassMembers.Reverse();
                                    break;
                            }
                            if (Null.SetNullString(context.Request.QueryString["names"]) != string.Empty || Null.SetNullString(context.Request.QueryString["Search"]) != string.Empty)
                            {
                                string[] name = context.Request.QueryString["names"].Split(',');
                                List<Student> stuList = new List<Student>();
                                name = name.Where(s => s != string.Empty).ToArray();
                                if (name.Length > 0)
                                {
                                    //  var mainList = StudentNameArrangement(context.Session["AddedStudentList"] as List<Student>);
                                    foreach (string s in name)
                                    {
                                        var text = s.Trim().ToLower();
                                        stuList.AddRange(sortClassMembers.Where(x => x.DisplayName.ToLower().Contains(text)));
                                    }
                                    switch (context.Request.QueryString["sortExp"])
                                    {
                                        case "ReadingLevel":
                                            stuList.Sort(Student => Student.CurrentReadingLevel);
                                            if (context.Request.QueryString["sortDir"] == "asc")
                                                stuList.Reverse();
                                            break;
                                        default:
                                            stuList.Sort(Student => Student.FullName);
                                            if (context.Request.QueryString["sortDir"] == "desc")
                                                stuList.Reverse();
                                            break;
                                    }
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(stuList.Distinct());

                                    if (Null.SetNullString(context.Request.QueryString["SearchCount"]) != string.Empty)
                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(Null.SetNullInteger(stuList.Distinct().Count()));
                                }
                                else
                                {
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(sortClassMembers);
                                    if (Null.SetNullString(context.Request.QueryString["SearchCount"]) != string.Empty)
                                        json = Newtonsoft.Json.JsonConvert.SerializeObject(Null.SetNullInteger(sortClassMembers.Count));
                                }
                            }
                            else
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(sortClassMembers);
                            context.Response.Write(json);
                            break;
                        case "seeallcreated":
                            switch (context.Request.QueryString["listType"].ToString())
                            {
                                default:
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(context.Session["AddedStudentList"] as List<Student>);
                                    context.Response.Write(json);
                                    break;
                                case "studentssearch":
                                    string[] name = context.Request.QueryString["names"].Split(',');
                                    List<Student> stuList = new List<Student>();
                                    var mainList = context.Session["AddedStudentList"] as List<Student>;
                                    foreach (string s in name)
                                    {
                                        var text = s.Trim().ToLower();
                                        stuList.AddRange(mainList.Where(x => x.LastName.Trim().ToLower() == text).Union(mainList.Where(x => x.FirstName.Trim().ToLower() == text)).Union(mainList.Where(x => x.FirstName.Trim().ToLower().Contains(text))).Union(mainList.Where(x => x.LastName.Trim().ToLower().Contains(text))).Union(mainList.Where(x => string.Concat(x.FirstName.Trim().ToLower(), ' ', x.LastName.Trim().ToLower()) == text)).Union(mainList.Where(x => x.FirstName.Trim().ToLower().StartsWith(text))).Union(mainList.Where(x => x.LastName.Trim().ToLower().StartsWith(text))).Union(mainList.Where(x => x.UserLoginName.Trim().ToLower().StartsWith(text))).Union(mainList.Where(x => x.UserLoginName.Trim().ToLower().Contains(text))).Union(mainList.Where(x => x.UserLoginName.Trim().ToLower() == text)).Union(mainList.Where(x => string.Concat(x.FirstName, ' ', x.LastName, " ( ", x.UserLoginName, " )").Trim().ToLower() == text)));
                                    }
                                    json = Newtonsoft.Json.JsonConvert.SerializeObject(stuList.Distinct());
                                    context.Response.Write(json);
                                    break;
                            }
                            break;
                        case "paging":
                            var pageno = int.Parse(context.Request.QueryString["pageno"]);
                            var pgitmscnt = int.Parse(context.Request.QueryString["pgitmscnt"]);
                            List<Student> pageStudents = null;
                            if (context.Request.QueryString["classmembers"] != null & context.Request.QueryString["classmembers"] != string.Empty)
                            {
                                student.ClassId = int.Parse(context.Request.QueryString["classmembers"]);
                                pageStudents = studentController.GetClassMembers(student);
                            }
                            else if (context.Request.QueryString["names"] != null & context.Request.QueryString["names"] != string.Empty)
                            {
                                string[] name = context.Request.QueryString["names"].Split(',');
                                pageStudents = new List<Student>();
                                var mainList = new List<Student>();
                                if (context.Request.QueryString["listpage"] != null && context.Request.QueryString["listpage"] == "seeall")
                                    mainList = context.Session["AddedStudentList"] as List<Student>;
                                else
                                    mainList = studentController.GetAll(student);
                                var text = string.Empty;
                                foreach (string s in name)
                                {
                                    text = s.Trim().ToLower();
                                    if (text != string.Empty)
                                    {
                                        pageStudents.AddRange(mainList.Where(x => x.LastName.Trim().ToLower() == text).Union(mainList.Where(x => x.FirstName.Trim().ToLower() == text)).Union(mainList.Where(x => x.FirstName.Trim().ToLower().Contains(text))).Union(mainList.Where(x => x.LastName.Trim().ToLower().Contains(text))).Union(mainList.Where(x => string.Concat(x.FirstName.Trim().ToLower(), ' ', x.LastName.Trim().ToLower()) == text)).Union(mainList.Where(x => x.FirstName.Trim().ToLower().StartsWith(text))).Union(mainList.Where(x => x.LastName.Trim().ToLower().StartsWith(text))).Union(mainList.Where(x => x.UserLoginName.Trim().ToLower().StartsWith(text))).Union(mainList.Where(x => x.UserLoginName.Trim().ToLower().Contains(text))).Union(mainList.Where(x => x.UserLoginName.Trim().ToLower() == text)).Union(mainList.Where(x => string.Concat(x.FirstName, ' ', x.LastName, " ( ", x.UserLoginName, " )").Trim().ToLower() == text)));
                                    }
                                }

                                pageStudents = pageStudents.Distinct().ToList();
                                pageStudents.Sort(Student => Student.FirstName);

                            }
                            else
                            {
                                pageStudents = studentController.GetAll(student);
                            }
                            if (pageno == -1)
                            {
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(pageStudents.Count);
                            }
                            else
                            {
                                pageStudents = pageStudents.GetRange((pageno - 1) * 10, Math.Min(10, (pageStudents.Count - (pageno - 1) * 10)));
                                //   pageStudents.Sort(Student => Student.CurrentReadingLevel);
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(pageStudents);
                            }
                            context.Response.Write(json);
                            break;
                        case "sorting":
                            student.Active = (char)MyEnums.Active.Yes;
                            var sortStudents = new List<Student>();
                            string[] stuIds = context.Request.QueryString["stuIds"].Split(' ');
                            var stu = new List<Student>();
                            if (Null.SetNullString(context.Request.QueryString["classId"]) != string.Empty)
                                stu = studentController.GetClassMembers(student);
                            else
                                stu = studentController.GetAll(student);
                            stuIds = stuIds.Where(s => s != string.Empty).ToArray();
                            foreach (string s in stuIds)
                                sortStudents.AddRange(stu.Where(x => s != string.Empty && x.StudentId == int.Parse(s)).ToList());
                            switch (context.Request.QueryString["sortExp"])
                            {
                                case "Names":
                                    sortStudents.Sort(Student => Student.FullName);
                                    if (context.Request.QueryString["sortDir"] == "desc")
                                        sortStudents.Reverse();
                                    break;
                                default:
                                    sortStudents.Sort(Student => Student.CurrentReadingLevel);
                                    if (context.Request.QueryString["sortDir"] == "asc")
                                        sortStudents.Reverse();
                                    break;
                            }

                            json = Newtonsoft.Json.JsonConvert.SerializeObject(sortStudents);
                            context.Response.Write(json);
                            break;
                        case "readingrecovery":
                            student.StudentId = int.Parse(context.Request.QueryString["studentid"]);
                            student.ReadingRecovery = context.Request.QueryString["readingrecovery"]!=null?char.Parse(context.Request.QueryString["readingrecovery"]):'N';
                            student.UpdateFlag = EnumHelper.GetDescription(MyEnums.Update.ReadingRecovery);
                            student.UserModified = context.Session["UserName"].ToString();
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.UpdateReadingRecovery(student));
                            context.Response.Write(json);
                            break;
                        case "esl":
                            student.StudentId = int.Parse(context.Request.QueryString["studentid"]);
                            student.ReadingRecovery = context.Request.QueryString["esl"]!=null?char.Parse(context.Request.QueryString["esl"]):'N';
                            student.UpdateFlag = EnumHelper.GetDescription(MyEnums.Update.ESL);
                            student.UserModified = context.Session["UserName"].ToString();
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.UpdateReadingRecovery(student));
                            context.Response.Write(json);
                            break;
                        case "readinglevel":
                            student.StudentId = int.Parse(context.Request.QueryString["studentid"]);
                            student.CurrentReadingLevel = int.Parse(context.Request.QueryString["readinglevel"]);
                            student.UserModified = context.Session["UserName"].ToString();
                            student.UpdateFlag = EnumHelper.GetDescription(MyEnums.Update.ReadingLevel);
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.UpdateReadingLevel(student));
                            context.Response.Write(json);
                            break;
                        case "bookreadinglevel":
                            student.StudentId = int.Parse(context.Request.QueryString["studentid"]);
                            string[] levels = context.Request.QueryString["bookreadinglevel"].Split('-');
                            student.CurrentReadinglevelFrom = int.Parse(levels[0]) + 1;
                            student.CurrentReadinglevelUpto = int.Parse(levels[1]);
                            student.UserModified = context.Session["UserName"].ToString();
                            student.UpdateFlag = EnumHelper.GetDescription(MyEnums.Update.BookReadingLevel);
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.UpdateBookReadingLevel(student));
                            context.Response.Write(json);
                            break;
                        case "subclasses":
                            student.UserLoginName = context.Session["UserName"].ToString();
                            student.GrpType = 'C';
                            student.ActionType = "FORSUBS";
                            student.GrpCacheName = "GetClassList";
                            student.SubscriptionId = int.Parse(context.Session["Subscription"].ToString());
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.GetGroupNames(student));
                            context.Response.Write(json);
                            break;
                        case "subgroups":
                            student.UserLoginName = context.Session["UserName"].ToString();
                            student.SubscriptionId = int.Parse(context.Session["Subscription"].ToString());
                            student.GrpCacheName = "GetGroupList";
                            student.ActionType = "FORSUBS";
                            student.GrpType = 'N';
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.GetGroupNames(student));
                            context.Response.Write(json);
                            break;
                        case "usergroups":
                            if (context.Request.QueryString["loginname"] != null)
                                student.UserLoginName = context.Request.QueryString["loginname"];
                            else
                                student.UserLoginName = context.Session["UserName"].ToString();
                            student.SubscriptionId = int.Parse(context.Session["Subscription"].ToString());
                            student.GrpCacheName = "GetGroupList";
                            student.ActionType = "FORSTUDENTS";
                            student.GrpType = 'N';
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.GetGroupNames(student));
                            context.Response.Write(json);
                            break;
                        case "userclass":
                            student.UserLoginName = context.Session["UserName"].ToString();
                            student.SubscriptionId = int.Parse(context.Session["Subscription"].ToString());
                            student.GrpCacheName = "GetGroupList";
                            student.ActionType = "FORTEACHERS";
                            student.GrpType = 'C';
                            var userclassLists = studentController.GetGroupNames(student);

                            if (context.Request.QueryString["firstclass"] == null)
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(userclassLists);
                            else
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(userclassLists.Count > 0 ? new int[] { userclassLists[0].GroupId, userclassLists[0].StudentsCount } : new int[] { 0, 0 });
                            context.Response.Write(json);
                            break;
                        case "groupsclasssearch":
                            student.UserLoginName = context.Session["UserName"].ToString();
                            student.SubscriptionId = int.Parse(context.Session["Subscription"].ToString());
                            student.GrpCacheName = "GetGroupList";
                            student.ActionType = "FORSUBS";
                            student.GrpType = 'N';
                            var grps = studentController.GetGroupNames(student);
                            //student.ActionType = "FORSUBS";
                            //student.GrpType = 'C';
                            //student.GrpCacheName = "GetClassList";
                            //var cls = studentController.GetGroupNames(student);
                            //grps.AddRange(cls);
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
                                case "PASSWORD_MANDATORY":
                                    msg = GetMessage(Constants.PASSWORD_MANDATORY);
                                    break;
                                case "USERNAME_MANDATORY":
                                    msg = GetMessage(Constants.USERNAME_MANDATORY);
                                    break;
                                case "FIRSTNAME_MANDATORY_EDIT":
                                    msg = GetMessage(Constants.FIRSTNAME_MANDATORY_EDIT);
                                    break;
                                case "LASTNAME_MANDATORY_EDIT":
                                    msg = GetMessage(Constants.LASTNAME_MANDATORY_EDIT);
                                    break;
                                case "PASSWORD_MANDATORY_EDIT":
                                    msg = GetMessage(Constants.PASSWORD_MANDATORY_EDIT);
                                    break;
                                case "VALIDATE_FIRSTNAME":
                                    msg = GetMessage(Constants.VALIDATE_FIRSTNAME);
                                    break;
                                case "VALIDATE_LASTNAME":
                                    msg = GetMessage(Constants.VALIDATE_LASTNAME);
                                    break;
                                case "VALIDATE_USERNAME1":
                                    msg = GetMessage(Constants.VALIDATE_USERNAME1);
                                    break;
                                case "VALIDATE_USERNAME2":
                                    msg = GetMessage(Constants.VALIDATE_USERNAME2);
                                    break;
                                case "VALIDATE_USERNAME3":
                                    msg = GetMessage(Constants.VALIDATE_USERNAME3);
                                    break;
                                case "VALIDATE_PASSWORD1":
                                    msg = GetMessage(Constants.VALIDATE_PASSWORD1);
                                    break;
                                case "VALIDATE_PASSWORD2":
                                    msg = GetMessage(Constants.VALIDATE_PASSWORD2);
                                    break;
                                case "VALIDATE_EMAIL":
                                    msg = GetMessage(Constants.VALIDATE_EMAIL);
                                    break;
                                case "VALIDATE_DOB1":
                                    msg = GetMessage(Constants.VALIDATE_DOB1);
                                    break;
                                case "VALIDATE_DOB2":
                                    msg = GetMessage(Constants.VALIDATE_DOB2);
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
                            student.MonthId = DateTime.Now.Month;
                            switch (Null.SetNullString(context.Request.QueryString["duration"]))
                            {
                                case "today":
                                    student.ActionType = "WEEKDAYS";
                                    student.DateModified = DateTime.Now;
                                    break;
                                case "yesterday":
                                    student.ActionType = "WEEKDAYS";
                                    student.DateModified = DateTime.Now.AddDays(-1);
                                    break;
                                case "lastweek":
                                    student.ActionType = "LASTWEEK";
                                    student.DateModified = DateTime.Now.AddDays(-2);
                                    break;
                                case "restofthemonth":
                                    student.ActionType = "RESTOFMONTH";
                                    student.DateModified = DateTime.Now;
                                    break;                                    
                                default: 
                                    student.ActionType = "MONTHWISE";
                                    student.DateModified = DateTime.Now;
                                    student.MonthId = Null.SetNullInteger(context.Request.QueryString["Month"]);
                                    break;

                            }
                            student.StudentId = Null.SetNullInteger(context.Request.QueryString["studentId"]);
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.ClearFromiPad(student));
                            context.Response.Write(json);
                            break;
                        case "checkclearfromiPad":
                            student.StudentId = Null.SetNullInteger(context.Request.QueryString["studentId"]);
                            var isClearedDetail = studentController.CheckClearedFromiPad(student).Find(x => x.Text == context.Request.QueryString["Duration"]);
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(isClearedDetail != null ? isClearedDetail.Id : 0);
                            context.Response.Write(json);
                            break;
                        case "addremove":
                            student.StudentId = Null.SetNullInteger(context.Request.QueryString["studentsk"]);
                            student.ActionType = Null.SetNullString(context.Request.QueryString["type"]);
                            var status = studentController.UpdateStudentLicenseAllocation(student);
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(status);
                            context.Response.Write(json);
                            break;
                        case "switchsubscription":
                            student.StudentId = Null.SetNullInteger(context.Request.QueryString["studentsk"]);
                            student.NewSubsSk = Null.SetNullInteger(context.Request.QueryString["newsubssk"]);
                            context.Session["SName" + HttpContext.Current.Session.SessionID] = Null.SetNullString(context.Request.QueryString["SName"]);

                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentController.SwitchStudentSubcriptions(student));
                            context.Response.Write(json);
                            break;
                        case "getunallocatedstudents":
                            student.SearchText = Null.SetNullString(context.Request.QueryString["searchtext"]);
                            var reader = studentController.GetAllUnallocatedStudents(student);
                            List<string> studentNames = new List<string>();
                            while (reader.Read())
                            {
                                studentNames.Add(reader["USER_LOGIN_NAME"].ToString());
                            }
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(studentNames.ToArray());
                            context.Response.Write(json);
                            break;
                        default:
                            var stuNames = new List<Student>();
                            if (context.Request.QueryString["listpage"] != null && context.Request.QueryString["listpage"] == "seeall")
                                stuNames = context.Session["AddedStudentList"] as List<Student>;
                            else
                            {
                                List<Groups> classnames = studentController.GetGroupNames(new Student()
                                {
                                    UserLoginName = student.TeacherLoginName,
                                    SubscriptionId = Null.SetNullInteger(context.Session["Subscription"]),
                                    GrpCacheName = "GetGroupList",
                                    ActionType = "FORTEACHERS",
                                    GrpType = 'C'
                                });
                                foreach (Groups group in classnames)
                                {
                                    student.ClassId = group.GroupId;
                                    stuNames.AddRange(studentController.GetClassMembers(student));
                                }
                            }
                            string[] names = stuNames.Select(e => string.Concat(e.FirstName, ' ', e.LastName)).Distinct().ToArray();//, " (", e.UserLoginName, ")")).ToArray();
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

        //private List<Student> StudentNameArrangement(List<Student> stuList)
        //{
        //    try
        //    {
        //        var fullName = string.Empty;
        //        var loginName = string.Empty;
        //        foreach (Student stu in stuList)
        //        {
        //            fullName = string.Concat(stu.FirstName, ' ', stu.LastName);
        //            loginName = stu.UserLoginName.Length > 15 ? string.Concat(stu.UserLoginName.Substring(0, 15), " ..") : stu.UserLoginName;
        //            stu.FullName = (stu.FullNameFlag == null) ? string.Concat(fullName, loginName).Length > 27 ? (string.Concat(fullName.Substring(0, Math.Min(fullName.Length, 23)), " ..", " (", loginName, ")")) : string.Concat(fullName, " (", loginName, ")") : stu.FullName;
        //            stu.FullNameFlag = "FIXED";
        //        }
        //        stuList.Sort(Student => Student.FirstName);
        //    }
        //    catch (Exception ex) { LogFileWrite(ex); }
        //    return stuList;
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}