
using DotNetNuke.Entities.Content;
namespace DotNetNuke.Modules.eCollection_Students.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Messages" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Messages
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Messages : ContentItem
    {
        private string _messageCode;
        private string _messageDesc;
        public string MessageCode
        {
            get
            {
                return _messageCode;
            }
            set { _messageCode = value; }
        }
        public string MessageDesc
        {
            get
            {
                return _messageDesc;
            }
            set { _messageDesc = value; }
        }

        /// <summary>
        /// CBO Fill Method
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            MessageCode = dr["MESSAGE_CODE"] as string;
            MessageDesc = dr["MESSAGE_DESCRIPTION"] as string;
        }
    }
}