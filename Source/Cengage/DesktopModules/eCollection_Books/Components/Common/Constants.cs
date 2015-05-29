using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Books.Components.Common
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
        internal const string SP_GETBOOKS = "eCollection_Books_GetBooks";
        internal const string SP_GETGRACEPERIOD = "eCollection_Books_GracePeriod_get";
        internal const string SP_GETREADINGAGE = "eCollection_Books_ReadingAge_get";
        internal const string SP_GETREADINGAGEBOOKS = "eCollection_Books_GetReadingAgeBooks";
        internal const string SP_GETCATEGORIES = "eCollection_Books_Categories_get";
        internal const string SP_GETLEVELS = "eCollection_Books_Levels_get";
        internal const string SP_ADDSUBSCRIPTION = "eCollection_Books_AddToSubscription";
        internal const string SP_REMOVESUBSCRIPTION = "eCollection_Books_RemoveBooks";
        internal const string SP_GETBOOKSBYCATEGORIES = "eCollection_Books_GetBooksByCategories";
        internal const string SP_GETBOOKSBYREADLEVEL = "eCollection_Books_GetByReadingLevelsAppended";
        internal const string SP_ISBOOKSADDED = "eCollection_Books_IsBookAdded";
        internal const string SP_GETBOOKLISTBYREADLEVEL = "eCollection_BookList_GetByReadingLevels";
        internal const string SP_GETBOOKLISTBYREADINGAGE = "eCollection_BookList_GetByReadingAge";
        internal const string SP_GETBOOKLISTBYSEARCH = "eCollection_BookList_GetBySearch";
        internal const string SP_GETERRORMESSAGES = "eCollection_Messages_Get";
        internal const string SP_GETBOOKSSEARCH = "eCollection_Books_GetBooksSearch";
        internal const string SP_GETBOOKSCATEGORIES = "eCollection_Session_AttributeValues_get";
        internal const string sp_EXCEPTIONMAIL = "eCollection_Exception_Insert";
        internal const string SP_AUTOASSIGNBOOKS = "eCollection_Books_AutoAssignBooks";
        internal const string SP_GETSELECTEDBOOKCOUNT = "eCollection_Books_SelectedCount_Get";
        internal const string SP_GETBOOKSCOUNTFORREDIRECTION = "eCollection_Books_GetBooksCount";
        internal const string sp_GRACEPERIODNOTIFICATIONMAIL = "eCollection_Books_EmailBeforeGracePeriodNoification";
        internal const string SP_GETBOOKPACK = "eCollection_Books_Get_All_Book_Packs";
        internal const string SP_GETBOOKPACKBOOKS = "eCollection_Books_Get_Book_Pack_Items";
        internal const string SP_GETCUSTOMPACKBOOKS = "eCollection_Books_Get_Custom_Pack_Items";
        internal const string SP_SEARCHBOOKPACKBOOKS = "eCollection_Books_Search_Book_Pack_Items";
        internal const string SP_SEARCHCUSTOMPACKBOOKS = "eCollection_Books_Search_Custom_Pack_Items";
        internal const string SP_SAVECUSTOMBOOKPACK = "eCollection_Books_SaveCustomBookPack";
        internal const string SP_SETBOOKPACK = "eCollection_Books_SetBookPack";
        internal const string SP_GETBOOKPACKSTATUS = "eCollection_Books_GetBookPackStatus";
        

        internal const string MESSAGES_BOOKSRANGE = "ECOLLBOOKS01";
        internal const string MESSAGES_ALREADYADDED = "ECOLLBOOKS02";

        /*No Record Info*/
        internal const string NOBOOKSINFO = "There are no books to display.";
    }
}