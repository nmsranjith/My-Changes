using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using System.Web.Services;
using System.Data;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using System.Linq;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentProfile" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To display the respective student's profile
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class StudentProfile : eCollection_StudentsModuleBase
    {
        Student studentProfile = null;
        DateTime studAddedDate = new DateTime();
        Int32 InitialReadingLevel = 0;
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

                if (!IsPostBack)
                {
                    DataSet ds = studentController.GetProfileDetails(studentProfile);
                    studAddedDate = Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[18].ToString());
                    InitialReadingLevel = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[17].ToString());
                    StudentProfileRepeater.DataSource = ds.Tables[0];
                    StudentProfileRepeater.DataBind();
                }
            }
            catch (Exception ex) { //Messages.ShowWarning(ex.Message); 
                LogFileWrite(ex); }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_PreRender runs when the control is rendered
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (ReadingHistory.IsNoReadingHistory)
            {
                Message1.Text = Constants.NOREADHISTORYINFO;
                Message2.Style.Add("Display", "none");
                MessageOuterDiv.Style.Add("Display", "block");
            }
            
            if (ProfileGroupsList.IsNoGroupExist)
            {
                Message2.Text = Constants.NOGROUPINFO;
            }

            if (Message1.Text == "" && Message2.Text == "")
            {
                MessageOuterDiv.Style.Add("Display", "none");
            }
        }
       

        /// <summary>
        /// Recordings screen url formation
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        protected string MyRecordingsUrl()
        {   
            return Globals.NavigateURL(PortalSettings.ActiveTab.TabID) + "?username="+Request.QueryString["username"]+"&pagename=" + MYRECORDINGS;
          
        }
        
        /// <summary>
        /// Words log screen url formation
        /// </summary>
        /// <returns></returns>
        [WebMethod]        
        protected string MyWordsUrl()
        {
            return Globals.NavigateURL(PortalSettings.ActiveTab.TabID) + "?username=" + Request.QueryString["username"]+"&pagename=" + MYWORDS;
        }

        /// <summary>
        /// Profile repeater data bound method and used for graph formation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         protected void StudentProfileRepeater_OnDataBound(object sender, ListViewItemEventArgs e)
         {
             try
             {
                 List<Student> studentCollection = studentController.GetReadingLevelHistory(studentProfile);
                 ListView graph = e.Item.FindControl("graph") as ListView;
                 Label PMReadingLevel = e.Item.FindControl("PMReadingLevelLabel") as Label;
                 Repeater ReadingLevelRepeater = e.Item.FindControl("ReadingLevelRepeater") as Repeater;
                 Label sixmonthBefore = e.Item.FindControl("sixmonthBefore") as Label;
                 Label TotalMonth = e.Item.FindControl("TotalMonth") as Label;
                 ReadingLevelRepeater.DataSource = studentCollection.Where(x => x.PercentRead != 0).Select(Lvl => new { Lvl.CurrentReadingLevel, Lvl.PercentRead }).Distinct().ToList();
                 ReadingLevelRepeater.DataBind();
                 List<string> str = new List<string>();
                 for (int i = 0; i < 12; i++)
                 {
                     str.Add(i.ToString());
                 }
                 graph.DataSource = str;
                 graph.DataBind();
                 grfHdnLine.Value = studAddedDate.Month.ToString();
                 Int32 mRL = 0;
                 Int32 MRL = mRL;
                 DateTime graffRefDateFrom = new DateTime();
                 DateTime graffRefDateTo = new DateTime();
                 int[] RL = new int[3];
                 int count = 0;
                 sixmonthBefore.Text = string.Format("{0:dd}{1} {0:MMM yyyy}", studAddedDate, (studAddedDate.Day == 1) ? "st" : (studAddedDate.Day == 21) ? "st" : (studAddedDate.Day == 31) ? "st" : (studAddedDate.Day == 2) ? "nd" : (studAddedDate.Day == 22) ? "nd" : (studAddedDate.Day == 3) ? "rd" : (studAddedDate.Day == 23) ? "rd" : "th");
                 if (studAddedDate.Year < DateTime.Now.Year)
                 {
                    count = 1;
                    DateTime refDate=new DateTime(DateTime.Now.Year, 1, 1);
                    TotalMonth.Text = ((DateTime.Now-refDate).Days / 30).ToString() + " MONTH(S)";
                 }
                 else
                 {
                     count = studAddedDate.Month;
                     TotalMonth.Text = ((DateTime.Now-studAddedDate).Days / 30).ToString() + " MONTH(S)";
                 }
                 
                 int count2 = count;
                 grfHdnLine.Value = count.ToString();
                 int studId = studentProfile.StudentId;
                 for (int i = 0; i < 12 - count2 + 1; i++)
                 {
                     if (count != 12)
                     {

                         graffRefDateTo = new DateTime(DateTime.Now.Year, count + 1, 1);
                         graffRefDateFrom = new DateTime(DateTime.Now.Year, count , 1);
                         if (graffRefDateFrom< (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(1))
                         {
                             if (studId != null)
                             {
                                 RL = studentController.GetMinMaxReadingLevelByDate(studId, graffRefDateFrom, graffRefDateTo);

                             }
                             else
                             {
                                 RL[0] = 0;
                                 RL[1] = 0;
                             }
                             if (RL[0] <= RL[2] )
                                mRL = RL[0];
                             else if (RL[2]>0)
                                 mRL = RL[2];
                             else mRL = RL[0];
                             MRL = RL[1];
                             if (RL[0] == 0 && RL[1] == 0)
                             {
                                 mRL = RL[2];
                                 MRL = RL[2];
                             }

                             pointsHdn.Value = string.Concat(pointsHdn.Value, '-', i.ToString(), mRL < 10 ? "0" + mRL.ToString() : mRL.ToString());
                             pointsHdn.Value = string.Concat(pointsHdn.Value, '-', i.ToString(), MRL < 10 ? "0" + MRL.ToString() : MRL.ToString());
                         }
                         count++;
                     }
                     else if (count == 12)
                     {
                         
                         graffRefDateTo = new DateTime(DateTime.Now.Year+1, 1, 1);
                         graffRefDateFrom = new DateTime(DateTime.Now.Year, count, 1);
                         if (graffRefDateFrom < (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(1))
                         {
                             if (studId != null)
                             {
                                 RL = studentController.GetMinMaxReadingLevelByDate(studId, graffRefDateFrom, graffRefDateTo);

                             }
                             else
                             {
                                 RL[0] = 0;
                                 RL[1] = 0;
                             }
                             if (RL[0] < RL[2])
                                 mRL = RL[0];
                             else mRL = RL[2];
                             MRL = RL[1];
                             if (RL[0] == 0 && RL[1] == 0)
                             {
                                 mRL = RL[2];
                                 MRL = RL[2];
                             }
                             
                             pointsHdn.Value = string.Concat(pointsHdn.Value, '-', i.ToString(), mRL < 10 ? "0" + mRL.ToString() : mRL.ToString());
                             pointsHdn.Value = string.Concat(pointsHdn.Value, '-', i.ToString(), MRL < 10 ? "0" + MRL.ToString() : MRL.ToString());
                         }
                         count++;
                     }
                 }
                 graph.DataSource = str;
                 graph.DataBind();
             }
             catch (Exception ex) { //Messages.ShowWarning(ex.Message); 
                 LogFileWrite(ex); }
         }
    }
}