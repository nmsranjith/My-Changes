using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Books.Books
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="BookSelectionWizard" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Book Selection Wizard screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class BookSelectionWizard : eCollection_BooksModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BindGracePeriodinfo();
                LevelsLink.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?SelectionTab=LEVELS";
                CategoriesLink.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?SelectionTab=CATEGORIES";
                ReadingAgeLink.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?SelectionTab=READINGAGES";

                string caseStr = Request.QueryString["SelectionTab"] != null ? Request.QueryString["SelectionTab"].ToString() != string.Empty ?Request.QueryString["SelectionTab"].ToString():"LEVELS" : "LEVELS";
                switch(caseStr)
                    {
                    case "LEVELS":
                            BooksLevels Levels = Page.LoadControl("DesktopModules/eCollection_Books/Books/BooksLevels.ascx") as BooksLevels;
                            Levels.ToggleDivOnOuter += new EventHandler(Level_ToggleDivOnOuter);
                            LevelsLink.CssClass = "H5 LevelBtnclass";
                            Levels.ToggleGracePeriodInfo += new EventHandler(Level_ToggleGracePeriodInfo);
                            BookSelectionContentPlaceHolder.Controls.Add(Levels);
                            break;
                    case "READINGAGES":
                            BooksReadingAge ReadingAges = Page.LoadControl("DesktopModules/eCollection_Books/Books/BooksReadingAge.ascx") as BooksReadingAge;
                            ReadingAgeLink.CssClass = "H5 LevelBtnclass";
                            ReadingAges.ToggleDivOnOuter += new EventHandler(ReadingAge_ToggleDivOnOuter);
                            ReadingAges.ToggleDivOnOuter1 += new EventHandler(ReadingAge_ToggleDivOnOuter1);
                            ReadingAges.ToggleGracePeriodInfo += new EventHandler(ReadingAge_ToggleGracePeriodInfo);
                            BookSelectionContentPlaceHolder.Controls.Add(ReadingAges);    
                            break;
                    case "CATEGORIES":
                            BooksCategories Categories = Page.LoadControl("DesktopModules/eCollection_Books/Books/BooksCategories.ascx") as BooksCategories;
                            CategoriesLink.CssClass = "H5 LevelBtnclass";
                            Categories.ToggleDivOnOuter += new EventHandler(Categories_ToggleDivOnOuter);
                            Categories.ToggleDivOnOuter1 += new EventHandler(Categories_ToggleDivOnOuter1);
                            Categories.ToggleGracePeriodInfo += new EventHandler(Categories_ToggleGracePeriodInfo);
                            BookSelectionContentPlaceHolder.Controls.Add(Categories);
                            break;
                    default:
                            break;                        
                    }
                LoadSeeAllBooks();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ReadingAge_ToggleGracePeriodInfo(object sender, EventArgs e)
        {
            BindGracePeriodinfo();
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=BookSelectionWizard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Level_ToggleGracePeriodInfo(object sender, EventArgs e)
        {
            BindGracePeriodinfo();
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=BookSelectionWizard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Categories_ToggleGracePeriodInfo(object sender, EventArgs e)
        {
            BindGracePeriodinfo();
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=BookSelectionWizard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Categories_ToggleDivOnOuter1(object sender, EventArgs e)
        {
            SeeAlldiv.Style.Add("display", "none");
            LevelsLink.CssClass = "H5 LevelBtnaltclass";
            ReadingAgeLink.CssClass = "H5 LevelBtnaltclass";
            CategoriesLink.CssClass = "H5 LevelBtnclass";
            SelectionContentDiv.Style.Add("display", "block");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ReadingAge_ToggleDivOnOuter1(object sender, EventArgs e)
        {
            SeeAlldiv.Style.Add("display", "none");
            LevelsLink.CssClass = "H5 LevelBtnaltclass";
            CategoriesLink.CssClass = "H5 LevelBtnaltclass";
            ReadingAgeLink.CssClass = "H5 LevelBtnclass";
            SelectionContentDiv.Style.Add("display", "block");            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            //SeeAlldiv.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/SeeAllCollection.ascx"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ReadingAge_ToggleDivOnOuter(object sender, EventArgs e)
        {
            ApplyReadingAgeCSS();
            SeeAllCollection seeAllcollection = (SeeAllCollection)SeeAllPlaceHolder.Controls[0];
            seeAllcollection.BindReadingAgeBooks();
        }      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Level_ToggleDivOnOuter(object sender, EventArgs e)
        {
            ApplyLevelsCSS();
            SeeAllCollection seeAllcollection = (SeeAllCollection)SeeAllPlaceHolder.Controls[0];
            seeAllcollection.BindReadingAgeBooks();            

        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSeeAllBooks()
        {
            Control uc = LoadControl("SeeAllCollection.ascx");
            SeeAllPlaceHolder.Controls.Add(uc);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Categories_ToggleDivOnOuter(object sender, EventArgs e)
        {
            ApplyCategoriesCSS();
            SeeAllCollection seeAllcollection = (SeeAllCollection)SeeAllPlaceHolder.Controls[0];
            seeAllcollection.BindReadingAgeBooks();
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
                AttributeValue= button.CommandArgument.ToString();
            }catch (Exception e1) { }

           
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
                AttributeType = "TEXT TYPE";
                AttributeValue= button.CommandArgument.ToString();
            }
             catch (Exception e1) { }

            
            SelectionContentDiv.Style.Add("display", "block");
            LevelsLink.CssClass = "H5 LevelBtnaltclass";
            ReadingAgeLink.CssClass = "H5 LevelBtnaltclass";
            CategoriesLink.CssClass = "H5 LevelBtnclass";
            SeeAlldiv.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/SeeAllCollection.ascx"));
            
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
                AttributeValue= button.CommandArgument.ToString();
            }
            catch (Exception e1) { }

            SelectionContentDiv.Style.Add("display", "block");
            LevelsLink.CssClass = "H5 LevelBtnaltclass";
            CategoriesLink.CssClass = "H5 LevelBtnaltclass";
            ReadingAgeLink.CssClass = "H5 LevelBtnclass";
            SeeAlldiv.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/SeeAllCollection.ascx"));
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BacktoCollectionButton_Click(object sender, EventArgs e)
        {
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SeeSelectedBokks_Click(object sender, EventArgs e)
        {
            DataCache.RemoveCache(string.Format("GetBooksByReadingLevel{0}", SelectedSubscriptionId));
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=SeeSelectedBooks");
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyLevelsCSS()
        {
            SeeAlldiv.Style.Add("display", "block");
            SelectionContentDiv.Style.Add("display", "none");
            LevelsLink.CssClass = "H5 LevelBtnclass";
            ReadingAgeLink.CssClass = "H5 LevelBtnaltclass";
            CategoriesLink.CssClass = "H5 LevelBtnaltclass";
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyReadingAgeCSS()
        {
            SeeAlldiv.Style.Add("display", "block");
            SelectionContentDiv.Style.Add("display", "none");
            LevelsLink.CssClass = "H5 LevelBtnaltclass";
            CategoriesLink.CssClass = "H5 LevelBtnaltclass";
            ReadingAgeLink.CssClass = "H5 LevelBtnclass";
        }

        /// <summary>
        /// 
        /// </summary>
        private void ApplyCategoriesCSS()
        {
            SeeAlldiv.Style.Add("display", "block");
            SelectionContentDiv.Style.Add("display", "none");
            LevelsLink.CssClass = "H5 LevelBtnaltclass";
            ReadingAgeLink.CssClass = "H5 LevelBtnaltclass";
            CategoriesLink.CssClass = "H5 LevelBtnclass";
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindGracePeriodinfo()
        {
            try
            {
                lblBooksSelected.Text = GracePeriod.SelectedBooks.ToString();
                lblBooksLeft.Text = (GracePeriod.BooksLeft).ToString();
                if (GracePeriod.DaysLeft >= 0)
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
            }
            catch (Exception ex) { LogFileWrite(ex); }
            
        }             
    }
}