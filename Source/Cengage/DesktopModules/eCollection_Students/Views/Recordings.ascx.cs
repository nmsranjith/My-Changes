using System;
using System.Data;
using System.Web.UI.WebControls;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using System.Linq;
using System.Collections.Generic;
using DotNetNuke.Common;
using System.Configuration;
using System.Web.UI;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Recordings" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the recording history of the respective student
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class Recordings : eCollection_StudentsModuleBase
    {
        Student studentProfile = null;
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
                BackButton.HRef = string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID) + "?pagename=" + STUDENTPROFILE, "&username=", Request.QueryString["username"]);
                studentProfile.ActionType = "Recordings";
                MyRecordingsCnt.InnerText = Null.SetNullString(studentController.GetMyWordsCount_MyRecordingsCount(studentProfile));        
                if (MyRecordingsCnt.InnerText != "0")
                {                    
                    FillReadingHistory(studentProfile);
                }
                else
                {
                    SecondDiv.Visible = false;
                    LastNodeDiv.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }
        /// <summary>
        /// Independent/Guided sessions data bound method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IndependentGrid_RowDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {               
                Image classImage = (Image)e.Item.FindControl("ClassImage");
                HiddenField SessType = (HiddenField)e.Item.FindControl("SessTypeVal");
                if (SessType.Value == "Independent")
                    classImage.ImageUrl = "~/Portals/0/images/students.png";
                else
                    classImage.ImageUrl = "~/Portals/0/images/groups.png";
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                Repeater IndependentRec = (Repeater)e.Item.FindControl("VideoGridView");

                List<StudentRecordingFiles> dataSource = studentController.GetMyRecordedFiles(studentProfile).Where(x => DateTime.Parse(x.OpenedDate) == DateTime.Parse((sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString())).ToList();
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
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }

        }
        
        /// <summary>
        /// Fill recording history method
        /// </summary>
        /// <param name="student"></param>
        public void FillReadingHistory(Student student)
        {
            try
            {
                bool ContentDisplayed = false;
                student.ActionType = "today";
                List<StudentRecordings> MyRecordings = studentController.GetMyRecordings(student); 
                if (MyRecordings.Count != 0)
                {
                    TodaysIndependentRecordings.DataSource = MyRecordings;
                    TodaysIndependentRecordings.DataBind();
                    ContentDisplayed = true;
                }
                else
                {
                    TodayHistory.Visible = false;
                    YesterdayHistory.Style.Add("margin-top", "-16px");
                }

                student.ActionType = "yesterday";
                MyRecordings = studentController.GetMyRecordings(student);                
                if (MyRecordings.Count != 0)
                {
                    YesterDayIndependentRecordings.DataSource = MyRecordings;
                    YesterDayIndependentRecordings.DataBind();
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

                student.ActionType = "Lastsevendays";
                MyRecordings = studentController.GetMyRecordings(student);
                if (MyRecordings.Count != 0)
                {
                    Last7DaysIndependentRecordings.DataSource = MyRecordings;
                    Last7DaysIndependentRecordings.DataBind();
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

                student.ActionType = "Restofthemonth";
                MyRecordings = studentController.GetMyRecordings(student);
                if (MyRecordings.Count != 0)
                {
                    RestIndependentHistory.DataSource = MyRecordings;
                    RestIndependentHistory.DataBind();
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
                student.ActionType = "RECORDINGS";
                var months = studentController.GetReadingHistoryMonths(student);
                if (months.Count > 0)
                {
                    MonthWiseHistory.DataSource = months;
                    MonthWiseHistory.DataBind();
                    if (!ContentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowMonthwiseReadingHistory", "<script>$($('#MonthwiseHistories').children()[0].children[0].children[1]).click()</script>", false);
                        ContentDisplayed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Monthwise Independent/Guided sessions data bound method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonthIndependentGrid_RowDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
               
                Image classImage = (Image)e.Item.FindControl("ClassImage");
                HiddenField SessType = (HiddenField)e.Item.FindControl("SessTypeVal");
                if (SessType.Value == "Independent")
                    classImage.ImageUrl = "~/Portals/0/images/students.png";
                else
                    classImage.ImageUrl = "~/Portals/0/images/groups.png";
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                Repeater IndependentRec = (Repeater)e.Item.FindControl("VideoGridView");
               
                List<StudentRecordingFiles> dataSource = studentController.GetMyRecordedFiles(studentProfile).Where(x => DateTime.Parse(x.OpenedDate) == DateTime.Parse((sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString())).ToList();
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
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Monthwise Independent/Guided recordings data bound method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonthWiseWordLogs_BindInfo(object sender, ListViewItemEventArgs e)
        {
            try
            {                
                ListView IndependentHistory = (ListView)e.Item.FindControl("MonthWiseIndependentRecordings");
                studentProfile.ActionType = "Months";
                var MyRecordings = studentController.GetMyRecordings(studentProfile);
                MyRecordings= MyRecordings.Where(x => string.Concat(DateTime.Parse(x.BookOpenedDate).Year, DateTime.Parse(x.BookOpenedDate).Month) == (sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString()).ToList();
                IndependentHistory.DataSource = MyRecordings;
                IndependentHistory.DataBind();
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }
    }
}