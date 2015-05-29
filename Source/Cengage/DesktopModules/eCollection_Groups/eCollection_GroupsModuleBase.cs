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
using System.Linq;
using System.Collections.Generic;
using DotNetNuke.Modules.eCollection_Groups.Components;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DotNetNuke.Modules.eCollection_Groups.Groups;
using System.Web;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;
using System.Text;
using System.Xml;
using System.Data;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
using DotNetNuke.Modules.eCollection_Dashboards.Components.Model;
using System.IO;
using System.Configuration;
using DotNetNuke.Common.Utilities;
namespace DotNetNuke.Modules.eCollection_Groups
{

    /// <summary>
    /// This base class can be used to define custom properties for multiple controls. 
    /// An example module, DNNSimpleArticle (http://dnnsimplearticle.codeplex.com) uses this for an ArticleId
    /// 
    /// Because the class inherits from PortalModuleBase, properties like ModuleId, TabId, UserId, and others, 
    /// are accessible to your module's controls (that inherity from eCollection_GroupsModuleBase
    /// 
    /// </summary>

    public class eCollection_GroupsModuleBase : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        protected const string GroupsModule = "eCollection Groups";
        protected const string SessionsModule = "eCollection_Sessions";
        protected const string SessionParameter = "createsession";
        protected const string Sessionprofile = "sessionprofile";
        protected const string StudentsModule = "eCollection_Students";
        protected const string Studentprofile = "profile";
        protected const string DashboardsModule = "eCollection_Dashboards";
        protected const string ContentManagement = "eCollection eBook Content Management";

        protected const string GROUPPROFILE = "groupprofile";
        protected const string CREATEGROUP = "creategroup";
        protected const string EDITGROUP = "editgroup";
        protected const string ADDSTUDENTTOCREATEGROUP = "addstudenttocreategroup";
        protected const string ADDSTUDENTTOEDITGROUP = "addstudenttoeditgroup";
        protected const string ADDTEACHERTOCREATEGROUP = "addteachertocreategroup";
        protected const string ADDTEACHERTOEDITGROUP = "addteachertoeditgroup";
        protected const string WORDS = "words";
        protected const string RECORDINGS = "recordings";

        protected const string CENGAGESTAGING = "cenagagestaging";
        public DataTable _subsList;
        private List<Components.Groups> _groups;
        private List<Components.Groups> _classes;
        private List<Students> _students;
        private List<IDCollection> _teachers;
        private List<IDCollection> _allOtherTeachers;
        private List<Components.Modal.Students> _allOtherStudents;
        private List<Staging> _getAllBooks;
        GroupController _groupController = GroupController.Instance;



        public string LoginName { get { return Session["UserName"].ToString(); } } 
        public char GroupType = 'N';
        public char ClassType = 'C';
        
        public bool ctype = true;


        //temp
       
        protected Components.Groups groupLoginDetails
        {
            get
            {
                try
                {
                    return _groupController.GetLoginDetails(LoginName, int.Parse(Session["Subscription"].ToString()));
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }
            
        }       

        public int userID 
        { 
            get
            {
                if (Session["userID"] == null)
                {
                   Components.Groups grouploginuserID= groupLoginDetails;
                   if (grouploginuserID.CustomerSubId.HasValue)
                    {
                        Session["userID"] = grouploginuserID.CustomerSubId.Value;
                        return grouploginuserID.CustomerSubId.Value;
                    }
                    else 
                        return 0;
                
                }
                else
                {
                    return int.Parse(Session["userID"].ToString());
                }
            }
            
        }
        public string LoginUserName
        {
            get
            {
                if (Session["GrpLoginUserName"] == null)
                {
                    Components.Groups grouploginname = groupLoginDetails;
                    if (grouploginname.UserLoginName != string.Empty)
                    {
                        Session["GrpLoginUserName"] = grouploginname.UserLoginName.ToLower();
                        return grouploginname.UserLoginName;
                    }
                    else
                        return string.Empty;

                }
                else
                {
                    return Session["GrpLoginUserName"].ToString();
                }
            }

        }
        protected DataTable SubsList
        {
            get
            {
                try
                {
                    _subsList = DashboardController.Instance.GetSubscriptionsList(new Users() { UserLoginName = LoginName, Active = 'Y' });
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return _subsList;
            }
            set
            {
                _subsList = value;
            }
        }
        
        protected int GetTabID(string ModuleName)
        {
            int modID = 0;
            try
            {
                DotNetNuke.Entities.Modules.ModuleController mc = new ModuleController();
                ArrayList existMods = mc.GetModulesByDefinition(this.PortalId, ModuleName);
                foreach (DotNetNuke.Entities.Modules.ModuleInfo mi in existMods)
                {
                    if (!mi.IsDeleted)
                    {
                        modID = mi.TabID;
                    }
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
            int iProfileTabId = Convert.ToInt32(modID);
            return iProfileTabId;
        }
        public bool AllOther
        {

            get
            {
                if (Session["AllOther"] == null)
                {
                    return false;
                }
                else
                {
                    return bool.Parse(Session["AllOther"].ToString());
                }
            }
            set
            {
                Session["AllOther"] = value;
            }
        }
        public int ConstRowCount
        {
            get
            {
                return int.Parse(Session["RowCount"].ToString());
            }
            set
            {
                Session["RowCount"] = value;
            }
        }
        public List<Components.Groups> GroupLists
        {
            get
            {
                if (_groups == null)
                {
                    try
                    {
                        _groups = _groupController.GetGroupList(GroupType, LoginName, AllOther, int.Parse(Session["Subscription"].ToString()));
                    }
                    catch (Exception ex) { LogFileWrite(ex); }
                }

                return _groups;
            }
            set { _groups = value; }
        }
        public List<Components.Groups> ClassList
        {
            get
            {
                if (_classes == null)
                {
                    try
                    {
                        _classes = _groupController.GetClassList(ClassType, LoginName, AllOther, int.Parse(Session["Subscription"].ToString()));
                    }
                    catch (Exception ex) { LogFileWrite(ex); }
                }

                return _classes;
            }
            set { _classes = value; }
        }
        public int? EditGroupId
        {
            get
            {
                if (Session["EditGroupID"] == null)
                {
                    return -1;
                }
                else
                {
                    return Session["EditGroupID"] as int?;
                }
            }
            set
            {
                Session["EditGroupID"] = value;
            }
        }
        public int? EditClassId
        {
            get
            {
                if (Session["EditClassID"] == null)
                {
                    return -1;
                }
                else
                {
                    return Session["EditClassID"] as int?;
                }
            }
            set
            {
                Session["EditClassID"] = value;
            }
        }

        public string[] MergeClassId
        {
            get
            {
                if (Session["MergeClassId"] == null)
                {
                    return null;
                }
                else
                {
                    return Session["MergeClassId"] as string[];
                }
            }
            set
            {
                Session["MergeClassId"] = value;
            }
        }
        public string[] SelectedID
        {
            get
            {
                if (Session["SelectedID"] == null)
                {
                    return null;
                }
                else
                {
                    return Session["SelectedID"] as string[];
                }
            }
            set
            {
                Session["SelectedID"] = value;
            }
        }
        public List<Students> StudentList
        {
            get
            {
                if (_students == null)
                {
                    if (Session["studentList"] != null)
                    {
                        _students = Session["studentList"] as List<Students>;
                    }

                }
                return _students;
            }
            set
            {
                _students = value;
                Session["studentList"] = _students;
            }
        }

        public List<IDCollection> TeachersList
        {
            get
            {
                if (_teachers == null)
                {
                    if (Session["teacherList"] != null)
                    {
                        _teachers = Session["teacherList"] as List<IDCollection>;
                    }

                }
                return _teachers;
            }
            set
            {
                _teachers = value;
                Session["teacherList"] = _teachers;
            }
        }
        public string GroupName
        {
            get
            {
                if (Session["GroupName"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Session["GroupName"] as string;
                }
            }
            set
            {
                Session["GroupName"] = value;
            }
        }
        public string SelectedSubscription
        {
            get
            {
                if (Session["SelectedSubscription"] == null)
                {
                    return string.Empty;
                }
                else
                {
                    return Session["SelectedSubscription"] as string;
                }
            }
            set
            {
                Session["SelectedSubscription"] = value;
            }
        }
        public bool Type
        {
            get
            {
                if (Session["ClassType"] == null)
                {
                    return ctype;
                }
                else
                {
                    return bool.Parse(Session["ClassType"].ToString());
                }
            }
            set
            {
                ctype = value;
                Session["ClassType"] = ctype;
            }
        }
        protected List<Components.Modal.Students> AllOtherStudents
        {
            get
            {
                try
                {
                    _allOtherStudents = _groupController.GetStudentsBySubcription(int.Parse(Session["Subscription"].ToString()), LoginName);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return _allOtherStudents;
            }
            set { _allOtherStudents = value; }
        }
        protected List<IDCollection> AllOtherTeachers
        {
            get
            {
                try
                {
                    _allOtherTeachers = _groupController.GetTeachersbySubscription(int.Parse(Session["Subscription"].ToString()), LoginName);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return _allOtherTeachers;
            }
            set { _allOtherTeachers = value; }
        }
        public int RoleId
        {
            get
            {
                object value = Session["RoleId"];
                return value == null ? 0 : (int)value;
            }
            protected set { Session["RoldId"] = value; }
        }
        protected string decrypt(string text)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(text);
            return UTF8Encoding.UTF8.GetString(toEncryptArray);
        }
        protected List<string> AddedBook
        {
            get
            {
                if (Session["AddedBook"] == null)
                {
                    return null;
                }
                else
                {
                    return Session["AddedBook"] as List<string>;
                }
            }
            set
            {
                Session["AddedBook"] = value;
            }
        }
        protected List<Staging> GetAllBooks
        {
            get
            {
                try
                {
                    _getAllBooks = _groupController.GetBooksDetails(LoginName);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return _getAllBooks;
            }
            set { _getAllBooks = value; }
        }

        protected List<Staging> GetStagingDetails
        {
            get
            {
                if (Session["GetStagingDetails"] == null)
                {
                    return null;
                }
                else
                {
                    return Session["GetStagingDetails"] as List<Staging>;
                }
            }
            set
            {
                Session["GetStagingDetails"] = value;
            }
        }

        protected List<DotNetNuke.Modules.eCollection_Groups.Components.Modal.Messages> GroupMessages
        {
            get
            {
                try
                {
                    return _groupController.GetErrorMessagesByModuleName(GroupsModule);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }
        }
        protected List<DotNetNuke.Modules.eCollection_Groups.Components.Modal.Messages> ContentMessages
        {
            get
            {
                try
                {
                    return _groupController.GetErrorMessagesByModuleName(ContentManagement);
                }
                catch (Exception ex) { LogFileWrite(ex); }
                return null;
            }
        }
        protected string GetContentMessage(string Message_Code)
        {
            try
            {
                DotNetNuke.Modules.eCollection_Groups.Components.Modal.Messages Error_Message = ContentMessages.Find(x => x.MessageCode.Equals(Message_Code));

                return Error_Message.MessageDesc;
            }
            catch (Exception ex) { LogFileWrite(ex); }

            return string.Empty;
        }
        protected string GetMessage(string Message_Code)
        {
            try
            {
                DotNetNuke.Modules.eCollection_Groups.Components.Modal.Messages Error_Message = GroupMessages.Find(x => x.MessageCode.Equals(Message_Code));

                return Error_Message.MessageDesc;
            }
            catch (Exception ex) { LogFileWrite(ex); }

            return string.Empty;
        }
        protected void PrintStudentCard(DataTable groupStudent)
        {
            try
            {
                string attachment = "inline; filename=StudentCard.pdf";

                Response.ClearContent();

                Response.AddHeader("content-disposition", attachment);

                Response.ContentType = "application/pdf";
                /*
                 * step 1
                 *
                 * __ONLY__ if using version >= 5.0.6
                 * see commented section directly below
                 *
                 */
                Rectangle page;
                using (Document doc = new Document(PageSize.A4))
                {
                    PdfPTable masterTable = new PdfPTable(4);
                    masterTable.SetWidths(new float[] { 70f, 3f, 70f, 3f });
                    page = doc.PageSize;
                    masterTable.TotalWidth = 450f;
                    masterTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    masterTable.LockedWidth = true;

                    // step 2
                    PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);

                    // step 3
                    doc.Open();

                    // step 4

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
                    int i = 0;
                    PdfPCell horiemptyCell = new PdfPCell();
                    PdfPCell empatcell = new PdfPCell();
                    for (int j = 0; j < groupStudent.Rows.Count; j++)
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
                        //cell2.Border = 0;
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

                        empatcell.Border = Rectangle.NO_BORDER;
                        horiemptyCell.FixedHeight = 10f;
                        horiemptyCell.Border = Rectangle.NO_BORDER;

                        PdfPCell header = new PdfPCell(table);
                        header.PaddingTop = 5f;
                        header.Border = 0;
                        Font f2 = new Font();
                        f2.Size = 10f;

                        printcardTable.AddCell(header);
                        printcardTable.AddCell(empatcell);
                        // Phrase UserName = new Phrase();
                        PdfPCell innertextcell = new PdfPCell(new Phrase(string.Concat(groupStudent.Rows[j]["FirstName"].ToString(), " ", groupStudent.Rows[j]["LastName"].ToString()), f2));
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

                        Font f5 = new Font();
                        f5.Size = 10f;
                        f5.Color = BaseColor.BLACK;
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
                        Font black = new Font();
                        black.Size = 6.5f;
                        black.Color = BaseColor.BLACK;
                        //Chunk c6 = new Chunk(groups.EmailAddress, f4);
                        //ph3.Add(c6);
                        ph1.Add(new Chunk("Username: ", red));
                        ph1.Add(new Chunk(groupStudent.Rows[j]["UserLoginName"].ToString(), f4));

                        ph2.Add(new Chunk("Password: ", red));
                        ph2.Add(new Chunk(decrypt(groupStudent.Rows[j]["Password"].ToString()), f4));

                        ph3.Add(new Chunk("Email: ", red));
                        ph3.Add(new Chunk(groupStudent.Rows[j]["EmailAddress"].ToString(), f4));

                        date.Add(new Chunk("Date set: ", black));
                        date.Add(new Chunk(DateTime.Parse(groupStudent.Rows[j]["DateCreated"].ToString()).ToString("dd/MM/yyyy"), black));

                        //Chunk c7 = new Chunk("Date Set ", f3);
                        //date.Add(c7);

                        //Chunk c1 = new Chunk("Username: ", red);
                        //ph1.Add(c1);

                        //Chunk c3 = new Chunk("Password: ", red);
                        //ph2.Add(c3);

                        //Chunk c5 = new Chunk("Email: ", red);
                        //ph3.Add(c5);

                        PdfPCell sixthCell = new PdfPCell(ph3);
                        sixthCell.Border = Rectangle.NO_BORDER;
                        sixthCell.PaddingTop = 8f;
                        sixthCell.PaddingLeft = 5f;
                        sixthCell.PaddingBottom = 5f;
                        printcardTable.AddCell(thirdCell);
                        printcardTable.AddCell(fourthCell);
                        printcardTable.AddCell(fifthCell);
                        printcardTable.AddCell(sixthCell);
                        //date.Add(new Chunk(groups.DateCreated.ToString("dd/MM/yyyy"), f3));
                        //Chunk c11 = new Chunk(groups.StudentNames, f5);
                        //Chunk c2 = new Chunk(groups.UserLoginName, f4);
                        //ph1.Add(c2);
                        //UserName.Add(c11);
                        //Chunk c4 = new Chunk(decrypt(groups.PassWord), f4);
                        //ph2.Add(c4);


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
                        i++;

                    }
                    if (i % 2 == 1)
                    {
                        masterTable.AddCell(horiemptyCell);
                        masterTable.AddCell(empatcell);
                        masterTable.AddCell(horiemptyCell);
                        masterTable.AddCell(empatcell);
                    }
                    doc.Add(masterTable);
                    /* step 5  */
                    if (masterTable.Rows.Count > 0)
                    {
                        doc.Close();
                        Response.Write(doc);
                    }

                    Response.End();
                }
            }
            catch (Exception ex)
            {               
                LogFileWrite(ex);
            }   
        }
        public static void LogFileWrite(Exception e)
        {
            if (e.Source != "mscorlib" && e.Message.ToLower()!="thread was being aborted." && e.Message.ToLower()!="the document has no pages.")
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
                         "<h4>Type: ", e.GetType(), "</h4><br /><h4>Source: ", e.Source, "</h4><br /><h4> Exception: ",
                           e.Message, "</h4><br /><h4> Description: ", e.StackTrace, "</h4><br /><h4>-----------------------------------------------------------------------------------------------------------------</h4><br />",
                     "</body>",
                 "</html>"); 
                  //GroupController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
                }
                finally
                {
                    if (streamWriter != null) streamWriter.Close();
                    if (fileStream != null) fileStream.Close();
                }
            }
        }       

    }
}
