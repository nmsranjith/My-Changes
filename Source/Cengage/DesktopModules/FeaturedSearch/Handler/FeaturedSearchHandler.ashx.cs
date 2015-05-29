using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.FeaturedSearch.Components.Controller;
using DotNetNuke.Modules.FeaturedSearch.Components.Modal;

namespace DotNetNuke.Modules.FeaturedSearch.Handler
{
    /// <summary>
    /// Summary description for FeaturedSearchHandler
    /// </summary>
    public class FeaturedSearchHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
             string cases = Null.SetNullString(context.Request.QueryString["section"]);
                Visitor visitor = new Visitor();
                if (context.Session["UserInfo"] != null)
                    visitor = (Visitor)(context.Session["UserInfo"]);
                switch (cases)
                {
                    case "setsearchvalue":
                        string json;
                        FeaturedSearches featuredSearch = new FeaturedSearches();
                        context.Response.ContentType = "text/plain";
                        featuredSearch = Deserialize<FeaturedSearches>(context);
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(FeaturedSearchController.Instance.SaveFeaturedSearch(featuredSearch));
                        context.Response.Write(json);   
                        break;
                    case "deletesearch":
                        featuredSearch = new FeaturedSearches();
                        context.Response.ContentType = "text/plain";
                        featuredSearch = Deserialize<FeaturedSearches>(context);
                        json = Newtonsoft.Json.JsonConvert.SerializeObject(FeaturedSearchController.Instance.DeleteFeaturedSearch(featuredSearch));
                        context.Response.Write(json);                          
                        break;
                    default:
                        break;
                }
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
            //read the json string
            string jsonData = new StreamReader(context.Request.InputStream).ReadToEnd();
            //cast to specified objectType
            var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);

            //return the object
            return obj;
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