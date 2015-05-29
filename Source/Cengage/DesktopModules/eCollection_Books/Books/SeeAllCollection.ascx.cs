using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using System.Text;
using System.IO;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace DotNetNuke.Modules.eCollection_Books.Books
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SeeAllCollection" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    See All Collection screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class SeeAllCollection : eCollection_BooksModuleBase
    {
        public event EventHandler ToggleDivOnOuter;

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                headSpan.InnerText = TabName;

                if (!Page.IsPostBack)
                    BindReadingAgeBooks();

                BindBooksSelectedInfo();
                lblBooksExceededSeeAll.Text = (GracePeriod.TotalBooks - GracePeriod.SelectedBooks).ToString();
            }
            catch (Exception ex) { LogFileWrite(ex); }
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
        protected void BacktoCollection_Click(object sender, EventArgs e)
        {
            //ToggleDivOnOuter(sender, e);
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=BookSelectionWizard");
        }

        /// <summary>
        /// 
        /// </summary>
        public void BindReadingAgeBooks()
        {
            headSpan.InnerText = TabName;
            RepSeeAllBooks.DataSource = ReadingAgeBooks;
            RepSeeAllBooks.DataBind();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void test_Click(object sender, EventArgs e)
        {

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
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowSASuccessMessage", "<script>SuccessMessage();</script>", false);
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
                ScriptManager.RegisterStartupScript(Page, GetType(), "ShowBotSASuccessMessage", "<script>SuccessMessage();</script>", false);
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
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=BookSelectionWizard");
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
        /// <returns></returns>
        protected List<int> GetSelectedBooks()
        {
            List<int> productIds = new List<int>();

            foreach (RepeaterItem rpItm in RepSeeAllBooks.Items)
            {
                HtmlInputCheckBox checkbox = (HtmlInputCheckBox)rpItm.FindControl("BooksCheckBox");
                if (checkbox.Checked)
                {
                    Label lblPRODUCT_SK = (Label)rpItm.FindControl("lblPRODUCT_SK");
                    if (lblPRODUCT_SK != null && lblPRODUCT_SK.Text.Trim().Length > 0)
                    {
                        productIds.Add(Convert.ToInt32(lblPRODUCT_SK.Text));
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
        protected void RepSeeAllBooks_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

    }
}