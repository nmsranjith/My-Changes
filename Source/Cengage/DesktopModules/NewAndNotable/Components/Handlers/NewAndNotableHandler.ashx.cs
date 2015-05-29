using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using DotNetNuke.Instrumentation;
using DotNetNuke.Modules.NewAndNotable.Components.Controller;
using DotNetNuke.Modules.NewAndNotable.Components.Model;

namespace DotNetNuke.Modules.NewAndNotable.Components.Handlers
{
    /// <summary>
    /// Summary description for NewAndNotableHandler
    /// </summary>
    public class NewAndNotableHandler : IRequiresSessionState, IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            NewModel newModel = null;
            string classjson = string.Empty;
            Visitor visitor = null;
            try
            {
                visitor = (context.Session["UserInfo"] != null) 
                            ? context.Session["UserInfo"] as Visitor
                            : new Visitor();
                 newModel = new NewModel();
                 newModel.Country = visitor.CountryCode;
                 if (context.Request.QueryString["isbn"] != null)
                 {
                     newModel.Isbn_13 = context.Request.QueryString["isbn"];
                 }
                 if (context.Request.QueryString["division"] != null)
                 {
                     switch (context.Request.QueryString["division"].ToLower())
                     {
                         case "higher":
                             newModel.Division = "HE";
                             break;
                         case "vpg":
                             newModel.Division = "VPG";
                             break;
                         case "gale":
                             newModel.Division = "GALE";
                             break;
                         case "primary":
                             newModel.Division = "PRI";
                             break;
                         case "secondary":
                             newModel.Division = "SEC";
                             break;
                     }
                 }
                classjson = Newtonsoft.Json.JsonConvert.SerializeObject(NewAndNotableController.Instance.IsISBNValid(newModel));
                context.Response.Write(classjson);
            }
            catch (Exception ex)
            {
                DnnLog.Error("error in validating ISBN... " + ex.Message);
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