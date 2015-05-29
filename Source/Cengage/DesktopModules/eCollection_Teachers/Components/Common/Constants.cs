
namespace DotNetNuke.Modules.eCollection_Teachers.Components.Common
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
        internal const string SP_GETTEACHERSLIST = "eCollection_Teachers_List";
        internal const string SP_GETSUBSCRIPTIONSLIST = "eCollection_Teachers_Subscriptions_Get";
        internal const string SP_ADDTEACHERPROFILE = "eCollection_Teachers_Insert";
        internal const string SP_DELETETEACHERPROFILE = "eCollection_Teachers_Delete";
        internal const string SP_GETALLTEACHERGROUPS = "eCollection_Teachers_Groups_Get";
        internal const string SP_GETALLSUBSGROUPS = "eCollection_Users_Groups_Get";
        internal const string SP_GETTEACHERNAMES = "eCollection_Teachers_Get";
        internal const string SP_ADDTEACHERTOGROUPS = "eCollection_Students_AddStudToGrp";
        internal const string SP_GETPROFILEDETAILS = "eCollection_Teachers_Details_Get";
        internal const string SP_GETPREADINGHISTORY = "eCollection_Teachers_ReadingHistory_Get";
        internal const string SP_UPDATEBOOKREADLEVEL = "eCollection_Teachers_BkReadLevel_Update";
        internal const string SP_GET_MY_WORDS_RECORDINGS = "eCollection_User_Words_Recordings_Get";
        internal const string SP_CLEARFROMIPAD = "eCollection_Users_ClearFromIpad";
        internal const string SP_ISCLEAREDFLAGCHECK = "eCollection_Users_IsClearedFlag_Check";
        internal const string SP_USERSBULKUPLOAD = "eCollection_Teachers_BulkUpload";
        internal const string SP_GETERRORMESSAGES = "eCollection_Messages_Get";
        internal const string sp_EXCEPTIONMAIL = "eCollection_Exception_Insert";
        /************************** READING HISTORY START *********************************/
        internal const string SP_GETREADINGHISTORYMONTHS = "eCollection_Users_Histroy_Month_Get";
        internal const string SP_GETREADINGHISTORY = "eCollection_Users_Reading_Histroy_Get";
        internal const string SP_GETMYWORDS = "eCollection_Users_Words_Get";
        internal const string SP_GETMYRECORDINGS = "eCollection_Users_Recordings_Get";
        internal const string SP_GETMYRECORDINGFILES = "eCollection_Users_RecordedFiles_Get";
        internal const string SP_GETMYRHWORDSCOUNT = "eCollection_Users_RH_WordCount_Get";
        internal const string SP_GETMYWORDSCOUNT_MYRECORDINGSCOUNT = "eCollection_Users_WordsCount_RecordingsCount_Get";
        /************************** READING HISTORY END *********************************/

        internal const string FIRSTNAME_MANDATORY = "ECOLLTEACH02";
        internal const string LASTNAME_MANDATORY = "ECOLLTEACH03";
        internal const string EMAIL_MANDATORY = "ECOLLTEACH04";
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
        internal const string TEACHER_BLK_All_ALREADY_INVITED = "ECOLLTEACH23";
        internal const string EMAIL_INVALID = "ECOLLUSER19";
        internal const string ALREADY_PART_OF_SUBS1 = "ECOLLTEACH24";
        internal const string ALREADY_PART_OF_SUBS2 = "ECOLLTEACH25";
        internal const string ALREADY_PART_OF_SUBS3 = "ECOLLTEACH26";
        internal const string ALREADY_PART_OF_ANOTHER_SCHOOL1 = "ECOLLTEACH27";
        internal const string ALREADY_PART_OF_ANOTHER_SCHOOL2 = "ECOLLTEACH28";
        internal const string ALREADY_PART_OF_ANOTHER_SCHOOL3 = "ECOLLTEACH29";
        
        /*Info Messages*/
        internal const string NOGROUPINFO = "There are no groups to display.";
        internal const string NOREADHISTORYINFO = "There is no reading history to display.";



    }
}