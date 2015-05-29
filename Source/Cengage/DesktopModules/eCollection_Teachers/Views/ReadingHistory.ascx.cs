using System;
using System.Web.UI.WebControls;
using System.Data;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Configuration;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Teachers.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="ReadingHistory" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the reading history of the respective teachers
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class ReadingHistory : eCollection_TeachersModuleBase
    {
        public bool IsNoReadingHistory { get; set; }

        Teacher teacherProfile = null;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                teacherProfile = selectedTeacher();
                teacherProfile.SubscriptionId = Null.SetNullInteger(Session["Subscription"]);
                teacherProfile.Active = (char)MyEnums.Active.Yes;
                FillReadingHistory(teacherProfile);
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IndependentGrid_RowDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                var readingHistory = teacherController.GetReadingHistory(selectedTeacher());
                Repeater IndependentRec = (Repeater)e.Item.FindControl("VideoGridView");
                Image classImage = (Image)e.Item.FindControl("ClassImage");
                HiddenField SessType = (HiddenField)e.Item.FindControl("SessTypeVal");
                if (SessType.Value == "Independent")
                    classImage.ImageUrl = "~/Portals/0/images/students.png";
                else
                    classImage.ImageUrl = "~/Portals/0/images/groups.png";
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                Label cntLbl = (Label)e.Item.FindControl("WordCntLbl");
               
                teacherProfile.ActionType = "ReadingSession";
                var wrdCnt = teacherController.GetMyRHWordsCount(teacherProfile).Where(x => DateTime.Parse(x.OpenedDate) == DateTime.Parse((sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString())).ToList();
              
                cntLbl.Text = "0";
                if (wrdCnt.Count != 0)
                    cntLbl.Text = wrdCnt[0].WordCount.ToString();
                List<TeacherRecordingFiles> dataSource = teacherController.GetMyRecordedFiles(teacherProfile).Where(x => DateTime.Parse(x.OpenedDate) == DateTime.Parse((sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString())).ToList();
                dataSource = SaveRecordFile(dataSource);
                switch (dataSource.Count == 0)
                {
                    default:
                        IndependentRec.DataSource = dataSource;
                        IndependentRec.DataBind();
                        break;
                    case true:
                        (e.Item.FindControl("RecBtnHdn") as HiddenField).Value = "No Recordings";
                        break;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }
            
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private Teacher GetTeacherDtTblRow(DataRow dr)
        {
            Teacher teacher = new Teacher();
            try
            {
                teacher.Title = dr["Title"].ToString();
                teacher.ImageUrl = dr["FileName"].ToString();
                teacher.BookOpenedDate = dr["BookOpenedDate"].ToString();
                teacher.BookOpenedTime = dr["BookOpenedTime"].ToString();
                teacher.OpenedDate = DateTime.Parse(dr["OpenedDate"].ToString());
                teacher.Minutes = int.Parse(dr["MinOpened"].ToString());
                teacher.Seconds = int.Parse(dr["SecOpened"].ToString());
                teacher.SessionType = dr["SessionType"].ToString();
            }
            catch (Exception ex) { LogFileWrite(ex); }      
            return teacher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teacher"></param>
        public void FillReadingHistory(Teacher teacher)
        {
            try
            {
                bool ContentDisplayed = false;              
                int nodes = 0;
                teacher.ActionType = "today";
                var datasource = teacherController.GetReadingHistories(teacher);
                if (datasource.Count != 0)
                {
                    TodaysIndependentRecordings.DataSource = datasource;
                    TodaysIndependentRecordings.DataBind();
                    nodes++;
                    ContentDisplayed = true;
                }
                else
                {
                    TodayHistory.Visible = false;
                    YesterdayHistory.Style.Add("margin-top", "-16px");
                    
                }

                teacher.ActionType = "yesterday";
                datasource = teacherController.GetReadingHistories(teacher);
                if (datasource.Count != 0)
                {
                    YesterDayIndependentRecordings.DataSource = datasource;
                    YesterDayIndependentRecordings.DataBind();
                    nodes++;
                    if (!ContentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowYesterdaysReadingHistory", "<script>$('#YestHistToggle').click()</script>", false);
                        ContentDisplayed = true;
                    }
                }
                else
                {
                    YesterdayHistory.Visible = false;
                    if (!TodayHistory.Visible)
                        Last7DaysHistory.Style.Add("margin-top", "-16px");
                }
                teacher.ActionType = "Lastsevendays";
                datasource = teacherController.GetReadingHistories(teacher);
                if (datasource.Count != 0)
                {
                    Last7DaysIndependentRecordings.DataSource = datasource;
                    Last7DaysIndependentRecordings.DataBind();
                    nodes++;
                    if (!ContentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Show7daysReadingHistory", "<script>$('#Last7DToggle').click()</script>", false);
                        ContentDisplayed = true;
                    }
                }
                else
                {
                    Last7DaysHistory.Visible = false;
                    if (!TodayHistory.Visible && !YesterdayHistory.Visible)
                        RestoftheMonthHistory.Style.Add("margin-top", "-16px");
                }
                teacher.ActionType = "Restofthemonth";
                datasource = teacherController.GetReadingHistories(teacher);
                if ( datasource.Count != 0)
                {
                    RestIndependentHistory.DataSource = datasource;
                    RestIndependentHistory.DataBind();
                    nodes++;
                    if (!ContentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowRestMonthReadingHistory", "<script>$('#RestMonthToggle').click()</script>", false);
                        ContentDisplayed = true;
                    }
                }
                else
                {
                    RestoftheMonthHistory.Visible = false;
                    if (!TodayHistory.Visible && !YesterdayHistory.Visible && !Last7DaysHistory.Visible)
                        MonthwiseHistories.Style.Add("margin-top", "-44px");
                }                
                var months = teacherController.GetReadingHistoryMonths(teacher);
                if (months.ToList().Count > 0)
                {
                    MonthWiseHistory.DataSource = months;
                    MonthWiseHistory.DataBind();
                    nodes++;
                    if (!ContentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowMonthwiseReadingHistory", "<script>$($('#MonthwiseHistories').children()[0].children[0].children[1]).click()</script>", false);
                        ContentDisplayed = true;
                    }
                }

                if (nodes == 0)
                {
                    HistoryMainDiv.Visible = false;
                    LastNodeDiv.Visible = false;
                    IsNoReadingHistory = true;
                }
                else IsNoReadingHistory = false;
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonthWiseWordLogs_BindInfo(object sender, ListViewItemEventArgs e)
        {
            try
            {
                ListView IndependentHistory = (ListView)e.Item.FindControl("IndependentRecordings");
                teacherProfile.ActionType = "Months";
                var readingHistory = teacherController.GetReadingHistories(teacherProfile);// teacherController.GetReadingHistory(selectedTeacher());            
                readingHistory = readingHistory.Where(x => string.Concat(DateTime.Parse(x.BookOpenedDate).Year, DateTime.Parse(x.BookOpenedDate).Month) == (sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString()).ToList();
                IndependentHistory.DataSource = readingHistory;
                IndependentHistory.DataBind();
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }
    }
}