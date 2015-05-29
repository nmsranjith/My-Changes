using System;
using System.Data;
using System.Linq;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Modules.SampleRequestForm.Components.Modal;
using DotNetNuke.Modules.SampleRequestForm.Data;

namespace DotNetNuke.Modules.SampleRequestForm.Components.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SampleRequestFormController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    
    public class SampleRequestFormController
    {
       
        public static readonly SampleRequestFormController studCtrl = new SampleRequestFormController();

        private SampleRequestFormController() { }

        public static SampleRequestFormController Instance
        {
            get { return studCtrl; }
            set { }
        }

        /// <summary>
        /// Send mail to sales representative
        /// </summary>
        /// <param name="toAddress"></param>
        /// <param name="mailBody"></param>
        /// <param name="mailSubject"></param>
        /// <returns></returns>
        public int SRFSendMail(string toAddress, string mailBody, string mailSubject)
        {
            try
            {
                return DataProvider.Instance().SRFSendMail(toAddress, mailBody, mailSubject);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Insert Orders
        /// </summary>
        /// <param name="sRFParameters"></param>
        /// <param name="orderDt"></param>
        /// <returns></returns>
        public string InsertOrders(SRFParameters sRFParameters, DataTable orderDt,string typeOfsale,string FormatType)
        {
            try
            {
                return DataProvider.Instance().InsertOrders(sRFParameters, orderDt, typeOfsale, FormatType);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sRFParameters"></param>
        /// <returns></returns>
        public int SaveSSOInstructorInfo(SRFParameters user)
        {
            try
            {
                return DataProvider.Instance().SaveSSOInstructorInfo(user);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Get Instructor Info
        /// </summary>
        /// <param name="UserLoginName"></param>
        /// <returns></returns>
        public SRFParameters GetInstructorInfo(string UserLoginName)
        {
            try
            {
                return CBO.FillObject<SRFParameters>(DataProvider.Instance().GetInstructorInfo(UserLoginName));
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Convertion of array of isbn to isbn with comma seperated
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns>string of Isbns</returns>
        private string GetString(string[] isbn)
        {
            try
            {
                return String.Join(",", isbn.Select(p => p.ToString()).ToArray());
            }
            catch (Exception ex) { throw ex; }
        }
    }
}