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

using System;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Modules.SavedSearch.Conrollers;
using System.Data.SqlClient;
using System.Data;

namespace DotNetNuke.Modules.SavedSearch
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditSavedSearch class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from SavedSearchModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : SavedSearchModuleBase
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
                if (!IsPostBack)
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());

                    SqlDataReader reader = SavedSearchController.Instance.GetModuleInformation(this.ModuleId);
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    if (dt.Rows.Count != 0)
                    {
                        txtTitle.Value = dt.Rows[0]["Content"].ToString().Split('Ñ')[0];
                        chkModuleWidth.SelectedValue = dt.Rows[0]["Content"].ToString().Split('Ñ')[1];
                        txtDefaultDisplayCount.Value = dt.Rows[0]["Content"].ToString().Split('Ñ')[2];
                    }
                    else
                    {
                        string build = txtTitle.Value + "Ñ" + chkModuleWidth.SelectedValue + "Ñ" + txtDefaultDisplayCount.Value;
                        int moduleId = this.ModuleId;
                        bool insertresult = SavedSearchController.Instance.InsertModuleInformation(moduleId, build, UserId);
                    }
                }
                

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {

            ViewState["update"] = Session["update"];
        }
        protected void cmdSave_Click(object sender, EventArgs e)
        {
            if (Session["update"] != null)
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    string build = txtTitle.Value + "Ñ" + chkModuleWidth.SelectedValue + "Ñ" + txtDefaultDisplayCount.Value;
                    int moduleId = this.ModuleId;
                    SqlDataReader reader = SavedSearchController.Instance.GetModuleInformation(moduleId);
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    if (dt.Rows.Count != 0)
                    {
                        if (txtTitle.Value != dt.Rows[0]["Content"].ToString().Split('Ñ')[0] || chkModuleWidth.SelectedValue != dt.Rows[0]["Content"].ToString().Split('Ñ')[2] || txtDefaultDisplayCount.Value != dt.Rows[0]["Content"].ToString().Split('Ñ')[3])
                        {
                            if (dt.Rows.Count != 0)
                            {
                                bool updateresult = SavedSearchController.Instance.UpdateModuleInformation(moduleId, build, UserId);
                            }

                        }
                    }
                    else
                    {
                        bool insertresult = SavedSearchController.Instance.InsertModuleInformation(moduleId, build, UserId);
                    }
                }
            }
        }
        #endregion

        

    }

}