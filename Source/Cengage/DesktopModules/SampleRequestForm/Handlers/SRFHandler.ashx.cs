using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using DotNetNuke.Modules.SampleRequestForm.Components.Controller;
using DotNetNuke.Modules.SampleRequestForm.Components.Modal;

namespace DotNetNuke.Modules.SampleRequestForm.Handlers
{
    /// <summary>
    /// Summary description for SRFHandler
    /// </summary>
    public class SRFHandler : SampleRequestFormModuleBase,IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string srfValue = context.Request.QueryString["log"];
                switch (srfValue)
                {
                    case "yes":
                        context.Response.ContentType = "text/plain";
                        LogContent log = Deserialize<LogContent>(context);
                        SampleRequestFormModuleBase.LogValues(log.Content);
                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(log.Content);
                        context.Response.Write(json);
                        break;
                    case "userexists":                        
                        int ResponceCode = 0;
                        SSOAPIController controller =  new SSOAPIController();
                        SRFParameters srfParams = new SRFParameters() {
                            Email = context.Request.QueryString["Email"],
                            SSOAuthKey = ConfigurationManager.AppSettings["SSOAuthKey"].ToString()
                        };
                        var userExists = controller.CheckUserExists(srfParams, out ResponceCode);
                        LogValues(string.Concat("\r\n Role -->",userExists["Role"].ToString(),"\r\n Response -->", userExists["Response"].ToString(), "\r\n ResultCode -->",userExists["ResultCode"].ToString()));
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(userExists["Role"].ToString());
                        context.Response.Write(json);
                        break;
                    default:
                        context.Session["SalesRep" + HttpContext.Current.Session.SessionID] = null;
                        context.Session["SalesRep" + HttpContext.Current.Session.SessionID] = string.Concat(context.Request.QueryString["SRepName"], "|", context.Request.QueryString["SRepEmail"]);
                        SampleRequestFormModuleBase.LogValues("\r\n Sales rep values handler: \r\n" + string.Concat("\r\n SRepName --> ", context.Request.QueryString["SRepName"], "|", "\r\n SRepEmail --> ", context.Request.QueryString["SRepEmail"]));
                        break;
                }
            }
            catch (Exception ex) { SampleRequestFormModuleBase.LogFileWrite(ex); }
        }

        /// <summary>
        /// This function will take httpcontext object and will read the input stream
        /// It will use the built in JavascriptSerializer framework to deserialize object based given T object type value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public T Deserialize<T>(HttpContext context)
        {
            object a = new LogContent();
            try
            {
                //read the json string
                string jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
                //cast to specified objectType
                var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);

                //return the object
                return obj;
            }
            catch (Exception ex) { SampleRequestFormModuleBase.LogFileWrite(ex); }
            return (T)a;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class LogContent
    {
        public string Content { get; set; }
    }
}