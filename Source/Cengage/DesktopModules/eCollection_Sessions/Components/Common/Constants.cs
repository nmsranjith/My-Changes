using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Common
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Constants" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Class for Constants
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    internal static class Constants
    {
        internal const string SP_GETSESSIONLIST = "eCollection_Session_Get";
        internal const string SP_GETSUBSCRIPTION = "eCollection_Session_GetSubscription";
        internal const string SP_GETTEACHERSLIST = "eCollection_Session_GetTeachersList";
        internal const string SP_ADDSESSION = "eCollection_Session_Save";
        internal const string SP_ADDSESSIONMEMBERS = "eCollection_Sessionmembers_Save";
        internal const string SP_ADDSESSIONPRODUCTS = "eCollection_SessionProducts_Save";
        internal const string SP_GETGROUPSLIST = "eCollection_Users_Groups_Get";
       // internal const string SP_GETTEACHERNAME = "eCollection_Session_Teachers_Get";
        internal const string SP_GETTEACHERNAME = "eCollection_Teachers_Get";
        internal const string SP_GETBOOKS = "eCollection_Session_GetBooks";
        internal const string SP_GETSTUDENTSLIST = "eCollection_Session_GetStudents";
        internal const string SP_UPDATEEXPIRYDATE = "eCollection_Session_UpdateExpiryDate";
        internal const string SP_DELETESESSION = "eCollection_Session_Delete";
        internal const string SP_SESSIONOPENEDSTUDENTS = "eCollection_Session_OpenedStudents"; // SessionProfile
        internal const string SP_SESSIONUNOPENEDSTUDENTS = "eCollection_Session_UnOpenedStudents"; // SessionProfile
        internal const string SP_SESSIONMEMBERSGET = "eCollection_SessionMembers_Get";
        internal const string SP_SESSIONPRODUCTGET = "eCollection_SessionProducts_Get";
        internal const string SP_UPDATESESSIONMEMBERS =   "eCollection_SessionMembers_Update";
        internal const string SP_UPDATEEXPIRYDATEALONE = "eCollection_Session_UpdateExpiryDateAlone";
        internal const string SP_GETREADINGHISTORY = "eCollectionSession_ReadingHistory_Get";
        internal const string SP_GETRECORDINGHISTORYGET = "eCollectionSession_RecordingHistory_Get";
        internal const string SP_GETREADINGHISTORYLASTSEVEN = "eCollectionSession_ReadingHistory_GetLastSevenDays";
        internal const string SP_GETRECORDINGHISTORYLASTSEVEN = "eCollection_Session_RecordingHistory_GetLastSevenDays";
        internal const string sp_GETREADINGHISTORYBYSESSIONMONTHS = "eCollection_Session_ReadingHistory_GetMonths";
        internal const string SP_GETPROFILEREADINGLEVEL = "eCollection_ReadingLevel_Get";
        internal const string SP_GETSTUDENTBYGROUP = "eCollection_Groups_GetStudentByGroup";
        internal const string SP_GETCLASSBYSUBS = "eCollection_Session_GetClassBySubscription";
        internal const string SP_GROUPSEARCH = "eCollection_GroupSearch_Get";
        internal const string SP_GETGROUPBYSUBS = "eCollection_Session_GetGroupsBySubscription";
        internal const string SP_GETBOOKSBYREADLEVEL = "eCollection_Books_GetByReadingLevelsAppended";
        internal const string SP_GETERRORMESSAGES = "eCollection_Messages_Get";
        internal const string sp_EXCEPTIONMAIL = "eCollection_Exception_Insert";

        internal const string SP_GETLOOKUPNAMES = "eCollection_Session_GetLookUpNames";
        internal const string SP_GETNAMESFORSEARCH = "eCollection_Session_GetNamesForSearch";
        internal const string SP_GETSTUDENTBYSUBS = "eCollection_Students_List";
        internal const string SP_GETSTUDENTNAMESFORSEARCH = "eCollection_Session_GetStudentsNameForSearch";
        internal const string SP_GETGROUPSEARCH = "eCollection_Groups_GetSearch";
        internal const string SP_GETGROUPNAME = "eCollection_Groups_GetGroupName";
        internal const string SP_GETTEACHERSSEARCH = "eCollection_Session_GetTeachersNamesForSearch";
        internal const string SP_GETBOOKSCATEGORIES = "eCollection_Session_AttributeValues_get";
        internal const string SP_GETBOOKSSEARCH = "eCollection_Books_GetBooksSearch";
        internal const string SP_GETBOOKLISTBYREADLEVEL = "eCollection_BookList_GetByReadingLevels";
        internal const string SP_GETBOOKLISTBYSEARCH = "eCollection_BookList_GetBySearch";
        

        // Error Messages
        internal const string MESSAGES_SUBSCRIPTION = "ECOLLSESS01";
        internal const string MESSAGES_NOTEBOOKS = "ECOLLSESS02";
        internal const string MESSAGES_SESSIONNAME = "ECOLLSESS03";
        internal const string MESSAGES_STUDENT = "ECOLLSESS04";
        internal const string MESSAGES_END = "ECOLLSESS05";
        internal const string MESSAGES_DELETE = "ECOLLSESS06";

        /*No Record Info*/
        internal const string NOBOOKINFO = "There are no books to display.";
        internal const string NOREADHISTORYINFO =  "There is no reading history to display.";
        internal const string  NOSESSIONINFO = "There are no sessions to display.";
        internal const string NOSTUDENTSINFO = "There are no students to display.";
        internal const string NOBOOKSINFO = "There are no books to display.";
        internal const string NOGROUPSINFO = "There are no groups to display.";

    }
}