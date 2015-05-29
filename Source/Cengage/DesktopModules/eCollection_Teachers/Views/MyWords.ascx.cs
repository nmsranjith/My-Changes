using System;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Teachers.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="MyWords" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the words circled by the teachers
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class MyWords : eCollection_TeachersModuleBase
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
                teacherProfile.SubscriptionId = Null.SetNullInteger(Session["Subscription"]);
                BackButton.NavigateUrl = string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID) + "?pagename=" + TEACHERPROFILE, "&username=", Request.QueryString["username"]);
                //MyWordsCnt.InnerText = Session["WordsCount"].ToString();
                teacherIdLabel.InnerText = teacherProfile.TeacherId.ToString();
                teacherProfile.ActionType = "Words";
                MyWordsCnt.InnerText = Null.SetNullString(teacherController.GetMyWordsCount_MyRecordingsCount(teacherProfile));
                if (MyWordsCnt.InnerText != "0")
                {
                    FillMyWords(teacherProfile);
                }
                else
                {
                    SecondDiv.Visible = false;
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
            Teacher Teacher = new Teacher();
            try
            {
                Teacher.Word = dr["Word"].ToString();
                Teacher.WordCount = dr["WordCount"].ToString();
            }
            catch (Exception ex) { LogFileWrite(ex); }      
            return Teacher;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void FillMyWords(Teacher teacher)
        {
            try
            {
                teacher.ActionType = "today";
                List<TeacherWords> MyWords = teacherController.GetMyWords(teacher);
                if (MyWords.Count == 0)
                    YesterdayHistory.Style.Add("margin-top", "-16px");  
                FillList(MyWords, TodayWordLog, TodayHistory);

                teacher.ActionType = "yesterday";
                MyWords = teacherController.GetMyWords(teacher);
                if (MyWords.Count == 0 && !TodayHistory.Visible)
                    Last7DaysHistory.Style.Add("margin-top", "-16px");
                FillList(MyWords, YesterdayWordLog, YesterdayHistory);

                teacher.ActionType = "Lastsevendays";
                MyWords = teacherController.GetMyWords(teacher);
                if (MyWords.Count == 0 && !TodayHistory.Visible && !YesterdayHistory.Visible)
                    RestoftheMonthHistory.Style.Add("margin-top", "-16px");     
                FillList(MyWords, LastSevenDaysWordLog, Last7DaysHistory);

                teacher.ActionType = "Restofthemonth";
                MyWords = teacherController.GetMyWords(teacher);
                if (MyWords.Count == 0 && !TodayHistory.Visible && !YesterdayHistory.Visible && !Last7DaysHistory.Visible)
                    MonthwiseHistory.Style.Add("margin-top", "-44px");
                FillList(MyWords, RestoftheMonthWordLog, RestoftheMonthHistory);                
                    
                teacher.ActionType = "WORDS";
                var months = teacherController.GetReadingHistoryMonths(teacher);
                if (months.ToList().Count > 0)
                {
                    MonthWiseWordLog.DataSource = months;
                    MonthWiseWordLog.DataBind();
                }               
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wordsList"></param>
        /// <param name="wordsListView"></param>
        /// <param name="ListHolder"></param>
        /// <param name="nextListHolder"></param>
        protected void FillList(List<TeacherWords> wordsList, ListView wordsListView, HtmlGenericControl ListHolder)
        {
            if (wordsList.Count > 0)
            {
                wordsListView.DataSource = wordsList;
                wordsListView.DataBind();
            }
            else
                ListHolder.Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonthWiseWordLog_BindInfo(object sender, ListViewItemEventArgs e)
        {
            try
            {
                ListView allMonthsWordLog = (ListView)e.Item.FindControl("MonthDataList");
                Label monthNameLabel = (Label)e.Item.FindControl("MonthLabel");
                teacherProfile.ActionType = "Months";
                var MyWords = teacherController.GetMyWords(teacherProfile);// studentController.GetStudentsReadingHistory(selectedStudent());               
                MyWords = MyWords.Where(x => x.CircledMonth.ToString() == (sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString()).ToList();
                allMonthsWordLog.DataSource = MyWords;
                allMonthsWordLog.DataBind();
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }
    }
}