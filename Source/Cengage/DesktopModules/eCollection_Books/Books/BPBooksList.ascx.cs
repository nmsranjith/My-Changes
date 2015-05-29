﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using DotNetNuke.Modules.eCollection_Books.Components.Common;

namespace DotNetNuke.Modules.eCollection_Books.Books
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="BPBooksList" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Book pack's Books List control
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class BPBooksList : eCollection_BooksModuleBase
    {
        int SelectedSubId = 0;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["UserName"] != null)
                {
                    SqlDataReader reader = BooksController.Instance.GetBookPacks(new BookPack() { SubscriptionId = Null.SetNullInteger(Session["Subscription"]) });
                    List<BookPack> bookPackList = new List<BookPack>();
                    while (reader.Read())
                    {
                        bookPackList.Add(new BookPack() { BookPackSk = Null.SetNullInteger(reader["BOOK_PACK_SK"]), BookPackName = Null.SetNullString(reader["BOOK_PACK_NAME"]), PackDescription = Null.SetNullString(reader["PACK_DESCRIPTION"]) });
                    }
                    BookPackRptr.DataSource = bookPackList;
                    BookPackRptr.DataBind();
                    reader.NextResult();
                    reader.NextResult();
                    while (reader.Read())
                    {
                        if (Null.SetNullInteger(reader["MY_BOOK_PACK_COUNT"]) > 100)
                        {
                            CustomButton.Visible = false;
                            //EditCustomButton.Visible = true;
                        }
                        else if (Null.SetNullInteger(reader["MY_BOOK_PACK_COUNT"]) > 0)
                        {
                            CustomButton.Visible = false;
                            //EditCustomButton.Visible = false;
                        }
                        else
                        {
                            CustomButton.Visible = true;
                            //EditCustomButton.Visible = false;
                        }
                    }

                    if (GracePeriod.DaysLeft == 1)
                    {
                        DaysLeftLbl.InnerText = GracePeriod.DaysLeft.ToString() + " day";
                        DaysLeftLbl1.InnerText = GracePeriod.DaysLeft.ToString() + " day";
                    }
                    else if (GracePeriod.DaysLeft >= 0)
                    {
                        DaysLeftLbl.InnerText = GracePeriod.DaysLeft.ToString() + " days";
                        DaysLeftLbl1.InnerText = GracePeriod.DaysLeft.ToString() + " days";
                    }
                    else
                    {
                        if (GracePeriod.isAfterGracePeriod && !GracePeriod.isAfterGracePeriod7Days)
                        {
                            DaysLeftLbl.InnerText = GracePeriod.Grace_period.Date.AddDays(7).Date.Subtract(DateTime.Now.Date).Days.ToString();
                        }
                    }
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["homepage"]);
                }
                pageurlhdn.Value = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
            }
            catch (Exception ex) { LogFileWrite(ex); }

            try
            {
                CustomPaging.OnbuttonClicked += new CustomPaging.ButtonClciked(BindBooks);
                if (!IsPostBack)
                {
                    //CollectionRepeater.DataSource = Products;
                    //CollectionRepeater.DataBind();
                    if (GracePeriod.BooksLeft > 0)
                    {

                        BooksController booksController = BooksController.Instance;
                        booksController.AutoAssignBooks(SelectedSubscriptionId, LoginName);
                    }
                    //Modified By kalai
                    BindBooks(10, 0, 0);
                    DataCache.RemoveCache(string.Format("GetBooksByReadingLevel{0}", SelectedSubscriptionId));
                }
                Session["BooksCategories"] = CategoriesDrpList.SelectedItem.Text;
            }
            catch (Exception ex) //Module failed to load
            {
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Duringgraceperiod_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=BookSelectionWizard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="take"></param>
        /// <param name="pageSize"></param>
        /// <param name="startCount"></param>
        private void BindBooks(int take, int pageSize, int startCount)
        {

            BooksController bookController = BooksController.Instance;
            //Added By kalai
            int startLevel = 1, endLevel = 24;
            try
            {
                SelectedSubId = Convert.ToInt32(Session["Subscription"]);
                string[] sliderrange = SliderRange.Value != string.Empty ? SliderRange.Value.Split('-') : new string[2] { startLevel.ToString(), endLevel.ToString() };
                if (sliderrange.Length > 0)
                {
                    startLevel = SliderRange.Value != string.Empty ? int.Parse(sliderrange[0]) + 1 : int.Parse(sliderrange[0]);
                    endLevel = int.Parse(sliderrange[1]);
                }

                List<Book> bookList = bookController.GetBooksByReadingLevel(SelectedSubId, startLevel, endLevel, LoginName);
                ConstRowCount = bookList.Count;
                bookList.ForEach(x => { x.Checked = false; x.ClassName = "bookRowStyle"; x.CheckImgPathName = "Portals/0/images/circle_big.png"; });
                //Added By kalai
                List<string> seletedId = custItmSKHidden.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToList<string>();

                bookList.ForEach(x => { x.Checked = false; x.ClassName = "bookRowStyle"; x.CheckImgPathName = "Portals/0/images/circle_big.png"; });

                //Added By kalai
                if (seletedId.Count > 0)
                {
                    bookList.Where(u => seletedId.Contains(u.CUST_SUBS_ITEM_SK.ToString())).ToList<Book>().ForEach(x => { x.Checked = true; x.ClassName = "bookRowStyle rowclick"; x.CheckImgPathName = "Portals/0/images/tick_student.png"; });
                }

                if (bookList != null && bookList.Count > 0)
                {
                    CustomPaging.CreatePagingControl(ConstRowCount, startCount);
                    CustomPaging.PageButtonStyle(startCount);
                    CollectionRepeater.DataSource = bookList.Take(take).Skip(pageSize).ToList();
                    CollectionRepeater.DataBind();
                    BooksUpdatePanel.Update();
                    if (!(ConstRowCount > 10))
                    {
                        CustomPaging.DisplayPropertyForPage("none");
                    }
                    else
                    {
                        CustomPaging.DisplayPropertyForPage("block");
                    }
                    MessageOuterDiv.Style.Add("Display", "none");
                    ScriptManager.RegisterStartupScript(Page, GetType(), "ShowBooksContent", "<script>$('#BooksDiv').css('display','block');</script>", false);
                    MessageDivUpdatePanel.Update();
                }
                else if (bookList.Count == 0)
                {
                    CollectionRepeater.DataSource = bookList;
                    CollectionRepeater.DataBind();
                    CustomPaging.DisplayPropertyForPage("none");
                    MessageOuterDiv.Style.Add("Display", "block");
                    Message1.Text = Constants.NOBOOKSINFO;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "HideBooksContent", "<script>$('#BooksDiv').css('display','none');</script>", false);
                    //SearchButton.Enabled = false;
                    BooksUpdatePanel.Update();
                    MessageDivUpdatePanel.Update();
                }

                //CollectionRepeater.DataSource = bookList.Take(10);
                //if (bookList.Count > 10)
                //{
                //    PreviousButton.Visible = true;
                //    ShowNextButton.Visible = true;
                //}
                //else
                //{
                //    PreviousButton.Visible = false;
                //    ShowNextButton.Visible = false;
                //}
                //CollectionRepeater.DataBind();
                //BooksUpdatePanel.Update();
                //PreviousButton.Visible = false; 
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hiddenbtn_Click(object sender, EventArgs e)
        {
            DataCache.RemoveCache(string.Format("GetBooksByReadingLevel{0}", SelectedSubscriptionId));
            CustomPaging.PageNumber = "0,1";
            BindBooks(10, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CollectionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void itemSelected(object sender, EventArgs e)
        {
            Session["BooksCategories"] = CategoriesDrpList.SelectedItem.Text;
            SearchTextBox.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                BooksController.Instance.GetSearchBook(SearchTextBox.Text.Replace(", ", ",").Trim() == string.Empty ? "%" : SearchTextBox.Text.Replace(", ", ",").Trim(), int.Parse(Session["Subscription"].ToString()), CategoriesDrpList.SelectedItem.Text);
                CustomPaging.PageNumber = "0,1";
                BindBooks(10, 0, 0);
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
    }
}