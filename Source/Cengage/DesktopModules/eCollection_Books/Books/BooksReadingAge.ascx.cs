using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using System.Text;
using System.IO;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using System.Xml;
using System.Web.UI.HtmlControls;
using DotNetNuke.Modules.eCollection_Books.Components.Common;

namespace DotNetNuke.Modules.eCollection_Books.Books
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="BooksReadingAge" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Books Reading Age 
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class BooksReadingAge : eCollection_BooksModuleBase
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
                ReadingAgeSelect.SelectedIndexChanged += new EventHandler(ReadingAgeSelect_SelectedIndexChanged);
                if (!Page.IsPostBack)
                {
                    SelectedReadingAge = ReadingAgeSelect.SelectedItem.Text;
                    BindReadingAge();
                }
                BindBooksSelectedInfo();
                lblBooksExceededReading.Text = (GracePeriod.TotalBooks - GracePeriod.SelectedBooks).ToString();
                LiteralAlreadyAdded.InnerText = GetMessage(Constants.MESSAGES_ALREADYADDED);
            }
            catch (Exception ex) {LogFileWrite(ex);}
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindBooksSelectedInfo()
        {
         //   SelectedBookscount.Text = GracePeriod.SelectedBooks.ToString();
         //   SelectedBookscountBot.Text = GracePeriod.SelectedBooks.ToString();
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
                BindReadingAge();
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowRASuccessMessage", "<script>SuccessMessage();</script>", false);
            }
            catch (Exception ex) {LogFileWrite(ex);}
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
                BindReadingAge();
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowBotRASuccessMessage", "<script>SuccessMessage();</script>", false);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReadingAgeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ReadingAgeSelect.SelectedItem.Text.Equals("[READINGAGE]"))
            {
                SelectedReadingAge = ReadingAgeSelect.SelectedItem.Text;
                BindReadingAge();
                ToggleDivOnOuter1(sender, e);
                //ReadingAgeRepeaterPanel.Update();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SeeallcurrentReadingAge_Click(object sender, EventArgs e)
        {
            try
            {
                var argValue = ((Button)sender).CommandArgument;
                Button button = (sender as Button);
                //Get the command argument
                AttributeType = "READING AGE";
                AttributeValue = button.CommandArgument.ToString();
                TabName = "Reading Age";
            }
            catch (Exception ex) { LogFileWrite(ex); }

            ToggleDivOnOuter(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BacktoCollectionButton_Click(object sender, EventArgs e)
        {
            //BacktoCollectionButton.Style.Add("display", "none");
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindReadingAge()
        {
            SelectedReadingAge = ReadingAgeSelect.SelectedItem.Text;
            ReadingAgeRepeater.DataSource = ReadingAge;
            ReadingAgeRepeater.DataBind();
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
        /// <returns></returns>
        protected List<int> GetSelectedBooks()
        {
            List<int> productIds = new List<int>();

            foreach (RepeaterItem rpItm in ReadingAgeRepeater.Items)
            {
                HtmlInputCheckBox checkbox = (HtmlInputCheckBox)rpItm.FindControl("ReadingCheckBoxes");
                if (checkbox.Checked)
                {
                    Label lblReadingType = (Label)rpItm.FindControl("ReadingTitleCollection");
                    if (lblReadingType != null && lblReadingType.Text.Trim().Length > 0)
                    {
                        AttributeType = "READING AGE";
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
        protected void ReadingAgeRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                    Label lblReadingType = (Label)e.Item.FindControl("ReadingTitleCollection");
                    if (lblReadingType != null && lblReadingType.Text.Trim().Length > 0)
                    {
                        AttributeType = "READING AGE";
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