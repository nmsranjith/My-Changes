using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.eCollection_Groups.Components
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
            Insert,
            Update,
            Delete
        }

        public  enum  GroupType
        {
            Class = 'C',
            Group='N'
        }

        public enum Active
        {
            Yes='Y',
            No='N'
        }
    }
}