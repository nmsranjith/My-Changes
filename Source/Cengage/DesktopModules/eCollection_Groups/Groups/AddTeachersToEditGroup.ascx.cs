using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DotNetNuke.Modules.eCollection_Groups.Components.Common;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Modules.eCollection_Groups.Components;
using System.Text;

namespace DotNetNuke.Modules.eCollection_Groups.Groups
{
    public partial class AddTeachersToEditGroup : eCollection_GroupsModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.Cookies["SubscriptionId"].Value != null)
                {
                    //StudentRepeater.DataSource = GroupController.Instance.GetTeachersbySubscription(HttpContext.Current.Request.Cookies["SubscriptionId"].Value, LoginName);
                    //StudentRepeater.DataBind();
                }
                BuildSelectedItems();
            }
        }
        private void BuildSelectedItems()
        {
            try
            {
                StringBuilder output = new StringBuilder();
                RepeaterItemCollection myItemCollection = StudentRepeater.Items;
                if (TeachersList != null && TeachersList.Count != 0)
                {
                    foreach (IDCollection idCollection in TeachersList)
                    {
                        output.AppendFormat("<li class=\"SelectedItem SelectedTeacher\"><span>{0}</span><span title=\'UserID\' style=\'display:none\'>{1}</span><a onclick='javascript:Remove(this)'>x</a></li>", idCollection.Text, idCollection.Id);
                        SelectedValueTextBox.Text += idCollection.Id + ",";
                    }
                    if (SelectedValueTextBox.Text.Length > 0)
                    {
                        //SelectedValueTextBox.Text = SelectedValueTextBox.Text.Trim().Remove(SelectedValueTextBox.Text.Length - 1, 1);
                        SelectedStudentList.InnerHtml = output.ToString();
                    }
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
        protected void AllOtherGroupButton_Click(object sender, EventArgs e)
        {
            AllotherGroupDivBtn.Style.Add("display", "none");
            if (HttpContext.Current.Request.Cookies["SubscriptionId"].Value != null)
            {
                //StudentRepeater.DataSource = GroupController.Instance.GetTeachersbySubscription(HttpContext.Current.Request.Cookies["SubscriptionId"].Value, LoginName);
                //StudentRepeater.DataBind();
            }
        }

        protected void Backtocreategroupbtn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID));
        }
        protected void BacktoCreateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                List<IDCollection> selectedStudentList = new List<IDCollection>();
                RepeaterItemCollection myItemCollection = StudentRepeater.Items;
                for (int index = 0; index < myItemCollection.Count; index++)
                {
                    HtmlInputCheckBox checkbox = (HtmlInputCheckBox)myItemCollection[index].Controls[0];
                    if (checkbox.Checked)
                    {
                        IDCollection teacherIDCollection = new IDCollection();
                        teacherIDCollection.Text = (myItemCollection[index].Controls[2] as Label).Text;
                        teacherIDCollection.Id = int.Parse((myItemCollection[index].Controls[1] as Label).Text);
                        selectedStudentList.Add(teacherIDCollection);
                    }
                    {
                        string[] addedStudent = SelectedValueTextBox.Text.Trim().Split(',');
                        foreach (string addStudentId in addedStudent)
                        {
                            if (addStudentId == (myItemCollection[index].Controls[1] as Label).Text)
                            {
                                DotNetNuke.Modules.eCollection_Groups.Components.Common.IDCollection addedStudentCollection = new DotNetNuke.Modules.eCollection_Groups.Components.Common.IDCollection();
                                addedStudentCollection.Text = (myItemCollection[index].Controls[2] as Label).Text;
                                addedStudentCollection.Id = int.Parse((myItemCollection[index].Controls[1] as Label).Text);
                                selectedStudentList.Add(addedStudentCollection);
                            }
                        }
                    }

                }
                TeachersList = selectedStudentList;
                Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=" + EDITGROUP);
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
    }
}