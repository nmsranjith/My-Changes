/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   FavouriteAndRecentlyViewed
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Instrumentation;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Components.Model;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Controller;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;


namespace DotNetNuke.Modules.FavouriteAndRecentlyViewed
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="View" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To show Favourite and Recently viewed products
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------

    public partial class View : FavouriteAndRecentlyViewedModuleBase, IActionable
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
            FavouriteModel favourite = null;
            HttpCookie recentProductCookie = null;
            bool isSeeAll = false;
            try
            {
                if (!Page.IsPostBack)
                {
                   FillFavourites(visitor, favourite, recentProductCookie, isSeeAll);
                }

            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                DnnLog.Error("Exception in Favourite Module is :" + exc.InnerException);
                DnnLog.Error("Exception in Favourite Module is :" + exc.Message);
            }
        }

        private void FillFavourites(Visitor visitor, FavouriteModel favourite, HttpCookie recentProductCookie, bool isSeeAll)
        {
            try
            {
                favourite = new FavouriteModel();
                LoadModuleInfo(favourite);
                visitor = (Session["UserInfo"] != null)
                           ? Session["UserInfo"] as Visitor
                           : new Visitor();
                DnnLog.Fatal("favourite.IsRecent --> " + favourite.IsRecent);
                recentProductCookie = (visitor.UserID == 0)
                                       ? HttpContext.Current.Request.Cookies["Anon-RecentlyViewed-Products"]
                                       : ((favourite.IsRecent == 'Y')
                                       ? HttpContext.Current.Request.Cookies[visitor.UserID + "RecentlyViewed-Products"]
                                       : null);
                DnnLog.Fatal("User logged in : " + visitor.UserID);
                if (recentProductCookie != null)
                    DnnLog.Fatal("recently viewed products list *** --> " + recentProductCookie.Values[0]);
                favourite.IsSeeAll = isSeeAll;
                favourite.UserSK = visitor.UserID;
                favourite.Country = visitor.CountryCode;
                favourite.RecentDataTable = FormateRecentProductTable(recentProductCookie);
                //ViewState["FavouriteInfo"] = favourite;
                SqlDataReader favReader = FavouriteController.Instance.GetFavouriteItems(favourite);
                DnnLog.Fatal("number of products returned are .... " + favReader.Depth);
                if (favReader.HasRows)
                {
                    divFavourite.Visible = true;
                    favUpdatePanel.Visible = true;
                    ProductResultsRptr.DataSource = favReader;
                    ProductResultsRptr.DataBind();

                    favReader.NextResult();
                    if (favReader.HasRows)
                    {
                        int noOfTotalFavourites = 0;
                        while (favReader.Read())
                        {
                            noOfTotalFavourites = int.Parse(favReader["TOTALCOUNT"].ToString());
                        }
                        if (noOfTotalFavourites > favourite.DisplayCount)
                        {
                            if (favourite.IsSeeAll)
                            {
                                divHideAll.Visible = true;
                                divSeeAll.Visible = false;
                            }
                            else
                            {
                                divHideAll.Visible = false;
                                divSeeAll.Visible = true;
                            }
                        }
                        else
                        {
                            divHideAll.Visible = false;
                            divSeeAll.Visible = false;
                            DnnLog.Info("Got exception in sql favourite procedure");
                        }
                    }
                    else
                    {
                        if (favourite.IsSeeAll)
                        {
                            divHideAll.Visible = true;
                            divSeeAll.Visible = false;
                        }
                        else
                        {
                            divHideAll.Visible = false;
                            divSeeAll.Visible = true;
                        }
                    }
                }
                else
                {
                    favUpdatePanel.Visible = false;
                    divFavourite.Visible = false;
                }

            }
            catch (Exception ex)
            {

            }
        }

        private static DataTable FormateRecentProductTable(HttpCookie recentProductCookie)
        {
            DataTable recentProducts = new DataTable();
            recentProducts.Columns.Add("ISBN", typeof(string));
            recentProducts.Columns.Add("ADDED_DATE", typeof(string));
            string[] data,data1;
            if (recentProductCookie != null)
            {
                data = recentProductCookie.Values[0].Split(',');
                if (data.Length - 1 == 0)
                {
                    data1=data[0].Split('|');
                    recentProducts.Rows.Add(data1[0],data1[1]);
                }
                else
                {   for (int i = 0; i < data.Length; i++)
                    {
                        data1 = data[i].Split('|');
                        recentProducts.Rows.Add(data1[0],data1[1]);                        
                    }
                }               
            }
            return recentProducts;
        }

        #endregion
        private void LoadModuleInfo(FavouriteModel favourite)
        {
            string[] split = null;
            IDataReader reader = null;
            try
            {
                reader = FavouriteController.Instance.GetModuleFavouriteSaveChanges(this.ModuleId);
                if (reader.Read())
                {
                    favourite.Content = reader["Content"].ToString();
                    if (favourite.Content != string.Empty)
                    {
                        split = favourite.Content.Split('Ñ');
                        spanTitle.InnerText = split[0];
                        favourite.Division = GetDivision();
                        switch (split[1].ToLower())
                        {
                            case "both":
                                favourite.IsFavourite = 'Y';
                                favourite.IsRecent = 'Y';
                                break;
                            case "fav":
                                favourite.IsFavourite = 'Y';
                                favourite.IsRecent = 'N';
                                break;
                            case "recent":
                                favourite.IsFavourite = 'N';
                                favourite.IsRecent = 'Y';
                                break;
                            default:
                                favourite.IsFavourite = 'N';
                                favourite.IsRecent = 'N';
                                break;
                        }

                        favourite.DisplayCount = int.Parse(split[2]);
                    }

                }
                else
                {
                    FavouriteController.Instance.InsertFavouriteSaveChanges(this.ModuleId, "ÑÑÑ", UserId);
                }

            }
            catch (Exception exc)
            {
                DnnLog.Error("Exception in Favourite Module is :" + exc.InnerException);
                DnnLog.Error("Exception in Favourite Module is :" + exc.Message);
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
            //switch()
            DnnLog.Error("division in Favourite Module :" + PageNameValue);
            return PageNameValue;
        }

        protected void btnFavSeeAll_Click(object sender, EventArgs e)
        {
            Visitor visitor = null;
            FavouriteModel favourite = null;
            HttpCookie recentProductCookie = null;
            bool isSeeAll = true;
            try
            {
                divHideAll.Visible = true;
                divSeeAll.Visible = false;
                FillFavourites(visitor, favourite, recentProductCookie, isSeeAll);
                favUpdatePanel.Update();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                DnnLog.Error("Exception in Favourite Module is :" + exc.InnerException);
                DnnLog.Error("Exception in Favourite Module is :" + exc.Message);
            }
        }

        protected void btnFavHideAll_Click(object sender, EventArgs e)
        {
            Visitor visitor = null;
            FavouriteModel favourite = null;
            HttpCookie recentProductCookie = null;
            bool isSeeAll = false;
            try
            {
                divHideAll.Visible = false;
                divSeeAll.Visible = true;
                FillFavourites(visitor, favourite, recentProductCookie, isSeeAll);
                favUpdatePanel.Update();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                DnnLog.Error("Exception in Favourite Module is :" + exc.InnerException);
                DnnLog.Error("Exception in Favourite Module is :" + exc.Message);
            }
        }

        /// <summary>
        /// Book image format url function
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        [WebMethod]
        protected string FormatImageURL()
        {
            if (GetDivision() != "gale")
                return Null.SetNullString(ConfigurationManager.AppSettings["BooksImgPath"] + "HER");
            else
                return Null.SetNullString(ConfigurationManager.AppSettings["BooksImgPath"] + "GR");
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
           // Null.SetNullString(string.Concat("~/product.aspx?title=", title, "&isbn=", ISBN));
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
