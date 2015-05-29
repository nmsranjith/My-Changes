
namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Common
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Constants" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Class for Constants
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Constants
    {
        internal const string SP_GETSUBSCRIPTIONSLIST = "eCollection_Students_Subscriptions_Get";
        internal const string SP_ADDSUBSCRIPTIONAME = "eCollection_Dashboards_Subscription_Edit";
        internal const string SP_GETUSERDETAIL = "eCollection_Dashboards_Userdetail_Get";
        internal const string SP_GETSUBSCRIPTIONDETAIL = "eCollection_Dashboards_Subscriptiondetail_Get";
        internal const string SP_GETALLACTIVITYDATES = "eCollection_Dashboards_ActivityDates_Get";
        internal const string SP_GETALLACTIVITIES = "eCollection_Dashboards_Activities_Get";
        internal const string SP_VIDEOGETSTARTEDUPDATEFLAG = "eCollection_Dashboards_VideoGetStarted_Update";
        internal const string sp_EXCEPTIONMAIL = "eCollection_Exception_Insert";
        internal const string SP_AFTERRENEWAL = "eCollection_Session_AfterRenewal";
        internal const string SP_GETRENEWALLICENSECOUNT = "eCollection_Session_GetRenewalLicenseCount";
        internal const string sp_UPDATEINACTIVEANDARCHIVE = "eCollection_Session_InactiveAndArchive_Cust_Sub";
        internal const string sp_GETRENEWALSUBSCRIPTIONNNAME = "eCollection_Session_RenewalSubscriptionName_Get";
        internal const string sp_GETRENEWALBOOKCOUNT = "eCollection_Session_GetRenewalBookCount";
        internal const string sp_GETALLRENEWELS = "eCollection_Session_GetAllRenewals";
        internal const string SP_GETERRORMESSAGES = "eCollection_Messages_Get";
        internal const string SP_USERSBULKUPLOAD = "eCollection_EarlyAccess_BulkUpload";
        internal const string SP_VALIDATESUBS = "eCollection_ValidateSubs";
        internal const string SP_INSERTAUDIODETAILS = "eCollection_Staging_Audio_Insert";
        internal const string SP_UPGRADEDATMIGRATION = "eCollection_Trial_Data_Migration";
        internal const string SP_RENEWALDATAMIGRATION = "eCollection_Trial_Data_Migration_Renewal";

        

        internal const string SP_GETACCOUNTINFO = "eCollection_AppData_AccountInfo_Get";
        internal const string SP_GETREPORTINFO = "eCollection_AppData_ReportInfo_Get";
        internal const string SP_GETACCOUNTDETAILS = "ecollection_AppData_AccountDetails";


        internal const string FIRSTNAME_MANDATORY = "ECOLLTEACH02";
        internal const string LASTNAME_MANDATORY = "ECOLLTEACH03";
        internal const string EMAIL_MANDATORY = "ECOLLTEACH04";
        internal const string PASSWORD_MANDATORY = "ECOLLTEACH03";
        internal const string ACCOUNT_NUMBER_MANDATORY = "ECOLLTEACH23";
        internal const string ROLE_MANDATORY = "ECOLLTEACH24";
        internal const string VALIDATE_FIRSTNAME = "ECOLLUSER03";
        internal const string VALIDATE_LASTNAME = "ECOLLUSER04";
        internal const string VALIDATE_EMAIL = "ECOLLUSER02";
        internal const string UNIQUE_EMAIL = "ECOLLUSER02";
        internal const string ALREADY_INVITED = "ECOLLUSER17";
        internal const string TEACHER_ADDED_TO_SUBS = "ECOLLUSER18";
        internal const string CREATE_SUCCESS1 = "ECOLLTEACH08";
        internal const string CREATE_SUCCESS2 = "ECOLLUSER06";
        internal const string UPLOAD_SUCCESS1 = "ECOLLTEACH18";
        internal const string UPLOAD_SUCCESS2 = "ECOLLTEACH19";
        internal const string SESSION_EXPIRE = "ECOLLUSER12";

        internal const string VALIDATE_BROWSETXTBX = "ECOLLUSER07";
        internal const string VALIDATE_EMPTY_FILE = "ECOLLUSER08";
        internal const string VALIDATE_MISMATCH_FILE = "ECOLLUSER09";
        internal const string UPLOAD_WAIT = "ECOLLUSER15";
        internal const string FIRSTNAME_MANDATORY_UPLOAD = "ECOLLTEACH11";
        internal const string LASTNAME_MANDATORY_UPLOAD = "ECOLLTEACH12";
        internal const string EMAIL_MANDATORY_UPLOAD = "ECOLLTEACH13";
        internal const string VALIDATE_MISMATCH_PATH = "ECOLLUSER01";
        internal const string GROUPS_SELECTED = "ECOLLUSER05";
        internal const string TEACHERS_ADDED_TO_GRPS = "ECOLLTEACH16";
        internal const string SELECT_TEACHERS = "ECOLLTEACH17";
        internal const string TEACHER_BLK_ADDED = "ECOLLTEACH21";
        internal const string TEACHER_BLK_ALREADY_INVITED = "ECOLLTEACH22";


        internal const string Ecomm_GetUserInfo = "Ecomm_GetUserInfo";

        internal const string NOSUBSCRIPTIONSINFO = "There are no subscriptions to display.";
    }
}