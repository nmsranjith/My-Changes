using System;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Collections.Generic;

namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="PurchaseDetails" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Displays the subscription purchased details at the bottom of the dashboard screen above the footer.
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class PurchaseDetails : eCollection_DashboardsModuleBase
    {
        List<Subscription> oMainIds = new List<Subscription>();

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
                    FillPurchaseDetails();
                }
            }
            catch (Exception ex){LogFileWrite(ex);}
        }

        #region Member functions
        /// <summary>
        /// Fill the Purchase Details
        /// </summary>
        protected void FillPurchaseDetails()
        {
            try
            {
                //Get Purchase Details
                PurchaseDetailrepeater.DataSource = _dashboardController.GetPurchaseDetails(int.Parse(Session["Subscription"].ToString()));
                PurchaseDetailrepeater.DataBind();
            }
            catch (Exception ex){LogFileWrite(ex);}
        }
        #endregion


    }
}