using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Content;
using DotNetNuke.Common.Utilities;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Common
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="IDCollection" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO Class for IDCollection as a key value pair
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class IDCollection : ContentItem, IComparable<IDCollection>
    {

        public IDCollection() { }
        public IDCollection(int id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
        private bool _checked;
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        private int _id = 0;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            Id = Null.SetNullInteger(dr["ID"]);
            Text = dr["Value"].ToString();
            Checked = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IDCollection other)
        {
            return _text.CompareTo(other.Text);
        }
    }
}