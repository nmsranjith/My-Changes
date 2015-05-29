using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using System.Web.UI.HtmlControls;
using System.Text;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using System.Xml;
using DotNetNuke.Common.Utilities;
using System.Configuration;

namespace DotNetNuke.Modules.eCollection_Books.Books
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SeeSelectedBooks" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    See Selected Books screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class SeeSelectedBooks : eCollection_BooksModuleBase
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
                CustomPaging.OnbuttonClicked += new CustomPaging.ButtonClciked(BindBooks);
                if (!IsPostBack)
                {
                    //Modified By kalai
                    BindBooks(10, 0, 0);
                }
                BindGracePeriodinfo();
                if (GracePeriod.isAfterGracePeriod)
                {
                    RemovetosubsButtontop.Enabled = false;
                    RemoveSubButtonbot.Enabled = false;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }           
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BacktoCollectionButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=BookSelectionWizard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SeeSelectedBokks_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=BookSelectionWizard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveFromSubscriptionBottom_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveFromsubscription();
                BindGracePeriodinfo();
                //Modified By kalai
                SliderRange.Value = "0-24";
                BindBooks(10,0,0);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveFromSubscription_Click(object sender, EventArgs e)    
        {
            try
            {                
                RemoveFromsubscription();
                BindGracePeriodinfo();
                //Modified By kalai
                SliderRange.Value = "0-24";
                BindBooks(10,0,0);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        private void RemoveFromsubscription()
        {
            try
            {
                StringBuilder output = new StringBuilder();
                BooksController booksController = BooksController.Instance;
                List<int> productIds = GetSelectedBooks();
                string strXML = string.Empty;
                if (productIds.Count > 0)
                {
                    foreach (int productId in productIds)
                    {
                        output.AppendFormat("<Products> <CUST_SUBS_SK>{0}</CUST_SUBS_SK><PRODUCT_SK>{1}</PRODUCT_SK></Products>", SelectedSubscriptionId, productId);
                    }
                }
                if (output.Length > 0)
                {
                    strXML = "<root>" + output.ToString() + "</root>";
                    Book selectedBooks = new Book()
                    {
                        ProductsDoc = new XmlDocument() { InnerXml = strXML }
                    };
                    booksController.RemoveBooksFromSubscription(Convert.ToInt32(SelectedSubscriptionId), selectedBooks.ProductsDoc, LoginName);
                    DataCache.RemoveCache(string.Format("GetBooksByReadingLevel{0}", SelectedSubscriptionId));
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<int> GetSelectedBooks()
        {
            List<int> productIds = new List<int>();            
            string[] prodList = custItmSKHidden.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            foreach (string prod in prodList)
                productIds.Add(Convert.ToInt32(prod));
            custItmSKHidden.Value = "";
            return productIds;
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindGracePeriodinfo()
        {
            lblBooksSelected.Text = GracePeriod.SelectedBooks.ToString();
            lblBooksLeft.Text = (GracePeriod.BooksLeft).ToString();
            if (GracePeriod.DaysLeft > 0)
            {
                lblDaysLeft.Text = GracePeriod.DaysLeft.ToString();
            }
            else
            {
                if (GracePeriod.isAfterGracePeriod && !GracePeriod.isAfterGracePeriod7Days)
                {
                    lblDaysLeft.Text = GracePeriod.Grace_period.Date.AddDays(7).Date.Subtract(DateTime.Now.Date).Days.ToString();
                }
            }
            lblGracePeriod.Text = GracePeriod.isAfterGracePeriod.ToString();
            SelectedBookscount.Text = GracePeriod.SelectedBooks.ToString();
            SelectedBookscountBot.Text = GracePeriod.SelectedBooks.ToString();
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
                //Added By kalai
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
                }
                else if (bookList.Count == 0)
                {
                    CollectionRepeater.DataSource = bookList;
                    CollectionRepeater.DataBind();
                    CustomPaging.DisplayPropertyForPage("none");
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
                //PreviousButton.Visible = false;
            }
            catch (Exception ex) { LogFileWrite(ex); }
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
        protected void hiddenbtn_Click(object sender, EventArgs e)
        {
            DataCache.RemoveCache(string.Format("GetBooksByReadingLevel{0}", SelectedSubscriptionId));
            CustomPaging.PageNumber = "0,1";
            BindBooks(10, 0, 0);           
        }       
    }
}