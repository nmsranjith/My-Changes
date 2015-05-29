/*
' Copyright (c) 2012 DotNetNuke Corporation
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System.Collections;
using DotNetNuke.Entities.Modules;
using System;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Students.Components.Model;
using DotNetNuke.Modules.eCollection_Students.Components.Controllers;
using System.Data;
using System.Xml;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Configuration;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using DotNetNuke.Common;
using System.IO;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Common.Utilities;
using System.Web;
namespace DotNetNuke.Modules.eCollection_Students
{

    /// <summary>
    /// This base class can be used to define custom properties for multiple controls. 
    /// An example module, DNNSimpleArticle (http://dnnsimplearticle.codeplex.com) uses this for an ArticleId
    /// 
    /// Because the class inherits from PortalModuleBase, properties like ModuleId, TabId, UserId, and others, 
    /// are accessible to your module's controls (that inherity from eCollection_StudentsModuleBase
    /// 
    /// </summary>

    public class eCollection_StudentsModuleBase : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        #region Module Names
        protected const string GroupsModule = "eCollection_Groups";
        protected const string SessionsModule = "eCollection_Sessions";
        protected const string StudentsModule = "eCollection_Students";
        protected const string BooksModule = "eCollection_Books";
        protected const string TeachersModule = "eCollection_Teachers";
        protected const string DashboardsModule = "eCollection_Dashboards";
        protected const string ActiveModule = "eCollection Students";
        public const string CommonModule = "eCollection Common";
       
        #endregion       

        #region Private Members
        private List<Student> _allStudents;
        private List<Student> _allOtherStudents;
        private Student _selected = new Student();
        private DataTable _subsList = new DataTable();
        private int _classId;
        #endregion

        #region Page Redirection Strings
        protected const string DASHBOARD = "dashboard";
        protected const string STUDENTPROFILE = "profile";
        protected const string CREATESTUDENTPROFILE = "createprofile";
        protected const string EDITSTUDENTPROFILE = "editprofile";
        protected const string STUDENTBULKUPLOAD = "bulkupload";
        protected const string MYRECORDINGS = "recordings";
        protected const string MYWORDS = "words";
        protected const string GROUPPROFILE = "groupprofile";
        protected const string ADDTOGROUP = "addtogroup";
        protected const string CREATEREADINGSESSION = "createsession";
        protected const string SEEALL = "all";
        protected const string CREATENEW = "createnew";
        protected const string BULKUPLOAD = "bulkupload";
        protected const string UNALLOCATED = "unallocated";
        protected const string STARTCREATION = "createprofile";

        #endregion

        #region Protected Members
        protected StudentsController studentController = StudentsController.Instance;
        protected int TeacherId
        {
            get
            {
                object value = Session["TeacherId"] = 12;
                return value == null ? 0 : (int)value;
            }
            private set
            {
                Session["TeacherId"] = value;
            }
        }       
        protected List<Student> AllStudents
        {
            get
            {
                if (_allStudents == null)
                {
                    _allStudents = AllOtherStudents.FindAll(delegate(Student stu) { return stu.AddedBy == TeacherId; });
                }

                return _allStudents;
            }
            set { _allStudents = value; }
        }        
        protected List<Student> AllOtherStudents
        {
            get
            {
                try
                {
                    _allOtherStudents = studentController.GetAll(new Student() { TeacherLoginName = TeacherLoginName, SubscriptionId = int.Parse(Session["Subscription"].ToString()) });
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return _allOtherStudents;
            }
            set { _allOtherStudents = value; }
        }
        public string TeacherLoginName
        {
            get
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));
                }
                return Session["UserName"].ToString();
            }
            set
            {
                Session["UserName"] = value;
            }
        }
        public int ClassID
        {
            get
            {
                if (Session["ClassId"] != null)
                    _classId = int.Parse(Session["ClassId"].ToString());
                return _classId;
            }
            set
            {
                Session["ClassId"] = value;
            }
        }
        protected DataTable SubsList
        {
            get
            {
                _subsList = DashboardController.Instance.GetSubscriptionsList(new Users() { UserLoginName = TeacherLoginName, Active = 'Y' });
                return _subsList;
            }
            set
            {
                _subsList = value;
            }        
        }

        public static string ProfileUrl
        {
            get
            {
                return Globals.NavigateURL(new eCollection_StudentsModuleBase().GetTabID(StudentsModule)) + "?pagename=" + STUDENTPROFILE + "&username=";
            }
            set
            {
                ProfileUrl = value;
            }
        }

        protected List<DotNetNuke.Modules.eCollection_Students.Components.Model.Messages> StudentsMessages
        {
            get
            {
                return studentController.GetErrorMessagesByModuleName(ActiveModule);
            }
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// Gets the details of a selected student
        /// </summary>
        /// <returns></returns>
        protected Student selectedStudent()
        {
            try
            {
                if (Session["listSelector"] == null)
                    Session["listSelector"] = "Others";
                switch (Session["listSelector"].ToString())
                {
                    case "General":
                        {
                            _selected = AllStudents.Find(delegate(Student stu) { return stu.UserLoginName == Request.QueryString["username"].ToString(); });// stu.StudentId == int.Parse(Session["selStudID"].ToString()); });
                            break;
                        }
                    case "Others":
                        {
                            _selected = AllOtherStudents.Find(delegate(Student stu) { return stu.UserLoginName == Request.QueryString["username"].ToString(); });
                            break;
                        }

                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return _selected;            
        }
        /// <summary>
        ///  Gets the tab id for givven module name
        /// </summary>
        /// <param name="modulename"></param>
        /// <returns></returns>
        protected int GetTabID(string modulename)
        {
            int modID = 0;
            DotNetNuke.Entities.Modules.ModuleController mc = new ModuleController();
            ArrayList existMods = mc.GetModulesByDefinition(this.PortalId, modulename);
            foreach (DotNetNuke.Entities.Modules.ModuleInfo mi in existMods)
            {
                if (!mi.IsDeleted)
                {
                    modID = mi.TabID;
                }
            }
            int iProfileTabId = Convert.ToInt32(modID);
            return iProfileTabId;
        }
        /// <summary>
        /// Creates XML document
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        protected Student CreateDocument(List<Student> students)
        {
            XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
             new XElement("Students",
             from student in students
             select new XElement("Student",
             new XElement("Id", student.StudentId))));

            return new Student { StudentsDoc = new XmlDocument() { InnerXml = xmlDocument.ToString() } };
        }

        Rectangle page;
        /// <summary>
        /// Prepares PDF for student print card
        /// </summary>
        /// <param name="printtable"></param>
        protected void PrintStudentCard(DataTable printtable)
        {
            try
            {
                string attachment = "inline; filename=StudentCard.pdf";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/pdf";
                using (Document doc = new Document(PageSize.A4))
                {
                    PdfPTable masterTable = new PdfPTable(4);
                    masterTable.SetWidths(new float[] { 70f, 3f, 70f, 3f });
                    page = doc.PageSize;
                    masterTable.TotalWidth = 450f;
                    masterTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    masterTable.LockedWidth = true;

                    PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();

                    PdfPTable head = new PdfPTable(1);
                    head.TotalWidth = page.Width;
                    Phrase phrase = new Phrase(
                      DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + " GMT", new Font(Font.FontFamily.COURIER, 8)
                    );
                    PdfPCell c = new PdfPCell(phrase);
                    c.Border = Rectangle.NO_BORDER;
                    c.VerticalAlignment = Element.ALIGN_TOP;
                    c.HorizontalAlignment = Element.ALIGN_CENTER;
                    head.AddCell(c);
                    iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(
                      Request.MapPath("~/Portals/0/images/PM.png")
                    );
                    gif.Alignment = iTextSharp.text.Image.LEFT_ALIGN;
                    gif.ScaleToFit(100f, 50f);

                    for (int i = 0; i < printtable.Rows.Count; i++)
                    {
                        PdfPTable printcardTable = new PdfPTable(1);
                        printcardTable.SetWidths(new float[] { 6f });
                        printcardTable.TotalWidth = 200f;
                        printcardTable.HorizontalAlignment = Element.ALIGN_MIDDLE;
                        printcardTable.LockedWidth = true;
                        PdfPTable table = new PdfPTable(3);
                        table.SetWidths(new float[] { 5f, 6f, 2.5f });

                        table.TotalWidth = 200f;
                        table.HorizontalAlignment = Element.ALIGN_LEFT;
                        table.LockedWidth = true;

                        PdfPCell imgcell = new PdfPCell(gif, true);
                        imgcell.HorizontalAlignment = Element.ALIGN_LEFT;
                        imgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        imgcell.Border = 0;
                        imgcell.FixedHeight = 30f;
                        imgcell.BorderWidthTop = 0f;
                        imgcell.BorderWidthBottom = 0f;
                        imgcell.BorderWidthLeft = 0f;
                        imgcell.BorderWidthRight = 0f;

                        table.AddCell(imgcell);
                        Font f = new Font();
                        f.Size = 5f;

                        PdfPCell cell2 = new PdfPCell(new Phrase("This card shows your                        PM eCollection username and      password. Keep it in a safe place.", f));
                        cell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell2.HorizontalAlignment = Element.ALIGN_MIDDLE;
                        cell2.BorderColor = new BaseColor(255, 0, 0);
                        cell2.BorderWidthRight = 0.8f;
                        cell2.BorderWidthTop = 0f;
                        cell2.BorderWidthBottom = 0f;
                        cell2.BorderWidthLeft = 0f;
                        table.AddCell(cell2);

                        PdfPCell cell3 = new PdfPCell(new Phrase("Available on iTunes App Store", f));
                        cell3.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell3.PaddingLeft = 8f;
                        cell3.Border = Rectangle.NO_BORDER;
                        table.AddCell(cell3);

                        PdfPCell empatcell = new PdfPCell();
                        empatcell.Border = Rectangle.NO_BORDER;
                        PdfPCell horiemptyCell = new PdfPCell();
                        horiemptyCell.FixedHeight = 10f;
                        horiemptyCell.Border = Rectangle.NO_BORDER;

                        PdfPCell header = new PdfPCell(table);
                        header.PaddingTop = 5f;
                        header.Border = 0;
                        Font f2 = new Font();
                        f2.Size = 10f;

                        printcardTable.AddCell(header);
                        printcardTable.AddCell(empatcell);

                        PdfPCell innertextcell = new PdfPCell(new Phrase(string.Concat(printtable.Rows[i]["FirstName"].ToString(), " ", printtable.Rows[i]["LastName"].ToString()), f2));
                        innertextcell.VerticalAlignment = Rectangle.ALIGN_LEFT;
                        innertextcell.HorizontalAlignment = Rectangle.ALIGN_MIDDLE;
                        innertextcell.PaddingTop = 8f;
                        innertextcell.PaddingLeft = -0.5f;
                        PdfPCell second = new PdfPCell(innertextcell);
                        second.BorderColor = new BaseColor(255, 0, 0);
                        second.BorderWidthTop = 0.8f;
                        second.BorderWidthBottom = 0.8f;
                        second.BorderWidthLeft = 0f;
                        second.BorderWidthRight = 0f;
                        //second.FixedHeight = 25f;
                        Font f3 = new Font();
                        f3.Size = 7f;
                        printcardTable.AddCell(second);
                        Phrase date = new Phrase();
                        PdfPCell thirdCell = new PdfPCell(date);
                        thirdCell.Border = Rectangle.NO_BORDER;
                        thirdCell.PaddingTop = 8f;
                        thirdCell.PaddingLeft = 5f;

                        Font f4 = new Font();
                        f4.Size = 8f;
                        Font red = new Font();
                        red.Size = 8f;
                        red.Color = BaseColor.RED;
                        Font black = new Font();
                        black.Size = 6.5f;
                        black.Color = BaseColor.BLACK;

                        Phrase ph1 = new Phrase();

                        PdfPCell fourthCell = new PdfPCell(ph1);
                        fourthCell.Border = Rectangle.NO_BORDER;
                        fourthCell.PaddingTop = 8f;
                        fourthCell.PaddingLeft = 5f;

                        Phrase ph2 = new Phrase();

                        PdfPCell fifthCell = new PdfPCell(ph2);
                        fifthCell.Border = Rectangle.NO_BORDER;
                        fifthCell.PaddingTop = 8f;
                        fifthCell.PaddingLeft = 5f;

                        Phrase ph3 = new Phrase();

                        ph1.Add(new Chunk("Username: ", red));
                        ph1.Add(new Chunk(printtable.Rows[i]["UserLoginName"].ToString(), f4));

                        ph2.Add(new Chunk("Password: ", red));
                        ph2.Add(new Chunk(decrypt(printtable.Rows[i]["Password"].ToString()), f4));

                        ph3.Add(new Chunk("Email: ", red));
                        ph3.Add(new Chunk(printtable.Rows[i]["Email"].ToString(), f4));

                        date.Add(new Chunk("Date set: ", black));
                        date.Add(new Chunk(printtable.Rows[i]["CreatedDate"].ToString(), black));


                        PdfPCell sixthCell = new PdfPCell(ph3);
                        sixthCell.Border = Rectangle.NO_BORDER;
                        sixthCell.PaddingTop = 8f;
                        sixthCell.PaddingLeft = 5f;
                        sixthCell.PaddingBottom = 5f;
                        printcardTable.AddCell(thirdCell);
                        printcardTable.AddCell(fourthCell);
                        printcardTable.AddCell(fifthCell);
                        printcardTable.AddCell(sixthCell);


                        PdfPCell column1 = new PdfPCell(printcardTable);
                        if (i % 2 == 0 & i != 0)
                        {
                            masterTable.AddCell(horiemptyCell);
                            masterTable.AddCell(empatcell);
                            masterTable.AddCell(horiemptyCell);
                            masterTable.AddCell(empatcell);
                        }
                        masterTable.AddCell(column1);
                        masterTable.AddCell(empatcell);

                        if (printtable.Rows.Count % 2 != 0 & i == printtable.Rows.Count - 1)
                        {
                            masterTable.AddCell(empatcell);
                            masterTable.AddCell(empatcell);
                        }
                    }

                    doc.Add(masterTable);

                    doc.Close();

                    Response.Write(doc);

                    Response.End();
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
        /// <summary>
        /// Password decrypting method
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string decrypt(string text)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(text);
                return UTF8Encoding.UTF8.GetString(toEncryptArray);
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return string.Empty;
        }
        /// <summary>
        /// Password encrypting method
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string encrypt(string text)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(toEncryptArray, 0, toEncryptArray.Length);
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return string.Empty;
        }      
        /// <summary>
        ///  Gets the message for the given message code
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        protected string GetMessage(string messageCode)
        {
            try
            {
                DotNetNuke.Modules.eCollection_Students.Components.Model.Messages error_Message = StudentsMessages.Find(x => x.MessageCode.Equals(messageCode));
                return error_Message.MessageDesc;
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return string.Empty;
        }

        /// <summary>
        /// Get the recording details
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected Student GetRecordingsDetails(DataRow dr)
        {
            Student student = new Student();
            try
            {
                student.PageName = string.Concat("Content Page ", dr["PageName"].ToString().Split('-')[1]);
                student.RecPath = dr["RecPath"].ToString();
                student.BookOpenedDate = dr["OpenedDate"].ToString();
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return student;
        }

        /// <summary>
        /// Get all the reading history details of the recordings
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected Student GetRHRecordingsDetails(DataRow dr)
        {
            Student student = new Student();
            try
            {
                student.BookID = dr["BookID"].ToString();
                student.BookOpenedDate = dr["OpenedDate"].ToString();
                student.PageName = string.Concat("Content Page ", dr["PageName"].ToString().Split('-')[1]);
                student.RecPath = dr["FilePath"].ToString().ToLower();
                student.BookOpenedTime = dr["TimeSpan"].ToString();
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return student;
        }

        /// <summary>
        ///  Save recorded files as mp4 audio
        /// </summary>
        /// <param name="datasource"></param>
        /// <returns></returns>
        protected List<StudentRecordingFiles> SaveRecordFile(List<StudentRecordingFiles> datasource)
        {
            try
            {
                datasource.ForEach(x =>
                {
                    if (x.RecPath != string.Empty)
                    {
                        string driveName = ConfigurationManager.AppSettings.Get("RecordingPath");
                        string text = System.IO.File.ReadAllText(Path.Combine(driveName, x.RecPath));

                        byte[] bytes = System.Convert.FromBase64String(text);
                        int indexoftxt = x.RecPath.LastIndexOf(".txt");
                        x.RecPath = string.Concat(x.RecPath.Substring(0, indexoftxt), x.RecPath.Substring(indexoftxt, 4).Replace(".txt", ".m4a"));
                        if (!(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecPath))))
                            File.WriteAllBytes(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecPath), bytes);
                    }
                });
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return datasource;
        }

        /// <summary>
        ///  Writes the exception into the log file
        /// </summary>
        /// <param name="e"></param>
        public static void LogFileWrite(Exception e)
        {
            if (e.Source != "mscorlib" && e.Message.ToLower() != "thread was being aborted." && e.Message.ToLower() != "the document has no pages.")
            {            
                FileStream fileStream = null;
                StreamWriter streamWriter = null;
                try
                {
                    string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "Exceptions - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");

                    if (logFilePath.Equals("")) return;
                    #region Create the Log file directory if it does not exists
                    DirectoryInfo logDirInfo = null;
                    FileInfo logFileInfo = new FileInfo(logFilePath);
                    logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                    if (!logDirInfo.Exists) logDirInfo.Create();
                    #endregion Create the Log file directory if it does not exists

                    if (!logFileInfo.Exists)
                    {
                        fileStream = logFileInfo.Create();
                    }
                    else
                    {
                        fileStream = new FileStream(logFilePath, FileMode.Append);
                    }
                    streamWriter = new StreamWriter(fileStream);
                    var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    streamWriter.WriteLine(string.Concat("\r\n-----------------------------------", DateTime.Now, "---------------------------------------------",
                       "\r\n Website: ", uri.Host,
                       "\r\n Logged-in UserName: ", HttpContext.Current.Session["UserName"].ToString(),
                        "\r\n Type: ", e.GetType(),
                        "\r\n Source: ", e.Source, "\r\n Exception: ", e.Message, "\r\n Description: ", e.StackTrace, "\r\n-----------------------------------------------------------------------------------------------------------------"));
                    string body = string.Concat("<html",
                     "<body>",
                         "<h4>-----------------------------------", DateTime.Now, "--------------------------------------------- </h4>",
                         "<h4> Website: <b>", uri.Host, "</b></h4>",
                         "<h4>Type: ", e.GetType(), "</h4><h4>Source: ", e.Source, "</h4><h4> Exception: ",
                           e.Message, "</h4><h4> Description: ", e.StackTrace, "</h4><h4>-----------------------------------------------------------------------------------------------------------------</h4>",
                     "</body>",
                 "</html>");                                  
                 //StudentsController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
                }
                catch(Exception ex){}
                finally
                {
                    if (streamWriter != null) streamWriter.Close();
                    if (fileStream != null) fileStream.Close();
                }
            }
        }
        /// <summary>
        ///  Writes values to log file for developer verification
        /// </summary>
        /// <param name="e"></param>
        public static void LogValues(string value)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "Value Check - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                streamWriter.WriteLine(string.Concat("\r\n-----------------------------------", DateTime.Now, "---------------------------------------------",
                   "\r\n Website: ", uri.Host,
                    "\r\n Given Text : ", value, "\r\n-----------------------------------------------------------------------------------------------------------------"));

                // TeacherController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }
        #endregion


    }
}
