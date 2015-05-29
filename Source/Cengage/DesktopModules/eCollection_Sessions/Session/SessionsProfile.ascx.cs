using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using _Sessions = DotNetNuke.Modules.eCollection_Sessions.Components.Modal.Sessions;
using System.Text;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Sessions.Components.Controllers;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;
using System.IO;
namespace DotNetNuke.Modules.eCollection_Sessions.Session
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SessionsProfile" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Session profile screen
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class SessionsProfile : eCollection_SessionsModuleBase
    {
        #region events
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (EditSessionId.HasValue)
                    {
                        BindSession(EditSessionId.Value);
                    }
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgEditSession_Click(object sender, EventArgs e)
        {
            Session["SelectedGroups"] = null;
            Session["SelectedProducts"] = null;           
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=editsession");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgEndSession_Click(object sender, EventArgs e)
        {
            SessionController _sessionController = SessionController.Instance;
            List<IDCollection> IDCollectionList = new List<IDCollection>();
            if (EditSessionId.HasValue)
            {
                IDCollectionList.Add(new IDCollection(EditSessionId.Value, ""));
                _sessionController.UpdateSessionExpiryDate(IDCollectionList, LoginName);
                _sessionController.ClearAllCaches();
                eCollection_Dashboards.Components.Controller.DashboardController.Instance.ClearAllCache();
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TodaysIndependentRecordings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Literal TodaysIndMemberID = (Literal)e.Item.FindControl("TodaysIndMemberID");
                Literal TodaysSessionHistoryID = (Literal)e.Item.FindControl("TodaysSessionHistoryID");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                Repeater gridView = (Repeater)e.Item.FindControl("TodayIndeVideoRepeater");
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                SessionController _sessionController = SessionController.Instance;
                List<Group> groupcollection = _sessionController.GetRecordingHistoryBySession(EditSessionId.Value, DateTime.Now.ToString("MMMM"), Convert.ToInt32(TodaysIndMemberID.Text), Convert.ToInt32(TodaysSessionHistoryID.Text));
                if (groupcollection.Count > 0)
                {
                    groupcollection.ForEach(x =>
                    {
                        if (x.RecordFilePath != string.Empty)
                        {
                            string driveName = ConfigurationManager.AppSettings.Get("RecordingPath");
                            string text = System.IO.File.ReadAllText(Path.Combine(driveName, x.RecordFilePath));

                            byte[] bytes = System.Convert.FromBase64String(text);

                            x.RecordFilePath = x.RecordFilePath.Split('.')[0] + ".m4a";
                            if (!(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath))))
                                File.WriteAllBytes(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath), bytes);
                        }
                        x.PageName = "Content Page " + x.PageName.Split('-')[1];

                    });
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
                else
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    if (recordpanel != null)
                        recordpanel.Style.Add("display", "none");

                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void YesterDayGuidedRecordings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Literal YesterdaysMemberID = (Literal)e.Item.FindControl("YesterdaysMemberID");
                Literal TodaysSessionHistoryID = (Literal)e.Item.FindControl("TodaysSessionHistoryID");
                Repeater gridView = (Repeater)e.Item.FindControl("YesterdayGuidVideoRepeater");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                SessionController _sessionController = SessionController.Instance;
                List<Group> groupcollection = _sessionController.GetRecordingHistoryBySession(EditSessionId.Value, DateTime.Now.ToString("MMMM"), Convert.ToInt32(YesterdaysMemberID.Text), Convert.ToInt32(TodaysSessionHistoryID.Text));
                if (groupcollection.Count > 0)
                {
                    groupcollection.ForEach(x =>
                    {
                        if (x.RecordFilePath != string.Empty)
                        {
                            string driveName = ConfigurationManager.AppSettings.Get("RecordingPath");
                            string text = System.IO.File.ReadAllText(Path.Combine(driveName, x.RecordFilePath));

                            byte[] bytes = System.Convert.FromBase64String(text);

                            x.RecordFilePath = x.RecordFilePath.Split('.')[0] + ".m4a";
                            if (!(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath))))
                                File.WriteAllBytes(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath), bytes);
                        }
                        x.PageName = "Content Page " + x.PageName.Split('-')[1];
                    });
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
                else
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    if (recordpanel != null)
                        recordpanel.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void YesterDayIndependentRecordings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Literal YesterdaysIndMemberID = (Literal)e.Item.FindControl("YesterdaysIndMemberID");
                Literal TodaysSessionHistoryID = (Literal)e.Item.FindControl("TodaysSessionHistoryID");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                Repeater gridView = (Repeater)e.Item.FindControl("YesterdayIndVideoRepeater");
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                SessionController _sessionController = SessionController.Instance;
                List<Group> groupcollection = _sessionController.GetRecordingHistoryBySession(EditSessionId.Value, DateTime.Now.ToString("MMMM"), Convert.ToInt32(YesterdaysIndMemberID.Text), Convert.ToInt32(TodaysSessionHistoryID.Text));
                if (groupcollection.Count > 0)
                {
                    groupcollection.ForEach(x =>
                    {
                        if (x.RecordFilePath != string.Empty)
                        {
                            string driveName = ConfigurationManager.AppSettings.Get("RecordingPath");
                            string text = System.IO.File.ReadAllText(Path.Combine(driveName, x.RecordFilePath));

                            byte[] bytes = System.Convert.FromBase64String(text);

                            x.RecordFilePath = x.RecordFilePath.Split('.')[0] + ".m4a";
                            if (!(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath))))
                                File.WriteAllBytes(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath), bytes);
                        }
                        x.PageName = "Content Page " + x.PageName.Split('-')[1];
                    });
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
                else
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    if (recordpanel != null)
                        recordpanel.Style.Add("display", "none");
                }

            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Last7DaysGuidedRecordings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {

                Literal LastSevendaysIndMemberID = (Literal)e.Item.FindControl("LastSevendaysGuiMemberID");
                Literal TodaysSessionHistoryID = (Literal)e.Item.FindControl("TodaysSessionHistoryID");
                Repeater gridView = (Repeater)e.Item.FindControl("LastSevenDaysVideoRepeater");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                SessionController _sessionController = SessionController.Instance;
                List<Group> groupcollection = _sessionController.GetRecordingHistoryBySessionLastSeven(EditSessionId.Value, Convert.ToInt32(LastSevendaysIndMemberID.Text), Convert.ToInt32(TodaysSessionHistoryID.Text));
                if (groupcollection.Count > 0)
                {
                    groupcollection.ForEach(x =>
                    {
                        if (x.RecordFilePath != string.Empty)
                        {
                            string driveName = ConfigurationManager.AppSettings.Get("RecordingPath");
                            string text = System.IO.File.ReadAllText(Path.Combine(driveName, x.RecordFilePath));

                            byte[] bytes = System.Convert.FromBase64String(text);

                            x.RecordFilePath = x.RecordFilePath.Split('.')[0] + ".m4a";
                            if (!(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath))))
                                File.WriteAllBytes(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath), bytes);
                        }
                        x.PageName = "Content Page " + x.PageName.Split('-')[1];
                    });
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
                else
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    if (recordpanel != null)
                    recordpanel.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Last7DaysIndependentRecordings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Literal LastSevendaysIndMemberID = (Literal)e.Item.FindControl("LastSevendaysIndMemberID");
                Literal TodaysSessionHistoryID = (Literal)e.Item.FindControl("TodaysSessionHistoryID");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                Repeater gridView = (Repeater)e.Item.FindControl("LastSevenDaysIndVideoRepeater");
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                SessionController _sessionController = SessionController.Instance;
                List<Group> groupcollection = _sessionController.GetRecordingHistoryBySessionLastSeven(EditSessionId.Value, Convert.ToInt32(LastSevendaysIndMemberID.Text), Convert.ToInt32(TodaysSessionHistoryID.Text));
                if (groupcollection.Count > 0)
                {

                    groupcollection.ForEach(x =>
                    {
                        if (x.RecordFilePath != string.Empty)
                        {
                            string driveName = ConfigurationManager.AppSettings.Get("RecordingPath");
                            string text = System.IO.File.ReadAllText(Path.Combine(driveName, x.RecordFilePath));

                            byte[] bytes = System.Convert.FromBase64String(text);

                            x.RecordFilePath = x.RecordFilePath.Split('.')[0] + ".m4a";
                            if (!(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath))))
                                File.WriteAllBytes(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath), bytes);
                        }
                        x.PageName = "Content Page " + x.PageName.Split('-')[1];
                    });
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
                else
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    if (recordpanel != null)
                        recordpanel.Style.Add("display", "none");
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RestMonthRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                SessionController _sessionController = SessionController.Instance;
                Label label = (Label)e.Item.FindControl("RestMonthLabel");
                Repeater independentRepeater = (Repeater)e.Item.FindControl("RestIndependentHistory");
                independentRepeater.DataSource = _sessionController.GetReadingHistoryBySession(EditSessionId.Value, label.Text);
                independentRepeater.DataBind();

            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RestIndependentHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Literal TodaysGuidedMemberID = (Literal)e.Item.FindControl("RestIndMemberID");
                Literal TodaysSessionHistoryID = (Literal)e.Item.FindControl("TodaysSessionHistoryID");
                Repeater gridView = (Repeater)e.Item.FindControl("RestIndenpVideoRepeater");
                Label monthLabel = (Label)e.Item.Parent.Parent.FindControl("RestMonthLabel");
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                SessionController _sessionController = SessionController.Instance;
                List<Group> groupcollection = _sessionController.GetRecordingHistoryBySession(EditSessionId.Value, monthLabel.Text.Trim(), Convert.ToInt32(TodaysGuidedMemberID.Text), Convert.ToInt32(TodaysSessionHistoryID.Text));
                if (groupcollection.Count > 0)
                {
                    groupcollection.ForEach(x =>
                    {
                        if (x.RecordFilePath != string.Empty)
                        {
                            string driveName = ConfigurationManager.AppSettings.Get("RecordingPath");
                            string text = System.IO.File.ReadAllText(Path.Combine(driveName, x.RecordFilePath));

                            byte[] bytes = System.Convert.FromBase64String(text);

                            x.RecordFilePath = x.RecordFilePath.Split('.')[0] + ".m4a";
                            if (!(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath))))
                                File.WriteAllBytes(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath), bytes);
                        }
                        x.PageName = "Content Page " + x.PageName.Split('-')[1];
                    });
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
                else
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    if (recordpanel != null)
                        recordpanel.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RestGuidedRecordings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Literal TodaysGuidedMemberID = (Literal)e.Item.FindControl("RestGuidMemberID");
                Literal TodaysSessionHistoryID = (Literal)e.Item.FindControl("TodaysSessionHistoryID");
                Repeater gridView = (Repeater)e.Item.FindControl("RestGuidVideoRepeater");
                Label monthLabel = (Label)e.Item.Parent.Parent.FindControl("RestMonthLabel");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                SessionController _sessionController = SessionController.Instance;
                List<Group> groupcollection = _sessionController.GetRecordingHistoryBySession(EditSessionId.Value,monthLabel.Text.Trim(), Convert.ToInt32(TodaysGuidedMemberID.Text), Convert.ToInt32(TodaysSessionHistoryID.Text));
                if (groupcollection.Count > 0)
                {
                    groupcollection.ForEach(x =>
                    {
                        if (x.RecordFilePath != string.Empty)
                        {
                            string driveName = ConfigurationManager.AppSettings.Get("RecordingPath");
                            string text = System.IO.File.ReadAllText(Path.Combine(driveName, x.RecordFilePath));

                            byte[] bytes = System.Convert.FromBase64String(text);

                            x.RecordFilePath = x.RecordFilePath.Split('.')[0] + ".m4a";
                            if (!(File.Exists(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath))))
                                File.WriteAllBytes(Path.Combine(ConfigurationManager.AppSettings.Get("AudioPath"), x.RecordFilePath), bytes);
                        }
                        x.PageName = "Content Page " + x.PageName.Split('-')[1];
                    });
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
                else
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    if (recordpanel != null)
                        recordpanel.Style.Add("display", "none");
                }
            }
            catch (Exception ex)
            {
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BooksRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImage");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
        #endregion

        #region MemberFunction
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SessionId"></param>
        private void BindSession(int SessionId)
        {
            try
            {
                _Sessions sessions = AllActiveSessions.Where(u => u.SessionId == SessionId).FirstOrDefault();
                if (sessions != null)
                {
                    SessionName.InnerText = (sessions.Name.Length > 18 ? sessions.Name.Substring(0, 18) + "..." : sessions.Name);
                    lblSessionCreatedDate.Text = sessions.SessionCreatedDate.ToString("hh;mmtt, dd/MM/yyyy");
                    DOBHdFld.Value = sessions.SessionExpiryDate.Date.ToString("dd/MM/yyyy");
                    txtNotes.Text = sessions.Notes.Replace(Environment.NewLine, "<br />");
                    BindOpenStudents();
                    BindUnOpenStudents();
                    BindBooks();
                    BindReadingHistory();
                }

                if (DateTime.Now.Date >= sessions.SessionExpiryDate.Date)
                {
                    imgEndSession.Visible = false;
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindReadingHistory()
        {
            try
            {
                bool ContentDisplayed = false;
                bool isNoReadingHistory = true;
                SessionController _sessionController = SessionController.Instance;                
                List<Group> getTodaysIndependentRecordings = _sessionController.GetReadingHistoryBySession(EditSessionId.Value, DateTime.Now.ToString("MMMM"));                
                List<Group> getYesterDayIndependentRecordings = _sessionController.GetReadingHistoryBySession(EditSessionId.Value,DateTime.Now.ToString("MMMM"));
                List<Group> getLast7DaysIndependentRecordings = _sessionController.GetReadingHistoryBySessionLastSeven(EditSessionId.Value);              
                List<Group> getrestOfMonthIndependentRecordings = _sessionController.GetReadingHistoryBySession(EditSessionId.Value, DateTime.Now.ToString("MMMM"));
                List<string> strMonths = _sessionController.GetSessionHistoryBySessionMonthNames(EditSessionId.Value);
                DateTime startOfWeek = DateTime.Today;
                int delta = DayOfWeek.Monday - startOfWeek.DayOfWeek;
                startOfWeek = startOfWeek.AddDays(delta);
                DateTime endOfWeek = startOfWeek.AddDays(7);
                try
                {
                    if (getTodaysIndependentRecordings.Count > 0)
                    {
                        TodaysIndependentRecordings.DataSource = getTodaysIndependentRecordings.Where(u => u.BookOpenAt.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
                        TodaysIndependentRecordings.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    //Messages.ShowWarning(ex.Message);
                    LogFileWrite(ex);
                }

                if (TodaysIndependentRecordings.Items.Count == 0)
                {
                    TodaysDiv.Visible = false;
                    YesterDayGuidedRecordingsDiv.Style.Add("margin-top", "-18px");
                }
                else
                {
                    isNoReadingHistory = false;
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "ShowTodaysReadingHistory", "<script>$('#TodaysHistToggle').click()</script>", false);
                    ContentDisplayed = true;
                }
                try
                {
                    if (getYesterDayIndependentRecordings.Count > 0)
                    {
                        YesterDayIndependentRecordings.DataSource = getYesterDayIndependentRecordings.Where(u => u.BookOpenAt.ToShortDateString() == DateTime.Now.AddDays(-1).ToShortDateString()).ToList();
                        YesterDayIndependentRecordings.DataBind();
                    }

                }
                catch (Exception ex)
                {
                    //Messages.ShowWarning(ex.Message);
                    LogFileWrite(ex);
                }

                if (YesterDayIndependentRecordings.Items.Count == 0)
                {
                    YesterDayGuidedRecordingsDiv.Visible = false;
                    if (!TodaysDiv.Visible)
                        Last7DaysGuidedRecordingsDiv.Style.Add("margin-top", "-18px");
                }
                else
                {
                    isNoReadingHistory = false;
                    if (!ContentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "ShowYesterdaysReadingHistory", "<script>$('#YestHistToggle').click()</script>", false);
                        ContentDisplayed = true;
                    }
                }

                try
                {

                    if (getLast7DaysIndependentRecordings.Count > 0)
                    {
                        Last7DaysIndependentRecordings.DataSource = getLast7DaysIndependentRecordings;
                        Last7DaysIndependentRecordings.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    //Messages.ShowWarning(ex.Message);
                    LogFileWrite(ex);
                }
                if (getLast7DaysIndependentRecordings.Count == 0 )
                {
                    Last7DaysGuidedRecordingsDiv.Visible = false;
                    if (!TodaysDiv.Visible && !YesterDayGuidedRecordingsDiv.Visible && !Last7DaysGuidedRecordingsDiv.Visible)
                        RestMonthDiv.Style.Add("margin-top", "-20px");
                }
                else
                {
                    isNoReadingHistory = false;
                    if (!ContentDisplayed)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Show7daysReadingHistory", "<script>$('#Last7DToggle').click()</script>", false);
                        ContentDisplayed = true;
                    }
                }
                try
                {
                    if (strMonths != null)
                    {
                        RestMonthRepeater.DataSource = strMonths;
                        RestMonthRepeater.DataBind();
                        if (!ContentDisplayed)
                        {
                            ScriptManager.RegisterStartupScript(Page, GetType(), "ShowMonthwiseReadingHistory", "<script>$($('#RestMonthDiv').children()[0].children[1]).click()</script>", false);
                            ContentDisplayed = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Messages.ShowWarning(ex.Message);
                    LogFileWrite(ex);
                }
                if (RestMonthRepeater.Items.Count == 0)
                {
                    RestMonthDiv.Visible = false;
                }
                else
                {
                    isNoReadingHistory = false;
                }

                if (isNoReadingHistory)
                {
                    SecondDiv.Visible = false;
                    MessageOuterDiv.Style.Add("Display", "block");
                    if (Message1.Text == "")
                    {
                        Message1.Text = Constants.NOREADHISTORYINFO;
                    }             

                }
                else
                {
                    if (Message1.Text=="")
                        MessageOuterDiv.Style.Add("Display", "none");
                    else MessageOuterDiv.Style.Add("Display", "block");
                }



            }
            catch (Exception ex)
            {
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void BindBooks()
        {
            try
            {
                BooksRepeater.DataSource = SessionBooks;
                BooksRepeater.DataBind();
                if (BooksRepeater.Items.Count > 0)
                {
                }
                else
                {
                    Message1.Text = Constants.NOBOOKINFO;
                }
            }
            catch (Exception ex)
            {
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindOpenStudents()
        {
            try
            {

                if (OpenedStudents != null)
                {
                    repOpenedStudents.DataSource = OpenedStudents.ToList();
                    repOpenedStudents.DataBind();
                }
            }
            catch (Exception ex)
            {
                //Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindUnOpenStudents()
        {
            try
            {
                if (UnOpenedStudents != null)
                {
                    repUnOpenedStudents.DataSource = UnOpenedStudents.ToList();
                    repUnOpenedStudents.DataBind();
                }
            }
            catch (Exception ex)
            {
               // Messages.ShowWarning(ex.Message);
                LogFileWrite(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void FillBookHistory()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("Student", typeof(string));
            dt.Columns.Add("BookName", typeof(string));
            dt.Columns.Add("DateTime", typeof(string));
            dt.Columns.Add("VocabLogCount", typeof(string));
            dt.Columns.Add("BookOpenTime", typeof(string));

            dr = dt.NewRow();
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dt.Rows.Add(dr);

            BooksRepeater.DataSource = dt.DefaultView;
            BooksRepeater.DataBind();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Notes"></param>
        public void DisplayNotes(string Notes)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.IO.StringReader sr = new System.IO.StringReader(Notes);
            string tmpS = null;
            int StartIndex = 0;
            int EndIndex = 75;
            int noOfLines = (Notes.Length) / 75;

            if (Notes.Length > 75)
            {

                for (int i = 0; i <= noOfLines; i++)
                {
                    sb.Append(Notes.Substring(StartIndex, EndIndex));
                    sb.Append("<br />");
                    StartIndex += 75;
                    if ((Notes.Length - StartIndex) < 75)
                    {
                        EndIndex = Notes.Length - StartIndex;
                    }
                }
            }
            var convertedString = sb.ToString();
            txtNotes.Text = convertedString;
        }
        #endregion

    }


}