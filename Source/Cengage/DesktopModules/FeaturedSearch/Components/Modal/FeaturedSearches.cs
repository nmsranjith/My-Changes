using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.FeaturedSearch.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="FeaturedSearches" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Featured Search Parameters
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------

    public class FeaturedSearches
    {
        public int FeaturedSearchSk { get; set; }
        public string SearchName { get; set; }
        public string CurrentUrl { get; set; }
        public string Division { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Subject { get; set; }
        public string Format { get; set; }
        public string LibraryType { get; set; }
        public string Origin { get; set; }
        public string PublishedYear { get; set; }
        public string Publisher { get; set; }
        public string EbookPlatform { get; set; }
        public string AllWords { get; set; }
        public string ExactPhrase { get; set; }
        public string NoneOfThese { get; set; }
        public int ModuleId { get; set; }
    }
}