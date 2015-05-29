using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;

namespace DotNetNuke.Modules.eCollection_Books.DashBoard
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="BookListDashBoard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Left panel for BookList DashBoard
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class BookListDashBoard : eCollection_BooksModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StartReadingSessionButton_Click(object sender, EventArgs e)
        {
            int SelectedSubId = 0;
            string[] SelectedID = selectedSessionID.Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            List<int> IDCollectionList = new List<int>();
            List<eCollection_Sessions.Components.Modal.SessionProducts> sessionproducts = new List<eCollection_Sessions.Components.Modal.SessionProducts>();
            try
            {
                SelectedSubId = Convert.ToInt32(Session["Subscription"]);

                BooksController bookController = BooksController.Instance;
                List<Book> bookList = bookController.GetBooksByReadingLevel(SelectedSubId, 1, 24, LoginName);
                //List<Books> bookList = bookController.GetBooksByReadingLevel(SelectedID, 1, 24, LoginName);
                foreach (string productId in SelectedID)
                {
                    //SelectedSessionId.Append(int.Parse((rpItm.FindControl("SessionId") as Label).Text));        
                    eCollection_Sessions.Components.Modal.SessionProducts _sessionProduct = new eCollection_Sessions.Components.Modal.SessionProducts()
                    {
                        CUST_SUBS_ITEM_SK = Convert.ToInt32(productId),
                        Books_AddedDate = DateTime.Now,
                        Active = "Y"
                    };
                    Book book = bookList.Find(e1 => e1.CUST_SUBS_ITEM_SK == Convert.ToInt32(productId));
                    if (book != null)
                        _sessionProduct.ImageFileName = book.IMAGE_FILE_NAME;
                    sessionproducts.Add(_sessionProduct);
                }

                Session["SelectedGroups"] = null;
                Session["SelectedProducts"] = null;
                Session["SelectedSubscriptionId"] = null;
                //SelectedValueTextBox.Text = string.Empty;
                //SelectedValueGroupTextBox.Text = string.Empty;
                Session["EditSelectedId"] = null;
                Session["SessionNotes"] = string.Empty;
                Session["SessionType"] = string.Empty;
                Session["SessionExpiryDate"] = null;
                Session["SessionName"] = string.Empty;
                Session["SelectedProducts"] = sessionproducts;
                string cacheKey = string.Format("GetBooksByReadingLevel{0}", SelectedSubscriptionId);
                DataCache.RemoveCache(cacheKey);

                Response.Redirect(Globals.NavigateURL(GetTabID(SessionsModule)) + "?pagename=" + SessionParameter + "&returnurl=books");
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
    }
}