using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using System.Text;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using System.Xml;
using System.IO;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Books.Components.Common;

namespace DotNetNuke.Modules.eCollection_Books.Books
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="BooksLevels" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Books Levels screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class BooksLevels : eCollection_BooksModuleBase
    {
        public event EventHandler ToggleDivOnOuter;
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
                if (!Page.IsPostBack)
                {
                    BindLevels();
                }
                BindBookSelectedInfo();
                lblBooksExceeded.Text = (GracePeriod.TotalBooks - GracePeriod.SelectedBooks).ToString();
                LiteralAlreadyAdded.InnerText = GetMessage(Constants.MESSAGES_ALREADYADDED);
            }
            catch (Exception ex) { LogFileWrite(ex); }
            //lblBooksExceeded.Text = "0";
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindBookSelectedInfo()
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
                BindBookSelectedInfo();
                BindLevels();
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowRLSuccessMessage", "<script>SuccessMessage();</script>", false);
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
                BindBookSelectedInfo();
                BindLevels();
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowBotRLSuccessMessage", "<script>SuccessMessage();</script>", false);
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
        protected void SeeallcurrentLevel_Click(object sender, EventArgs e)
        {
            try
            {
                var argValue = ((Button)sender).CommandArgument;
                Button button = (sender as Button);
                //Get the command argument
                AttributeType = "GUIDED READING LEVEL";
                AttributeValue = button.CommandArgument.ToString();
                TabName = "Level collections";
            }
            catch (Exception ex) { LogFileWrite(ex); }

            ToggleDivOnOuter(sender, e);            

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BooksRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                List<string> productIds = new List<string>();
                StringBuilder output = new StringBuilder();
                string strXML = string.Empty;
                BooksController booksController = BooksController.Instance;

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Literal colorLiteral = (Literal)e.Item.FindControl("ColorValue");
                    Repeater booksLevelRepeater = (Repeater)e.Item.FindControl("BooksLevelRepeater");
                    Literal ColorValue = (Literal)e.Item.FindControl("ColorValue");
                    HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("LevelContentDiv");

                    div.Style.Add("background", ColorValue.Text);

                    if (ColorValue.Text == "Magenta")
                    {
                        div.Style.Add("background", "rgb(195, 0, 95)");
                    }
                    if (ColorValue.Text == "Red")
                    {
                        div.Style.Add("background", "rgb(194,0,22)");
                    }
                    if (ColorValue.Text == "Yellow")
                    {
                        div.Style.Add("background", "rgb(234,184,20)");
                    }
                    if (ColorValue.Text == "Blue")
                    {
                        div.Style.Add("background", "rgb(0,95,170)");
                    }
                    if (ColorValue.Text == "Green")
                    {
                        div.Style.Add("background", "rgb(0,105,50)");
                    }
                    if (ColorValue.Text == "Orange")
                    {
                        div.Style.Add("background", "rgb(208,101,25)");
                    }
                    if (ColorValue.Text == "Turquoise")
                    {
                        div.Style.Add("background", "rgb(117,176,160)");
                    }
                    if (ColorValue.Text == "Purple")
                    {
                        div.Style.Add("background", "rgb(73,40,127)");
                    }
                    if (ColorValue.Text == "Gold")
                    {
                        div.Style.Add("background", "rgb(217,138,0)");
                    }
                    if (ColorValue.Text == "Silver")
                    {
                        div.Style.Add("background", "rgb(152,165,174)");
                    }


                    AttributeType = "GUIDED READING LEVEL";
                    if (colorLiteral != null)
                        AttributeValue = colorLiteral.Text;
                    booksLevelRepeater.DataSource = Levels;
                    booksLevelRepeater.DataBind();

                    //Repeater hiddencategoriesRepeater = (Repeater)e.Item.FindControl("hiddencategoriesRepeater");

                    //Repeater nestedRepeater = (Repeater)e.Item.FindControl("BooksLevelRepeater");
                    //foreach (RepeaterItem nestedItm in nestedRepeater.Items)
                    //{

                    //    Label lblAttributeType = (Label)nestedItm.FindControl("lblAttributeType");
                    //    if (lblAttributeType != null && lblAttributeType.Text.Trim().Length > 0)
                    //    {
                    //        AttributeType = "GUIDED READING LEVEL";
                    //        AttributeValue = lblAttributeType.Text;
                    //        foreach (Book book in ReadingAgeBooks)
                    //        {
                    //            output.AppendFormat("<Products><PRODUCT_SK>{0}</PRODUCT_SK></Products>", book.PRODUCT_SK);
                    //        }
                    //    }

                    //    if (output.Length > 0)
                    //    {
                    //        strXML = "<root>" + output.ToString() + "</root>";
                    //        Book selectedBooks = new Book()
                    //        {
                    //            ProductsDoc = new XmlDocument() { InnerXml = strXML }
                    //        };

                    //        int ProductCount = booksController.IsBooksAdded(SelectedSubscriptionId, selectedBooks.ProductsDoc);
                    //        List<int> productList = new List<int>();
                    //        productList.Add(ProductCount);
                    //        hiddencategoriesRepeater.DataSource = productList;
                    //        hiddencategoriesRepeater.DataBind();
                    //    }
                    //}
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BooksLevelRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                    Label lblReadingType = (Label)e.Item.FindControl("lblAttributeType");
                    if (lblReadingType != null && lblReadingType.Text.Trim().Length > 0)
                    {
                        AttributeType = "GUIDED READING LEVEL";
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
        private void BindLevels()
        {
            AttributeType = "COLOUR LEVEL";           
            BooksRepeater.DataSource = Categories.OrderBy(x=> x.ColourOrder);
            BooksRepeater.DataBind();
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
            }catch (Exception ex) {LogFileWrite(ex);}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<int> GetSelectedBooks()
        {
            List<int> productIds = new List<int>();

            foreach (RepeaterItem rpItm in BooksRepeater.Items)
            {
                HtmlInputCheckBox checkbox = (HtmlInputCheckBox)rpItm.FindControl("BookColourLevelCheckBox");
                
                    Repeater nestedRepeater = (Repeater)rpItm.FindControl("BooksLevelRepeater");
                    foreach (RepeaterItem nestedItm in nestedRepeater.Items)
                    {
                        HtmlInputCheckBox checkbox1 = (HtmlInputCheckBox)nestedItm.FindControl("CheckBoxes");
                        if (checkbox1.Checked)
                        {
                            Label lblAttributeType = (Label)nestedItm.FindControl("lblAttributeType");
                            if (lblAttributeType != null && lblAttributeType.Text.Trim().Length > 0)
                            {
                                AttributeType = "GUIDED READING LEVEL";
                                AttributeValue = lblAttributeType.Text;
                                foreach (Book book in ReadingAgeBooks)
                                {

                                    if ((!productIds.Contains(book.PRODUCT_SK)) &&(book.AlreadyAvailable==true) )
                                    {
                                        productIds.Add(book.PRODUCT_SK);
                                    }                                    
                                }
                            }
                        }
                    }                    
                
            }
            return productIds;
        }
    }
}