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
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Localization;


namespace DotNetNuke.Modules.HESearchResults
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from HESearchResultsModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : HESearchResultsModuleBase, IActionable
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
                string cases = Null.SetNullString(Request.QueryString["division"]);
                switch (cases.ToLower())
                {
                    case "higher":
                        SearchResultsPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/HEProductResults.ascx"));
                        break;
                    case "primary":
                        SearchResultsPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsProductResults.ascx"));
                        break;
                    case "secondary":
                        SearchResultsPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/SchoolsProductResults.ascx"));
                        break;
                    case "vocational":
                        SearchResultsPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/HEProductResults.ascx"));
                        break;
                    case "gale":
                        SearchResultsPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/GaleProductResults.ascx"));
                        break;
                    default:
                        Response.Redirect("404error.aspx");
                        break;
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

    }

}