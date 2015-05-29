using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.InformationBanner.Components.Controllers;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.InformationBanner.Handlers
{
    /// <summary>
    /// Summary description for AlertDeleteHandler
    /// </summary>
    public class AlertDeleteHandler : InformationBannerModuleBase, IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string alertid = context.Request.QueryString["alertid"].ToString();
                string username = context.Request.QueryString["username"].ToString();
                string pageUrl = context.Request.QueryString["pageUrl"].ToString();
                int result = InformationBannerController.Instance.DeleteUserInfoID(int.Parse(alertid), username, pageUrl);

                context.Response.Write(result);
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