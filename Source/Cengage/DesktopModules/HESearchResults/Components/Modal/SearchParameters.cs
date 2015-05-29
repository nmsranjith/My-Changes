using System.Data;

namespace DotNetNuke.Modules.HESearchResults.Components.Modal
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SearchParameters" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Search Parameters
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class SearchParameters
    {
        public int UserSk { get; set; }
        public string ProductSk { get; set; }
        public string ActionType { get; set; }
        public string CurrentUrl { get; set; }
        public string SearchText { get; set; }
        public string SearchName { get; set; }
        public string KeyWords { get; set; }
        public char AudienceCodeFlag { get; set; }
        public char PublishedFlag { get; set; }
        public string Facets { get; set; }
        public int PageNumber { get; set; }
        public string Division { get; set; }
        public int StoreSK { get; set; }
        public string Condition { get; set; }
        public string ExactText { get; set; }
        public string Weightage { get; set; }
        public string FormsString { get; set; }
        public int NumberOfResults { get; set; }
        public string SortOrder { get; set; }
        public string Country { get; set; }
        public string DisciplineType { get; set; }
        public string CategoryValue { get; set; }
        public string DisciplineValue { get; set; }
        public string SubjectValue { get; set; }
        public DataTable ShowOnlyAttributesTable { get; set; }
        public DataTable FacetAttributesTable { get; set; }
        public DataTable SaveSearchTable { get; set; }
        public DataTable AdvFacets { get; set; }
        public DataTable AttributeStateTable { get; set; }
        public int TabId { get; set; }
        public string TabName { get; set; }
        public string TabUrl { get; set; }
        public string TabDescription { get; set; }
        public string[] WordsSearched { get; set; }
        public string GstFlag { get; set; }
        //For Browse
        public string BwsFormat { get; set; }
        public DataTable GaleAreaTable { get; set; }
        public DataTable GaleDisciplineTable { get; set; }

        // For AdvanceSearch keyword
        public string ExactTitle { get; set; }
        public string ExactAuthor { get; set; }
        public string TitleWeightage { get; set; }
        public string AuthorWeightage { get; set; }
        public string KeyWordWeightage { get; set; }
        public string SubjectWeightage { get; set; }
        public string AndAllwordsWeighate { get; set; }
        public string ExactphraseWeightage { get; set; }
        public string NonephraseWeightage { get; set; }
        public string AttributeWeightage { get; set; }
        public string AttributeValueWeightage { get; set; }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Products" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Products
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class Products
    {
        public string TITLE { get; set; }
        public string IMAGE_FILE_NAME { get; set; }
        public string PREFERRED_NAME { get; set; }
        public string ISBN_13 { get; set; }
        public int PRODUCT_SK { get; set; }
        public string PUBLICATION_DATE { get; set; }
        public string PRINT_PRICE { get; set; }
        public string EBOOK { get; set; }
        public string ECHAPTER { get; set; }
        public string SUBPRODUCT_TYPE { get; set; }
        public string AUDIENCE_TARGET { get; set; }
        public string NEW_EDITION { get; set; }
        public string EDITION { get; set; }
        public string SUPPLEMENTS { get; set; }
        public string CODE { get; set; }
        public string CODE_NAME { get; set; }
        public string PRODUCT_FORMAT { get; set; }
        public string FAVOURITE_FLAG { get; set; }
        public string DetailUrl { get; set; }
        public string CoverType { get; set; }
        public string ToolTip { get; set; }
        public int AccountNumber { get; set; }

        public string PriceVal { get; set; }
        public bool DiscountFlag
        {
            get;
            set;
        }
        public double DiscountVal { get; set; }
        public bool RrpFlag { get; set; }
        public string StockAvail { get; set; }
        public string AllowSale { get; set; }
        public string IsCachedPrice { get; set; }
        public double RRPPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double DiscountRate { get; set; }
        public int Stockqty { get; set; }

    }


    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="DisciplineCategorySubjects" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Discipline Category , Discipline and Subject informations
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class DisciplineCategorySubjects
    {
        public int Discipline_Category_Name_Sk { get; set; }
        public int Discipline_Sk { get; set; }
        public int Subject_Sk { get; set; }
        public string Discipline_Category_Name { get; set; }
        public string Discipline { get; set; }
        public string Subject_Name { get; set; }
        public int Products_Count { get; set; }
    }

    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="ShowOnlyAttributes" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    DAO class for Show Only Attributes
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class ShowOnlyAttributes
    {
        public string SHOWONLY_FILTER_TYPE { get; set; }
    }
}