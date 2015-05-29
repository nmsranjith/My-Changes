using System.Xml;

namespace DotNetNuke.Modules.eCollection_Dashboards.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Audio" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Audio
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Audio
    {
        public string ISBN { get; set; }
        public XmlDocument AudioXml { get; set; }
        public string FileType { get; set; }
    }
}