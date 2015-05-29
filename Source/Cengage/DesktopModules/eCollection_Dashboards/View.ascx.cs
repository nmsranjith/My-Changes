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
using DotNetNuke.Common;
using System.Globalization;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Data;
using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Login.Components.Controller;
using System.Web;
using System.Web.UI.HtmlControls;


namespace DotNetNuke.Modules.eCollection_Dashboards
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from eCollection_DashboardsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : eCollection_DashboardsModuleBase, IActionable
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
            HtmlGenericControl pageHeader = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("SecondaryPageHeader");            
            try
            {
                DotNetNuke.Framework.CDefault tp = (DotNetNuke.Framework.CDefault)this.Page;
                if (PortalSettings.ActiveTab.TabName.ToLower() == "request-access")
                {
                    pageHeader.InnerHtml = "PM eCollection";                    
                    ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/RequestAccess.ascx"));
                    return;
                }
                else if (PortalSettings.ActiveTab.TabName.ToLower() == "feedback")
                {
                    ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/FeedBack.ascx"));
                    return;
                }
                else if (Null.SetNullString(Request.QueryString["pagename"]).ToLower() == APPDATACOLLECTION)
                {
                    pageHeader.InnerHtml = "PM eCollection";
                    ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/AppDataCollection.ascx"));
                    eCollectionMenuPlaceHolder.Controls.Add(Page.LoadControl(ResolveUrl("~/controls/eCollectionControls/eCollectionMenu.ascx")));
                    pageHeader.Attributes.Add("style", "display:block;");
                    return;
                }

                if (Session["UserName"] != null)
                {
                    string cases = Null.SetNullString(Request.QueryString["pagename"]).ToLower();
                    if (SubsList.Rows.Count == 1 && SubsList.Rows[0]["fororder"].ToString() != "c" && SubsList.Rows[0]["fororder"].ToString() != "d")
                    {
                        Session["Subscription"] = int.Parse(SubsList.Rows[0]["SubscriptionId"].ToString());
                        if (cases != LOGOUT && cases != CENGAGESTAGING && cases!= UPGRADESTEPONE && cases!=UPGRADESTEPTWO && cases !=UPGRADESTEPS  )
                            cases = DASHBOARD;
                    }
                    else if (StaffDetail != null && SubsList.Rows.Count == 0 && cases != LOGOUT && Session["UserName"].ToString().ToLower() == "brendan.bolton@cengage.com")
                        cases = CENGAGESTAGING;
                    else { }

                    if (PortalSettings.ActiveTab.TabName.ToLower() == "customer-service")
                    {
                        pageHeader.InnerHtml = "PM eCollection: Customer Service";
                        ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/customerservice.ascx"));
                        return;
                    }
                    if (cases == DASHBOARD || cases == UPGRADESTEPS || cases == UPGRADESTEPONE || cases == UPGRADESTEPTWO)
                    {
                        try
                        {
                            if (Session["Subscription"] == null)
                            {
                                SubsCnt.Value = "0";
                                Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
                            }
                            else
                            {
                                var subsList = new List<Subscription>(from c in SubsList.AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == int.Parse(Session["Subscription"].ToString()))
                                                                      select new Subscription
                                                                      {
                                                                          SubsId = c.Field<int>("SubscriptionId"),
                                                                          Name = c.Field<string>("SubscriptionText"),
                                                                          NewName = c.Field<string>("TitleText")
                                                                      });

                                SelectedSubscription.InnerText = (subsList[0].NewName == string.Empty || subsList[0].NewName == null) ? subsList[0].Name : subsList[0].NewName;
                                SubsCnt.Value = SubsList.Rows.Count.ToString();                                
                            }
                            if (cases == DASHBOARD && subValidation[0] == 1 && (Allrenewels == null || Allrenewels.Count == 0))
                            {
                                cases = UPGRADESTEPS;
                            }
                        }
                        catch (Exception ex)
                        { LogFileWrite(ex); }
                    }
                 
                    switch (cases)
                    {
                        case CENGAGESTAGING:
                            pageHeader.InnerHtml = "PM eCollection";
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CengageStaging.ascx"));
                            break;
                        case DASHBOARD:
                            eCollectionMenuPlaceHolder.Controls.Add(Page.LoadControl(ResolveUrl("~/controls/eCollectionControls/eCollectionMenu.ascx")));
                            //FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Common/LeftDashboard.ascx"));
                            
                            ScriptManager.RegisterStartupScript(Page, GetType(), "SelectedMenuCss", "<script>SelectedMenuCss('DashboardTabHolder', 'DashboardTab')</script>", false);
                            Session["SelTeacherId"] = UserDetail.UserId;
                           
                            string userName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(UserDetail.FirstName, ' ', UserDetail.LastName));
                            if (userName.Length > 20)
                            {
                                userName = string.Concat(userName.Substring(0, 19).ToLower(), "..");
                                pageHeader.InnerHtml = Localization.GetString("DashboardHeader", this.LocalResourceFile) + userName + Localization.GetString("EndFont", this.LocalResourceFile);
                            }
                            else
                                pageHeader.InnerHtml = Localization.GetString("DashboardHeader", this.LocalResourceFile) + userName + Localization.GetString("EndFont", this.LocalResourceFile);

                            if (Allrenewels != null && Allrenewels.Count > 0)
                            {
                                Session["renewupgradetype"] = "renew";
                                ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/UpgradeMainScreen.ascx")); 
                            }
                            else
                            {
                              //  ScriptManager.RegisterStartupScript(Page, GetType(), "SelectedMenuCss", "<script>SelectedMenuCss('DashboardTabHolder', 'DashboardTab')</script>", false);
                            if (UserDetail.UserRole == Localization.GetString("AdminRole", this.LocalResourceFile) || UserDetail.UserRole == Localization.GetString("CengageAdminRole", this.LocalResourceFile))
                            {
                                SubsDetailsPlaceHdr.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/SubscriptionDetails.ascx"));
                            }
                            
                                VideoPlaceHdr.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/Video_GetStarted.ascx"));
                                DailyActivitiesPlaceHdr.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/DailyActivities.ascx"));
                                PurchaseDetailsPlaceHdr.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/PurchaseDetails.ascx"));
                            }
                           
                            break;
                        case LOGOUT:
                            Session.Clear(); _dashboardController.ClearAllCache();
                            RedirectToHomePage();
                            break;
                        case UPGRADESTEPS:
                            Session["renewupgradetype"] = "upgrade";
                            userName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(UserDetail.FirstName, ' ', UserDetail.LastName));
                            if (userName.Length > 20)
                            {
                                userName = string.Concat(userName.Substring(0, 19).ToLower(), "..");
                                pageHeader.InnerHtml = Localization.GetString("DashboardHeader", this.LocalResourceFile) + userName + Localization.GetString("EndFont", this.LocalResourceFile);
                           }
                            else
                            {
                                pageHeader.InnerHtml = Localization.GetString("DashboardHeader", this.LocalResourceFile) + userName + Localization.GetString("EndFont", this.LocalResourceFile);
                            }

                            eCollectionMenuPlaceHolder.Controls.Add(Page.LoadControl(ResolveUrl("~/controls/eCollectionControls/eCollectionMenu.ascx")));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/UpgradeMainScreen.ascx"));
                            break;
                        case UPGRADESTEPONE:
                            //Session["renewupgradetype"] = "upgrade";
                            userName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(UserDetail.FirstName, ' ', UserDetail.LastName));
                            if (userName.Length > 20)
                            {
                                userName = string.Concat(userName.Substring(0, 19).ToLower(), "..");
                                pageHeader.InnerHtml = Localization.GetString("DashboardHeader", this.LocalResourceFile) + userName + Localization.GetString("EndFont", this.LocalResourceFile);
                            }
                            else
                                pageHeader.InnerHtml = Localization.GetString("DashboardHeader", this.LocalResourceFile) + userName + Localization.GetString("EndFont", this.LocalResourceFile);

                            eCollectionMenuPlaceHolder.Controls.Add(Page.LoadControl(ResolveUrl("~/controls/eCollectionControls/eCollectionMenu.ascx")));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/UpgradeStepOne.ascx"));
                            break;
                        case UPGRADESTEPTWO:
                            //Session["renewupgradetype"] = "upgrade";
                            userName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(UserDetail.FirstName, ' ', UserDetail.LastName));
                            if (userName.Length > 20)
                            {
                                userName = string.Concat(userName.Substring(0, 19).ToLower(), "..");
                                pageHeader.InnerHtml = Localization.GetString("DashboardHeader", this.LocalResourceFile) + userName + Localization.GetString("EndFont", this.LocalResourceFile);
                            }
                            else
                                pageHeader.InnerHtml = Localization.GetString("DashboardHeader", this.LocalResourceFile) + userName + Localization.GetString("EndFont", this.LocalResourceFile);

                            eCollectionMenuPlaceHolder.Controls.Add(Page.LoadControl(ResolveUrl("~/controls/eCollectionControls/eCollectionMenu.ascx")));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/UpgradeStep2.ascx"));
                            break;
                        case ASIANUSERSETUP:
                             pageHeader.InnerHtml = "PM eCollection: Asian User Set Up";
                             ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/AsianUsersSetUp.ascx"));
                            break;
                        default:
                            //pageHeader.InnerHtml = Localization.GetString("SubscriptionHeader", this.LocalResourceFile);  
                            tp.Title =  Localization.GetString("Subscriptions", this.LocalResourceFile);  
                            eCollectionMenuPlaceHolder.Controls.Add(Page.LoadControl(ResolveUrl("~/controls/eCollectionControls/eCollectionMenu.ascx")));
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Dashboards/Views/SubscriptionsList.ascx"));
                            if (Session["Subscription"] != null)
                            {
                                var subsList = new List<Subscription>(from c in SubsList.AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == int.Parse(Session["Subscription"].ToString()))
                                                                      select new Subscription
                                                                      {
                                                                          SubsId = c.Field<int>("SubscriptionId"),
                                                                          Name = c.Field<string>("SubscriptionText"),
                                                                          NewName = c.Field<string>("SubscriptionName")
                                                                      });

                                SelectedSubscription.InnerText = subsList[0].NewName == string.Empty ? subsList[0].Name : subsList[0].NewName;
                                SubsCnt.Value = SubsList.Rows.Count.ToString();
                            }
                            else
                                SubsCnt.Value = "0";
                            break;
                    }
                    pageHeader.Attributes.Add("style", "display:block;");
                    PurchasePlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/Subscriptions/Views/PurchaseSubscription.ascx")); 
                    schoolname.Value = eCollLoginController.Instance.GetSchoolName(LoginName); 
                }
                else
                {                    
                    Session.Clear(); _dashboardController.ClearAllCache();
                    RedirectToHomePage();
                }
            }
            catch (Exception exc) {LogFileWrite(exc);}
        }
        
        #endregion

        #region private methods
        private void RedirectToHomePage()
        {            
            var uri = new Uri(Request.Url.AbsoluteUri);
            Response.Redirect("http://"+uri.Host);
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
