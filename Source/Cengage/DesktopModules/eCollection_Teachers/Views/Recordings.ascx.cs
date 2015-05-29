using System;
using System.Data;
using System.Web.UI.WebControls;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using System.Collections.Generic;
using DotNetNuke.Common;
using System.Web.UI;
using System.Configuration;
using DotNetNuke.Common.Utilities;
using System.Linq;

namespace DotNetNuke.Modules.eCollection_Teachers.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Recordings" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the recording history of the respective teachers
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class Recordings : eCollection_TeachersModuleBase
    {
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
                teacherProfile.Active = (char)MyEnums.Active.Yes;
                teacherProfile.SubscriptionId = Null.SetNullInteger(Session["Subscription"]);
                BackButton.NavigateUrl = string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID) + "?pagename=" + TEACHERPROFILE, "&username=", Request.QueryString["username"]);
                teacherProfile.ActionType = "Recordings";
                MyRecordingsCnt.InnerText = Null.SetNullString(teacherController.GetMyWordsCount_MyRecordingsCount(teacherProfile));
                if (MyRecordingsCnt.InnerText != "0")
                {                   
                    FillReadingHistory(teacherProfile);
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
       /// 
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
                Repeater IndependentRec = (Repeater)e.Item.FindControl("VideoGridView");
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
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
                teacher.ProductId = int.Parse(dr["ProductId"].ToString());
                teacher.Title = dr["Title"].ToString();
                teacher.ImageUrl = dr["ImgFileName"].ToString();
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
                teacher.ActionType = "today";
                List<TeacherRecordings> MyRecordings = teacherController.GetMyRecordings(teacher); 
                if (MyRecordings.Count != 0)
                {
                    TodaysIndependentRecordings.DataSource = MyRecordings;
                    TodaysIndependentRecordings.DataBind();
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "ShowTodaysReadingHistory", "<script>$('#TodaysHistToggle').click()</script>", false);
                    ContentDisplayed = true;
                }
                else
                {
                    TodayHistory.Visible = false;
                    YesterdayHistory.Style.Add("margin-top", "-16px");
                }

                teacher.ActionType = "yesterday";
                MyRecordings = teacherController.GetMyRecordings(teacher); 
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

                teacher.ActionType = "Lastsevendays";
                MyRecordings = teacherController.GetMyRecordings(teacher); 
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

                teacher.ActionType = "Restofthemonth";
                MyRecordings = teacherController.GetMyRecordings(teacher); 
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

                teacher.ActionType = "RECORDINGS";
                var months = teacherController.GetReadingHistoryMonths(teacher);
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
            catch (Exception ex) { LogFileWrite(ex); }      
        }
       
        /// <summary>
        /// 
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
                Repeater IndependentRec = (Repeater)e.Item.FindControl("VideoGridView");
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonthWiseWordLogs_BindInfo(object sender, ListViewItemEventArgs e)
        {
            try
            {                
                ListView IndependentHistory = (ListView)e.Item.FindControl("MonthWiseIndependentRecordings");
                var MyRecordings = teacherController.GetMyRecordings(teacherProfile);
                MyRecordings = MyRecordings.Where(x => string.Concat(DateTime.Parse(x.BookOpenedDate).Year, DateTime.Parse(x.BookOpenedDate).Month) == (sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString()).ToList();
                IndependentHistory.DataSource = MyRecordings;              
                IndependentHistory.DataBind();
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }
    }
}