using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using DotNetNuke.Modules.eCollection_Students.Components.Model;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="EditStudentProfile" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Screen to edit the student details
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class EditStudentProfile : eCollection_StudentsModuleBase
    {
        /// <summary>
        /// OnInit Method
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_PreRender);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string date = DateofBirthTextBox.Value;
                if (!IsPostBack)
                {
                    FillStudentInfo();
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                GetAllSubscriptions();
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
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


        protected void GetAllSubscriptions()
        {
            try
            {
                CurrentSubsSk.Value = Null.SetNullString(Session["Subscription"]);
                SqlDataReader reader = studentController.GetAllActiveSubcriptions(Null.SetNullString(Session["UserName"]), Null.SetNullInteger(Session["Subscription"]));
                if (reader.HasRows)
                {
                    List<IDCollection> activeSubsList = new List<IDCollection>();
                    while (reader.Read())
                    {
                        activeSubsList.Add(new IDCollection()
                        {
                            Id = Null.SetNullInteger(reader["SubscriptionId"]),
                            Text = Null.SetNullString(reader["SubscriptionName"])
                        });
                    }
                    if (activeSubsList.Count > 1)
                    {
                        MoveSubscriptionsDpn.DataSource = activeSubsList;
                        MoveSubscriptionsDpn.DataBind();
                    }
                    else
                        SwitchSubscriptionLink.Visible = false;
                }                           
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        Student editStudentProfile = new Student();
        Student checkStudentProfile = new Student();

        /// <summary>
        ///  Fill student informations
        /// </summary>
        protected void FillStudentInfo()
        {
            try
            {
                if (Request.QueryString["username"] != null)
                {
                    editStudentProfile = GetValues(studentController.GetProfileDetails((selectedStudent())).Tables[0], editStudentProfile);
                    StudentSk.Value = Null.SetNullString(editStudentProfile.StudentId);
                    NameTextBox.Text = editStudentProfile.FirstName;
                    LastNameTextBox.Text = editStudentProfile.LastName;
                    EmailTextBox.Text = editStudentProfile.Email;
                    StudentUserNameTextBox.Text = editStudentProfile.UserLoginName.Length > 25 ? string.Concat(editStudentProfile.UserLoginName.Substring(0, 23), "..") : editStudentProfile.UserLoginName;
                    StudentUserNameTextBox.ToolTip = editStudentProfile.UserLoginName;
                    PasswordTextBox.Attributes.Add("value", editStudentProfile.Password);
                    GenderDropDown.Value = editStudentProfile.Gender.ToString();
                    if (editStudentProfile.DateofBirth != null)
                        DOBHdFld.Value = editStudentProfile.DateofBirth.Value.ToString("dd/MM/yyyy");
                    GradeDropDown.Value = editStudentProfile.Grade;
                    ESLCheck.Text = editStudentProfile.ESL.Value.ToString();
                    ReadingRecoveryCheck.Text = editStudentProfile.ReadingRecovery.Value.ToString();
                    StudentNameHeader.InnerText = string.Concat("Move ", editStudentProfile.FirstName, " to:");
                    AfterSwitchUrl.InnerText = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
                    StudentUserName.Value = editStudentProfile.UserLoginName;
                }
                else
                {
                    Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
                }
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// Get the student's details
        /// </summary>
        /// <param name="table"></param>
        /// <param name="stu"></param>
        /// <returns></returns>
        public Student GetValues(DataTable table, Student stu)
        {
            try
            {
                foreach (DataRow row in table.Rows)
                {
                    stu.StudentId = int.Parse(row["StudentId"].ToString());
                    stu.FirstName = row["FirstName"].ToString();
                    stu.LastName = row["LastName"].ToString();
                    stu.Email = row["Email"].ToString();
                    stu.UserLoginName = row["UserLoginName"].ToString();
                    stu.Password = decrypt(row["Password"].ToString());
                    if (row["Gender"].ToString() != string.Empty)
                        stu.Gender = char.Parse(row["Gender"].ToString());
                    if (row["DateofBirth"].ToString() != string.Empty)
                        stu.DateofBirth = DateTime.Parse(row["DateofBirth"].ToString());
                    stu.Grade = row["Grade"].ToString();
                    stu.ESL = row["ESL"] == null | row["ESL"].ToString() == string.Empty ? 'N' : char.Parse(row["ESL"].ToString());
                    stu.ReadingRecovery = row["ReadingRecovery"] == null | row["ReadingRecovery"].ToString() == string.Empty ? 'N' : char.Parse(row["ReadingRecovery"].ToString());
                    stu.SubscriptionId = int.Parse(row["SubsSK"].ToString());
                }
                return stu;
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
            return stu;
        }

        /// <summary>
        ///  Update the student details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveStudentProfile_Click(object sender, EventArgs e)
        {
            try
            {
                Messages.ClearMessages();
                if (Session["update"] != null)
                {
                    if (Session["update"].ToString() == ViewState["update"].ToString())
                    {
                        editStudentProfile = selectedStudent();
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        editStudentProfile.FirstName = NameTextBox.Text.Trim();
                        editStudentProfile.LastName = LastNameTextBox.Text.Trim();
                        editStudentProfile.Email = EmailTextBox.Text.Trim();
                        editStudentProfile.Password = encrypt(PasswordTextBox.Text.Trim());
                        editStudentProfile.Gender = char.Parse(GenderDropDown.Value);
                        if (DateofBirthTextBox.Value.Trim() != string.Empty)
                            if (DateTime.ParseExact(DateofBirthTextBox.Value.Trim(), "dd/MM/yyyy", null) >= DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null))
                            {
                                Messages.ShowWarning(GetMessage(Constants.VALIDATE_DOB2));
                                PasswordTextBox.Text = editStudentProfile.Password.Trim();
                                return;
                            }
                            else
                                editStudentProfile.DateofBirth = DateTime.ParseExact(DateofBirthTextBox.Value.Trim(), "dd/MM/yyyy", null);
                        editStudentProfile.UserModified = TeacherLoginName;
                        editStudentProfile.Grade = GradeDropDown.Value;
                        editStudentProfile.ESL = char.Parse(ESLHdn.Value);
                        editStudentProfile.ReadingRecovery = char.Parse(RRHdn.Value);

                    }
                    checkStudentProfile = GetValues(studentController.GetProfileDetails((selectedStudent())).Tables[0], checkStudentProfile);
                    if (checkStudentProfile.FirstName == editStudentProfile.FirstName &&
                        checkStudentProfile.LastName == editStudentProfile.LastName &&
                        checkStudentProfile.Email == editStudentProfile.Email &&
                        checkStudentProfile.Password == decrypt(editStudentProfile.Password) &&
                        checkStudentProfile.Gender == editStudentProfile.Gender &&
                        checkStudentProfile.DateofBirth == editStudentProfile.DateofBirth &&
                        checkStudentProfile.Grade == editStudentProfile.Grade &&
                        checkStudentProfile.ESL == editStudentProfile.ESL &&
                        checkStudentProfile.ReadingRecovery == editStudentProfile.ReadingRecovery)
                    {
                        Messages.ShowSuccess(GetMessage(Constants.EDIT_NO_CHANGE));
                    }
                    else
                    {
                        if (studentController.Update(editStudentProfile) > 0)
                        {
                            editStudentProfile.FullName = string.Concat(editStudentProfile.FirstName, " ", editStudentProfile.LastName);
                            Messages.ShowSuccess(string.Concat(editStudentProfile.FullName.Length > 25 ? string.Concat(editStudentProfile.FullName.Substring(0, 23), "..") : editStudentProfile.FullName, " was successfully edited."));
                            FillStudentInfo();
                        }
                        else
                            Messages.ShowError(GetMessage(Constants.EDIT_FAILED));
                    }
                }
                else
                    Messages.ShowError(GetMessage(Constants.SESSION_EXPIRE));
            }
            catch (Exception ex)
            {
                if (ex.Message == "String was not recognized as a valid DateTime.")
                    Messages.ShowWarning(GetMessage(Constants.VALIDATE_DOB1));
                else
                {
                    Messages.ShowWarning(ex.Message);
                    LogFileWrite(ex);
                }
            }
        }
    }
}