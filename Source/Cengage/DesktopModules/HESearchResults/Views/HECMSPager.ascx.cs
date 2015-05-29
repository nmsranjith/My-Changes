using System;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cengage.eCommerce.ExceptionHandling;

namespace DotNetNuke.Modules.HESearchResults.Views
{
    public partial class HECMSPager : HESearchResultsModuleBase
    {
        private int ItemCount = int.Parse(ConfigurationManager.AppSettings["NO_OF_RECORDS_PER_PAGE"]);
        public delegate void ButtonClciked(int take, int skip, int startCount);
        public event ButtonClciked OnbuttonClicked;
        private int CurrentPage;
        private int lastpage;

        /// <summary>
        /// Cms page result page number page load
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
                    plcPagingcms.Controls.Clear();
                    CreatePagingControl(0, 0);
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
        
        /// <summary>
        /// Cms page result display property for page function
        /// </summary>
        /// <param name="display"></param>
        public void DisplayPropertyForPage(string display)
        {
            try
            {
                StudentPagerDivcms.Style.Add("display", display);
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
        /// Cms page result for page button style function
        /// </summary>
        /// <param name="startCount"></param>
        public void PageButtonStyle(int startCount)
        {
            try
            {
                string[] pageNum = pageNumbercms.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                if (startCount == 0 && int.Parse(pageNum[1]) == 1)
                {
                    PreviousButtoncms.Attributes.Add("class", "disabledpage");
                    ShowPreviousButtoncms.Attributes.Add("class", "disabledpage");
                }
                foreach (HtmlAnchor linkButton in plcPagingcms.Controls)
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
        /// Cms page result get curent page number
        /// </summary>
        /// <returns></returns>
        public int GetCurrentPageNo()
        {

            string[] pageNum = pageNumbercms.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            int currentPage = int.Parse(pageNum[1]);
            int pageno = 0;
            pageno = int.Parse(pageNum[0]);
            return pageno;

        }

        /// <summary>
        /// Cms page result cretae paging control
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="startCount"></param>
        public void CreatePagingControl(int rowCount, int startCount)
        {
            try
            {
                CurrentPage = (Request.QueryString["cp"] != null && Request.QueryString["cp"] != "") ? int.Parse(Request.QueryString["cp"]) : 1;
               // LogValues(string.Concat("rowCount-->", rowCount.ToString(), "startCount-->", startCount.ToString(), ",CurrentPage -->", CurrentPage));
                plcPagingcms.Controls.Clear();
                string[] pageNum = pageNumbercms.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                int count = 0;
                int rowcountresult;
                if (!int.TryParse(((decimal)rowCount / ItemCount).ToString(), out rowcountresult))
                {
                    decimal incrementRowcnt = Math.Ceiling((decimal)rowCount / ItemCount);
                    rowcountresult = int.Parse(incrementRowcnt.ToString());
                }
                else { }
                int totalpage = rowCount % ItemCount == 0 ? rowCount / ItemCount : (rowCount / ItemCount) + 1;
                lastpage = totalpage;
                hdnpagecountcms.Value = totalpage.ToString();
                if (totalpage > int.Parse(pageNum[1]) + 4)
                {
                    if (int.Parse(pageNum[1]) - 5 >= 0)
                        startCount = int.Parse(pageNum[1]) - 5;
                }
                if (rowcountresult == 0)
                    rowcountresult++;
               // LogValues(string.Concat("pageNum Length-->", pageNum.Length.ToString(), "CurrentPage-->", CurrentPage.ToString()));
                for (int i = startCount; i < rowcountresult; i++)
                {
                    HtmlAnchor anchor = new HtmlAnchor();
                    //if (int.Parse(pageNum[pageNum.Length - 1]) == 1 && i == 0)
                    //    anchor.Attributes.Add("class", "Highlight page SortText"); 
                    //else
                    //    anchor.Attributes.Add("class", "page SortText");
                    if (CurrentPage == i + 1)
                    {
                        anchor.Attributes.Add("class", "Highlight page SortText");
                    }
                    else
                        anchor.Attributes.Add("class", "page SortText");
                    anchor.InnerText = (i + 1).ToString();
                    anchor.Attributes.Add("href", getPageURL(anchor.InnerText));
                    plcPagingcms.Controls.Add(anchor);
                    if (count == 9 && rowcountresult != (i + 1))
                        break;
                    count++;
                }
                if (CurrentPage == 1)
                {
                    PreviousButtoncms.Attributes.Add("class", "disabledpage");
                    ShowPreviousButtoncms.Attributes.Add("class", "disabledpage");
                }
                else
                {
                    PreviousButtoncms.Attributes.Add("href", getPageURL("1"));
                    ShowPreviousButtoncms.Attributes.Add("href", getPageURL((CurrentPage > 1) ? (CurrentPage - 1).ToString() : "1"));
                }

                if (CurrentPage == lastpage)
                {
                    ShowNextLinkcms.Attributes.Add("class", "disabledpage");
                    NextLinkcms.Attributes.Add("class", "disabledpage");
                }
                else
                {
                    ShowNextLinkcms.Attributes.Add("href", getPageURL((CurrentPage != lastpage) ? (CurrentPage + 1).ToString() : lastpage.ToString()));
                    NextLinkcms.Attributes.Add("href", getPageURL(lastpage.ToString()));
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
        /// Cms page result get page url
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string getPageURL(string p)
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
            var advOpen = string.IsNullOrEmpty(AdvSearchOpenFlagHdn3.Value) ? "f" : AdvSearchOpenFlagHdn3.Value;
            var bwf = (Request.QueryString["format"] != null) ? Request.QueryString["format"] : "";
            switch (searchtype.ToUpper())
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
                    //    + "&amp;division=" + division + "&amp;cp=" + p + "&amp;s=" + sort + "&amp;t_q=" + title
                    //    + "&amp;aq=" + author + "&amp;subq=" + subject + "&amp;faq=" + attr + "&amp;fatvq=" + attrval + "&amp;allq=" + allwords + "&amp;nq=" + noneph
                    //    + "&amp;etq=" + extph + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen + "&amp;fv=" + fv + "&amp;pv=" + pv + "&amp;pyv" + pyv + "&amp;ov=" + ov;
                    //else
                        return "/search?st=ad"
                        + "&amp;division=" + division + "&amp;cp=" + p + "&amp;s=" + sort + "&amp;t_q=" + title
                        + "&amp;aq=" + author + "&amp;subq=" + subject + "&amp;faq=" + attr + "&amp;fatvq=" + attrval + "&amp;allq=" + allwords + "&amp;nq=" + noneph
                        + "&amp;etq=" + extph + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen + "&amp;fv=" + fv + "&amp;pv=" + pv + "&amp;pyv=" + pyv + "&amp;ov=" + ov+"&amp;epf=" + epf + "&amp;flt=" + flt;                    
                    break;        
                case "advisbn":
                    var isbn = (Request.QueryString["keyisbn"] != null) ? Request.QueryString["q"] : "";
                    return "/search?st=ad"
                            + "&amp;division=" + division + "&amp;cp=" + p + "&amp;s=" + sort + "&amp;keyisbn=" + isbn + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen ;//+ "&fv=" + fv + "&pv=" + pv + "&pyv" + pyv + "&ov=" + ov;
                    break;
                default:
                    return "/search?q=" + keyword.Replace("\"", "&quot;") + "&amp;division=" + division + "&amp;cp=" + p + "&amp;s=" + sort + "&amp;f=" + facet + "&amp;cms=t" + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen + "&amp;format=" + bwf;
                    break;
            }
            return "/search?q=" + keyword.Replace("\"", "&quot;") + "&amp;division=" + division + "&amp;cp=" + p + "&amp;s=" + sort + "&amp;f=" + facet + "&amp;cms=t" + "&amp;sf=" + sf + "&amp;dt=" + dt + "&amp;dc=" + dc + "&amp;dv=" + dv + "&amp;ds=" + ds + "&amp;ao=" + advOpen + "&amp;format=" + bwf;
        }
    }
}