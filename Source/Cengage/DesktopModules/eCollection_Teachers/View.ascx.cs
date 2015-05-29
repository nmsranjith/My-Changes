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
using System.Globalization;
using System.Configuration;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using System.Web.UI.HtmlControls;


namespace DotNetNuke.Modules.eCollection_Teachers
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from eCollection_TeachersModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : eCollection_TeachersModuleBase, IActionable
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
                  HtmlGenericControl pageHeader = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("SecondaryPageHeader"); 
                  //Session["UserName"] = ConfigurationManager.AppSettings["SessonLoginName"];
                  //Label pageHeader = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                  //Label schoolname = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("schoolname");
                  
                  if (Session["UserName"] != null)
                  {
                      if (Session["Subscription"] == null)
                      {
                          SubsCnt.Value = "0";                        
                          Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));
                      }
                      else
                      {
                          var subsList = new List<Teacher>(from c in SubsList.AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == int.Parse(Session["Subscription"].ToString()))
                                                           select new Teacher
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
                                  pageHeader.InnerHtml = Localization.GetString("HomePage", this.LocalResourceFile);
                                  FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Dashboard/TeachersListDashboard.ascx"));
                                  ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Views/TeachersList.ascx"));                                  
                                  break;
                              }
                          case CREATETEACHERPROFILE:
                              {
                                  pageHeader.InnerHtml = Localization.GetString("CreateHeader", this.LocalResourceFile);
                                  tp.Title = Localization.GetString(CREATETEACHERPROFILE, this.LocalResourceFile);  
                                  FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Dashboard/CreateTeacherProfileDashboard.ascx"));
                                  ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Views/CreateTeacherProfile.ascx"));                                 
                                 
                                  break;
                              }
                          case ADDTOGROUP:
                              {                                  
                                  pageHeader.InnerHtml = Localization.GetString("HomePage", this.LocalResourceFile);
                                  ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Views/AddToGroup.ascx"));
                                  FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Dashboard/CreateTeacherProfileDashboard.ascx"));
                                  break;
                              }
                          case TEACHERPROFILEBULKUPLOAD:
                              {
                                  pageHeader.InnerHtml = Localization.GetString("CreateHeader", this.LocalResourceFile);
                                  tp.Title = Localization.GetString(TEACHERPROFILEBULKUPLOAD, this.LocalResourceFile);  
                                  FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Dashboard/TeachersBulkUploadDashboard.ascx"));
                                  ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Views/TeachersBulkUpload.ascx"));
                             
                                  break;
                              }
                          case TEACHERPROFILE:
                              {
                                  tp.Title = Localization.GetString(TEACHERPROFILE, this.LocalResourceFile);  
                                  if (Request.QueryString["username"] != null)
                                  {
                                      string teacherName = selectedTeacher().FirstName.TrimEnd('.');
                                      if (teacherName.Length >= 12)
                                      {
                                          teacherName = string.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(teacherName.Substring(0, 12).ToLower()), "..");
                                          pageHeader.InnerHtml = Localization.GetString("ProfileHeader1", this.LocalResourceFile) + teacherName + Localization.GetString("EndFont", this.LocalResourceFile);
                                      }
                                      else
                                          pageHeader.InnerHtml = Localization.GetString("ProfileHeader", this.LocalResourceFile) + teacherName + Localization.GetString("EndFont", this.LocalResourceFile);

                                      ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Views/TeacherProfile.ascx"));
                                    
                                  }
                                  else
                                  {
                                      Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
                                  }
                                  break;

                              }
                          case MYWORDS:
                              {
                                  if (Request.QueryString["username"] != null)
                                  {
                                      string teacherName = selectedTeacher().FirstName.TrimEnd('.');
                                      if (teacherName.Length >= 12)
                                      {
                                          teacherName = string.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(teacherName.Substring(0, 12).ToLower()), "..");
                                          pageHeader.InnerHtml = Localization.GetString("ProfileHeader1", this.LocalResourceFile) + teacherName + Localization.GetString("EndFont", this.LocalResourceFile);
                                      }
                                      else
                                          pageHeader.InnerHtml = Localization.GetString("ProfileHeader", this.LocalResourceFile) + teacherName + Localization.GetString("EndFont", this.LocalResourceFile);
                                      ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Views/MyWords.ascx"));
                                      
                                  }
                                  else
                                  {
                                      Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
                                  }
                                  break;
                              }
                          case MYRECORDINGS:
                              {
                                  if (Request.QueryString["username"] != null)
                                  {
                                      string teacherName = selectedTeacher().FirstName.TrimEnd('.');
                                      if (teacherName.Length >= 12)
                                      {
                                          teacherName = string.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(teacherName.Substring(0, 12).ToLower()), "..");
                                          pageHeader.InnerHtml = Localization.GetString("ProfileHeader1", this.LocalResourceFile) + teacherName + Localization.GetString("EndFont", this.LocalResourceFile);
                                      }
                                      else
                                          pageHeader.InnerHtml = Localization.GetString("ProfileHeader", this.LocalResourceFile) + teacherName + Localization.GetString("EndFont", this.LocalResourceFile);
                                      ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Teachers/Views/Recordings.ascx"));
                                      
                                  }
                                  else
                                  {
                                      Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
                                  }
                                  break;
                              }
                      }
                      pageHeader.Attributes.Add("style", "display:block;");
                      //schoolname.Value = eCollLoginController.Instance.GetSchoolName(TeacherLoginName); 
                      //Session["SchoolName"] = schoolname.Value = Null.SetNullString(Session["SchoolName"]);
                  }
                  else
                  {
                      Response.Redirect(ConfigurationManager.AppSettings["homepage"]);
                   //   Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));
                  }
            }
            catch (Exception exc) //Module failed to load
            {
                //Exceptions.ProcessModuleLoadException(this, exc);
                //DotNetNuke.Services.Exceptions.Exceptions.LogException(exc);
                LogFileWrite(exc);
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
