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
using System.Configuration;
using System.IO;
using System.Web;
using Cengage.eCommerce.Lib;
using System.Collections.Generic;
using DotNetNuke.Modules.SampleRequestForm.Components.Modal;
using DotNetNuke.Common.Utilities;
using System.Globalization;
using DotNetNuke.Modules.SampleRequestForm.Components.Controller;
using System.Text;
namespace DotNetNuke.Modules.SampleRequestForm
{

    /// <summary>
    /// This base class can be used to define custom properties for multiple controls. 
    /// An example module, DNNSimpleArticle (http://dnnsimplearticle.codeplex.com) uses this for an ArticleId
    /// 
    /// Because the class inherits from PortalModuleBase, properties like ModuleId, TabId, UserId, and others, 
    /// are accessible to your module's controls (that inherity from SampleRequestFormModuleBase
    /// 
    /// </summary>

    public class SampleRequestFormModuleBase : DotNetNuke.Entities.Modules.PortalModuleBase
    {
        /// <summary>
        ///  Writes Exceptions to log file in user readable format
        /// </summary>
        /// <param name="e"></param>
        public static void LogFileWrite(Exception e)
        {
            if (e.Source != "mscorlib")
            {
                FileStream fileStream = null;
                StreamWriter streamWriter = null;
                try
                {
                    string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "SRF-Exceptions - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");

                    if (logFilePath.Equals("")) return;
                    #region Create the Log file directory if it does not exists
                    DirectoryInfo logDirInfo = null;
                    FileInfo logFileInfo = new FileInfo(logFilePath);
                    logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                    if (!logDirInfo.Exists) logDirInfo.Create();
                    #endregion Create the Log file directory if it does not exists

                    if (!logFileInfo.Exists)
                    {
                        fileStream = logFileInfo.Create();
                    }
                    else
                    {
                        fileStream = new FileStream(logFilePath, FileMode.Append);
                    }
                    streamWriter = new StreamWriter(fileStream);
                    var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                    streamWriter.WriteLine(string.Concat("\r\n-----------------------------------", DateTime.Now, "---------------------------------------------",
                       "\r\n Website: ", uri.Host,
                        "\r\n Module: Sample Request Form ",
                        "\r\n User Name: " + (HttpContext.Current.Session["UserName"]!=null?Null.SetNullString(HttpContext.Current.Session["UserName"]):"Anonymous User"),
                        "\r\n Type: ", e.GetType(),
                        "\r\n Source: ", e.Source, "\r\n Exception: ", e.Message, "\r\n Description: ", e.StackTrace, "\r\n-----------------------------------------------------------------------------------------------------------------"));

                    // TeacherController.Instance.SendExceptionMail(body, e.Message, Null.SetNullInteger(HttpContext.Current.Session["Subscription"]));
                }
                finally
                {
                    if (streamWriter != null) streamWriter.Close();
                    if (fileStream != null) fileStream.Close();
                }
            }
        }

        /// <summary>
        ///  Writes values to log file for developer verification
        /// </summary>
        /// <param name="e"></param>
        public static void LogValues(string value)
        {
            FileStream fileStream = null;
            StreamWriter streamWriter = null;
            try
            {
                string logFilePath = string.Concat(ConfigurationManager.AppSettings["LogFilePath"], "SRF-Value Log - ", DateTime.Today.ToString("dd-MM-yyyy"), "." + "txt");

                if (logFilePath.Equals("")) return;
                #region Create the Log file directory if it does not exists
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                #endregion Create the Log file directory if it does not exists

                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                streamWriter = new StreamWriter(fileStream);
                var uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                string mailBody=string.Concat("\r\n -----------------------------------", DateTime.Now, "---------------------------------------------",
                   "\r\n Website: ", uri.Host,
                    "\r\n Logged Content : \r\n ", value, "\r\n -----------------------------------------------------------------------------------------------------------------");
                streamWriter.WriteLine(mailBody);

                try
                {
                   if (uri.Host.ToLower() == "cengage.com.au" || uri.Host.ToLower() == "cengage.co.nz")
                    {
                        mailBody = mailBody.Replace("\r\n ", "<br/> ");
                        SampleRequestFormController.Instance.SRFSendMail("ranjith.m@htcindia.com", mailBody, "Sample Request Log");
                    }
                }
                catch(Exception ex){}
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (fileStream != null) fileStream.Close();
            }
        }

            /// <summary>
        /// Generate the email template body
        /// </summary>
        /// <param name="registrationParameters"></param>
        /// <returns></returns>
        public string UpdateSSOErrorMailBody(string userName, string dateTime, string templatePath,string isbns,string ssoUrl)
        {
            try
            {          
                string Body = string.Empty, user = string.Empty, AddressSk = string.Empty;              
                //Read the email template into a string
                using (StreamReader reader = new StreamReader(templatePath))
                {
                    Body = reader.ReadToEnd();
                }

                Body = Body.Replace("{USERNAME}", userName);
                Body = Body.Replace("{TIME}", dateTime);
                Body = Body.Replace("{ISBN}", isbns);
                Body = Body.Replace("{SSOURL}", ssoUrl);
                
                //LogValues("Body \r\n" + Body);
                return Body;
            }
            catch (Exception ex) { LogFileWrite(ex); return string.Empty; }
        }
        /// <summary>
        /// Generate the email template body
        /// </summary>
        /// <param name="registrationParameters"></param>
        /// <returns></returns>
        public string UpdateMailBody(List<SRFItems> formatItems,List<SRFSupplements> teachingSuplements,string salesRepId, string usertype, SRFParameters sRFParameters,string templatePath, string orderNumber1,string orderNumber2)
        {
            try
            {
                string Body = string.Empty, user = string.Empty, AddressSk = string.Empty;
                int fitemscnt = 0, sitemscnt = 0;
                string[] state = { "NIL","NSW", "VIC", "QLD", "SA", "WA", "TAS", "NT", "ACT" },
                    howYouKnow = { "NIL", "Colleague or friend", "Cengage sales representative", "Browsing this site", "Somewhere else on the web", "Via Cengage marketing - email", "Via Cengage marketing - other", "Review publication", "Other" },
                    semester = { "NIL", "Semester/Trimester 1", "Semester/Trimester 2", "Trimester 3", "Other" },
                    contact = { "NIL","Mobile", "Work", "Contact email" };
                //Read the email template into a string
                using (StreamReader reader = new StreamReader(templatePath))
                {
                    Body = reader.ReadToEnd();
                }
                SRFItems coreProduct = new SRFItems();
                try { coreProduct = Session["CoreProductDetail"] as SRFItems; }catch (Exception ex) { LogFileWrite(ex); }

                try { Body = Body.Replace("{SalesRepId}", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(salesRepId)); }
                catch (Exception ex) { LogFileWrite(ex); }
               // List<SRFItems> formatItems = Session["CoreFormatProducts"] as List<SRFItems>;
               // List<SRFSupplements> teachingSuplements = Session["CoreSupplementsProducts"] as List<SRFSupplements>;
                string requestItems = string.Empty;
               // requestItems = string.Concat(requestItems, "<div style=\"margin-bottom: 30px;float: left;\"><h4 stye=\"margin: 7px 0;font-size: 12pt;text-align: left;\">Details of items requested:</h4>");
                try
                {
                   
                    requestItems = "<table width=\"500\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" align=\"left\"  style=\"font-family: Arial;border:1px solid #000000;\">" +
                                "<tr style=\"border:1px solid #000000;\">" +
                                    "<td style=\"border-right:1px solid #000000;border-bottom:1px solid #000000;\">" +
                                        "<p style=\"color: #000000;margin:0;padding:5px;\">ISBN</p>" +
                                    "</td>" +
                                    "<td style=\"border-bottom:1px solid #000000;\">" +
                                        "<p style=\"color: #000000;margin:0;padding:5px;\">Title</p>" +
                                    "</td>" +
                                "</tr>";
                    if (formatItems != null && formatItems.Count > 0)
                    {
                        for (int i = 0; i < formatItems.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(formatItems[i].ISBN))
                            {
                                //requestItems = string.Concat(requestItems, "<div style=\"border: 0 none;outline: 0 none;margin: 6px 0 6px;\">",
                                //            "<div style=\"padding: 10px 0 0; margin-top: 10px;float: left;width: 100%;\">",
                                //                "<div style=\"height: 82px;width: 70px; float: left !important;\">",
                                //                    "<img style=\"height: 82px;width: 70px;\" src='" + formatItems[i].ProductImageUrl + "' alt=\"book\"  onError=\"this.onerror=null;this.src='" + string.Concat(ConfigurationManager.AppSettings["BooksImgPath"], "HERnoimage.png") + "';\"  />" +
                                //                "</div>",
                                //                "<div style=\"padding-left: 10px;float: left;width: 369px;font-size: 10pt;\">",
                                //                   "<h5 style=\"margin-bottom: 0;margin-top: 0;\">" + formatItems[i].FormatType + "</h5>",
                                //                    "<p style=\"min-height: 40px;width: 360px;overflow: hidden;text-overflow: ellipsis;max-height: 60px;line-height: 15px;font-family: Arial;\">" + formatItems[i].Description + "</p>",
                                //                "</div></div></div>");                           


                                requestItems = string.Concat(requestItems, "<tr style=\"border:1px solid #000000;\">" +
                                     "<td style=\"border-right:1px solid #000000;border-bottom:1px solid #000000;\">" +
                                         "<p style=\"color: #000000;margin:0;padding:5px;\">" + formatItems[i].ISBN + "</p>" +
                                     "</td>" +
                                     "<td style=\"border-bottom:1px solid #000000;\">" +
                                         "<p style=\"color: #000000;margin:0;padding:5px;\">" + formatItems[i].ProductName + "</p>" +
                                     "</td>" +
                                 "</tr>");
                                fitemscnt++;
                            }
                        }

                    }

                    if (teachingSuplements != null && teachingSuplements.Count > 1)
                    {
                        //requestItems = string.Concat(requestItems, "<div id=\"TeachingSupplementsTopDiv\" style=\"clear: both;margin-top: 10px;float: left;\"><h5 style=\"margin: 12px 0 4px 0;font-size: 10pt;\">Teaching supplements</h5>"
                        // , "<p id=\"SRFSuppMsgPara\" style=\"text-align: left;padding-bottom: 8px;margin: 0 0 10px;\">" + teachingSuplements[0].Description + "</p><div style=\"border: 1px solid #707070;max-height: 340px;overflow: auto;width: 460px;\" id=\"TeachingSupplementsDiv\">");
                        for (int i = 0; i < teachingSuplements.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(teachingSuplements[i].ISBN) && teachingSuplements[i].IsCore != true)
                            {
                                requestItems = string.Concat(requestItems, "<tr style=\"border:1px solid #000000;\">" +
                                       "<td style=\"border-right:1px solid #000000;border-bottom:1px solid #000000;\">" +
                                           "<p style=\"color: #000000;margin:0;padding:5px;\">" + teachingSuplements[i].ISBN + "</p>" +
                                       "</td>" +
                                       "<td style=\"border-bottom:1px solid #000000;\">" +
                                           "<p style=\"color: #000000;margin:0;padding:5px;\">" + teachingSuplements[i].Title + "</p>" +
                                       "</td>" +
                                   "</tr>");
                                sitemscnt++;
                            }
                            //requestItems = string.Concat(requestItems, "<div style=\"padding: 15px 0 22px;\"><div style=\"float: left;font-size: 11px;padding-left: 20px;width: 440px;\">",
                            //                    "<h5 style=\"font-size: 12px !important;margin-bottom: 0;margin-top: 0;\">" + teachingSuplements[i].Title + "</h5><span>ISBN: </span><span>" + teachingSuplements[i].ISBN + "</span>",
                            //                    "<p style=\"text-align: left;width: 100%;overflow: hidden;text-overflow: ellipsis;\">" + teachingSuplements[i].Description + "</p></div>",
                            //                    BindHtml(teachingSuplements[i].WatchDemo, teachingSuplements[i].LearnMore), "</div>");
                        }
                        //requestItems = string.Concat(requestItems, "</div></div>");
                    }
                    else if (teachingSuplements != null && teachingSuplements.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(teachingSuplements[0].ISBN) && teachingSuplements[0].IsCore != true)
                        {
                            string tt1 = "<td style=\"border-right:1px solid #000000;border-bottom:1px solid #000000;\">" +
                                   "<p style=\"color: #000000;margin:0;padding:5px;\"></p>" +
                                   "</td>";
                            string tt2 = "<td style=\"border-right:1px solid #000000;border-bottom:1px solid #000000;\">" +
                                   "<p style=\"color: #000000;margin:0;padding:5px;\">" + teachingSuplements[0].ISBN + "</p>" +
                                   "</td>";
                            string res = teachingSuplements[0].ISBN == "PVRJ" ? tt1 : tt2;
                            requestItems = string.Concat(requestItems, "<tr style=\"border:1px solid #000000;\">" +
                                res +
                                   "<td style=\"border-bottom:1px solid #000000;\">" +
                                       "<p style=\"color: #000000;margin:0;padding:5px;\">" + teachingSuplements[0].Title + "</p>" +
                                   "</td>" +
                               "</tr>");
                            sitemscnt++;
                        }
                    }
                    requestItems = string.Concat(requestItems, "</table>");
                }
                catch (Exception ex) { LogFileWrite(ex); }
                //requestItems = string.Concat(requestItems, "</div>");
                try { Body = Body.Replace("{FormatItems}", requestItems); }catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{Name}", sRFParameters.Name);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{Email}", sRFParameters.Email);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{ContactId}", "<span style=\"float:left;width:130px\">"+contact[Null.SetNullInteger(sRFParameters.ContactType)]+"</span>: "+sRFParameters.ContactValue);}catch (Exception ex) { LogFileWrite(ex); }// sRFParameters.Mobile != string.Empty ? "<span style=\"float:left;width:130px\">Mobile</span>: " + sRFParameters.Mobile : sRFParameters.Work != string.Empty ? "<span style=\"float:left;width:130px\">Work</span>: " + sRFParameters.Work : "<span style=\"float:left;width:130px\">Contact email</span>: " + sRFParameters.Email);
                if (usertype != "DIGITAL ONLY")
                {
                    try { Body = Body.Replace("{ShippingAddress1}", sRFParameters.Address1);}catch (Exception ex) { LogFileWrite(ex); }
                    try { Body = Body.Replace("{ShippingAddress2}", sRFParameters.Address2);}catch (Exception ex) { LogFileWrite(ex); }
                    try { Body = Body.Replace("{ShippingAddress3}", sRFParameters.Address3);}catch (Exception ex) { LogFileWrite(ex); }
                    try { Body = Body.Replace("{Suburb}", sRFParameters.SubUrb);}catch (Exception ex) { LogFileWrite(ex); }
                    try { Body = Body.Replace("{Postcode}", Null.SetNullString(sRFParameters.PostalCode));}catch (Exception ex) { LogFileWrite(ex); }
                    try { Body = Body.Replace("{State}", state[Null.SetNullInteger(sRFParameters.State)]);}catch (Exception ex) { LogFileWrite(ex); }
                    try { Body = Body.Replace("{Country}", sRFParameters.Country);}catch (Exception ex) { LogFileWrite(ex); }
                }
                try { Body = Body.Replace("{CourseName}", sRFParameters.CourseName);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{CourseCode}", sRFParameters.CourseCode);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{Semester}", semester[Null.SetNullInteger(sRFParameters.Semester)]);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{SemesterEnrolment}", sRFParameters.Enrolment);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{BookCurrentlyInUse}", sRFParameters.BookInUse);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{Institution}", sRFParameters.Institution);}catch (Exception ex) { LogFileWrite(ex); }
                                
                try { Body = Body.Replace("{COREISBN}", coreProduct.ISBN);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{CORETITLE}", coreProduct.Description);}catch (Exception ex) { LogFileWrite(ex); }

               // Body = Body.Replace("{SecondaryEmail}", sRFParameters.SecondaryEmail);
               
                string orderNumbers = string.Empty;
                try
                {
                    if (!string.IsNullOrEmpty(orderNumber1))
                    {
                        if (!string.IsNullOrEmpty(orderNumber2))
                            orderNumbers = string.Concat(orderNumber1, ',', orderNumber2);
                        else
                            orderNumbers = orderNumber1.ToString();
                    }
                    else if (!string.IsNullOrEmpty(orderNumber2))
                        orderNumbers = orderNumber2.ToString();
                    else
                        orderNumbers = string.Empty;
                }
                catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{OrderNumber}",orderNumbers!=string.Empty?"<span style=\"width:125px; float:left \">Order Number</span>: "+orderNumbers:string.Empty);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{Message}", sRFParameters.Message.Trim()==string.Empty?string.Empty:"<br/><p style=\"color: #000000;margin:0; font-size:13px\"><span style=\"width:120px; float:left\">Message</span>: "+sRFParameters.Message+"</p>" );}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{HowYouKnow}", howYouKnow[Null.SetNullInteger(sRFParameters.HowYouKnow)]);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{ForTeaching}", sRFParameters.ForTeaching);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{AlreadyUsing}", sRFParameters.AlreadyUsing);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{NeedHelp}", sRFParameters.NeedHelp);}catch (Exception ex) { LogFileWrite(ex); }
                try { Body = Body.Replace("{hyperlink_contactus}", HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority.Trim() + "/contactus.aspx"); }
                catch (Exception ex) { LogFileWrite(ex); }
                if (fitemscnt + sitemscnt > 0)
                    return Body;
                else
                    return string.Empty;
            }
            catch (Exception ex) { LogFileWrite(ex); return string.Empty;}
        }

        /// <summary>
        /// Function to construct html elements for products
        /// </summary>
        /// <param name="TabID"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        protected string BindHtml(string watchDemo, string learnMore)
        {
            if (!string.IsNullOrEmpty(watchDemo) && !string.IsNullOrEmpty(learnMore))
            {
                string res = "<div style=\"border-bottom: 1px solid #ccc;height: 19px;margin: 0 0 15px 20px !important;width: 125px !important;font-size: 14px !important;word-spacing: 3px !important;clear: both;float: left !important;\">";
                if (!string.IsNullOrEmpty(watchDemo))
                {
                    res = string.Concat(res, "<a style=\"color: #707070 !important;font-size: 10px;outline: none !important;background: transparent;\" href='" + watchDemo + "'>Watch Demo</a>");
                    if (!string.IsNullOrEmpty(learnMore))
                        res = string.Concat(res, " | ");
                }
                if (!string.IsNullOrEmpty(learnMore))
                    res = string.Concat(res, "<a style=\"color: #707070 !important;font-size: 10px;outline: none !important;background: transparent;\" href='" + learnMore + "'>Learn More</a>");
                res = string.Concat(res, "</div>");
                return res;
            }
            else
                return string.Empty;
        }

      /*  /// <summary>
        /// To encrypt the input text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string encrypt(string text)
        {
            try
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(text);
                return Convert.ToBase64String(toEncryptArray, 0, toEncryptArray.Length);
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
            return string.Empty;
        }

        /// <summary>
        /// To decrypt the input text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string decrypt(string text)
        {
            try
            {
                byte[] toEncryptArray = Convert.FromBase64String(text);
                return UTF8Encoding.UTF8.GetString(toEncryptArray);
            }
            catch (Exception ex)
            {
                LogFileWrite(ex);
            }
            return string.Empty;
        }*/
    }
}
