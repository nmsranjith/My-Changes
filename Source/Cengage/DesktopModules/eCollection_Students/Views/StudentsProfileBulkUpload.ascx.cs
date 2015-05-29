using System;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using System.IO;
using System.Data;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using System.Web.UI;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Text;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using System.Configuration;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentsProfileBulkUpload" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To create student profiles through bulk upload
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class StudentsProfileBulkUpload : eCollection_StudentsModuleBase
    {
        #region Page Events
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
                        image_preview.InnerText = GetMessage(Constants.UPLOAD_WAIT);
                        SeeAllLink.NavigateUrl = string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID), "?pagename=", SEEALL);
                        if (Session["Subscription"] != null)
                        {
                            CheckForLicenseExhaustion();
                            Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                            Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));
                    }
                }
                catch (Exception ex) { Messages.ShowWarning(ex.Message); LogFileWrite(ex); }
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

        #endregion

        #region Button Events

        /// <summary>
        /// Download the format Excel file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownLoadExcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=Cengage_Students_Bulk_Upload_File.xls");
                Response.TransmitFile(Server.MapPath("~/DesktopModules/eCollection_Students/Files/Cengage_Students_Bulk_Upload_File.xls"));
                Response.End();        
            }
            catch (Exception ex) { Messages.ShowWarning(ex.Message); LogFileWrite(ex); }
        }

        /// <summary>
        /// Upload the excel file to do bulk upload of student profiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadFileImageButton_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            try
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    if (CheckForLicenseExhaustion() == 1)
                        return;
                    else { }
                    //Get the file details 
                    if (AttachAFile.PostedFile.FileName == string.Empty)
                        AttachAFile.Attributes.Add("value", txtAttachment.Value);
                    fileName = string.Concat(ConfigurationManager.AppSettings["UploadPath"], TeacherLoginName, Path.GetFileName(AttachAFile.PostedFile.FileName));
                    // fileName = string.Concat(Localization.GetString("UploadPath", Localization.GlobalResourceFile), TeacherLoginName, Path.GetFileName(AttachAFile.PostedFile.FileName));

                    string fileExtension = Path.GetExtension(AttachAFile.PostedFile.FileName);

                    //Check for the file format 
                    if (fileExtension == ".xls" | fileExtension == ".xlsx")
                    {
                        //Save the file in server                                                               
                        if (AttachAFile.PostedFile != null && AttachAFile.PostedFile.ContentLength > 0)
                        {
                            AttachAFile.PostedFile.SaveAs(fileName);
                        }

                        //Import Excel into Data table 
                        DataTable studentProfiles = ImportExcelXLS(fileName);

                        // Check for valid excel file and Change Column Names 
                        string[] realColNames = { "First Name", "Last Name", "Date of Birth (dd/mm/yyyy)", "Gender (M/F)", "Year Level (F, 1 - 6)", "Email", "PM Reading Level (1 – 24)", "Reading Recovery (Y/N)", "ESL (Y/N)" };
                        string[] columnNames = { "FirstName", "LastName", "DOB", "Gender", "Grade", "Email", "ReadingLevel", "ReadingRecovery", "ESL" };
                        int errValue = 0;
                      
                        foreach (string col in realColNames)
                        {
                            if (!studentProfiles.Columns.Contains(col))
                            {
                                errValue = 1;
                                break;
                            }
                            studentProfiles.Columns[studentProfiles.Columns[col].Ordinal].ColumnName = columnNames[Array.IndexOf(realColNames, col)];
                        }

                        if (errValue == 0)
                        {

                            //Get all the affected records 
                            IEnumerable<DataRow> query1 = from row in studentProfiles.AsEnumerable()
                                                          where !(string.IsNullOrEmpty(row["FirstName"].ToString()) && string.IsNullOrEmpty(row["LastName"].ToString()) &&
                                                                string.IsNullOrEmpty(row["DOB"].ToString()) &&  string.IsNullOrEmpty(row["ESL"].ToString()) &&
                                                                string.IsNullOrEmpty(row["ReadingRecovery"].ToString()))
                                                          select row;
                            //Check whether excel file is not empty
                            if (query1.Count() > 0)
                            {
                                studentProfiles = query1.CopyToDataTable();


                                //Check records less than or equal to licence count 
                                if (studentProfiles.Rows.Count <= DashboardController.Instance.SubscriptionDetails(new Users() { SubscriptionId = int.Parse(Session["Subscription"].ToString()), UserLoginName = TeacherLoginName }).AvailableLicenses)
                                {
                                    //Remove the unwanted columns in the datatable 
                                    studentProfiles.Columns.RemoveAt(10);
                                    studentProfiles.Columns.RemoveAt(9);

                                    //Add new columns                                 
                                    studentProfiles.Columns.Add(new DataColumn("Password"));
                                    studentProfiles.Columns.Add(new DataColumn("DateofBirth"));
                                    studentProfiles.Columns.Add(new DataColumn("RowId"));                                   

                                    foreach (DataRow row in studentProfiles.Rows)
                                    {
                                        //Mandatory validation - First Name  
                                        if (row["FirstName"].ToString() == string.Empty)
                                        {
                                            errValue = 2;
                                            break;
                                        }
                                        //Mandatory validation - Last Name  
                                        if (row["LastName"].ToString() == string.Empty)
                                        {
                                            errValue = 3;
                                            break;
                                        }
                                        //Mandatory validation - Last Name  
                                        if (row["Grade"].ToString() == string.Empty)
                                        {
                                            errValue = 4;
                                            break;
                                        }
                                        //Validate email id
                                        if (row["Email"].ToString() != string.Empty)
                                            if (!ValidateEmail(row["Email"].ToString()))
                                            {
                                                errValue = 5;
                                                break;
                                            }
                                        //Set Row Id 
                                        row["RowId"] = studentProfiles.Rows.IndexOf(row);
                                        //Generate password 
                                        row["Password"] = encrypt(string.Concat(row["FirstName"].ToString().Substring(0, Math.Min(row["FirstName"].ToString().Length, 4)), new Random().Next(10, 99)));
                                        //Convert date of birth 
                                        row["DateofBirth"] = row["DOB"].ToString() != string.Empty ? DateTime.Parse(row["DOB"].ToString()).ToString("MM/dd/yyyy") : string.Empty;
                                        //Set Reading level
                                        row["ReadingLevel"] = row["ReadingLevel"].ToString() == string.Empty ? "1" : row["ReadingLevel"].ToString();
                                    }
                                    studentProfiles.Columns.RemoveAt(2);

                                    // Check whether any error occured
                                    if (errValue == 0)
                                    {
                                        // if there is no error

                                        // Construct the xml from datatable
                                        StringBuilder str = new StringBuilder();
                                        StringWriter writer = new StringWriter(str);
                                        studentProfiles.TableName = "StudentProfiles";
                                        studentProfiles.WriteXml(writer, true);


                                        // create a student object for upload
                                        Student uploadStudents = new Student()
                                        {
                                            UserCreated = TeacherLoginName,
                                            SubscriptionId = int.Parse(Session["Subscription"].ToString()),
                                            CreatedDate = DateTime.Now,
                                            StudentsDoc = new XmlDocument() { InnerXml = str.ToString() }
                                        };

                                        // call the business method
                                        Session["BlkUploadedStuds"]=studentController.UploadStudentProfiles(uploadStudents);
                                        if (Null.SetNullInteger((Session["BlkUploadedStuds"] as List<Student>).Count) > 0)
                                        {
                                            Session["AddedStudentCount"] = Null.SetNullInteger(Session["AddedStudentCount"]) + Null.SetNullInteger((Session["BlkUploadedStuds"] as List<Student>).Count);
                                            // if upload is success, show the success message
                                            StudentsCount.InnerText = string.Concat(studentProfiles.Rows.Count.ToString(), GetMessage(Constants.UPLOAD_SUCCESS));
                                            TotalAddedStudents.InnerText = string.Concat(GetMessage(Constants.CREATE_SUCCESS2), Null.SetNullInteger(Session["AddedStudentCount"]).ToString());
                                            ScriptManager.RegisterStartupScript(Page, GetType(), "CreateProfilesSuccess", "<script>CreateProfilesSuccess()</script>", false);
                                            (Session["AddedStudentList"] as List<Student>).AddRange(Session["BlkUploadedStuds"] as List<Student>);
                                        }
                                    }
                                    else
                                    {
                                        // Show message according to the type of error
                                        switch (errValue)
                                        {
                                            // if first name field is empty
                                            case 2:
                                                Messages.ShowWarning(GetMessage(Constants.FIRSTNAME_MANDATORY_UPLOAD));
                                                break;
                                            // if last name field is empty
                                            case 3:
                                                Messages.ShowWarning(GetMessage(Constants.LASTNAME_MANDATORY_UPLOAD));
                                                break;
                                            // if grade field is empty
                                            case 4:
                                                Messages.ShowWarning(GetMessage(Constants.GRADE_MANDATORY_UPLOAD));
                                                break;
                                            // if emaild is not valid
                                            case 5:
                                                Messages.ShowWarning(GetMessage(Constants.VALIDATE_EMAIL));
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    Messages.ShowWarning(GetMessage(Constants.VALIDATE_LICENCES));
                                }
                            }
                            else
                            {
                                Messages.ShowWarning(GetMessage(Constants.VALIDATE_EMPTY_FILE));
                                return;
                            }
                        }
                        else
                        {
                            Messages.ShowWarning(GetMessage(Constants.VALIDATE_MISMATCH_FILE));
                        }
                    }
                    else
                    {
                        Messages.ShowWarning(GetMessage(Constants.VALIDATE_MISMATCH_PATH));
                    }

                    studentController.ClearAllCache();

                }
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
                AttachAFile.Attributes.Remove("value");
            }
            finally
            {
               if (fileName!=string.Empty)
                File.Delete(fileName);
                txtAttachment.Value = string.Empty;
                AttachAFile.Attributes.Remove("value");
            }
        }

            /// <summary>
            /// Print student card
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void PrintStudentCards(object sender, EventArgs e)
            {
                try
                {
                    PrintStudentCard(studentController.GetStudentsDetails(CreateDocument(Session["AddedStudentList"] as List<Student>)));
                }
                catch (Exception ex) { Messages.ShowWarning(ex.Message); LogFileWrite(ex); }
            }

            /// <summary>
            /// Cancel button event
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void CancelButton_Click(object sender, EventArgs e)
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
            }

        #endregion

        #region Private Methods

            /// <summary>
            ///  Convert Excel file into Datatable
            /// </summary>
            /// <param name="filename"></param>
            /// <param name="hasHeaders"></param>
            /// <returns></returns>
            public DataTable ImportExcelXLS(string filename)
            {
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=0\"";
                DataTable outputTable = null;
                try
                {
                    using (OleDbConnection conn = new OleDbConnection(strConn))
                    {
                        conn.Open();

                        DataTable schemaTable = conn.GetOleDbSchemaTable(
                            OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                        foreach (DataRow schemaRow in schemaTable.Rows)
                        {
                            string sheet = schemaRow["TABLE_NAME"].ToString();
                            if (sheet.ToLower() == "'student bulk upload$'")
                            {
                                outputTable = new DataTable(sheet);
                                if (!sheet.EndsWith("_"))
                                {
                                    try
                                    {
                                        OleDbCommand cmd = new OleDbCommand("SELECT TOP 2000 * FROM [" + sheet + "] ", conn);
                                        cmd.CommandType = CommandType.Text;

                                        new OleDbDataAdapter(cmd).Fill(outputTable);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Messages.ShowWarning(string.Concat(ex.Message, string.Format(" Sheet:{0} .File:F{1}", sheet, filename)));
                                    }
                                }
                            }
                        }

                    }
                }
                catch (Exception ex) { /*Messages.ShowWarning(ex.Message);*/ LogFileWrite(ex); }
                return outputTable;
            }      

            /// <summary>
            /// Validate Email Address
            /// </summary>
            /// <param name="emailaddress"></param>
            /// <returns></returns>
            private bool ValidateEmail(string emailaddress)
            {
                try
                {
                    return Regex.IsMatch(emailaddress,
                    @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                    RegexOptions.IgnoreCase);
                }
                catch (Exception ex) { Messages.ShowWarning(ex.Message); LogFileWrite(ex); }
                return false;
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
        #endregion
    }
}