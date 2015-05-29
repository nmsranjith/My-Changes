using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;
using System;

namespace DotNetNuke.Modules.eCollection_Students.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentReadingHistory" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for StudentReadingHistory
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class StudentReadingHistory : ContentItem
    {
        public int ProductId { get; set; }
        public string FileName { get; set; }
        public string SessionType { get; set; }
        public string Title { get; set; }
        public DateTime OpenedDate { get; set; }
        public string BookOpenedDate { get; set; }
        public string BookOpenedTime { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public string SessionTypeImage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            ProductId = Null.SetNullInteger(dr["ProductId"]);
            FileName = Null.SetNullString(dr["FileName"]);
            SessionType = Null.SetNullString(dr["SessionType"]);
            Title = Null.SetNullString(dr["Title"]);
            OpenedDate = Null.SetNullDateTime(dr["OpenedDate"]);
            BookOpenedDate = Null.SetNullString(dr["BookOpenedDate"]);
            BookOpenedTime = Null.SetNullString(dr["BookOpenedTime"]);
            Minutes = Null.SetNullInteger(dr["MinOpened"]);
            Seconds = Null.SetNullInteger(dr["SecOpened"]);
            SessionTypeImage = Null.SetNullString(dr["SessionTypeImage"]);
        }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentReadingHistoryMonths" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for StudentReadingHistoryMonths
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class StudentReadingHistoryMonths : ContentItem
    {
        public int MonthId { get; set; }
        public string OpenedMonths { get; set; }
        public int year { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr1"></param>
        public override void Fill(System.Data.IDataReader dr1)
        {
            MonthId = Null.SetNullInteger(dr1["MonthId"]);
            OpenedMonths = Null.SetNullString(dr1["OpenedMonths"]);
            year = Null.SetNullInteger(dr1["year"]);       
        }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentRecordings" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for StudentRecordings
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class StudentRecordings : ContentItem
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string OpenedDate { get; set; }
        public string BookOpenedDate { get; set; }
        public string BookOpenedTime { get; set; }
        public string Seconds { get; set; }
        public string Minutes { get; set; }
        public string SessionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr1"></param>
        public override void Fill(System.Data.IDataReader dr1)
        {
            ImageUrl = Null.SetNullString(dr1["ImageUrl"]);
            Title = Null.SetNullString(dr1["Title"]);
            OpenedDate = Null.SetNullString(dr1["OpenedDate"]);
            BookOpenedDate = Null.SetNullString(dr1["BookOpenedDate"]);
            BookOpenedTime = Null.SetNullString(dr1["BookOpenedTime"]);
            Minutes = Null.SetNullString(dr1["MinOpened"]);
            Seconds = Null.SetNullString(dr1["SecOpened"]);
            SessionType = Null.SetNullString(dr1["SessionType"]);
        }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentRecordingFiles" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for StudentRecordingFiles
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class StudentRecordingFiles : ContentItem
    {
        public int ProductId { get; set; }
        public string OpenedDate { get; set; }
        public string PageName { get; set; }
        public string RecPath { get; set; }
        public string SessionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr1"></param>
        public override void Fill(System.Data.IDataReader dr1)
        {
            ProductId = Null.SetNullInteger(dr1["ProductId"]);
            OpenedDate = Null.SetNullString(dr1["OpenedDate"]);
            PageName = Null.SetNullString(dr1["PageName"]);
            RecPath = Null.SetNullString(dr1["RecPath"]);
            SessionType = Null.SetNullString(dr1["SessionType"]);
        }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentWords" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for StudentWords
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class StudentWords : ContentItem
    {
        public string Word { get; set; }
        public int WordCount { get; set; }
        public int CircledMonth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr1"></param>
        public override void Fill(System.Data.IDataReader dr1)
        {
            Word = Null.SetNullString(dr1["Word"]);
            WordCount = Null.SetNullInteger(dr1["WordCount"]);
            CircledMonth = Null.SetNullInteger(dr1["CircledMonth"]);
        }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="StudentRHWordCount" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for StudentRHWordCount
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class StudentRHWordCount : ContentItem
    {
        public int WordCount { get; set; }
        public string OpenedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr1"></param>
        public override void Fill(System.Data.IDataReader dr1)
        {
            WordCount = Null.SetNullInteger(dr1["WordCount"]);
            OpenedDate = Null.SetNullString(dr1["OpenedDate"]);
        }
    }
}