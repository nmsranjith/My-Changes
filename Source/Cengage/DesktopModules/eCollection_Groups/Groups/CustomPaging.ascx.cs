using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Groups.Components;
using System.Reflection;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CustomPaging" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //   This page is used for creating pagers.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    
    public partial class CustomPaging : eCollection_GroupsModuleBase
    {
        private int ItemCount = 10;
        public delegate void ButtonClciked(int take, int skip, int startCount);
        public event ButtonClciked OnbuttonClicked;

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
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
        /// 
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="startCount"></param>
        private void Paging(int currentPage, int startCount)
        {

            int take = currentPage * ItemCount;
            int skip = currentPage == 1 ? 0 : take - ItemCount;

            OnbuttonClicked(take, skip, startCount);
            foreach (LinkButton linkButton in plcPaging.Controls)
            {
                int Id = 0;
                bool isValid = int.TryParse(linkButton.Text.Trim(), out Id);
                if (isValid)
                {
                    if (Id == currentPage)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="display"></param>
        public void DisplayPropertyForPage(string display)
        {
            StudentPagerDiv.Style.Add("display", display);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startCount"></param>
        public void PageButtonStyle(int startCount)
        {
            string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            if (startCount == 0 && int.Parse(pageNum[1]) == 1)
            {
               PreviousButton.Enabled = false;
               ShowPreviousButton.Enabled = false;
            }
            foreach (LinkButton linkButton in plcPaging.Controls)
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetCurrentPageNo()
        {
            string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            int currentPage = int.Parse(pageNum[1]);
            int pageno = 0;
            pageno = int.Parse(pageNum[0]);
            return pageno;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="startCount"></param>
        public void CreatePagingControl(int rowCount, int startCount)
        {
            plcPaging.Controls.Clear();
            string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            int count = 0;
            int rowcountresult;
            if (!int.TryParse(((decimal)rowCount / ItemCount).ToString(), out rowcountresult))
            {
                decimal incrementRowcnt = Math.Ceiling((decimal)rowCount / ItemCount);
                rowcountresult = int.Parse(incrementRowcnt.ToString());
            }
            else
            {
            }
            int totalpage = rowCount % ItemCount == 0 ? rowCount / ItemCount : (rowCount / ItemCount) + 1;
            if (totalpage > int.Parse(pageNum[1]) + 4)
            {
                if (int.Parse(pageNum[1]) - 5 >= 0)
                    startCount = int.Parse(pageNum[1]) - 5;
                else startCount = 0;
            }
            else if (totalpage > int.Parse(pageNum[1]) + 3)
            {
                if (int.Parse(pageNum[1]) - 6 >= 0)
                    startCount = int.Parse(pageNum[1]) - 6;
            }
            else if (totalpage > int.Parse(pageNum[1]) + 2)
            {
                if (int.Parse(pageNum[1]) - 7 >= 0)
                    startCount = int.Parse(pageNum[1]) - 7;
            }
            else if (totalpage > int.Parse(pageNum[1]) + 1)
            {
                if (int.Parse(pageNum[1]) - 8 >= 0)
                    startCount = int.Parse(pageNum[1]) - 8;
            }
            else if (totalpage > int.Parse(pageNum[1]) )
            {
                if (int.Parse(pageNum[1]) - 9 >= 0)
                    startCount = int.Parse(pageNum[1]) - 9;
            }
            else 
            {
                if (int.Parse(pageNum[1]) - 10 >= 0)
                    startCount = int.Parse(pageNum[1]) - 10;
            }
            
            

            if (rowcountresult == 0)
                rowcountresult++;

            for (int i = startCount; i < rowcountresult; i++)
            {               
                LinkButton lnk = new LinkButton();
                lnk.Click += new EventHandler(linkbtn_Click);
                lnk.ID = "linkbtn";
                lnk.OnClientClick = "GetPageNumber(this)";


                if (int.Parse(pageNum[pageNum.Length - 1]) == 1 && i == 0)
                {
                    lnk.CssClass = "page Highlight SortText";
                }
                else
                {
                    lnk.CssClass = "page SortText";
                }
                lnk.Text = (i + 1).ToString();
                plcPaging.Controls.Add(lnk);
               
                if (count == 9 && rowcountresult != (i + 1))
                {
                    return;
                }
                count++;
            }

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void linkbtn_Click(object sender, EventArgs e)
        {           
            LinkButton lnk = sender as LinkButton;
            string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            int currentPage = int.Parse(pageNum[1]);
            int pageno = 0;
            pageno = int.Parse(pageNum[0]);
            if (currentPage == 1)
            {
                NextLink.Enabled = true;
                NextLink.Style.Add("color", "#707070");
                ShowNextLink.Enabled = true;
                ShowNextLink.Style.Add("color", "#707070");
                ShowPreviousButton.Enabled = false;
                ShowPreviousButton.Style.Add("color", "lightgray");
                PreviousButton.Enabled = false;
                PreviousButton.Style.Add("color", "lightgray");
            }
            else
            {
                ShowPreviousButton.Enabled = true;
                ShowPreviousButton.Style.Add("color", "#707070");
                PreviousButton.Enabled = true;
                PreviousButton.Style.Add("color", "#707070");
                int crpagesize;
                if (!int.TryParse(((decimal)ConstRowCount / ItemCount).ToString(), out crpagesize))
                {
                    decimal incrementRowcnt = Math.Ceiling((decimal)ConstRowCount / ItemCount);
                    crpagesize = int.Parse(incrementRowcnt.ToString());                   
                }
                else
                {                    
                }

                if (crpagesize == currentPage)
                {
                    NextLink.Enabled = false;
                    NextLink.Style.Add("color", "lightgray");
                    ShowNextLink.Enabled = false;
                    ShowNextLink.Style.Add("color", "lightgray");
                }
                else
                {
                    NextLink.Enabled = true;
                    NextLink.Style.Add("color", "#707070");
                    ShowNextLink.Enabled = true;
                    ShowNextLink.Style.Add("color", "#707070");
                }
            }
            pageno--;
            pageNumber.Value = pageno.ToString() + ',' + currentPage.ToString();
            Paging(currentPage, pageno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PreviousButton_Click(object sender, EventArgs e)
        {
            int currentPage = 1;
            pageNumber.Value = "0" + ',' + currentPage.ToString();
            NextLink.Enabled = true;
            NextLink.Style.Add("color", "#707070");
            ShowNextLink.Enabled = true;
            ShowNextLink.Style.Add("color", "#707070");
            ShowPreviousButton.Enabled = false;
            ShowPreviousButton.Style.Add("color", "lightgray");
            PreviousButton.Enabled = false;
            PreviousButton.Style.Add("color", "lightgray");
            Paging(currentPage, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NextLink_Click(object sender, EventArgs e)
        {
            int currentPage = 0;
            int pageno = 0;
            if (!int.TryParse(((decimal)ConstRowCount / ItemCount).ToString(), out currentPage))
            {
                decimal incrementRowcnt = Math.Ceiling((decimal)ConstRowCount / ItemCount);
                currentPage = int.Parse(incrementRowcnt.ToString());
            }
            else
            {
            }


            NextLink.Enabled = false;
            NextLink.Style.Add("color", "lightgray");
            ShowNextLink.Enabled = false;
            ShowNextLink.Style.Add("color", "lightgray");
            ShowPreviousButton.Enabled = true;
            ShowPreviousButton.Style.Add("color", "#707070");
            PreviousButton.Enabled = true;
            PreviousButton.Style.Add("color", "#707070");
            pageno = (currentPage / ItemCount) * ItemCount;
            if (pageno == currentPage)
                pageno = currentPage - ItemCount;
            pageNumber.Value = pageno.ToString() + ',' + currentPage.ToString();
            Paging(currentPage, pageno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowPreviousButton_Click(object sender, EventArgs e)
        {
            string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            int currentPage = int.Parse(pageNum[1]);
            int pageno = 0;
            pageno = int.Parse(pageNum[0]);
            if (currentPage == 2)
            {
                NextLink.Enabled = true;
                NextLink.Style.Add("color", "#707070");
                ShowNextLink.Enabled = true;
                ShowNextLink.Style.Add("color", "#707070");
                ShowPreviousButton.Enabled = false;
                ShowPreviousButton.Style.Add("color", "lightgray");
                PreviousButton.Enabled = false;
                PreviousButton.Style.Add("color", "lightgray");
            }
            else
            {
                ShowPreviousButton.Enabled = true;
                ShowPreviousButton.Style.Add("color", "#707070");
                PreviousButton.Enabled = true;
                PreviousButton.Style.Add("color", "#707070");
                NextLink.Enabled = true;
                NextLink.Style.Add("color", "#707070");
                ShowNextLink.Enabled = true;
                ShowNextLink.Style.Add("color", "#707070");
            }
            currentPage--;
            if ((currentPage % ItemCount) == 0)
            {
                pageno = pageno - ItemCount;
            }
            pageNumber.Value = pageno.ToString() + ',' + currentPage.ToString();
            Paging(currentPage, pageno);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowNextLink_Click(object sender, EventArgs e)
        {           
            string[] pageNum = pageNumber.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            int currentPage = int.Parse(pageNum[1]);
            int pageno = int.Parse(pageNum[0]);
            int CheckcurrentPage;
            if (!int.TryParse(((decimal)ConstRowCount / ItemCount).ToString(), out CheckcurrentPage))
            {
                decimal incrementRowcnt = Math.Ceiling((decimal)ConstRowCount / ItemCount);
                CheckcurrentPage = int.Parse(incrementRowcnt.ToString());                
            }
            else
            {
            }

            if ((currentPage + 1) == CheckcurrentPage)
            {
                NextLink.Enabled = false;
                NextLink.Style.Add("color", "lightgray");
                ShowNextLink.Enabled = false;
                ShowNextLink.Style.Add("color", "lightgray");
                ShowPreviousButton.Enabled = true;
                ShowPreviousButton.Style.Add("color", "#707070");
                PreviousButton.Enabled = true;
                PreviousButton.Style.Add("color", "#707070");
            }
            else
            {
                NextLink.Enabled = true;
                NextLink.Style.Add("color", "#707070");
                ShowNextLink.Enabled = true;
                ShowNextLink.Style.Add("color", "#707070");
                ShowPreviousButton.Enabled = true;
                ShowPreviousButton.Style.Add("color", "#707070");
                PreviousButton.Enabled = true;
                PreviousButton.Style.Add("color", "#707070");

            }
            if ((currentPage % ItemCount) == 0)
            {
                pageno = currentPage;
            }
            currentPage++;
            pageNumber.Value = pageno.ToString() + ',' + currentPage.ToString();
            Paging(currentPage, pageno);
        }

    }
}