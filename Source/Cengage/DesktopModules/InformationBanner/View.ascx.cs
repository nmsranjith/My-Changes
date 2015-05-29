/*
' Copyright (c) 2012  DotNetNuke Corporation
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using DotNetNuke.Modules.InformationBanner.Components.Controllers;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.UI;
using DotNetNuke.Instrumentation;
using DotNetNuke.Common;


namespace DotNetNuke.Modules.InformationBanner
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from InformationBannerModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : InformationBannerModuleBase, IActionable
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                
                Visitor visitor = Session["UserInfo"] as Visitor;
                if (Session["UserInfo"] != null)
                    visitor = Session["UserInfo"] as Visitor;
                else
                    visitor = new Visitor();
                if (visitor.UserID != 0)
                {
                    LoadGridData(visitor.UserName);
                    InfoBannerUserName.Value = visitor.UserName;
                }
                PageUrlHdn.Value = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }        
        #endregion

        #region Optional Interfaces

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), Localization.GetString("EditModule", this.LocalResourceFile), "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

        #endregion      

        void LoadGridData(string UserEmail)
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("Alert_ID");
                dt1.Columns.Add("ERROR_MESSAGE");
                dt1.Columns.Add("ErrorType");
                dt1.Columns.Add("Start_Date");
                dt1.Columns.Add("End_Date");
                /* SqlDataReader reader = InformationBannerController.Instance.GetAlerts("view", (System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")).Trim(), UserEmail);
                 dt.Load(reader);
                 foreach (DataRow row in dt.Rows)
                 {
                     DnnLog.Fatal("Start/End time -->" + DateTime.Now + "?" + row["START_DATE"].ToString() +":"+ DateTime.Parse(row["END_DATE"].ToString())+"<--");
                     if ((DateTime.Now >= DateTime.Parse(row["START_DATE"].ToString())) && (DateTime.Now <= DateTime.Parse(row["END_DATE"].ToString())))
                     {
                    

                         DataRow dr = dt1.NewRow();
                         dr["Alert_ID"] = row["Alert_ID"].ToString();
                         dr["ERROR_MESSAGE"] = row["ERROR_MESSAGE"].ToString();
                         dr["ErrorType"] = row["ErrorType"].ToString();
                         dr["Start_Date"] = row["Start_Date"].ToString();
                         dr["End_Date"] = row["End_Date"].ToString();
                         dt1.Rows.Add(dr);
                     }
                 }
                 Visitor visitor = Session["UserInfo"] as Visitor;*/
                AlertRepeater.DataSource = InformationBannerController.Instance.GetAlerts("view", System.DateTime.Now.ToString(), UserEmail);// dt1;
                AlertRepeater.DataBind();
            }
            catch (Exception ex) { LogFileWrite(ex); }
            //Visitor visitor = Session["UserInfo"] as Visitor;
            //AlertRepeater.DataSource = InformationBannerController.Instance.GetAlerts("view", (System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")).Trim());
            //AlertRepeater.DataBind();
        }

        [WebMethod]
        protected string setBorderClass(string ErrorType)
        {
            if (ErrorType == "General Message")
            {
                return "info-msg";
            }
            else if (ErrorType == "Error Message")
            {
                return "error-msg";
            }
            else if (ErrorType == "Success Message")
            {
                return "success-msg";
            }
            else if (ErrorType == "Warning Message")
            {
                return "warning-msg";
            }
            else
            {
                return "";
            }
        }

        [WebMethod]
        protected string setIconClass(string ErrorType)
        {
            if (ErrorType == "General Message")
            {
                return "ico-flag";
            }
            else if (ErrorType == "Error Message")
            {
                return "ico-cancel-white";
            }
            else if (ErrorType == "Success Message")
            {
                return "ico-check";
            }
            else if (ErrorType == "Warning Message")
            {
                return "ico-warning";
            }
            else
            {
                return "";
            }
        }

        [WebMethod]
        protected string setColorClass(string ErrorType)
        {
            if (ErrorType == "General Message")
            {
                return "ico-close-black";
            }
            else if (ErrorType == "Error Message")
            {
                return "ico-close-red";
            }
            else if (ErrorType == "Success Message")
            {
                return "ico-close-green";
            }
            else if (ErrorType == "Warning Message")
            {
                return "ico-close-orange";
            }
            else
            {
                return "";
            }
        }

    }

}
