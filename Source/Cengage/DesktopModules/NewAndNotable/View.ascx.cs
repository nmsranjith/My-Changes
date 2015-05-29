/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   New and Notable products
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */

using System;
using System.Configuration;
using System.Data;
using System.Web.Services;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Instrumentation;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Controller;
using DotNetNuke.Modules.NewAndNotable.Components.Controller;
using DotNetNuke.Modules.NewAndNotable.Components.Model;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using System.Data.SqlClient;
using System.Web.UI;
using System.Net;
using System.Collections.Generic;


namespace DotNetNuke.Modules.NewAndNotable
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="View" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    The View class displays the content
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    
    public partial class View : NewAndNotableModuleBase, IActionable
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            Visitor visitor = null;
            NewModel newModel = null;
            bool isSeeAll = false;
            try
            {
                if (!Page.IsPostBack)
                {
                   FillNewProducts(visitor, newModel, isSeeAll);
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                DnnLog.Error("Exception in NewProduct Module is :" + exc.InnerException);
                DnnLog.Error("Exception in NewProduct Module is :" + exc.Message);
            }
        }

        private void FillNewProducts(Visitor visitor, NewModel newModel,bool isSeeAll)
        {
            try
            {
                newModel = new NewModel();
                LoadModuleInfo(newModel);

                visitor = (Session["UserInfo"] != null)
                           ? Session["UserInfo"] as Visitor
                           : new Visitor();

                newModel.IsSeeAll = isSeeAll;
                newModel.UserSK = visitor.UserID;
                newModel.Country = visitor.CountryCode;
                newModel.IsbnTable = FormatNewIsbnstable(newModel);

                SqlDataReader newReader = NewAndNotableController.Instance.GetNewProducts(newModel);
                if (newReader.HasRows)
                {
                    divNew.Visible = true;
                    List<Products> productList = new List<Products>();
                    while (newReader.Read())
                    {
                        productList.Add(new Products()
                        {
                            TITLE = Null.SetNullString(newReader["TITLE"]),
                            ToolTip = WebUtility.HtmlEncode(Null.SetNullString(newReader["TITLE"])),
                            IMAGE_FILE_NAME = string.Concat(FormatImageURL(), Null.SetNullString(newReader["IMAGE_FILE_NAME"])),
                            PREFERRED_NAME = Null.SetNullString(newReader["PREFERRED_NAME"]),
                            ISBN_13 = Null.SetNullString(newReader["ISBN_13"]),
                            PRODUCT_SK = Null.SetNullInteger(newReader["PRODUCT_SK"]),
                            PUBLICATION_DATE = Null.SetNullString(newReader["PUBLICATION_DATE"]),
                           // PRINT_PRICE = Null.SetNullString(newReader["PRINT_PRICE"]),
                           // EBOOK = Null.SetNullString(newReader["EBOOK"]),
                           // ECHAPTER = Null.SetNullString(newReader["ECHAPTER"]),
                           // SUBPRODUCT_TYPE = Null.SetNullString(newReader["SUBPRODUCT_TYPE"]),
                            AUDIENCE_TARGET = Null.SetNullString(newReader["AUDIENCE_TARGET"]),
                            NEW_EDITION = Null.SetNullString(newReader["NEW_EDITION"]),
                            EDITION = Null.SetNullString(newReader["EDITION"]),
                            SUPPLEMENTS = Null.SetNullString(newReader["SUPPLEMENTS"]),
                            //CODE = Null.SetNullString(newReader["CODE"]),
                            //CODE_NAME = Null.SetNullString(newReader["CODE_NAME"]),
                            //PRODUCT_FORMAT = Null.SetNullString(newReader["PRODUCT_FORMAT"]),
                            FAVOURITE_FLAG = Null.SetNullString(newReader["FAVOURITE_FLAG"]),
                            DetailUrl = CengageSearchResult.getHEProductURL(Null.SetNullString(newReader["ISBN_13"]), Null.SetNullString(newReader["TITLE"]),GetDivision())
                        });
                    }


                    newProductResultsRptr.DataSource = productList;
                    newProductResultsRptr.DataBind();
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "ShowNew", "<script>ShowNew()</script>", false);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ShowNew", "ShowNew();", true);
                    DnnLog.Info("newModel.IsbnTable.Rows.Count-->" + newModel.IsbnTable.Rows.Count);
                    DnnLog.Info("newModel.DisplayCount-->" + newModel.DisplayCount);
                    if (newModel.IsbnTable.Rows.Count > newModel.DisplayCount)
                    {
                        if (newModel.IsSeeAll)
                        {
                            divNewSeeAll.Visible = false;
                            divNewHideAll.Visible = true;
                        }
                        else
                        {
                            divNewSeeAll.Visible = true;
                            divNewHideAll.Visible = false;
                        }
                    }
                    else
                    {
                        divNewSeeAll.Visible = false;
                        divNewHideAll.Visible = false;
                        DnnLog.Info("Got exception in sql new&notable procedure");
                    }
                }
                else
                    divNew.Visible = false;
            }
            catch (Exception ex) { LogFileWrite(ex); DnnLog.Error("New and Notable FillNewProducts Method Exception"); DnnLog.Error(ex.Message); }
        }

        private DataTable FormatNewIsbnstable(NewModel newModel)
        {
            DataTable newIsbnTable = new DataTable();
            newIsbnTable.Columns.Add("ISBN", typeof(string));
            string[] isbn = newModel.Isbns.Split(',');
            DnnLog.Error("isbns are : " + isbn);
            if (isbn.Length > 0)
            {
                for (int i = 0; i < isbn.Length; i++)
                {
                    if(isbn[i].Trim()!=string.Empty)
                        newIsbnTable.Rows.Add(isbn[i]);
                    DnnLog.Error("isbns are : " + isbn[i]);
                }
            }
            return newIsbnTable;
        }

        private void LoadModuleInfo(NewModel newModel)
        {
            IDataReader reader = null;
            try
            {
                reader = FavouriteController.Instance.GetModuleFavouriteSaveChanges(this.ModuleId);
                if (reader.Read())
                {
                    newModel.Content = reader["Content"].ToString();
                    if (newModel.Content != "")
                    {
                        string[] split = newModel.Content.Split('Ñ');
                        spanTitle.InnerText = split[0];
                        newModel.Division = GetDivision();
                        newModel.Isbns = split[1].Trim(',');
                        newModel.DisplayCount = int.Parse(split[2]);
                    }
                }
                else
                {
                    FavouriteController.Instance.InsertFavouriteSaveChanges(this.ModuleId, "ÑÑÑ", UserId);
                }

            }
            catch (Exception exc)
            {
                DnnLog.Error("Exception in NewProduct Module is :" + exc.InnerException);
                DnnLog.Error("Exception in NewProduct Module is :" + exc.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            
        }

        private string GetDivision()
        {

            string PageName = PortalSettings.ActiveTab.TabName;
            string PageNameValue = "";
            if (PageName.StartsWith("higher"))
            {
                PageNameValue = "higher";
            }
            else if (PageName.StartsWith("vpg"))
            {
                PageNameValue = "vocational";
            }
            else if (PageName.StartsWith("gale"))
            {
                PageNameValue = "gale";
            }
            else
            {
                //PageNameValue = "";
                PageNameValue = PortalSettings.ActiveTab.TabPath.Contains("/gale/") ? "gale" : (PortalSettings.ActiveTab.TabPath.Contains("/higher/") ? "higher" : "vocational");
            }
            return PageNameValue;
        }

        protected void btnNewSeeAll_Click(object sender, EventArgs e)
        {
            Visitor visitor = null;
            NewModel newModel = null;
            bool isSeeAll = true;
            try
            {
                divNewSeeAll.Visible = false;
                divNewHideAll.Visible = true;
                FillNewProducts(visitor, newModel, isSeeAll);
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                DnnLog.Error("Exception in NewProduct Module is :" + exc.InnerException);
                DnnLog.Error("Exception in NewProduct Module is :" + exc.Message);
            }
        }
        protected void btnNewHideAll_Click(object sender, EventArgs e)
        {
            Visitor visitor = null;
            NewModel newModel = null;
            bool isSeeAll = false;
            try
            {
                divNewSeeAll.Visible = true;
                divNewHideAll.Visible = false;
               FillNewProducts(visitor, newModel, isSeeAll);
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                DnnLog.Error("Exception in NewProduct Module is :" + exc.InnerException);
                DnnLog.Error("Exception in NewProduct Module is :" + exc.Message);
            }
        }
        #endregion
        /// <summary>
        /// Book image format url function
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        [WebMethod]
        protected string FormatImageURL()
        {
            if (GetDivision()!="gale")
                return Null.SetNullString(ConfigurationManager.AppSettings["BooksImgPath"]+"HER");
            else
                return Null.SetNullString(ConfigurationManager.AppSettings["BooksImgPath"]+"GR");
        }

        /// <summary>
        /// Product detail page's url format  function
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        [WebMethod]
        protected string FormatURL(string title, string ISBN)
        {
            return CengageSearchResult.getHEProductURL(ISBN, title, GetDivision());
        }
        #region Optional Interfaces

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), Localization.GetString("EditModule", this.LocalResourceFile), "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

        #endregion

    }

}
