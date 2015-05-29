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
    public partial class SchoolsProductsPager : System.Web.UI.UserControl
    {
        private int ItemCount = int.Parse(ConfigurationManager.AppSettings["ITEMCOUNT"]);
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
            if (!IsPostBack)
            {

            }
            else
            {
                plcPaging.Controls.Clear();
                CreatePagingControl(0, 0);
            }


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

                    //PreviousButton.Enabled = false;
                    PreviousButton.CssClass = PreviousButton.CssClass.Replace("whoverll", "Leftlovercod");
                    //ShowPreviousButton.Enabled = false;
                    ShowPreviousButton.CssClass = ShowPreviousButton.CssClass.Replace("whoverl", "Leftovercod");


                }
                foreach (HyperLink linkButton in plcPaging.Controls)
                {
                    int Id = 0;
                    bool isValid = int.TryParse(linkButton.Text.Trim(), out Id);
                    if (isValid)
                    {
                        if (Id == int.Parse(pageNum[1]))
                        {
                            linkButton.CssClass = "page Highlight SortText";
                        }
                        else
                        {
                            linkButton.CssClass = "page SortText";
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
                string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                int count = 0;
                int rowcountresult;
                if (!int.TryParse(((decimal)rowCount / ItemCount).ToString(), out rowcountresult))
                {
                    decimal incrementRowcnt = Math.Ceiling((decimal)rowCount / ItemCount);
                    rowcountresult = int.Parse(incrementRowcnt.ToString());
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
                int pageid = 0;
                for (int i = startCount; i < rowcountresult; i++)
                {

                    HyperLink lnk = new HyperLink();
                    lnk.ID = "linkbtn" + pageid++;
                    //pageid++;
                    if (int.Parse(pageNum[pageNum.Length - 1]) == 1 && i == 0)
                    {
                        lnk.CssClass = "Highlight page SortText";
                    }
                    else
                    {
                        lnk.CssClass = "page SortText";
                    }
                    lnk.Text = (i + 1).ToString();
                    lnk.Attributes.Add("href", getPageURL(lnk.Text));
                    plcPaging.Controls.Add(lnk);
                    if (count == 9 && rowcountresult != (i + 1))
                    {
                        break;
                    }
                    count++;
                }




                if (CurrentPage == 1)
                {
                    //PreviousButton.Enabled = false;
                    //ShowPreviousButton.Enabled = false;
                    PreviousButton.CssClass = PreviousButton.CssClass.Replace("whoverll", "Leftlovercod");
                    ShowPreviousButton.CssClass = ShowPreviousButton.CssClass.Replace("whoverl", "Leftovercod");

                }
                else
                {
                    PreviousButton.Attributes.Add("href", getPageURL("1"));
                    ShowPreviousButton.Attributes.Add("href", getPageURL((CurrentPage > 1) ? (CurrentPage - 1).ToString() : "1"));
                    //PreviousButton.Enabled = true;
                    //ShowPreviousButton.Enabled = true;
                }

                if (CurrentPage == lastpage)
                {
                    //ShowNextLink.Enabled = false;
                    //NextLink.Enabled = false;
                }
                else
                {
                    ShowNextLink.Attributes.Add("href", getPageURL((CurrentPage != lastpage) ? (CurrentPage + 1).ToString() : lastpage.ToString()));
                    NextLink.Attributes.Add("href", getPageURL(lastpage.ToString()));
                    //ShowNextLink.Enabled = true;
                    //NextLink.Enabled = true;
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
        /// Product result getpageurl
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
                        + "&division=" + division + "&p=" + p + "&s=" + sort + "&k_q=" + keyword + "&t_q=" + title
                + "&a_q=" + author + "&Sub_q=" + subject + "&fa_q=" + attr + "&fatv_q=" + attrval + "&all_q=" + allwords
                + "&et_q=" + extph;
                    break;
                case "advpri":
                    break;
                case "advsec":
                    break;
                case "advisbn":
                    var isbn = (Request.QueryString["keyisbn"] != null) ? Request.QueryString["keyisbn"] : "";
                    return "./search?searchtype=advisbn"
                            + "&division=" + division + "&p=" + p + "&s=" + sort + "&keyisbn=" + isbn;
                    break;
                default:
                    return "./search?q=" + HttpUtility.UrlEncode(keyword) + "&division=" + division + "&p=" + p + "&s=" + sort + "&f=" + facet;
                    break;
            }
            return "./search?q=" + HttpUtility.UrlEncode(keyword) + "&division=" + division + "&p=" + p + "&s=" + sort + "&f=" + facet;
        }




    }
}