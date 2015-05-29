using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DotNetNuke.Modules.HESearchResults.Controls
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SearchEngine" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    searchresult parameter class.
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class SearchEngine
    {
        // For Common Search
        public int PageNumber { get; set; }
        public string Division { get; set; }
        public int StoreSK { get; set; }
        public string SearchText { get; set; }
        public string ExactText { get; set; }
        public int NumberOfResults { get; set; }
        public string SortOrder { get; set; }
        public string Facets { get; set; }
        public string CountryCode { get; set; }
        public DataTable AttributeStateTable { get; set; }

        // For AdvanceSearch keyword
        public string TitleWeighate { get; set; }
        public string AuthorWeighate { get; set; }
        public string KeyWordWeightage { get; set; }
        public string SubjectWeighate { get; set; }
        public string AndAllwordsWeighate { get; set; }
        public string ExactphraseWeighate { get; set; }
        public string AttributeWeighate { get; set; }
        public string AttributeValueWeighate { get; set; }
    }
}