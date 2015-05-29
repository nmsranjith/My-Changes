using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Modules.eCollection_Students.Components.Common;
using DotNetNuke.Modules.eCollection_Students.Components.Model;

namespace DotNetNuke.Modules.eCollection_Students.Views
{
    public partial class UnallocatedStudents : eCollection_StudentsModuleBase
    {
        int exhaust = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["AddedStudentCount"] == null)
                    Session["AddedStudentCount"] = 0;
                if (Session["AddedStudentList"] == null)
                    Session["AddedStudentList"] = new List<Student>();
                SeeAllLink.HRef = string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID), "?pagename=", SEEALL);
                if (Session["Subscription"] != null)
                {
                    if (Null.SetNullInteger(DashboardController.Instance.SubscriptionDetails(new Users() { SubscriptionId = int.Parse(Session["Subscription"].ToString()), UserLoginName = TeacherLoginName }).AvailableLicenses) <= 0)
                    {
                        //  LogValues("ZERO");
                        exhaust = 1;
                        string roleId = DashboardController.Instance.UserDetails(new Users() { UserLoginName = TeacherLoginName, SubscriptionId = int.Parse(Session["Subscription"].ToString()) }).UserRole;
                        if (roleId.ToUpper() != "SUBS ADMIN" && roleId.ToUpper() != "CEN ADMIN")
                            TeacherTxt.Visible = true;
                        else
                            AdminText.Visible = true;

                        ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile", "<script>createProfile()</script>", false);
                    }
                    else { }
                    
                    //  LogValues("NON ZERO"+Null.SetNullInteger(DashboardController.Instance.SubscriptionDetails(new Users() { SubscriptionId = int.Parse(Session["Subscription"].ToString()), UserLoginName = TeacherLoginName }).AvailableLicenses.ToString()));
                    if(!IsPostBack)
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                }
                else
                    Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));

                AfterSwitchUrl.InnerText = Globals.NavigateURL(PortalSettings.ActiveTab.TabID);
            }
            catch (Exception ex)
            {
                Messages.ShowWarning("Error in student creation.");
                LogFileWrite(ex);
            }

            try
            {
                FillUnAllocatedList();
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        protected void FillUnAllocatedList()
        {
            try
            {
                SqlDataReader reader = null;
                int itemsCount = 0, pageNumber = Request.QueryString["p"] != null ? Null.SetNullInteger(Request.QueryString["p"]) : 1, numberOfResults = int.Parse(ConfigurationManager.AppSettings["NO_OF_UNALLOCATED_STUDENTS"]), st = 0, end = 0;
                st = pageNumber == 1 ? 1 : (pageNumber * numberOfResults) - (numberOfResults - 1);
                end = st + numberOfResults - 1;

                reader = studentController.GetAllUnallocatedStudents(new Student() { TeacherLoginName = Null.SetNullString(Session["UserName"]), SubscriptionId = Null.SetNullInteger(Session["Subscription"]), SearchText = string.IsNullOrEmpty(Request.QueryString["unq"]) ? string.Empty : Request.QueryString["unq"], Start = st, End = end });
                List<UnallocatedStudent> unallocatedStudents = new List<UnallocatedStudent>();
                while(reader.Read())
                {
                    unallocatedStudents.Add(new UnallocatedStudent() {
                        CUST_SUBS_ALLOC_SK = Null.SetNullInteger(reader["CUST_SUBS_ALLOC_SK"]),
                        USER_SK = Null.SetNullInteger(reader["USER_SK"]),
                        USER_LOGIN_NAME = Null.SetNullString(reader["USER_LOGIN_NAME"]),
                        EMAIL = Null.SetNullString(reader["EMAIL"]),
                        ALLOC_STATUS = Null.SetNullString(reader["ALLOC_STATUS"])
                    });
                }
                reader.NextResult();
                while (reader.Read())
                    itemsCount = Null.SetNullInteger(reader["TotalItems"]);
                UnAllocatedStudentsRptr.DataSource = unallocatedStudents;
                UnAllocatedStudentsRptr.DataBind();               

                if (itemsCount <= 0)
                    NoSearchResult.Visible = true;
                else if (itemsCount > numberOfResults)
                {
                    int totalpage = 0;
                    int startCount = 0, totalcount = 0;
                    if (pageNumber != 0)
                    {
                        totalpage = Null.SetNullInteger(Math.Ceiling(Convert.ToDouble(Convert.ToDecimal(itemsCount) / numberOfResults)));// itemsCount % sParams.NumberOfResults == 0 ? itemsCount / sParams.NumberOfResults : itemsCount / sParams.NumberOfResults + 1;
                        if (totalpage > pageNumber + 5)
                        {
                            if (pageNumber - 6 >= 0)
                                startCount = pageNumber - 6;
                            else
                                startCount = 0;
                        }
                        else
                        {
                            if (totalpage > 10 && totalpage - pageNumber <= 6)
                                startCount = totalpage - 10;
                            else if (totalpage > 10)
                                startCount = pageNumber - 10;
                            else
                                startCount = 0;
                        }
                    }
                    else
                    {
                        startCount = 0;
                    }
                    LogValues(string.Concat("totalpage=", totalpage, "itemsCount=", itemsCount, "  numberOfResults=", numberOfResults, "  startCount=", startCount));
                    if (totalpage > 1)
                    {
                        SetPagingDefaults(itemsCount, numberOfResults, startCount);
                        UnallocatedPagerDiv.Visible = true;
                    }
                    else
                        UnallocatedPagerDiv.Visible = false;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// product result set paging default function
        /// </summary>
        /// <param name="TotalNoOfRecords"></param>
        /// <param name="NoOfRecordsPerPage"></param>
        /// <param name="FirstValue"></param>
        private void SetPagingDefaults(int TotalNoOfRecords, int NoOfRecordsPerPage, int FirstValue)
        {
            try
            {
                UnallocatedPager.CreatePagingControl(TotalNoOfRecords, FirstValue);
                //HEProductsPager.PageButtonStyle(FirstValue);
                if (TotalNoOfRecords > NoOfRecordsPerPage)
                {
                    UnallocatedPager.DisplayPropertyForPage("block");
                }
                else
                {
                    UnallocatedPager.DisplayPropertyForPage("none");
                }
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["update"] = Session["update"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddStudentProfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["update"] != null)
                {
                    if (Session["update"].ToString() == ViewState["update"].ToString() && exhaust == 0)
                    {
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
                            (Session["AddedStudentList"] as List<Student>).Add(AllOtherStudents.Find(stu => stu.UserLoginName == createStudent.UserLoginName));
                        }
                        else
                        {
                            PasswordTextBox.Text = createStudent.Password;
                            ScriptManager.RegisterStartupScript(Page, GetType(), "UserNameExists", "<script>UserNameExists()</script>", false);
                        }
                    }
                    else
                        Response.Redirect(string.Concat(Globals.NavigateURL(PortalSettings.ActiveTab.TabID), "?pagename=", STARTCREATION));
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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.ActiveTab.TabID));
        }

        /// <summary>
        /// 
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
        /// 
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
                if (Session["update"].ToString() == ViewState["update"].ToString() && exhaust == 0)
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
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
                        string[] realColNames = { "First Name", "Last Name","Password", "Date of Birth (dd/mm/yyyy)", "Gender (M/F)", "Year Level (F, 1 - 6)", "Email", "PM Reading Level (1 – 24)", "Reading Recovery (Y/N)", "ESL (Y/N)" };
                        string[] columnNames = { "FirstName", "LastName","Password", "DOB", "Gender", "Grade", "Email", "ReadingLevel", "ReadingRecovery", "ESL" };
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
                                                          where !(string.IsNullOrEmpty(row["FirstName"].ToString()) && string.IsNullOrEmpty(row["LastName"].ToString())
                                                                && string.IsNullOrEmpty(row["Password"].ToString()) &&
                                                                string.IsNullOrEmpty(row["DOB"].ToString()) && string.IsNullOrEmpty(row["ESL"].ToString()) &&
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
                                    studentProfiles.Columns.RemoveAt(11);
                                    studentProfiles.Columns.RemoveAt(10);

                                    //Add new columns                                 
                                    //studentProfiles.Columns.Add(new DataColumn("Password"));
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
                                        //Mandatory validation - Grade
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
                                        //Mandatory validation - Password
                                        if (row["Password"].ToString() == string.Empty)
                                        {
                                            errValue = 6;
                                            break;
                                        }
                                        else
                                        {
                                            row["Password"] = encrypt(row["Password"].ToString());
                                        }
                                        //Set Row Id 
                                        row["RowId"] = studentProfiles.Rows.IndexOf(row);
                                       /* //Generate password 
                                        row["Password"] = encrypt(string.Concat(row["FirstName"].ToString().Substring(0, Math.Min(row["FirstName"].ToString().Length, 4)), new Random().Next(10, 99)));
                                       */
                                        //Convert date of birth 
                                        row["DateofBirth"] = row["DOB"].ToString() != string.Empty ? DateTime.Parse(row["DOB"].ToString()).ToString("MM/dd/yyyy") : string.Empty;
                                        //Set Reading level
                                        row["ReadingLevel"] = row["ReadingLevel"].ToString() == string.Empty ? "1" : row["ReadingLevel"].ToString();
                                    }
                                    studentProfiles.Columns.RemoveAt(3);

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
                                        Session["BlkUploadedStuds"] = studentController.UploadStudentProfiles(uploadStudents);
                                        if (Null.SetNullInteger((Session["BlkUploadedStuds"] as List<Student>).Count) > 0)
                                        {
                                            Session["AddedStudentCount"] = Null.SetNullInteger(Session["AddedStudentCount"]) + Null.SetNullInteger((Session["BlkUploadedStuds"] as List<Student>).Count);
                                            // if upload is success, show the success message
                                           AddedName.InnerText = string.Concat(studentProfiles.Rows.Count.ToString(), GetMessage(Constants.UPLOAD_SUCCESS));
                                           StudentsCount.InnerText = string.Concat(GetMessage(Constants.CREATE_SUCCESS2), Null.SetNullInteger(Session["AddedStudentCount"]).ToString());
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
                                            case 6:
                                                Messages.ShowWarning(GetMessage(Constants.PASSWORD_MANDATORY_UPLOAD));
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
                if (fileName != string.Empty)
                    File.Delete(fileName);
                txtAttachment.Value = string.Empty;
                AttachAFile.Attributes.Remove("value");
            }
        }

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="hasHeaders"></param>
        /// <returns></returns>
        public DataTable ImportExcelXLS(string FileName)
        {
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=0\"";
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
                                    Messages.ShowWarning(string.Concat(ex.Message, string.Format(" Sheet:{0} .File:F{1}", sheet, FileName)));
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex) { /*Messages.ShowWarning(ex.Message);*/ LogFileWrite(ex); }
            return outputTable;
        }

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

        #endregion
    }
}