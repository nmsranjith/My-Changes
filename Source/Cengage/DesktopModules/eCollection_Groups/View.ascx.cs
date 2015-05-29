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
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using DotNetNuke.Common.Utilities;
using System.Web.UI.HtmlControls;


namespace DotNetNuke.Modules.eCollection_Groups
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from eCollection_GroupsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : eCollection_GroupsModuleBase, IActionable
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
                string cases = Null.SetNullString(Request.QueryString["pagename"]).ToLower();
                //Label pageHeader = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                HtmlGenericControl pageHeader = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("SecondaryPageHeader"); 
                //Label schoolname = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("schoolname");                
                if (Session["Subscription"] == null)
                {
                    SubsCnt.Value = "0";
                    Response.Redirect(ConfigurationManager.AppSettings["homepage"]);
                    //Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));
                }
                else
                {
                    var subsList = new List<Components.Groups>(from c in SubsList.AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == int.Parse(Session["Subscription"].ToString()))
                                                               select new Components.Groups
                                                                {
                                                                    SubscriptionId = c.Field<int>("SubscriptionId"),
                                                                    SubscriptionName = c.Field<string>("SubscriptionText"),
                                                                    FullName = c.Field<string>("SubscriptionName")
                                                                });

                    SelectedSubscription.InnerText = subsList[0].FullName == string.Empty ? subsList[0].SubscriptionName : subsList[0].FullName;
                    SubsCnt.Value = SubsList.Rows.Count.ToString();
                }
                if (cases != CENGAGESTAGING)
                    eCollectionMenuPlaceHolder.Controls.Add(Page.LoadControl("controls/eCollectionControls/eCollectionMenu.ascx"));

                switch (cases)
                {

                    case CREATEGROUP:
                        {
                            pageHeader.InnerHtml = "Create: <font color='#20B3E6'>GROUP</font>";
                            tp.Title = Localization.GetString(CREATEGROUP, this.LocalResourceFile);  
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CreateGroup.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CreateGroupDashBoard.ascx"));
                            break;
                        }
                    case ADDSTUDENTTOCREATEGROUP:
                        {
                            pageHeader.InnerHtml = "Create: <font color='#20B3E6'>GROUP</font>";
                            tp.Title = Localization.GetString(ADDSTUDENTTOCREATEGROUP, this.LocalResourceFile);  
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/AddStudentstoCreateGroup.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CreateGroupDashBoard.ascx"));
                            break;
                        }
                    case ADDTEACHERTOCREATEGROUP:
                        {
                            pageHeader.InnerHtml = "Create: <font color='#20B3E6'>GROUP</font>";
                            tp.Title = Localization.GetString(ADDTEACHERTOCREATEGROUP, this.LocalResourceFile);  
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/AddTeachersToCreateGroup.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CreateGroupDashBoard.ascx"));
                            break;
                        }
                    case EDITGROUP:
                        {
                            pageHeader.InnerHtml = "Edit: <font color='#20B3E6'>Group</font>";
                            tp.Title = Localization.GetString(EDITGROUP, this.LocalResourceFile);  
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CreateGroup.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CreateGroupDashBoard.ascx"));
                            break;
                        }
                    case ADDSTUDENTTOEDITGROUP:
                        {
                            pageHeader.InnerHtml = "Edit: <font color='#20B3E6'>GROUP</font>";
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/AddStudentstoCreateGroup.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CreateGroupDashBoard.ascx"));
                            break;
                        }
                    case ADDTEACHERTOEDITGROUP:
                        {
                            pageHeader.InnerHtml = "Edit: <font color='#20B3E6'>GROUP</font>"; 
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/AddTeachersToCreateGroup.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CreateGroupDashBoard.ascx"));
                            break;
                        }
                    case GROUPPROFILE:
                        {
                            pageHeader.InnerHtml = "PM eCollection:<font style='color:#20B3E6;padding-left: 17px;'>" + (GroupName.Length > 20 ? string.Concat(GroupName.Substring(0, 20), "..") : GroupName) + "</font>";
                            tp.Title = Localization.GetString(GROUPPROFILE, this.LocalResourceFile);  
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/GroupProfile.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/GroupProfileDashBoard.ascx"));
                            PageHeaderButton.Visible = false;
                            break;
                        }
                    case WORDS:
                        {
                            pageHeader.InnerHtml = "PM eCollection:<font style='color:#20B3E6;padding-left: 17px;'>" + (GroupName.Length > 20 ? string.Concat(GroupName.Substring(0, 20), "..") : GroupName) + "</font>";
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/GroupWords.ascx"));

                            PageHeaderButton.Visible = false;
                            break;
                        }
                    case RECORDINGS:
                        {
                            pageHeader.InnerHtml = "PM eCollection:<font style='color:#20B3E6;padding-left: 17px;'>" + (GroupName.Length > 20 ? string.Concat(GroupName.Substring(0, 20), "..") : GroupName) + "</font>";
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/GroupRecordings.ascx"));

                            PageHeaderButton.Visible = false;
                            break;
                        }
                    case CENGAGESTAGING:
                        {
                            pageHeader.InnerHtml = "PM eCollection";
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/CengageStaging.ascx"));
                            PageHeaderButton.Visible = false;
                            break;
                        }
                    default:
                        {
                            pageHeader.InnerHtml = Localization.GetString("GroupsHeader", this.LocalResourceFile); 
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/GroupList.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Groups/Groups/GroupListDashBoardMenu.ascx"));

                            PageHeaderButton.Visible = false;
                            break;
                        }

                }
                pageHeader.Attributes.Add("style", "display:block;");
               // schoolname.Value = eCollLoginController.Instance.GetSchoolName(LoginName); 
               // Session["SchoolName"]=schoolname.Value = Null.SetNullString(Session["SchoolName"]);
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
                Exceptions.ProcessModuleLoadException(this, ex);
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
