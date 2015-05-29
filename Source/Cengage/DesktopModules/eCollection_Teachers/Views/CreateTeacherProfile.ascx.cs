using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web.UI;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using System.Globalization;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;

namespace DotNetNuke.Modules.eCollection_Teachers.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CreateTeacherProfile" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    create teacher profile screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class CreateTeacherProfile :eCollection_TeachersModuleBase
    {
        #region Page Events
            /// -----------------------------------------------------------------------------
            /// <summary>
            /// Page_PreRender runs when the control is before the page gets rendered
            /// </summary>
            /// -----------------------------------------------------------------------------
            protected void Page_PreRender(object sender, EventArgs e)
            {
                ViewState["update"] = Session["update"];
            }

            /// -----------------------------------------------------------------------------
            /// <summary>
            /// Page_Load runs when the control is loaded
            /// </summary>
            /// -----------------------------------------------------------------------------
            protected void Page_Load(object sender, EventArgs e)
            {          
                if (!IsPostBack)
                {                   
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    SeeAllLink.NavigateUrl = Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID);
                }

            }
        #endregion

        #region Button Events
            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void FinishLinkButton_Click(object sender, EventArgs e)
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=bulkupload");
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void CancelButton_Click(object sender, EventArgs e)
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
            }
        
            /// <summary>
            /// 
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void AddTeacherProfile_Click(object sender, EventArgs e)
            {
                try
                {
                    if (ValidateEmail(EmailTextBox.Text))
                    {
                        if (Session["update"].ToString() == ViewState["update"].ToString() && Session["Subscription"]!=null)
                        {
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            var UserId = DashboardController.Instance.UserDetails(new Users() { UserLoginName = TeacherLoginName, SubscriptionId = Null.SetNullInteger(Session["Subscription"]) }).UserId;
                            var uri = new Uri(Request.Url.AbsoluteUri);
                            var url = string.Concat(uri.Scheme, "://", uri.Host, "/primary?inviteesk=", encrypt(EmailTextBox.Text.Trim()), "&subsid=", encrypt(Session["Subscription"].ToString()), "&usersk=", encrypt(UserId.ToString()), "&app=ecollection");

                            Teacher CreateTeacher = new Teacher()
                            {
                                FirstName = FirstNameTextBox.Text,
                                LastName = LastNameTextBox.Text,
                                Email = EmailTextBox.Text.Trim(),
                                SubscriptionId = int.Parse(Session["Subscription"].ToString()),
                                UserLoginName = TeacherLoginName,
                                EmailUrl = url
                            };
                            CreateTeacher.FullName = string.Concat(CreateTeacher.FirstName, " ", CreateTeacher.LastName);
                            Messages.ClearMessages();
                            int? result = teacherController.InviteTeacher(CreateTeacher);
                            if (result == 1)
                            {
                                AddedName.InnerText = string.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CreateTeacher.FullName.Length > 25 ? string.Concat(CreateTeacher.FullName.Substring(0, 23), "..") : CreateTeacher.FullName), GetMessage(Constants.CREATE_SUCCESS1));
                                TeachersCount.InnerText = string.Concat(GetMessage(Constants.CREATE_SUCCESS2), AllTeachers.Count.ToString());
                                ScriptManager.RegisterStartupScript(Page, GetType(), "InviteTeacher", "<script>InviteTeacher()</script>", false);
                                //MailMessage m = new MailMessage();
                                //m.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmailAddress"], ConfigurationManager.AppSettings["AdminName"]);
                                //m.Subject = ConfigurationManager.AppSettings["InvitationText"];
                                //m.Body = string.Concat("You have been added as a Teacher to the PM eCollection. Click on this link to complete your profile in the Cengage Learning eCommerce system and fully access the benefits of the PM eCollection. ",
                                //          ConfigurationManager.AppSettings["eCommSignUp"], encrypt(Session["Subscription"].ToString()));
                                //m.To.Add(CreateTeacher.Email);

                                //SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["EmailDomainName"]);
                                //smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["NetworkCredentialUserName"], ConfigurationManager.AppSettings["NetworkCredentialPassword"]);
                                //smtp.Send(m);

                            }
                            else if (result == 0)
                            {
                                Messages.ShowInfo(GetMessage(Constants.ALREADY_INVITED));
                            }
                            else if (result == 3)
                            {
                                Messages.ShowInfo(GetMessage(Constants.ALREADY_PART_OF_SUBS1));
                            }
                            else if (result == 4)
                            {
                                Messages.ShowInfo(GetMessage(Constants.ALREADY_PART_OF_ANOTHER_SCHOOL1));
                            }
                            else
                            {
                                Messages.ShowSuccess(string.Concat(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CreateTeacher.FullName.Length > 25 ? string.Concat(CreateTeacher.FullName.Substring(0, 23), "..") : CreateTeacher.FullName), " ", GetMessage(Constants.TEACHER_ADDED_TO_SUBS)));
                            }
                            teacherController.ClearAllCache();
                        }
                    }
                    else
                    {
                        Messages.ShowWarning(GetMessage(Constants.VALIDATE_EMAIL));
                        EmailTxtBxParent.Style.Add("border", "1px solid #ED175B");
                    }
                }
                catch (Exception ex)
                {
                    Messages.ClearMessages();
                    Messages.ShowWarning(ex.Message);
                    LogFileWrite(ex);
                }
                finally
                {
                    FirstNameTextBox.Text = string.Empty; LastNameTextBox.Text = string.Empty; EmailTextBox.Text = string.Empty;
                }
            }
        #endregion
    }
}