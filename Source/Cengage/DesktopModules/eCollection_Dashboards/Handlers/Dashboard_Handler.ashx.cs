using System.Web;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using System.Web.SessionState;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Data;
using Cengage.Ecommerce.SignUp.Data;

namespace DotNetNuke.Modules.eCollection_Dashboards.Handlers
{
    /// <summary>
    /// Summary description for Dashboard_Handler
    /// </summary>
    public class Dashboard_Handler :eCollection_DashboardsModuleBase, IHttpHandler,IRequiresSessionState
    {
        /// <summary>
        /// Processes all the request done from javascript
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string page = context.Request.QueryString["pageContent"].ToString();
                Subscription subscription = new Subscription();
                //if (context.Session["UserName"] != null)
                // {
                switch (page)
                {
                    case "alldates":
                        string json;
                        List<DateCollection> activityDates = new List<DateCollection>();
                        //  activityDates.Add(DashboardController.Instance.GetAllDates(new Subscription() { SubsId = int.Parse(context.Session["Subscription"].ToString()) })[int.Parse(context.Session["DateCount"].ToString())]);
                        context.Session["DateCount"] = int.Parse(context.Session["DateCount"].ToString()) + 1;
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(activityDates);
                        context.Response.Write(json);
                        break;
                    case "activity":
                        Subscription subsActivity = new Subscription()
                        {
                            SubsId = int.Parse(context.Session["Subscription"].ToString()),
                            AdminName = context.Session["UserName"].ToString(),
                            ActivityType = context.Request.QueryString["contenttype"],
                            ScrollCount = int.Parse(context.Request.QueryString["scrollcount"].ToString())
                        };
                        var innerCollection = new List<Activity>();
                        switch (context.Request.QueryString["contenttype"])
                        {
                            default:
                                innerCollection = DashboardController.Instance.GetDailyActivities(subsActivity);
                                break;
                            case "UPGRADEDDETAILS":
                                innerCollection = DashboardController.Instance.GetUpgradedActivities(subsActivity);
                                break;
                            case "LESSBOOKS":
                                subsActivity.ActivityType = "ADDEDBOOKS";
                                innerCollection = DashboardController.Instance.GetAllBooksAdded(subsActivity);
                                innerCollection = innerCollection.FindAll(x => x.DateCreated == context.Request.QueryString["ActionDate"].ToString().TrimStart('-', ' '));
                                innerCollection = innerCollection.GetRange(0, Math.Min(innerCollection.Count, 4));
                                break;
                            case "ALLBOOKS":
                                subsActivity.ActivityType = "ADDEDBOOKS";
                                innerCollection = DashboardController.Instance.GetAllBooksAdded(subsActivity);
                                break;
                        }
                        innerCollection = innerCollection.FindAll(x => x.DateCreated == context.Request.QueryString["ActionDate"].ToString().TrimStart('-', ' '));
                        innerCollection = innerCollection.Select(x =>
                        {
                            x.Name = x.Name.Length >= 10 ? string.Concat(x.Name.Substring(0, Math.Min(x.Name.Length, 9)), " ..") : x.Name;
                            return x;
                        }).ToList();
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(innerCollection);
                        context.Response.Write(json);
                        break;
                    case "close":
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(DashboardController.Instance.VideoGetStartedClose(new Users()
                        {
                            SubscriptionId = int.Parse(context.Session["Subscription"].ToString()),
                            VideoFlag = char.Parse(context.Request.QueryString["video"]),
                            GetStartedFlag = char.Parse(context.Request.QueryString["getstarted"]),
                            UserLoginName = context.Session["UserName"].ToString()
                        }));
                        context.Response.Write(json);
                        break;
                    case "subscriptions":
                        // string UserName = context.Session["UserName"].ToString();
                        var a = DashboardController.Instance.GetSubscriptionsList(new Users() { UserLoginName = context.Session["UserName"].ToString(), Active = 'Y' });//
                        var pageCount = context.Request.QueryString["pageCount"] == string.Empty ? 0 : int.Parse(context.Request.QueryString["pageCount"].ToString());
                        if (pageCount == -1)
                            json = Newtonsoft.Json.JsonConvert.SerializeObject(a.Rows.Count);
                        else
                        {
                            var ab = a.Select().ToList().Where(x => x.Field<string>("PartnerType") == context.Request.QueryString["PartnerType"]);
                            if (ab.Count() > 0)
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(ab.CopyToDataTable());
                            else
                                json = Newtonsoft.Json.JsonConvert.SerializeObject(ab);
                        }
                        context.Response.Write(json);
                        break;
                    case "addsubsname":
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(DashboardController.Instance.AddSubscriptionName(new Subscription() { SubsId = int.Parse(context.Request.QueryString["subsid"].ToString()), Name = context.Request.QueryString["newname"].ToString(), AdminName = context.Session["UserName"].ToString() }));
                        context.Response.Write(json);
                        break;
                    case "error":
                        string classjson = string.Empty;
                        RegBLL regbl = new RegBLL();
                        string resourceFile = "~/App_GlobalResources/Exceptions.resx";
                        string filePath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                        classjson = Newtonsoft.Json.JsonConvert.SerializeObject(regbl.ValidateUser(context.Request.QueryString["data"].ToString().Trim()));
                        context.Response.Write(classjson);
                        break;
                }
                //}
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