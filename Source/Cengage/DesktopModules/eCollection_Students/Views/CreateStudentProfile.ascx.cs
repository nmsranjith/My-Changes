using System;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.Globalization;
using DotNetNuke.Common.Utilities;
using System.Collections.Generic;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CreateStudentProfile" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To create a student profile
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class CreateStudentProfile : eCollection_StudentsModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["AddedStudentCount"] == null)
                        Session["AddedStudentCount"] = 0;
                    if (Session["AddedStudentList"] == null)
                        Session["AddedStudentList"] = new List<Student>();
                    SeeAllLink.HRef= string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID),"?pagename=",SEEALL);
                    if (Session["Subscription"] != null)
                    {
                        CheckForLicenseExhaustion();
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                    else
                        Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));
                }
            }
            catch (Exception ex)
            {
                Messages.ShowWarning("Error in student creation.");
                LogFileWrite(ex);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_PreRender runs when the control is rendered
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["update"] = Session["update"];
        }

        /// <summary>
        ///  Create a student profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddStudentProfile_Click(object sender, EventArgs e)
        {
            try
            {                 
                if (Session["update"] != null)
                {
                    if (Session["update"].ToString() == ViewState["update"].ToString())
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        if (CheckForLicenseExhaustion() == 1)
                            return;
                        else { }
                        string[] levels = SliderValue.Value.Split('-');
                        Student createStudent = new Student()
                        {
                            UserDomain = EnumHelper.GetDescription(MyEnums.Users.UserDomain),
                            InternalUse = (char)(MyEnums.Users.InternalUse),
                            FirstName = FirstNameTextBox.Text.Trim(),
                            LastName = LastNameTextBox.Text.Trim(),
                            Email = EmailTextBox.Text.Trim(),
                            UserLoginName = StudentUserNameTextBox.Text.Trim(),
                            Password = encrypt(PasswordTextBox.Text.Trim()),
                            Active = (char)MyEnums.Active.Yes,
                            UserCreated = TeacherLoginName,
                            Gender = char.Parse(GenderDropDown.Value),
                            StartingReadinglevel = int.Parse(PMReadingLevel.Value),
                            CurrentReadingLevel = int.Parse(PMReadingLevel.Value),
                            StartingReadinglevelFrom = int.Parse(levels[0]) + 1,
                            StartingReadinglevelUpto = int.Parse(levels[1]),
                            CurrentReadinglevelFrom = int.Parse(levels[0]) + 1,
                            CurrentReadinglevelUpto = int.Parse(levels[1]),
                            Grade = GradeDropDown.Value,
                            ESL = char.Parse(ESLHdn.Value),
                            ReadingRecovery = char.Parse(RRHdn.Value),
                            AddedDate = DateTime.Now,
                            Description = string.Concat(EnumHelper.GetDescription(MyEnums.TradingPartnerUser.Prefix), FirstNameTextBox.Text.ToUpper(), EnumHelper.GetDescription(MyEnums.TradingPartnerUser.Description)),
                            TeacherLoginName = TeacherLoginName,
                            PurchaseFlag = (char)MyEnums.TradingPartnerUser.PurchaseFlag,
                            IsDefaultPartner = (char)MyEnums.TradingPartnerUser.IsDefaultPartner,
                            SubscriptionId = int.Parse(Session["Subscription"].ToString())
                        };
                        if (DateofBirthTextBox.Value != string.Empty)
                        {
                            if (DateTime.ParseExact(DateofBirthTextBox.Value, "dd/MM/yyyy", null) >= DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null))
                            {
                                Messages.ShowWarning(GetMessage(Constants.VALIDATE_DOB2));
                                PasswordTextBox.Text = createStudent.Password;
                                return;
                            }
                            else
                                createStudent.DateofBirth = DateTime.ParseExact(DateofBirthTextBox.Value, "dd/MM/yyyy", null);
                        }

                        Messages.ClearMessages();
                        UserCount.Value = studentController.Add(createStudent).ToString();
                        if (UserCount.Value == "0")
                        {
                            Session["AddedStudentCount"] = Null.SetNullInteger(Session["AddedStudentCount"]) + 1;
                            CreateForm.Controls.OfType<TextBox>().ToList().ForEach(textBox => textBox.Text = string.Empty);
                            createStudent.FullName = string.Concat(createStudent.FirstName, " ", createStudent.LastName);
                            AddedName.InnerText = string.Concat(createStudent.FullName.Length > 15 ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(string.Concat(createStudent.FullName.Substring(0, 15), "..")) : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(createStudent.FullName), GetMessage(Constants.CREATE_SUCCESS1));
                            StudentsCount.InnerText = string.Concat(GetMessage(Constants.CREATE_SUCCESS2), Null.SetNullInteger(Session["AddedStudentCount"]));
                            ScriptManager.RegisterStartupScript(Page, GetType(), "CreateProfilesSuccess", "<script>CreateProfilesSuccess()</script>", false);
                            (Session["AddedStudentList"] as List<Student>).Add(AllOtherStudents.Find(stu=>stu.UserLoginName==createStudent.UserLoginName));
                        }
                        else
                        {
                            PasswordTextBox.Text = createStudent.Password;
                            ScriptManager.RegisterStartupScript(Page, GetType(), "UserNameExists", "<script>UserNameExists()</script>", false);
                        }
                    }
                    else
                        Response.Redirect(string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID), "?pagename=", CREATESTUDENTPROFILE));
                }
                else
                {
                    Messages.ShowInfo(GetMessage(Constants.SESSION_EXPIRE));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "String was not recognized as a valid DateTime.")
                    Messages.ShowWarning(GetMessage(Constants.VALIDATE_DOB1));
                else
                {
                    Messages.ShowWarning("Error in student creation.");
                    LogFileWrite(ex);
                }
            }

        }

        /// <summary>
        ///  Cancel button event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
        }

        /// <summary>
        ///  Print student card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PrintStudentCards(object sender, EventArgs e)
        {
            try
            {
                PrintStudentCard(studentController.GetStudentsDetails(CreateDocument(Session["AddedStudentList"] as List<Student>)));
            }
            catch (Exception ex)
            {
                Messages.ShowWarning("Error in student print card.");
                LogFileWrite(ex);
            }
        }

        /// <summary>
        ///  Username Textbox text change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UserName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                StudentUserNameTextBox.Text = string.Concat(FirstNameTextBox.Text, LastNameTextBox.Text.Substring(0, 1)).ToLower();
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Checks for License Exhaustion
        /// </summary>
        /// <returns></returns>
        private int CheckForLicenseExhaustion()
        {
            if (Null.SetNullInteger(DashboardController.Instance.SubscriptionDetails(new Users() { SubscriptionId = int.Parse(Session["Subscription"].ToString()), UserLoginName = TeacherLoginName }).AvailableLicenses) <= 0)
            {
                string roleId = DashboardController.Instance.UserDetails(new Users() { UserLoginName = TeacherLoginName, SubscriptionId = int.Parse(Session["Subscription"].ToString()) }).UserRole;
                if (roleId.ToUpper() != "SUBS ADMIN" && roleId.ToUpper() != "CEN ADMIN")
                {
                    TeacherTxt.Visible = true;
                    TeacherTxt.Text = "Licences for the subscription are exhausted, Please contact your Administrator";
                }
                else
                {
                    AdminText.Visible = true;
                    AdminText.Text = "Licences for the subscription are exhausted, Please buy/update the subscription. Please <a id='VisitLink' href='/pmecollection' runat='server'>visit</a>";
                    // BuyLink.HRef = "/subscription/pagename=manageusersubscriptions&custsubsk=" + Session["Subscription"].ToString();
                }
                ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile", "<script>SubscriptionExpired()</script>", false);
                return 1;
            }
            return 0;
        }
    }
}