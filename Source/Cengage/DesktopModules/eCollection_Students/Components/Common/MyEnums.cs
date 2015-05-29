using System;
using System.ComponentModel;
using System.Reflection;

namespace DotNetNuke.Modules.eCollection_Students.Components.Common
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
        public enum Users
        {
           [Description("ECOLL")]
            UserDomain = 1,
            InternalUse='N'
        }
        public enum Active
        {
            Yes='Y',
            No='N'
        }
        public enum TradingPartnerUser
        {
            [Description("USER ")]
            Prefix=1,
            [Description(" IS STUDENT OF THIS INSTITUTION")]
            Description=2,
            PurchaseFlag='N',
            IsDefaultPartner='N'
        }
        public enum ESL
        {
            Yes='Y',
            No='N'
        }
        public enum ReadingRecovery
        {
            Yes = 'Y',
            No = 'N'
        }
        public enum Update
        {
            [Description("ReadingRecovery")]
            ReadingRecovery=1,
            [Description("ReadingLevel")]
            ReadingLevel=2,
            [Description("BookReadingLevel")]
            BookReadingLevel = 3,
            [Description("ESL")]
            ESL = 4
        }      
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="EnumHelper" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A class for EnumHelper
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public static class EnumHelper
    {
        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetDescription(Enum en)
        {            
            FieldInfo field = en.GetType().GetField(en.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;
            return attribute == null ? en.ToString() : attribute.Description;
        }

    }
}