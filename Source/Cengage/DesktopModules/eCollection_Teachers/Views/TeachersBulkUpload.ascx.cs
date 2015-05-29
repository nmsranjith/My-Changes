using System;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using System.IO;
using System.Data;
using DotNetNuke.Modules.eCollection_Teachers.Components.Model;
using System.Web.UI;
using DotNetNuke.Modules.eCollection_Teachers.Components.Common;
using System.Xml;
using System.Data.OleDb;
using System.Text;
using System.Configuration;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;

namespace DotNetNuke.Modules.eCollection_Teachers.Views
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="TeachersBulkUpload" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To create student profiles through bulk upload
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class TeachersBulkUpload : eCollection_TeachersModuleBase
    {
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                image_preview.InnerText = GetMessage(Constants.UPLOAD_WAIT);
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
        /// Download the format Excel file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownLoadExcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=Cengage_Teacher_Bulk_Upload_file.xlsx");
                Response.TransmitFile(Server.MapPath("~/DesktopModules/eCollection_Teachers/Files/Cengage_Teacher_Bulk_Upload_file.xlsx"));
                Response.End();
            }
            catch (Exception ex) { LogFileWrite(ex); }      
        }

        /// <summary>
        /// Upload the excel file to do bulk upload of student profiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadFileImageButton_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;

            var uri = new Uri(Request.Url.AbsoluteUri);            
            Messages.ClearMessages();
            try
            {
                //  MailMessage m = new MailMessage();
                if (Session["update"].ToString() == ViewState["update"].ToString() && Session["Subscription"] != null)
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    //Get the file details 
                    var UserId = DashboardController.Instance.UserDetails(new Users() { UserLoginName = TeacherLoginName, SubscriptionId = Null.SetNullInteger(Session["Subscription"]) }).UserId;
                    if (AttachAFile.PostedFile.FileName == string.Empty)
                        AttachAFile.Attributes.Add("value", txtAttachment.Value);
                    //fileName = string.Concat(Localization.GetString("UploadPath", Localization.GlobalResourceFile), TeacherLoginName, Path.GetFileName(AttachAFile.PostedFile.FileName));
                    fileName = string.Concat(ConfigurationManager.AppSettings["UploadPath"], TeacherLoginName, Path.GetFileName(AttachAFile.PostedFile.FileName));
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
                        DataTable teachers = ImportExcelXLS(fileName);
                        // Check for valid excel file and Change Column Names 
                        string[] realColNames = { "First Name", "Last Name", "Email Address" };
                        string[] columnNames = { "FirstName", "LastName", "Email" };
                        int errValue = 0;
                        foreach (string col in realColNames)
                        {
                            if (!teachers.Columns.Contains(col))
                            {
                                errValue = 1;
                                break;
                            }
                            teachers.Columns[teachers.Columns[col].Ordinal].ColumnName = columnNames[Array.IndexOf(realColNames, col)];
                        }
                        if (errValue == 0)
                        {
                            //Get all the affected records 
                            IEnumerable<DataRow> query1 = from row in teachers.AsEnumerable()
                                                          where !(string.IsNullOrEmpty(row["FirstName"].ToString()) && string.IsNullOrEmpty(row["LastName"].ToString()) &&
                                                          string.IsNullOrEmpty(row["Email"].ToString()))
                                                          select row;
                            //Check whether excel file is not empty
                            if (query1.Count() > 0)
                            {
                                teachers = query1.CopyToDataTable();
                                //Remove the unwanted columns in the datatable 
                                teachers.Columns.RemoveAt(4);
                                teachers.Columns.RemoveAt(3);
                                //Add new columns                                 
                                teachers.Columns.Add(new DataColumn("RowId"));
                                teachers.Columns.Add(new DataColumn("emailerror"));
                                teachers.Columns.Add(new DataColumn("InviteeUrl"));
                                teachers.Columns.Add(new DataColumn("MailBody"));
                                int emailCnt = 0;
                                foreach (DataRow row in teachers.Rows)
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
                                    //Set Row Id 
                                    row["RowId"] = teachers.Rows.IndexOf(row);
                                    //Validate email id
                                    if (row["Email"].ToString() == string.Empty)
                                    {
                                        errValue = 4;
                                        break;
                                    }
                                    else
                                    {
                                        if (!ValidateEmail(row["Email"].ToString()))
                                        {
                                            //errValue = 5;
                                            emailCnt++;
                                            row["emailerror"] = 'Y';
                                            //break;
                                        }                                        
                                    }
                                    
                                    row["InviteeUrl"] = string.Concat(uri.Scheme, "://", uri.Host, "/primary?inviteesk=", encrypt(row["Email"].ToString().Trim()), "&subsid=", encrypt(Session["Subscription"].ToString()), "&usersk=", encrypt(UserId.ToString()), "&app=ecollection");
                                    row["MailBody"] = teacherController.MailbodyBuilder(row["FirstName"].ToString(), row["InviteeUrl"].ToString());
                                }
                                // Check whether any error occured
                                if (errValue == 0)
                                {
                                    // if there is no error

                                    // Construct the xml from datatable
                                    StringBuilder str = new StringBuilder();
                                    StringWriter writer = new StringWriter(str);
                                    teachers.TableName = "Teachers";
                                    teachers.WriteXml(writer, true);

                                    DataTable result = null;
                              
                                    // create a teacher object for upload
                                    Teacher uploadTeachers = new Teacher()
                                    {
                                        UserCreated = TeacherLoginName,
                                        SubscriptionId = int.Parse(Session["Subscription"].ToString()),
                                        CreatedDate = DateTime.Now,
                                        TeachersDoc = new XmlDocument() { InnerXml = str.ToString() }
                                    };
                                    // call the business method
                                    result = teacherController.UploadTeacherProfiles(uploadTeachers);
                                    
                                    if (result.Rows.Count > 0)// Subscriptions.Length)
                                    {
                                        // if upload is success, show the success message
                                        teacherController.ClearAllCache();
                                        if (Null.SetNullInteger(result.Rows[0][1]) > 0)
                                        {
                                            TeachersCount.InnerText = string.Concat(result.Rows[0][1], GetMessage(Constants.UPLOAD_SUCCESS1));// teachers.Rows.Count.ToString(), GetMessage(Constants.UPLOAD_SUCCESS1));
                                            TotalAddedTeachers.InnerText = string.Concat(GetMessage(Constants.UPLOAD_SUCCESS2), AllTeachers.Count.ToString());
                                            ScriptManager.RegisterStartupScript(Page, GetType(), "CreateProfilesSuccess", "<script>CreateProfilesSuccess()</script>", false);
                                        }
                                        // To notify already invited,Teachers added and invalid email ids
                                        string InfoText = "";
                                        if (Null.SetNullInteger(result.Rows[0][2]) > 0)// && (Null.SetNullInteger(result.Rows[0][1]) + Null.SetNullInteger(result.Rows[0][3]) != Null.SetNullInteger(result.Rows[0][2])))                                            
                                        {
                                            //if (Null.SetNullInteger(result.Rows[0][2]) != Null.SetNullInteger(result.Rows[0][0]))
                                            //{
                                                if (Null.SetNullInteger(result.Rows[0][2]) == teachers.Rows.Count)
                                                    InfoText = string.Concat(GetMessage(Constants.TEACHER_BLK_All_ALREADY_INVITED), (Null.SetNullInteger(result.Rows[0][3]) > 0) || (Null.SetNullInteger(result.Rows[0][4]) > 0) || (Null.SetNullInteger(result.Rows[0][5]) > 0) || (emailCnt > 0) ? " and " : string.Empty);
                                                else
                                                    InfoText = string.Concat(Null.SetNullString(result.Rows[0][2]), ' ', GetMessage(Constants.TEACHER_BLK_ALREADY_INVITED), (Null.SetNullInteger(result.Rows[0][3]) > 0) || (Null.SetNullInteger(result.Rows[0][4]) > 0)||(Null.SetNullInteger(result.Rows[0][5]) > 0) || (emailCnt > 0) ? " and " : string.Empty);
                                            //}
                                        }

                                        if (Null.SetNullInteger(result.Rows[0][4]) > 0)// && (Null.SetNullInteger(result.Rows[0][1]) + Null.SetNullInteger(result.Rows[0][3]) != Null.SetNullInteger(result.Rows[0][2])))                                            
                                        {
                                            //if (Null.SetNullInteger(result.Rows[0][4]) != Null.SetNullInteger(result.Rows[0][0]))
                                            //{
                                                if (Null.SetNullInteger(result.Rows[0][4]) == teachers.Rows.Count)
                                                    InfoText = string.Concat(InfoText, GetMessage(Constants.ALREADY_PART_OF_SUBS2), (Null.SetNullInteger(result.Rows[0][5]) > 0) || (Null.SetNullInteger(result.Rows[0][3]) > 0) || (emailCnt > 0) ? " and " : string.Empty);
                                                else
                                                    InfoText = string.Concat(InfoText, Null.SetNullString(result.Rows[0][4]), ' ', GetMessage(Constants.ALREADY_PART_OF_SUBS3), (Null.SetNullInteger(result.Rows[0][5]) > 0) || (Null.SetNullInteger(result.Rows[0][3]) > 0) || (emailCnt > 0) ? " and " : string.Empty);
                                            //}
                                        }

                                        if (Null.SetNullInteger(result.Rows[0][5]) > 0)// && (Null.SetNullInteger(result.Rows[0][1]) + Null.SetNullInteger(result.Rows[0][3]) != Null.SetNullInteger(result.Rows[0][2])))                                            
                                        {
                                            //if (Null.SetNullInteger(result.Rows[0][5]) != Null.SetNullInteger(result.Rows[0][0]))
                                            //{
                                                if (Null.SetNullInteger(result.Rows[0][5]) == teachers.Rows.Count)
                                                    InfoText = string.Concat(InfoText, GetMessage(Constants.ALREADY_PART_OF_ANOTHER_SCHOOL2), (Null.SetNullInteger(result.Rows[0][3]) > 0) || (emailCnt > 0) ? " and " : string.Empty);
                                                else
                                                    InfoText = string.Concat(InfoText, Null.SetNullString(result.Rows[0][5]), ' ', GetMessage(Constants.ALREADY_PART_OF_ANOTHER_SCHOOL3), (Null.SetNullInteger(result.Rows[0][3]) > 0) || (emailCnt > 0) ? " and " : string.Empty);
                                            //}
                                        }

                                       
                                        if (Null.SetNullInteger(result.Rows[0][3]) > 0)
                                            InfoText = string.Concat(InfoText, Null.SetNullString(result.Rows[0][3]), ' ', GetMessage(Constants.TEACHER_BLK_ADDED), emailCnt > 0 ? " and " : string.Empty);
                                        if (emailCnt > 0)
                                            InfoText = string.Concat(InfoText, emailCnt, ' ', GetMessage(Constants.EMAIL_INVALID));
                                       
                                        Messages1.ShowInfo(InfoText);
                                        
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
                                        // if emaild is empty
                                        case 4:
                                            Messages.ShowWarning(GetMessage(Constants.EMAIL_MANDATORY_UPLOAD));
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
                                Messages.ShowWarning(GetMessage(Constants.VALIDATE_EMPTY_FILE));
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
                }
            }
            catch (Exception ex)
            {
                Messages.ShowWarning(ex.Message); LogFileWrite(ex); AttachAFile.Attributes.Remove("value");
            }
            finally
            {
                if (fileName != string.Empty)
                    File.Delete(fileName);
                txtAttachment.Value = string.Empty;
                AttachAFile.Attributes.Remove("value");
            }
        }

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
        /// <param name="FileName"></param>
        /// <returns></returns>
        public DataTable ImportExcelXLS(string FileName)
        {
            DataTable outputTable = null;
            try
            {
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=0\"";               
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    DataTable schemaTable = conn.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    foreach (DataRow schemaRow in schemaTable.Rows)
                    {
                        string sheet = schemaRow["TABLE_NAME"].ToString();
                        outputTable = new DataTable(sheet);
                        if (!sheet.EndsWith("_"))
                        {
                            try
                            {
                                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "] ", conn);
                                cmd.CommandType = CommandType.Text;

                                new OleDbDataAdapter(cmd).Fill(outputTable);
                                break;
                            }
                            catch (Exception ex)
                            {
                                LogFileWrite(ex);
                                Messages.ShowWarning(string.Concat(ex.Message, string.Format(" Sheet:{0} .File:F{1}", sheet, FileName)));
                            }
                        }
                    }

                }
            }
            catch (Exception ex) { LogFileWrite(ex); }      
            return outputTable;
        }
    }
}