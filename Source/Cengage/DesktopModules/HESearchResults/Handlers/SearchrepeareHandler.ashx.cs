using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using DotNetNuke.Modules.HESearchResults.Controls;
using DotNetNuke.Modules.HESearchResults.Components.Modal;


namespace DotNetNuke.Modules.HESearchResults.Handlers
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SearchrepeareHandler" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Used for binding the search result data
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class SearchrepeareHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// product result handler processrequest function
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {

            SearchEngine searchEngine = new SearchEngine();
            string json = string.Empty;
            HttpSessionState Session = HttpContext.Current.Session;
            Visitor UserDetailInfo = (Visitor)Session["UserInfo"];
            List<CengageSearchResult> list = new List<CengageSearchResult>();
            
            if (UserDetailInfo != null)
            {
                searchEngine.StoreSK = Convert.ToInt32(UserDetailInfo.StoreID);
                searchEngine.Division = context.Request.QueryString["division"].ToString();
                searchEngine.SearchText = context.Request.QueryString["q"].ToString();
                searchEngine.PageNumber = Convert.ToInt32(context.Request.QueryString["p"].ToString());
                searchEngine.NumberOfResults = 20;
                SearchEngineModel searchEngineModel = new SearchEngineModel();
                if (context.Request.QueryString["f"] != null)
                {
                    searchEngine.Facets = context.Request.QueryString["f"];
                }

                else
                {
                    //list = searchEngineModel.GetComponentResults(searchEngine);
                   
                }
                var prods = from item in list
                            group item by new { item.Isbn13, item.Title, item.Author } into tempprods

                            select new CengageSearchResult()
                            {
                                Isbn13 = tempprods.Key.Isbn13,
                                Title = tempprods.Key.Title.TrimStart(),
                                Author = tempprods.Key.Author,

                            };
                list = prods.ToList<CengageSearchResult>();
                
            }
            json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            context.Response.Write(json);
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