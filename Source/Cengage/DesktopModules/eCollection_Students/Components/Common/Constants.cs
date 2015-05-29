using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Students.Components.Common
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
        internal const string SP_GETSTUDENTSLIST = "eCollection_Students_List";
        internal const string SP_GETSUBSCRIPTIONSLIST = "eCollection_Students_Subscriptions_Get";
        internal const string SP_ADDSTUDENTPROFILE = "eCollection_Students_Insert";
        internal const string SP_PRINT_DELETESTUDENTSPROFILE = "eCollection_Students_Print_Delete";
        internal const string SP_CHECKUSERNAMEEXISTS = "eCollection_Students_UserName_Check";
        internal const string SP_UPDATESTUDENTPROFILE = "eCollection_Students_Update";
        internal const string SP_UPDATESTUDENTPROFILEVALUES = "eCollection_Students_Profile_Update";
        internal const string SP_GET_MY_WORDS_RECORDINGS = "eCollection_User_Words_Recordings_Get";
        internal const string SP_GETPROFILEDETAILS = "eCollection_Students_Details_Get";
        internal const string SP_GETPREADINGHISTORY = "eCollection_Students_ReadingHistory_Get";
        internal const string SP_GETTEACHERNAMES = "eCollection_Teachers_Get";
        internal const string SP_GETCLASSMEMBERS = "eCollection_Students_ClassMembers_Get";
        internal const string SP_UPLOADSTUDENTPROFILES = "eCollection_Students_BulkUpload";
        internal const string SP_GETALLSTUDENTGROUPS = "eCollection_Students_Groups_Get";
        internal const string SP_GETALLSUBSGROUPS = "eCollection_Users_Groups_Get";
        internal const string SP_ADDSTUDENTTOGROUPS = "eCollection_Students_AddStudToGrp";
        internal const string SP_GETREADINGLEVEL = "eCollection_Students_ReadingLevel_Get";
        internal const string SP_USERSBULKUPLOAD = "eCollection_Students_Users_BulkUpload";
        internal const string SP_CLEARFROMIPAD = "eCollection_Users_ClearFromIpad";
        internal const string SP_ISCLEAREDFLAGCHECK = "eCollection_Users_IsClearedFlag_Check";
        internal const string SP_GETMINMAXREADLEVELBYDATE = "eCollection_Stud_GetMinMaxReadingLevelByDate";
        internal const string SP_GETERRORMESSAGES = "eCollection_Messages_Get";
        internal const string sp_EXCEPTIONMAIL = "eCollection_Exception_Insert";

        /************    EC-1201     ***************/
        internal const string SP_GETALLUNALLOCATEDSTUDENTS = "eCollection_Students_Unallocated_List";
        internal const string SP_GETUPDATESTUDENTLICENSEALLOCATION = "eCollection_Students_License_Allocation_Update";
        internal const string SP_SWITCHSTUDENTSUBSCRIPTION = "eCollection_Students_Switch_Subscriptions";
        internal const string SP_GETACTIVESUBSCRIPTIONSLIST = "eCollection_Students_Active_Subscriptions_Get";
        /************ EC-1201 - end ***************/

        internal const string FIRSTNAME_MANDATORY = "ECOLLSTU01";
        internal const string LASTNAME_MANDATORY = "ECOLLSTU02";
        internal const string PASSWORD_MANDATORY = "ECOLLSTU03";
        internal const string FIRSTNAME_MANDATORY_EDIT = "ECOLLSTU19";
        internal const string LASTNAME_MANDATORY_EDIT = "ECOLLSTU20";
        internal const string PASSWORD_MANDATORY_EDIT = "ECOLLSTU21";
        internal const string USERNAME_MANDATORY = "ECOLLSTU04";
        internal const string VALIDATE_FIRSTNAME = "ECOLLUSER03";
        internal const string VALIDATE_LASTNAME = "ECOLLUSER04";
        internal const string VALIDATE_EMAIL = "ECOLLUSER02";
        internal const string CREATE_SUCCESS1 = "ECOLLSTU11";
        internal const string UPLOAD_SUCCESS = "ECOLLSTU23";
        internal const string CREATE_SUCCESS2 = "ECOLLUSER06";
        internal const string SESSION_EXPIRE = "ECOLLUSER12";
        internal const string EDIT_FAILED = "ECOLLUSER14";
        internal const string EDIT_NO_CHANGE = "ECOLLUSER13";
        internal const string GROUPS_SELECTED = "ECOLLUSER05";
        internal const string STUDENTS_ADDED_TO_GRPS = "ECOLLSTU15";
        internal const string SELECT_STUDENTS = "ECOLLSTU16";

        internal const string VALIDATE_DOB1 = "ECOLLSTU07";
        internal const string VALIDATE_DOB2 = "ECOLLSTU18";
        internal const string VALIDATE_PASSWORD1 = "ECOLLSTU08";
        internal const string VALIDATE_PASSWORD2 = "ECOLLUSER16";
        internal const string VALIDATE_USERNAME1 = "ECOLLSTU05";
        internal const string VALIDATE_USERNAME2 = "ECOLLSTU10";
        internal const string VALIDATE_USERNAME3 = "ECOLLSTU09";

        internal const string VALIDATE_BROWSETXTBX = "ECOLLUSER07";
        internal const string VALIDATE_EMPTY_FILE = "ECOLLUSER08";
        internal const string VALIDATE_MISMATCH_FILE = "ECOLLUSER09";
        internal const string UPLOAD_WAIT = "ECOLLUSER15";

        internal const string FIRSTNAME_MANDATORY_UPLOAD = "ECOLLSTU12";
        internal const string LASTNAME_MANDATORY_UPLOAD = "ECOLLSTU13";
        internal const string PASSWORD_MANDATORY_UPLOAD = "ECOLLSTU24";
        
        internal const string GRADE_MANDATORY_UPLOAD = "ECOLLSTU14";
        internal const string VALIDATE_LICENCES = "ECOLLSTU17";
        internal const string VALIDATE_MISMATCH_PATH = "ECOLLUSER01";

        /************************** READING HISTORY START *********************************/
        internal const string SP_GETREADINGHISTORYMONTHS = "eCollection_Users_Histroy_Month_Get";
        internal const string SP_GETREADINGHISTORY = "eCollection_Users_Reading_Histroy_Get";
        internal const string SP_GETMYWORDS = "eCollection_Users_Words_Get";
        internal const string SP_GETMYRECORDINGS = "eCollection_Users_Recordings_Get";
        internal const string SP_GETMYRECORDINGFILES = "eCollection_Users_RecordedFiles_Get";
        internal const string SP_GETMYRHWORDSCOUNT = "eCollection_Users_RH_WordCount_Get";
        internal const string SP_GETMYWORDSCOUNT_MYRECORDINGSCOUNT = "eCollection_Users_WordsCount_RecordingsCount_Get";
        /************************** READING HISTORY END *********************************/


        /*No Record Info*/
        internal const string NOGROUPINFO = "There are no groups to display.";
        internal const string NOREADHISTORYINFO = "There is no reading history to display.";
        internal const string NOSTUDENTSINFO= "There are no students to display.";


   }
}