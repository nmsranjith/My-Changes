using System;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Video_GetStarted" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To check the flags to display the Welcome video and Get started steps
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class Video_GetStarted : eCollection_DashboardsModuleBase
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
                VideoHdn.Value = UserDetail.VideoFlag.ToString();
                if (UserDetail.VideoFlag == 'Y')
                    VideoPlaceHdr.Visible = true;
                if (Session["GraceDaysLeft"] == null)
                    Session["GraceDaysLeft"] = _dashboardController.SubscriptionDetails(new Users() { SubscriptionId = int.Parse(Session["Subscription"].ToString()), UserLoginName = LoginName }).GraceDaysLeft;
                if (Null.SetNullInteger(Session["GraceDaysLeft"]) > 0)
                {
                    GetStartedHdn.Value = UserDetail.GetStartedFlag.ToString();
                    if (UserDetail.GetStartedFlag == 'Y')
                        GetStartedPlaceHdr.Visible = true;

                    Subscription subGetStarted = _dashboardController.GetStartedDetails(int.Parse(Session["Subscription"].ToString()));
                    if (subGetStarted.TotalBooks == subGetStarted.BooksAdded)
                    {
                        BooksStep.Attributes.Add("Class", "StepSetup1 greenBtn");
                        BooksStepTick.Attributes.Add("Class", "greenTick");
                        AddedBooks.Visible = false;
                    }
                    else if (subGetStarted.BooksAdded > 0)
                        AddedBooks.InnerText = string.Concat("( ", subGetStarted.BooksAdded, " / ", subGetStarted.TotalBooks, " )");

                    if (UserDetail.UserRole != "SUBS ADMIN" && UserDetail.UserRole != "CEN ADMIN")
                    {
                        StudentsStepSno.InnerText = "2. ";
                        StepsDiv_Label.InnerText = "2";
                        TeachersStep.Visible = false;
                    }
                    else
                    {
                        if (subGetStarted.TeachersCount > 0)
                        {
                            TeachersStep.Attributes.Add("Class", "StepSetup1 greenBtn");
                            TeachersStepTick.Attributes.Add("Class", "greenTick");
                        }
                    }

                    if (subGetStarted.UsedLicences > 0)
                    {
                        StudentsStep.Attributes.Add("Class", "StepSetup1 greenBtn");
                        StudentsStepTick.Attributes.Add("Class", "greenTick");
                    }
                }
            }
            catch (Exception ex){LogFileWrite(ex);}
        }
    }
}