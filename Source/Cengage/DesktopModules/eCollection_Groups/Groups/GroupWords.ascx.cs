using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Groups.Components;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="GroupWords" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Displays all the words done by all the group members excluding teachers
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class GroupWords : eCollection_GroupsModuleBase
    {
        GroupController groupController = GroupController.Instance;
        int? groupId = null;

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (EditClassId.Value != -1)
            {
                groupId = EditClassId.Value;
            }
            else if (EditGroupId.Value != -1)
            {
                groupId = EditGroupId.Value;
            }
            else
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=groupprofile");
            }
            if (!IsPostBack)
            {
                try
                {
                    Components.Groups groupsWords = groupController.GetGroupProfileByGroup(groupId.Value);
                    WordsCount.InnerText = groupsWords.MyWords.ToString();
                    GroupNameLabel.Text = GroupName.ToString();
                    List<Components.Groups> groups = groupController.GetWordsByGroup(groupId.Value);
                    TodayWordLog.DataSource = groups.Where(u => u.WordCircledAt.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
                    TodayWordLog.DataBind();
                    if (groups.Where(u => u.WordCircledAt.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList().All(u => u.IsClearFromIpad.ToString() == "Y"))
                    {
                        TodayClearFrmIpBtn.Text = "Removed from ipad";
                        TodayClearFrmIpBtn.CssClass = "RemoveWords";
                        TodayClearFrmIpBtn.Enabled = false;
                    }

                    YesterdayWordLog.DataSource = groups.Where(u => u.WordCircledAt.ToShortDateString() == DateTime.Now.AddDays(-1).ToShortDateString()).ToList();
                    YesterdayWordLog.DataBind();
                    if (groups.Where(u => u.WordCircledAt.ToShortDateString() == DateTime.Now.AddDays(-1).ToShortDateString()).ToList().All(u => u.IsClearFromIpad.ToString() == "Y"))
                    {
                        YstrClearFrmBtn.Text = "Removed from ipad";
                        YstrClearFrmBtn.CssClass = "RemoveWords";
                        YstrClearFrmBtn.Enabled = false;
                    }

                    LastSevenDaysWordLog.DataSource = groups.Where(u => u.WordCircledAt >= DateTime.Now.AddDays(-9) && (u.WordCircledAt <= DateTime.Now.AddDays(-2))).ToList();
                    LastSevenDaysWordLog.DataBind();
                    if (groups.Where(u => u.WordCircledAt >= DateTime.Now.AddDays(-9) && (u.WordCircledAt <= DateTime.Now.AddDays(-2))).ToList().All(u => u.IsClearFromIpad.ToString() == "Y"))
                    {
                        LastSvndyClrFromIpad.Text = "Removed from ipad";
                        LastSvndyClrFromIpad.CssClass = "RemoveWords";
                        LastSvndyClrFromIpad.Enabled = false;
                    }

                    MonthWiseWordLog.DataSource = groupController.GetMonthByGroup(groupId.Value,"WORDS");
                    MonthWiseWordLog.DataBind();
                    if (groups.Count == 0)
                        LastMonthNodeImage.Visible = false;
                }
                catch (Exception ex)
                {
                    this.Messages.ShowError(ex.Message);
                    LogFileWrite(ex);
                }   
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonthWiseWordLog_BindInfo(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            try
            {
                ListView allMonthsWordLog = (ListView)e.Item.FindControl("MonthWordLog");
                Label monthNameLabel = (Label)e.Item.FindControl("MonthLabel");
                RepeaterItem MonthWiseWordLog = (e.Item as RepeaterItem);
                List<Components.Groups> groups = groupController.GetWordsByGroup(groupId.Value);
                if (DateTime.Now.ToString("MMMM") == monthNameLabel.Text)
                {
                    groups = groups.Where(u => u.WordCircledAt.Month==DateTime.Now.Month && u.WordCircledAt > DateTime.Now.AddDays(-9) && u.WordCircledAt< DateTime.Now.AddDays(-9)).ToList();
                    if (groups.All(u => u.IsClearFromIpad.ToString() == "Y"))
                    {
                        Button monthBtn = (Button)e.Item.FindControl("RestMnthClrFromIpad");
                        monthBtn.Text = "Removed from ipad";
                        monthBtn.CssClass = "RemoveWords";
                        monthBtn.Enabled = false;
                    }
                    if (groups.Count != 0)
                    {
                        allMonthsWordLog.DataSource = groups;
                        allMonthsWordLog.DataBind();
                    }
                    else
                    {
                        MonthWiseWordLog.Visible = false;// Style.Add("display", "none");
                    }
                    monthNameLabel.Text = "Rest of the month";
                }
                else
                {
                    groups = groups.Where(u => string.Concat(u.WordCircledAt.ToString("MMMM"), " (", u.WordCircledAt.Year, ")") == monthNameLabel.Text).ToList();
                    if (groups.Any(u => u.IsClearFromIpad.ToString() == "Y"))
                    {
                        Button monthBtn = (Button)e.Item.FindControl("RestMnthClrFromIpad");
                        monthBtn.Text = "Removed from ipad";
                        monthBtn.CssClass = "RemoveWords";
                        monthBtn.Enabled = false;
                    }
                    if (groups.Count != 0)
                    {
                        allMonthsWordLog.DataSource = groups;
                        allMonthsWordLog.DataBind();
                    }
                    else
                    {
                        MonthWiseWordLog.Visible = false; //Style.Add("display", "none");
                        LastMonthNodeImage.Style.Add("display", "none");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=groupprofile");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TodayClearFrmIpBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (groupId.HasValue)
                {
                    int result = groupController.UpdateIsClearFromStatus(groupId.Value, DateTime.Now, null, null, string.Empty, "WeekDays");
                    TodayClearFrmIpBtn.Text = "Removed from ipad";
                    TodayClearFrmIpBtn.CssClass = "RemoveWords";
                }
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }               
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void YstrClearFrmBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (groupId.HasValue)
                {
                    int result = groupController.UpdateIsClearFromStatus(groupId.Value, DateTime.Now.AddDays(-1), null, null, string.Empty, "BetweenDays");
                    YstrClearFrmBtn.Text = "Removed from ipad";
                    YstrClearFrmBtn.CssClass = "RemoveWords";
                }
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LastSvndyClrFromIpad_Click(object sender, EventArgs e)
        {
            try
            {
                if (groupId.HasValue)
                {
                    int result = groupController.UpdateIsClearFromStatus(groupId.Value, null, DateTime.Now.AddDays(-9), DateTime.Now.AddDays(-2), string.Empty, "BetweenDays");
                    LastSvndyClrFromIpad.Text = "Removed from ipad";
                    LastSvndyClrFromIpad.CssClass = "RemoveWords";
                }
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RestMnthClrFromIpad_Click(object sender, EventArgs e)
        {
            try
            {
                if (groupId.HasValue)
                {
                    int result = groupController.UpdateIsClearFromStatus(groupId.Value, DateTime.Now, null, null, (((sender as Button).Parent as RepeaterItem).Controls[5] as Label).Text, "Month");
                    Button RestMnthClrFromIpad = (sender as Button);
                    RestMnthClrFromIpad.Text = "Removed from ipad";
                    RestMnthClrFromIpad.CssClass = "RemoveWords";
                }
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }   
        }
    }
}