using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;
using ICSharpCode.SharpZipLib.Zip;
using System.Configuration;
using DotNetNuke.Modules.eCollection_Groups.Components;
using log4net;
using System.Xml.Linq;
using System.Xml;
using SevenZip;
using DotNetNuke.Common;
using System.Text;
using System.Drawing;
using DotNetNuke.Common.Utilities;
using System.Data;
using System.Reflection;
using DotNetNuke.Instrumentation;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="CengageStaging" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //   To manage the functionality of upload eBooks, Take live and Staging eBooks
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public partial class CengageStaging : eCollection_GroupsModuleBase
    {
        #region Private Members
        private SortDirection DateSortDirection
        {
            get
            {
                if (Session["DateSortDirection"] == null)
                    Session["DateSortDirection"] = SortDirection.Ascending;
                return (SortDirection)Session["DateSortDirection"];
            }
            set { Session["DateSortDirection"] = value; }
        }
        private string driveName = ConfigurationManager.AppSettings.Get("eCollectionPath");
        private string destinationPath_physical = ConfigurationManager.AppSettings.Get("StagingPath");
        private string publishingPath_physical = ConfigurationManager.AppSettings.Get("PublishingPath");
        private string publishingPath_tempPhysical = ConfigurationManager.AppSettings.Get("PublishingTempPath");
        private string LogPath_physical = string.Empty;
        //IsPageRefresh
        private bool IsPageRefresh;
        #endregion

        #region Events
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            UploadBooksButton.Click += new EventHandler(UploadBooksButton_Click); // added by darga
            LogPath_physical = Path.Combine(driveName, "PMeBookContentManagement.log");
            CancelButton.NavigateUrl = Globals.NavigateURL(GetTabID("eCollection_Dashboards"));
            //browser refresh fires the last event (for ex button click) again 
            //the following code prevents this action.
            if (!IsPostBack)
            {
                try
                {
                    DataCache.RemoveCache(string.Format("GetBooksDetails{0}", LoginName));
                    GetStagingDetails = null;
                    GetAllBooks.ForEach(x => { x.Checked = false; });
                    Session["DateSortDirection"] = null;
                    FillStagingList(GetAllBooks);
                    AddedBook = null;
                    IsbnIdHidden.Value = string.Empty;
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ViewState["postids"] = System.Guid.NewGuid().ToString();
                    Session["postid"] = ViewState["postids"].ToString();
                }
                catch (Exception ex) { LogFileWrite(ex); }
            }
            else
            {
                //Set the Guid to avoid Post back button click event
                if (ViewState["postids"].ToString() != Session["postid"].ToString())
                {
                    IsPageRefresh = true;
                }
                Session["postid"] = System.Guid.NewGuid().ToString();
                ViewState["postids"] = Session["postid"].ToString();
            }
            Messages_warning.Style.Add("display", "none");
            Messages.ClearMessages();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_PreRender runs when the control is before the page gets rendered
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["update"] = Session["update"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadBooksButton_Click(object sender, EventArgs e)
        {
            try
            {
                DnnLog.Error("UploadBooksButton_Click");
                if (Session["update"].ToString() == ViewState["update"].ToString())                
                {
                    string fromXmlOperation = Session["update"].ToString(); // added by darga
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    GroupController groupcontroller = GroupController.Instance;
                    string filePath = string.Concat(driveName, LoginName);
                    int flag = 1;
                    if (GetStagingDetails == null)
                    {
                        //GetFilePath
                        if (AttachAFile.HasFile)
                        {
                            AttachAFile.PostedFile.SaveAs(filePath);
                            if (txtAttachment.Value.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[txtAttachment.Value.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length - 1].ToString() == "zip")
                            {
                                List<Staging> isbnCollection = GetISBNDetails(filePath);
                                //isbnCollection.ForEach(x => { x.ISBN = long.Parse(x.ISBN.ToString().Split('_')[0]); });
                                if (isbnCollection.Count == 0)
                                {
                                    File.Delete(filePath);
                                    txtAttachment.Value = string.Empty;
                                    Messages.ShowWarning(GetContentMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_OTHERFILEFORMAT));
                                    return;
                                }
                                XDocument employeeXML = GenerateListToXML(isbnCollection, LoginName);
                                XmlDocument stagingXml = new XmlDocument();
                                stagingXml.InnerXml = employeeXML.ToString();

                                int results = groupcontroller.CheckISBN(stagingXml);
                                //if ((results == 1 && ValidateeBook(filePath, stagingXml, groupcontroller)))
                                if (results == 1)
                                {
                                    DnnLog.Error("ISBN EXISTS");
                                    flag = 0;
                                    GetStagingDetails = isbnCollection;
                                }
                                else
                                {
                                    //File.Delete(filePath);
                                    //txtAttachment.Value = string.Empty;
                                    //Messages.ShowWarning(GetContentMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_FILEFORMATVALID));
                                    //return;
                                    // added by darga//
                                    DnnLog.Error("CODE START");
                                    GetXMLOperation(filePath, fromXmlOperation);
                                    DnnLog.Error("CODE END");
                                    return;
                                    // added by darga//
                                }
                            }
                            else
                            {
                                File.Delete(filePath);
                                txtAttachment.Value = string.Empty;
                                Messages.ShowWarning(GetContentMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_ZIPFILEVALID));
                                return;
                            }
                        }
                        else
                        {
                            txtAttachment.Value = string.Empty;
                            Messages.ShowWarning(GetContentMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_SELECTZIPFILE));
                            return;
                        }
                    }
                    else
                    {
                        if (IsbnIdHidden.Value == "true")
                        {
                            IsbnIdHidden.Value = string.Empty;
                        }
                        else
                        {
                            GetStagingDetails = null;
                            IsbnIdHidden.Value = string.Empty;
                            txtAttachment.Value = string.Empty;
                            return;
                        }
                    }
                    IsbnIdHidden.Value = string.Empty;
                    XDocument stagingDetails = GenerateListToXML(GetStagingDetails, LoginName);
                    XmlDocument stagingDetailsXml = new XmlDocument();
                    stagingDetailsXml.InnerXml = stagingDetails.ToString();
                    int result = groupcontroller.AddIsbnDetails(stagingDetailsXml, flag, "STAGE");
                    if (result > 0)
                    {
                        //Unzip the file
                        Unzip(filePath, destinationPath_physical);

                        List<Staging> logList = GetAllBooks;
                        List<string> existed = GetFileNames(destinationPath_physical);
                        foreach (string isbn in existed)
                        {
                            if (!logList.Exists(u => u.ISBN.Equals(long.Parse(isbn))))
                            {
                                File.Delete(Path.Combine(destinationPath_physical, isbn + ".epub"));
                            }
                        }
                        logList = logList.Where(u => GetStagingDetails.Select(x => x.ISBN).Contains(u.ISBN) && u.Server.Equals("STAGE")).ToList();
                        CreateLog(logList);
                        GetStagingDetails = null;
                        FillStagingList(GetAllBooks);
                        File.Delete(filePath);
                        Messages.ShowSuccess(GetContentMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_UPLOADSUCCESS));
                        txtAttachment.Value = string.Empty;
                    }
                    else
                    {
                        IsbnIdHidden.Value = "Upload";
                        AlertmessageLabel.Text = GetContentMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_FILEEXISTINSTAGE);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallSetPopupFlag", "SetPopUpFlag()", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TakeLiveButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<Staging> PublishList = new List<Staging>();
                int flag = 1;
                if (!Directory.Exists(publishingPath_physical))
                {
                    Directory.CreateDirectory(publishingPath_physical);
                }
                if (AddedBook == null)
                {
                    List<string> existedBookInStaging = GetFileNames(destinationPath_physical);

                    List<string> addedBook = IsbnIdHidden.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToList<string>();
                    if ((!existedBookInStaging.Exists(u => addedBook.Contains(u.ToString()))) && existedBookInStaging.Count != 0)
                    {
                        addedBook = existedBookInStaging.Where(u => addedBook.Contains(u.ToString())).ToList<string>();
                    }
                     List<string> existedBookInPublishing = GetFileNames(publishingPath_physical);
                    AddedBook = addedBook;
                    if (existedBookInPublishing.Exists(u => addedBook.Contains(u.ToString())))
                    {
                        flag = 0;
                    }
                    else
                    {
                        IsbnIdHidden.Value = string.Empty;
                    }
                }
                else
                {
                    if (IsbnIdHidden.Value == "true")
                    {
                        IsbnIdHidden.Value = string.Empty;
                    }
                    else
                    {
                        AddedBook = null;
                        IsbnIdHidden.Value = string.Empty;
                        GetAllBooks.ForEach(x => { x.Checked = false; });
                        FillStagingList(GetAllBooks);
                        return;
                    }
                }
                if (Directory.Exists(destinationPath_physical))
                {
                    foreach (string book in AddedBook)
                    {
                        string strNewFile = System.IO.Path.Combine(destinationPath_physical, book + ".epub");
                        if (File.Exists(strNewFile))
                        {
                            Staging stage = new Staging();
                            stage.ISBN = long.Parse(Path.GetFileNameWithoutExtension(strNewFile));
                            stage.EpubFileName = Path.GetFileName(strNewFile);
                            stage.Server = "PRODUCTION";
                            stage.Active = 'Y';
                            PublishList.Add(stage);
                        }
                    }
                }
                GroupController groupcontroller = GroupController.Instance;
                XDocument employeeXML = GenerateListToXML(PublishList,LoginName);
                XmlDocument stagingXml = new XmlDocument();
                stagingXml.InnerXml = employeeXML.ToString();
                if (PublishList.Count != 0)
                {
                    int results = groupcontroller.AddIsbnDetails(stagingXml, flag, "PRODUCTION");                   
                    if (results > 0)
                    {                       
                        Messages.ShowSuccess(GetContentMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_STAGESUCCESS));
                        PublishList = StagingToPublish(publishingPath_physical, destinationPath_physical, true, AddedBook);
                        List<Staging> logList = RollBack();
                        logList = logList.Where(u => PublishList.Any(u2 => u2.ISBN == u.ISBN) && u.Server.Equals("PRODUCTION")).ToList();
                        CreateLog(logList);
                        AddedBook = null;
                    }
                    else
                    {
                        IsbnIdHidden.Value = string.Empty;
                        AlertmessageLabel.Text = GetContentMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_FILEEXISTINPUBLISH);
                        ScriptManager.RegisterStartupScript(StagingUpdatePanel, StagingUpdatePanel.GetType(), "CallSetPopupFlag", "SetPopUpFlag()", true);
                        return;
                    }
                }
                GetAllBooks.ForEach(x => { x.Checked = false; });
                FillStagingList(GetAllBooks);
                SelectedBook.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                //this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NewBooksSortButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (NewBooksSortButton.CommandName == "Ascending")
                {
                    DateSortDirection = SortDirection.Descending;
                    NewBooksSortButton.CommandName = "Descending";
                }
                else
                {
                    DateSortDirection = SortDirection.Ascending;
                    NewBooksSortButton.CommandName = "Ascending";
                }
                Messages.ClearMessages();
                List<Staging> stagingCollection = GetAllBooks;
                stagingCollection = Sort(stagingCollection);
                if (ViewLiveAllCheckbox.Checked)
                {
                    stagingCollection = stagingCollection.Where(u => u.Server.Equals("PUBLISHED")).ToList();
                }
                stagingCollection.Where(u => u.Server.Equals("PUBLISHED")).ToList().ForEach(x => { x.Checked = false; });
                FillStagingList(stagingCollection);
                BuildSelectedItems(stagingCollection);
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ViewLiveAllCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                List<Staging> stagingCollection = GetAllBooks;
                stagingCollection = Sort(stagingCollection);
                Messages.ClearMessages();
                if (ViewLiveAllCheckbox.Checked)
                {
                    stagingCollection.Where(u => u.Server.Equals("PRODUCTION")).ToList().ForEach(x => { x.Server = "PUBLISHED"; });
                    stagingCollection.Where(u => u.Server.Equals("PUBLISHED")).ToList().ForEach(x => { x.Checked = false; });
                    List<string> existedFileInPublish = GetFileNamesInPublish(publishingPath_physical);
                    List<Staging> PublishCollection = stagingCollection.Where(u => (existedFileInPublish.Contains(u.ISBN.ToString()))).ToList();
                    StagingRepeater.DataSource = stagingCollection.Where(u => u.Server.Equals("PUBLISHED")).ToList();
                    StagingRepeater.DataBind();
                    SelectedBook.InnerHtml = string.Empty;
                }
                else
                {
                    stagingCollection.Where(u => u.Server.Equals("PUBLISHED")).ToList().ForEach(x => { x.Checked = false; });
                    FillStagingList(stagingCollection);
                    BuildSelectedItems(stagingCollection);
                }

            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                GroupController.Instance.GetSearchBooksDetails(LoginName, SearchTextBox.Value.Replace(", ", ",").Trim() != string.Empty ? SearchTextBox.Value.Replace(", ", ",").Trim() : "%");
                IsbnIdHidden.Value = string.Empty;
                GetAllBooks.ForEach(x => { x.Checked = false; });
                FillStagingList(GetAllBooks);
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteFromStagingButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> deletedBook = IsbnIdHidden.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToList<string>();
                List<string> existedFileNames = GetFileNames(destinationPath_physical);
                List<Staging> deleteBooksInStage = new List<Staging>();
                foreach (string isbn in deletedBook)
                {
                    Staging stage = new Staging();
                    if (existedFileNames.Exists(u => u.ToString().Equals(isbn.ToString())))
                    {
                        stage.ISBN = long.Parse(isbn);
                        stage.Active = 'N';
                        stage.Server = "STAGE";
                        stage.DateCreated = DateTime.Now;
                        stage.UserCreated = LoginName;
                        deleteBooksInStage.Add(stage);
                    }
                }

                GroupController groupcontroller = GroupController.Instance;
                XDocument employeeXML = GenerateListToXML(deleteBooksInStage,LoginName);
                XmlDocument stagingXml = new XmlDocument();
                stagingXml.InnerXml = employeeXML.ToString();
                if (deleteBooksInStage.Count != 0)
                {
                    int results = groupcontroller.DeleteBooksInStaging(stagingXml);
                    if (results > 0)
                    {
                        Messages.ShowSuccess(GetContentMessage(DotNetNuke.Modules.eCollection_Groups.Components.Common.Constants.MS_DELETESUCCESS));
                        IsbnIdHidden.Value = string.Empty;
                    }
                }
                GetAllBooks.ForEach(x => { x.Checked = false; });
                FillStagingList(GetAllBooks);
                SelectedBook.InnerHtml = string.Empty;
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DownloadLogfile_Click(object sender, EventArgs e)
        {
            Messages.ClearMessages();
            Response.ContentType = "application/text";
            Response.AddHeader("content-disposition", "attachment;filename=PMeBookContentManagement.log");
            Response.WriteFile(LogPath_physical);
            Response.End();
        }
       
        #endregion

        #region MemberFunction
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Staging> RollBack()
        {
            try
            {
                List<Staging> logList = GetAllBooks;
                List<string> existed = GetFileNames(publishingPath_physical);                
                return logList;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildCollection"></param>
        private void BuildSelectedItems(List<Staging> buildCollection)
        {
            StringBuilder output = new StringBuilder();
            if (buildCollection != null && buildCollection.Count != 0)
            {
                IsbnIdHidden.Value = string.Empty;
                foreach (Staging stage in buildCollection)
                {
                    if (stage.Server == string.Empty && stage.Checked)
                    {
                        output.AppendFormat("<li class=\"SelectedBook\"><span class=\"titleIsbnspan\">{0}</span><span style=\'display:none\'>{1}</span><a class=\"removeanchor\" onclick='javascript:Remove(this)'>x</a></li>", "[" + stage.Title + "]" + " " + "[" + stage.ISBN + "]", stage.ISBN);
                        IsbnIdHidden.Value += stage.ISBN + ",";
                    }
                }
                if (IsbnIdHidden.Value.Trim().Length > 0)
                {
                    SelectedBook.InnerHtml = output.ToString();
                }
                else
                {
                    SelectedBook.InnerHtml = string.Empty;
                }
            }
        }

        /// <summary>
        /// This method will validate the eBook details
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="stagingXml"></param>
        /// <param name="groupcontroller"></param>
        /// <returns></returns>
        private bool ValidateeBook(string filePath, XmlDocument stagingXml, GroupController groupcontroller)
        {
            try
            {                
                List<string> isbncoll = GetISBNDetails(filePath).Select(u => u.ISBN.ToString().Trim()).ToList<string>();

                List<Staging> existed = groupcontroller.GetIsbnDetails(stagingXml);
                if (!existed.Exists(u => isbncoll.Contains(u.ISBN.ToString())))
                {
                    return false;
                }
                else
                {
                    return true;
                }               
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
            return false;
        }

        /// <summary>
        /// This method will get the ISBN from zip
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private List<Staging> GetISBNDetails(string filePath)
        {
            List<Staging> isbnCollection = new List<Staging>();
            //Check the sourcePhysicalPath
            if (File.Exists(filePath))
            {

                //Read the sourceFile
                using (ZipInputStream ZipStream = new ZipInputStream(File.OpenRead(filePath)))
                {
                    ZipEntry theEntry;

                    while ((theEntry = ZipStream.GetNextEntry()) != null)
                    {
                        //Check the current entry is file or directory
                        if (!theEntry.IsDirectory && Path.GetExtension(theEntry.Name) == ".epub")
                        {
                            Staging stage = new Staging();
                            stage.ISBN = long.Parse(Path.GetFileNameWithoutExtension(theEntry.Name));
                            stage.Server = "STAGE";
                            stage.Active = 'Y';
                            isbnCollection.Add(stage);

                        }
                    }
                }
            }

            return isbnCollection;
        }

        /// <summary>
        /// This method will give data source for repeater
        /// </summary>
        /// <param name="stagingCollection">Data source for repeater</param>
        private void FillStagingList(List<Staging> stagingCollection)
        {
            try
            {             
                List<string> existedFileInStaging = GetFileNames(destinationPath_physical);
                List<Staging> Stagecollection = stagingCollection.Where(u => (existedFileInStaging.Contains(u.ISBN.ToString()))).ToList();
               
                //For Schedule
                List<string> existedFileInPub = GetFileNames(publishingPath_physical);
                List<string> existedFileInPublish = GetFileNames(publishingPath_tempPhysical);
                existedFileInPublish = existedFileInPublish.Concat(existedFileInPub).Distinct().ToList();
                List<Staging> PublishCollection = stagingCollection.Where(u => (existedFileInPublish.Contains(u.ISBN.ToString()))).ToList();
                stagingCollection = Stagecollection.Concat(PublishCollection).ToList<Staging>();
                stagingCollection = stagingCollection.Distinct().ToList<Staging>();
                stagingCollection.Where(u => u.Server.Equals("STAGE") && existedFileInStaging.Contains(u.ISBN.ToString())).ToList().ForEach(x => { x.Server = string.Empty; });
                stagingCollection.Where(u => u.Server.Equals("PRODUCTION")).ToList().ForEach(x => { x.Server = "PUBLISHED"; });
                stagingCollection.Where(u => u.Server.Equals("PUBLISHED") && (!existedFileInPublish.Contains(u.ISBN.ToString()))).ToList().ForEach(x => { x.Server = "<font style='float: right;margin-top: 10px;color:#D82525;line-height: 15px;'>DELETED</font><font style='font-size: 8pt; font-weight: lighter;color:#D82525;float: right;margin-top: 12px;margin-right: -63px;' >from production server</font>"; });
                stagingCollection.Where(u => (!u.Server.Equals("PUBLISHED")) && (!u.Server.Equals(string.Empty))).ToList().ForEach(x => { x.Checked = false; });
                stagingCollection = stagingCollection.Where(u => (!u.Server.Equals("STAGE"))).ToList<Staging>();
                stagingCollection = Sort(stagingCollection);
                StagingRepeater.DataSource = stagingCollection;
                StagingRepeater.DataBind();                
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// This Method will unzip the file and get the data for insert or update
        /// </summary>
        /// <param name="sourceZipFilePath">Source file path</param>
        /// <param name="destinationPath_physical">Destination file path</param>
        private void Unzip(string sourceZipFilePath, string destinationPath_physical)
        {

            try
            {
                //sourcePhysicalPath
                string sourceZipFilePath_physical = sourceZipFilePath;

                List<Staging> insertList = new List<Staging>();

                //Check the sourcePhysicalPath
                if (File.Exists(sourceZipFilePath_physical))
                {

                    //Read the sourceFile
                    using (ZipInputStream ZipStream = new ZipInputStream(File.OpenRead(sourceZipFilePath_physical)))
                    {

                        ZipEntry theEntry;

                        while ((theEntry = ZipStream.GetNextEntry()) != null)
                        {

                            //Check the current entry is file or directory
                            if (!theEntry.IsDirectory)
                            {

                                string strNewFile = System.IO.Path.Combine(destinationPath_physical, Path.GetFileName(theEntry.Name));

                                string pathFortheNewFile = Path.GetDirectoryName(strNewFile);

                                //Create the staging directory if not exist
                                if (!Directory.Exists(pathFortheNewFile))
                                {
                                    Directory.CreateDirectory(pathFortheNewFile);
                                }
                                using (FileStream streamWriter = File.Create(strNewFile))
                                {

                                    int size = 2048;

                                    byte[] data = new byte[2048];

                                    while (true)
                                    {
                                        size = ZipStream.Read(data, 0, data.Length);

                                        if (size > 0)

                                            streamWriter.Write(data, 0, size);

                                        else

                                            break;

                                    }
                                    streamWriter.Close();
                                }

                            }
                        }

                        ZipStream.Close();

                        File.Delete(sourceZipFilePath);
                    }

                }

            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }

        }

        /// <summary>
        /// This method will create log
        /// </summary>
        /// <param name="logList">collection of logs</param>
        private void CreateLog(List<Staging> logList)
        {            
            if (System.IO.File.Exists(LogPath_physical))
            {
                using (FileStream fs = new FileStream(LogPath_physical, FileMode.Append))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    foreach (Staging stage in logList)
                    {
                        sw.Write("\r\n");
                        sw.Write(stage.ISBN + "    " + "\t");
                        sw.Write(stage.Version + "     " + "\t");

                        if (stage.Server == "STAGE")
                        {
                            sw.Write(stage.Server + "           " + "\t");
                            sw.Write(stage.DateOfUpload.ToShortDateString() + "        " + "\t");
                        }
                        else
                        {
                            sw.Write(stage.Server + "      " + "\t");
                            sw.Write(stage.DateOfUpload.ToShortDateString() + "        " + "\t");
                        }
                        sw.Write(stage.TimeOfUpload.ToShortTimeString().Replace(" ", "") + " " + "\t");
                        sw.Write("\r\n");
                    }
                    sw.Close();
                }
            }
            else
            {
                using (FileStream fs = new FileStream(LogPath_physical, FileMode.OpenOrCreate))
                {
                    bool header = true;
                    StreamWriter sw = new StreamWriter(fs);
                    foreach (Staging stage in logList)
                    {
                        if (header)
                        {
                            sw.Write("\r\n");
                            sw.Write("ISBN            " + "\t");
                            sw.Write("Version_number " + "\t");
                            sw.Write("Staging/Publish " + "\t");
                            sw.Write("Date(DD/MM/YYYY) " + "\t");
                            sw.Write("Time " + "\t");
                            sw.Write("\r\n");
                            sw.Write("\r\n");

                            header = false;
                        }
                        sw.Write("\r\n");
                        sw.Write(stage.ISBN + "    " + "\t");
                        sw.Write(stage.Version + "     " + "\t");

                        if (stage.Server == "STAGE")
                        {
                            sw.Write(stage.Server + "           " + "\t");
                            sw.Write(stage.DateOfUpload.ToShortDateString() + "        " + "\t");
                        }
                        else
                        {
                            sw.Write(stage.Server + "      " + "\t");
                            sw.Write(stage.DateOfUpload.ToShortDateString() + "        " + "\t");
                        }
                        sw.Write(stage.TimeOfUpload.ToShortTimeString().Replace(" ", "") + " " + "\t");
                        sw.Write("\r\n");
                    }
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// This Method will generate list TO XML using LINQ
        /// </summary>
        /// <param name="staging">Collection of staging</param>
        /// <returns>XmlDocument</returns>
        private static XDocument GenerateListToXML(List<Staging> staging, string loginName)
        {
            
            XDocument xmlDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                          new XElement("DocumentElement",
                          from stage in staging
                          select new XElement("Staging",
                          new XElement("ISBN", stage.ISBN), new XElement("Active", stage.Active), new XElement("DateCreated", DateTime.Now), new XElement("UserCreated", loginName), new XElement("Server", stage.Server), new XElement("DateOfUpload", DateTime.Now), new XElement("Version", 0.01), new XElement("TimeOfUpload", DateTime.Now.ToString("HH:mm:ss")))));
            return xmlDocument;
        }
        
        /// <summary>
        /// This method will copy the file from stage to publish
        /// </summary>
        /// <param name="publishingPath">Publish folder physical path</param>
        /// <param name="destinationPath">Stage folder physical path</param>
        /// <param name="exist"></param>
        /// <param name="addedBookIsbn"></param>
        /// <returns></returns>
        private List<Staging> StagingToPublish(string publishingPath, string destinationPath, bool exist, List<string> addedBookIsbn)
        {
            List<Staging> updateStageToPubList = new List<Staging>();
            try
            {               
                if (Directory.Exists(destinationPath))
                {
                    foreach (string book in addedBookIsbn)
                    {
                        string strNewFile = System.IO.Path.Combine(destinationPath, book + ".epub");
                        //string strExistFile = System.IO.Path.Combine(publishingPath, book + ".epub");
                        string strExistFile = System.IO.Path.Combine(publishingPath, book);
                        if (!Directory.Exists(publishingPath_tempPhysical))
                        {
                            //Directory.Delete(publishingPath_tempPhysical, true);
                            Directory.CreateDirectory(publishingPath_tempPhysical);
                        }
                        // Directory.CreateDirectory(publishingPath_tempPhysical);
                        string strTempFile = System.IO.Path.Combine(publishingPath_tempPhysical, book);
                        if (File.Exists(strNewFile))
                        {
                            Staging stage = new Staging();
                            if (Directory.Exists(strExistFile))
                            {
                                if (exist)
                                {
                                   // File.Replace(strNewFile, strExistFile + ".epub", "", false);
                                   // Directory.Delete(strExistFile, true);
                                    File.Delete(strTempFile + ".epub");
                                    File.Copy(strNewFile, strTempFile + ".epub");
                                    stage.ISBN = long.Parse(Path.GetFileNameWithoutExtension(strNewFile));
                                    stage.EpubFileName = Path.GetFileName(strNewFile);
                                    stage.Server = "PRODUCTION";
                                    stage.Active = 'Y';
                                    updateStageToPubList.Add(stage);
                                }
                            }
                            else
                            {
                                File.Copy(strNewFile, strTempFile + ".epub");
                                stage.ISBN = long.Parse(Path.GetFileNameWithoutExtension(strNewFile));
                                stage.EpubFileName = Path.GetFileName(strNewFile);
                                stage.Server = "PRODUCTION";
                                stage.Active = 'Y';
                                updateStageToPubList.Add(stage);
                            }
                        }
                    }
                }             
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }
            return updateStageToPubList;
        }

        /// <summary>
        /// This method will get the collection of files existed in the path
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <returns>return collection of filenames</returns>
        private List<string> GetFileNames(string destinationPath)
        {
            try
            {
                List<string> existedBook = new List<string>();
                if (Directory.Exists(destinationPath))
                {
                    DirectoryInfo MyRoot = new DirectoryInfo(destinationPath);
                    foreach (FileInfo f in MyRoot.GetFiles("*.epub"))
                    {
                        existedBook.Add(Path.GetFileNameWithoutExtension(f.FullName));
                    }
                }
                return existedBook;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// This method will get the collection of files existed in the path
        /// </summary>
        /// <param name="destinationPath"></param>
        /// <returns>return collection of filenames</returns>
        private List<string> GetFileNamesInPublish(string destinationPath)
        {
            try
            {
                List<string> existedBook = new List<string>();
                if (Directory.Exists(destinationPath))
                {
                    DirectoryInfo MyRoot = new DirectoryInfo(destinationPath);
                    foreach (DirectoryInfo f in MyRoot.GetDirectories())
                    {
                        existedBook.Add(Path.GetFileName(f.FullName));
                    }
                }
                return existedBook;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Based on the sort direction this method will sort the collection
        /// </summary>
        /// <param name="stagingCollection">stagingCollection</param>
        /// <returns></returns>
        private List<Staging> Sort(List<Staging> stagingCollection)
        {

            if (DateSortDirection == SortDirection.Descending)
            {
                stagingCollection = stagingCollection.OrderBy(s => s.DateOfUpload).ToList();
            }
            else
            {
                stagingCollection = stagingCollection.OrderByDescending(s => s.DateOfUpload).ToList();
            }
            List<string> addedBook = IsbnIdHidden.Value.Trim().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(sValue => sValue.Trim()).ToList<string>();
            stagingCollection.ForEach(x => { x.Checked = false; });
            if (addedBook.Count > 0)
                stagingCollection.Where(x => addedBook.Contains(x.ISBN.ToString())).ToList().ForEach(x => { x.Checked = true; });
            return stagingCollection;
        }

        #endregion
        private void GetXMLOperation(string filePath, string sessionUpdate)
        {
            DnnLog.Error("FUNCTION START");
            string xmlPath_physical = ConfigurationManager.AppSettings["TempExtractPath"].ToString(); // from config

            if (File.Exists(ConfigurationManager.AppSettings["7Zippath"].ToString()))
                SevenZip.SevenZipBase.SetLibraryPath(ConfigurationManager.AppSettings["7Zippath"].ToString());
            SevenZipExtractor extractor = null;
            if (!Directory.Exists(xmlPath_physical))
            {
                Directory.CreateDirectory(xmlPath_physical);
                
            }
            Unzip(filePath, xmlPath_physical);
            List<string> isbnInxml = GetFileNames(xmlPath_physical, "*.epub");
            
            //TempExtractPath con
            foreach (string isbn in isbnInxml)
            {
                //xmlTempPath = xmlTempPath.Replace("ISBN", isbn.Substring(0, isbn.LastIndexOf('.')));
                if (File.Exists(Path.Combine(xmlPath_physical, isbn)))
                {
                    try
                    {

                        string output = Path.Combine(xmlPath_physical, isbn).Substring(0, Path.Combine(xmlPath_physical, isbn).LastIndexOf('.')).Trim();
                        //extractor = new SevenZipExtractor(Path.Combine(xmlPath_physical, isbn));
                        //extractor.ExtractArchive(output);
                        //if (Directory.Exists(output))
                        //{
                        //    DirectoryInfo di = new DirectoryInfo(output);
                        //    di.Delete(true);
                        //}

                        Directory.CreateDirectory(output);
                        DnnLog.Info("test... from groups11");
                        if (File.Exists(Path.Combine(xmlPath_physical, isbn)))
                            extractor = new SevenZipExtractor(Path.Combine(xmlPath_physical, isbn));
                        //DnnLog.Info("test... from groups11");
                        extractor.ExtractArchive(output);
                        extractor.Dispose();
                        DnnLog.Info("test... from groups12");
                        //if (File.Exists(Path.Combine(xmlPath_physical, isbn)))
                        //    File.Delete(Path.Combine(xmlPath_physical, isbn));
                        // File.Copy(Path.Combine(xmlPath_physical, isbn), Path.Combine(xmlPath_physical, isbn));
                        // File.Delete(Path.Combine(xmlPath_physical, isbn));

                        //    Read direct and get xml
                        //insert into table
                        //callupload again
                        // get current identities
                        string identities = GroupController.Instance.GetProductAndAuthorIds();
                        List<StoreXmlProperties> ePubNewList = new List<StoreXmlProperties>();
                        foreach (var item in Directory.GetFiles(output))
                        //foreach (var item in Directory.GetFiles(xmlPath_physical))
                        {
                            DnnLog.Error("test... from groups11");
                            if (Path.GetExtension(item).ToLower() == ".xml")
                            {
                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.Load(item);
                                DnnLog.Error("test... from groups");
                                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/Product");
                                string xmlValues = "\r\n";
                                foreach (XmlNode node in nodeList)
                                {
                                    Random random = new Random();
                                    StoreXmlProperties xmlProp = new StoreXmlProperties();
                                    xmlProp.AuthorID = !string.IsNullOrEmpty(identities.Split('|')[1]) ? (Convert.ToInt32(identities.Split('|')[1]) + 1).ToString() : random.Next().ToString();
                                    xmlProp.AuthorName = node.SelectSingleNode("Authors").InnerText;
                                    xmlProp.CopyRightYear = node.SelectSingleNode("CopyRightYear").InnerText;
                                    xmlProp.ISBN_13 = node.SelectSingleNode("ISBN13").InnerText;
                                    xmlProp.Product_ID = !string.IsNullOrEmpty(identities.Split('|')[0]) ? (Convert.ToInt32(identities.Split('|')[0]) + 1).ToString() : random.Next().ToString();
                                    xmlProp.ReadingLevel = node.SelectSingleNode("ReadingLevel").InnerText;
                                    xmlProp.Title = node.SelectSingleNode("Title").InnerText;
                                    xmlValues = string.Concat(xmlValues, "\r\n AuthorID-->" + xmlProp.AuthorID);
                                    xmlValues = string.Concat(xmlValues, "\r\n AuthorName-->" + xmlProp.AuthorName);
                                    xmlValues = string.Concat(xmlValues, "\r\n CopyRightYear-->" + xmlProp.CopyRightYear);
                                    xmlValues = string.Concat(xmlValues, "\r\n ISBN_13-->" + xmlProp.ISBN_13);
                                    xmlValues = string.Concat(xmlValues, "\r\n Product_ID-->" + xmlProp.Product_ID);
                                    xmlValues = string.Concat(xmlValues, "\r\n ReadingLevel-->" + xmlProp.ReadingLevel);
                                    xmlValues = string.Concat(xmlValues, "\r\n Title-->" + xmlProp.Title);
                                    xmlValues = string.Concat(xmlValues, "\r\n -----------------");
                                    ePubNewList.Add(xmlProp);
                                }
                                DnnLog.Error(xmlValues);
                                DnnLog.Error("test... from groups");
                            }
                        }
                        if (File.Exists(Path.Combine(xmlPath_physical, isbn)))
                            File.Delete(Path.Combine(xmlPath_physical, isbn));
                        if(File.Exists(output))
                            File.Delete(output);
                        DataTable newXmlIsbnsDt = ToDataTable(ePubNewList);
                        DnnLog.Error("Xmltableinfo :" + newXmlIsbnsDt.Rows.Count);
                        if (GroupController.Instance.InsertNewePubBooks(newXmlIsbnsDt))
                        {
                            Session["update"] = sessionUpdate;
                            UploadBooksButton_Click(UploadBooksButton, EventArgs.Empty);
                        }
                        DnnLog.Error("CODE BEFORE THUMBNAILS EXTRACT");
                        string thumNailsSourcePath = ConfigurationManager.AppSettings["TempISBNThumNailsSourcePath"],
                            ISBNThumNailsPath = ConfigurationManager.AppSettings["LIVE_IMAGE_PATH"],
                        ISBNThumNailsPath1 = ConfigurationManager.AppSettings["ISBNThumNailsPath"];
                        
                        thumNailsSourcePath = thumNailsSourcePath.Replace("ISBN", isbn.Substring(0, isbn.LastIndexOf('.')));
                        DnnLog.Error("thumNailsSourcePath-->" + thumNailsSourcePath + ", ISBNThumNailsPath-->" + ISBNThumNailsPath);
                        // if (File.Exists(Path.Combine(ISBNThumNailsSourcePath, isbn).Substring(0, Path.Combine(ISBNThumNailsSourcePath, isbn).LastIndexOf('.')).Trim()))
                        if (Directory.Exists(thumNailsSourcePath))
                        {
                            DnnLog.Error("Directory.Exists(thumNailsSourcePath)");
                            DirectoryInfo thumbNailsDirectory = new DirectoryInfo(thumNailsSourcePath);
                            foreach (FileInfo file in thumbNailsDirectory.GetFiles())
                            {
                                try
                                {
                                    DnnLog.Error("thumbNailsDirectory.GetFiles()"+file.ToString());
                                    if (File.Exists(Path.Combine(ISBNThumNailsPath, 'F'+file.ToString())))
                                        File.Delete(Path.Combine(ISBNThumNailsPath, 'F' + file.ToString()));
                                    File.Copy(Path.Combine(thumNailsSourcePath, file.ToString()), Path.Combine(ISBNThumNailsPath, 'F' + file.ToString()));

                                   // if (File.Exists(Path.Combine(ISBNThumNailsPath1, 'F' + file.ToString())))
                                   //     File.Delete(Path.Combine(ISBNThumNailsPath1, 'F' + file.ToString()));
                                   // File.Copy(Path.Combine(thumNailsSourcePath, file.ToString()), Path.Combine(ISBNThumNailsPath1, 'F' + file.ToString()));
                                }
                                catch (Exception ex) { DnnLog.Error("exception in thumbnail code :" + ex.Message); LogFileWrite(new Exception(ex.Message + " for ISBN : " + isbn, ex.InnerException)); }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        DnnLog.Error("exception in xmlisbns :" + ex.Message);
                        LogFileWrite(ex);
                    }
                }
            }

        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        private List<string> GetFileNames(string destinationPath, string extension)
        {
            List<string> existedBook = new List<string>();
            if (Directory.Exists(destinationPath))
            {
                DirectoryInfo MyRoot = new DirectoryInfo(destinationPath);
                foreach (FileInfo f in MyRoot.GetFiles(extension))
                {
                    existedBook.Add(Path.GetFileName(f.FullName));
                }
            }
            return existedBook;
        }

    }

    class StoreXmlProperties
    {


        public string Title { get; set; }
        public string ISBN_13 { get; set; }
        public string Product_ID { get; set; }
        public string CopyRightYear { get; set; }
        public string AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string ReadingLevel { get; set; }
    }
}