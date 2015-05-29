using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.SavedSearch.Conrollers;
using System.Data.SqlClient;
using DotNetNuke.Instrumentation;
using System.Web.SessionState;
using System.Data;

namespace DotNetNuke.Modules.SavedSearch.Handlers
{
    /// <summary>
    /// Summary description for AlertDeleteHandler
    /// </summary>
    public class AlertDeleteHandler : SavedSearchModuleBase, IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                Visitor visitr = new Visitor();
                if (context.Session["UserInfo"] != null)
                    visitr = context.Session["UserInfo"] as Visitor;
                else
                    visitr = new Visitor();
                string savedsearchid = context.Request.QueryString["savedsearchid"].ToString();
                string count = context.Request.QueryString["count"].ToString();
                string PageNameValue = string.Empty;//context.Request.QueryString["page"].ToString();
                string PageName = context.Request.QueryString["page"].ToString();
                if (PageName.StartsWith("higher"))
                {
                    PageNameValue = "higher";
                }
                else if (PageName.StartsWith("vpg"))
                {
                    PageNameValue = "vocational";
                }
                else if (PageName.StartsWith("gale"))
                {
                    PageNameValue = "gale";
                }
                else
                {
                    PageNameValue = "";
                }
                int result = SavedSearchController.Instance.DeleteSavedSearchName(int.Parse(savedsearchid));
                DnnLog.Info("savedsearchhandler1?Count:" + count + ";" + "userid:" + visitr.UserID + "PageNameValue:" + PageNameValue);
                DataTable dt = new DataTable();
                SqlDataReader reader;
                if (result > 0)
                {
                    if (count != "")
                    {
                         reader = SavedSearchController.Instance.GetSavedSearchName(count, visitr.UserID, PageNameValue);
                         DnnLog.Info("data... " + SavedSearchController.Instance.GetSavedSearchName(count, visitr.UserID, PageNameValue).HasRows);
                        dt.Load(reader);
                        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(dt));
                        DnnLog.Info("savedsearchhandler1... " + dt.Rows.Count);
                    }
                    else
                    {
                        DnnLog.Info("savedsearchhandler1" + result);
                        context.Response.Write(result);
                    }

                }

            }
            catch (Exception e)
            {
                DnnLog.Info("savedsearchhandler"+e);
                    
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