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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using DotNetNuke.Common;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using DotNetNuke.Common.Utilities;
using System.Web.UI.HtmlControls;

namespace DotNetNuke.Modules.eCollection_Sessions
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from eCollection_SessionsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : eCollection_SessionsModuleBase, IActionable
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
                DotNetNuke.Framework.CDefault tp = (DotNetNuke.Framework.CDefault)this.Page;
                //Label pageHeader = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                HtmlGenericControl pageHeader = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("SecondaryPageHeader"); 
                //Label schoolname = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("schoolname");                
                if (Session["Subscription"] == null || Session["UserName"] == null || Session["UserName"] == string.Empty)
                {
                    SubsCnt.Value = "0";
                    Response.Redirect(ConfigurationManager.AppSettings["homepage"]);
                    //Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));
                }
                else
                {
                      var subsList = new List<DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Sessions>(from c in SubsList.AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == int.Parse(Session["Subscription"].ToString()))
                                                  select new DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Sessions
                                                  {
                                                      SubscriptionId = c.Field<int>("SubscriptionId"),
                                                      SubscriptionName = c.Field<string>("SubscriptionText"),
                                                      FullName = c.Field<string>("SubscriptionName")
                                                  });

                    SelectedSubscription.InnerText = subsList[0].FullName == string.Empty ? subsList[0].SubscriptionName : subsList[0].FullName;
                    SubsCnt.Value = SubsList.Rows.Count.ToString();
                }
                eCollectionMenuPlaceHolder.Controls.Add(Page.LoadControl(ResolveUrl("~/controls/eCollectionControls/eCollectionMenu.ascx")));


                switch (Null.SetNullString(Request.QueryString["pagename"]).ToLower())
                {
                    default:
                        {
                            pageHeader.InnerHtml = Localization.GetString("SessionsHeader", this.LocalResourceFile); 
                            PageHeaderButton.Visible = true;
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/SessionDashBoardMenu.ascx"));
                             ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Session/Sessions.ascx"));
                            PageHeaderButton.Visible = false;
                            break; 
                        }
                    case CREATESESSION:
                        {
                            pageHeader.InnerHtml  = "Create: <font color='#20B3E6'>SESSION</font>";
                            tp.Title = Localization.GetString(CREATESESSION, this.LocalResourceFile);  
                            //FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/SessionDashBoardMenu.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/CreateSessionDashBoardMenu.ascx"));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Session/CreateSession.ascx"));
                            PageHeaderButton.Text = "CANCEL MAKING SESSION";
                            PageHeaderButton.Width = Unit.Pixel(145);
                            PageHeaderButton.Style.Add("padding-left", "3px");                            
                            PageHeaderButton.Style.Add("font-family", "Raleway-regular,Raleway, Arial, sans-serif;");                            
                            PageHeaderButton.Style.Add("font-size", "7.9pt");
                            PageHeaderButton.Style.Add("padding-top", "4px !important");
                            PageHeaderButton.Visible = false;
                            PageHeaderButton.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
                            break;
                        }
                    case ADDGROUPSTOSESSION:
                        {
                            pageHeader.InnerHtml = "Create: <font color='#20B3E6'>SESSION</font>";
                            tp.Title = Localization.GetString(ADDGROUPSTOSESSION, this.LocalResourceFile);  
                            //FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/SessionDashBoardMenu.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/CreateSessionDashBoardMenu.ascx"));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Session/AddGroupsToSession.ascx"));
                            PageHeaderButton.Text = "CANCEL MAKING SESSION";
                            PageHeaderButton.Width = Unit.Pixel(145);
                            PageHeaderButton.Style.Add("padding-left", "3px");
                            PageHeaderButton.Style.Add("font-family", "Raleway-regular,Raleway, Arial, sans-serif;");
                            PageHeaderButton.Style.Add("font-size", "7.9pt");
                            PageHeaderButton.Style.Add("padding-top", "4px !important");
                            PageHeaderButton.Visible = false;  
                            PageHeaderButton.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
                            break;
                        }
                    case ADDSTUDENTTOSESSION:
                        {
                            pageHeader.InnerHtml = "Create: <font color='#20B3E6'>SESSION</font>";
                            tp.Title = Localization.GetString(ADDSTUDENTTOSESSION, this.LocalResourceFile);  
                            //FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/SessionDashBoardMenu.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/CreateSessionDashBoardMenu.ascx"));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Session/AddStudentToSession.ascx"));
                            PageHeaderButton.Text = "CANCEL MAKING SESSION";
                            PageHeaderButton.Width = Unit.Pixel(145);
                            PageHeaderButton.Style.Add("padding-left", "3px");
                            PageHeaderButton.Style.Add("font-family", "Raleway-regular,Raleway, Arial, sans-serif;");
                            PageHeaderButton.Style.Add("font-size", "7.9pt");
                            PageHeaderButton.Style.Add("padding-top", "4px !important");
                            PageHeaderButton.Visible = false;                            
                            PageHeaderButton.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
                            break;
                        }
                    case ADDTEACHERSTOSESSION:
                        {
                            pageHeader.InnerHtml = "Create: <font color='#20B3E6'>SESSION</font>";
                            tp.Title = Localization.GetString(ADDTEACHERSTOSESSION, this.LocalResourceFile);  
                            //FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/SessionDashBoardMenu.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/CreateSessionDashBoardMenu.ascx"));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Session/AddTeachersToSession.ascx"));
                            PageHeaderButton.Text = "CANCEL MAKING SESSION";
                            PageHeaderButton.Width = Unit.Pixel(145);
                            PageHeaderButton.Style.Add("padding-left", "3px");
                            PageHeaderButton.Style.Add("font-family", "Raleway-regular,Raleway, Arial, sans-serif;");
                            PageHeaderButton.Style.Add("font-size", "7.9pt");
                            PageHeaderButton.Style.Add("padding-top", "4px !important");
                            PageHeaderButton.Visible = false;  
                            PageHeaderButton.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
                            break;
                        }
                    case ADDBOOKSTOSESSION:
                        {
                            pageHeader.InnerHtml = "Create: <font color='#20B3E6'>SESSION</font>";
                            tp.Title = Localization.GetString(ADDBOOKSTOSESSION, this.LocalResourceFile);  
                            //FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/SessionDashBoardMenu.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/CreateSessionDashBoardMenu.ascx"));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Session/AddBooksToSession.ascx"));
                            PageHeaderButton.Text = "CANCEL MAKING SESSION";
                            PageHeaderButton.Width = Unit.Pixel(145);
                            PageHeaderButton.Style.Add("padding-left", "3px");
                            PageHeaderButton.Style.Add("font-family", "Raleway-regular,Raleway, Arial, sans-serif;");
                            PageHeaderButton.Style.Add("font-size", "7.9pt");
                            PageHeaderButton.Style.Add("padding-top", "4px !important");
                            PageHeaderButton.Visible = false;  
                            //PageHeaderButton.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
                            break;
                        }
                    case EDITSESSION:
                        {
                            pageHeader.InnerHtml  = "Edit: <font color='#20B3E6'>SESSION</font>";
                            //FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/SessionDashBoardMenu.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/CreateSessionDashBoardMenu.ascx"));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Session/CreateSession.ascx"));
                            PageHeaderButton.Text = "CANCEL MAKING SESSION";
                            PageHeaderButton.Width = Unit.Pixel(145);
                            PageHeaderButton.Style.Add("padding-left", "3px");
                            PageHeaderButton.Style.Add("font-family", "Raleway-regular,Raleway, Arial, sans-serif;");
                            PageHeaderButton.Style.Add("font-size", "7.9pt");
                            PageHeaderButton.Style.Add("padding-top", "4px !important");
                            PageHeaderButton.Visible = false;
                            PageHeaderButton.NavigateUrl = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
                            break;
                        }
                    case SESSIONPROFILE:
                        {
                            pageHeader.InnerHtml = "PM eCollection"; 
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Dashboard/SessionProfileDashBoardMenu.ascx"));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Sessions/Session/SessionsProfile.ascx"));
                            PageHeaderButton.Visible = false;
                            break;
                        }                   
                }
                pageHeader.Attributes.Add("style", "display:block;");
                //schoolname.Value = Null.SetNullString(Session["SchoolName"]); 
                //schoolname.Value = eCollLoginController.Instance.GetSchoolName(LoginName); 
            }            
            catch (Exception ex)
            {
                //Exceptions.ProcessModuleLoadException(this, exc);
                //this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
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

    }

}
