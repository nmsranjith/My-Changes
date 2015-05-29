using System;
using System.Configuration;
using System.IO;
using System.Net;
using DotNetNuke.Modules.SampleRequestForm.Components.Modal;
using Newtonsoft.Json;
using DotNetNuke.Instrumentation;
using System.Web;

namespace DotNetNuke.Modules.SampleRequestForm.Components.Controller
{
    public class SSOAPIController:SampleRequestFormModuleBase
    {
        #region SSO Apis
        /// <summary>
        /// Checking User exists in SSO or not
        /// </summary>
        /// <param name="sRFParameters"></param>
        /// <param name="ResponceCode"></param>
        /// <returns>returns json data</returns>
        public dynamic CheckUserExists(SRFParameters sRFParameters, out int ResponceCode)
        {
            ResponceCode = 0;
            try
            {
                //LogValues(string.Concat(ConfigurationManager.AppSettings["SSOUrl"].ToString() +
                //                                  "CheckUserIdExists/" + sRFParameters.Email + "," + sRFParameters.SSOAuthKey));
                return SSOAPICalling(ConfigurationManager.AppSettings["SSOUrl"].ToString() +
                                                  "CheckUserIdExists/" + sRFParameters.Email + "," + sRFParameters.SSOAuthKey, out ResponceCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic SSOLogin(SRFParameters sRFParameters, out int ResponceCode)
        {
            ResponceCode = 0;
            try
            {
                return SSOAPICalling(ConfigurationManager.AppSettings["SSOUrl"].ToString() +
                                                  "SsoLogin/" + sRFParameters.Email + "," + sRFParameters.Password + "," + sRFParameters.SSOAuthKey, out ResponceCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creating Student Account in SSO
        /// </summary>
        /// <param name="sRFParameters"></param>
        /// <param name="ResponceCode"></param>
        /// <returns>returns json data</returns>
        public dynamic CreateStudentAccount(SRFParameters sRFParameters, out int ResponceCode)
        {
            ResponceCode = 0;
            try
            {
                return SSOAPICalling(ConfigurationManager.AppSettings["SSOUrl"].ToString() +
                                                  "CreateStudentAccount/" + sRFParameters.SecondaryEmail + "," + sRFParameters.Password
                                                                          + "," + sRFParameters.FirstName + "," + sRFParameters.LastName + "," + sRFParameters.SSOAuthKey
                                                                            , out ResponceCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Getting Product details of the SSO User.
        /// </summary>
        /// <param name="sRFParameters"></param>
        /// <param name="ResponceCode"></param>
        /// <returns>returns as Json</returns>
        public dynamic GetUserProductsByTitle(SRFParameters sRFParameters, out int ResponceCode)
        {
            try
            {
                return SSOAPICalling(ConfigurationManager.AppSettings["SSOUrl"].ToString() +
                                                      "GetUserProductsByTitle/" + sRFParameters.Token + "," + sRFParameters.SSOAuthKey // Need to fill the TOKEN from SESSION 
                                                                                , out ResponceCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creating digital(eac,ess) request forms
        /// </summary>
        /// <param name="sRFParameters"></param>
        /// <param name="ResponceCode"></param>
        /// <returns>retuens as Json</returns>
        public dynamic CreateIROrder(SRFParameters sRFParameters, out int ResponceCode)
        {
            try
            {
                string isbnWithPipeSeperated = string.Empty,isbnWithCommaSeperated=string.Empty;
                foreach (var item in sRFParameters.ISBNS)
                {
                    isbnWithPipeSeperated = isbnWithPipeSeperated + "|" + item;
                    isbnWithCommaSeperated = isbnWithCommaSeperated + "," + item;
                }
                isbnWithPipeSeperated = isbnWithPipeSeperated.Trim('|');
                HttpContext.Current.Session["SSOREQISBNS"] = isbnWithCommaSeperated.Trim(',');
                string ssoUrl=ConfigurationManager.AppSettings["SSOUrl"].ToString()+"CreateIROrder/" + sRFParameters.Email + "," + isbnWithPipeSeperated + "," + sRFParameters.SSOAuthKey;
                HttpContext.Current.Session["SSOURL"] = ssoUrl;
                return SSOAPICalling(ssoUrl, out ResponceCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Generating samples for core isbn
        /// </summary>
        /// <param name="sRFParameters"></param>
        /// <param name="ResponceCode"></param>
        /// <returns>retuens as json</returns>
        public dynamic GenerateConsumeIacForEisbn(SRFParameters sRFParameters, out int ResponceCode)
        {
            try
            {
                HttpContext.Current.Session["SSOREQISBNS"] = sRFParameters.CoreIsbn;
                string ssoUrl=ConfigurationManager.AppSettings["SSOUrl"].ToString() +"GenerateConsumeIacForEisbn/" + sRFParameters.CoreIsbn + "," + sRFParameters.SecondaryEmail+ ",1," + sRFParameters.SSOAuthKey;
                HttpContext.Current.Session["SSOURL"] = ssoUrl;
                return SSOAPICalling(ssoUrl, out ResponceCode);
            }
            catch (Exception ex){throw ex;}
        }

        #endregion

        #region Mangallean Serive
        /// <summary>
        /// Creating Sample request form in BM
        /// </summary>
        /// <param name="contactID"></param>
        /// <param name="isbns"></param>
        /// <returns>returns "successfield as true/false"</returns>
        public ReturnCreateSamples CreateSample(string contactID, string[] isbns)
        {         
           SamplingService sampleService =null;
            string isbnList = string.Empty;
            try
            {
                sampleService = new SamplingService();
                DnnLog.Info("Magellan URL -- >>> " + sampleService.Url);
                foreach (string item in isbns)
                {
                   isbnList = isbnList + " | " + item;
                }
                DnnLog.Info("ContactID :" + contactID + "\r\n" + "Isbns are seperated with | symbol : " + isbnList);

                return sampleService.CreateSample(contactID, isbns);
               // return sampleService.CreateSample(contactID, arrayOfIsbns);
            }
            catch (Exception ex)
            {
                DnnLog.Info("Magellan Call--> Failed--> Exception-->"+ex.Message);
                throw ex;
            }
            return new ReturnCreateSamples();
        }

        #endregion

        #region common call to SSO API
        /// <summary>
        /// Calling SSO API
        /// </summary>
        /// <param name="SSOUrl"></param>
        /// <param name="ResponceCode"></param>
        /// <returns>returns json data</returns>
        private static dynamic SSOAPICalling(string SSOUrl, out int ResponceCode)
        {
            try
            {
                WebRequest request = WebRequest.Create(SSOUrl);
                request.ContentType = "application/json; charset=utf-8";
                if (ConfigurationManager.AppSettings["UseProxy"].ToString() == "true")
                {

                    request.UseDefaultCredentials = true;
                    request.Proxy.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

                }
                request.Timeout = HttpContext.Current.Session["TimeOutValue"] != null ? (int)HttpContext.Current.Session["TimeOutValue"] : 60000;

                using (WebResponse response = request.GetResponse())
                {
                    Stream DataStream = response.GetResponseStream();
                    StreamReader Reader = new StreamReader(DataStream);
                    string ResponseFromServer = Reader.ReadToEnd();

                    ResponceCode = (int)((HttpWebResponse)response).StatusCode;
                    Reader.Close();

                    var results = JsonConvert.DeserializeObject<dynamic>(ResponseFromServer);
                    return results;
                }
            }
            catch (Exception ex) { throw; }
        }
        #endregion
    }
}