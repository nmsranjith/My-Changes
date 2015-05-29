using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using System.Data;
using DotNetNuke.Entities.Portals;
using System.Text;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using System.Web.UI.HtmlControls;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using DotNetNuke.Common.Utilities;
using System.Configuration;


namespace DotNetNuke.Modules.eCollection_Sessions.Session
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="AddBooksToSession" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Add books to session
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class AddBooksToSession : eCollection_SessionsModuleBase
    {
        #region Events
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
                    BuildSelectedItems();
                    BindBooks(10, 0, 0);
                }
                Session["BooksCategories"] = CategoriesDrpList.SelectedItem.Text;
            }
            catch (Exception ex){ LogFileWrite(ex);} 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackToSession_Click(object sender, EventArgs e)
        {
            DataCache.RemoveCache(string.Format("GetSessionBooksByReadingLevel{0}", SelectedSubscriptionId));
            GetSelectedBooks();
            if (Request.QueryString["returnurl"] == null)
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession");
            else
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession&returnurl=" + Request.QueryString["returnurl"]);       
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackToSession_Click1(object sender, EventArgs e)
        {
            DataCache.RemoveCache(string.Format("GetSessionBooksByReadingLevel{0}", SelectedSubscriptionId));
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=createsession");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="take"></param>
        /// <param name="pageSize"></param>
        /// <param name="startCount"></param>
        private void BindBooks(int take, int pageSize, int startCount)
        {
            SessionController bookController = SessionController.Instance;
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

                List<Books> bookList = bookController.GetBooksByReadingLevel(SelectedSubId, startLevel, endLevel, LoginName);
                ConstRowCount = bookList.Count;
                string[] selectedBook = ProductsSelected.Select(u => u.CUST_SUBS_ITEM_SK.ToString()).ToArray<string>();
                foreach (string custSubItmSK in selectedBook)
                {
                    custItmSKHidden.Value += custSubItmSK + ",";
                }
                List<string> seletedId = custItmSKHidden.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToList<string>();

                bookList.ForEach(x => { x.Checked = false; x.ClassName = "bookRowStyle"; x.CheckImgPathName = "circle_big.png"; });

                bookList.Where(u => selectedBook.Contains(u.CUST_SUBS_ITEM_SK.ToString())).ToList<Books>().ForEach(x => { x.Checked = true; x.ClassName = "bookRowStyle rowclick"; x.CheckImgPathName = "tick_student.png"; });
                if (seletedId.Count > 0)
                {
                    bookList.Where(u => seletedId.Contains(u.CUST_SUBS_ITEM_SK.ToString())).ToList<Books>().ForEach(x => { x.Checked = true; x.ClassName = "bookRowStyle rowclick"; x.CheckImgPathName = "tick_student.png"; });
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
                    MessageDivUpdatePanel.Update();
                }
                else if (bookList.Count == 0)
                {
                    CollectionRepeater.DataSource = bookList;
                    CollectionRepeater.DataBind();
                    MessageOuterDiv.Style.Add("Display", "block");
                    CustomPaging.DisplayPropertyForPage("none");
                    Message1.Text = Constants.NOBOOKSINFO;
                    MessageDivUpdatePanel.Update();
                    BooksUpdatePanel.Update();
                }
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
            DataCache.RemoveCache(string.Format("GetSessionBooksByReadingLevel{0}", SelectedSubscriptionId));
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
        #endregion

        #region MemberFunction
        /// <summary>
        /// 
        /// </summary>
        private void BuildSelectedItems()
        {
            try
            {
                StringBuilder output = new StringBuilder();
                List<SessionMembers> SelectedGroups = GroupsSelected;
                foreach (SessionMembers sm in SelectedGroups)
                {
                    if (sm.MemberType == "GROUP" && sm.GroupName.Trim().Length > 0)
                    {
                        output.AppendFormat("<li class=\"SelectedGroupItem\"><span>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'RemoveGroup(this);\'>x</a></li>", TruncateName(sm.GroupName), sm.GRP_MEM_SK);
                        SelectedValueGroupTextBox.Text += sm.GRP_MEM_SK + ",";
                    }
                    if (sm.MemberType == "USER" && sm.StudentName.Trim().Length > 0)
                    {
                        output.AppendFormat("<li class=\"SelectedStudentItem\"><span>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(this);\'>x</a></li>", TruncateName(sm.StudentName), sm.CUST_SUBS_USER_SK);
                        SelectedValueTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                    }
                    if (sm.MemberType == "USER" && sm.TeacherName.Trim().Length > 0)
                    {
                        output.AppendFormat("<li class=\"SelectedTeacherItem\"><span>{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'RemoveTeacher(this);\'>x</a></li>", TruncateName(sm.TeacherName), sm.CUST_SUBS_USER_SK);
                        SelectedValueTeacherTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                    }

                }
                SelectedStudentList.InnerHtml = output.ToString();
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }

        /// <summary>
        /// 
        /// </summary>
        protected void GetSelectedBooks()
        {
            ProductsSelected = null;
            try
            {
                SelectedSubId = Convert.ToInt32(Session["Subscription"]);
                
                List<string> seletedId = custItmSKHidden.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToList<string>();
                SessionController bookController = SessionController.Instance;
                List<Books> bookList = bookController.GetBooksByReadingLevel(SelectedSubId, 1, 24, LoginName);

                foreach (string lstItems in seletedId)
                {                    
                    if (lstItems != null)
                    {
                        SessionProducts _sessionProduct = new SessionProducts()
                        {
                            CUST_SUBS_ITEM_SK = Convert.ToInt32(lstItems),
                            Books_AddedDate = DateTime.Now,
                            Active = "Y"
                        };
                        Books book = bookList.Find(e => e.CUST_SUBS_ITEM_SK == Convert.ToInt32(lstItems));
                        if (book != null)
                            _sessionProduct.ImageFileName = book.IMAGE_FILE_NAME;                       
                        ProductsSelected.Add(_sessionProduct);
                    }
                }                
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string TruncateName(string Name)
        {
            return (Name.ToString().Length >= 25 ? Name.ToString().Substring(0, 23) + "..." : Name.ToString());
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
                SessionController.Instance.GetSearchBook(SearchTextBox.Text.Replace(", ", ",").Trim() == string.Empty ? "%" : SearchTextBox.Text.Replace(", ", ",").Trim(), int.Parse(Session["Subscription"].ToString()), Session["BooksCategories"].ToString());
                BuildSelectedItems();
                GetSelectedBooks();
                BindBooks(10, 0, 0);
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
            }
            catch (Exception ex) { LogFileWrite(ex); } 
        }

        #endregion

    }
}