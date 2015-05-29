using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;

namespace DotNetNuke.Modules.eCollection_Books.Books
{
    public partial class BookPacks : eCollection_BooksModuleBase
    {
        int customBooksCount = 0;
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
                        bookPackList.Add(new BookPack() { BookPackSk = Null.SetNullInteger(reader["BOOK_PACK_SK"]), BookPackName = Null.SetNullString(reader["BOOK_PACK_NAME"]), PackDescription = Null.SetNullString(reader["PACK_DESCRIPTION"]), BookPackType = Null.SetNullString(reader["BOOK_PACK_TYPE"]) });
                    }
                    
                    reader.NextResult();
                    while (reader.Read())
                    {
                        customBooksCount = Null.SetNullInteger(reader["MY_BOOK_PACK_COUNT"]);
                        if (customBooksCount < 100)
                        {
                            CustomButton.Visible = true;
                            //EditCustomButton.Visible = true;
                        }                       
                        else
                        {
                            CustomButton.Visible = false;
                            //EditCustomButton.Visible = false;
                        }
                    }

                    BookPackRptr.DataSource = bookPackList;
                    BookPackRptr.DataBind();

                    if (GracePeriod.DaysLeft == 1)
                    {
                        DaysLeftLbl.InnerText = GracePeriod.DaysLeft.ToString() + " day";
                    }
                    else if (GracePeriod.DaysLeft >= 0)
                    {
                        DaysLeftLbl.InnerText = GracePeriod.DaysLeft.ToString() + " days";
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
        }

        protected void BookPackRptr_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                HtmlControl editButton = (HtmlControl)e.Item.FindControl("EditPack");
                HiddenField bookTypeHdn = (HiddenField)e.Item.FindControl("BookTypeHdn");
                if (customBooksCount == 100 && bookTypeHdn.Value == "CUSTOM")
                {
                    editButton.Visible = true;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }


       
    }
}