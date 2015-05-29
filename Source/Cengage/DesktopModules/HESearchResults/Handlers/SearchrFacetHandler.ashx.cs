using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Cengage.eCommerce.Lib;


namespace DotNetNuke.Modules.HESearchResults.Handlers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SearchrFacetHandler" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Used for searchresult facet.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class SearchrFacetHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// product result handler processrequest function
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            
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