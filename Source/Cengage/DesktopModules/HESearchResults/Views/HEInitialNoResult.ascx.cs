using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.HESearchResults.Components.Controller;

namespace DotNetNuke.Modules.HESearchResults.Views
{
    public partial class HEInitialNoResult : HESearchResultsModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string corrected = Null.SetNullString(Session["corrected"]),division=Request.QueryString["division"];
                if (Null.SetNullString(corrected) != string.Empty && Request.QueryString["q"] != corrected.ToString())
                {
                    Visitor visitor = new Visitor();
                    //Check whether logged in user info available in session
                    if (Session["UserInfo"] != null)
                        visitor = (Visitor)(Session["UserInfo"]);
                    if (HESearchResultController.Instance.CheckForHEDidYouMeanWordExist(corrected, division, visitor.CountryCode))
                    {
                        DidYouMeanLink.Text = Session["corrected"].ToString();
                        DidYouMeanLink.NavigateUrl = string.Concat("/search?q=", Session["corrected"].ToString(), "&division=", division, "&dcheck=y");
                    }
                    else
                        didyoumeanPara.Visible = false;
                }
                else { didyoumeanPara.Visible = false; }
                SearchTextLbl.Text = Request.QueryString["q"];
                if (Null.SetNullString(Request.QueryString["st"]) == "ad")
                {
                    //ForLabel.Visible = false;
                    SearchTextLbl.Visible = false;
                }                
                //CmsPgResPlaceHldr.Controls.Add(Page.LoadControl("DesktopModules/HESearchResults/Views/HECMSResults.ascx"));
            }
            catch (Exception ex) { LogFileWrite(ex); }
             
        }
    }
}