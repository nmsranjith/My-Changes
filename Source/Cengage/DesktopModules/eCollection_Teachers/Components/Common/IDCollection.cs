using DotNetNuke.Entities.Content;

namespace DotNetNuke.Modules.eCollection_Teachers.Components.Common
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="IDCollection" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO Class for IDCollection as a key value pair
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class IDCollection:ContentItem
    {
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

        #region Overridden Fill Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {

            Id = int.Parse(dr["Id"].ToString());
            Text = dr["Text"].ToString();
        }
        #endregion
    }
}