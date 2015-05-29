using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Groups.Components;
using DotNetNuke.Modules.eCollection_Groups.Components.Common ;
using _Students = DotNetNuke.Modules.eCollection_Groups.Components.Modal.Students;
using System.Text;
using DotNetNuke.Modules.eCollection_Groups.Components.Modal;
using DotNetNuke.Common.Utilities;
using System.IO;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="GroupProfile" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //  To display profile details of the selected group
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class GroupProfile : eCollection_GroupsModuleBase
    {
        int? groupId = null;
        string groupName = null;
        int studentCount;
        int RMCount = 0;
        //
        DateTime GrpAddedDate = new DateTime();
        GroupController groupController = GroupController.Instance;

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=groupprofile");
            if (EditClassId.Value != -1)
            {
                groupId = EditClassId.Value;
            }
            else if (EditGroupId.Value != -1)
            {
                groupId = EditGroupId.Value;
            }
            else
            {
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
            }
            if (!IsPostBack)
            {
                int memberCount = 0;

                //if (GroupName != null)
                //{
                try
                {
                    StudentList = null;
                    TeachersList = null;
                    DataCache.RemoveCache(string.Format("GetStudentNameInGroup{0}", LoginName));
                    DataCache.RemoveCache(string.Format("GetStudentByGroup{0}", LoginName));
                    Components.Groups groups = groupController.GetGroupProfileByGroup(groupId.Value);
                    //groupId = groups.GroupId;
                    Int32 minStartFrom = 0;
                    Int32 maxStartFrom = 0;
                    Int32 maxReadLevelUpto = 0;
                    Int32 minReadLevelUpto = 0;
                    SortAndBind();
                    if (EditClassId.Value == -1)
                        GpYourClass.Style.Add("display", "none");
                    GroupNameLabel.InnerText = groups.Name.Length > 22 ? groups.Name.Substring(0, 22) + "..." : groups.Name;
                    GroupName = GroupNameLabel.InnerText;
                    MemebersCountLabel.Text = groups.MemberCount.ToString();
                    if (groups.BooksOpened > 1)
                        BookOpenedText.InnerText = "BOOKS OPENED";
                    NoOfBoooksOpenedLabel.Text = groups.BooksOpened.ToString();
                    if (studentCount != 0 && groups.BooksOpened != 0)
                    {
                        IndependentCountLabel.Text = groups.Independent == 0 ? "0%" : string.Concat((Convert.ToInt64((groups.Independent * 100.0 / (groups.Independent + groups.Guided)))).ToString(), "%");// (((Convert.ToInt64((groups.Independent * 100.0 / studentCount))) / groups.BooksOpened) > 100 ? "100%" : ((Convert.ToInt64((groups.Independent * 100.0 / studentCount))) / groups.BooksOpened) + "%").ToString();

                        GuidedCountLabel.Text = string.Concat((100 - Convert.ToInt64((groups.Independent * 100.0 / (groups.Independent + groups.Guided)))).ToString(), "%"); // groups.Guided == 0 ? "0%" : (((Convert.ToInt64((groups.Guided * 100.0 / studentCount))) / groups.BooksOpened) > 100 ? "100%" : ((Convert.ToInt64((groups.Guided * 100.0 / studentCount))) / groups.BooksOpened) + "%").ToString();
                    }
                    //if (groups.MyWords == 0)
                    //{
                    //    ListenButton.Attributes.Add("onclick", "return false");
                    //}
                    NoOfWordsLabel.Text = groups.MyWords.ToString();
                    //if (groups.MyRecording == 0)
                    //{
                    //    WordLogButton.Attributes.Add("onclick", "return false");
                    //}
                    Dictionary<string, string> myDictionary = new Dictionary<string, string>();
                    myDictionary["Magenta"] = "rgb(195, 0, 95)";
                    myDictionary["Blue"] = "rgb(0,95,170)";
                    myDictionary["Emerald"] = "#009D58";
                    myDictionary["Gold"] = "rgb(217,138,0)";
                    myDictionary["Green"] = "rgb(0,105,50)";
                    myDictionary["Orange"] = "rgb(208,101,25)";
                    myDictionary["Purple"] = "rgb(73,40,127)";
                    myDictionary["Red"] = "rgb(194,0,22)";
                    myDictionary["Ruby"] = "#A8052C";
                    myDictionary["Sapphire"] = "#00316E";
                    myDictionary["Silver"] = "rgb(152,165,174)";
                    myDictionary["Turquoise"] = "rgb(117,176,160)";
                    myDictionary["Yellow"] = "rgb(234,184,20)";
                    NoOfRecordingsLabel.Text = groups.MyRecording.ToString();
                    foreach (Components.Groups group in groupController.GetReadingGraphByGroup(groupId.Value))
                    {
                        MaxReadLevel.InnerText = group.MaxReadLevel.ToString();
                        MinReadLevel.InnerText = group.MinReadLevel.ToString();
                        List<string> colorList = groupController.GetColorByReadLevel(group.MinReadLevel, group.MaxReadLevel);
                        divcircle.Style.Add("background", myDictionary[colorList[0]] == null ? "#707070" : myDictionary[colorList[0]]);
                        leftcolorreading.Value = myDictionary[colorList[0]].ToString();
                        rightcolorreading.Value = myDictionary[colorList[1]].ToString();
                        divcircletwo.Style.Add("background", myDictionary[colorList[1]] == null ? "#707070" : myDictionary[colorList[1]]);
                        sixmonthBefore.InnerText = group.DateCreated.ToString("dd/MM/yyyy");
                        MinStartReadFrom.InnerText = group.MinStartReadFrom.ToString();
                        minStartFrom = group.MinStartReadFrom;
                        minReadLevelUpto=group.MinReadLevel;
                        maxStartFrom = group.MaxStartReadUpto;
                        maxReadLevelUpto = group.MaxReadLevel; 
                        
                        GrpAddedDate = group.DateCreated;
                        MaxStartReadUpto.InnerText = group.MaxStartReadUpto.ToString();
                        int month = (DateTime.Now - group.DateCreated).Days / 30 ;
                        if (month > 1)
                        {
                            TotalMonth.InnerText = month + " MONTHS";
                        }
                        else
                        {
                            TotalMonth.InnerText = month + " MONTH";
                        }

                        TodayDate.InnerText = DateTime.Now.ToString("dd/MM/yyyy");
                        MinCurrReadFrom.InnerText = group.MinCurrReadFrom.ToString();
                        MaxCurrReadUpto.InnerText = group.MaxCurrReadUpto.ToString();
                    }

                    List<Students> studentCollection = groupController.GetReadingLevelByGroup(groupId.Value);
                    List<string> str = new List<string>();
                    for (int i = 0; i < 12; i++)
                    {
                        str.Add(i.ToString());
                    }
                    graph.DataSource = str;
                    graph.DataBind();



                    Int32 mRL = 0;
                    Int32 MRL= mRL;
                    DateTime graffRefDate = new DateTime();
                    int[] RL = new int[2];
                    int count =0;
                    if (GrpAddedDate.Year < DateTime.Now.Year)
                    {
                        count = 1;
                        DateTime refDate=new DateTime(DateTime.Now.Year, 1, 1);
                        sixmonthBefore.InnerText = refDate.ToString("dd/MM/yyyy");
                        TotalMonth.InnerText = ((DateTime.Now - refDate).Days / 30).ToString() + " MONTH(S)";

                    }
                    else count = GrpAddedDate.Month;
                    int count1 = count;
                    grfHdnLine.Value = count.ToString();
                    for (int i = 0; i < 12 - count1 + 1; i++)
                    {
                        if (count != 12)
                        {
                            
                            graffRefDate = new DateTime(DateTime.Now.Year, count + 1, 1);
                            if (graffRefDate.Month <= DateTime.Now.Month + 1)
                            {
                                if (groupId != null)
                                {
                                    int grpId = (int)groupId;
                                    RL = groupController.GetMinMaxReadingLevelByDate(grpId, graffRefDate);

                                }
                                else
                                {
                                    RL[0] = 0;
                                    RL[1] = 0;
                                }
                                mRL = RL[0];
                                MRL = RL[1];
                                pointsHdn.Value = string.Concat(pointsHdn.Value, '-', i.ToString(), mRL < 10 ? "0" + mRL.ToString() : mRL.ToString());
                                pointsHdn.Value = string.Concat(pointsHdn.Value, '-', i.ToString(), MRL < 10 ? "0" + MRL.ToString() : MRL.ToString());
                            }
                            count++;

                        }
                        else if (count==12)
                        {
                            graffRefDate = new DateTime(DateTime.Now.Year +1, 1, 1);
                            if (graffRefDate <= DateTime.Now.AddMonths(1))
                            {
                                if (groupId != null)
                                {
                                    int grpId = (int)groupId;
                                    RL = groupController.GetMinMaxReadingLevelByDate(grpId, graffRefDate);

                                }
                                else
                                {
                                    RL[0] = 0;
                                    RL[1] = 0;
                                }
                                mRL = RL[0];
                                MRL = RL[1];
                                pointsHdn.Value = string.Concat(pointsHdn.Value, '-', i.ToString(), mRL < 10 ? "0" + mRL.ToString() : mRL.ToString());
                                pointsHdn.Value = string.Concat(pointsHdn.Value, '-', i.ToString(), MRL < 10 ? "0" + MRL.ToString() : MRL.ToString());
                            }
                            count++;
                        }

                    }
                    studentCollection = studentCollection.Where(u => u.ReadingPercentage.ToString() != "0%").ToList();
                    ReadingLevelRepeater.DataSource = studentCollection.Select(u => new { u.ReadingLevel, u.ReadingPercentage }).Distinct().ToList();
                    ReadingLevelRepeater.DataBind();
                   
                    RestMonthRepeater.DataSource = groupController.GetMonthByGroup(groupId.Value,"PROFILE");
                    RestMonthRepeater.DataBind();
                    List<Components.Groups> getTodaysIndependentRecordings = groupController.GetReadingHistoryByGroup(groupId.Value, DateTime.Now.ToString("MMMM"));
                    TodaysIndependentRecordings.DataSource = getTodaysIndependentRecordings.Where(u => u.BookOpenAt.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
                    TodaysIndependentRecordings.DataBind();
                    List<Components.Groups> getYesterdayIndependent = groupController.GetReadingHistoryByGroup(groupId.Value, DateTime.Now.AddDays(-1).ToString("MMMM"));
                    YesterDayIndependentRecordings.DataSource = getYesterdayIndependent.Where(u => u.BookOpenAt.ToShortDateString() == DateTime.Now.AddDays(-1).ToShortDateString()).ToList();
                    YesterDayIndependentRecordings.DataBind();

                    Last7DaysIndependentRecordings.DataSource = groupController.GetLastSevenDaysReadings(groupId.Value, DateTime.Now.AddDays(-9), DateTime.Now.AddDays(-2));
                    Last7DaysIndependentRecordings.DataBind();

                    
                    List < List < Components.Groups >> masterGrpSessionList= groupController.GetReadingSessionByGroupAndSectionCat(groupId.Value, DateTime.Now);
                    Last7DaysReadingSessionRepeater.DataSource = masterGrpSessionList[2];
                    Last7DaysReadingSessionRepeater.DataBind();

                    YesterdaysReadingSessionRepeater.DataSource = masterGrpSessionList[1];
                    YesterdaysReadingSessionRepeater.DataBind();
                    TodaysReadingSessionRepeater.DataSource = masterGrpSessionList[0];
                    TodaysReadingSessionRepeater.DataBind();
                    if (TodaysReadingSessionRepeater.Items.Count == 0)
                        ScriptManager.RegisterStartupScript(Page, GetType(), "HideTodaysSession", "<script>$('#TSessionDiv').parent().css('display','none')</script>", false);
                    if (YesterdaysReadingSessionRepeater.Items.Count == 0)
                        ScriptManager.RegisterStartupScript(Page, GetType(), "HideYesterdaysSession", "<script>$('#YSessionDiv').parent().css('display','none')</script>", false);
                    if (Last7DaysReadingSessionRepeater.Items.Count == 0)
                        ScriptManager.RegisterStartupScript(Page, GetType(), "HideL7Session", "<script>$('#L7SessionDiv').parent().css('display','none')</script>", false);

                    if (TodaysIndependentRecordings.Items.Count == 0 && YesterDayIndependentRecordings.Items.Count == 0 && Last7DaysIndependentRecordings.Items.Count == 0 && RestMonthRepeater.Items.Count == 0 && TodaysReadingSessionRepeater.Items.Count == 0 && YesterdaysReadingSessionRepeater.Items.Count == 0 && Last7DaysReadingSessionRepeater.Items.Count == 0)
                    {
                        Message1.Text = Constants.NOREADHISTORYINFO;
                        Message2.Style.Add("Display", "none");
                        MessageOuterDiv.Style.Add("Display", "block");
                    }
                    
                    if (StudentRepeater.Items.Count == 0)
                    {
                        Message2.Text = Constants.NOSTUDENTINFO;
                        StudentDiv.Style.Add("Display", "none");

                    }
                    
                    if(Message1.Text =="" && Message2.Text=="")
                    {
                        MessageOuterDiv.Style.Add("Display", "none");
                    }



                }
                catch (Exception ex)
                {
                    //this.Messages.ShowError(ex.Message);
                    LogFileWrite(ex);
                }
                //}
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
                Label totalHoursLabel = (Label)e.Item.FindControl("BookOpenedMin");
                Label hourspan = (Label)e.Item.FindControl("hourspan");
                Literal productId = (Literal)e.Item.FindControl("ProductId");
                Image classImage = (Image)e.Item.FindControl("ClassImage");
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImg");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                HiddenField SessType = (HiddenField)e.Item.FindControl("SessTypeVal");
                if (SessType.Value == "Independent")
                    classImage.ImageUrl = "~/Portals/0/images/students.png";
                else
                    classImage.ImageUrl = "~/Portals/0/images/groups.png";
                Int32 totalhours = Int32.Parse(totalHoursLabel.Text.Trim());
                StringBuilder sb = new StringBuilder();

                if (totalhours >= 3600)
                {
                    string s = (totalhours / 3600).ToString() + "." + ((totalhours % 3600 / 60).ToString("00"));
                    sb.Append(s);
                    hourspan.Text = "hrs";
                }
                else
                {
                    string s = (totalhours / 60).ToString() + "." + ((totalhours % 60).ToString("00"));
                    sb.Append(s);
                    hourspan.Text = "mins";
                }

                totalHoursLabel.Text = sb.ToString();
                Literal literal = (Literal)e.Item.FindControl("TodaysIndMemberID");
                Repeater gridView = (Repeater)e.Item.FindControl("TodayIndeVideoRepeater");
                //Label label = (Label)e.Item.FindControl("WordCount");
                Label BookOpenedMinsLabel = (Label)e.Item.FindControl("BookOpenedMinsLabel");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");

                List<Components.Groups> groupcollection = GroupController.Instance.GetRecordingHistoryByGroup(groupId.Value, int.Parse(literal.Text), int.Parse(productId.Text.Trim()), DateTime.Now.ToString("MMMM"));
                //label.Text = (from collection in groupcollection select collection.WordCount).FirstOrDefault().ToString() == string.Empty ? "0" : (from collection in groupcollection select collection.WordCount).FirstOrDefault().ToString();
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
                });
                groupcollection = groupcollection.Where(u => u.BookOpenAt.ToShortDateString() == DateTime.Now.ToShortDateString() && u.BookOpenAt.ToString() == BookOpenTime.Text).ToList();
                if (groupcollection.Count == 0)
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    recordpanel.Style.Add("display", "none");
                    BookOpenedMinsLabel.Text = "(Book Opened " + totalHoursLabel.Text + " " + hourspan.Text + ")";
                }
                else
                {
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
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
                Label totalHoursLabel = (Label)e.Item.FindControl("BookOpenedMin");
                Label hourspan = (Label)e.Item.FindControl("hourspan");
                Literal productId = (Literal)e.Item.FindControl("ProductId");
                Image classImage = (Image)e.Item.FindControl("ClassImage");
                HiddenField SessType = (HiddenField)e.Item.FindControl("SessTypeVal");
                if (SessType.Value == "Independent")
                    classImage.ImageUrl = "~/Portals/0/images/students.png";
                else
                    classImage.ImageUrl = "~/Portals/0/images/groups.png";
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImg");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                Int32 totalhours = Int32.Parse(totalHoursLabel.Text.Trim());
                StringBuilder sb = new StringBuilder();

                if (totalhours >= 3600)
                {
                    string s = (totalhours / 3600).ToString() + "." + ((totalhours % 3600 / 60).ToString("00"));
                    sb.Append(s);
                    hourspan.Text = "hrs";
                }
                else
                {
                    string s = (totalhours / 60).ToString() + "." + ((totalhours % 60).ToString("00"));
                    sb.Append(s);
                    hourspan.Text = "mins";
                }

                totalHoursLabel.Text = sb.ToString();
                Literal literal = (Literal)e.Item.FindControl("YesterdaysIndMemberID");
                Repeater gridView = (Repeater)e.Item.FindControl("YesterdayIndVideoRepeater");
                //Label label = (Label)e.Item.FindControl("WordCount");
                Label BookOpenedMinsLabel = (Label)e.Item.FindControl("BookOpenedMinsLabel");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                List<Components.Groups> groupcollection = GroupController.Instance.GetRecordingHistoryByGroup(groupId.Value, int.Parse(literal.Text), int.Parse(productId.Text.Trim()), DateTime.Now.AddDays(-1).ToString("MMMM"));
                //label.Text = (from collection in groupcollection select collection.WordCount).FirstOrDefault().ToString() == string.Empty ? "0" : (from collection in groupcollection select collection.WordCount).FirstOrDefault().ToString();
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
                });
                groupcollection = groupcollection.Where(u => u.BookOpenAt.ToShortDateString() == DateTime.Now.AddDays(-1).ToShortDateString() && u.BookOpenAt.ToString() == BookOpenTime.Text).ToList();
                if (groupcollection.Count == 0)
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    recordpanel.Style.Add("display", "none");
                    BookOpenedMinsLabel.Text = "(Book Opened " + totalHoursLabel.Text + " " + hourspan.Text + ")";
                }
                else
                {
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
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
        protected void Last7DaysIndependentRecordings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Label totalHoursLabel = (Label)e.Item.FindControl("BookOpenedMin");
                Label hourspan = (Label)e.Item.FindControl("hourspan");
                Literal productId = (Literal)e.Item.FindControl("ProductId");
                Image classImage = (Image)e.Item.FindControl("ClassImage");
                HiddenField SessType = (HiddenField)e.Item.FindControl("SessTypeVal");
                if (SessType.Value == "Independent")
                    classImage.ImageUrl = "~/Portals/0/images/students.png";
                else
                    classImage.ImageUrl = "~/Portals/0/images/groups.png";
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImg");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                Int32 totalhours = Int32.Parse(totalHoursLabel.Text.Trim());
                StringBuilder sb = new StringBuilder();

                if (totalhours >= 3600)
                {
                    string s = (totalhours / 3600).ToString() + "." + ((totalhours % 3600 / 60).ToString("00"));
                    sb.Append(s);
                    hourspan.Text = "hrs";
                }
                else
                {
                    string s = (totalhours / 60).ToString() + "." + ((totalhours % 60).ToString("00"));
                    sb.Append(s);
                    hourspan.Text = "mins";
                }

                totalHoursLabel.Text = sb.ToString();
                Literal literal = (Literal)e.Item.FindControl("LastSevendaysIndMemberID");
                Repeater gridView = (Repeater)e.Item.FindControl("LastSevenDaysVideoRepeater");
                //Label label = (Label)e.Item.FindControl("WordCount");
                Label BookOpenedMinsLabel = (Label)e.Item.FindControl("BookOpenedMinsLabel");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                List<Components.Groups> groupcollection = GroupController.Instance.GetLastSevenDaysRecordings(groupId.Value, int.Parse(literal.Text.Trim()), int.Parse(productId.Text.Trim()), DateTime.Now.AddDays(-9), DateTime.Now.AddDays(-2));
               // label.Text = (from collection in groupcollection select collection.WordCount).FirstOrDefault().ToString() == string.Empty ? "0" : (from collection in groupcollection select collection.WordCount).FirstOrDefault().ToString();
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
                });
                groupcollection = groupcollection.Where(u => u.BookOpenAt.ToString() == BookOpenTime.Text).ToList();
                if (groupcollection.Count == 0)
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    recordpanel.Style.Add("display", "none");
                    BookOpenedMinsLabel.Text = "(Book Opened " + totalHoursLabel.Text + " " + hourspan.Text + ")";
                }
                else
                {
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }

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
        protected void RestIndependentHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Label totalHoursLabel = (Label)e.Item.FindControl("BookOpenedMin");
                Label hourspan = (Label)e.Item.FindControl("hourspan");
                Literal productId = (Literal)e.Item.FindControl("ProductId");
                Image classImage = (Image)e.Item.FindControl("ClassImage");
                HiddenField SessType = (HiddenField)e.Item.FindControl("SessTypeVal");
                if (SessType.Value == "Independent")
                    classImage.ImageUrl = "~/Portals/0/images/students.png";
                else
                    classImage.ImageUrl = "~/Portals/0/images/groups.png";
                Image BookCoverImage = (Image)e.Item.FindControl("BookCoverImg");
                BookCoverImage.ImageUrl = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], BookCoverImage.ImageUrl);
                Int32 totalhours = Int32.Parse(totalHoursLabel.Text.Trim());
                StringBuilder sb = new StringBuilder();

                if (totalhours >= 3600)
                {
                    string s = (totalhours / 3600).ToString() + "." + ((totalhours % 3600 / 60).ToString("00"));
                    sb.Append(s);
                    hourspan.Text = "hrs";
                }
                else
                {
                    string s = (totalhours / 60).ToString() + "." + ((totalhours % 60).ToString("00"));
                    sb.Append(s);
                    hourspan.Text = "mins";
                }

                totalHoursLabel.Text = sb.ToString();
                Literal literal = (Literal)e.Item.FindControl("RestIndMemberID");
                Repeater gridView = (Repeater)e.Item.FindControl("RestIndenpVideoRepeater");
                //Label label = (Label)e.Item.FindControl("WordCount");
                Label BookOpenedMinsLabel = (Label)e.Item.FindControl("BookOpenedMinsLabel");
                Label monthLabel = (Label)e.Item.Parent.Parent.FindControl("RestMonthLabel");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                List<Components.Groups> groupcollection = GroupController.Instance.GetRecordingHistoryByGroup(groupId.Value, int.Parse(literal.Text), int.Parse(productId.Text.Trim()), monthLabel.Text);
                //label.Text = (from collection in groupcollection select collection.WordCount).FirstOrDefault().ToString() == string.Empty ? "0" : (from collection in groupcollection select collection.WordCount).FirstOrDefault().ToString();
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
                });
                groupcollection = groupcollection.Where(u => u.BookOpenAt.ToString("MMMM") == monthLabel.Text && u.BookOpenAt.ToString() == BookOpenTime.Text).ToList();
                if (groupcollection.Where(u => u.BookOpenAt.ToString("MMMM") == monthLabel.Text).ToList().Count == 0)
                {
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    recordpanel.Style.Add("display", "none");
                    BookOpenedMinsLabel.Text = "(Book Opened " + totalHoursLabel.Text + " " + hourspan.Text + ")";
                }
                else
                {
                   
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
                }
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
        protected void RestMonthRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Label label = (Label)e.Item.FindControl("RestMonthLabel");
                Repeater independentRepeater = (Repeater)e.Item.FindControl("RestIndependentHistory");
                List<Components.Groups> independentGroups = groupController.GetReadingHistoryByGroup(groupId.Value, label.Text);
                if (DateTime.Now.ToString("MMMM") == label.Text)
                {
                    independentGroups = independentGroups.Where(u => u.BookOpenAt < DateTime.Now.AddDays(-9) && !(u.BookOpenAt > DateTime.Now.AddDays(-9))).ToList();
                }
                independentRepeater.DataSource = independentGroups;
                independentRepeater.DataBind();
                Repeater sessionRepeater = (Repeater)e.Item.FindControl("ReadingSessionRepeater");
                List<Components.Groups> groupSession = groupController.GetReadingSessionByGroup(groupId.Value, label.Text);
                sessionRepeater.DataSource = groupSession;//.Take(1);
                sessionRepeater.DataBind();
                
                if (sessionRepeater.Items.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "HideSsionRepeater" + RMCount, "<script>Hide(RMSession"+RMCount+")</script>", false);
                    if ( independentRepeater.Items.Count == 0)
                    {
                        e.Item.Visible = false;
                    }
                }
                if (DateTime.Now.ToString("MMMM") == label.Text)
                {
                    label.Text = "Rest of the month";
                }
                RMCount++;
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
        protected void Last7DaysReadingSessionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                StringBuilder output = new StringBuilder();
                Label groupId = (Label)e.Item.FindControl("Last7DaysGpId");
                Button sessionSK = (Button)e.Item.FindControl("Last7DayReviewButton");
                
                HtmlGenericControl ul = (HtmlGenericControl)e.Item.FindControl("L7WrapList");
                List<string> WrapperList = groupController.GetReadingSessionWrapperBySectionCat(int.Parse(groupId.Text.Trim()), "Last7Days", int.Parse(sessionSK.CommandArgument));
                if (WrapperList != null && WrapperList.Count != 0)
                {
                    string ImageUrlPath;
                    foreach (string wrapperName in WrapperList)
                    {
                        ImageUrlPath = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], wrapperName);
                        output.AppendFormat("<li><img  style='width: 148px; height: 122px;' src='{0}' class='GpYstrDyReptcontentbookimg2Div'/></li>", ImageUrlPath);

                    }
                }
                ul.InnerHtml = output.ToString();
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
        protected void TodaysReadingSessionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                StringBuilder output = new StringBuilder();
                Label groupId = (Label)e.Item.FindControl("TodaysGpId");
                Button sessionSK = (Button)e.Item.FindControl("TodaysReviewButton");

                HtmlGenericControl ul = (HtmlGenericControl)e.Item.FindControl("TWrapList");
                List<string> WrapperList = groupController.GetReadingSessionWrapperBySectionCat(int.Parse(groupId.Text.Trim()), "Todays", int.Parse(sessionSK.CommandArgument));
                if (WrapperList != null && WrapperList.Count != 0)
                {
                    string ImageUrlPath;
                    foreach (string wrapperName in WrapperList)
                    {
                        ImageUrlPath = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], wrapperName);
                        output.AppendFormat("<li><img  style='width: 148px; height: 122px;' src='{0}' class='GpYstrDyReptcontentbookimg2Div'/></li>", ImageUrlPath);

                    }
                }
                ul.InnerHtml = output.ToString();
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
        protected void YesterdaysReadingSessionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                StringBuilder output = new StringBuilder();
                Label groupId = (Label)e.Item.FindControl("YesterdaysGpId");
                Button sessionSK = (Button)e.Item.FindControl("YesterdaysReviewButton");

                HtmlGenericControl ul = (HtmlGenericControl)e.Item.FindControl("YWrapList");
                List<string> WrapperList = groupController.GetReadingSessionWrapperBySectionCat(int.Parse(groupId.Text.Trim()), "Yesterdays", int.Parse(sessionSK.CommandArgument));
                if (WrapperList != null && WrapperList.Count != 0)
                {
                    string ImageUrlPath;
                    foreach (string wrapperName in WrapperList)
                    {
                        ImageUrlPath = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], wrapperName);
                        output.AppendFormat("<li><img  style='width: 148px; height: 122px;' src='{0}' class='GpYstrDyReptcontentbookimg2Div'/></li>", ImageUrlPath);

                    }
                }
                ul.InnerHtml = output.ToString();
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
        protected void ReadingSessionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                StringBuilder output = new StringBuilder();
                Label groupId = (Label)e.Item.FindControl("GroupId");
                Button sessionSK = (Button)e.Item.FindControl("ReviewButton");
                Label month = (Label)((RepeaterItem)e.Item.Parent.NamingContainer).FindControl("RestMonthLabel");
                HtmlGenericControl ul = (HtmlGenericControl)e.Item.FindControl("WrpList");
                List<string> WrapperList = groupController.GetReadingSessionWrapper(int.Parse(groupId.Text.Trim()), month.Text.Trim(), int.Parse(sessionSK.CommandArgument));
                if (WrapperList != null && WrapperList.Count != 0)
                {
                    string ImageUrlPath;
                    foreach (string wrapperName in WrapperList)
                    {
                        ImageUrlPath = string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], wrapperName);
                        output.AppendFormat("<li><img  style='width: 148px; height: 122px;' src='{0}' class='GpYstrDyReptcontentbookimg2Div'/></li>", ImageUrlPath);

                    }
                }
                ul.InnerHtml = output.ToString();
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
        protected void WordLogButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=words");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListenButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=recordings");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReviewButton_Click(object sender, EventArgs e)
        {
            var argValue = ((Button)sender).CommandArgument;
            Button button = (sender as Button);
            string GetAllActiveSessionsCacheKey = string.Format("GetAllActiveSessions{0}", 0);
            DataCache.RemoveCache(GetAllActiveSessionsCacheKey);
            //Get the command argument
            Session["EditSelectedId"] = Convert.ToInt32(button.CommandArgument);
            Response.Redirect(Globals.NavigateURL(GetTabID(SessionsModule)) + "?pagename=" + Sessionprofile);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClassProfileButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(GetTabID(StudentsModule)) + "?pagename=" + Studentprofile + "&username="+(sender as Button).CommandArgument);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void StudentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label label = (Label)e.Item.FindControl("StudentNameLabel");
            label.Text = label.Text.Length > 20 ? label.Text.Substring(0, 20) + "..." : label.Text;
            Label StudentLoginNamelabel = (Label)e.Item.FindControl("StudentLoginName");
            StudentLoginNamelabel.Text = StudentLoginNamelabel.Text.Length > 20 ? StudentLoginNamelabel.Text.Substring(0, 20) + "..." : StudentLoginNamelabel.Text;
        }
       
        #region Private Members
        private List<_Students> _studentList = new List<_Students>();

        private SortDirection StudentSortDirection
        {
            get
            {
                if (Session["StudentSortDirection"] == null)
                    Session["StudentSortDirection"] = SortDirection.Ascending;
                return (SortDirection)Session["StudentSortDirection"];
            }
            set { Session["StudentSortDirection"] = value; }
        }

        private string StudentSortExpression
        {
            get
            {
                if (Session["StudentSortExpression"] == null)
                    Session["StudentSortExpression"] = "StudentName";
                return (string)Session["StudentSortExpression"];
            }
            set { Session["StudentSortExpression"] = value; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReadingLevelButton_Click(object sender, EventArgs e)
        {

            StudentSortExpression = "PMReading";

            if (ReadingLevelButton.CommandName == "Ascending")
            {
                StudentSortDirection = SortDirection.Descending;
                ReadingLevelButton.CommandName = "Descending";
                ReadingLevelButton.CssClass = String.Join(" ", ReadingLevelButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).Concat(new string[] { "sortimage" }).ToArray());
            }
            else
            {
                StudentSortDirection = SortDirection.Ascending;
                ReadingLevelButton.CommandName = "Ascending";
                ReadingLevelButton.CssClass = String.Join(" ", ReadingLevelButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).ToArray());
            }

            SortAndBind();
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
                groupController.GetSearchStudentInGroup(EditGroupId.Value != -1 ? EditGroupId.Value : EditClassId.Value, SearchTextBox.Value.Replace(", ", ",").Trim() == string.Empty ? "%" : SearchTextBox.Value.Replace(", ", ",").Trim(), LoginName);
                SortAndBind();
                
                ScriptManager.RegisterStartupScript(Page, GetType(), "HideSearchProgress", "<script>EndUpdateProgress()</script>", false);
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
        private void SortAndBind()
        {
            try
            {
                _studentList = groupController.GetStudentByGroup(EditGroupId.Value != -1 ? EditGroupId.Value : EditClassId.Value, LoginName);
                studentCount = _studentList.Count;
                _studentList.Sort();
                if (_studentList.Count > 0)
                {
                    switch (StudentSortExpression)
                    {
                        case "StudentName":
                            _studentList.Sort();
                            break;
                        case "PMReading":
                            _studentList.Sort(_Students.StudentReadingLevelSorter);
                            break;
                        default:
                            break;
                    }

                    if (StudentSortDirection == SortDirection.Descending)
                    {
                        _studentList.Reverse();
                    }

                }
                else
                {
                    // StudentContentPanel.Visible = false;
                }
                FillStudentsList(_studentList);
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
        /// <param name="_studentList"></param>
        private void FillStudentsList(List<_Students> _studentList)
        {
            if (_studentList != null)
            {
                StudentRepeater.DataSource = _studentList;
                StudentRepeater.DataBind();
                CheckALLUpdatePanel.Update();
                StudentRepeaterUpdatePanel.Update();
                SearchUpdatePanel.Update();
                if (_studentList.Count > 0)
                    ScriptManager.RegisterStartupScript(Page, GetType(), "showleftline", "<script>showleftline(1)</script>", false);
                else
                    ScriptManager.RegisterStartupScript(Page, GetType(), "showleftline", "<script>showleftline(0)</script>", false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SortingButton_Click(object sender, EventArgs e)
        {
            StudentSortExpression = "StudentName";

            if (SortingButton.CommandName == "Ascending")
            {
                StudentSortDirection = SortDirection.Descending;
                SortingButton.CommandName = "Descending";
                SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).Concat(new string[] { "sortimage" }).ToArray());
            }
            else
            {
                StudentSortDirection = SortDirection.Ascending;
                SortingButton.CommandName = "Ascending";
                SortingButton.CssClass = String.Join(" ", SortingButton.CssClass.Split(' ').Except(new string[] { "", "sortimage" }).ToArray());
            }
            SortAndBind();
        }
    }
}