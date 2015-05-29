/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   New and Notable products
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */
using System.Data;

namespace DotNetNuke.Modules.NewAndNotable.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="NewModel" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To Store Data
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class NewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Division { get; set; }
        public string Isbns { get; set; }
        public int DisplayCount { get; set; }
        public int UserSK { get; set; }
        public bool IsSeeAll { get; set; }
        public DataTable IsbnTable { get; set; }
        public string Isbn_13 { get; set; }
        public string Country { get; set; }
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
    }
}