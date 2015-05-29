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
using DotNetNuke.Modules.InformationBanner.Components.Controllers;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;

namespace DotNetNuke.Modules.InformationBanner
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditInformationBanner class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from InformationBannerModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : InformationBannerModuleBase
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
                lblError.Text = "";
                if (!IsPostBack)
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    SqlDataReader reader = InformationBannerController.Instance.GetAlerts("edit", System.DateTime.Now.ToString(),string.Empty);
                   // DataTable dt = new DataTable();
                    //dt.Load(reader);
                    if (reader.HasRows)//(dt.Rows.Count != 0)
                    {
                        grdAlert.DataSource = reader;//InformationBannerController.Instance.GetAlerts("edit", System.DateTime.Now.ToString(), string.Empty);
                        grdAlert.DataBind();
                    }
                    else
                    {
                        DataTable dt1 = new DataTable();

                        // Define all of the columns you are binding in your GridView
                        dt1.Columns.Add("Alert_ID");
                        dt1.Columns.Add("ERROR_MESSAGE");
                        dt1.Columns.Add("ErrorType");
                        dt1.Columns.Add("Start_Date");
                        dt1.Columns.Add("End_Date");

                        DataRow dr = dt1.NewRow();
                        dr["Alert_ID"] = "";
                        dr["ERROR_MESSAGE"] = "";
                        dr["ErrorType"] = "";
                        dr["Start_Date"] = "";
                        dr["End_Date"] = "";
                        dt1.Rows.Add(dr);

                        grdAlert.DataSource = dt1;
                        grdAlert.DataBind();
                        grdAlert.Rows[0].Visible = false;
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
        void LoadGrid()
        {
            Visitor visitor = Session["UserInfo"] as Visitor;
            grdAlert.DataSource = InformationBannerController.Instance.GetAlerts("edit", System.DateTime.Now.ToString(),string.Empty);
            grdAlert.DataBind();
        }
        protected void grdAlert_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            grdAlert.EditIndex = e.NewEditIndex;
            LoadGrid();
            TextBox inputErrorMessage = (TextBox)grdAlert.Rows[e.NewEditIndex].Cells[1].Controls[1];
            DropDownList inputErrorType = (DropDownList)grdAlert.Rows[e.NewEditIndex].Cells[2].Controls[1];
            TextBox inputStartDate = (TextBox)grdAlert.Rows[e.NewEditIndex].Cells[3].Controls[1];
            TextBox inputEndDate = (TextBox)grdAlert.Rows[e.NewEditIndex].Cells[4].Controls[1];
            LinkButton inputAdd = (LinkButton)grdAlert.Rows[e.NewEditIndex].Cells[5].Controls[1];

            inputAdd.Attributes.Add("onclick", "return ValidateAdd('" + inputErrorMessage.ClientID + "','" + inputErrorType.ClientID + "','" + inputStartDate.ClientID + "','" + inputEndDate.ClientID + "');");
        }

        protected void grdAlert_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            grdAlert.EditIndex = -1;
            LoadGrid();
        }

        protected void grdAlert_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            if (Session["update"] != null)
            {
                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                Visitor visitor = Session["UserInfo"] as Visitor;
                int alertid = int.Parse(((Label)grdAlert.Rows[e.RowIndex].FindControl("lblAlertID")).Text);
                SqlDataReader reader = InformationBannerController.Instance.GetAlerts("edit", System.DateTime.Now.ToString(),string.Empty);
                DataTable dt = new DataTable();
                dt.Load(reader);
                foreach (DataRow row in dt.Rows)
                {
                    if (alertid == int.Parse(row["ALERT_ID"].ToString()))
                    {
                        if (((TextBox)grdAlert.Rows[e.RowIndex].FindControl("txtErrorMessage")).Text != row["ERROR_MESSAGE"].ToString() || ((DropDownList)grdAlert.Rows[e.RowIndex].FindControl("ddlErrorType")).SelectedValue != row["ERRORTYPE"].ToString() || ((TextBox)grdAlert.Rows[e.RowIndex].FindControl("txtStartDate1")).Text != row["START_DATE"].ToString() || ((TextBox)grdAlert.Rows[e.RowIndex].FindControl("txtEndDate1")).Text != row["END_DATE"].ToString())
                        {
                            int result = InformationBannerController.Instance.UpdateAlerts(alertid, ((TextBox)grdAlert.Rows[e.RowIndex].FindControl("txtErrorMessage")).Text, ((DropDownList)grdAlert.Rows[e.RowIndex].FindControl("ddlErrorType")).SelectedValue, DateTime.ParseExact(((TextBox)grdAlert.Rows[e.RowIndex].FindControl("txtStartDate1")).Text, "dd/MM/yyyy", null).ToString(), DateTime.ParseExact(((TextBox)grdAlert.Rows[e.RowIndex].FindControl("txtEndDate1")).Text, "dd/MM/yyyy", null).ToString());
                            if (result != 0)
                            {
                                lblError.Text = result + " row updated.";
                                grdAlert.EditIndex = -1;
                                LoadGrid();
                            }
                            else
                            {
                                lblError.Text = "Not added";
                            }
                        }
                    }
                }
            }
        }

        protected void grdAlert_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            if (Session["update"] != null)
            {

                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                int usersk = int.Parse(((Label)grdAlert.Rows[e.RowIndex].FindControl("lblAlertID")).Text);
                int result = InformationBannerController.Instance.DeleteAlerts(usersk);
                if (result == 1)
                {
                    lblError.Text = "1 row Deleted.";
                    LoadGrid();
                }
                else
                {
                    lblError.Text = "Not added";
                }
            }
        }

        protected void grdAlert_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (Session["update"] != null)
            {
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile", "<script>alink();</script>", false);
                    if (e.CommandName == "Add")
                    {
                        TextBox strtxtinputErrorMessage = (TextBox)grdAlert.FooterRow.FindControl("txtinputErrorMessage");
                        DropDownList strddlinputErrorType = (DropDownList)grdAlert.FooterRow.FindControl("ddlinputErrorType");
                        TextBox strtxtinputStartDate = (TextBox)grdAlert.FooterRow.FindControl("txtinputStartDate");
                        TextBox strtxtinputEndDate = (TextBox)grdAlert.FooterRow.FindControl("txtinputEndDate");


                        int result = InformationBannerController.Instance.AddAlerts(0, strtxtinputErrorMessage.Text, strddlinputErrorType.SelectedValue, DateTime.ParseExact(strtxtinputStartDate.Text, "dd/MM/yyyy", null).ToString(), DateTime.ParseExact(strtxtinputEndDate.Text, "dd/MM/yyyy", null).ToString());
                        if (result == 1)
                        {
                            lblError.Text = "1 row added.";
                            LoadGrid();
                        }
                        else
                        {
                            lblError.Text = "Not added";
                        }
                    }
                }
            }
        }

        protected void grdAlert_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "createProfile", "<script>alink();</script>", false);
                TextBox inputErrorMessage = (TextBox)e.Row.FindControl("txtinputErrorMessage");
                DropDownList inputErrorType = (DropDownList)e.Row.FindControl("ddlinputErrorType");
                TextBox inputStartDate = (TextBox)e.Row.FindControl("txtinputStartDate");
                TextBox inputEndDate = (TextBox)e.Row.FindControl("txtinputEndDate");
                LinkButton inputAdd = (LinkButton)e.Row.FindControl("lnkAdd");

                inputAdd.Attributes.Add("onclick", "return ValidateAdd('" + inputErrorMessage.ClientID + "','" + inputErrorType.ClientID + "','" + inputStartDate.ClientID + "','" + inputEndDate.ClientID + "');");
            }
        }


        #endregion


    }


}

