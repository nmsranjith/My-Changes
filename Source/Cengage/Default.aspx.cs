#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2012
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
#region Usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using DotNetNuke.Application;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Host;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Entities.Users;
using DotNetNuke.Instrumentation;
using DotNetNuke.Security.Permissions;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Log.SiteLog;
using DotNetNuke.Services.Personalization;
using DotNetNuke.Services.Vendors;
using DotNetNuke.UI;
using DotNetNuke.UI.Internals;
using DotNetNuke.UI.Modules;
using DotNetNuke.UI.Skins.Controls;
using DotNetNuke.UI.Utilities;
using DotNetNuke.Web.Client.ClientResourceManagement;

using Globals = DotNetNuke.Common.Globals;

#endregion

namespace DotNetNuke.Framework
{
    using System.Configuration;
    using System.Data;
    using System.Net;
    using System.Net.Sockets;
    using Cengage.eCommerce.CountryDetection;
    using Cengage.Ecommerce.CountryDetection.Components.Controllers;
    using Cengage.Ecommerce.CengageLogin.Components.Modal;
    using Web.Client;
    using Cengage.Ecommerce.CengageLogin.Components.Controllers;
    using Cengage.eCommerce.Lib;
    using DotNetNuke.Modules.eCollection_Dashboards.Components.Controller;
    using System.Runtime.Serialization.Formatters.Binary;
    using Cengage.Ecommerce.EditableFooter.Components.Controller;

    /// -----------------------------------------------------------------------------
    /// Project	 : DotNetNuke
    /// Class	 : CDefault
    /// 
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// 	[sun1]	1/19/2004	Created
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class DefaultPage : CDefault, IClientAPICallbackEventHandler
    {
        #region
        static int CenUser = 0;
        string fileName = string.Empty;
        string sandBox = string.Empty;
        string strIpAddress = string.Empty;
        string countryName = string.Empty;
        Visitor visitor = null;
        DataSet countryRestrictionDataset;
        CountryController objController;
        private const string aus_country = "Australia";
        private const string nz_country = "NewZealand";
        string strImage = string.Empty;
        #endregion
        #region Properties

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Property to allow the programmatic assigning of ScrollTop position
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[Jon Henning]	3/23/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public int PageScrollTop
        {
            get
            {
                int pageScrollTop = Null.NullInteger;
                if (ScrollTop != null && !String.IsNullOrEmpty(ScrollTop.Value) && Regex.IsMatch(ScrollTop.Value, "^\\d+$"))
                {
                    pageScrollTop = Convert.ToInt32(ScrollTop.Value);
                }
                return pageScrollTop;
            }
            set { ScrollTop.Value = value.ToString(); }
        }

        protected string HtmlAttributeList
        {
            get
            {
                if ((HtmlAttributes != null) && (HtmlAttributes.Count > 0))
                {
                    var attr = new StringBuilder("");
                    foreach (string attributeName in HtmlAttributes.Keys)
                    {
                        if ((!String.IsNullOrEmpty(attributeName)) && (HtmlAttributes[attributeName] != null))
                        {
                            string attributeValue = HtmlAttributes[attributeName];
                            if ((attributeValue.IndexOf(",") > 0))
                            {
                                var attributeValues = attributeValue.Split(',');
                                for (var attributeCounter = 0;
                                     attributeCounter <= attributeValues.Length - 1;
                                     attributeCounter++)
                                {
                                    attr.Append(" " + attributeName + "=\"" + attributeValues[attributeCounter] + "\"");
                                }
                            }
                            else
                            {
                                attr.Append(" " + attributeName + "=\"" + attributeValue + "\"");
                            }
                        }
                    }
                    return attr.ToString();
                }
                return "";
            }
        }

        public string CurrentSkinPath
        {
            get
            {
                return ((PortalSettings)HttpContext.Current.Items["PortalSettings"]).ActiveTab.SkinPath;
            }
        }

        private bool IsPopUp
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString().Contains("popUp=true");
            }
        }

        string ImageFilePath = ConfigurationManager.AppSettings["HTTP_IMAGE"].ToString();

        #endregion

        #region IClientAPICallbackEventHandler Members

        public string RaiseClientAPICallbackEvent(string eventArgument)
        {
            var dict = ParsePageCallBackArgs(eventArgument);
            if (dict.ContainsKey("type"))
            {
                if (DNNClientAPI.IsPersonalizationKeyRegistered(dict["namingcontainer"] + ClientAPI.CUSTOM_COLUMN_DELIMITER + dict["key"]) == false)
                {
                    throw new Exception(string.Format("This personalization key has not been enabled ({0}:{1}).  Make sure you enable it with DNNClientAPI.EnableClientPersonalization", dict["namingcontainer"], dict["key"]));
                }
                switch ((DNNClientAPI.PageCallBackType)Enum.Parse(typeof(DNNClientAPI.PageCallBackType), dict["type"]))
                {
                    case DNNClientAPI.PageCallBackType.GetPersonalization:
                        return Personalization.GetProfile(dict["namingcontainer"], dict["key"]).ToString();
                    case DNNClientAPI.PageCallBackType.SetPersonalization:
                        Personalization.SetProfile(dict["namingcontainer"], dict["key"], dict["value"]);
                        return dict["value"];
                    default:
                        throw new Exception("Unknown Callback Type");
                }
            }
            return "";
        }

        #endregion

        #region Private Methods

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// - Obtain PortalSettings from Current Context
        /// - redirect to a specific tab based on name
        /// - if first time loading this page then reload to avoid caching
        /// - set page title and stylesheet
        /// - check to see if we should show the Assembly Version in Page Title 
        /// - set the background image if there is one selected
        /// - set META tags, copyright, keywords and description
        /// </remarks>
        /// <history>
        /// 	[sun1]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        private void InitializePage()
        {
            var tabController = new TabController();

            //redirect to a specific tab based on name
            if (!String.IsNullOrEmpty(Request.QueryString["tabname"]))
            {
                TabInfo tab = tabController.GetTabByName(Request.QueryString["TabName"], ((PortalSettings)HttpContext.Current.Items["PortalSettings"]).PortalId);
                if (tab != null)
                {
                    var parameters = new List<string>(); //maximum number of elements
                    for (int intParam = 0; intParam <= Request.QueryString.Count - 1; intParam++)
                    {
                        switch (Request.QueryString.Keys[intParam].ToLower())
                        {
                            case "tabid":
                            case "tabname":
                                break;
                            default:
                                parameters.Add(
                                    Request.QueryString.Keys[intParam] + "=" + Request.QueryString[intParam]);
                                break;
                        }
                    }
                    Response.Redirect(Globals.NavigateURL(tab.TabID, Null.NullString, parameters.ToArray()), true);
                }
                else
                {
                    //404 Error - Redirect to ErrorPage
                    Exceptions.ProcessHttpException(Request);
                }
            }
            if (Request.IsAuthenticated)
            {
                switch (Host.AuthenticatedCacheability)
                {
                    case "0":
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        break;
                    case "1":
                        Response.Cache.SetCacheability(HttpCacheability.Private);
                        break;
                    case "2":
                        Response.Cache.SetCacheability(HttpCacheability.Public);
                        break;
                    case "3":
                        Response.Cache.SetCacheability(HttpCacheability.Server);
                        break;
                    case "4":
                        Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
                        break;
                    case "5":
                        Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
                        break;
                }
            }

            //page comment
            if (Host.DisplayCopyright)
            {
                Comment += string.Concat(Environment.NewLine,
                                         "<!--**********************************************************************************-->",
                                         Environment.NewLine,
                                         "<!-- DotNetNuke - http://www.dotnetnuke.com                                          -->",
                                         Environment.NewLine,
                                         "<!-- Copyright (c) 2002-2012                                                          -->",
                                         Environment.NewLine,
                                         "<!-- by DotNetNuke Corporation                                                        -->",
                                         Environment.NewLine,
                                         "<!--**********************************************************************************-->",
                                         Environment.NewLine);
            }
            Page.Header.Controls.AddAt(0, new LiteralControl(Comment));

            if (PortalSettings.ActiveTab.PageHeadText != Null.NullString && !Globals.IsAdminControl())
            {
                Page.Header.Controls.Add(new LiteralControl(PortalSettings.ActiveTab.PageHeadText));
            }

            //set page title
            string strTitle = PortalSettings.PortalName;
            if (IsPopUp)
            {
                var slaveModule = UIUtilities.GetSlaveModule(PortalSettings.ActiveTab.TabID);

                //Skip is popup is just a tab (no slave module)
                if (slaveModule.DesktopModuleID != Null.NullInteger)
                {
                    var control = ModuleControlFactory.CreateModuleControl(slaveModule) as IModuleControl;
                    control.LocalResourceFile = slaveModule.ModuleControl.ControlSrc.Replace(Path.GetFileName(slaveModule.ModuleControl.ControlSrc), "") + Localization.LocalResourceDirectory + "/" +
                                                Path.GetFileName(slaveModule.ModuleControl.ControlSrc);
                    var title = Localization.LocalizeControlTitle(control);

                    strTitle += string.Concat(" > ", PortalSettings.ActiveTab.TabName);
                    strTitle += string.Concat(" > ", title);
                }
                else
                {
                    strTitle += string.Concat(" > ", PortalSettings.ActiveTab.TabName);
                }
            }
            else
            {

                foreach (TabInfo tab in PortalSettings.ActiveTab.BreadCrumbs)
                {
                    strTitle += string.Concat(" > ", tab.TabName);
                }

                //tab title override
                if (!string.IsNullOrEmpty(PortalSettings.ActiveTab.Title))
                {
                    strTitle = PortalSettings.ActiveTab.Title;
                }
            }
            Title = strTitle;

            //set the background image if there is one selected
            if (!IsPopUp && FindControl("Body") != null)
            {
                if (!string.IsNullOrEmpty(PortalSettings.BackgroundFile))
                {
                    var fileInfo = GetBackgroundFileInfo();
                    var url = FileManager.Instance.GetUrl(fileInfo);

                    ((HtmlGenericControl)FindControl("Body")).Attributes["style"] = string.Concat("background-image: url('", url, "')");
                }
            }

            //META Refresh
            if (PortalSettings.ActiveTab.RefreshInterval > 0 && Request.QueryString["ctl"] == null)
            {
                MetaRefresh.Content = PortalSettings.ActiveTab.RefreshInterval.ToString();
            }
            else
            {
                MetaRefresh.Visible = false;
            }

            //META description
            if (!string.IsNullOrEmpty(PortalSettings.ActiveTab.Description))
            {
                Description = PortalSettings.ActiveTab.Description;
            }
            else
            {
                Description = PortalSettings.Description;
            }

            //META keywords
            if (!string.IsNullOrEmpty(PortalSettings.ActiveTab.KeyWords))
            {
                KeyWords = PortalSettings.ActiveTab.KeyWords;
            }
            else
            {
                KeyWords = PortalSettings.KeyWords;
            }
            if (Host.DisplayCopyright)
            {
                KeyWords += ",DotNetNuke,DNN";
            }

            //META copyright
            if (!string.IsNullOrEmpty(PortalSettings.FooterText))
            {
                Copyright = PortalSettings.FooterText.Replace("[year]", DateTime.Now.Year.ToString());
            }
            else
            {
                Copyright = string.Concat("Copyright (c) ", DateTime.Now.Year, " by ", PortalSettings.PortalName);
            }

            //META generator
            if (Host.DisplayCopyright)
            {
                Generator = "DotNetNuke ";
            }
            else
            {
                Generator = "";
            }

            //META Robots
            if (Request.QueryString["ctl"] != null &&
                (Request.QueryString["ctl"] == "Login" || Request.QueryString["ctl"] == "Register"))
            {
                MetaRobots.Content = "NOINDEX, NOFOLLOW";
            }
            else
            {
                MetaRobots.Content = "INDEX, FOLLOW";
            }
if (PortalSettings.ActiveTab.TabName.ToLower() == "signup" || PortalSettings.ActiveTab.TabName.ToLower() == "cengageecommerce" || PortalSettings.ActiveTab.TabName.ToLower() == "countrydetection" || PortalSettings.ActiveTab.TabName.ToLower() == "advancedsearch")
            {
                MetaRobots.Content = "NOINDEX";
				RevisitMetadata.Visible = false;
            }
            //NonProduction Label Injection
            if (NonProductionVersion() && Host.DisplayBetaNotice && !IsPopUp)
            {
                string versionString = string.Format(" ({0} Version: {1})", DotNetNukeContext.Current.Application.Status,
                                                     DotNetNukeContext.Current.Application.Version);
                Title += versionString;
            }

            //register DNN SkinWidgets Inititialization scripts
            if (PortalSettings.EnableSkinWidgets)
            {
                jQuery.RequestRegistration();
                // don't use the new API to register widgets until we better understand their asynchronous script loading requirements.
                ClientAPI.RegisterStartUpScript(Page, "initWidgets", string.Format("<script type=\"text/javascript\" src=\"{0}\" ></script>", ResolveUrl("~/Resources/Shared/scripts/initWidgets.js")));
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Look for skin level doctype configuration file, and inject the value into the top of default.aspx
        /// when no configuration if found, the doctype for versions prior to 4.4 is used to maintain backwards compatibility with existing skins.
        /// Adds xmlns and lang parameters when appropiate.
        /// </summary>
        /// <param name="Skin">The currently loading skin</param>
        /// <remarks></remarks>
        /// <history>
        /// 	[cathal]	11/29/2006	Created
        ///     [cniknet]   05/20/2009  Refactored to use HtmlAttributes collection
        /// </history>
        /// -----------------------------------------------------------------------------
        private void SetSkinDoctype()
        {
            string strLang = CultureInfo.CurrentCulture.ToString();
            string strDocType = PortalSettings.ActiveTab.SkinDoctype;
            if (strDocType.Contains("XHTML 1.0"))
            {
                //XHTML 1.0
                HtmlAttributes.Add("xml:lang", strLang);
                HtmlAttributes.Add("lang", strLang);
                HtmlAttributes.Add("xmlns", "http://www.w3.org/1999/xhtml");
            }
            else if (strDocType.Contains("XHTML 1.1"))
            {
                //XHTML 1.1
                HtmlAttributes.Add("xml:lang", strLang);
                HtmlAttributes.Add("xmlns", "http://www.w3.org/1999/xhtml");
            }
            else
            {
                //other
                HtmlAttributes.Add("lang", strLang);
            }
            //Find the placeholder control and render the doctype
            skinDocType.Text = PortalSettings.ActiveTab.SkinDoctype;
            attributeList.Text = HtmlAttributeList;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// - manage affiliates
        /// - log visit to site
        /// </remarks>
        /// <history>
        /// 	[sun1]	1/19/2004	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        private void ManageRequest()
        {
            //affiliate processing
            int affiliateId = -1;
            if (Request.QueryString["AffiliateId"] != null)
            {
                if (Regex.IsMatch(Request.QueryString["AffiliateId"], "^\\d+$"))
                {
                    affiliateId = Int32.Parse(Request.QueryString["AffiliateId"]);
                    var objAffiliates = new AffiliateController();
                    objAffiliates.UpdateAffiliateStats(affiliateId, 1, 0);

                    //save the affiliateid for acquisitions
                    if (Request.Cookies["AffiliateId"] == null) //do not overwrite
                    {
                        var objCookie = new HttpCookie("AffiliateId");
                        objCookie.Value = affiliateId.ToString();
                        objCookie.Expires = DateTime.Now.AddYears(1); //persist cookie for one year
                        Response.Cookies.Add(objCookie);
                    }
                }
            }

            //site logging
            if (PortalSettings.SiteLogHistory != 0)
            {
                //get User ID

                //URL Referrer
                string urlReferrer = "";
                try
                {
                    if (Request.UrlReferrer != null)
                    {
                        urlReferrer = Request.UrlReferrer.ToString();
                    }
                }
                catch (Exception exc)
                {
                    DnnLog.Error(exc);

                }
                string strSiteLogStorage = Host.SiteLogStorage;
                int intSiteLogBuffer = Host.SiteLogBuffer;

                //log visit
                var objSiteLogs = new SiteLogController();

                UserInfo objUserInfo = UserController.GetCurrentUserInfo();
                objSiteLogs.AddSiteLog(PortalSettings.PortalId, objUserInfo.UserID, urlReferrer, Request.Url.ToString(),
                                       Request.UserAgent, Request.UserHostAddress, Request.UserHostName,
                                       PortalSettings.ActiveTab.TabID, affiliateId, intSiteLogBuffer,
                                       strSiteLogStorage);
            }
        }

        private void ManageFavicon()
        {
            string headerLink = FavIcon.GetHeaderLink(PortalSettings.PortalId);

            if (!String.IsNullOrEmpty(headerLink))
            {
                Page.Header.Controls.Add(new Literal { Text = headerLink });
            }
        }

        //I realize the parsing of this is rather primitive.  A better solution would be to use json serialization
        //unfortunately, I don't have the time to write it.  When we officially adopt MS AJAX, we will get this type of 
        //functionality and this should be changed to utilize it for its plumbing.
        private Dictionary<string, string> ParsePageCallBackArgs(string strArg)
        {
            string[] aryVals = strArg.Split(new[] { ClientAPI.COLUMN_DELIMITER }, StringSplitOptions.None);
            var objDict = new Dictionary<string, string>();
            if (aryVals.Length > 0)
            {
                objDict.Add("type", aryVals[0]);
                switch (
                    (DNNClientAPI.PageCallBackType)Enum.Parse(typeof(DNNClientAPI.PageCallBackType), objDict["type"]))
                {
                    case DNNClientAPI.PageCallBackType.GetPersonalization:
                        objDict.Add("namingcontainer", aryVals[1]);
                        objDict.Add("key", aryVals[2]);
                        break;
                    case DNNClientAPI.PageCallBackType.SetPersonalization:
                        objDict.Add("namingcontainer", aryVals[1]);
                        objDict.Add("key", aryVals[2]);
                        objDict.Add("value", aryVals[3]);
                        break;
                }
            }
            return objDict;
        }

        /// <summary>
        /// check if a warning about account defaults needs to be rendered
        /// </summary>
        /// <returns>localised error message</returns>
        /// <remarks></remarks>
        /// <history>
        /// 	[cathal]	2/28/2007	Created
        /// </history>
        private string RenderDefaultsWarning()
        {
            string warningLevel = Request.QueryString["runningDefault"];
            string warningMessage = string.Empty;
            switch (warningLevel)
            {
                case "1":
                    warningMessage = Localization.GetString("InsecureAdmin.Text", Localization.SharedResourceFile);
                    break;
                case "2":
                    warningMessage = Localization.GetString("InsecureHost.Text", Localization.SharedResourceFile);
                    break;
                case "3":
                    warningMessage = Localization.GetString("InsecureDefaults.Text", Localization.SharedResourceFile);
                    break;
            }
            return warningMessage;
        }

        private IFileInfo GetBackgroundFileInfo()
        {
            string cacheKey = String.Format(DotNetNuke.Common.Utilities.DataCache.PortalCacheKey, PortalSettings.PortalId, "BackgroundFile");
            var file = CBO.GetCachedObject<DotNetNuke.Services.FileSystem.FileInfo>(new CacheItemArgs(cacheKey, DotNetNuke.Common.Utilities.DataCache.PortalCacheTimeOut, DotNetNuke.Common.Utilities.DataCache.PortalCachePriority),
                                                    GetBackgroundFileInfoCallBack);

            return file;
        }

        private IFileInfo GetBackgroundFileInfoCallBack(CacheItemArgs itemArgs)
        {
            return FileManager.Instance.GetFile(PortalSettings.PortalId, PortalSettings.BackgroundFile);
        }

        #endregion

        #region Protected Methods

        protected bool NonProductionVersion()
        {
            return DotNetNukeContext.Current.Application.Status != ReleaseMode.Stable;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Contains the functionality to populate the Root aspx page with controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// - obtain PortalSettings from Current Context
        /// - set global page settings.
        /// - initialise reference paths to load the cascading style sheets
        /// - add skin control placeholder.  This holds all the modules and content of the page.
        /// </remarks>
        /// <history>
        /// 	[sun1]	1/19/2004	Created
        ///		[jhenning] 8/24/2005 Added logic to look for post originating from a ClientCallback
        /// </history>
        /// -----------------------------------------------------------------------------
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (Session["footer_megatop"]==null)
                Session["footer_megatop"] = GetFooterContent();
            //set global page settings
            InitializePage();

            //load skin control and register UI js
            UI.Skins.Skin ctlSkin;
            if (PortalSettings.EnablePopUps)
            {
                ctlSkin = IsPopUp ? UI.Skins.Skin.GetPopUpSkin(this) : UI.Skins.Skin.GetSkin(this);

                //register popup js
                jQuery.RegisterJQueryUI(Page);

                var popupFilePath = HttpContext.Current.IsDebuggingEnabled
                                   ? "~/js/Debug/dnn.modalpopup.js"
                                   : "~/js/dnn.modalpopup.js";

                ClientResourceManager.RegisterScript(this, popupFilePath);
            }
            else
            {
                ctlSkin = UI.Skins.Skin.GetSkin(this);
            }

            // DataBind common paths for the client resource loader
            ClientResourceLoader.DataBind();

            //check for and read skin package level doctype
            SetSkinDoctype();

            //Manage disabled pages
            if (PortalSettings.ActiveTab.DisableLink)
            {
                if (TabPermissionController.CanAdminPage())
                {
                    var heading = Localization.GetString("PageDisabled.Header");
                    var message = Localization.GetString("PageDisabled.Text");
                    UI.Skins.Skin.AddPageMessage(ctlSkin, heading, message,
                                                 ModuleMessage.ModuleMessageType.YellowWarning);
                }
                else
                {
                    if (PortalSettings.HomeTabId > 0)
                    {
                        Response.Redirect(Globals.NavigateURL(PortalSettings.HomeTabId), true);
                    }
                    else
                    {
                        Response.Redirect(Globals.GetPortalDomainName(PortalSettings.PortalAlias.HTTPAlias, Request, true), true);
                    }
                }
            }
            //Manage canonical urls
            if (PortalSettings.PortalAliasMappingMode == PortalSettings.PortalAliasMapping.CanonicalUrl && PortalSettings.PortalAlias.HTTPAlias != PortalSettings.DefaultPortalAlias)
            {
                var originalurl = Context.Items["UrlRewrite:OriginalUrl"].ToString();

                //Add Canonical <link>
                var canonicalLink = new HtmlLink();
                canonicalLink.Href = originalurl.Replace(PortalSettings.PortalAlias.HTTPAlias, PortalSettings.DefaultPortalAlias);
                canonicalLink.Attributes.Add("rel", "canonical");

                // Add the HtmlLink to the Head section of the page.
                Page.Header.Controls.Add(canonicalLink);
            }

            //check if running with known account defaults
            var messageText = "";
            if (Request.IsAuthenticated && string.IsNullOrEmpty(Request.QueryString["runningDefault"]) == false)
            {
                var userInfo = HttpContext.Current.Items["UserInfo"] as UserInfo;
                //only show message to default users
                if ((userInfo.Username.ToLower() == "admin") || (userInfo.Username.ToLower() == "host"))
                {
                    messageText = RenderDefaultsWarning();
                    var messageTitle = Localization.GetString("InsecureDefaults.Title", Localization.GlobalResourceFile);
                    UI.Skins.Skin.AddPageMessage(ctlSkin, messageTitle, messageText, ModuleMessage.ModuleMessageType.RedError);
                }
            }

            //add CSS links
            ClientResourceManager.RegisterStyleSheet(this, Globals.HostPath + "default.css", FileOrder.Css.DefaultCss);
            ClientResourceManager.RegisterStyleSheet(this, ctlSkin.SkinPath + "skin.css", FileOrder.Css.SkinCss);
            ClientResourceManager.RegisterStyleSheet(this, ctlSkin.SkinSrc.Replace(".ascx", ".css"), FileOrder.Css.SpecificSkinCss);

            //add skin to page
            SkinPlaceHolder.Controls.Add(ctlSkin);

            ClientResourceManager.RegisterStyleSheet(this, PortalSettings.HomeDirectory + "portal.css", 60);

            //add Favicon
            ManageFavicon();

            //ClientCallback Logic 
            ClientAPI.HandleClientAPICallbackEvent(this);

            //add viewstateuserkey to protect against CSRF attacks
            if (User.Identity.IsAuthenticated)
            {
                ViewStateUserKey = User.Identity.Name;
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Initialize the Scrolltop html control which controls the open / closed nature of each module 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[sun1]	1/19/2004	Created
        ///		[jhenning] 3/23/2005 No longer passing in parameter to __dnn_setScrollTop, instead pulling value from textbox on client
        /// </history>
        /// -----------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
		if(Request.QueryString["testcountry"]!=null){
		if(Request.QueryString["testcountry"]=="nz"){
		 visitor = new Visitor();
            // Get the restrictions for the country
                visitor.StoreID ="2";
                visitor.StoreSK = "S02-NZ";
                visitor.ShippingCountry = "New Zealand";
                visitor.CurrencyCode = "NZD";
                visitor.GstApplicable = "Y";
                visitor.TradingAccountSK = 1;
            visitor.CountryName = "New Zealand";
            visitor.CountryCode = "NZ";
            if (Session["UserInfo"] == null) { Session["UserInfo"] = visitor; }
            else
            {
                Visitor loggedinVisitor = (Visitor)Session["UserInfo"];
                visitor.UserID = loggedinVisitor.UserID;
                visitor.UserName = loggedinVisitor.UserName;
                visitor.DomainOfUser = loggedinVisitor.DomainOfUser;
                visitor.UserCreated = loggedinVisitor.UserCreated;
            }
            Session["Visitor"] = "visited";
            Session["UserInfo"] = visitor;
		}
		//intl
		if(Request.QueryString["testcountry"]=="intl"){
		 visitor = new Visitor();
            // Get the restrictions for the country
                visitor.StoreID ="2";
                visitor.StoreSK = "S05-INTL";
                visitor.ShippingCountry = "OTHER";
                visitor.CurrencyCode = "AUD";
                visitor.GstApplicable = "N";
                visitor.TradingAccountSK = 1;
            visitor.CountryName = "OTHER";
            visitor.CountryCode = "other";
            if (Session["UserInfo"] == null) { Session["UserInfo"] = visitor; }
            else
            {
                Visitor loggedinVisitor = (Visitor)Session["UserInfo"];
                visitor.UserID = loggedinVisitor.UserID;
                visitor.UserName = loggedinVisitor.UserName;
                visitor.DomainOfUser = loggedinVisitor.DomainOfUser;
                visitor.UserCreated = loggedinVisitor.UserCreated;
            }
            Session["Visitor"] = "visited";
            Session["UserInfo"] = visitor;
		}
		Session["Visitor"] = "visited";
		Response.Redirect("/");
		}
            if (Request.QueryString["c"] != null)
            {
                Session["UserInfo"] = null;
            }
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
            if (PortalSettings.ActiveTab.TabName.ToLower().Contains("ecollection"))
            {
                if (Request.QueryString["guid"] != null)
                {
                    string result = DashboardController.Instance.GetUserinfo(Request.QueryString["guid"]);
                    if (result != null && result != string.Empty)
                    {
                        visitor = (Visitor)DeserializeObject(result);
                        Session["UserInfo"] = visitor;
                        Session["UserName"] = visitor.UserName;
                        Session["Visitor"] = "visited";
                    }
                }
            }
            if (Session["Visitor"] == null || Session["UserInfo"] == null)
            {
                if (Request.QueryString["c"] != null)
                {
                    Session["Visitor"] = "visited";
                }
                
                DataSet countryRestrictionDataset = new DataSet();
                visitor = new Visitor();
				visitor = LookupCountry();
                objController = new CountryController();
                countryRestrictionDataset = objController.GetCountryRestrictions(visitor.CountryCode);
                if (countryRestrictionDataset.Tables[0].Rows.Count > 0)
                {
                    DataRow row = countryRestrictionDataset.Tables[0].Rows[0];
                    visitor.StoreID = row["STORESK"].ToString();
                    visitor.StoreSK = row["STOREID"].ToString();
                    visitor.TradingAccountSK = Convert.ToInt32(row["TRADINGACCNUMBER"].ToString());
                    visitor.ShippingCountry = row["SHIPPINGCOUNTRY"].ToString();
                    visitor.CurrencyCode = row["CURRENCYCODE"].ToString();
                    visitor.GstApplicable = row["GST"].ToString();
                }
                else
                {
                    visitor.StoreID = null;
                    visitor.TradingAccountSK = 0;
                    visitor.ShippingCountry = null;
                }
                string SandboxDomain = string.Empty;
                SandboxDomain = ConfigurationManager.AppSettings.Get("Production").ToLower() == "no" ? ConfigurationManager.AppSettings.Get("SimulateDomain") : HttpContext.Current.Request.Url.ToString();
                string[] host = HttpContext.Current.Request.Url.ToString().Split('/');
                DnnLog.Fatal(host[2].ToString());
                if (ConfigurationManager.AppSettings.Get("Production").ToLower() == "no")
                {
                    if ((SandboxDomain.Contains(".au") && visitor.CountryName.ToLower() == "australia") || (SandboxDomain.Contains(".nz") && visitor.CountryName.ToLower() == "new zealand"))//&&)
                    {
                        Session["Visitor"] = "visited";
                    }
                    else
                    {
                        Session["Visitor"] = null;
                    }
                    DnnLog.Fatal("Assigning ApptagetSearchcode Session..");
                    if (!HttpContext.Current.Request.Url.ToString().ToUpper().Contains(ConfigurationManager.AppSettings["DivisionPrimary"].ToString().ToUpper())
                        && !HttpContext.Current.Request.Url.ToString().ToUpper().Contains(ConfigurationManager.AppSettings["DivisionSecondary"].ToString().ToUpper()))
                    {
                        DnnLog.Info("Assigning dropdown values of division..");
                        switch (visitor.CountryCode.ToUpper())
                        {
                            default:
                                if (SandboxDomain.Contains("primary"))
                                {
                                    if (Session["UserInfo"] != null)
                                    {
                                        visitor = (Visitor)Session["UserInfo"];
                                    }
                                    visitor.AppTragetSearchCode = ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString();
                                }
                                else
                                {
                                    if (SandboxDomain.Contains("secondary"))
                                    {
                                        if (Session["UserInfo"] != null)
                                        {
                                            visitor = (Visitor)Session["UserInfo"];
                                        }
                                        visitor.AppTragetSearchCode = ConfigurationManager.AppSettings["AUSECAppTarget"].ToString();
                                    }
                                }
                                break;
                            case "NZ":
                                if (SandboxDomain.Contains("primary"))
                                {
                                    if (Session["UserInfo"] != null)
                                    {
                                        visitor = (Visitor)Session["UserInfo"];
                                    }
                                    visitor.AppTragetSearchCode = ConfigurationManager.AppSettings["NZPRIAppTarget"].ToString();
                                }
                                else
                                {
                                    if (SandboxDomain.Contains("secondary"))
                                    {
                                        if (Session["UserInfo"] != null)
                                        {
                                            visitor = (Visitor)Session["UserInfo"];
                                        }
                                        visitor.AppTragetSearchCode = ConfigurationManager.AppSettings["NZSECAppTarget"].ToString();
                                    }
                                }
                                break;
                        }
                    }
                }
                else
                {
                    if (HttpContext.Current.Request.Url.ToString().ToUpper().Contains(ConfigurationManager.AppSettings["DivisionPrimary"].ToString()))
                    {
                        DnnLog.Fatal("Assigning primary url of Domain..");
                        if (visitor.CountryCode == "AU")
                        {
                            if (Session["UserInfo"] != null)
                            {
                                visitor = (Visitor)Session["UserInfo"];
                            }
                            visitor.AppTragetSearchCode = ConfigurationManager.AppSettings["AUPRIAppTarget"].ToString();
                        }
                        else if (visitor.CountryCode == "NZ")
                        {
                            if (Session["UserInfo"] != null)
                            {
                                visitor = (Visitor)Session["UserInfo"];
                            }
                            visitor.AppTragetSearchCode = ConfigurationManager.AppSettings["NZPRIAppTarget"].ToString();
                        }
                    }
                    else if (HttpContext.Current.Request.Url.ToString().ToLower().Contains(ConfigurationManager.AppSettings["DivisionSecondary"].ToString()))
                    {
                        DnnLog.Info("Assigning secondary url of domain..");
                        if (visitor.CountryCode == "AU")
                        {
                            if (Session["UserInfo"] != null)
                            {
                                visitor = (Visitor)Session["UserInfo"];
                            }
                            visitor.AppTragetSearchCode = ConfigurationManager.AppSettings["AUSECAppTarget"].ToString();
                        }
                        else if (visitor.CountryCode == "NZ")
                        {
                            if (Session["UserInfo"] != null)
                            {
                                visitor = (Visitor)Session["UserInfo"];
                            }
                            visitor.AppTragetSearchCode = ConfigurationManager.AppSettings["NZSECAppTarget"].ToString();
                        }
                    }
					bool isAsianRoot = false;
                    bool isWhitelist = false;
					bool isasianvisitor = false;
					string friendlyurl = PortalSettings.ActiveTab.TabName;
					
					DotNetNuke.Entities.Tabs.TabController objTabs;
					DotNetNuke.Entities.Tabs.TabInfo objTab;
            
					objTabs = new DotNetNuke.Entities.Tabs.TabController();
					objTab = new DotNetNuke.Entities.Tabs.TabInfo();
					objTab = objTabs.GetTab(PortalSettings.ActiveTab.TabID);
					isasianvisitor = ConfigurationManager.AppSettings.Get("AsianCountres").ToLower().Contains(visitor.CountryName.ToLower());
					while (objTab.ParentId != -1 && !objTab.IsDeleted && objTab.IsVisible)
					{

                    objTab = objTabs.GetTab(objTab.ParentId);
					}
					friendlyurl = objTab.TabName;
					
					foreach (string str in ConfigurationManager.AppSettings.Get("AsianRootPages").ToLower().Split(','))
                    {
                        if (friendlyurl.ToLower().Contains(str))
                        {
                            isAsianRoot = true;
                        }
						else if(HttpContext.Current.Request.RawUrl.ToString().ToLower().Contains(str))
						{
							isAsianRoot = true;
						}
                    }
                    foreach (string str in ConfigurationManager.AppSettings.Get("AsianPages").ToLower().Split(','))
                    {
                        if (PortalSettings.ActiveTab.TabName.ToLower().Trim().EndsWith(str))
						{
							isWhitelist = true;							
						}
						else if(HttpContext.Current.Request.RawUrl.ToString().ToLower().Trim().EndsWith(str))
						{
							isWhitelist = true;
						}
                    }
                    if ((HttpContext.Current.Request.Url.ToString().ToLower().Contains(".au") && visitor.CountryName.ToLower() == "australia") 
					|| (HttpContext.Current.Request.Url.ToString().ToLower().Contains(".nz") && visitor.CountryName.ToLower() == "new zealand"))
                    {
                        Session["Visitor"] = "visited";
						if(ConfigurationManager.AppSettings.Get("AsianCountres").ToLower().Contains(visitor.CountryName.ToLower()))
							Session["AsianCountryName"] = visitor.CountryName;
                    }
                    else if (Request.QueryString["c"] != null)
                    {
						DnnLog.Fatal("Query string is not null for 'C'....");
                        Session["Visitor"] = "visited";
                    }
					else if((isasianvisitor) && (isAsianRoot || isWhitelist))
					{
						DnnLog.Fatal("condition step 1 ");
						Session["Visitor"] = "visited";
						Session["AsianCountryName"] = visitor.CountryName;
						Session["Asian"] = "no";
					}
					else if(isasianvisitor)
					{
						DnnLog.Fatal("condition step 2 ");
						Session["AsianCountryName"] = visitor.CountryName;
						Session["Asian"] = "yes";
						if((Session["SUBSCRIPTIONPURCHASE"] != null || Session["SubscriptionUpgrade"] != null 
						|| Session["TrialSubscription"] != null)
						&& HttpContext.Current.Request.RawUrl.ToString().ToLower().Contains("checkout"))
						{
							Session["Visitor"] = "visited";
							DnnLog.Fatal("if condition step 3 ");
						}
						else
						{
								Session["Visitor"] = null;
								DnnLog.Fatal("if condition step 5 ");
						}
					}
					else if(isWhitelist)
					{
						Session["Visitor"] = "visited";
					}
                    else { Session["Visitor"] = null; }
                }
                if (Session["UserInfo"] == null)
                {
                    Session["UserInfo"] = visitor;
                }
                if (HttpContext.Current.Request.Url.ToString().ToLower().Contains(".co.nz"))
                {
				 DnnLog.Fatal("nz domain is detected...");
                    if(visitor.CountryCode.ToLower() != "nz")
                    {
                        DnnLog.Fatal("country is not nz ...");
                        string[] pagename = Request.Url.AbsoluteUri.Split('/');
						//Session["visitor"] =null;
						DnnLog.Fatal("session setting for country is not nz ..."+Session["visitor"]+"...test");
                        if(pagename[pagename.Length - 1] != "")
                        {
                            //Session["visitor"] = "visited";
                            //Response.Redirect(HttpContext.Current.Request.Url.ToString().Replace(".co.nz",".com.au?c=" + visitor.CountryCode));
                        }
                        else { 
						DnnLog.Fatal("redirecting..............->");
						Response.Redirect("/"); 
						}
                    }
                    else
                    {
                        DnnLog.Fatal("country is nz ...");
                    }
                }
				
            }
			else
            {
                visitor=(Visitor)Session["UserInfo"];
                if(HttpContext.Current.Request.Url.ToString().ToLower().Contains(".com.au"))
                {
                    if(visitor.CountryCode.ToLower()=="nz")
                    {
                        Session["Visitor"]=null;
						Session["UserInfo"]=null;
                    }
                }
				if(HttpContext.Current.Request.Url.ToString().ToLower().Contains(".co.nz"))
                {
                    if(visitor.CountryCode.ToLower()!="nz")
                    {
                        Session["Visitor"]=null;
                    }
                }
				
				bool isAsianRoot = false;
				bool isWhitelist = false;
				bool isasianvisitor = false;
				string friendlyurl = PortalSettings.ActiveTab.TabName;
				
				DotNetNuke.Entities.Tabs.TabController objTabs;
				DotNetNuke.Entities.Tabs.TabInfo objTab;
				
				objTabs = new DotNetNuke.Entities.Tabs.TabController();
				objTab = new DotNetNuke.Entities.Tabs.TabInfo();
				objTab = objTabs.GetTab(PortalSettings.ActiveTab.TabID);
				isasianvisitor = ConfigurationManager.AppSettings.Get("AsianCountres").ToLower().Contains(visitor.CountryName.ToLower());
				while (objTab.ParentId != -1 && !objTab.IsDeleted && objTab.IsVisible)
				{
					objTab = objTabs.GetTab(objTab.ParentId);
				}
				friendlyurl = objTab.TabName;
				foreach (string str in ConfigurationManager.AppSettings.Get("AsianRootPages").ToLower().Split(','))
				{
					if (friendlyurl.ToLower().Contains(str))
					{
								isAsianRoot = true;
					}
					else if(HttpContext.Current.Request.RawUrl.ToString().ToLower().Contains(str))
					{
						isAsianRoot = true;
					}
				}
				foreach (string str in ConfigurationManager.AppSettings.Get("AsianPages").ToLower().Split(','))
				{
					if (PortalSettings.ActiveTab.TabName.ToLower().EndsWith(str))
					{
						isWhitelist = true;
						DnnLog.Fatal("Is asia page visited str " + str + " value " + HttpContext.Current.Request.RawUrl.ToString().ToLower());
					}
					else if(HttpContext.Current.Request.RawUrl.ToString().ToLower().Trim().EndsWith(str))
					{
						isWhitelist = true;
					}
				}
				
				
				if((isasianvisitor) && (isAsianRoot || isWhitelist))
				{
					DnnLog.Fatal("condition step 1 ");
					Session["Visitor"] = "visited";
					Session["Asian"] = "no";
				}
				else if(isasianvisitor)
				{
					DnnLog.Fatal("condition step 2 ");
					Session["Asian"] = "yes";
					DnnLog.Fatal("condition step 2 PortalSettings.ActiveTab.TabName.ToLower()" + PortalSettings.ActiveTab.TabName.ToLower());
					if((Session["SUBSCRIPTIONPURCHASE"] != null || Session["SubscriptionUpgrade"] != null 
						|| Session["TrialSubscription"] != null)
						&& HttpContext.Current.Request.RawUrl.ToString().ToLower().Contains("checkout"))
					{
						Session["Visitor"] = "visited";
						DnnLog.Fatal("condition step 3 else");
							
					}
					else
					{
						Session["Visitor"] = null;
						DnnLog.Fatal("condition step 5 else");
					}
				}
				else if(isWhitelist)
				{
					Session["Visitor"] = "visited";
				}
				
            }
			 if (PortalSettings.Current.ActiveTab.TabID!=PortalSettings.HomeTabId)
            {
                if (visitor.CountryCode.ToUpper() == "NZ")
                    ((DotNetNuke.Framework.CDefault)this.Page).Title = ((DotNetNuke.Framework.CDefault)this.Page).Title + " | " + ConfigurationManager.AppSettings["NewZealandPageTitle"].ToString().Trim();
                else
                    ((DotNetNuke.Framework.CDefault)this.Page).Title = ((DotNetNuke.Framework.CDefault)this.Page).Title + " | " + ConfigurationManager.AppSettings["AustraliaPageTitle"].ToString().Trim();
            }

             if (visitor.CountryCode.ToUpper() == "NZ")
             {
                 OpenGraphSiteName.Content = ConfigurationManager.AppSettings["NewZealandPageTitle"].ToString().Trim();
             }
             else
             {
                 OpenGraphSiteName.Content = ConfigurationManager.AppSettings["AustraliaPageTitle"].ToString().Trim();
             }            

            if (Request.QueryString["u"] != null)
            {
                List<UserIdApplicationCollection> userValidateApplication = null;
                List<UserAccountCollection> UserAccountCollection = null;
                UserInfoController UserCengage = new UserInfoController();
                userValidateApplication = UserCengage.ValidateCengageUser(Request.QueryString["u"].Split('/')[0]);
                UserAccountCollection = UserCengage.GetAccountsOfUser(Request.QueryString["u"].Split('/')[0]);
                visitor = new Visitor();
                DnnLog.Fatal("default.aspx::->" + Request.QueryString["u"].Split('/')[0]);
                DnnLog.Fatal("default.aspx:::-->Country code is: " + userValidateApplication[0].CountryCode);
                if (!string.IsNullOrEmpty(userValidateApplication[0].CountryCode))
                {
                    visitor.CountryCode = userValidateApplication[0].CountryCode;
                    DnnLog.Fatal("ShippingCountry :" + userValidateApplication[0].ShippingCountry.ToString());
                    visitor.ShippingCountry = userValidateApplication[0].ShippingCountry;
                    DnnLog.Fatal("Currency Code :" + userValidateApplication[0].CurrencyCode.ToString());
                    visitor.CurrencyCode = userValidateApplication[0].CurrencyCode;
                    DnnLog.Fatal("Gst Applicable :" + userValidateApplication[0].GstApplicable.ToString());
                    visitor.GstApplicable = userValidateApplication[0].GstApplicable;
                }
                if (!string.IsNullOrEmpty(userValidateApplication[0].CountryName))
                {
                    DnnLog.Error("Country code is: " + userValidateApplication[0].CountryName);
                    visitor.CountryName = userValidateApplication[0].CountryName;
                }
                if (!string.IsNullOrEmpty(userValidateApplication[0].BranchCode))
                {
                    DnnLog.Error("Country code is: " + userValidateApplication[0].BranchCode);
                    visitor.BranchCode = Convert.ToInt32(userValidateApplication[0].BranchCode);
                }
                visitor.UserID = userValidateApplication[0].UserID;
                visitor.UserName = userValidateApplication[0].UserLoginName.Trim();
                visitor.UserEmail = userValidateApplication[0].EmailID;
                visitor.DomainOfUser = userValidateApplication[0].UserDomain;
                visitor.TradingAccountSK = userValidateApplication[0].TradingAccountSK;
                visitor.DefaultAccountRoleSK = userValidateApplication[0].DefaultAccountRoleSK;
                visitor.FirstName = userValidateApplication[0].FirstName;
                visitor.LastName = userValidateApplication[0].LastName;
                int i = 0;
                for(;i <= UserAccountCollection.Count-1;i++)
                {
                    if(UserAccountCollection[i].PartnerType.ToLower() == ConfigurationManager.AppSettings["PersonalPartner"].ToString().ToLower())
                    {
                        visitor.PersonalAccount = UserAccountCollection[i].PersonalAccount;
                    }
                    else if(UserAccountCollection[i].PartnerType.ToLower() == ConfigurationManager.AppSettings["BusinessPartner"].ToString().ToLower())
                    {
                        visitor.BusinessAccount = UserAccountCollection[i].BusinessAccount;
                    }
                    else
                    {
                        visitor.StaffAccount = UserAccountCollection[i].StaffAccount;
                    }
                }
                
                DnnLog.Error(userValidateApplication[0].UserDomain);
                if (userValidateApplication[0].StoreSk != 0)
                {
                    DnnLog.Fatal("store setting for AD users:" + userValidateApplication[0].StoreSk.ToString());
                    visitor.StoreSK = userValidateApplication[0].StoreID.ToString();
                    visitor.StoreID = userValidateApplication[0].StoreSk.ToString();
                }
                Session["UserInfo"] = visitor;
				Session["UserName"]=visitor.UserName;
                Session["IsAuthenticated"] = true;
				Session["visitor"] = "visited";
                string[] pagename = Request.Url.AbsoluteUri.Split('/');
                if (pagename[pagename.Length - 1] != "")
                {
                    if (pagename[pagename.Length - 1].ToLower() == "primary.aspx" || pagename[pagename.Length - 1].ToLower() == "secondary.aspx"
                    || pagename[pagename.Length - 1].ToLower() == "primary.aspx#" || pagename[pagename.Length - 1].ToLower() == "secondary.aspx#"
                    || pagename[pagename.Length - 1].ToLower() == "primary" || pagename[pagename.Length - 1].ToLower() == "secondary"
                    || pagename[pagename.Length - 1].ToLower() == "forgot-password" || pagename[pagename.Length - 1].ToLower() == "forgotpassword"
                    || pagename[pagename.Length - 1].ToLower() == "forgot-password#")
                    {
                        DnnLog.Error("page name is :" + pagename[pagename.Length - 1].ToLower());
                        string RedirectPagename = "dashboard.aspx";
                        Response.Redirect(RedirectPagename);
                    }
                    else if (pagename[pagename.Length - 1].ToLower() == "pmecollection.aspx#" || pagename[pagename.Length - 1].ToLower() == "pmecollection#")
                    {
                        DnnLog.Error("page name is :" + pagename[pagename.Length - 1].ToLower());
                        string RedirectPagename = "ecollection.aspx";
                        Response.Redirect(RedirectPagename);
                    }
                    else
                    {
                        DnnLog.Error("page name is :" + pagename[pagename.Length - 1].ToLower());
                        string CurrentPagename = pagename[pagename.Length - 1].ToLower();
                        if(pagename[pagename.Length - 1].Contains("cart"))
                    {
                        Response.Redirect("/list/item/cart");
                    }
                    else
                    {
                        Response.Redirect(pagename[pagename.Length - 1]);
                    }
                    }
                }
                else { Response.Redirect("/"); }

            }
			if(Session["SetUserDomainForGA"]!=null){
			_userDomainForGA.Value=Session["SetUserDomainForGA"].ToString();
			if(!PortalSettings.ActiveTab.TabName.Contains("signin.aspx"))
			Session["SetUserDomainForGA"]=null;
			}
			else
			{
			_userDomainForGA.Visible = false;
			}
			if(Session["CallSuccessSignForGA"]!=null){
			_successSignForGA.Value=Session["CallSuccessSignForGA"].ToString();
			 if(PortalSettings.ActiveTab.TabName.ToLower().Contains("dashboard")||PortalSettings.ActiveTab.TabName.ToLower().Contains("checkout"))
			Session["CallSuccessSignForGA"]=null;
			}
			else
			{
			_successSignForGA.Visible = false;
			}
			_storeName.Value=visitor.StoreSK;
             string pagenames = PortalSettings.ActiveTab.TabName;
            if (pagenames != "")
            {
                if (pagenames.ToLower() == "404error" || pagenames.Contains("404error"))
                {
                    //Response.StatusCode = 404;
					Server.ClearError();
					Response.StatusCode = 404; 
					Response.TrySkipIisCustomErrors = true; 
                }
            }
           
            base.OnLoad(e);
            if (!String.IsNullOrEmpty(ScrollTop.Value))
            {
                DNNClientAPI.AddBodyOnloadEventHandler(Page, "__dnn_setScrollTop();");
                ScrollTop.Value = ScrollTop.Value;
            }
            
        }
        public string RemoveQueryStringByKey(string url, string key)
        {
            var uri = new Uri(url);

            // this gets all the query string key value pairs as a collection
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            // this removes the key if exists
            newQueryString.Remove(key);

            // this gets the page path from root without QueryString
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }
        private object DeserializeObject(string result)
        {
            byte[] bytes = Convert.FromBase64String(result);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }
        protected override void OnPreRender(EventArgs evt)
        {
		  
			
            base.OnPreRender(evt);

			
            //process the current request
            if (!Globals.IsAdminControl())
            {
                ManageRequest();
            }

            //Set the Head tags
            Page.Header.Title = Title;
            MetaGenerator.Content = Generator;
            MetaGenerator.Visible = (!String.IsNullOrEmpty(Generator));
            MetaAuthor.Content = PortalSettings.PortalName;
            MetaCopyright.Content = Copyright;
            MetaCopyright.Visible = (!String.IsNullOrEmpty(Copyright));
            MetaKeywords.Content = KeyWords;
            MetaKeywords.Visible = (!String.IsNullOrEmpty(KeyWords));
            if (PortalSettings.ActiveTab.TabName.ToLower()=="product")
            {
                if (Session["metadatacontent"] != null)
                {
                    Description = Session["metadatacontent"].ToString();
					OpenGraphDescription.Visible = true;
					OpenGraphDescription.Content = Description;
					
                }                
            }
			MetaDescription.Content = Description;
            MetaDescription.Visible = (!String.IsNullOrEmpty(Description));
            OpenGraphTitle.Content = ((DotNetNuke.Framework.CDefault)this.Page).Title;
            OpenGraphDescription.Visible = false;
            try
            {
                string SiteUrl = string.Empty, AppendUrl = string.Empty;
                SiteUrl = DotNetNuke.Common.Globals.NavigateURL(this.PortalSettings.HomeTabId);
                if(SiteUrl .Length > 0)
                AppendUrl = SiteUrl.Substring(0, SiteUrl.Length - 1);
                OpenGraphURL.Content = AppendUrl + Page.Request.RawUrl.ToString();
            }
            catch (Exception e1) { }
            
        }

        #endregion
        private Visitor LookupCountry()
        {
             DnnLog.Fatal("enters into country set up...");
            CountryLookup getCountry = null;
            try
            {
                fileName = Server.MapPath(ConfigurationManager.AppSettings.Get("GeoIPDBpath"));
                sandBox = ConfigurationManager.AppSettings.Get("SandBox");
                strIpAddress = string.Empty;
                if (sandBox.ToLower() == "true")
                {
                    strIpAddress = ConfigurationManager.AppSettings.Get("SimulateIP");
                }
                else
                {
                    strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (strIpAddress == null)
                    {
                        strIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                visitor = new Visitor();
                visitor.IPAddress = strIpAddress;
                getCountry = new CountryLookup(fileName);
                if (Request.QueryString["c"] != null)
                {
                    DnnLog.Fatal("enters into cross domain country set up...");
                    string[] countryCode = Request.QueryString["c"].Split('/');
                    visitor.CountryCode = Request.QueryString["c"];
                    string[] pagename = Request.Url.AbsoluteUri.Split('/');
                    if (countryCode[0].ToLower() == "other")
                    {
                        DnnLog.Fatal("enters into cross country set up to aus...");
                        visitor.CountryCode = "AU";
                        visitor.CountryName = "Australia";
                        visitor.User_IP_Country = "Australia";
                    }
                    else if (countryCode[0].ToLower() == "nz")
                    {
                        DnnLog.Fatal("enters into country set up to nz...");
                        visitor.CountryCode = "NZ";
                        visitor.CountryName = "New Zealand";
                        visitor.User_IP_Country = "New Zealand";
                    }
                    else if(countryCode[0].ToLower() == "au")
                    {
                        DnnLog.Fatal("au is detected...");
                        visitor.CountryCode = "AU";
                        visitor.CountryName = "Australia";
                        visitor.User_IP_Country = "Australia";
                    }
                    else
                    {
                        DnnLog.Fatal("get the unknown country and set up the settings...");
                        DnnLog.Fatal("." + countryCode[0]);
                        visitor.CountryCode = countryCode[0];
                        DnnLog.Fatal("country index::" + getCountry.GetCounrty(countryCode[0]));
                        visitor.CountryName = getCountry.GetCounrty(countryCode[0]);
                        visitor.User_IP_Country = getCountry.GetCounrty(countryCode[0]);
                    }
                    objController = new CountryController();
                    countryRestrictionDataset = objController.GetCountryRestrictions(visitor.CountryCode);
                    if (countryRestrictionDataset.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = countryRestrictionDataset.Tables[0].Rows[0];
                        visitor.StoreID = row["STORESK"].ToString();
                        visitor.StoreSK = row["STOREID"].ToString();
                        visitor.TradingAccountSK = Convert.ToInt32(row["TRADINGACCNUMBER"].ToString());
                        visitor.ShippingCountry = row["SHIPPINGCOUNTRY"].ToString();
                        visitor.CurrencyCode = row["CURRENCYCODE"].ToString();
                        visitor.GstApplicable = row["GST"].ToString();
                    }
                    else
                    {
                        visitor.StoreID = null;
                        visitor.TradingAccountSK = 0;
                        visitor.ShippingCountry = null;
                    }
                    Session["UserInfo"] = visitor;
                    DnnLog.Fatal("url to redirect is :" + pagename[2] + "test-->" + Request.Url.AbsoluteUri);
                    if (pagename[pagename.Length - 1] != "")
                    {
                        Response.Redirect(pagename[pagename.Length - 1]);
                    }
                    else { Response.Redirect("/"); }
                }
                else
                {
                    DnnLog.Fatal("enters into country set up for ip..."+strIpAddress);
                    visitor.CountryCode = getCountry.lookupCountryCode(strIpAddress);
					DnnLog.Fatal("enters into country set up for ip country code"+visitor.CountryCode);
                    visitor.CountryName = getCountry.lookupCountryName(strIpAddress);
                    visitor.User_IP_Country = getCountry.lookupCountryName(strIpAddress);
                }
                if (visitor.CountryName.ToUpper() == "N/A")
                {
                    DnnLog.Fatal("enters into country set up for internal cengage ip...");
                    objController = new CountryController();
                    DataSet IPData = new DataSet();
                    IPData = objController.CheckIPAvailability();
                    for (int i = 0; i < IPData.Tables[0].Rows.Count; i++)
                    {
                        DataRow IPDataValues = IPData.Tables[0].Rows[i];
                        IPAddressRange IPRange = new IPAddressRange(IPAddress.Parse(IPDataValues["Startrange"].ToString()), IPAddress.Parse(IPDataValues["endrange"].ToString()));
                        if (IPRange.IsInRange(IPAddress.Parse(strIpAddress)))
                        {
                            visitor.CountryCode = IPDataValues["COUNTRYCODE"].ToString();
                            visitor.CountryName = IPDataValues["COUNTRYNAME"].ToString();
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)// file not found exception
            {
                StringBuilder ExceptionMessage = new StringBuilder();
                ExceptionMessage.Append("Message" + ex.Message + "\t");
                if (ex.InnerException != null)
                {
                    ExceptionMessage.Append("Inner Exception Message" + ex.InnerException.Message + "\t");
                    ExceptionMessage.Append("Inner Exception Stacktrace" + ex.InnerException.StackTrace + "\t");
                }
                DnnLog.Error(ExceptionMessage);
            }
            return visitor;
        }

        public  string GetFooterContent()
        {
            FooterController fr = FooterController.Instance;
                      return fr.GetFooterContent() .Rows[0]["Content"].ToString();
        }
    }
    public class IPAddressRange
    {
        readonly AddressFamily addressFamily;
        readonly byte[] lowerBytes;
        readonly byte[] upperBytes;
        public IPAddressRange(IPAddress lower, IPAddress upper)
        {
            this.addressFamily = lower.AddressFamily;
            this.lowerBytes = lower.GetAddressBytes();
            this.upperBytes = upper.GetAddressBytes();
        }
        public bool IsInRange(IPAddress address)
        {
            if (address.AddressFamily != addressFamily)
            {
                return false;
            }
            byte[] addressBytes = address.GetAddressBytes();
            bool lowerBoundary = true, upperBoundary = true;
            for (int i = 0; i < this.lowerBytes.Length &&
            (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < lowerBytes[i]) ||
                (upperBoundary && addressBytes[i] > upperBytes[i]))
                {
                    return false;
                }
                lowerBoundary &= (addressBytes[i] == lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == upperBytes[i]);
            }
            return true;
        }
    }
}
