using System;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Web.UI.WebControls;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Configuration;
using DotNetNuke.Common.Utilities;
using System.Threading;
namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="DailyActivities" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    This is to display all the activities done by all users under this subscriptions, eg: Students added, books selected, teachers added, etc.,
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class DailyActivities : eCollection_DashboardsModuleBase
    {    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindActivities();
                    Session["DateCount"] = 0;
                }
            }
            catch (Exception ex){LogFileWrite(ex);}
        }            

        #region Member Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityListView"></param>
        /// <param name="activityList"></param>
        protected void BindActivities()
        {
            try
            {
                var a = _dashboardController.GetAllDates(new Subscription() { SubsId = int.Parse(Session["Subscription"].ToString()), AdminName = LoginName });
                if (a.ToList().Count >= Null.SetNullInteger(LoadMoreButton.CommandArgument))
                {
                    BoolCount.Value = "continue";
                    var b = a.Take(Null.SetNullInteger(LoadMoreButton.CommandArgument));
                    LoadMoreButton.CommandArgument = (Null.SetNullInteger(LoadMoreButton.CommandArgument) + 35).ToString();
                    ActivityRepeater.DataSource = b;
                    ActivityRepeater.DataBind();
                }
                else
                {
                    BoolCount.Value = "stop";
                    ActivityRepeater.DataSource = a;
                    ActivityRepeater.DataBind();
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }         
        }  
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LoadMore_Click(object sender, EventArgs e)
        {
            try
            {
                if (BoolCount.Value != "stop")
                {
                    Thread.Sleep(800);
                    BindActivities();
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
            finally
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "LoadMore", "<script>LoadMore();</script>", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ActivityRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Label activity = (Label)e.Item.FindControl("Activity");
                Label date = (Label)e.Item.FindControl("Date");
                Label user = (Label)e.Item.FindControl("User");
                switch (activity.Text)
                {
                    default:
                        Repeater contentRptr = (Repeater)e.Item.FindControl("ContentRepeater");
                        var result = _dashboardController.GetDailyActivities(new Subscription() { SubsId = int.Parse(Session["Subscription"].ToString()), AdminName = LoginName });
                        var res = result.AsEnumerable().Where(x => x.ActivityType == activity.Text && x.DateCreated == date.Text && x.AddedBy == user.Text).ToList();
                        contentRptr.DataSource = res;
                        contentRptr.DataBind();
                        break;
                    case "PM ECOLLECTION UPGRADED":
                        Repeater upgraded = (Repeater)e.Item.FindControl("Upgraded");
                        result = _dashboardController.GetDailyActivities(new Subscription() { SubsId = int.Parse(Session["Subscription"].ToString()), AdminName = LoginName });
                        res = result.AsEnumerable().Where(x => x.ActivityType == activity.Text && x.DateCreated == date.Text && x.AddedBy == user.Text).ToList();
                        upgraded.DataSource = res;
                        upgraded.DataBind();
                        break;
                    case "BOOKS SELECTED":
                        Repeater bookRepeater = (Repeater)e.Item.FindControl("BookRepeater");
                        result = _dashboardController.GetDailyActivities(new Subscription() { SubsId = int.Parse(Session["Subscription"].ToString()), AdminName = LoginName });
                        res = result.AsEnumerable().Where(x => x.ActivityType == activity.Text && x.DateCreated == date.Text && x.AddedBy == user.Text).ToList();
                        if (res.Count > 6)
                        {
                            LinkButton seesAll = (LinkButton)e.Item.FindControl("SeeAllBtn");
                            seesAll.CommandArgument = bookRepeater.ClientID;
                            res = res.Take(4).ToList();
                            seesAll.Visible = true;
                        }
                        bookRepeater.DataSource = res;
                        bookRepeater.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Image bookCoverImage = (Image)e.Item.FindControl("BookCoverImg");
                bookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], bookCoverImage.ImageUrl);
            }
            catch (Exception ex){LogFileWrite(ex);}
         }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SeeAllBtn_Click(object sender, EventArgs e)
        {
            try
            {       
                Repeater bookRepeater = (Repeater)(sender as LinkButton).Parent.FindControl("BookRepeater");                
                Label date=(Label)(sender as LinkButton).Parent.FindControl("Date");
                Label activity = (Label)(sender as LinkButton).Parent.FindControl("Activity");
                Label user = (Label)(sender as LinkButton).Parent.FindControl("User");
                var result = _dashboardController.GetDailyActivities(new Subscription() { SubsId = int.Parse(Session["Subscription"].ToString()), AdminName = LoginName });
                var res = result.AsEnumerable().Where(x => x.ActivityType == activity.Text && x.DateCreated == date.Text && x.AddedBy == user.Text).ToList();
                if ((sender as LinkButton).Text == "See all")
                {
                    (sender as LinkButton).Text = "Hide";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "SeeAllBooks", "<script>SeeAllBooks(250)</script>", false);
                }
                else
                {
                    res = res.Take(4).ToList();
                    (sender as LinkButton).Text = "See all";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "SeeAllBooks", "<script>SeeAllBooks(-250)</script>", false);
                }
                bookRepeater.DataSource = res;
                bookRepeater.DataBind();
            }
            catch (Exception ex){LogFileWrite(ex);}
        }
    }
}