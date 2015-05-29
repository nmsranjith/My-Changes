using System;
using System.Web.UI.WebControls;
using System.Data;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Configuration;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="ReadingHistory" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the reading history of the respective student
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class ReadingHistory : eCollection_StudentsModuleBase
    {
        Student studentProfile = null;
        public bool IsNoReadingHistory { get; set; }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               
                studentProfile = selectedStudent();
                studentProfile.Active = (char)MyEnums.Active.Yes;
                studentProfile.SubscriptionId = Null.SetNullInteger(Session["Subscription"]);
                FillReadingHistory(studentProfile);
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }
        
        /// <summary>
        /// Guided and independent listview databound method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IndependentGrid_RowDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                Repeater independentRec = (Repeater)e.Item.FindControl("VideoGridView");
                Image classImage = (Image)e.Item.FindControl("ClassImage");
                Image bookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                bookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], bookCoverImage.ImageUrl);
                HiddenField sessType = (HiddenField)e.Item.FindControl("SessTypeVal");
                if (sessType.Value == "Independent")
                    classImage.ImageUrl = "~/Portals/0/images/students.png";
                else
                    classImage.ImageUrl = "~/Portals/0/images/groups.png";
                Label cntLbl = (Label)e.Item.FindControl("WordCntLbl");               
                studentProfile.ActionType = "ReadingSession";
                var wrdCnt = studentController.GetMyRHWordsCount(studentProfile).Where(x => DateTime.Parse(x.OpenedDate) == DateTime.Parse((sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString())).ToList();
                cntLbl.Text = "0";
                if (wrdCnt.Count != 0)
                    cntLbl.Text = wrdCnt[0].WordCount.ToString();

                List<StudentRecordingFiles> dataSource = studentController.GetMyRecordedFiles(studentProfile).Where(x => DateTime.Parse(x.OpenedDate) == DateTime.Parse((sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString())).ToList();
                dataSource = SaveRecordFile(dataSource);
                switch (dataSource.Count == 0)
                {
                    default:
                        independentRec.DataSource = dataSource;
                        independentRec.DataBind();
                        break;
                    case true:
                        (e.Item.FindControl("RecBtnHdn") as HiddenField).Value = "No Recordings";
                        break;
                }
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
            
        }
       
        /// <summary>
        ///  Fill Reading History
        /// </summary>
        /// <param name="student"></param>
        public void FillReadingHistory(Student student)
        {
            try
            {
                bool contentDisplayed = false;               
                student.ActionType = "today";
                var dataSource = studentController.GetReadingHistories(student);
                int nodes = 0;
                if (dataSource.Count != 0)
                {                    
                    TodaysIndependentRecordings.DataSource = dataSource; 
                    TodaysIndependentRecordings.DataBind();
                    nodes++;
                    contentDisplayed = true;
                }
                else
                {
                    TodayHistory.Visible = false;
                    YesterdayHistory.Style.Add("margin-top", "-16px");
                }

                student.ActionType = "yesterday";
                dataSource = studentController.GetReadingHistories(student);
                if (dataSource.Count != 0)
                {                   
                    YesterDayIndependentRecordings.DataSource = dataSource;
                    YesterDayIndependentRecordings.DataBind();
                    nodes++;
                    if (!contentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowYesterdaysReadingHistory", "<script>$('#YestHistToggle').click()</script>", false);
                        contentDisplayed = true;
                    }
                }
                else
                {
                    YesterdayHistory.Visible = false;
                    if (!TodayHistory.Visible)
                        Last7DaysHistory.Style.Add("margin-top", "-16px");
                }
                
                student.ActionType = "Lastsevendays";
                dataSource = studentController.GetReadingHistories(student);
                if (dataSource.Count != 0)
                {
                    Last7DaysIndependentRecordings.DataSource = studentController.GetReadingHistories(student);
                    Last7DaysIndependentRecordings.DataBind();
                    nodes++;
                    if (!contentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Show7daysReadingHistory", "<script>$('#Last7DToggle').click()</script>", false);
                        contentDisplayed = true;
                    }
                }
                else
                {
                    Last7DaysHistory.Visible = false;
                    if (!TodayHistory.Visible && !YesterdayHistory.Visible)
                        RestoftheMonthHistory.Style.Add("margin-top", "-16px");
                }

                student.ActionType = "Restofthemonth";
                dataSource = studentController.GetReadingHistories(student);
                if (dataSource.Count != 0)
                {
                    RestIndependentHistory.DataSource = studentController.GetReadingHistories(student);
                    RestIndependentHistory.DataBind();
                    nodes++;
                    if (!contentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowRestMonthReadingHistory", "<script>$('#RestMonthToggle').click()</script>", false);
                        contentDisplayed = true;
                    }
                }
                else
                {
                    RestoftheMonthHistory.Visible = false;
                    if (!TodayHistory.Visible && !YesterdayHistory.Visible && !Last7DaysHistory.Visible)
                        MonthwiseHistories.Style.Add("margin-top", "-44px");
                }              
                student.ActionType = "PROFILE";
                var months=studentController.GetReadingHistoryMonths(student);
                if (months.ToList().Count > 0)
                {
                    MonthWiseHistory.DataSource = months;
                    MonthWiseHistory.DataBind();
                    nodes++;
                    if (!contentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowMonthwiseReadingHistory", "<script>$($('#MonthwiseHistories').children()[0].children[0].children[1]).click()</script>", false);
                        contentDisplayed = true;
                    }
                }

                if (nodes == 0)
                {
                    HistoryMainDiv.Visible = false;
                    LastNodeDiv.Visible = false;
                    IsNoReadingHistory = true;
                }
                else
                    IsNoReadingHistory = false ;
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        } 
       
        /// <summary>
        /// Monthwise reading history data bound method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonthWiseWordLogs_BindInfo(object sender, ListViewItemEventArgs e)
        {
            try
            {
                ListView independentHistory = (ListView)e.Item.FindControl("IndependentRecordings");
                studentProfile.ActionType = "Months";
                var readingHistory = studentController.GetReadingHistories(studentProfile);// studentController.GetStudentsReadingHistory(selectedStudent());               
                readingHistory = readingHistory.Where(x => string.Concat(DateTime.Parse(x.BookOpenedDate).Year, DateTime.Parse(x.BookOpenedDate).Month) == (sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString()).ToList();
                independentHistory.DataSource = readingHistory;
                independentHistory.DataBind();
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }
    }
}