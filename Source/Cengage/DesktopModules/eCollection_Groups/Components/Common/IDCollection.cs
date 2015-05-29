using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using System.Globalization;

namespace DotNetNuke.Modules.eCollection_Groups.Components.Common
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="IDCollection" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO Class for IDCollection as a key value pair
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class IDCollection : ContentItem, IComparable<IDCollection>, IEquatable<IDCollection>
    {
        public IDCollection() { }
        public IDCollection(int id, string text) { this._id = id; this._text = text; }
        private int _id = 0;
        private bool _checked;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private string _text = string.Empty;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            try
            {
                string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dr["FirstName"].ToString().ToLower() + " " + dr["LastName"].ToString().ToLower());
                Text = name.Length > 40 ? name.ToString().Take(40).ToString() + "..." : name.ToString();
                Id = int.Parse(dr["UserId"].ToString());
                Checked = false;
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IDCollection other)
        {
            if (this.Id == other.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IDCollection other)
        {
            return Text.CompareTo(other.Text);
        }
    }
}