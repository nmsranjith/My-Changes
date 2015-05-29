using System;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="MyWords" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Lists all the words circled by the student
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class MyWords : eCollection_StudentsModuleBase
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

                BackButton.NavigateUrl = string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID) + "?pagename=" + STUDENTPROFILE, "&username=", Request.QueryString["username"]);
                studentIdLabel.InnerText = studentProfile.StudentId.ToString();
                studentProfile.SubscriptionId = Null.SetNullInteger(Session["Subscription"]);

                studentProfile.ActionType = "Words";
                MyWordsCnt.InnerText = Null.SetNullString(studentController.GetMyWordsCount_MyRecordingsCount(studentProfile));
                //MyWordsCnt.InnerText = Session["WordsCount"].ToString();                
                if (MyWordsCnt.InnerText != "0")
                {
                    FillMyWords(studentProfile);
                }
                else
                {
                    SecondDiv.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }
         
        /// <summary>
        ///  Fill words log
        /// </summary>
        protected void FillMyWords(Student student)
        {
            try
            {
                student.ActionType = "today";
                List<StudentWords> MyWords = studentController.GetMyWords(student); 
                if (MyWords.Count == 0)
                    YesterdayHistory.Style.Add("margin-top", "-16px");              
                FillList(MyWords, TodayWordLog, TodayHistory);               

                student.ActionType = "yesterday";
                MyWords = studentController.GetMyWords(student);
                if (MyWords.Count == 0 && !TodayHistory.Visible)
                    Last7DaysHistory.Style.Add("margin-top", "-16px");           
                FillList(MyWords, YesterdayWordLog, YesterdayHistory);

                student.ActionType = "Lastsevendays";
                MyWords = studentController.GetMyWords(student);
                if (MyWords.Count == 0 && !TodayHistory.Visible && !YesterdayHistory.Visible)
                    RestoftheMonthHistory.Style.Add("margin-top", "-16px");                
                FillList(MyWords, LastSevenDaysWordLog, Last7DaysHistory);

                student.ActionType = "Restofthemonth";
                MyWords = studentController.GetMyWords(student);
                if (MyWords.Count == 0 && !TodayHistory.Visible && !YesterdayHistory.Visible && !Last7DaysHistory.Visible)
                    MonthwiseHistory.Style.Add("margin-top", "-44px");         
                FillList(MyWords, RestoftheMonthWordLog, RestoftheMonthHistory);

               
                //var months = from c in MyWords.Tables[3].AsEnumerable().Where(x => x.Field<string>("CircledMonths") != string.Format("{0:MMMM}", DateTime.Now))
                //             select new
                //             {
                //                 CircledMonths = c.Field<string>("CircledMonths"),
                //                 MonthId = c.Field<int>("MonthId"),
                //             };
                student.ActionType = "WORDS";
                var months = studentController.GetReadingHistoryMonths(student);
                if (months.ToList().Count > 0)
                {
                    MonthWiseWordLog.DataSource = months;
                    MonthWiseWordLog.DataBind();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Data binding method
        /// </summary>
        /// <param name="wordsList"></param>
        /// <param name="wordsListView"></param>
        /// <param name="listHolder"></param>
        /// <param name="nextListHolder"></param>
        protected void FillList(List<StudentWords> wordsList, ListView wordsListView, HtmlGenericControl listHolder)
        {
            try
            {
                if (wordsList.Count > 0)
                {
                    wordsListView.DataSource = wordsList;
                    wordsListView.DataBind();
                }
                else
                    listHolder.Visible = false;
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }

        /// <summary>
        ///  Monthwise databound method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonthWiseWordLog_BindInfo(object sender, ListViewItemEventArgs e)
        {
            try
            {
                ListView allMonthsWordLog = (ListView)e.Item.FindControl("MonthDataList");
                Label monthNameLabel = (Label)e.Item.FindControl("MonthLabel");
                studentProfile.ActionType = "Months";
                var myWords = studentController.GetMyWords(studentProfile);
                myWords = myWords.Where(x => x.CircledMonth.ToString() == (sender as ListView).DataKeys[e.Item.DataItemIndex].Value.ToString()).ToList();
                allMonthsWordLog.DataSource = myWords;               
                allMonthsWordLog.DataBind();
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }
 
    }
}