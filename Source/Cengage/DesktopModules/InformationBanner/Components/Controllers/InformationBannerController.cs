using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.InformationBanner.Data;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.InformationBanner.Components.Controllers
{
    public class InformationBannerController
    {
        private static InformationBannerController subCtrl;

        private InformationBannerController() { }

        public static InformationBannerController Instance
        {
            get
            {
                if (subCtrl == null)
                {
                    subCtrl = new InformationBannerController();
                }
                return subCtrl;
            }
        }

        /// <summary>
        /// Get All banner messages
        /// </summary>
        /// <param name="val"></param>
        /// <param name="currentDate"></param>
        /// <param name="userLoginName"></param>
        /// <returns></returns>
        public SqlDataReader GetAlerts(string val, string currentDate, string userLoginName)
        {
            try
            {
                return DataProvider.Instance().GetAlerts(val, currentDate, userLoginName);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Add a new banner message
        /// </summary>
        /// <param name="usersk"></param>
        /// <param name="ErrorMsg"></param>
        /// <param name="ErrorType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public int AddAlerts(int usersk, string ErrorMsg, string ErrorType, string StartDate, string EndDate)
        {
            try
            {
                return DataProvider.Instance().AddAlerts(usersk, ErrorMsg, ErrorType, StartDate, EndDate);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// Delete a banner message
        /// </summary>
        /// <param name="AlertID"></param>
        /// <returns></returns>
        public int DeleteAlerts(int AlertID)
        {
            try
            {
                return DataProvider.Instance().DeleteAlerts(AlertID);
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// Update a banner message
        /// </summary>
        /// <param name="AlertID"></param>
        /// <param name="ErrorMsg"></param>
        /// <param name="ErrorType"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public int UpdateAlerts(int AlertID, string ErrorMsg, string ErrorType, string StartDate, string EndDate)
        {
            try
            {
                return DataProvider.Instance().UpdateAlerts(AlertID, ErrorMsg, ErrorType, StartDate, EndDate);
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Delete a banner message for the paritcular user
        /// </summary>
        /// <param name="AlertID"></param>
        /// <param name="userLoginName"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public int DeleteUserInfoID(int AlertID, string userLoginName, string pageUrl)
        {
            try
            {
                return DataProvider.Instance().DeleteUserInfoID(AlertID, userLoginName, pageUrl);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}