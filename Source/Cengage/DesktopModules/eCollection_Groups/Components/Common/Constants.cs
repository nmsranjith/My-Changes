using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Groups.Components.Common
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
        internal const string SP_GETGROUPLIST = "eCollection_Groups_Get";
        internal const string SP_GETALLGROUPLIST = "eCollection_Groups_GetAll";
        internal const string SP_GROUPSEARCH = "eCollection_GroupSearch_Get";
        internal const string SP_ADDGROUP= "eCollection_Groups_AddGroup";
        internal const string SP_ADDGROUPMEMBER = "eCollection_Groups_AddGroupMember";
        internal const string SP_GROUPNAMEVALIDATION = "eCollection_GroupNameValidation_Get";
        internal const string SP_GETGROUPPROFILE = "eCollection_GroupProfile_Get";
        internal const string SP_GETPROFILEREADINGLEVEL = "eCollection_ReadingLevel_Get";
        internal const string SP_GETPROFILEREADINGHISTORY = "eCollection_ReadingHistory_Get";
        internal const string SP_GETPROFILEREADINGSESSION = "eCollection_ReadingSession_Get";
        internal const string SP_GETPROFILEREADINGSESSIONBYCAT = "eCollection_ReadingSession_GetBySelectionCat";
        internal const string SP_GETSUBSCRIPTION = "eCollection_Subscription_Get";
        internal const string SP_GETSTUDENTBYSUBS = "eCollection_Students_List";
        internal const string SP_GETTEACHERBYSUBS = "eCollection_Groups_GetTeachersBySubscription";
        internal const string SP_GETTEACHER = "eCollection_Teachers_Get";
        internal const string SP_GETSTUDENTBYGROUP = "eCollection_Groups_GetStudentByGroup";
        internal const string SP_GETGROUPMEMBER = "eCollection_Members_Get";
        internal const string SP_GETPROFILERECORDINGHISTORY = "eCollection_RecordingHistory_Get";
        internal const string SP_UPDATEGROUPMEMBERS = "eCollection_Members_Update";
        internal const string SP_UPDATEGROUP = "eCollection_Groups_Update";
        internal const string SP_ADDEDITEDGROUP = "eCollection_EditGroup_AddGroupMember";
        internal const string SP_DELETEGROUP = "eCollection_Groups_Delete";
        internal const string SP_GETCLASSBYSUBS = "eCollection_Groups_GetClassBySubscription";
        internal const string SP_GETCOLORBYREADLEVEL = "eCollection_Groups_GetColorByReadLevel";
        internal const string SP_GETCIRCLEDWORDS = "eCollection_Groups_Words_Get";
        internal const string SP_GETMONTH = "eCollection_Groups_GetMonth";
        internal const string SP_GETLASTSEVENDAYSREADING = "eCollection_Groups_GetLastSevenDaysReadings";
        internal const string SP_GETLASTSEVENDAYSRECORDING = "eCollection_Groups_GetLastSevenDaysRecordings";
        internal const string SP_GETREADINGLEVELBYGROUP = "eCollection_Groups_GetReadingLevelByGroup";
        internal const string SP_GETBOOKCOUNT = "eCollection_Groups_GetBookCount";
        internal const string SP_ADDSTAGING = "eCollection_Staging_Add1";
        internal const string SP_GETSTAGING = "eCollection_Staging_Get";
        internal const string SP_GETSEARCHSTAGING = "eCollection_Staging_GetSearch";
        internal const string SP_ADDPUBLISH="eCollection_Publish_Add";
        internal const string SP_DELETESTAGING = "eCollection_Staging_Delete";
        internal const string SP_CHECKISBN = "eCollection_Staging_CheckIsbn";
        internal const string SP_GETISBNDETAILS = "eCollection_PMeBook_Get";
        internal const string SP_GETERRORMESSAGES = "eCollection_Messages_Get";
        internal const string SP_GETREADINGSESSIONWRAPPER = "eCollection_Groups_GetReadingSessionWrapper";
        internal const string SP_GETREADINGSESSIONWRAPPERBYCAT = "eCollection_Groups_GetReadingSessionWrapperBySectionCat";
        internal const string SP_UPDATEISCLEARFROMIPAD = "eCollection_Groups_ClearFromIpad";
        internal const string sp_EXCEPTIONMAIL = "eCollection_Exception_Insert";
        internal const string SP_GETGROUPSEARCH = "eCollection_Groups_GetSearch";
        internal const string SP_GETSTUDENTSEARCH = "eCollection_Groups_GetStudentsBySubscription";
        internal const string SP_GETGROUPNAME = "eCollection_Groups_GetGroupName";
        internal const string SP_GETTEACHERSEARCH = "eCollection_Groups_GetTeachersSearch";
        internal const string SP_GETMINMAXLEVEL ="eCollection_Groups_GetMinMaxReadingLevelByDate";
        internal const string SP_GETSTUDENTSEARCHBYGROUP = "eCollection_Groups_GetStudentSearchByGroup";
        internal const string SP_GETGROUPOWNERID = "eCollection_Groups_Owner_Id_Get";
        
        
        internal const string MS_MEMBERVALIDATION = "ECOLLGRP01";
        internal const string MS_GRPNAMEEXIST = "ECOLLGRP02";
        internal const string MS_DONTDELOWNERGRP = "ECOLLGRP03";
        internal const string MS_NOSTUDTOPRINT = "ECOLLGRP04";
        internal const string MS_GRPNAMEVALIDATION = "ECOLLGRP05";


        internal const string MS_SELECTZIPFILE = "ECOLLSTG01";
        internal const string MS_FILEFORMATVALID = "ECOLLSTG02";
        internal const string MS_FILEEXISTINSTAGE = "ECOLLSTG03";
        internal const string MS_STAGESUCCESS = "ECOLLSTG04";
        internal const string MS_FILEEXISTINPUBLISH = "ECOLLSTG05";
        internal const string MS_DELETESUCCESS = "ECOLLSTG06";
        internal const string MS_UPLOADSUCCESS = "ECOLLSTG07";
        internal const string MS_ZIPFILEVALID = "ECOLLSTG08";
        internal const string MS_OTHERFILEFORMAT = "ECOLLSTG09";
       
        //TEMP

        internal const string SP_GETLOGINDETAILS = "eCollection_Groups_GetLoginDetails";


        /*No Record Info*/
        internal const string NOREADHISTORYINFO = "There is no reading history to display.";
        internal const string NOSTUDENTINFO = "There are no students to display.";
        internal const string NOGROUPINFO = "There are no groups to display.";
        
    }
}