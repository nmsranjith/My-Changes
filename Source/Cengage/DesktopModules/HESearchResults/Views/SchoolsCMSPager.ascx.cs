using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cengage.eCommerce.ExceptionHandling;
using System.Configuration;

namespace DotNetNuke.Modules.HESearchResults.Views
{
    public partial class SchoolsCMSPager : System.Web.UI.UserControl
    {
        private int ItemCount = int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"]);
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



            if (!IsPostBack)
            {
                /*PreviousButton.Attributes.Add("href", "javascript:GetFirstPageNumber(this);");
                ShowPreviousButton.Attributes.Add("href", "javascript:GetPreviousPageNumber(this);");
                ShowNextLink.Attributes.Add("href", "javascript:GetNextPageNumber(this);");
                NextLink.Attributes.Add("href", "javascript:GetLastPageNumber(this);");*/
            }
            else
            {
                plcPagingcms.Controls.Clear();
                CreatePagingControl(0, 0);
            }


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
                DataAccessException.Instance.ExceptionMessage(ex);
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

                    //PreviousButtoncms.Enabled = false;
                    PreviousButtoncms.CssClass = PreviousButtoncms.CssClass.Replace("whoverllcms", "Leftlovercodcms");
                    //ShowPreviousButtoncms.Enabled = false;
                    ShowPreviousButtoncms.CssClass = ShowPreviousButtoncms.CssClass.Replace("whoverlcms", "Leftovercodcms");


                }
                foreach (HyperLink linkButton in plcPagingcms.Controls)
                {
                    int Id = 0;
                    bool isValid = int.TryParse(linkButton.Text.Trim(), out Id);
                    if (isValid)
                    {
                        if (Id == int.Parse(pageNum[1]))
                        {
                            linkButton.CssClass = "pagecms Highlightcms SortTextcms";
                        }
                        else
                        {
                            linkButton.CssClass = "pagecms SortTextcms";
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
            //if (((currentPage) % 10) == 0)
            //{
            //    pageno = currentPage;
            //}
            //pageno--;
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
                plcPagingcms.Controls.Clear();
                //triggerFirstlnk.Style.Add("display", "none");
                //triggerEndlnk.Style.Add("display", "none");
                string[] pageNum = pageNumbercms.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                int count = 0;
                int rowcountresult;
                if (!int.TryParse(((decimal)rowCount / ItemCount).ToString(), out rowcountresult))
                {
                    decimal incrementRowcnt = Math.Ceiling((decimal)rowCount / ItemCount);
                    rowcountresult = int.Parse(incrementRowcnt.ToString());
                    //rowcountresult += rowcountresult;
                    //rowcountresult++;
                }

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
                int pageid = 0;
                for (int i = startCount; i < rowcountresult; i++)
                {

                    HyperLink lnk = new HyperLink();
                    lnk.ID = "linkbtn" + pageid++;


                    if (int.Parse(pageNum[pageNum.Length - 1]) == 1 && i == 0)
                    {
                        lnk.CssClass = "Highlightcms pagecms SortTextcms";
                    }
                    else
                    {
                        lnk.CssClass = "pagecms SortTextcms";
                    }
                    lnk.Text = (i + 1).ToString();
                    lnk.Attributes.Add("href", getPageURL(lnk.Text));
                    plcPagingcms.Controls.Add(lnk);
                    if (count == 9 && rowcountresult != (i + 1))
                    {
                        break;
                    }
                    count++;
                }




                if (CurrentPage == 1)
                {
                    //PreviousButtoncms.Enabled = false;
                    //ShowPreviousButtoncms.Enabled = false;
                    PreviousButtoncms.CssClass = PreviousButtoncms.CssClass.Replace("whoverllcms", "Leftlovercodcms");
                    ShowPreviousButtoncms.CssClass = ShowPreviousButtoncms.CssClass.Replace("whoverlcms", "Leftovercodcms");

                }
                else
                {
                    PreviousButtoncms.Attributes.Add("href", getPageURL("1"));
                    ShowPreviousButtoncms.Attributes.Add("href", getPageURL((CurrentPage > 1) ? (CurrentPage - 1).ToString() : "1"));
                    //PreviousButtoncms.Enabled = true;
                    //ShowPreviousButtoncms.Enabled = true;
                }

                if (CurrentPage == lastpage)
                {
                    //ShowNextLinkcms.Enabled = false;
                    //NextLinkcms.Enabled = false;
                }
                else
                {
                    ShowNextLinkcms.Attributes.Add("href", getPageURL((CurrentPage != lastpage) ? (CurrentPage + 1).ToString() : lastpage.ToString()));
                    NextLinkcms.Attributes.Add("href", getPageURL(lastpage.ToString()));
                    //ShowNextLinkcms.Enabled = true;
                    //NextLinkcms.Enabled = true;
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
        /// Cms page result get page url
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private string getPageURL(string p)
        {
            string keyword = (Request.QueryString["q"] != null) ? Request.QueryString["q"] : "";
            string division = (Request.QueryString["division"] != null) ? Request.QueryString["division"] : "both";
            string sort = (Request.QueryString["s"] != null) ? Request.QueryString["s"] : "R";
            string facet = (Request.QueryString["f"] != null) ? Request.QueryString["f"] : "";
            string searchtype = (Request.QueryString["searchtype"] != null) ? Request.QueryString["searchtype"] : "";

            switch (searchtype.ToLower())
            {
                case "advkey":
                    keyword = (Request.QueryString["k_q"] != null) ? Request.QueryString["k_q"] : "";
                    var title = (Request.QueryString["t_q"] != null) ? Request.QueryString["t_q"] : "";
                    var author = (Request.QueryString["a_q"] != null) ? Request.QueryString["a_q"] : "";
                    var subject = (Request.QueryString["Sub_q"] != null) ? Request.QueryString["Sub_q"] : "";
                    var attr = (Request.QueryString["fa_q"] != null) ? Request.QueryString["fa_q"] : "";
                    var attrval = (Request.QueryString["fatv_q"] != null) ? Request.QueryString["fatv_q"] : "";
                    var allwords = (Request.QueryString["all_q"] != null) ? Request.QueryString["all_q"] : "";
                    var extph = (Request.QueryString["et_q"] != null) ? Request.QueryString["et_q"] : "";

                    return "./search?searchtype=advkey"
                        + "&division=" + division + "&cp=" + p + "&s=" + sort + "&k_q=" + keyword + "&t_q=" + title
                + "&a_q=" + author + "&Sub_q=" + subject + "&fa_q=" + attr + "&fatv_q=" + attrval + "&all_q=" + allwords
                + "&et_q=" + extph + "&cms=t";
                    break;
                case "advpri":
                    break;
                case "advsec":
                    break;
                case "advisbn":
                    var isbn = (Request.QueryString["keyisbn"] != null) ? Request.QueryString["q"] : "";
                    return "./search?searchtype=advisbn"
                            + "&division=" + division + "&cp=" + p + "&s=" + sort + "&keyisbn=" + isbn;
                    break;
                default:
                    return "./search?q=" + keyword + "&division=" + division + "&cp=" + p + "&s=" + sort + "&f=" + facet + "&cms=t";
                    break;
            }
            return "./search?q=" + keyword + "&division=" + division + "&cp=" + p + "&s=" + sort + "&f=" + facet + "&cms=t";
        }
    }
}