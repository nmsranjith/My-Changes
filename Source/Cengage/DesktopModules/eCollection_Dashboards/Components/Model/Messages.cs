using DotNetNuke.Entities.Content;

namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Model
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
        private string _messagecode;
        private string _messagedesc;

        public string MessageCode
        {
            get
            {
                return _messagecode;
            }
            set { _messagecode = value; }
        }

        public string MessageDesc
        {
            get
            {
                return _messagedesc;
            }
            set { _messagedesc = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        public override void Fill(System.Data.IDataReader dr)
        {
            MessageCode = dr["MESSAGE_CODE"] as string;
            MessageDesc = dr["MESSAGE_DESCRIPTION"] as string;
        }
    }
}