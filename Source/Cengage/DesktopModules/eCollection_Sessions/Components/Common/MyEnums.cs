using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Common
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="MyEnums" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A class for MyEnums
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class MyEnums
    {

        public enum CrudState
        {
            Insert ,
            Update,
            Delete
        }

        public enum SessionType
        {
            Guided = 1,
            Independent =2
        }


        public enum WorkType
        {
            Session ,
            BOOK
        }

        public enum Status
        {
            Yes='Y',
            No = 'N'
        }

        public enum GroupType
        {
            C = 'Y',
            N = 'N'
        }
    }
}