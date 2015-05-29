using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Modules.eCollection_Sessions.Components;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using DotNetNuke.Modules.eCollection_Sessions.Components.ExceptionHandling;
using _Sessions = DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Sessions;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
//using CengageLib.ExceptionHandling;
using System.Text;
using System.Data;
using DotNetNuke.Common.Utilities;
using System.Configuration;

namespace DotNetNuke.Modules.eCollection_Sessions.Session
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CreateSession" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Create Session
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class CreateSession : eCollection_SessionsModuleBase
    {
        #region Events
        int SelectedSubId = 0;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Messages.ClearMessages();
            try
            {
                SelectedSubId = Convert.ToInt32(Session["Subscription"]);
            }
            catch (Exception ex) { LogFileWrite(ex); } 
            if (!Page.IsPostBack)
            {                
                DOBHdFld.Value = DateTime.Now.Date.AddDays(7).ToString("dd/MM/yyyy");
                if (EditSessionId.HasValue && EditSessionId.Value != -1)
                {
                    if (CreateSessionId.Value == -1)
                    {
                        Session["SelectedGroups"] = null;
                        Session["SelectedProducts"] = null;
                    }
                    BindSession(EditSessionId.Value);
                }
                else
                {

                }
            }                                     

            if (EditSessionId.Value == -1)
            {
                CreateSessionButton.Text = "CREATE SESSION";
                if (Request.QueryString["returnurl"] == null && CreateSessionId==-1)
                {
                    Session["SelectedGroups"] = null;
                    Session["SelectedProducts"] = null;
                    Session["SelectedSubscriptionId"] = null;
                    //SelectedValueTextBox.Text = string.Empty;
                    //SelectedValueGroupTextBox.Text = string.Empty;
                    Session["EditSelectedId"] = null;
                    Session["SessionNotes"] = string.Empty;
                    Session["SessionType"] = string.Empty;
                    Session["SessionExpiryDate"] = null;
                    Session["SessionName"] = string.Empty;
                    CreateSessionId = 1;
                    EditSessionId = -1;
                }
            }
            else
            {
                CreateSessionId = 1;
                CreateSessionButton.Text = "SAVE SESSION";
            }

            BuildSelectedItems();
            SetSessionName();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>  
        protected void CancelMakingSessionLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelCreateSession_Click(object sender, EventArgs e)
        {
            Session["SelectedGroups"] = null;
            Session["SelectedProducts"] = null;
            SelectedValueTextBox.Text = string.Empty;
            SelectedValueGroupTextBox.Text = string.Empty;
            Session["EditSelectedId"] = null;
            Session["SessionNotes"] = string.Empty;
            Session["SessionType"] = string.Empty;
            Session["SessionExpiryDate"] = null;
            Session["SessionName"] = null;

            if (Request.QueryString["returnurl"] != null)
            {
                if (Request.QueryString["returnurl"].Equals("students"))
                {
                    Response.Redirect(Globals.NavigateURL(GetTabID(StudentsModule)));
                }
                if (Request.QueryString["returnurl"].ToUpper().Equals("GROUPS"))
                {
                    Response.Redirect(Globals.NavigateURL(GetTabID(GroupsModule)));
                }
                if (Request.QueryString["returnurl"].ToUpper().Equals("TEACHERS"))
                {
                    Response.Redirect(Globals.NavigateURL(GetTabID(TeachersModule)));
                }
                if (Request.QueryString["returnurl"].ToUpper().Equals("BOOKS"))
                {
                    Response.Redirect(Globals.NavigateURL(GetTabID(BooksModule)));
                }
            }
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddStudentButton_Click(object sender, EventArgs e)
        {
            SetSessionName();
            //Response.Redirect(Globals.NavigateURL(615));
            GetValidGroupsTeachStudents();
            if (Request.QueryString["returnurl"] == null)
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addstudenttosession");
            else
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addstudenttosession&returnurl=" + Request.QueryString["returnurl"]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddGroupsButton_Click(object sender, EventArgs e)
        {
            SetSessionName();
            GetValidGroupsTeachStudents();
            if (Request.QueryString["returnurl"] == null)
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addgroupstosession");
            else
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addgroupstosession&returnurl=" + Request.QueryString["returnurl"]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddTeachersButton_Click(object sender, EventArgs e)
        {
            SetSessionName();           
            //Response.Redirect(Globals.NavigateURL(616));
            GetValidGroupsTeachStudents();
            if (Request.QueryString["returnurl"] == null)
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addteacherstosession");
            else
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addteacherstosession&returnurl=" + Request.QueryString["returnurl"]);
    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AttachBooks_Click1(object sender, EventArgs e)
        {
            SetSessionName();              
            GetValidGroupsTeachStudents();   
            if (Request.QueryString["returnurl"] == null)
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addbookstosession");
            else
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=addbookstosession&returnurl=" + Request.QueryString["returnurl"]);    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CreateSessionButton_Click(object sender, EventArgs e)
        {            
            try
            {
                string[] StudTeaIDs = SelectedValueTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                string[] GroupIDs = SelectedValueGroupTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
                string[] ProductIDs = SelectedValueProductTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();

                SessionController sessionController = SessionController.Instance;
                List<SessionMembers> SelectedGroups = new List<SessionMembers>();
                DateTime sessionExpiryDate = DateTime.Now;
                bool IsStudentRemoved = true;
                bool IsProductRemoved = true;
                // This method will update the GroupsSelected and ProductSelection session object, based up on the removal of Students, Groups and Products . 
                GetValidGroupsTeachStudents();
                BuildSelectedItems();
                try
                {
                    SelectedSubId = Convert.ToInt32(Session["Subscription"]);
                }
                catch (Exception ex) { LogFileWrite(ex); } 
                if (SelectedSubId == 0)
                {
                    this.Messages.ShowWarning(GetMessage(Constants.MESSAGES_SUBSCRIPTION));
                    return;
                }

                if (txtNotes.Value.Trim().Length == 0 && ProductsSelected.Count == 0)
                {
                    this.Messages.ShowWarning(GetMessage(Constants.MESSAGES_NOTEBOOKS));
                    return;
                }

                if (txtSessionName.Text.Trim().Length == 0)
                {
                    this.Messages.ShowWarning(GetMessage(Constants.MESSAGES_SESSIONNAME));
                    return;
                }

                if ((GroupsSelected.Count == 0) || (StudTeaIDs.Length == 0 && GroupIDs.Length == 0))
                {
                    this.Messages.ShowWarning(GetMessage(Constants.MESSAGES_STUDENT));
                    return;
                }

                if (DOBHdFld.Value != string.Empty)
                    sessionExpiryDate = DateTime.ParseExact(DOBHdFld.Value, "dd/MM/yyyy", null);
                else
                    sessionExpiryDate = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", null);


                // TODO : Update session members 
                // If the student or group removed Make Active  ='N' and update the respective record.
                // If the student or group not newely added make Active = 'Y'
                // If the student or group added newely , add new record in the DB.
                // Bala 12-03-2013

                if (EditSessionId.Value >= 0)
                {
                    foreach (SessionProducts selectedSessionProducts in sessionController.GetSessionProducts(EditSessionId.Value))
                    {
                        IsProductRemoved = true;
                        if (selectedSessionProducts.CUST_SUBS_ITEM_SK != null)
                        {
                            foreach (string IDs in ProductIDs)
                            {
                                if (IDs.Trim().Length > 0)
                                {
                                    // If exising student available after editing
                                    if (selectedSessionProducts.CUST_SUBS_ITEM_SK == Convert.ToInt32(IDs))
                                    {
                                        IsProductRemoved = false;
                                        SessionProducts RemovedProduct = ProductsSelected.Find(e1 => e1.CUST_SUBS_ITEM_SK == selectedSessionProducts.CUST_SUBS_ITEM_SK);
                                        if (RemovedProduct != null)
                                            RemovedProduct.Active = "Y";
                                    }
                                }
                            }

                            // When AddedProducts,If existing products for this session removed when editing
                            if (IsProductRemoved)
                            {
                                // If existing students removed in the final create session page(CreateSession -> Add Students -> Remove Existing student)
                                SessionProducts RemovedProduct = ProductsSelected.Find(e1 => e1.CUST_SUBS_ITEM_SK == selectedSessionProducts.CUST_SUBS_ITEM_SK);
                                if (RemovedProduct != null)
                                {
                                    RemovedProduct.Active = "N";
                                }
                                else
                                {
                                    // If existing student removed in the first stage(Create sesson -> remove Existing Student -> Add Students)
                                    SessionProducts sessionProducts = new SessionProducts()
                                    {
                                        CUST_SUBS_ITEM_SK = selectedSessionProducts.CUST_SUBS_ITEM_SK,                                       
                                        Active = "N"
                                    };
                                    bool productExists = ProductsSelected.Any(e1 => e1.CUST_SUBS_ITEM_SK == selectedSessionProducts.CUST_SUBS_ITEM_SK);
                                    if (!productExists)
                                    {
                                        ProductsSelected.Add(sessionProducts);
                                    }
                                }
                            }
                        }
                    }

                    foreach (SessionMembers selectedSessionMembers in sessionController.GetSessionMembers(EditSessionId.Value))
                    {
                        IsStudentRemoved = true;
                        if (selectedSessionMembers.CUST_SUBS_USER_SK != null)
                        {
                            foreach (string IDs in StudTeaIDs)
                            {
                                if (IDs.Trim().Length > 0)
                                {
                                    // If exising student available after editing
                                    if (selectedSessionMembers.CUST_SUBS_USER_SK == Convert.ToInt32(IDs))
                                    {
                                        IsStudentRemoved = false;
                                        SessionMembers RemovedMember = GroupsSelected.Find(e1 => e1.CUST_SUBS_USER_SK == selectedSessionMembers.CUST_SUBS_USER_SK);
                                        if(RemovedMember != null)
                                        RemovedMember.Active = "Y";
                                    }
                                }
                            }
                            // When AddingStudents,If existing student for this session removed when editing
                            if (IsStudentRemoved)
                            {                                
                                // If existing students removed in the final create session page(CreateSession -> Add Students -> Remove Existing student)
                                SessionMembers RemovedMember = GroupsSelected.Find(e1 => e1.CUST_SUBS_USER_SK == selectedSessionMembers.CUST_SUBS_USER_SK);                                
                                if (RemovedMember != null)
                                {
                                    RemovedMember.Active = "N";
                                }
                                else
                                {
                                    // If existing student removed in the first stage(Create sesson -> remove Existing Student -> Add Students)
                                    SessionMembers sessionMembers = new SessionMembers()
                                    {
                                        CUST_SUBS_USER_SK = selectedSessionMembers.CUST_SUBS_USER_SK,
                                        StudentName = selectedSessionMembers.StudentName,
                                        MemberType = "USER",
                                        Added_date = selectedSessionMembers.Added_date,
                                        Active = "N"
                                    };
                                    bool studentExists = GroupsSelected.Any(e1 => e1.CUST_SUBS_USER_SK == selectedSessionMembers.CUST_SUBS_USER_SK);
                                    if (!studentExists)
                                    {
                                        GroupsSelected.Add(sessionMembers);
                                    }
                                }
                            }
                        }
                        IsStudentRemoved = true;
                        if (selectedSessionMembers.GRP_MEM_SK != null)
                        {
                            foreach (string IDs in GroupIDs)
                            {
                                if (IDs.Trim().Length > 0)
                                {
                                    if (selectedSessionMembers.GRP_MEM_SK == Convert.ToInt32(IDs))
                                    {
                                        IsStudentRemoved = false;
                                        SessionMembers RemovedMember = GroupsSelected.Find(e1 => e1.GRP_MEM_SK == selectedSessionMembers.GRP_MEM_SK);
                                        if (RemovedMember != null)
                                        RemovedMember.Active = "Y";
                                    }
                                }
                            }
                            if (IsStudentRemoved)
                            {
                                SessionMembers RemovedMember = GroupsSelected.Find(e1 => e1.GRP_MEM_SK == selectedSessionMembers.GRP_MEM_SK);
                                if (RemovedMember != null)
                                {
                                    RemovedMember.Active = "N";
                                }
                                else
                                {
                                    SessionMembers sessionMembers = new SessionMembers()
                                    {
                                          GRP_MEM_SK = selectedSessionMembers.GRP_MEM_SK,
                                          GroupName = selectedSessionMembers.GroupName, 
                                          MemberType = "GROUP",
                                          Added_date = selectedSessionMembers.Added_date,
                                          Active = "N"                                        
                                    };
                                    bool studentExists = GroupsSelected.Any(e1 => e1.GRP_MEM_SK == selectedSessionMembers.GRP_MEM_SK);
                                    if (!studentExists)
                                    {
                                        GroupsSelected.Add(sessionMembers);
                                    }
                                }
                            }
                        }
                    }
                }

                if (EditSessionId.Value == -1)
                {
                    // Add new Session
                    _Sessions sessions = new _Sessions
                    {
                        SessionId = 0,
                        Name = txtSessionName.Text,
                        SessionType = (MyEnums.SessionType)Convert.ToInt32(lblSessiontype.Value),
                        CUST_SUBS_SK = Convert.ToInt32(SelectedSubId),
                        CREATED_BY_CUST_SUBS_USER_SK = 5,
                        ENDED_BY_CUST_SUBS_USER_SK = 12,
                        WorkType = "sess",
                        SessionCreatedDate = DateTime.Now,
                        SessionExpiryDate = sessionExpiryDate,
                        SessionStatus = "Active",
                        IsNameManualOverride = "N",
                        Notes = txtNotes.InnerText,
                        IsActive = "Y",
                        CreatedByUserId = 1,
                        LastModifiedByUserId = 1,
                        CreatedByUserName = LoginName,
                        SessionMembers = GroupsSelected,
                        SessionProducts = ProductsSelected,
                    };
                    sessionController.Add(sessions);
                }
                else
                {
                    // Update Existing Session
                    _Sessions EditSession = AllActiveSessions.Where(u => u.SessionId == EditSessionId.Value).FirstOrDefault();
                    if (EditSession != null)
                    {
                        _Sessions sessions = new _Sessions
                        {
                            SessionId = EditSessionId.Value,
                            Name = txtSessionName.Text,
                            SessionType = (MyEnums.SessionType)Convert.ToInt32(lblSessiontype.Value),
                            CUST_SUBS_SK = Convert.ToInt32(SelectedSubId),
                            CREATED_BY_CUST_SUBS_USER_SK = 5,
                            ENDED_BY_CUST_SUBS_USER_SK = 12,
                            WorkType = "sess",
                            SessionCreatedDate = DateTime.Now,
                            SessionExpiryDate = sessionExpiryDate,
                            SessionStatus = EditSession.SessionStatus,
                            IsNameManualOverride = "Y",
                            Notes = txtNotes.InnerText,
                            IsActive = "Y",
                            CreatedByUserId = 1,
                            LastModifiedByUserId = 1,
                            CreatedByUserName = LoginName,
                            SessionMembers = GroupsSelected,
                            SessionProducts = ProductsSelected,
                        };
                        sessionController.Add(sessions);
                    }
                }
                //sessions.Name = GenerateSessionName(sessions.SessionMembers,sessions.SessionCreatedDate);
                //txtSessionName.Text = sessions.Name;                
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID)) ;

            }
            catch (SessionValidationException exc)
            {
                if (exc.getErrorState() == MyEnums.CrudState.Insert)
                {
                    Messages.ShowWarning(exc.getErrorMessage());
                }
                else
                    LogFileWrite(exc);
            }
            catch (Exception ex) { LogFileWrite(ex); }       
        }
        #endregion

        #region MemberFunction
        /// <summary>
        /// 
        /// </summary>
        private void SetSessionName()
        {
            if (Null.SetNullString(Session["SessionName"]) != txtSessionName.Text && SessionNameHdn.Value == string.Empty)
                SessionNameHdn.Value = Null.SetNullString(Session["SessionName"]);
            else
                Session["SessionName"] = txtSessionName.Text;
            if (GroupsSelected.Count > 0)
            {
                if (EditSessionId.Value == -1)
                {
                    if (txtSessionName.Text.Trim().Equals(string.Empty))
                        txtSessionName.Text = GenerateSessionName(GroupsSelected, DateTime.Now, SessionNameHdn.Value);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SessionId"></param>
        private void BindSession(int SessionId)
        {
            try
            {
                _Sessions sessions = AllActiveSessions.Where(u => u.SessionId == SessionId).FirstOrDefault();
                SessionController sessionController = SessionController.Instance;
                Session["SessionNotes"] = string.Empty;
                Session["SessionType"] = string.Empty;
                Session["SessionExpiryDate"] = null;
                Session["SessionName"] = string.Empty;
                if (sessions != null)
                {
                    txtSessionName.Text = sessions.Name;
                    txtNotes.InnerText = sessions.Notes;
                    //this.DrpSubscription.SelectedValue = sessions.CUST_SUBS_SK.ToString();
                    //this.SessionDropDownList.DataValueField = "Next Week";
                    //if (sessions.SessionExpiryDate.Date == DateTime.Now.Date)
                    //{
                    //    SessionDropDownList.SelectedValue = "Today";
                    //}
                    //else if (sessions.SessionExpiryDate.Date < DateTime.Now.Date)
                    //{
                    //    SessionDropDownList.SelectedValue = "Today";
                    //}
                    //else 
                    //{
                    //    SessionDropDownList.SelectedValue = "Next Week";
                    //}

                    DOBHdFld.Value = sessions.SessionExpiryDate.Date.ToString("dd/MM/yyyy");

                    if (sessions.SessionType == MyEnums.SessionType.Guided)
                    {
                        //Switch.Attributes.Add("style", "background-image:url('Portals/0/images/guided_on.png');display: inline-block; float: left; margin-top: 14px; background-position: center; background-repeat: no-repeat;height: 39px; margin-left: 100px");
                        Switch.Attributes.Add("class", "CreateSwitch");
                        lblSessiontype.Value = "1";
                    }
                    else
                    {
                        //Switch.Attributes.Add("style", "background-image:url('Portals/0/images/independant_on.png');display: inline-block; float: left; margin-top: 14px; background-position: center; background-repeat: no-repeat;height: 39px; margin-left: 100px");
                        Switch.Attributes.Add("class", "CreateSwitch independant_on");
                        lblSessiontype.Value = "2";
                    }
                    if (GroupsSelected.Count == 0)
                    {
                        GroupsSelected = sessionController.GetSessionMembers(sessions.SessionId.Value);
                    }
                    if (ProductsSelected.Count == 0)
                    {
                        ProductsSelected = sessionController.GetSessionProducts(sessions.SessionId.Value);
                    }
                    //DrpSubscription.Enabled = false;
                    //BuildSelectedItems();
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }     
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<SessionMembers> GetValidGroupsTeachStudents()
        {
            List<SessionMembers> _validGroupsTeachStudents = new List<SessionMembers>();
            List<SessionProducts> _validProducts= new List<SessionProducts>();

            string[] StudTeaIDs = SelectedValueTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            string[] GroupIDs = SelectedValueGroupTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();
            string[] ProductIDs = SelectedValueProductTextBox.Text.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToArray();

            
            foreach (string IDs in StudTeaIDs)
            {
                if (IDs.Trim().Length > 0)
                {
                    foreach (SessionMembers sm in GroupsSelected)
                    {
                        if (sm.CUST_SUBS_USER_SK == Convert.ToInt32(IDs))
                        {
                            bool studentExists = _validGroupsTeachStudents.Any(e => e.CUST_SUBS_USER_SK == sm.CUST_SUBS_USER_SK);
                            if (!studentExists)
                            {
                                _validGroupsTeachStudents.Add(sm);
                                break;
                            }
                        }
                    }
                }
            }

            foreach (string IDs in GroupIDs)
            {
                if (IDs.Trim().Length > 0)
                {
                    foreach (SessionMembers sm in GroupsSelected)
                    {
                        if (sm.GRP_MEM_SK == Convert.ToInt32(IDs))
                        {
                            if (!_validGroupsTeachStudents.Contains(sm))
                            {
                                _validGroupsTeachStudents.Add(sm);
                                break;

                            }
                        }
                    }
                }
            }


            foreach (string IDs in ProductIDs)
            {
                if (IDs.Trim().Length > 0)
                {
                    foreach (SessionProducts sp in ProductsSelected)
                    {
                        if (sp.CUST_SUBS_ITEM_SK == Convert.ToInt32(IDs))
                        {
                            bool productExists = _validProducts.Any(e => e.CUST_SUBS_ITEM_SK == sp.CUST_SUBS_ITEM_SK);
                            if (!productExists)
                            {
                                _validProducts.Add(sp);
                                break;
                            }
                        }
                    }
                }
            }

            GroupsSelected = _validGroupsTeachStudents;
            ProductsSelected = _validProducts;
            SessionNotes = txtNotes.InnerText;
            SessionType = lblSessiontype.Value;

            SessionExpiryDate = DateTime.ParseExact(DOBHdFld.Value.Replace('-','/'), "dd/MM/yyyy", null);
            //SessionName = txtSessionName.Text;
            //Session["NewSessionName"] = txtSessionName.Text;
            return _validGroupsTeachStudents;            
        }

        /// <summary>
        /// 
        /// </summary>
        private void    BuildSelectedItems()
        {
            try
            {
                StringBuilder output = new StringBuilder();
                List<SessionMembers> SelectedGroups = GroupsSelected;
                foreach (SessionMembers sm in SelectedGroups)
                {
                    if (sm.MemberType == "GROUP" && sm.GroupName.Trim().Length > 0)
                    {
                        output.AppendFormat("<li class=\"SelectedGroupItem\"><span title=" + sm.GroupName + ">{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'RemoveGroup(this);\'>x</a></li>", TruncateName(sm.GroupName), sm.GRP_MEM_SK);
                        SelectedValueGroupTextBox.Text += sm.GRP_MEM_SK + ",";
                    }
                    if (sm.MemberType == "USER" && sm.StudentName.Trim().Length > 0)
                    {
                        output.AppendFormat("<li class=\"SelectedStudentItem\"><span title=" + sm.StudentName + ">{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(this);\'>x</a></li>", TruncateName(sm.StudentName), sm.CUST_SUBS_USER_SK);
                        SelectedValueTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                    }
                    if (sm.MemberType == "USER" && sm.TeacherName.Trim().Length > 0)
                    {
                        output.AppendFormat("<li class=\"SelectedTeacherItem\"><span title=" + sm.TeacherName + ">{0}</span><span style=\'display:none\'>{1}</span><a onclick=\'Remove(this);\'>x</a></li>", TruncateName(sm.TeacherName), sm.CUST_SUBS_USER_SK);
                        SelectedValueTextBox.Text += sm.CUST_SUBS_USER_SK + ",";
                    }
                }               
                SelectedStudentList.InnerHtml = output.ToString();

                List<SessionProducts> SelectedProducts = ProductsSelected;
                if (SelectedProducts != null)
                {
                    BookAddedRepeater.DataSource = ProductsSelected;
                    BookAddedRepeater.DataBind();
                }
                foreach (SessionProducts sp in ProductsSelected)
                {
                    SelectedValueProductTextBox.Text += sp.CUST_SUBS_ITEM_SK + ",";
                }
                if (SessionNotes.Trim().Length > 0)
                {
                    txtNotes.InnerText = SessionNotes;
                }

                if (SessionType.Trim().Length > 0)
                {
                    if (SessionType == "1")
                    {
                        //Switch.Attributes.Add("style", "background-image:url('Portals/0/images/guided_on.png');display: inline-block; float: left; margin-top: 14px; background-position: center; background-repeat: no-repeat;height: 39px; margin-left: 100px");
                        lblSessiontype.Value = "1";
                        Switch.Attributes.Add("class", "CreateSwitch");          
                    }
                    else if (SessionType == "2")
                    {
                        //Switch.Attributes.Add("style", "background-image:url('Portals/0/images/independant_on.png');display: inline-block; float: left; margin-top: 14px; background-position: center; background-repeat: no-repeat;height: 39px; margin-left: 100px");
                        lblSessiontype.Value = "2";
                        Switch.Attributes.Add("class", "CreateSwitch independant_on");        
                    }
                }
                if (SessionExpiryDate != DateTime.MinValue)
                {
                    DOBHdFld.Value = SessionExpiryDate.Date.ToString("dd/MM/yyyy");
                }               
            }
            catch (Exception ex) { LogFileWrite(ex); }     
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        private string TruncateName(string Name)
        {            
            return (Name.ToString().Length >= 10 ? Name.ToString().Substring(0, 9) + " ..." : Name.ToString());
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookAddedRepeater_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                Image BookCoverImage = (Image)e.Item.FindControl("AddedBooks");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

    }
}