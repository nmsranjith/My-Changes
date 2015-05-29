/*
' Copyright (c) 2012  DotNetNuke Corporation
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using DotNetNuke.Modules.SavedSearch.Conrollers;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using DotNetNuke.Instrumentation;


namespace DotNetNuke.Modules.SavedSearch
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from SavedSearchModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : SavedSearchModuleBase, IActionable
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                Visitor visitor = Session["UserInfo"] as Visitor;
                //visitr.UserID;
                if (!IsPostBack)
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    hndToggle.Value = "See all";
                }
                if (Session["UserInfo"] != null)
                    visitor = Session["UserInfo"] as Visitor;
                else
                    visitor = new Visitor();
                if (visitor.UserID != 0)
                {
                    string PageName = PortalSettings.ActiveTab.TabName;
                    string PageNameValue = "";
                    if (PageName.StartsWith("higher"))
                    {
                        PageNameValue = "higher";
                    }
                    else if (PageName.StartsWith("vpg"))
                    {
                        PageNameValue = "vocational";
                    }
                    else if (PageName.StartsWith("gale"))
                    {
                        PageNameValue = "gale";
                    }
                    else
                    {
                        PageNameValue = "";
                    }
                    SqlDataReader reader1 = SavedSearchController.Instance.GetSavedSearchName("", visitor.UserID, PageNameValue);
                    DataTable dtSavedTable = new DataTable();
                    dtSavedTable.Load(reader1);
                    hndTotalSeeAll.Value = dtSavedTable.Rows.Count.ToString();
                    DnnLog.Fatal("row count is ... " + dtSavedTable.Rows.Count);
                    if (dtSavedTable.Rows.Count==0)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile", "<script>alink();</script>", false);
                    }
                    else
                    {
                        SqlDataReader reader = SavedSearchController.Instance.GetModuleInformation(this.ModuleId);
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        if (dt.Rows.Count != 0)
                        {
                            lblTitle.Text = dt.Rows[0]["Content"].ToString().Split('Ñ')[0];
                            if (dt.Rows[0]["Content"].ToString().Split('Ñ')[1] == "2/3 Width")
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile", "<script>blink();</script>", false);
                            }
                            if (dtSavedTable.Rows.Count <= int.Parse(dt.Rows[0]["Content"].ToString().Split('Ñ')[2]))
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile3", "<script>dlink();</script>", false);
                            }
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "YourUniqueScriptKey1", "clink();", true);
                            ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile", "<script>clink();</script>", false);
                            DnnLog.Fatal("MOduleCounrt ... " + dt.Rows[0]["Content"].ToString().Split('Ñ')[2]+":"+dt.Rows.Count);
                            hndCount.Value = dt.Rows[0]["Content"].ToString().Split('Ñ')[2];
                            DnnLog.Fatal("***** -->" + hndCount.Value);
                            tempHdn.Text = dt.Rows[0]["Content"].ToString().Split('Ñ')[2];
                            DnnLog.Fatal("***** -->" + tempHdn.Text);
                            DnnLog.Fatal("***** -->" + dt.Rows[0]["Content"].ToString().Split('Ñ')[2]);
                            //hndToggle.Value = "See All";
                            LoadSavedSearch(dt.Rows[0]["Content"].ToString().Split('Ñ')[2]);
                            SShomesearchdiv.Visible = true;
                        }
                        else
                        {
                            SShomesearchdiv.Visible = false;
                           
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile3", "<script>alink();</script>", false);
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "YourUniqueScriptKey1", "alink();", true);
                    //ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile", "<script>alink();</script>", false);
                }                
            }
            catch (Exception exc) //Module failed to load
            {
                LogFileWrite(exc);
             //   Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {

            ViewState["update"] = Session["update"];
        }

        protected void YesButton_Click(object sender, EventArgs e)
        {
            DnnLog.Fatal("calledqk ... " + hndCount.Value);
            LoadSavedSearch(hndCount.Value);
            SavedSearchUpdatePanel.Update();
        }
        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Session["update"] != null)
                //{
                //if (Session["update"].ToString() == ViewState["update"].ToString())
                // {

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                var btn = (ImageButton)sender;
                var item = (RepeaterItem)btn.NamingContainer;
                var ddl = (HiddenField)item.FindControl("idHidden");

                int result = SavedSearchController.Instance.DeleteSavedSearchName(int.Parse(ddl.Value));
                DnnLog.Fatal("calledsk ... " + ddl + ":" + result);
                if (result == 1)
                {
                    LoadSavedSearch("");
                }
                //   }
                //}
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
        protected void cmdSeeAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["update"] != null)
                {
                    if (Session["update"].ToString() == ViewState["update"].ToString())
                    {
                        Visitor visitr = null;
                        if (Session["UserInfo"] != null)
                            visitr = Session["UserInfo"] as Visitor;
                        else
                            visitr = new Visitor();
                        string PageName = PortalSettings.ActiveTab.TabName;
                        string PageNameValue = "";
                        if (PageName.StartsWith("higher"))
                        {
                            PageNameValue = "higher";
                        }
                        else if (PageName.StartsWith("vpg"))
                        {
                            PageNameValue = "vocational";
                        }
                        else if (PageName.StartsWith("gale"))
                        {
                            PageNameValue = "gale";
                        }
                        else
                        {
                            PageNameValue = "";
                        }
                        if (Session["update"] != null)
                        {
                            if (Session["update"].ToString() == ViewState["update"].ToString())
                            {

                                if (hndToggle.Value == "Hide all")
                                {
                                    hndToggle.Value = "See all";
                                    cmdSeeAll.Value = "See all";
                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                    string count = "";
                                    SqlDataReader reader = SavedSearchController.Instance.GetModuleInformation(this.ModuleId);
                                    DataTable dt = new DataTable();
                                    dt.Load(reader);
                                    if (dt.Rows.Count != 0)
                                    {
                                        count = dt.Rows[0]["Content"].ToString().Split('Ñ')[2];
                                    }
                                    SearchRepeater.DataSource = SavedSearchController.Instance.GetSavedSearchName(count, visitr.UserID, PageNameValue);
                                    SearchRepeater.DataBind();
                                }
                                else
                                {
                                    hndToggle.Value = "Hide all";
                                    cmdSeeAll.Value = "Hide all";
                                    SearchRepeater.DataSource = SavedSearchController.Instance.GetSavedSearchName("", visitr.UserID, PageNameValue);
                                    SearchRepeater.DataBind();
                                }
                                SavedSearchUpdatePanel.Update();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { LogFileWrite(ex); }
        }
        #endregion

        #region Optional Interfaces

        public ModuleActionCollection ModuleActions
        {
            get
            {
                ModuleActionCollection Actions = new ModuleActionCollection();
                Actions.Add(GetNextActionID(), Localization.GetString("EditModule", this.LocalResourceFile), "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true, false);
                return Actions;
            }
        }

        #endregion


        void LoadSavedSearch(string count)
        {
            try
            {
                Visitor visitr = null;
                if (Session["UserInfo"] != null)
                    visitr = Session["UserInfo"] as Visitor;
                else
                    visitr = new Visitor();
                string PageName = PortalSettings.ActiveTab.TabName;
                string PageNameValue = "";
                if (PageName.StartsWith("higher"))
                {
                    PageNameValue = "higher";
                }
                else if (PageName.StartsWith("vpg"))
                {
                    PageNameValue = "vocational";
                }
                else if (PageName.StartsWith("gale"))
                {
                    PageNameValue = "gale";
                }
                else
                {
                    PageNameValue = "";
                }


                SearchRepeater.DataSource = SavedSearchController.Instance.GetSavedSearchName(count, visitr.UserID, PageNameValue);
                SearchRepeater.DataBind();


            }

            catch (Exception ex) { LogFileWrite(ex); }
        }
    }

}
