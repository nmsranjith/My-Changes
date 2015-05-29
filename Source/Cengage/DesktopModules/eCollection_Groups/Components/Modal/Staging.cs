using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Groups.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Staging" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Staging
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Staging : ContentItem
    {
        private long _isbn;
        private string _description;
        private string _epubFileName;
        private int _noOfUniqueWords;
        private int _noOfUniqueWordAudioFiles;
        private int _noOfScreens;
        private int _noOfScreensAudioFiles;
        private char _active;
        private string _status;
        private DateTime _dateCreated;
        private string _userCreated;
        private string _server;
        private DateTime _dateOfUpload;
        private DateTime _timeOfUpload;
        private decimal _version;
        private string _title;
        private bool _checked;
        private string _mode;
      
        public long ISBN
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string EpubFileName
        {
            get { return _epubFileName; }
            set { _epubFileName = value; }
        }
        public int NoOfUniqueWords
        {
            get { return _noOfUniqueWords; }
            set { _noOfUniqueWords = value; }
        }
        public int NoOfUniqueWordAudioFiles
        {
            get { return _noOfUniqueWordAudioFiles; }
            set { _noOfUniqueWordAudioFiles = value; }
        }
        public int NoOfScreens
        {
            get { return _noOfScreens; }
            set { _noOfScreens = value; }
        }
        public int NoOfScreensAudioFiles
        {
            get { return _noOfScreensAudioFiles; }
            set { _noOfScreensAudioFiles = value; }
        }
        public char Active
        {
            get { return _active; }
            set { _active = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { _dateCreated = value; }
        }
        public string UserCreated
        {
            get { return _userCreated; }
            set { _userCreated = value; }
        }

        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        public DateTime DateOfUpload
        {
            get { return _dateOfUpload; }
            set { _dateOfUpload = value; }
        }
        public decimal Version
        {
            get { return _version; }
            set { _version = value; }
        }
        public DateTime TimeOfUpload
        {
            get { return _timeOfUpload; }
            set { _timeOfUpload = value; }
        }
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        public string Mode
        {

            get { return _mode; }
            set { _mode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            ISBN = long.Parse(dr["ISBN"].ToString());
            Server = Null.SetNullString(dr["SERVER"].ToString().ToUpper());
            Title = Null.SetNullString(dr["TITLE"].ToString());
            DateOfUpload = DateTime.Parse(dr["DATE_OF_UPLOAD"].ToString());
            Version = decimal.Parse(dr["VERSION"].ToString());
            TimeOfUpload = DateTime.Parse(dr["TIME_OF_UPLOAD"].ToString());
            Checked = false;
        }
    }
}