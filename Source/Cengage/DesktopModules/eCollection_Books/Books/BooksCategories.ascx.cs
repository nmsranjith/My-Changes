using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using System.Xml;
using System.Text;
using System.IO;
using System.Data;
using System.Web.UI.HtmlControls;
using DotNetNuke.Modules.eCollection_Books.Components.Common;

namespace DotNetNuke.Modules.eCollection_Books.Books
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="BooksCategories" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Books Categories control
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class BooksCategories : eCollection_BooksModuleBase
    {
        public event EventHandler ToggleDivOnOuter;
        public event EventHandler ToggleDivOnOuter1;
        public event EventHandler ToggleGracePeriodInfo;

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AttributeType = "";
                AttributeValue = "";
                BindBooksSelectedInfo();
                if (!Page.IsPostBack)
                {
                    AttributeType = CategoriesDrpList.SelectedItem.Text;
                    BindCategories();
                }
                lblBooksExceededCategory.Text = (GracePeriod.TotalBooks - GracePeriod.SelectedBooks).ToString();
                LiteralBooksRange.InnerText = GetMessage(Constants.MESSAGES_BOOKSRANGE);
                LiteralAlreadyAdded.InnerText = GetMessage(Constants.MESSAGES_ALREADYADDED);
            }
            catch (Exception ex) {LogFileWrite(ex);}

        }

        /// <summary>
        /// 
        /// </summary>
        private void BindBooksSelectedInfo()
        {
            //SelectedBookscount.Text = GracePeriod.SelectedBooks.ToString();
            //SelectedBookscountBot.Text = GracePeriod.SelectedBooks.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddToSub_click(object sender, EventArgs e)
        {
            try
            {
               
                AddBooksToSubscription();
                BindBooksSelectedInfo();
                BindCategories();
                //ToggleGracePeriodInfo(sender, e);
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowRCSuccessMessage", "<script>SuccessMessage();</script>", false);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void botAddToSub_click(object sender, EventArgs e)
        {
            try
            {
                AddBooksToSubscription();
                BindBooksSelectedInfo();
                BindCategories();
                //ToggleGracePeriodInfo(sender, e);
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowBotRCSuccessMessage", "<script>SuccessMessage();</script>", false);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void toggleEventButton_Click(object sender, EventArgs e)
        {
            try
            {
                ToggleGracePeriodInfo(sender, e);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddBooksToSubscription()
        {
            try
            {
                StringBuilder output = new StringBuilder();
                BooksController booksController = BooksController.Instance;
                List<int> productIds = GetSelectedBooks();
                string strXML = string.Empty;
                if (!isBooksExceeded(productIds.Count))
                {

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
                        booksController.AddBooksToSubscription(SelectedSubscriptionId, selectedBooks.ProductsDoc, LoginName);
                    }
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected  void CategoriesDrpList_SelectedIndexChanged(object sender, EventArgs e)
        {            
            AttributeType = CategoriesDrpList.SelectedItem.Text;            
            BindCategories();
            ToggleDivOnOuter1(sender, e);
            
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       protected void SeeallcurrentCategories_Click(object sender, EventArgs e)
       {
           try
           {
               var argValue = ((Button)sender).CommandArgument;
               Button button = (sender as Button);
               //Get the command argument
               AttributeType = CategoriesDrpList.SelectedItem.Text;
               AttributeValue = button.CommandArgument.ToString();
               TabName = "Categories";
           }
           catch (Exception ex) { LogFileWrite(ex); }
           //BookSelectionWizard BookSelpage = (BookSelectionWizard)this.Parent;
           ToggleDivOnOuter(sender, e);                      
           //SeeAllDiv.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/SeeAllCollection.ascx"));           
           //headSpan.InnerText = "Categories";
       }
     
        /// <summary>
        /// 
        /// </summary>
        private void BindCategories()
        {
            AttributeType = CategoriesDrpList.SelectedItem.Text;  
            CategoriesRepeater.DataSource = BooksByCategories;
            CategoriesRepeater.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<int> GetSelectedBooks()
        {
            List<int> productIds = new List<int>();

            foreach (RepeaterItem rpItm in CategoriesRepeater.Items)
            {
                HtmlInputCheckBox checkbox = (HtmlInputCheckBox)rpItm.FindControl("CategoriesCheckBoxes");
                if (checkbox.Checked)
                {
                    Label lblReadingType = (Label)rpItm.FindControl("CategoriesTitleCollection");
                    if (lblReadingType != null && lblReadingType.Text.Trim().Length > 0)
                    {
                        AttributeType = CategoriesDrpList.SelectedItem.Text;
                        AttributeValue = lblReadingType.Text;
                        foreach (Book book in ReadingAgeBooks)
                        {
                            if ((!productIds.Contains(book.PRODUCT_SK)) && (book.AlreadyAvailable == true))
                            {
                                productIds.Add(book.PRODUCT_SK);
                            }
                        }
                    }
                }
            }
            return productIds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CategoriesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                List<string> productIds = new List<string>();
                StringBuilder output = new StringBuilder();
                string strXML = string.Empty;
                BooksController booksController = BooksController.Instance;
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Repeater hiddencategoriesRepeater = (Repeater)e.Item.FindControl("hiddencategoriesRepeater");
                    Label lblReadingType = (Label)e.Item.FindControl("CategoriesTitleCollection");
                    if (lblReadingType != null && lblReadingType.Text.Trim().Length > 0)
                    {
                        AttributeType = CategoriesDrpList.SelectedItem.Text;
                        AttributeValue = lblReadingType.Text;
                    }
                    foreach (Book book in ReadingAgeBooks)
                    {
                        output.AppendFormat("<Products><PRODUCT_SK>{0}</PRODUCT_SK></Products>", book.PRODUCT_SK);
                    }

                    if (output.Length > 0)
                    {
                        strXML = "<root>" + output.ToString() + "</root>";
                        Book selectedBooks = new Book()
                        {
                            ProductsDoc = new XmlDocument() { InnerXml = strXML }
                        };

                        int ProductCount = booksController.IsBooksAdded(SelectedSubscriptionId, selectedBooks.ProductsDoc);
                        List<int> productList = new List<int>();
                        productList.Add(ProductCount);
                        hiddencategoriesRepeater.DataSource = productList;
                        hiddencategoriesRepeater.DataBind();
                    }

                }
            }
            catch (Exception ex) { LogFileWrite(ex); }            
        }
    }
}