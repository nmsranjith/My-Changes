using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Cengage.eCommerce.ExceptionHandling;
using System.Web.UI.HtmlControls;

namespace DotNetNuke.Modules.HESearchResults.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="HEProductsPager" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Used for paging functionality for the HE product results page.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public partial class HEProductsPager : HESearchResultsModuleBase
    {

        private int ItemCount = int.Parse(ConfigurationManager.AppSettings["NO_OF_RECORDS_PER_PAGE"]);
        public delegate void ButtonClciked(int take, int skip, int startCount);
        public event ButtonClciked OnbuttonClicked;
        private int CurrentPage;
        private int lastpage;
        /// <summary>
        /// Product result paging page load function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                }
                else
                {
                    plcPaging.Controls.Clear();
                    CreatePagingControl(0, 0);
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }

        }

       /// <summary>
       /// Product result display property function
       /// </summary>
       /// <param name="display"></param>
        public void DisplayPropertyForPage(string display)
        {
            try
            {
                StudentPagerDiv.Style.Add("display", display);
            }
            catch (Exception ex)
            {
                DataAccessException.Instance.ExceptionMessage(ex);
            }
            finally
            {
            }
        }

        /// <summary>
        /// product result pagebutton style function
        /// </summary>
        /// <param name="startCount"></param>
        public void PageButtonStyle(int startCount)
        {
            try
            {
                string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                if (startCount == 0 && int.Parse(pageNum[1]) == 1)
                {
                    PreviousButton.Attributes.Add("class", "disabledpage");
                    ShowPreviousButton.Attributes.Add("class", "disabledpage"); 
                }
                foreach (HtmlAnchor linkButton in plcPaging.Controls)
                {
                    int Id = 0;
                    bool isValid = int.TryParse(linkButton.InnerText.Trim(), out Id);
                    if (isValid)
                    {
                        if (Id == int.Parse(pageNum[1]))
                        {
                            linkButton.Attributes.Add("class", "page Highlight SortText");
                        }
                        else
                        {
                            linkButton.Attributes.Add("class", "page SortText");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                DataAccessException.Instance.ExceptionMessage(ex);
            }
            finally
            {
            }
        }


        /// <summary>
        /// product result getcurrentpagenumber function
        /// </summary>
        /// <returns></returns>
        public int GetCurrentPageNo()
        {

            string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            int currentPage = int.Parse(pageNum[1]);
            int pageno = 0;
            pageno = int.Parse(pageNum[0]);
            //if (((currentPage) % 10) == 0)
            //{
            //    pageno = currentPage;
            //}
            //pageno--;
            return pageno;

        }

        /// <summary>
        /// Product result create paging control
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="startCount"></param>
        public void CreatePagingControl(int rowCount, int startCount)
        {
            try
            {
                CurrentPage = (Request.QueryString["p"] != null && Request.QueryString["p"] != "") ? int.Parse(Request.QueryString["p"]) : 1;
                plcPaging.Controls.Clear();
                //triggerFirstlnk.Style.Add("display", "none");
                //triggerEndlnk.Style.Add("display", "none");
                string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                int count = 0;
                int rowcountresult;
                if (!int.TryParse(((decimal)rowCount / ItemCount).ToString(), out rowcountresult))
                {
                    decimal incrementRowcnt = Math.Ceiling((decimal)rowCount / ItemCount);
                    rowcountresult = int.Parse(incrementRowcnt.ToString());
                    //rowcountresult += rowcountresult;
                    //rowcountresult++;
                }
                else
                {
                    //rowcountresult += rowcountresult;
                }
                int totalpage = rowCount % ItemCount == 0 ? rowCount / ItemCount : (rowCount / ItemCount) + 1;
                lastpage = totalpage;
                Hdnpagecount.Value = totalpage.ToString();
                if (totalpage > int.Parse(pageNum[1]) + 4)
                {
                    if (int.Parse(pageNum[1]) - 5 >= 0)
                        startCount = int.Parse(pageNum[1]) - 5;
                }
                if (rowcountresult == 0)
                    rowcountresult++;

                for (int i = startCount; i < rowcountresult; i++)
                {                    
                    HtmlAnchor anchor = new HtmlAnchor();
                    if (int.Parse(pageNum[pageNum.Length - 1]) == 1 && i == 0)
                        anchor.Attributes.Add("class", "Highlight page SortText");
                    else
                        anchor.Attributes.Add("class", "page SortText");
                    anchor.InnerText = (i + 1).ToString(); 
                    anchor.Attributes.Add("href", getPageURL(anchor.InnerText));
                    plcPaging.Controls.Add(anchor);
                    if (count == 9 && rowcountresult != (i + 1))
                        break;
                    count++;
                }
                if (CurrentPage == 1)
                {
                    PreviousButton.Attributes.Add("class", "disabledpage");
                    ShowPreviousButton.Attributes.Add("class", "disabledpage");
                }
                else
                {
                    PreviousButton.Attributes.Add("href", getPageURL("1"));
                    ShowPreviousButton.Attributes.Add("href", getPageURL((CurrentPage > 1) ? (CurrentPage - 1).ToString() : "1"));
                }

                if (CurrentPage == lastpage)
                {
                    ShowNextLink.Attributes.Add("class", "disabledpage");
                    NextLink.Attributes.Add("class", "disabledpage");
                }
                else
                {
                    ShowNextLink.Attributes.Add("href", getPageURL((CurrentPage != lastpage) ? (CurrentPage + 1).ToString() : lastpage.ToString()));
                    NextLink.Attributes.Add("href", getPageURL(lastpage.ToString()));
                }
               
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
            finally
            {
            }
        }


        /// <summary>
        /// Product result getpageurl
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string getPageURL(string p)
        {
            try
            {
                string keyword = (Request.QueryString["q"] != null) ? Request.QueryString["q"] : "";
                string division = (Request.QueryString["division"] != null) ? Request.QueryString["division"] : "all";
                string sort = (Request.QueryString["s"] != null) ? Request.QueryString["s"] : "R";
                string facet = (Request.QueryString["f"] != null) ? Request.QueryString["f"] : "";
                string searchtype = (Request.QueryString["st"] != null) ? Request.QueryString["st"] : "";
                string sf = (Request.QueryString["sf"] != null) ? Request.QueryString["sf"] : "";
                string dt = (Request.QueryString["dt"] != null) ? Request.QueryString["dt"] : "";
                string dc = (Request.QueryString["dc"] != null) ? Request.QueryString["dc"] : "";
                string dv = (Request.QueryString["dv"] != null) ? Request.QueryString["dv"] : "";
                string ds = (Request.QueryString["ds"] != null) ? Request.QueryString["ds"] : "";
                var advOpen = string.IsNullOrEmpty(AdvSearchOpenFlagHdn2.Value) ? "f" : AdvSearchOpenFlagHdn2.Value;
                var bwf = (Request.QueryString["format"] != null) ? Request.QueryString["format"] : "";
                switch (searchtype.ToLower())
                {
                    case "ad":
                        var title = (Request.QueryString["tq"] != null) ? Request.QueryString["tq"] : "";
                        var author = (Request.QueryString["aq"] != null) ? Request.QueryString["aq"] : "";
                        var subject = (Request.QueryString["subq"] != null) ? Request.QueryString["subq"] : "";
                        var attr = (Request.QueryString["faq"] != null) ? Request.QueryString["faq"] : "";
                        var attrval = (Request.QueryString["fatvq"] != null) ? Request.QueryString["fatvq"] : "";
                        var allwords = (Request.QueryString["allq"] != null) ? Request.QueryString["allq"] : "";
                        var extph = (Request.QueryString["etq"] != null) ? Request.QueryString["etq"] : "";
                        var noneph = (Request.QueryString["nq"] != null) ? Request.QueryString["nq"] : "";
                        var fv = (Request.QueryString["fv"] != null) ? Request.QueryString["fv"] : "";
                        var pv = (Request.QueryString["pv"] != null) ? Request.QueryString["pv"] : "";
                        var pyv = (Request.QueryString["pyv"] != null) ? Request.QueryString["pyv"] : "";
                        var ov = (Request.QueryString["ov"] != null) ? Request.QueryString["ov"] : "";
                        var epf = (Request.QueryString["epf"] != null) ? Request.QueryString["epf"] : "";
                        var flt = (Request.QueryString["flt"] != null) ? Request.QueryString["flt"] : "";
                        //if (division.ToLower()!="gale")
                        //    return "/search?st=ad"
                        //        + "&amp;division=" + division + "&amp;p=" + p + "&amp;s=" + sort + "&amp;tq=" + title
                        //+ "&amp;aq=" + author + "&amp;subq=" + subject + "&amp;faq=" + attr + "&amp;fatvq=" + attrval + "&amp;allq=" + allwords + "&amp;nq=" + noneph
                        //+ "&amp;etq=" + extph + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen + "&amp;fv=" + fv + "&amp;pv=" + pv + "&amp;pyv" + pyv + "&amp;ov=" + ov;
                        //else
                        return "/search?st=ad"
                            + "&amp;division=" + division + "&amp;p=" + p + "&amp;s=" + sort + "&amp;tq=" + title
                    + "&amp;aq=" + author + "&amp;subq=" + subject + "&amp;faq=" + attr + "&amp;fatvq=" + attrval + "&amp;allq=" + allwords + "&amp;nq=" + noneph
                    + "&amp;etq=" + extph + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen + "&amp;fv=" + fv + "&amp;pv=" + pv + "&amp;pyv=" + pyv + "&amp;ov=" + ov + "&amp;epf=" + epf + "&amp;flt=" + flt + "&amp;format=" + bwf;

                        break;
                    case "advisbn":
                        var isbn = (Request.QueryString["keyisbn"] != null) ? Request.QueryString["keyisbn"] : "";
                        return "/search?st=ad"
                                + "&amp;division=" + division + "&amp;p=" + p + "&amp;s=" + sort + "&amp;keyisbn=" + isbn + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen + "&amp;format=" + bwf;
                        break;
                    default:
                        return "/search?q=" + keyword.Replace("\"", "&quot;") + "&amp;division=" + division + "&amp;p=" + p + "&amp;s=" + sort + "&amp;f=" + facet + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen + "&amp;format=" + bwf;
                        break;
                }
                return "/search?q=" + keyword + "&amp;division=" + keyword.Replace("\"", "&quot;") + "&amp;p=" + p + "&amp;s=" + sort + "&amp;f=" + facet + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen + "&amp;format=" + bwf;
            }
            catch (Exception ex) { LogFileWrite(ex); return string.Empty; }
        }
        
    }
}