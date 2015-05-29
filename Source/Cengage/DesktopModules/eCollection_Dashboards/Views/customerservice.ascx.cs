using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using DotNetNuke.Common;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Common;

namespace DotNetNuke.Modules.eCollection_Dashboards.Views
{
    public partial class customerservice : eCollection_DashboardsModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
             Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());            
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["update"] = Session["update"];
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(GetTabID("eCollection_Dashboards")));
        }
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
                            catch (Exception ex){LogFileWrite(ex); }
                        }
                    }

                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return outputTable;
        }


        /// <summary>
        /// Upload the excel file to do bulk upload of teacher and admin profiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadBooksButton_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            Messages.ClearMessages();
            try
            {
                //  MailMessage m = new MailMessage();
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    //Get the file details 
                    if (AttachAFile.PostedFile.FileName == string.Empty)
                        AttachAFile.Attributes.Add("value", txtAttachment.Value);
                    //fileName = string.Concat(Localization.GetString("UploadPath", Localization.GlobalResourceFile), TeacherLoginName, Path.GetFileName(AttachAFile.PostedFile.FileName));
                    fileName = string.Concat(ConfigurationManager.AppSettings["UploadPath"], "Customer Service", LoginName, Path.GetFileName(AttachAFile.PostedFile.FileName));
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
                        string[] realColNames = { "Account Id", "First Name", "Last Name","email(email will be the username for the respective user)","Date of Birth","Role(Admin/Teacher)","Password"};
                        string[] columnNames = { "AccountNumber", "FirstName", "LastName", "Email", "DOB", "Role", "Password" };
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
                                //Add new columns                                 
                                teachers.Columns.Add(new DataColumn("RowId"));
                                teachers.Columns.Add(new DataColumn("DateofBirth"));
                                teachers.Columns.Add(new DataColumn("dnnPassword"));
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
                                            errValue = 5;
                                            break;
                                        }
                                        else
                                        {
                                            if (emailCnt == 0)
                                            {
                                                //m.To.Add(row["Email"].ToString());
                                                emailCnt++;
                                            }
                                            //else
                                            //   m.Bcc.Add(row["Email"].ToString());
                                        }                                       
                                    }                                   

                                    //Mandatory validation - Account Number  
                                    if (row["AccountNumber"].ToString() == string.Empty)
                                    {
                                        errValue = 6;
                                        break;
                                    }

                                    //Mandatory validation - Password
                                    if (row["Password"].ToString() == string.Empty)
                                    {
                                        errValue = 7;
                                        break;
                                    }
                                    else
                                    {
                                        row["dnnPassword"] = row["Password"].ToString();
                                        row["Password"]= encrypt(row["Password"].ToString());
                                    }

                                    //Mandatory validation - Role
                                    if (row["Role"].ToString() == string.Empty)
                                    {
                                        errValue = 8;
                                        break;
                                    }

                                    row["DateofBirth"] = row["DOB"].ToString() != string.Empty ? DateTime.Parse(row["DOB"].ToString()).ToString("MM/dd/yyyy") : string.Empty;
                                    
                                    //Set Row Id 
                                    row["RowId"] = teachers.Rows.IndexOf(row);
                                }
                                // Check whether any error occured
                                if (errValue == 0)
                                {
                                    // if there is no error

                                    // Construct the xml from datatable
                                    StringBuilder str = new StringBuilder();
                                    StringWriter writer = new StringWriter(str);
                                    teachers.TableName = "EarlyAccess";
                                    teachers.WriteXml(writer, true);

                                    int result = 0;                             
                                    // call the business method
                                    result = _dashboardController.UploadTeacherProfiles(new XmlDocument() { InnerXml = str.ToString() });
                                    /* }*/
                                    if (result > 0)// Subscriptions.Length)
                                    {
                                        // if upload is success, show the success message
                                        _dashboardController.ClearAllCache();
                                        Messages.ShowSuccess("Teacher profiles uploaded successfully.");

                                        //UserInfo objUser = new UserInfo();

                                        //foreach (DataRow row in teachers.Rows)
                                        //{
                                        //    objUser.PortalID = this.PortalId;
                                        //    objUser.IsSuperUser = false;
                                        //    objUser.FirstName = row["FirstName"].ToString();
                                        //    objUser.LastName = row["LastName"].ToString();
                                        //    objUser.DisplayName = row["FirstName"].ToString() + " " + row["LastName"].ToString();
                                        //    objUser.Email = row["Email"].ToString();
                                        //    objUser.Username = row["Email"].ToString();

                                        //    UserMembership objMembership = new UserMembership();
                                        //    objMembership.Approved = true;
                                        //    objMembership.CreatedDate = DateTime.Now;
                                        //    objMembership.Email = row["Email"].ToString();
                                        //    objMembership.Username = row["Email"].ToString();
                                        //    objMembership.Password = row["dnnPassword"].ToString();

                                        //    //Assign membership object to the UserInfo Object
                                        //    objUser.Membership = objMembership;

                                        //    UserCreateStatus res = UserController.CreateUser(ref objUser);
                                        //    //Check status
                                        //    if (res == UserCreateStatus.Success)
                                        //    {
                                        //        Messages.ShowSuccess("Teacher profiles uploaded successfully.");
                                        //    }   
                                        //}
                                       
                                                                            
                                    }

                                    //m.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmailAddress"], ConfigurationManager.AppSettings["AdminName"]);

                                    //SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["EmailDomainName"]);
                                    //smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["NetworkCredentialUserName"], ConfigurationManager.AppSettings["NetworkCredentialPassword"]);
                                    //m.Subject = ConfigurationManager.AppSettings["InvitationText"];
                                    //m.Body = string.Concat("You have been added as a Teacher to the PM eCollection. Click on this link to complete your profile in the Cengage Learning eCommerce system and fully access the benefits of the PM eCollection. ",
                                    //         ConfigurationManager.AppSettings["eCommSignUp"], encrypt(Session["Subscription"].ToString()));                                                                           
                                    //smtp.Send(m);                                   
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
                                        // if emaild is empty
                                        case 6:
                                            Messages.ShowWarning(GetMessage(Constants.PASSWORD_MANDATORY));
                                            break;
                                        // if Account Number is empty
                                        case 7:
                                            Messages.ShowWarning(GetMessage(Constants.ACCOUNT_NUMBER_MANDATORY));
                                            break;
                                        // if role is empty
                                        case 8:
                                            Messages.ShowWarning(GetMessage(Constants.ACCOUNT_NUMBER_MANDATORY));
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
                LogFileWrite(ex); //Messages.ShowWarning(ex.Message); 
                AttachAFile.Attributes.Remove("value");
            }
            finally
            {
                if (fileName != string.Empty)
                    File.Delete(fileName);
                txtAttachment.Value = string.Empty;
                AttachAFile.Attributes.Remove("value");
            }
        }
        
        protected bool ValidateEmail(string emailaddress)
        {
            try
            {
                return Regex.IsMatch(emailaddress.Trim(), @"^(([^<>()[\]\\.,;:\s@\""]+"
                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                        + @"[a-zA-Z]{2,}))$", RegexOptions.IgnoreCase);
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return false;
        }

        protected string encrypt(string text)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(toEncryptArray, 0, toEncryptArray.Length);
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
            return string.Empty;
        }
    }
}