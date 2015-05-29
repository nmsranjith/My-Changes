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
using System.Web.UI;
using System.Globalization;
using System.Configuration;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using System.Web.UI.HtmlControls;


namespace DotNetNuke.Modules.eCollection_Students
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from eCollection_StudentsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : eCollection_StudentsModuleBase, IActionable
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
                HtmlGenericControl pageHeader = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("SecondaryPageHeader"); 
                DotNetNuke.Framework.CDefault tp = (DotNetNuke.Framework.CDefault)this.Page;
                if (Session["UserName"] != null)
                {                    
                    if (Session["Subscription"] == null)
                    {
                        SubsCnt.Value = "0";
                        Response.Redirect(ConfigurationManager.AppSettings["homepage"]);
                    }
                    else
                    {
                        var subsList = new List<Student>(from c in SubsList.AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == int.Parse(Session["Subscription"].ToString()))
                                                              select new Student
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
                                //pageHeader.InnerHtml = Localization.GetString("HomePage", this.LocalResourceFile);
                                FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/StudentsListDashboard.ascx"));
                                ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/StudentsList.ascx"));
                                if (AllOtherStudents != null)
                                    StudentsCount.Value = AllOtherStudents.Count.ToString();
                                PageHeaderButton.Visible = false;
                                break;
                            }
                        case CREATENEW:
                            {
                          //      pageHeader.InnerHtml = Localization.GetString("CreateHeader", this.LocalResourceFile);                                
                                tp.Title = Localization.GetString(CREATESTUDENTPROFILE, this.LocalResourceFile);
                                FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/CreateStudentProfileDashboard.ascx"));
                                ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/UnallocatedStudents.ascx"));
                                break;
                            }
                        case UNALLOCATED:
                            {
                           //     pageHeader.InnerHtml = Localization.GetString("CreateHeader", this.LocalResourceFile);
                                tp.Title = Localization.GetString(CREATESTUDENTPROFILE, this.LocalResourceFile);
                                FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/CreateStudentProfileDashboard.ascx"));
                                ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/UnallocatedStudents.ascx"));
                                break;
                            }
                        case BULKUPLOAD:
                            {
                             //   pageHeader.InnerHtml = Localization.GetString("CreateHeader", this.LocalResourceFile);
                                tp.Title = Localization.GetString(CREATESTUDENTPROFILE, this.LocalResourceFile);
                                FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/CreateStudentProfileDashboard.ascx"));
                                ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/UnallocatedStudents.ascx"));
                                break;
                            }
                        case STARTCREATION:
                            {
                            //    pageHeader.InnerHtml = Localization.GetString("CreateHeader", this.LocalResourceFile);
                                tp.Title = Localization.GetString(CREATESTUDENTPROFILE, this.LocalResourceFile);
                                FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/CreateStudentProfileDashboard.ascx"));
                                ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/StartCreateStudents.ascx"));
                                break;
                            }
                        //case CREATESTUDENTPROFILE:
                        //    {
                        //        pageHeader.InnerHtml = Localization.GetString("CreateHeader", this.LocalResourceFile);
                        //        tp.Title = Localization.GetString(CREATESTUDENTPROFILE, this.LocalResourceFile);  
                        //        if (Request.QueryString["seeall"] != null && Request.QueryString["seeall"] == SEEALL)
                        //        {
                        //            pageHeader.InnerHtml = Localization.GetString("HomePage", this.LocalResourceFile);
                        //            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/StudentsListDashboard.ascx"));
                        //            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/SeeAllList.ascx"));
                        //            break;
                        //        }
                        //        FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/CreateStudentProfileDashboard.ascx"));
                        //        ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/CreateStudentProfile.ascx"));                                                            
                        //        break;
                        //    }
                        case SEEALL:
                            {
                                pageHeader.InnerHtml = Localization.GetString("HomePage", this.LocalResourceFile);
                                FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/StudentsListDashboard.ascx"));
                                ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/SeeAllList.ascx"));
                                break;
                            }
                        //case STUDENTBULKUPLOAD:
                        //    {
                        //        pageHeader.InnerHtml = Localization.GetString("CreateHeader", this.LocalResourceFile);
                        //        tp.Title = Localization.GetString(STUDENTBULKUPLOAD, this.LocalResourceFile);  
                        //        FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/studentsProfileBulkUploadDashboard.ascx"));
                        //        ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/StudentsProfileBulkUpload.ascx"));                                                               
                        //        break;
                        //    }
                        case STUDENTPROFILE:
                            {
                                tp.Title = Localization.GetString(STUDENTPROFILE, this.LocalResourceFile);  
                                if (Request.QueryString["username"] != null)
                                {
                                    string stuName = selectedStudent().FirstName.TrimEnd('.', ' ');
                                    if (stuName.Length >= 12)                                                                          
                                        pageHeader.InnerHtml = string.Concat(Localization.GetString("ProfileHeader1", this.LocalResourceFile) , stuName , Localization.GetString("ProfileHeader1End", this.LocalResourceFile),string.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stuName.Substring(0, 12).ToLower()), ".."),Localization.GetString("EndFont", this.LocalResourceFile));                                    
                                    else
                                        pageHeader.InnerHtml = Localization.GetString("ProfileHeader", this.LocalResourceFile) + stuName + Localization.GetString("EndFont", this.LocalResourceFile);
                                    ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/StudentProfile.ascx"));
                                    FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/CreateStudentProfileDashboard.ascx"));
                                }
                                else
                                {
                                    Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
                                }                                
                                break;
                            }
                        case EDITSTUDENTPROFILE:
                            {
                                tp.Title = Localization.GetString(EDITSTUDENTPROFILE, this.LocalResourceFile);  
                                if (Request.QueryString["username"] != null)
                                {
                                    pageHeader.InnerHtml = Localization.GetString("EditHeader", this.LocalResourceFile);
                                    ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/EditStudentProfile.ascx"));
                                    FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/CreateStudentProfileDashboard.ascx"));
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
                                    string stuName = selectedStudent().FirstName.TrimEnd('.', ' ');
                                    if (stuName.Length >= 12)
                                        pageHeader.InnerHtml = string.Concat(Localization.GetString("ProfileHeader1", this.LocalResourceFile), stuName, Localization.GetString("ProfileHeader1End", this.LocalResourceFile), string.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stuName.Substring(0, 12).ToLower()), ".."), Localization.GetString("EndFont", this.LocalResourceFile));
                                    else
                                        pageHeader.InnerHtml = Localization.GetString("ProfileHeader", this.LocalResourceFile) + stuName + Localization.GetString("EndFont", this.LocalResourceFile);
                                    ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/MyWords.ascx"));                                    
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
                                    string stuName = selectedStudent().FirstName.TrimEnd('.', ' ');
                                    if (stuName.Length >= 12)
                                        pageHeader.InnerHtml = string.Concat(Localization.GetString("ProfileHeader1", this.LocalResourceFile), stuName, Localization.GetString("ProfileHeader1End", this.LocalResourceFile), string.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stuName.Substring(0, 12).ToLower()), ".."), Localization.GetString("EndFont", this.LocalResourceFile));
                                    else
                                        pageHeader.InnerHtml = Localization.GetString("ProfileHeader", this.LocalResourceFile) + stuName + Localization.GetString("EndFont", this.LocalResourceFile);
                                    ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/Recordings.ascx"));                                  
                                }

                                else
                                {
                                    Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
                                }
                                break;
                            }
                        case ADDTOGROUP:
                            {
                                pageHeader.InnerHtml = Localization.GetString("HomePage", this.LocalResourceFile);
                                ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Views/AddToGroup.ascx"));
                                FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Students/Dashboard/CreateStudentProfileDashboard.ascx"));
                                break;
                            }
                    }
                    //pageHeader.Attributes.Add("style", "display:block;");
                    //schoolname.Value = eCollLoginController.Instance.GetSchoolName(TeacherLoginName); 
                   // Session["SchoolName"] = schoolname.Value = Null.SetNullString(Session["SchoolName"]);
                }
                else
                {
                    Response.Redirect("/pmecollection?type=auto");//ConfigurationManager.AppSettings["homepage"]);
                    //Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));                   
                }
            }
            catch (Exception ex) //Module failed to load
            {
                //Exceptions.ProcessModuleLoadException(this, exc);
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
                Actions.Add(GetNextActionID(), Localization.GetString("EditModule",this.LocalResourceFile), "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);            
                return Actions;
            }
        }

        #endregion
    }

}
