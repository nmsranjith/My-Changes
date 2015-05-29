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
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals; 
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;
using System.Collections.Generic;
using System.Data;
using DotNetNuke.Modules.eCollection_Books.Components.Modal;
using System.Web.UI.WebControls;
using System.Configuration;
using DotNetNuke.Modules.eCollection_Books.Components.Controllers;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Common;
using System.Web.UI.HtmlControls;


namespace DotNetNuke.Modules.eCollection_Books
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from eCollection_BooksModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : eCollection_BooksModuleBase, IActionable
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
                //Session["UserName"] = "ecomanz";
                //Session["Subscription"] = 4997;
                string cases = Request.QueryString["pagename"];                
                //Label pageHeader = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("lblbannertxt");
                HtmlGenericControl pageHeader = (HtmlGenericControl)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("SecondaryPageHeader"); 
                //Label schoolname = (Label)DotNetNuke.UI.Skins.Skin.GetParentSkin(this).FindControl("schoolname");
                // schoolname.Value = Null.SetNullString(Session["SchoolName"]);
                if (Session["Subscription"] == null || Session["UserName"] == null || Session["UserName"] == string.Empty)
                {
                    SubsCnt.Value = "0";
                    Response.Redirect(ConfigurationManager.AppSettings["homepage"]);
                    //Response.Redirect(Globals.NavigateURL(GetTabID(DashboardsModule)));
                }
                else
                {
                    var subsList = new List<Book>(from c in SubsList.AsEnumerable().Where(x => x.Field<int>("SubscriptionId") == int.Parse(Session["Subscription"].ToString()))
                                                   select new Book
                                                     {
                                                         SubscriptionId = c.Field<int>("SubscriptionId"),
                                                         SubscriptionName = c.Field<string>("SubscriptionText"),
                                                         FullName = c.Field<string>("SubscriptionName")
                                                     });

                    SelectedSubscription.InnerText = subsList[0].FullName == string.Empty ? subsList[0].SubscriptionName : subsList[0].FullName;
                    SubsCnt.Value = SubsList.Rows.Count.ToString();
                }
                pageHeader.InnerHtml = Localization.GetString("BooksHeader", this.LocalResourceFile); 
                eCollectionMenuPlaceHolder.Controls.Add(Page.LoadControl("controls/eCollectionControls/eCollectionMenu.ascx"));
                switch (cases)
                {

                    case BOOKSELECTIONWIZARD:
                        {
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/BookSelectionWizard.ascx"));
                            break;
                        }

                    case SEESELECTEDBOOKS:
                        {
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/SeeSelectedBooks.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/DashBoard/BookListDashBoard.ascx"));                           
                            break;
                        }
                    case BOOKLIST:
                        {
                            ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/BookList.ascx"));
                            // uncomment later // ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/BPBooksList.ascx"));
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/DashBoard/BookListDashBoard.ascx"));
                            break;
                        }
                    default:
                        {                           
                            if (GracePeriod.isAfterGracePeriod7Days)
                            {
                                ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/BookList.ascx"));
                                // uncomment later // // ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/BPBooksList.ascx"));
                            }
                            else
                            {
                                //int[] BooksCounts = BooksController.Instance.GetBooksCountForPageRedirection(Null.SetNullInteger(Session["Subscription"]));
                                int bpStatus = BooksController.Instance.GetBookPackStatus(Null.SetNullInteger(Session["Subscription"]),Null.SetNullString(Session["UserName"]));
                                switch(bpStatus)//BooksCounts[0] <= BooksCounts[1] && BooksCounts[1] == BooksCounts[2])
                                {
                                    case 1:
                                        ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/BookList.ascx"));
                                        //Response.Redirect(Globals.NavigateURL(PortalSettings.Current.ActiveTab.TabID) + "?pagename=BookList");                                       
                                        break;
                                    case 2:
                                        ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/BPBooksList.ascx"));
                                        break;                                 
                                    default:
                                        ContentPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/Books/BookPacks.ascx"));
                                        break;
                                }                               
                            }
                            FunctionalityPlaceHolder.Controls.Add(Page.LoadControl("DesktopModules/eCollection_Books/DashBoard/BookListDashBoard.ascx"));
                            break;
                        }
                }
                pageHeader.Attributes.Add("style", "display:block;");
               // schoolname.Value = eCollLoginController.Instance.GetSchoolName(LoginName);                             
            }
            catch (Exception ex) //Module failed to load
            {
                //Exceptions.ProcessModuleLoadException(this, exc);            
                LogFileWrite(ex);
            }
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
