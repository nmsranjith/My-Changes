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
using System.Collections.Generic;
using System.Data.SqlClient;
using DotNetNuke.Modules.LibraryResources.Components.Controller;
using DotNetNuke.Modules.LibraryResources.Components.Modal;
using DotNetNuke.Services.Exceptions;

namespace DotNetNuke.Modules.LibraryResources
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The EditLibraryResources class is used to manage content
    /// 
    /// Typically your edit control would be used to create new content, or edit existing content within your module.
    /// The ControlKey for this control is "Edit", and is defined in the manifest (.dnn) file.
    /// 
    /// Because the control inherits from LibraryResourcesModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class Edit : LibraryResourcesModuleBase
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
            SqlDataReader reader = null;
            try
            {
                if (!IsPostBack)
                {
                    reader = LibraryResourcesController.Instance.GetLibraryResources();
                    reader.NextResult(); reader.NextResult();
                    while (reader.Read())
                    {
                        switch (reader["LIBRARY_TYPE"].ToString())
                        {
                            case "State, National & Public Libraries":
                                StateDescriptionTxt.Text = reader["DESCRIPTION"].ToString();
                                StateUrlTxt.Text = reader["URL"].ToString();
                                StateDpn.Value = reader["SEQ_NUMBER"].ToString();
                                break;
                            case "University, TAFE. RTO Libraries":
                                UnivDescriptionTxt.Text = reader["DESCRIPTION"].ToString();
                                UnivUrlTxt.Text = reader["URL"].ToString();
                                UniversityDpn.Value = reader["SEQ_NUMBER"].ToString();
                                break;
                            case "School Library":
                                SchoolDescriptionTxt.Text = reader["DESCRIPTION"].ToString();
                                SchoolUrlTxt.Text = reader["URL"].ToString();
                                SchoolDpn.Value = reader["SEQ_NUMBER"].ToString();
                                break;
                            case "Special Library":
                                SpecialDescriptionTxt.Text = reader["DESCRIPTION"].ToString();
                                SpecialUrlTxt.Text = reader["URL"].ToString();
                                SpecialDpn.Value = reader["SEQ_NUMBER"].ToString();
                                break;
                            default:
                                break;

                        }
                    }
                } 
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// To insert/update all library resources
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void SaveButton_Clsick(object sender, System.EventArgs e)
        {
            try
            {
                List<LibraryResource> libResourcesList = new List<LibraryResource>();
                libResourcesList.Add(new LibraryResource() {
                    LibraryType = "State, National & Public Libraries",
                    Description=StateDescriptionTxt.Text,
                    Url=StateUrlTxt.Text,
                    SequenceNumber=StateDpn.Value
                });
                libResourcesList.Add(new LibraryResource()
                {
                    LibraryType = "School Library",
                    Description = SchoolDescriptionTxt.Text,
                    Url = SchoolUrlTxt.Text,
                    SequenceNumber = SchoolDpn.Value
                });
                libResourcesList.Add(new LibraryResource()
                {
                    LibraryType = "University, TAFE. RTO Libraries",
                    Description = UnivDescriptionTxt.Text,
                    Url = UnivUrlTxt.Text,
                    SequenceNumber = UniversityDpn.Value
                });
                libResourcesList.Add(new LibraryResource()
                {
                    LibraryType = "Special Library",
                    Description = SpecialDescriptionTxt.Text,
                    Url = SpecialUrlTxt.Text,
                    SequenceNumber = SpecialDpn.Value
                });

                LibraryResourcesController.Instance.SaveLibraryResources(libResourcesList);
                Response.Redirect("/gale");

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
        

        #endregion

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<LibraryResource> libResourcesList = new List<LibraryResource>();
                libResourcesList.Add(new LibraryResource()
                {
                    LibraryType = "State, National & Public Libraries",
                    Description = StateDescriptionTxt.Text,
                    Url = StateUrlTxt.Text,
                    SequenceNumber = StateDpn.Value
                });
                libResourcesList.Add(new LibraryResource()
                {
                    LibraryType = "School Library",
                    Description = SchoolDescriptionTxt.Text,
                    Url = SchoolUrlTxt.Text,
                    SequenceNumber = SchoolDpn.Value
                });
                libResourcesList.Add(new LibraryResource()
                {
                    LibraryType = "University, TAFE. RTO Libraries",
                    Description = UnivDescriptionTxt.Text,
                    Url = UnivUrlTxt.Text,
                    SequenceNumber = UniversityDpn.Value
                });
                libResourcesList.Add(new LibraryResource()
                {
                    LibraryType = "Special Library",
                    Description = SpecialDescriptionTxt.Text,
                    Url = SpecialUrlTxt.Text,
                    SequenceNumber = SpecialDpn.Value
                });

                LibraryResourcesController.Instance.SaveLibraryResources(libResourcesList);

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

    }

}