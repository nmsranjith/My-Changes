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
using System.Text;
using System.Configuration;
using System.IO;
using System.Web.UI.HtmlControls;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="GroupRecordings" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Displays all the recordings done by all the group members excluding teachers
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class GroupRecordings : eCollection_GroupsModuleBase
    {
        GroupController groupController = GroupController.Instance;
        int? groupId = null;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
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
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=groupprofile");
            }
            if (!IsPostBack)
            {
                try
                {
                    Components.Groups groups = groupController.GetGroupProfileByGroup(groupId.Value);
                    RecordingsCount.InnerText = groups.MyRecording.ToString();
                    GroupNameLabel.Text = GroupName.ToString();
                    RestMonthRepeater.DataSource = groupController.GetMonthByGroup(groupId.Value,"RECORDINGS");
                    RestMonthRepeater.DataBind();
                    List<Components.Groups> getTodaysIndependentRecordings = groupController.GetReadingHistoryByGroup(groupId.Value,  DateTime.Now.ToString("MMMM"));
                    TodaysIndependentRecordings.DataSource = getTodaysIndependentRecordings.Where(u => u.BookOpenAt.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
                    TodaysIndependentRecordings.DataBind();
                    List<Components.Groups> getYesterdayIndependent = groupController.GetReadingHistoryByGroup(groupId.Value, DateTime.Now.AddDays(-1).ToString("MMMM"));
                    YesterDayIndependentRecordings.DataSource = getYesterdayIndependent.Where(u => u.BookOpenAt.ToShortDateString() == DateTime.Now.AddDays(-1).ToShortDateString()).ToList();
                    YesterDayIndependentRecordings.DataBind();

                    //RestIndependentHistory.DataSource = groupController.GetReadingHistoryByGroup(GroupName.ToString(), "Independent", DateTime.Today.Month.ToString());
                    //RestIndependentHistory.DataBind();
                    //RestGuidedRecordings.DataSource = groupController.GetReadingHistoryByGroup(GroupName.ToString(), "Guided", DateTime.Today.Month.ToString());
                    //RestGuidedRecordings.DataBind();

                    Last7DaysIndependentRecordings.DataSource = groupController.GetLastSevenDaysReadings(groupId.Value,  DateTime.Now.AddDays(-9), DateTime.Now.AddDays(-2));
                    Last7DaysIndependentRecordings.DataBind();


                }
                catch (Exception ex)
                {
                    this.Messages.ShowError(ex.Message);
                    LogFileWrite(ex);
                }                
            }
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
            //Get the command argument
            Session["EditSelectedId"] = Convert.ToInt32(button.CommandArgument);
            Response.Redirect(Globals.NavigateURL(GetTabID(SessionsModule)) + "?pagename=" + Sessionprofile);
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
                Literal literal = (Literal)e.Item.FindControl("TodaysIndMemberID");
                Repeater gridView = (Repeater)e.Item.FindControl("TodayIndeVideoRepeater");
                Label BookOpenedMinsLabel = (Label)e.Item.FindControl("BookOpenedMinsLabel");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                List<Components.Groups> groupcollection = GroupController.Instance.GetRecordingHistoryByGroup(groupId.Value, int.Parse(literal.Text), int.Parse(productId.Text.Trim()), DateTime.Now.ToString("MMMM"));
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
                    e.Item.Visible = false;
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    recordpanel.Style.Add("display", "none");
                    BookOpenedMinsLabel.Text = "(Book Opened " + totalHoursLabel.Text + " " + hourspan.Text + ")";
                   // Repeater reptr = (Repeater)(e.Item.Parent.NamingContainer).FindControl("TodaysIndependentRecordings");
                   // reptr.Visible = false;
                }
                else
                {
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty );
                    gridView.DataBind();
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
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RestIndependentHistory_ItemDataBound(object source, RepeaterItemEventArgs e)
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
                Label BookOpenedMinsLabel = (Label)e.Item.FindControl("BookOpenedMinsLabel");
                Label monthLabel = (Label)e.Item.Parent.Parent.FindControl("RestMonthLabel");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                List<Components.Groups> groupcollection = GroupController.Instance.GetRecordingHistoryByGroup(groupId.Value, int.Parse(literal.Text), int.Parse(productId.Text.Trim()), monthLabel.Text);
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
                    e.Item.Visible = false;
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    recordpanel.Style.Add("display", "none");
                    BookOpenedMinsLabel.Text = "(Book Opened " + totalHoursLabel.Text + " " + hourspan.Text + ")";
                   // Repeater reptr = (Repeater)(e.Item.Parent.NamingContainer).FindControl("RestIndependentHistory");
                  //  reptr.Visible = false;
                }
                else
                {
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
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
        protected void RestMonthRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                Label label = (Label)e.Item.FindControl("RestMonthLabel");
                Repeater independentRepeater = (Repeater)e.Item.FindControl("RestIndependentHistory");
                List<Components.Groups> independentGroups = groupController.GetReadingHistoryByGroup(groupId.Value, label.Text);
                if (DateTime.Now.ToString("MMMM") == label.Text)
                {
                    independentGroups = independentGroups.Where(u => u.BookOpenAt < DateTime.Now.AddDays(-9) && !(u.BookOpenAt > DateTime.Now.AddDays(-9)) ).ToList();
                }
                if (independentGroups.Count > 0)
                {
                    independentRepeater.DataSource = independentGroups;
                    independentRepeater.DataBind();
                }
                else
                {
                    independentRepeater.Visible = false;
                    (e.Item.Parent as Repeater).Visible = false;
                }
                
                Repeater sessionRepeater = (Repeater)e.Item.FindControl("ReadingSessionRepeater");
              //  sessionRepeater.DataSource = groupController.GetReadingSessionByGroup(groupId.Value, label.Text);
                //sessionRepeater.DataBind();
                if (DateTime.Now.ToString("MMMM") == label.Text)
                {
                    label.Text = "Rest of the month";
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
        protected void BackToProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=groupprofile");
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
                Label BookOpenedMinsLabel = (Label)e.Item.FindControl("BookOpenedMinsLabel");
                Label monthLabel = (Label)e.Item.Parent.Parent.FindControl("RestMonthLabel");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                List<Components.Groups> groupcollection = GroupController.Instance.GetRecordingHistoryByGroup(groupId.Value, int.Parse(literal.Text), int.Parse(productId.Text.Trim()), DateTime.Now.AddDays(-1).ToString("MMMM"));
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
                    e.Item.Visible = false;
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    recordpanel.Style.Add("display", "none");
                    BookOpenedMinsLabel.Text = "(Book Opened " + totalHoursLabel.Text + " " + hourspan.Text + ")";
                  //  Repeater reptr = (Repeater)(e.Item.Parent.NamingContainer).FindControl("YesterDayIndependentRecordings");
                  //  reptr.Visible = false;
                }
                else
                {
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
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
                Label BookOpenedMinsLabel = (Label)e.Item.FindControl("BookOpenedMinsLabel");
                Label BookOpenTime = (Label)e.Item.FindControl("BookOpenTime");
                List<Components.Groups> groupcollection = GroupController.Instance.GetLastSevenDaysRecordings(groupId.Value, int.Parse(literal.Text), int.Parse(productId.Text.Trim()), DateTime.Now.AddDays(-9), DateTime.Now.AddDays(-2));
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
                    e.Item.Visible = false;
                    Panel recordpanel = (Panel)e.Item.FindControl("RecordingButtonPanel");
                    recordpanel.Style.Add("display", "none");
                    BookOpenedMinsLabel.Text = "(Book Opened " + totalHoursLabel.Text + " " + hourspan.Text + ")";
                  //  Repeater reptr = (Repeater)(e.Item.Parent.NamingContainer).FindControl("Last7DaysIndependentRecordings");
                  //  reptr.Visible = false;
                }
                else
                {
                    gridView.DataSource = groupcollection.Where(u => u.RecordFilePath != string.Empty);
                    gridView.DataBind();
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
                    foreach (string wrapperName in WrapperList)
                    {
                        output.AppendFormat("<li><img  style='width: 82px; height: 104px;' src='/Portals/0/images/{0}' class='GpYstrDyReptcontentbookimg2Div'/></li>", wrapperName);

                    }
                }
                else
                {
                    (e.Item.Parent as Repeater).Visible = false;
                }
                ul.InnerHtml = output.ToString();
            }
            catch (Exception ex)
            {
                this.Messages.ShowError(ex.Message);
                LogFileWrite(ex);
            }   
        }

    }
}