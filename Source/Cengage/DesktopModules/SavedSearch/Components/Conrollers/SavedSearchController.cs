using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.SavedSearch.Data;
using System.Data.SqlClient;
using System.Data;

namespace DotNetNuke.Modules.SavedSearch.Conrollers
{
    public class SavedSearchController
    {
        private static SavedSearchController subCtrl;

        private SavedSearchController() { }

        public static SavedSearchController Instance
        {
            get
            {
                if (subCtrl == null)
                {
                    subCtrl = new SavedSearchController();
                }
                return subCtrl;
            }
        }
        public SqlDataReader GetSavedSearchName(string count,int usersk,string PageName)
        {
            return DataProvider.Instance().GetSavedSearchName(count, usersk, PageName);
        }
        public int DeleteSavedSearchName(int PROD_SAV_FAV_SK)
        {
            return DataProvider.Instance().DeleteSavedSearchName(PROD_SAV_FAV_SK);
        }
        /// <summary>
        /// getting module information
        /// </summary>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public SqlDataReader GetModuleInformation(int moduleID)
        {
            return DataProvider.Instance().GetModuleInformation(moduleID);
        }
        /// <summary>
        /// saving module information
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="content"></param>
        /// <param name="usersk"></param>
        /// <returns>bool</returns>
        public bool InsertModuleInformation(int moduleID, string content, int usersk)
        {
            return DataProvider.Instance().InsertModuleInformation(moduleID, content, usersk);
        }
        /// <summary>
        /// updating module information
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="content"></param>
        /// <param name="usersk"></param>
        /// <returns>bool</returns>
        public bool UpdateModuleInformation(int moduleID, string content, int usersk)
        {
            return DataProvider.Instance().UpdateModuleInformation(moduleID, content, usersk);
        }



    }
}