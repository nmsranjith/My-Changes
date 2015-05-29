using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="HEProductsPager" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Used for paging functionality for the HE product results page.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public partial class UnallocatedStudentsPager : eCollection_StudentsModuleBase
    {

        private int ItemCount = int.Parse(ConfigurationManager.AppSettings["NO_OF_UNALLOCATED_STUDENTS"]);
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
                    //LogValues(string.Concat("totalpage=", totalpage, "itemsCount=", itemsCount, "  numberOfResults=", numberOfResults, "  startCount=", startCount));                    
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
                LogFileWrite(ex);
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
                    PreviousButton.Attributes.Add("class", "PreviousButton");
                    ShowPreviousButton.Attributes.Add("class", "ShowPreviousButton"); 
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
                LogFileWrite(ex);
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
                //LogValues(string.Concat("totalpage=", rowCount, "startCount=", startCount));
                    
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
                    //PreviousButton.Attributes.Add("class", "disabledpage");
                    //ShowPreviousButton.Attributes.Add("class", "disabledpage");
                }
                else
                {
                    PreviousButton.Attributes.Add("href", getPageURL("1"));
                    ShowPreviousButton.Attributes.Add("href", getPageURL((CurrentPage > 1) ? (CurrentPage - 1).ToString() : "1"));
                }

                if (CurrentPage == lastpage)
                {
                    ShowNextLink.Attributes.Add("class", "Rightrovercod");
                    NextLink.Attributes.Add("class", "Rightovercod");
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
                string unq = (Request.QueryString["unq"] != null) ? Request.QueryString["unq"] : "";
                return string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID), "?pagename=unallocated&unq=", unq, "&p="+p);
            }
            catch (Exception ex) { LogFileWrite(ex); return string.Empty; }
        }
        
    }
}