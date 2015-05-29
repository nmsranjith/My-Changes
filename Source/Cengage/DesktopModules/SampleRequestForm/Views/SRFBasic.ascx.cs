using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Services;
using Cengage.eCommerce.Lib;
using Cengage.Ecommerce.Dashboard.Components.Controller;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Instrumentation;
using DotNetNuke.Modules.CengageRegistration.Components.Controller;
using DotNetNuke.Modules.SampleRequestForm.Components.Controller;
using DotNetNuke.Modules.SampleRequestForm.Components.Modal;
using System.Text;
using System.Web;
using System.Threading;
using System.Web.UI;

namespace DotNetNuke.Modules.SampleRequestForm.Views
{
    public partial class SRFBasic : SampleRequestFormModuleBase
    {
        SRFParameters sRFParameters = null;
        List<string> itemsCourse = null;
        List<string> itemsDigitalEAC = null;
        List<string> itemsDigitalESS = null;
        List<string> itemsPrint = null;
        DataTable orderPhyDt = null;
        DataTable orderDigDt = null;
        Visitor visitor = null;
        string redirectUrl = string.Empty, instructorInfo = string.Empty,errEmailLog=string.Empty;
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            string logContent = "\r\n Page_Load Start";
            try
            {               
                visitor = Session["UserInfo"] as Visitor;
                SRFCountry.SelectedValue = visitor.CountryCode.ToUpper();
                if (!IsPostBack)
                {
                    try
                    {
                        var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                        logContent = string.Concat(logContent, "\r\n Website URL --> ", uri.Host);
                    }
                    catch (Exception) { }
                    logContent = string.Concat(logContent, "\r\n CountryCode --> ", visitor.CountryCode.ToUpper());
                    sRFParameters = new SRFParameters();
                    SRFContactUsLnk.HRef = "/contactus/div/" + Request.QueryString["div"];
                    logContent = string.Concat(logContent, "\r\n Division --> ", Null.SetNullString(Request.QueryString["div"]));
                    SRFCountry.DataSource = (new RegBLL()).GetCountries();
                    SRFCountry.DataBind();
                    itemsCourse = new List<string>();
                    itemsDigitalEAC = new List<string>();
                    itemsDigitalESS = new List<string>();
                    itemsPrint = new List<string>();
                    orderPhyDt = new DataTable();
                    orderDigDt = new DataTable();
                    List<SRFItems> formatItemsList = new List<SRFItems>();
                    List<SRFItems> formatItems = Session["CoreFormatProducts"] as List<SRFItems>;
                    logContent = string.Concat(logContent, "\r\n Session[CoreFormatProducts] - Count --> ", formatItems.Count);
                    List<SRFSupplements> teachingSuplements = Session["CoreSupplementsProducts"] as List<SRFSupplements>;
                    logContent = string.Concat(logContent, "\r\n Session[CoreSupplementsProducts] - Count --> ", teachingSuplements.Count);
                    if ((formatItems == null && teachingSuplements == null) || (formatItems.Count + teachingSuplements.Count == 0))
                    {
                        switch (Request.QueryString["div"])
                        {
                            case "vocational":
                                Response.Redirect("/vpg");
                                break;
                            default:
                                Response.Redirect("/higher");
                                break;
                        }
                    }

                    if (teachingSuplements != null && teachingSuplements.Count > 1)
                    {
                        List<SRFSupplements> supplementsList = new List<SRFSupplements>();

                        for (int i = 1; i < teachingSuplements.Count; i++)//!(teachingSuplements[i].MediaType=="EAC" && teachingSuplements[i].IsCore==false)
                        {
                            if (teachingSuplements[i].MediaType != "EAC")
                                supplementsList.Add(teachingSuplements[i]);
                        }
                        SRFSuppMsgPara.InnerText = teachingSuplements[0].Description;
                        TeachingSupplementsRptr.DataSource = supplementsList;
                        TeachingSupplementsRptr.DataBind();
                    }
                    else if (teachingSuplements != null && teachingSuplements.Count > 0)
                    {
                        SRFSuppMsgPara.InnerText = teachingSuplements[0].Description;
                        TeachingSupplementsDiv.Visible = false;
                    }
                    else
                        TeachingSupplementsTopDiv.Visible = false;

                    orderPhyDt.Columns.AddRange(new DataColumn[4] { new DataColumn("ID", typeof(int))
                                                                , new DataColumn("PRODUCT_SK", typeof(int))
                                                                , new DataColumn("QUANTITY", typeof(int))
                                                                , new DataColumn("TOTAL_PRICE", typeof(decimal))});
                    orderDigDt.Columns.AddRange(new DataColumn[4] { new DataColumn("ID", typeof(int))
                                                                , new DataColumn("PRODUCT_SK", typeof(int))
                                                                , new DataColumn("QUANTITY", typeof(int))
                                                                , new DataColumn("TOTAL_PRICE", typeof(decimal))});
                    // For format Items ie., course smart items
                    // SqlDataReader courseItems = sampleRequestFormController.CheckForSSOReedamable(formatItems.Select(x => x.ISBN).ToArray(), ISbnType.FORMAT.ToString());

                    FillingItemsintoList(itemsCourse, itemsPrint, formatItems, new List<SRFSupplements>());

                    // For supplements ie., Eac,Ess in supplements

                    FillingEACItemsintoList(itemsDigitalEAC, itemsPrint, new List<SRFItems>(), teachingSuplements);
                    FillingESSItemsintoList(itemsDigitalESS, itemsPrint, new List<SRFItems>(), teachingSuplements);
                    if ((itemsCourse.Count != 0 || itemsDigitalEAC.Count != 0 || itemsDigitalESS.Count != 0) && itemsPrint.Count == 0)
                    {
                        logContent = string.Concat(logContent, "\r\n Print Item Exists --> No");
                        //sRFParameters.TypeOfPack = "SSO";
                        SRFAddressDiv.Visible = false;
                    }
                    else
                    {
                        logContent = string.Concat(logContent, "\r\n Print Item Exists --> Yes");
                        SRFAddressDiv.Visible = true;
                        //sRFParameters.TypeOfPack = string.Empty;
                    }


                    if (formatItems != null && formatItems.Count > 0)
                    {
                        SecLvlDisciplineLnk.HRef = formatItems[0].SecondLevelUrl;
                        SecLvlDisciplineLnk.InnerText = formatItems[0].SecondLevelDiscipline;
                        ThirdLvlDisciplineLnk.HRef = formatItems[0].ThirdLevelUrl;
                        ThirdLvlDisciplineLnk.InnerText = formatItems[0].ThirdLevelDiscipline;
                        SRFProductName.InnerText = formatItems[0].ProductName;

                        for (int i = 0; i < formatItems.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(formatItems[i].ISBN))
                                formatItemsList.Add(formatItems[i]);
                        }
                        SelectedFormatsRptr.DataSource = formatItemsList;
                        SelectedFormatsRptr.DataBind();
                    }


                    if (Session["IsAuthenticated"] != null)
                    {
                        logContent = string.Concat(logContent, "\r\n Is User Authenticated --> Yes");
                        logContent = string.Concat(logContent, "\r\n Logged in email --> " + Null.SetNullString(visitor.UserEmail));
                        logContent = string.Concat(logContent, "\r\n Logged in user name --> " + Null.SetNullString(visitor.UserName));
                        SRFPersonalName.Disabled = true;
                        SRFEmailid.Disabled = true;
                        if (visitor.VerificationStatus == "VERIFIED")
                        {
                            logContent = string.Concat(logContent, "\r\n User Verification Status --> VERIFIED");
                            if (itemsCourse.Count != 0 || itemsDigitalEAC.Count != 0 || itemsDigitalESS.Count != 0)
                            {
                                logContent = string.Concat(logContent, "\r\n Digital item exists --> Yes");
                                DataTable Studentavailbility = DashBoardController.Instance.GetStudentDetails(visitor.UserID);
                                if (Studentavailbility.Rows.Count != 0)
                                {
                                    logContent = string.Concat(logContent, "\r\n Secondary email exists --> Yes");
                                    CurrentSecEmailId.InnerText = string.Concat("Currently you are using [", Studentavailbility.Rows[0]["Email"].ToString(), "]");
                                }
                                else
                                {
                                    logContent = string.Concat(logContent, "\r\n Secondary email exists --> No");
                                    SRFSecondaryEmailDiv.Visible = true;
                                    SRFSecondaryEmailPara.Visible = true;
                                    SRFSecondaryPasswordDiv.Visible = true;
                                }
                            }
                            else
                            {
                                logContent = string.Concat(logContent, "\r\n Digital item exists --> No");
                                HideSecondaryEmailContent();
                            }
                            SSOAPIController controller = new SSOAPIController();
                            int ResponceCode = 0;
                            sRFParameters.Email = visitor.UserName;
                            logContent = string.Concat(logContent, "\r\n UserName --> ", Null.SetNullString(sRFParameters.Email));
                            sRFParameters.UserSk = visitor.UserID;
                            logContent = string.Concat(logContent, "\r\n UserSK --> ", Null.SetNullString(sRFParameters.UserSk));
                            byte[] ToEncryptArray = Convert.FromBase64String(visitor.PrimaryPassword);
                            sRFParameters.Password = UTF8Encoding.UTF8.GetString(ToEncryptArray);
                            var instructorCredentials = controller.SSOLogin(sRFParameters, out ResponceCode);
                            logContent = string.Concat(logContent, "\r\n Password --> ", Null.SetNullString(sRFParameters.Password));
                            if (!string.IsNullOrEmpty(instructorCredentials["token"].ToString()))
                            {
                                sRFParameters.Token = instructorCredentials["token"].ToString();
                                logContent = string.Concat(logContent, "\r\n Token --> ", sRFParameters.Token);
                            }
                            SRFLoggedInPara.Visible = true;
                            SRFLoggedOutDiv.Visible = false;
                            SRFLoggedOutPara2.Visible = false;
                        }
                        else
                        {
                            logContent = string.Concat(logContent, "\r\n User Verification Status --> Not Verified");
                            HideSecondaryEmailContent();
                            SRFLoggedOutDiv.Visible = false;
                            SRFLoggedOutPara2.Visible = true;
                            SRFLoggedInPara.Visible = false;
                        }
                        FillInstructorInfo(visitor.UserName);
                        logContent = string.Concat(logContent, instructorInfo);
                       
                        sRFParameters.ContactID = visitor.ContactID;
                        logContent = string.Concat(logContent, "\r\n ContactID --> ", visitor.ContactID);
                        // DnnLog.Info("Token is :" + sRFParameters.Token);
                    }
                    else
                    {
                        logContent = string.Concat(logContent, "\r\n Is User Authenticated --> No");
                        SRFLoggedInPara.Visible = false;
                        SRFLoggedOutDiv.Visible = true;
                        SRFLoggedOutPara2.Visible = true;
                        HideSecondaryEmailContent();
                    }

                    if (visitor.CountryCode != "AU")
                        SRFStateDiv.Attributes.Add("class", "control-group tip cname-div HideItems");
                    else
                        SRFStateDiv.Attributes.Add("class", "control-group tip cname-div ShowItems");

                    Cache["itemsCourse" + HttpContext.Current.Session.SessionID] = itemsCourse;
                    logContent = string.Concat(logContent, "\r\n Course Mart Items - Count --> ", itemsCourse.Count);
                    Cache["itemsDigitalEAC" + HttpContext.Current.Session.SessionID] = itemsDigitalEAC;
                    logContent = string.Concat(logContent, "\r\n EAC Items - Count --> ", itemsDigitalEAC.Count);
                    Cache["itemsDigitalESS" + HttpContext.Current.Session.SessionID] = itemsDigitalESS;
                    logContent = string.Concat(logContent, "\r\n ESS Items - Count --> ", itemsDigitalESS.Count);
                    Cache["itemsPrint" + HttpContext.Current.Session.SessionID] = itemsPrint;
                    logContent = string.Concat(logContent, "\r\n Print Items - Count --> ", itemsPrint.Count);

                    try
                    {
                        sRFParameters.UserAgent = HttpContext.Current.Request.UserAgent;
                        logContent = string.Concat(logContent, "\r\n User Agent --> ", Request.UserAgent);
                    }
                    catch (Exception) { }
                    //sRFParameters.Token = visitor.Token;
                  

                    // LogValues("Contact ID from SRF :" + visitor.ContactID);
                    sRFParameters.SSOAuthKey = ConfigurationManager.AppSettings["SSOAuthKey"].ToString();
                    logContent = string.Concat(logContent, "\r\n SSOAuthKey --> ", sRFParameters.SSOAuthKey);

                    
                    Cache["SRFParameters" + HttpContext.Current.Session.SessionID] = sRFParameters;

                }

                if (Session["IsAuthenticated"] != null)
                {
                    UserTypeHdn.Value = visitor.VerificationStatus;
                }
                else
                    UserTypeHdn.Value = "NOT VERIFIED";
            }
            catch (Exception exc) //Module failed to load
            {
                logContent = string.Concat(logContent, "\r\n Exception --> ", exc.Message);
                LogFileWrite(exc);
            }
            finally
            {
                if (!IsPostBack)
                    LogValues(logContent);
            }
        }

        /// <summary>
        /// To hide the secondary email content for unverified/anonymous users
        /// </summary>
        private void HideSecondaryEmailContent()
        {
            SRFSecondaryEmailDiv.Visible = false;
            SRFSecondaryEmailPara.Visible = false;
            SRFSecondaryPasswordDiv.Visible = false;
            SRFSecondaryEmailDiv1.Visible = false;
            SRFSecondaryEmailDiv2.Visible = false;
        }

        int threadSuccessCheck = 0;
        /// <summary>
        /// Send Request Button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SubmitRequest_OnClick(object sender, System.EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "StartUpdateProgress", "<script>StartUpdateProgress();</script>", false);
            string logContent = string.Empty;
            int check = 0;
            try
            {
                logContent = string.Concat(logContent, "\r\n SEND REQUEST BUTTON CLICK EVENT ");
                sRFParameters = Cache["SRFParameters" + HttpContext.Current.Session.SessionID] as SRFParameters;
                if (sRFParameters == null)
                    sRFParameters = new SRFParameters();
                sRFParameters.Name = SRFPersonalName.Value;
                logContent = string.Concat(logContent, "\r\n Name-->", SRFPersonalName.Value);
                sRFParameters.Email = SRFEmailid.Value;
                logContent = string.Concat(logContent, "\r\n Email-->", SRFEmailid.Value);
                logContent = string.Concat(logContent, "\r\n Contact ID-->", sRFParameters.ContactID);
                logContent = string.Concat(logContent, "\r\n Token-->", sRFParameters.Token);                
                List<SRFItems> formatItems = Session["CoreFormatProducts"] as List<SRFItems>;
                List<SRFSupplements> teachingSuplements = Session["CoreSupplementsProducts"] as List<SRFSupplements>;
                sRFParameters.ContactType = SRFContactDpn.Value;
                logContent = string.Concat(logContent, "\r\n ContactType-->", SRFContactDpn.Value);
                switch (SRFContactDpn.Value)
                {
                    case "1":
                        sRFParameters.Mobile = SRFPhone.Value;
                        logContent = string.Concat(logContent, "\r\n Mobile-->", SRFPhone.Value);
                        break;
                    case "2":
                        sRFParameters.Work = SRFPhone.Value;
                        logContent = string.Concat(logContent, "\r\n Work-->", SRFPhone.Value);
                        break;
                    case "3":
                        sRFParameters.Contact_Email = SRFPhone.Value;
                        logContent = string.Concat(logContent, "\r\n Contact_Email-->", SRFPhone.Value);
                        break;
                }
                sRFParameters.ContactValue = SRFPhone.Value;
                
                sRFParameters.CourseName = SRFcourseName.Value;
                logContent = string.Concat(logContent, "\r\n CourseName-->", SRFcourseName.Value);
                sRFParameters.CourseCode = SRFcoursesCode.Value;
                logContent = string.Concat(logContent, "\r\n CourseCode-->", SRFcoursesCode.Value);
                sRFParameters.Semester = SRFSemesterDpn.Value;
                logContent = string.Concat(logContent, "\r\n Semester-->", SRFSemesterDpn.Value);
                sRFParameters.Enrolment = SRFSemesterEnrolment.Value;
                logContent = string.Concat(logContent, "\r\n Enrolment Number-->", SRFSemesterEnrolment.Value);
                sRFParameters.BookInUse = SRFBookCurrently.Value;
                logContent = string.Concat(logContent, "\r\n BookInUse-->", SRFBookCurrently.Value);
                sRFParameters.Institution = SRFInstitution.Value;
                logContent = string.Concat(logContent, "\r\n Institution-->", SRFInstitution.Value);

                sRFParameters.AlreadyUsing = SRFAlreadyUsingChkbx.Checked ? "Yes" : "No";
                logContent = string.Concat(logContent, "\r\n AlreadyUsing-->", sRFParameters.AlreadyUsing);
                sRFParameters.ForTeaching = SRFForTeachingChkbx.Checked ? "Yes" : "No";
                logContent = string.Concat(logContent, "\r\n ForTeaching-->", sRFParameters.ForTeaching);
                sRFParameters.NeedHelp = SRFNeedHelpChkbx.Checked ? "Yes" : "No";
                logContent = string.Concat(logContent, "\r\n NeedHelp-->", sRFParameters.NeedHelp);
                sRFParameters.Message = SRFCommentTxt.Value;
                logContent = string.Concat(logContent, "\r\n Message-->", sRFParameters.Message);

                if ((!string.IsNullOrEmpty(sRFParameters.Name) && !string.IsNullOrEmpty(SRFPersonalName.Value) 
                    && !string.IsNullOrEmpty(sRFParameters.Email) && !string.IsNullOrEmpty(SRFEmailid.Value)
                    && !string.IsNullOrEmpty(sRFParameters.ContactValue) && !string.IsNullOrEmpty(SRFPhone.Value)
                    && !string.IsNullOrEmpty(sRFParameters.CourseName) && !string.IsNullOrEmpty(SRFcourseName.Value)
                    && !string.IsNullOrEmpty(sRFParameters.CourseCode) && !string.IsNullOrEmpty(SRFcoursesCode.Value)
                    && !string.IsNullOrEmpty(sRFParameters.Semester) && SRFSemesterDpn.Value!="0"
                    && !string.IsNullOrEmpty(sRFParameters.Enrolment) && !string.IsNullOrEmpty(SRFSemesterEnrolment.Value)
                    && !string.IsNullOrEmpty(sRFParameters.Institution) && !string.IsNullOrEmpty(SRFInstitution.Value)
                    && !string.IsNullOrEmpty(sRFParameters.BookInUse) && !string.IsNullOrEmpty(SRFBookCurrently.Value)) == false)
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "SRFSubmit", "<script>SRFSubmit();</script>", false);
                    LogValues("VALIDATION STATUS --> SOME MANDATORY FIELDS ARE EMPTY");
                    return;
                }                

                if (SRFAddressDiv.Visible == true)
                {
                    sRFParameters.Address1 = SRFShippingAddressTxt1.Value;
                    logContent = string.Concat(logContent, "\r\n Address1-->", sRFParameters.Address1);
                    sRFParameters.Address2 = SRFShippingAddressTxt2.Value;
                    logContent = string.Concat(logContent, "\r\n Address2-->", sRFParameters.Address2);
                    sRFParameters.Address3 = SRFShippingAddressTxt3.Value;
                    logContent = string.Concat(logContent, "\r\n Address3-->", sRFParameters.Address3);
                    sRFParameters.SubUrb = SRFSuburbTxt.Value;
                    logContent = string.Concat(logContent, "\r\n SubUrb-->", sRFParameters.SubUrb);
                    sRFParameters.PostalCode = Null.SetNullInteger(SRFPostcode.Value);
                    logContent = string.Concat(logContent, "\r\n PostalCode-->", sRFParameters.PostalCode);
                    sRFParameters.State = SRFStateDpn.Value;
                    logContent = string.Concat(logContent, "\r\n State-->", sRFParameters.State);
                    sRFParameters.Country = SRFCountry.SelectedValue;
                    logContent = string.Concat(logContent, "\r\n Country-->", sRFParameters.Country);
                    sRFParameters.StateText = SRFStateDpn.Items[SRFStateDpn.SelectedIndex].Text;
                    logContent = string.Concat(logContent, "\r\n StateText-->", sRFParameters.StateText);
                    sRFParameters.IsAddressExist = 'Y';

                    if ((!string.IsNullOrEmpty(sRFParameters.Address1 + sRFParameters.Address2 + sRFParameters.Address3)
                    && !string.IsNullOrEmpty(sRFParameters.SubUrb) && !string.IsNullOrEmpty(sRFParameters.PostalCode.ToString()) && !string.IsNullOrEmpty(sRFParameters.State)
                    && !string.IsNullOrEmpty(sRFParameters.Country)) == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "SRFSubmit", "<script>SRFSubmit();</script>", false);
                        LogValues("ADDRESS VALIDATION STATUS --> SOME MANDATORY FIELDS ARE EMPTY");
                        return;
                    }

                }
                else
                    sRFParameters.IsAddressExist = 'N';
                sRFParameters.HowYouKnow = SRFResourceDpn.Value;
                logContent = string.Concat(logContent, "\r\n HowYouKnow-->", sRFParameters.HowYouKnow);
                string[] salesRepDetails = null;
                if (Session["IsAuthenticated"] != null)
                {
                    logContent = string.Concat(logContent, "\r\n IsAuthenticated-->Yes");
                    //Save Instructor Information
                    if (SampleRequestFormController.Instance.SaveSSOInstructorInfo(sRFParameters) > 0)
                        DnnLog.Info("Saved Instructor(" + sRFParameters.Email + ") Information successfully.");
                    else
                        DnnLog.Info("Save Instructor(" + sRFParameters.Email + ") Information failed.");

                    if (visitor.VerificationStatus == "VERIFIED")
                    {
                        logContent = string.Concat(logContent, "\r\n VerificationStatus-->", visitor.VerificationStatus);
                        // Inserting Orders --to do--            
                        orderPhyDt = Cache["OrderPhyDtDTDCDEFK" + HttpContext.Current.Session.SessionID] as DataTable;
                        orderDigDt = Cache["OrderDigDtDTDCDEFK" + HttpContext.Current.Session.SessionID] as DataTable;
                        sRFParameters.UserAgent = Request.UserAgent;
                        logContent = string.Concat(logContent, "\r\n UserAgent-->", Request.UserAgent);
                        itemsCourse = Cache["itemsCourse" + HttpContext.Current.Session.SessionID] as List<string>;
                        itemsDigitalEAC = Cache["itemsDigitalEAC" + HttpContext.Current.Session.SessionID] as List<string>;
                        itemsDigitalESS = Cache["itemsDigitalESS" + HttpContext.Current.Session.SessionID] as List<string>;
                        itemsPrint = Cache["itemsPrint" + HttpContext.Current.Session.SessionID] as List<string>;
                        SSOandBMcontroller postController = new SSOandBMcontroller();
                        int printSuccess = 0, digitalSuccess = 0, mgRes = 0;
                        string orderNumber1 = string.Empty, orderNumber2 = string.Empty, itemType = "", failedItems = string.Empty, errPrintISBN = string.Empty,
                            errEbookISBN = string.Empty, errEACISBN = string.Empty, errESSISBN = string.Empty,isbns=string.Empty;
                        if ((orderDigDt.Rows.Count > 0))
                        {
                            DataTable Studentavailbility = DashBoardController.Instance.GetStudentDetails(visitor.UserID);
                            if (Studentavailbility.Rows.Count != 0)
                                sRFParameters.SecondaryEmail = Studentavailbility.Rows[0]["Email"].ToString();
                            if ((!string.IsNullOrEmpty(sRFParameters.SecondaryEmail)) == false)
                            {
                                ScriptManager.RegisterStartupScript(Page, GetType(), "SRFSubmit", "<script>SRFSubmit();</script>", false);
                                LogValues("SECONDARY EMAIL VALIDATION STATUS --> SOME MANDATORY FIELD IS EMPTY");
                                return;
                            }
                        }
                        if (orderPhyDt.Rows.Count > 0)
                        {
                            try
                            {
                                itemType = string.Concat(itemType, "PRINT/");
                                orderNumber1 = SampleRequestFormController.Instance.InsertOrders(sRFParameters, orderPhyDt, TypeOfSale.Physical.ToString(), "PRINT");
                                //orderNumber1 = string.Empty;
                                if (!string.IsNullOrEmpty(orderNumber1))
                                {
                                    printSuccess = 1;
                                    logContent = string.Concat(logContent, "\r\n Print Order Number -->", orderNumber1);
                                    // orders inserted in UM DB.
                                    DnnLog.Info("Physical Orders inserted in UM DB");
                                }
                                else
                                {
                                    logContent = string.Concat(logContent, "\r\n Print Order Failed");
                                    redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                    // failure while insertig..
                                    DnnLog.Info("Physical Orders failure while inserting in UM DB");
                                    failedItems = string.Concat(failedItems, "PRINT/");                                   
                                }
                            }
                            catch (Exception ex) { failedItems = string.Concat(failedItems, "PRINT/"); logContent = string.Concat(logContent, "\r\n Print Order Failed with exception \r\n", ex.Message); LogFileWrite(ex); }
                            if (printSuccess != 1)
                            {
                                errPrintISBN = string.Concat(errPrintISBN, " -- > Product Sk -->");
                                foreach (string item in itemsPrint)
                                    errPrintISBN = string.Concat(errPrintISBN, ',', Null.SetNullString(item));
                                isbns = string.IsNullOrEmpty(errPrintISBN) ? isbns : string.Concat(isbns, ',', errPrintISBN);
                            }
                        }
                        else
                        {
                        }

                        if (orderDigDt.Rows.Count > 0)
                        {
                            try
                            {                                
                                orderNumber2 = SampleRequestFormController.Instance.InsertOrders(sRFParameters, orderDigDt, TypeOfSale.SSO.ToString(), "DIGITAL");
                                if (!string.IsNullOrEmpty(orderNumber2))
                                {
                                    logContent = string.Concat(logContent, "\r\n Digital Order Number -->", orderNumber2);
                                    // orders inserted in UM DB.
                                    DnnLog.Info("Digital Orders inserted in UM DB");

                                    try
                                    {
                                        int timeoutValue = 0;
                                        if (itemsCourse.Count != 0)
                                        {
                                            timeoutValue++;
                                            itemType = string.Concat(itemType, "EBOOK/");
                                        }
                                        if (itemsDigitalEAC.Count > 0)
                                        {
                                            timeoutValue++;
                                            itemType = string.Concat(itemType, "EAC/");
                                        }
                                        if (itemsDigitalESS.Count != 0)
                                        {
                                            timeoutValue++;
                                            itemType = string.Concat(itemType, "ESS/");
                                        }

                                        Session["TimeOutValue"] = null;
                                        switch (timeoutValue)
                                        {
                                            case 2:
                                                Session["TimeOutValue"] = 30000;
                                                break;
                                            case 3:
                                                Session["TimeOutValue"] = 20000;
                                                break;
                                            default:
                                                Session["TimeOutValue"] = 60000;
                                                break;
                                        }
                                        Session["SSOURL"] = null;
                                        Session["SSOREQISBNS"] = null;
                                       
                                        // course smart items
                                        if (itemsCourse.Count != 0)
                                        {
                                            try
                                            {
                                                if (!string.IsNullOrEmpty(sRFParameters.SecondaryEmail))
                                                {
                                                    //  DnnLog.Info("Ranjith--> before sso digital call-->" + sRFParameters.SecondaryEmail);
                                                    logContent = string.Concat(logContent, "\r\n itemsCourse --> Processing : SecondaryEmail-->", sRFParameters.SecondaryEmail);
                                                    digitalSuccess = postController.PostingDetailsToServer(sRFParameters, itemsCourse, ItemType.SSOREEDAMCOURSESMART);//== 0 ? printSuccess : 2;
                                                    logContent = string.Concat(logContent, "\r\n itemsCourse --> ItemType-->SSOREEDAMCOURSESMART --> Processed --> ISBNS -->" + Null.SetNullString(Session["SSOREQISBNS"]));
                                                    failedItems = digitalSuccess == 0 ? failedItems : string.Concat(failedItems, "EBOOK/");
                                                    errEbookISBN = digitalSuccess == 0 ? string.Empty : string.Concat(errEbookISBN, Null.SetNullString(Session["SSOREQISBNS"]));
                                                    SendErrorEmails(digitalSuccess, visitor.UserName, errEbookISBN, string.Concat("<br/><a href=" + Null.SetNullString(Session["SSOURL"]) + ">", Null.SetNullString(Session["SSOURL"]), "</a>"));
                                                    logContent = string.Concat(logContent, errEmailLog);
                                                    isbns = string.IsNullOrEmpty(errEbookISBN) ? isbns : string.Concat(isbns, ',', errEbookISBN);
                                                }
                                                else
                                                {
                                                    ScriptManager.RegisterStartupScript(Page, GetType(), "SRFSubmit", "<script>SRFSubmit();</script>", false);
                                                    logContent = string.Concat(logContent, "\r\n itemsCourse --> Processing : SecondaryEmail-->EMPTY");
                                                    return;
                                                }
                                            }
                                            catch (Exception ex) { failedItems = string.Concat(failedItems, "EBOOK/"); logContent = string.Concat(logContent, "\r\n itemsCourse --> Processing failed \r\n", ex.Message); LogFileWrite(ex); }
                                        }
                                        Session["SSOURL"] = null;
                                        Session["SSOREQISBNS"] = null;
                                        // eac in same api call
                                        if (itemsDigitalEAC.Count != 0)
                                        {
                                            try
                                            {
                                                logContent = string.Concat(logContent, "\r\n itemsDigitalEAC --> Processing");
                                                digitalSuccess = postController.PostingDetailsToServer(sRFParameters, itemsDigitalEAC, ItemType.SSOREEDAMDIGITAL);//==0 ? printSuccess : 2;
                                                //digitalSuccess = -3;
                                                logContent = string.Concat(logContent, "\r\n itemsDigitalEAC --> ItemType-->SSOREEDAMDIGITAL --> Processed --> ISBNS -->" + Null.SetNullString(Session["SSOREQISBNS"]));
                                                failedItems = digitalSuccess == 0 ? failedItems : string.Concat(failedItems, "EAC/");
                                                foreach (SRFSupplements item in teachingSuplements)
                                                {
                                                    if (!string.IsNullOrEmpty(item.MediaType) && item.MediaType.ToUpper() == "EAC")
                                                        errEACISBN = digitalSuccess == 0 ? string.Empty : string.Concat(errEACISBN,',', Null.SetNullString(item.ISBN));
                                                }
                                                errEACISBN=errEACISBN.Trim().Trim(',');
                                                SendErrorEmails(digitalSuccess, visitor.UserName, errEACISBN, string.Concat("<br/><a href=" + Null.SetNullString(Session["SSOURL"]) + ">", Null.SetNullString(Session["SSOURL"]), "</a>"));
                                                logContent = string.Concat(logContent, errEmailLog);
                                                isbns = string.IsNullOrEmpty(errEACISBN) ? isbns : string.Concat(isbns, ',', errEACISBN);
                                            }
                                            catch (Exception ex) { failedItems = string.Concat(failedItems, "EAC/"); logContent = string.Concat(logContent, "\r\n itemsDigitalEAC --> Processing failed \r\n", ex.Message); LogFileWrite(ex); }
                                        }
                                        Session["SSOURL"] = null;
                                        Session["SSOREQISBNS"] = null;
                                        //ess in same api call
                                        if (itemsDigitalESS.Count != 0)
                                        {
                                            try
                                            {
                                                string err1=string.Empty,err2=string.Empty,err3=string.Empty,err4=string.Empty, ssoUrl=string.Empty;
                                                int essErrCnt = 0;
                                                foreach (string essItem in itemsDigitalESS)
                                                {
                                                    sRFParameters.SecondaryEmail = sRFParameters.Email;
                                                    logContent = string.Concat(logContent, "\r\n itemsDigitalESS --> Processing ");
                                                    List<string> essItemList = new List<string>();
                                                    essItemList.Add(essItem);
                                                    digitalSuccess = postController.PostingDetailsToServer(sRFParameters, essItemList, ItemType.SSOREEDAMCOURSESMART);//== 0 ? printSuccess : 2;
                                                    logContent = string.Concat(logContent, "\r\n itemsDigitalESS --> ItemType-->SSOREEDAMCOURSESMART --> Processed --> ISBNS -->" + Null.SetNullString(Session["SSOREQISBNS"]));
                                                    errESSISBN = digitalSuccess == 0 ? errESSISBN : string.Concat(errESSISBN, ',', Null.SetNullString(Session["SSOREQISBNS"]));
                                                    if (digitalSuccess == -11)
                                                        err1 = string.Concat(err1, ',', Null.SetNullString(Session["SSOREQISBNS"]));
                                                    else if (digitalSuccess == -15)
                                                        err2 = string.Concat(err2, ',', Null.SetNullString(Session["SSOREQISBNS"]));
                                                    else if (digitalSuccess == -14)
                                                        err3 = string.Concat(err3, ',', Null.SetNullString(Session["SSOREQISBNS"]));
                                                    else if (digitalSuccess == -13)
                                                        err4 = string.Concat(err4, ',', Null.SetNullString(Session["SSOREQISBNS"]));
                                                    if (digitalSuccess != 0)
                                                    {
                                                        ssoUrl = ssoUrl == string.Empty ? string.Concat(ssoUrl, "<a href=" + Null.SetNullString(Session["SSOURL"]) + ">", Null.SetNullString(Session["SSOURL"]), "</a>") : string.Concat(ssoUrl, "<br/><a href=" + Null.SetNullString(Session["SSOURL"]) + ">", Null.SetNullString(Session["SSOURL"]), "</a>");
                                                        logContent = string.Concat(logContent, "\r\n Error SSO Url -->  ", ssoUrl);
                                                        essErrCnt++;
                                                    }
                                                }
                                                if (!string.IsNullOrEmpty(err1))
                                                {
                                                    SendErrorEmails(-11, visitor.UserName, err1.Trim().Trim(','),ssoUrl);
                                                    logContent = string.Concat(logContent, errEmailLog);
                                                }
                                                if (!string.IsNullOrEmpty(err2))
                                                {
                                                    SendErrorEmails(-15, visitor.UserName, err2.Trim().Trim(','),ssoUrl);
                                                    logContent = string.Concat(logContent, errEmailLog);
                                                }
                                                if (!string.IsNullOrEmpty(err3))
                                                {
                                                    SendErrorEmails(-14, visitor.UserName, err3.Trim().Trim(','),ssoUrl);
                                                    logContent = string.Concat(logContent, errEmailLog);
                                                }
                                                if (!string.IsNullOrEmpty(err4))
                                                {
                                                    SendErrorEmails(-13, visitor.UserName, err4.Trim().Trim(','),ssoUrl);
                                                    logContent = string.Concat(logContent, errEmailLog);
                                                }                                          
                                                
                                                isbns = string.IsNullOrEmpty(errESSISBN) ? isbns : string.Concat(isbns, ',', errESSISBN);

                                                failedItems = essErrCnt > 0 ? string.Concat(failedItems, "ESS/") : failedItems;
                                                logContent = string.Concat(logContent, "\r\n Error ESS Count -->  ", essErrCnt);
                                            }
                                            catch (Exception ex) { failedItems = string.Concat(failedItems, "ESS/"); logContent = string.Concat(logContent, "\r\n itemsDigitalESS --> Processing failed \r\n", ex.Message); LogFileWrite(ex); }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        logContent = string.Concat(logContent, "\r\n Digital SSO Call Failures-->", ex.Message);
                                        digitalSuccess = 2;
                                        LogFileWrite(ex);
                                    }


                                    try
                                    {
                                        // Magallean service for digital items
                                        if (digitalSuccess == 0)
                                        {
                                            logContent = string.Concat(logContent, "\r\n Digital Magellan Started");
                                            mgRes = postController.PostingDetailsToServer(sRFParameters, itemsDigitalESS.Concat(itemsDigitalEAC.Concat(itemsCourse)).ToList(), ItemType.NONE);
                                            logContent = string.Concat(logContent, "\r\n Digital Magellan Completed");
                                        }
                                    }
                                    catch (Exception ex)
                                    { LogFileWrite(ex); logContent = string.Concat(logContent, "\r\n Digital Magellan Failed-->", ex.Message); }
                                }
                                else
                                {
                                    logContent = string.Concat(logContent, "\r\n Digital order failed");
                                    // failure while insertig..
                                    DnnLog.Info("Digital Orders failure while inserting in UM DB");
                                }
                            }
                            catch (Exception ex) { logContent = string.Concat(logContent, "\r\n Digital order failed with exception \r\n", ex.Message); LogFileWrite(ex); }
                        }
                        DnnLog.Info("SRF ORDERS --> \r\n Physical Order Number=" + orderNumber1 + "\r\n Digital Order Number=" + orderNumber2);
                        try
                        {
                            // mangellan web service call
                            if (itemsPrint.Count != 0 && printSuccess == 1 && mgRes == 0)
                            {
                                logContent = string.Concat(logContent, "\r\n Print magellan Started");
                                mgRes = postController.PostingDetailsToServer(sRFParameters, itemsPrint, ItemType.PRINT);
                                logContent = string.Concat(logContent, "\r\n Print magellan Completed");
                            }
                        }
                        catch (Exception ex) { LogFileWrite(ex); }



                        switch (mgRes)
                        {
                            case -1:
                                logContent = string.Concat(logContent, "\r\n Magellan returned some sort of error response.");
                                SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(visitor.UserName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR7.htm"), string.Empty, string.Empty), "Sample Request Magellan Failure");
                                break;
                            case -3:
                                logContent = string.Concat(logContent, "\r\n A sample request order could not be sent to Magellan due to an internal 500 error");
                                SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(visitor.UserName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR8.htm"), string.Empty, string.Empty), "Sample Request Magellan Failure");
                                break;
                            default:
                                logContent = string.Concat(logContent, "\r\n Magellan Calls completed successfully");
                                break;
                        }
                        sRFParameters.Country = SRFCountry.SelectedItem.Text;

                        //bool bprint = false, beBook = false, bEac = false, bEss = false;
                        itemType = itemType.Trim('/'); failedItems = failedItems.Trim('/');

                        logContent = string.Concat(logContent, "\r\n itemType --> ", itemType, "\r\n failedItems --> ", failedItems);

                        switch (itemType)
                        {
                            case "PRINT/EBOOK/EAC/ESS":
                                switch (failedItems)
                                {
                                    case "PRINT/EBOOK/EAC/ESS":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "PRINT/EBOOK/EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em3");
                                        break;
                                    case "PRINT/EBOOK/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em3");
                                        break;
                                    case "PRINT/EAC/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em2");
                                        break;
                                    case "EBOOK/EAC/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em1");
                                        break;
                                    case "PRINT/EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em3");
                                        break;
                                    case "PRINT/EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em2");
                                        break;
                                    case "PRINT/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em2");
                                        break;
                                    case "EBOOK/EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em5");
                                        break;
                                    case "EBOOK/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em5");
                                        break;
                                    case "EAC/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em4");
                                        break;
                                    case "PRINT":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em8");
                                        break;
                                    case "EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em7");
                                        break;
                                    case "EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em6");
                                        break;
                                    case "ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em6");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "PRINT/EBOOK/EAC":
                                switch (failedItems)
                                {
                                    case "PRINT/EBOOK/EAC":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "PRINT/EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em3");
                                        break;
                                    case "PRINT/EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em2");
                                        break;
                                    case "EBOOK/EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em9");
                                        break;
                                    case "PRINT":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em8");
                                        break;
                                    case "EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em7");
                                        break;
                                    case "EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em6");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "PRINT/EBOOK/ESS":
                                switch (failedItems)
                                {
                                    case "PRINT/EBOOK/ESS":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "PRINT/EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em3");
                                        break;
                                    case "PRINT/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em2");
                                        break;
                                    case "EBOOK/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em9");
                                        break;
                                    case "PRINT":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em8");
                                        break;
                                    case "EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em7");
                                        break;
                                    case "ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em6");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "PRINT/EAC/ESS":
                                switch (failedItems)
                                {
                                    case "PRINT/EAC/ESS":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "PRINT/EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em2");
                                        break;
                                    case "PRINT/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em2");
                                        break;
                                    case "EAC/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em1");
                                        break;
                                    case "PRINT":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em8");
                                        break;
                                    case "EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em6");
                                        break;
                                    case "ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em6");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "EBOOK/EAC/ESS":
                                switch (failedItems)
                                {
                                    case "EBOOK/EAC/ESS":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "EBOOK/EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em11");
                                        break;
                                    case "EBOOK/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em11");
                                        break;
                                    case "EAC/ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em10");
                                        break;
                                    case "EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em13");
                                        break;
                                    case "EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em12");
                                        break;
                                    case "ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em12");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "PRINT/EBOOK":
                                switch (failedItems)
                                {
                                    case "PRINT/EBOOK":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "PRINT":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em8");
                                        break;
                                    case "EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em7");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "PRINT/EAC":
                                switch (failedItems)
                                {
                                    case "PRINT/EAC":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "PRINT":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em8");
                                        break;
                                    case "EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em14");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "PRINT/ESS":
                                switch (failedItems)
                                {
                                    case "PRINT/ESS":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "PRINT":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em8");
                                        break;
                                    case "ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em14");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "EBOOK/EAC":
                                switch (failedItems)
                                {
                                    case "EBOOK/EAC":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em13");
                                        break;
                                    case "EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em12");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "EBOOK/ESS":
                                switch (failedItems)
                                {
                                    case "EBOOK/ESS":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "EBOOK":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em13");
                                        break;
                                    case "ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em12");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "EAC/ESS":
                                switch (failedItems)
                                {
                                    case "EAC/ESS":
                                        redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                        break;
                                    case "EAC":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em12");
                                        break;
                                    case "ESS":
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf&fc=em12");
                                        break;
                                    default:
                                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                        break;
                                }
                                break;
                            case "PRINT":
                                if (failedItems == "PRINT")
                                    redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                else
                                    redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                break;
                            case "EBOOK":
                                if (failedItems == "EBOOK")
                                    redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                else
                                    redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                break;
                            case "EAC":
                                if (failedItems == "EAC")
                                    redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                else
                                    redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                break;
                            case "ESS":
                                if (failedItems == "ESS")
                                    redirectUrl = string.Concat("/request-error?div=" + Request.QueryString["div"], "&ut=sso");
                                else
                                    redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");
                                break;

                        }

                        try
                        {
                            salesRepDetails = Session["SalesRep" + HttpContext.Current.Session.SessionID].ToString().Split('|');
                            //   LogValues("Sales rep values-->" + salesRepDetails[1]+ salesRepDetails[0]);
                            if (SRFAddressDiv.Visible == false)
                            {
                                logContent = string.Concat(logContent, "\r\n Sales Rep Details --> ", salesRepDetails[0], " --> ", salesRepDetails[1]);
                                // SampleRequestFormController.Instance.SRFSendMail(salesRepDetails[1], UpdateMailBody(formatItems, teachingSuplements, salesRepDetails[0], "DIGITAL ONLY", sRFParameters, Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepVFDig.htm"), orderNumber1, orderNumber2), "Record of digital sample");
                                ProcessEmailItems(formatItems, teachingSuplements, salesRepDetails[0], salesRepDetails[1], orderNumber1, orderNumber2, sRFParameters, string.Empty, string.Empty, Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepVFDig.htm"), isbns.Trim().Trim(','),false);
                                logContent = string.Concat(logContent, instructorInfo);
                                logContent = string.Concat(logContent, "\r\n VERIFIED USER DIGITAL ONLY EMAIL SENT");
                            }
                            else
                            {                                
                                logContent = string.Concat(logContent, "\r\n Sales Rep Details --> ", salesRepDetails[0], " --> ", salesRepDetails[1]);
                                ProcessEmailItems(formatItems, teachingSuplements, salesRepDetails[0], salesRepDetails[1], orderNumber1, orderNumber2, sRFParameters, "SSO verified instructor, Sample Request - Requires action", Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepVF.htm"), Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepVFDig.htm"),isbns.Trim().Trim(','),true);
                                logContent = string.Concat(logContent, instructorInfo);
                                logContent = string.Concat(logContent, "\r\n VERIFIED USER --> MIXED(DIGITAL + PHYSICAL) EMAIL SENT");
                                // SampleRequestFormController.Instance.SendMailTorep(salesRepDetails[1], UpdateMailBody(formatItems,teachingSuplements,salesRepDetails[0], "GENERAL", sRFParameters, Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepVF.htm"), orderNumber1, orderNumber2), "SSO verified instructor, Sample Request - Requires action");
                            }
                        }
                        catch (Exception ex) { LogFileWrite(ex); }
                        //redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=vf");

                        //if (digitalSuccess < 0)                          
                        //else                           
                        check = 1;
                    }
                    else
                    {
                        check = 2;
                        sRFParameters.Country = SRFCountry.SelectedItem.Text;
                        try
                        {
                            salesRepDetails = Session["SalesRep" + HttpContext.Current.Session.SessionID].ToString().Split('|');
                            if (SRFAddressDiv.Visible == false)
                            {
                                logContent = string.Concat(logContent, "\r\n Sales Rep Details --> ", salesRepDetails[0], " --> ", salesRepDetails[1]);
                                SampleRequestFormController.Instance.SRFSendMail(salesRepDetails[1], UpdateMailBody(formatItems, teachingSuplements, salesRepDetails[0], "DIGITAL ONLY", sRFParameters, Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepNVFDig.htm"), string.Empty, string.Empty), "Non-SSO instructor, sample Request - Requires Action");
                                logContent = string.Concat(logContent, "\r\n NON VERIFIED USER DIGITAL ONLY EMAIL SENT");
                            }
                            else
                            {
                                logContent = string.Concat(logContent, "\r\n Sales Rep Details --> ", salesRepDetails[0], " --> ", salesRepDetails[1]);
                                ProcessEmailItemsForNVFUsers(formatItems, teachingSuplements, salesRepDetails[1], salesRepDetails[0]);
                                logContent = string.Concat(logContent, instructorInfo);
                                logContent = string.Concat(logContent, "\r\n NON VERIFIED USER --> MIXED(DIGITAL + PHYSICAL) EMAIL SENT");
                                //SampleRequestFormController.Instance.SendMailTorep(salesRepDetails[1], UpdateMailBody(formatItems, teachingSuplements,salesRepDetails[0], "GENERAL", sRFParameters, Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepNVF.htm"), string.Empty, string.Empty), "Non-SSO instructor, sample Request - Requires Action");
                            }
                        }
                        catch (Exception ex) { LogFileWrite(ex); }
                        redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=nvf");
                    }
                }
                else
                {
                    check = 2;
                    sRFParameters.Country = SRFCountry.SelectedItem.Text;
                    try
                    {
                        salesRepDetails = Session["SalesRep" + HttpContext.Current.Session.SessionID].ToString().Split('|');
                        if (SRFAddressDiv.Visible == false)
                        {
                            logContent = string.Concat(logContent, "\r\n Sales Rep Details --> ", salesRepDetails[0], " --> ", salesRepDetails[1]);
                            SampleRequestFormController.Instance.SRFSendMail(salesRepDetails[1], UpdateMailBody(formatItems, teachingSuplements, salesRepDetails[0], "DIGITAL ONLY", sRFParameters, Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepNVFDig.htm"), string.Empty, string.Empty), "Non-SSO instructor, sample Request - Requires Action");
                            logContent = string.Concat(logContent, "\r\n ANONYMOUS USER DIGITAL ONLY EMAIL SENT");
                        }
                        else
                        {
                            logContent = string.Concat(logContent, "\r\n Sales Rep Details --> ", salesRepDetails[0], " --> ", salesRepDetails[1]);
                            ProcessEmailItemsForNVFUsers(formatItems, teachingSuplements, salesRepDetails[1], salesRepDetails[0]); 
                            logContent = string.Concat(logContent, instructorInfo);
                            logContent = string.Concat(logContent, "\r\n ANONYMOUS USER --> MIXED(DIGITAL + PHYSICAL) EMAIL SENT");
                            //SampleRequestFormController.Instance.SendMailTorep(salesRepDetails[1], UpdateMailBody(formatItems, teachingSuplements,salesRepDetails[0], "GENERAL", sRFParameters, Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepNVF.htm"), string.Empty, string.Empty), "Non-SSO instructor, sample Request - Requires Action");
                        }
                    }
                    catch (Exception ex) { LogFileWrite(ex); }
                    redirectUrl = string.Concat("/request-success?div=" + Request.QueryString["div"], "&ut=nvf");
                }
                Session["CoreSupplementsProducts"] = null;
                Response.Redirect(redirectUrl);
            }
            catch (Exception ex)
            {
                if (ex.Source != "mscorlib" || ex.Message.ToLower() != "thread was being aborted.")
                {
                    logContent = string.Concat(logContent, "\r\n SEND REQUEST BUTTON CLICK EVENT EXCEPTION --> " + ex.Message);
                    LogFileWrite(ex);
                    DnnLog.Error("Exception in Sending Request :" + ex.Message);
                }
            }
            finally
            {
                logContent = string.Concat(logContent, "\r\n redirectUrl --> " + redirectUrl);
                LogValues(logContent);
                Response.Redirect(redirectUrl);
                ScriptManager.RegisterStartupScript(Page, GetType(), "EndUpdateProgress", "<script>EndUpdateProgress();</script>", false);
            }
        }

        /// <summary>
        /// Sends SSO error emails to Castley Will
        /// </summary>
        /// <param name="responseCode"></param>
        /// <param name="userName"></param>
        /// <param name="isbns"></param>
        protected void SendErrorEmails(int responseCode, string userName, string isbns, string ssoUrl)
        {
            try
            {
                switch (responseCode)
                {
                    case -1:
                        errEmailLog = string.Concat(errEmailLog, "\r\n SSO not accepting one of the passed variables in relation to the CreateIrOrder call.");
                        SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(userName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR1.htm"), isbns, ssoUrl), "Sample Request SSO Failure");
                        break;
                    case -3:
                        errEmailLog = string.Concat(errEmailLog, "\r\n SSO timeout error in relation to the CreateIrOrder call.");
                        SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(userName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR3.htm"), isbns, ssoUrl), "Sample Request SSO Failure");
                        break;
                    case -4:
                        errEmailLog = string.Concat(errEmailLog, "\r\n SSO 400 internal server error in relation to the CreateIrOrder call.");
                        SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(userName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR10.htm"), isbns, ssoUrl), "Sample Request SSO Failure");
                        break;
                    case -5:
                        errEmailLog = string.Concat(errEmailLog, "\r\n SSO 500 internal server error in relation to the CreateIrOrder call.");
                        SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(userName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR2.htm"), isbns, ssoUrl), "Sample Request SSO Failure");
                        break;
                    case -11:
                        errEmailLog = string.Concat(errEmailLog, "\r\n  SSO not accepting one of the passed variables in relation to the GenerateConsumeIacForEisbn call.");
                        SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(userName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR4.htm"), isbns, ssoUrl), "Sample Request SSO Failure");
                        break;
                    case -13:
                        errEmailLog = string.Concat(errEmailLog, "\r\n SSO timeout error in relation to the GenerateConsumeIacForEisbn call.");
                        SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(userName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR6.htm"), isbns, ssoUrl), "Sample Request SSO Failure");
                        break;
                    case -14:
                        errEmailLog = string.Concat(errEmailLog, "\r\n SSO 400 internal server error in relation to the GenerateConsumeIacForEisbn call.");
                        SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(userName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR9.htm"), isbns, ssoUrl), "Sample Request SSO Failure");
                        break;
                    case -15:
                        errEmailLog = string.Concat(errEmailLog, "\r\n SSO 500 internal server error in relation to the GenerateConsumeIacForEisbn call.");
                        SampleRequestFormController.Instance.SRFSendMail(ConfigurationManager.AppSettings["SSO_ERROR_TO_ADDRESS"].ToString(), UpdateSSOErrorMailBody(userName, string.Concat(DateTime.Now.ToShortDateString(), " ", DateTime.Now.ToShortTimeString()), Server.MapPath("~/Portals/0/MailTemplates/SRFSSOERROR5.htm"), isbns, ssoUrl), "Sample Request SSO Failure");
                        break;
                    default:
                        errEmailLog = string.Concat(errEmailLog, "\r\n SSO Call completed successfully for the isbns --> ",isbns);
                        break;
                }
            }
            catch (Exception ex) { errEmailLog = ex.Message;  LogFileWrite(ex); }
        }

        /// <summary>
        /// Processes emails for print and digital items seperately for SSO Verified Instructors
        /// </summary>
        /// <param name="formatItems"></param>
        /// <param name="teachingSuplements"></param>
        /// <param name="repName"></param>
        /// <param name="repEmail"></param>
        /// <param name="orderNumber1"></param>
        /// <param name="orderNumber2"></param>
        /// <param name="sRFParameters"></param>
        /// <param name="title"></param>
        /// <param name="printTemplatePath"></param>
        /// <param name="digitalTemplatePath"></param>
        private void ProcessEmailItems(List<SRFItems> formatItems, List<SRFSupplements> teachingSuplements, string repName, string repEmail, string orderNumber1, string orderNumber2, SRFParameters sRFParameters, string title,string printTemplatePath,string digitalTemplatePath,string errorIsbns,bool printMailFlag)
        {
            string logContent = string.Empty;
            try
            {
                logContent= string.Concat(logContent,"\r\n Process Email Items for Verified Users");
                List<SRFItems> digitalSRFItems = new List<SRFItems>();
                List<SRFItems> printSRFItems = new List<SRFItems>();
                List<SRFSupplements> digitalSRFSupplements = new List<SRFSupplements>();
                List<SRFSupplements> printSRFSupplements = new List<SRFSupplements>();
                string fs = string.Empty, ss = string.Empty, dig = string.Empty, prn = string.Empty;
                logContent = string.Concat(logContent, "\r\n Error Isbns --> ",errorIsbns);
                var errIsbns = errorIsbns.Split(',').Where(val => !string.IsNullOrEmpty(val)).ToList();
                logContent = string.Concat(logContent, "\r\n Error Isbns count --> ", errIsbns.Count(),", length --> ",errorIsbns.Length);
                logContent = string.Concat(logContent, "\r\n FormatItems Count --> ", formatItems.Count);
                logContent = string.Concat(logContent, "\r\n SRFSupplements Count --> ", teachingSuplements.Count);                
                foreach (SRFItems srfItem in formatItems)
                {
                    var errCheck = 0;
                    if (errIsbns.Count > 0)
                    {
                        foreach (string eIsbn in errIsbns)
                        {
                            logContent = string.Concat(logContent, "\r\n srfItem.ISBN-->", srfItem.ISBN, "eIsbn.Trim()-->", eIsbn.Trim());
                            if (srfItem.ISBN == eIsbn.Trim())
                            {
                                errCheck++;
                                break;
                            }
                        }
                    }
                    if (errCheck==0)
                    {
                        if (!string.IsNullOrEmpty(srfItem.FormatType) && srfItem.FormatType.ToLower() == "ebook" && !digitalSRFItems.Contains(srfItem))
                        {
                            dig = string.Concat(dig, "\r\n b ISBN-->", srfItem.ISBN, ", FORMAT-->", srfItem.FormatType, ", TITLE-->", srfItem.ProductName);
                            digitalSRFItems.Add(srfItem);
                        }
                        else
                        {
                            if (!printSRFItems.Contains(srfItem))
                            {
                                prn = string.Concat(prn, "\r\n b ISBN-->", srfItem.ISBN, ", FORMAT-->", srfItem.FormatType, ", TITLE-->", srfItem.ProductName);
                                printSRFItems.Add(srfItem);
                            }
                        }
                    }     
                    fs = string.Concat(fs,"\r\n ISBN-->", srfItem.ISBN, ", FORMAT-->", srfItem.FormatType, ", TITLE-->", srfItem.ProductName);
                }

                foreach (SRFSupplements srfSupplements in teachingSuplements)
                {
                    var errCheck = 0;
                    if (errIsbns.Count > 0)
                    {
                        foreach (string eIsbn in errIsbns)
                        {
                            logContent = string.Concat(logContent, "\r\n srfItem.ISBN-->", srfSupplements.ISBN, "eIsbn.Trim()-->", eIsbn.Trim());
                            if (srfSupplements.ISBN == eIsbn.Trim())
                            {
                                errCheck++;
                                break;
                            }
                        }
                    }
                    if (errCheck==0 && srfSupplements.IsCore != true)
                    {
                        if (!string.IsNullOrEmpty(srfSupplements.MediaType) && (srfSupplements.MediaType.ToUpper() == "EAC" || srfSupplements.MediaType.ToUpper() == "ESS") && !digitalSRFSupplements.Contains(srfSupplements) && !digitalSRFItems.Exists(x => x.ISBN == srfSupplements.ISBN))
                        {
                            dig = string.Concat(dig, "\r\n b ISBN-->", srfSupplements.ISBN, ", MEDIA TYPE-->", srfSupplements.MediaType, ", TITLE-->", srfSupplements.Title);
                            digitalSRFSupplements.Add(srfSupplements);
                        }
                        else
                        {
                            if (!printSRFSupplements.Contains(srfSupplements) && !printSRFItems.Exists(x => x.ISBN == srfSupplements.ISBN))
                            {
                                prn = string.Concat(prn, "\r\n b ISBN-->", srfSupplements.ISBN, ", MEDIA TYPE-->", srfSupplements.MediaType, ", TITLE-->", srfSupplements.Title);
                                printSRFSupplements.Add(srfSupplements);
                            }
                        }
                    }

                    ss = string.Concat(ss, "\r\n ISBN-->", srfSupplements.ISBN, ", MEDIA TYPE-->", srfSupplements.MediaType, ", TITLE-->", srfSupplements.Title, srfSupplements.MediaType, ", ISCORE-->", srfSupplements.IsCore);
                }
                logContent = string.Concat(logContent, "\r\n FORMAT ITEMS: ", fs, "\r\n SUPPLEMENTS ITEMS: ", ss);
                
                logContent = string.Concat(logContent,"\r\n \r\n PRINT ITEMS : ", prn, "\r\n DIGITAL ITEMS CHECK : ", dig);
                logContent = string.Concat(logContent, "\r\n \r\n printSRFItems.Count  : ", printSRFItems.Count, "\r\n printSRFSupplements.Count : ", +printSRFSupplements.Count);
                logContent = string.Concat(logContent, "\r\n \r\n digitalSRFItems.Count  : ", digitalSRFItems.Count, "\r\n digitalSRFSupplements.Count : ", +digitalSRFSupplements.Count);

                if (printSRFItems.Count + printSRFSupplements.Count > 0 && printMailFlag)
                {
                    string emailBody=UpdateMailBody(printSRFItems, printSRFSupplements, repName, "GENERAL", sRFParameters, printTemplatePath, orderNumber1, string.Empty);
                    if(emailBody!=string.Empty)
                        SampleRequestFormController.Instance.SRFSendMail(repEmail, emailBody, title);
                }
                if (digitalSRFItems.Count + digitalSRFSupplements.Count > 0)
                    SampleRequestFormController.Instance.SRFSendMail(repEmail, UpdateMailBody(digitalSRFItems, digitalSRFSupplements, repName, "DIGITAL ONLY", sRFParameters, digitalTemplatePath, string.Empty, orderNumber2), "Record of digital sample");
            }
            catch (Exception ex) { logContent = ex.Message; LogFileWrite(ex); }
            finally { instructorInfo = logContent; }
        }

        /// <summary>
        /// Process emails for Non verified instructors or anonymous users 
        /// </summary>
        /// <param name="formatItems"></param>
        /// <param name="teachingSuplements"></param>
        /// <param name="toAddress"></param>
        /// <param name="salesRepName"></param>
        private void ProcessEmailItemsForNVFUsers(List<SRFItems> formatItems, List<SRFSupplements> teachingSuplements, string toAddress, string salesRepName)
        {
            string logContent = string.Empty;
            try
            {
                logContent = string.Concat(logContent, "\r\n Process Email Items for Not Verified/Anonymous Users");
                List<SRFItems> newSRFItems = new List<SRFItems>();
                List<SRFSupplements> newSRFSupplements = new List<SRFSupplements>();
                string fs = string.Empty, ss = string.Empty;
                foreach (SRFItems srfItem in formatItems)
                {
                    if (!newSRFItems.Contains(srfItem))
                        newSRFItems.Add(srfItem);
                    fs = string.Concat(fs, "\r\n ISBN-->", srfItem.ISBN, ", FORMAT-->", srfItem.FormatType, ", TITLE-->", srfItem.ProductName);
                }               
                if (teachingSuplements != null && teachingSuplements.Count > 1)
                {
                    foreach (SRFSupplements srfSupplements in teachingSuplements)
                    {
                        if (!newSRFSupplements.Contains(srfSupplements) && srfSupplements.IsCore != true)
                            newSRFSupplements.Add(srfSupplements);
                        ss = string.Concat(ss, "\r\n ISBN-->", srfSupplements.ISBN, ", MEDIA TYPE-->", ", TITLE-->", srfSupplements.Title, srfSupplements.MediaType, ", ISCORE-->", srfSupplements.IsCore);
                    }
                }
                else if (teachingSuplements != null && teachingSuplements.Count > 0)
                {
                    newSRFSupplements.Add(new SRFSupplements() { ISBN = "PVRJ", Title = "Expressed interest in teaching supplements" });
                    ss = string.Concat(ss, "\r\n Expressed interest in teaching supplements");
                }
                else { }                
                logContent = string.Concat(logContent, "\r\n FORMAT ITEMS: ", fs, "\r\n SUPPLEMENTS ITEMS: ", ss);
                SampleRequestFormController.Instance.SRFSendMail(toAddress, UpdateMailBody(newSRFItems, newSRFSupplements, salesRepName, "GENERAL", sRFParameters, Server.MapPath("~/Portals/0/MailTemplates/SRF_SendEmailToRepNVF.htm"), string.Empty, string.Empty), "Non-SSO instructor, sample Request - Requires Action");
            }
            catch (Exception ex) { logContent = string.Concat(logContent, ex.Message); LogFileWrite(ex); }
            finally { instructorInfo = logContent; }
        }

        private void FillInstructorInfo(string UserLoginName)
        {
            try
            {
                sRFParameters = SampleRequestFormController.Instance.GetInstructorInfo(UserLoginName);
                if (sRFParameters != null && !string.IsNullOrEmpty(UserLoginName))
                {
                    string value = !string.IsNullOrEmpty(sRFParameters.Email) ? sRFParameters.Email : "its null";
                    //  LogValues("sRFParameters : " + value);
                    instructorInfo = string.Concat(instructorInfo, "\r\n Email value --> ", value);

                    SRFPersonalName.Value = sRFParameters.Name;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Name --> ", sRFParameters.Name);
                    SRFEmailid.Value = sRFParameters.Email;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Email --> ", sRFParameters.Email);
                    SRFShippingAddressTxt1.Value = sRFParameters.Address1;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Address1 --> ", sRFParameters.Address1);
                    SRFShippingAddressTxt2.Value = sRFParameters.Address2;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Address2 --> ", sRFParameters.Address2);
                    SRFShippingAddressTxt3.Value = sRFParameters.Address3;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Address3 --> ", sRFParameters.Address3);
                    SRFSuburbTxt.Value = sRFParameters.SubUrb;
                    instructorInfo = string.Concat(instructorInfo, "\r\n SubUrb --> ", sRFParameters.SubUrb);
                    SRFPostcode.Value = sRFParameters.PostalCode > 0 ? Null.SetNullString(sRFParameters.PostalCode) : string.Empty;
                    instructorInfo = string.Concat(instructorInfo, "\r\n PostalCode -->", sRFParameters.PostalCode);
                    SRFStateDpn.Value = sRFParameters.State;
                    instructorInfo = string.Concat(instructorInfo, "\r\n State -->", sRFParameters.State);
                    SRFCountry.SelectedValue = string.IsNullOrEmpty(sRFParameters.Country) ? visitor.CountryCode : (sRFParameters.Country.ToUpper() == "AU" && visitor.CountryCode.ToUpper() == "NZ") ? visitor.CountryCode : sRFParameters.Country;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Country --> ", sRFParameters.Country);
                    SRFcourseName.Value = sRFParameters.CourseName;
                    instructorInfo = string.Concat(instructorInfo, "\r\n CourseName --> ", sRFParameters.CourseName);
                    SRFcoursesCode.Value = sRFParameters.CourseCode;
                    instructorInfo = string.Concat(instructorInfo, "\r\n CourseCode --> ", sRFParameters.CourseCode);
                    SRFSemesterDpn.Value = sRFParameters.Semester;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Semester --> ", sRFParameters.Semester);
                    SRFSemesterEnrolment.Value = sRFParameters.Enrolment;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Enrolment --> ", sRFParameters.Enrolment);
                    SRFBookCurrently.Value = sRFParameters.BookInUse;
                    instructorInfo = string.Concat(instructorInfo, "\r\n BookInUse --> ", sRFParameters.BookInUse);
                    ContactMobNumber.Value = sRFParameters.Mobile;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Mobile --> ", sRFParameters.Mobile);
                    ContactWorkNumber.Value = sRFParameters.Work;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Work --> ", sRFParameters.Work);
                    SRFInstitution.Value = sRFParameters.Institution;
                    instructorInfo = string.Concat(instructorInfo, "\r\n Institution --> ", sRFParameters.Institution);
                    SRFResourceDpn.Value = sRFParameters.HowYouKnow;
                    instructorInfo = string.Concat(instructorInfo, "\r\n HowYouKnow --> ", sRFParameters.HowYouKnow);
                    SRFBookCurrently.Value = sRFParameters.BookInUse;
                    instructorInfo = string.Concat(instructorInfo, "\r\n BookInUse --> ", sRFParameters.BookInUse);
                    SRFBookCurrently.Value = sRFParameters.BookInUse;
                    instructorInfo = string.Concat(instructorInfo, "\r\n BookInUse --> ", sRFParameters.BookInUse);
                    SRFResourceDpn.Value = sRFParameters.HowYouKnow;
                    instructorInfo = string.Concat(instructorInfo, "\r\n HowYouKnow --> ", sRFParameters.HowYouKnow);
                }

                if (visitor.CountryCode == "NZ")
                {
                    sRFParameters.Currency = "NZD";
                    instructorInfo = string.Concat(instructorInfo, "\r\n Currency --> NZD");
                }
                else
                {
                    sRFParameters.Currency = "AUD";
                    instructorInfo = string.Concat(instructorInfo, "\r\n Currency --> AUD");
                }
                sRFParameters.StoreSk = Null.SetNullInteger(visitor.StoreID);
                instructorInfo = string.Concat(instructorInfo, "\r\n UserSK-->", sRFParameters.UserSk);
                instructorInfo = string.Concat(instructorInfo, "\r\n Currency --> ", visitor.StoreID);
            }
            catch (Exception ex) { instructorInfo = string.Concat(instructorInfo,"\r\n Exception in Fill Instructor info function : ", ex.Message); LogFileWrite(ex); }            
        }
        private void FillingItemsintoList(List<string> itemsCourseOrDigital, List<string> itemsPrint, List<SRFItems> formatItems, List<SRFSupplements> teachingSuplements)
        {
            try
            {
                int i = 1, j = 1;
                if (orderPhyDt.Rows.Count != 0)
                {
                    if (int.Parse(orderPhyDt.Rows[orderPhyDt.Rows.Count - 1]["ID"].ToString()) >= 1)
                    {
                        i = int.Parse(orderPhyDt.Rows[orderPhyDt.Rows.Count - 1]["ID"].ToString()) + 1;
                    }
                }
                if (orderDigDt.Rows.Count != 0)
                {
                    if (int.Parse(orderDigDt.Rows[orderDigDt.Rows.Count - 1]["ID"].ToString()) >= 1)
                    {
                        j = int.Parse(orderDigDt.Rows[orderDigDt.Rows.Count - 1]["ID"].ToString()) + 1;
                    }
                }
                if (formatItems != null && formatItems.Count != 0)
                {

                    foreach (SRFItems item in formatItems)
                    {
                        if (!string.IsNullOrEmpty(item.ISBN))
                            if (!itemsPrint.Contains(item.ISBN) && !string.IsNullOrEmpty(item.FormatType) && item.FormatType.ToLower() != ListItemType.ebook.ToString())
                            {

                                itemsPrint.Add(item.ISBN);
                                if (orderPhyDt.Rows.Count != 0)
                                {
                                    if (orderPhyDt.Select().ToList().Exists(row => row["PRODUCT_SK"].ToString() != item.ProductSk.ToString()))
                                    {
                                        orderPhyDt.Rows.Add(i, item.ProductSk, 1, "0.00");
                                    }
                                }
                                else
                                {
                                    orderPhyDt.Rows.Add(i, item.ProductSk, 1, "0.00");
                                }
                                i++;
                            }
                            else
                                if (!itemsCourseOrDigital.Contains(item.ISBN) && !string.IsNullOrEmpty(item.FormatType) && item.FormatType.ToLower() == ListItemType.ebook.ToString())
                                {
                                //    LogValues("Checking Course : " + item.ISBN);
                                    itemsCourseOrDigital.Add(item.ISBN);
                                    if (orderDigDt.Rows.Count != 0)
                                    {
                                        if (orderDigDt.Select().ToList().Exists(row => row["PRODUCT_SK"].ToString() != item.ProductSk.ToString()))
                                        {
                                            orderDigDt.Rows.Add(j, item.ProductSk, 1, "0.00");
                                        }
                                    }
                                    else
                                    {
                                        orderDigDt.Rows.Add(j, item.ProductSk, 1, "0.00");
                                    }
                                    j++;
                                }

                        //i++;
                    }
                }




            }
            catch (Exception ex) { LogFileWrite(ex); }

        }
        private void FillingEACItemsintoList(List<string> itemsDigitalEAC, List<string> itemsPrint, List<SRFItems> formatItems, List<SRFSupplements> teachingSuplements)
        {
            try
            {
                int i = 1, j = 1;
                if (orderPhyDt.Rows.Count != 0)
                {
                    if (int.Parse(orderPhyDt.Rows[orderPhyDt.Rows.Count - 1]["ID"].ToString()) >= 1)
                    {
                        i = int.Parse(orderPhyDt.Rows[orderPhyDt.Rows.Count - 1]["ID"].ToString()) + 1;
                    }
                }
                if (orderDigDt.Rows.Count != 0)
                {
                    if (int.Parse(orderDigDt.Rows[orderDigDt.Rows.Count - 1]["ID"].ToString()) >= 1)
                    {
                        j = int.Parse(orderDigDt.Rows[orderDigDt.Rows.Count - 1]["ID"].ToString()) + 1;
                    }
                }
                if (teachingSuplements != null && teachingSuplements.Count != 0)
                {

                    foreach (SRFSupplements item in teachingSuplements)
                    {
                      //  LogValues("Checking Condition :" + item.IsCore + " " + item.ISBN + " " + item.MediaType );
                        if (!string.IsNullOrEmpty(item.ISBN))
                        {
                            if (!itemsPrint.Contains(item.ISBN) && !string.IsNullOrEmpty(item.MediaType) && item.MediaType.ToLower() != ListItemType.ess.ToString() && item.MediaType.ToLower() != ListItemType.eac.ToString())
                            {
                                itemsPrint.Add(item.ISBN);
                                if (orderPhyDt.Rows.Count != 0)
                                {
                                    if (orderPhyDt.Select().ToList().Exists(row => row["PRODUCT_SK"].ToString() != item.ProductSk.ToString()))
                                    {
                                        orderPhyDt.Rows.Add(i, item.ProductSk, 1, "0.00");
                                    }
                                }
                                else
                                {
                                    orderPhyDt.Rows.Add(i, item.ProductSk, 1, "0.00");
                                }
                                i++;
                            }
                            else
                            {
                                if (!itemsDigitalEAC.Contains(item.ISBN))
                                {

                                    if (item.IsCore)
                                    {
                                    //    LogValues("Checking Core : " + item.ISBN);
                                        itemsDigitalEAC.Add(item.ISBN);
                                    }
                                }
                                //LogValues("Checking Condition :" + item.MediaType.ToLower());
                               
                                if (!string.IsNullOrEmpty(item.MediaType) && item.MediaType.ToLower() == ListItemType.eac.ToString())
                                {
                                   // LogValues("Checking EAC : " + item.ISBN);
                                    if (orderDigDt.Rows.Count != 0)
                                    {
                                        if (orderDigDt.Select().ToList().Exists(row => row["PRODUCT_SK"].ToString() != item.ProductSk.ToString() && !item.IsCore))
                                        {
                                            orderDigDt.Rows.Add(j, item.ProductSk, 1, "0.00");
                                        }
                                    }
                                    else
                                    {
                                        orderDigDt.Rows.Add(j, item.ProductSk, 1, "0.00");
                                    }
                                    j++;

                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
        }
        private void FillingESSItemsintoList(List<string> itemsDigitalESS, List<string> itemsPrint, List<SRFItems> formatItems, List<SRFSupplements> teachingSuplements)
        {
            try
            {
                int j = 1;

                if (orderDigDt.Rows.Count != 0)
                {
                    if (int.Parse(orderDigDt.Rows[orderDigDt.Rows.Count - 1]["ID"].ToString()) >= 1)
                    {
                        j = int.Parse(orderDigDt.Rows[orderDigDt.Rows.Count - 1]["ID"].ToString()) + 1;
                    }
                }
                if (teachingSuplements != null && teachingSuplements.Count != 0)
                {
                    
                    foreach (SRFSupplements item in teachingSuplements)
                    {
                        if (!string.IsNullOrEmpty(item.ISBN))
                        {

                            if (!itemsDigitalESS.Contains(item.ISBN) && !string.IsNullOrEmpty(item.MediaType) && item.MediaType.ToLower() == ListItemType.ess.ToString())
                            {
                               
                                itemsDigitalESS.Add(item.ISBN);
                                if (orderDigDt.Rows.Count != 0)
                                {
                                    if (orderDigDt.Select().ToList().Exists(row => row["PRODUCT_SK"].ToString() != item.ProductSk.ToString()))
                                    {
                                        orderDigDt.Rows.Add(j, item.ProductSk, 1, "0.00");
                                    }
                                }
                                else
                                {
                                    orderDigDt.Rows.Add(j, item.ProductSk, 1, "0.00");
                                }
                                j++;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
            Cache["OrderPhyDtDTDCDEFK" + HttpContext.Current.Session.SessionID] = orderPhyDt;
            Cache["OrderDigDtDTDCDEFK" + HttpContext.Current.Session.SessionID] = orderDigDt;

            //string phy = orderPhyDt.Rows.Count + " Physical-----> ";
            //foreach (DataRow row in orderPhyDt.Rows)
            //{
            //    phy = string.Concat(phy, "ID=" + row["ID"].ToString() + ", PRODUCT_SK=" + row["PRODUCT_SK"].ToString() + ", QUANTITY=" + row["QUANTITY"].ToString() + ", TOTAL_PRICE=" + row["TOTAL_PRICE"].ToString() + "\r\n");
            //    LogValues(phy);
            //}
            
            //phy = orderDigDt.Rows.Count + " Digital-----> ";
            //foreach (DataRow row in orderDigDt.Rows)
            //{
            //    phy = string.Concat(phy, "ID=" + row["ID"].ToString() + ", PRODUCT_SK=" + row["PRODUCT_SK"].ToString() + ", QUANTITY=" + row["QUANTITY"].ToString() + ", TOTAL_PRICE=" + row["TOTAL_PRICE"].ToString() + "\r\n");
            //    LogValues(phy);
            //}
            
        }

        /* ***************************************   darga ended *******************************************/


        /// <summary>
        /// Function to format book thumbnail path
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        [WebMethod]
        protected string FormatImageURL()
        {
            return Null.SetNullString(ConfigurationManager.AppSettings["BooksImgPath"]);
        }


        /// <summary>
        /// Function to construct html elements for products
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        [WebMethod]
        protected string BindHtml(string title, string isbn, string description, string mediaType, string watchDemo, string learnMore, string isCore)
        {
            //LogValues("title-->" + title + " isbn-->" + isbn + " description-->" + description + " mediaType-->" + mediaType + " watchDemo-->" + watchDemo + " learnMore-->" + learnMore);
            string res = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(isCore) || isCore.ToLower()=="false")
                {
                    res = string.Concat("<div class=\"book-content text-only\"><h5>", title, "</h5><span>ISBN: </span><span>",
                                                  isbn, "</span><p class=\"book-full-exp\">", description, "</p></div>");
                    if (!string.IsNullOrEmpty(watchDemo) && !string.IsNullOrEmpty(learnMore))
                    {
                        res = string.Concat(res, "<div class=\"publisher pull-left\">");
                        if (!string.IsNullOrEmpty(watchDemo))
                        {
                            res = string.Concat(res, "<a class=\"watchdemo\" href='" + watchDemo + "'>Watch Demo</a>");
                            if (!string.IsNullOrEmpty(learnMore))
                                res = string.Concat(res, " | ");
                        }
                        if (!string.IsNullOrEmpty(learnMore))
                            res = string.Concat(res, "<a class=\"learnmore\" href='" + learnMore + "'>Learn More</a>");
                        res = string.Concat(res, "</div>");
                    }
                }
                else
                {
                    return "<div class=\"book-content text-only\"><h5>Request instructor site access for all digital resources</h5></div>";
                }
                return res;
            }
            catch (Exception ex) { LogFileWrite(ex); }
            return res;
        }
    }
}