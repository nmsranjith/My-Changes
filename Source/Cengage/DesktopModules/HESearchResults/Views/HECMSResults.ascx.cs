using System;
using System.Configuration;
using System.Web.Services;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.HESearchResults.Components.Controller;
using DotNetNuke.Modules.HESearchResults.Components.Modal;
using System.Collections.Generic;

namespace DotNetNuke.Modules.HESearchResults.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="HECMSResults" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Provides the CMS page results.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------

    public partial class HECMSResults : HESearchResultsModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Visitor visitor = new Visitor();
                int totalpage = 0, itemsCount = 0;
                if (Session["UserInfo"] != null)
                    visitor = (Visitor)(Session["UserInfo"]);
                SearchParameters sParams = new SearchParameters()
                {
                    SearchText = Request.QueryString["q"],
                    Division = Request.QueryString["division"],
                    NumberOfResults = Null.SetNullInteger(ConfigurationManager.AppSettings["NO_OF_RECORDS_PER_PAGE"]),
                    PageNumber = string.IsNullOrEmpty(Request.QueryString["cp"]) ? 1 : int.Parse(Request.QueryString["cp"]),
                    StoreSK = Null.SetNullInteger(visitor.StoreID)
                };
                var siteList = HESearchResultController.Instance.GetSiteResults(sParams);
                List<SearchParameters> cmsResults = new List<SearchParameters>();
                while (siteList.Read())
                {
                    cmsResults.Add(new SearchParameters()
                    {
                        TabId=Null.SetNullInteger(siteList["TABID"]),
                        TabUrl = Globals.NavigateURL(TabId),
                        TabName = Null.SetNullString(siteList["TABNAME"]),
                        TabDescription = Null.SetNullString(siteList["DESCRIPTION"])
                    });
                }
                siteList.NextResult();
                while (siteList.Read())
                    itemsCount=Null.SetNullInteger(siteList["totalrows"]);
                //string ss = string.Empty;
                //foreach (SearchParameters s in cmsResults)
                //{
                //    ss = string.Concat(ss, "\r\n TabId-->", s.TabId, ", \r\n TabUrl-->", s.TabUrl, ", \r\n TabName-->", s.TabName, ", \r\n TabDescription-->", s.TabDescription, "\r\n");
                //}                
                //LogValues(ss);
                
                CMSSiteResultsRptr.DataSource = cmsResults;
                CMSSiteResultsRptr.DataBind();
               
                // CMSSiteResultsRptr.Items.Count;
                string countCheck = string.Empty;
               // LogValues(string.Concat("\r\n Initial Count --> ",itemsCount.ToString()));
                if (itemsCount <= 0 || Request.QueryString["division"] == string.Empty)
                {
                  //  LogValues("LoadNoResult");
                    LoadNoResult();
                }
                else
                {
                    int startCount = 0, totalcount = 0;
                   if (sParams.PageNumber != 0)
                    {
                        totalpage = Null.SetNullInteger(Math.Ceiling(Convert.ToDouble(Convert.ToDecimal(itemsCount) / sParams.NumberOfResults)));// itemsCount % sParams.NumberOfResults == 0 ? itemsCount / sParams.NumberOfResults : itemsCount / sParams.NumberOfResults + 1;
                        if (totalpage > sParams.PageNumber + 5)
                        {
                            if (sParams.PageNumber - 6 >= 0)
                                startCount = sParams.PageNumber - 6;
                            else
                                startCount = 0;
                        }
                        else
                        {
                            if (totalpage > 10 && totalpage - sParams.PageNumber <= 6)
                                startCount = totalpage - 10;
                            else if (totalpage > 10)
                                startCount = sParams.PageNumber - 10;
                            else
                                startCount = 0;
                        }
                    }
                    else
                    {
                        startCount = 0;
                    }

                    CurrentPgStartSizeLbl.Text = ((sParams.PageNumber - 1) * sParams.NumberOfResults + 1).ToString();
                    CurrentPgEndSizeLbl.Text = Math.Min((sParams.PageNumber * sParams.NumberOfResults), itemsCount).ToString();
                    TotalResultLbl.Text = Null.SetNullString(itemsCount);
                    DivisionLbl.Text = Null.SetNullString("Higher Education");

                   // countCheck = string.Concat(countCheck, "\r\n PageNumber --> ", sParams.PageNumber.ToString());
                  //  countCheck = string.Concat(countCheck, "\r\n totalpage --> ", totalpage.ToString());
                   // countCheck = string.Concat(countCheck, "\r\n NumberOfResults --> ", sParams.NumberOfResults.ToString());
                   // countCheck = string.Concat(countCheck, "\r\n startCount --> ", startCount.ToString());
                    LogValues(countCheck);

                    if (totalpage > 1)
                    {
                        
                        SetPagingDefaults(itemsCount, sParams.NumberOfResults, startCount);
                       HECMSPager.Visible = true;
                    }
                    else
                        HECMSPager.Visible = false;
                }

            }
            catch (Exception exc) //Module failed to load
            {
                LogFileWrite(exc);
                LoadNoResult();
            }
        }


        /// <summary>
        /// Loads No result page
        /// </summary>
        private void LoadNoResult()
        {
            HECMSPager.Visible = false;
            ResultCountPlaceHldr.Visible = false;
            NoResultPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/HENoResult.ascx"));
        }

        /// <summary>
        /// Cms page result format url function
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        [WebMethod]
        protected string FormatURL(int TabID, string Link)
        {
            string strURL=string.Empty;
            try
            {            
                if (String.IsNullOrEmpty(Link))
                {
                    strURL = Globals.NavigateURL(TabID);
                }
                else
                {
                    strURL = Globals.NavigateURL(TabID, "", Link);
                }

            }
            catch (Exception exc) //Module failed to load
            {
                LogFileWrite(exc);
            }
            return strURL;
        }


        /// <summary>
        /// product result set paging default function
        /// </summary>
        /// <param name="TotalNoOfRecords"></param>
        /// <param name="NoOfRecordsPerPage"></param>
        /// <param name="FirstValue"></param>
        private void SetPagingDefaults(int TotalNoOfRecords, int NoOfRecordsPerPage, int FirstValue)
        {
            try
            {
                HECMSPager.CreatePagingControl(TotalNoOfRecords, FirstValue);
                //HECMSPager.PageButtonStyle(FirstValue);
                if (TotalNoOfRecords > NoOfRecordsPerPage)
                {
                    HECMSPager.DisplayPropertyForPage("block");
                }
                else
                {
                    HECMSPager.DisplayPropertyForPage("none");
                }
            }
            catch (Exception exc) //Module failed to load
            {
                LogFileWrite(exc);
            }
        }
    }
}