using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DotNetNuke.Modules.ProductFormat.Components.Modal;
using DotNetNuke.Modules.ProductFormat.Data;

namespace DotNetNuke.Modules.ProductFormat.Components.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="ProductFormatController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for to perform business activities
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------    
    public class ProductFormatController
    {
        public static readonly ProductFormatController studCtrl = new ProductFormatController();

        private ProductFormatController() { }

        public static ProductFormatController Instance
        {
            get { return studCtrl; }
            set { }
        }

        /// <summary>
        ///  Get all the product Formats
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public SqlDataReader GetAllProductFormats()
        {
            try
            {
                return DataProvider.Instance().GetAllProductFormats();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To insert/update product formats
        /// </summary>
        /// <param name="lstProductFormat"></param>
        /// <returns></returns>
        public int SaveProductFormats(List<ProductFormats> lstProductFormat)
        {
            try
            {
                return DataProvider.Instance().SaveProductFormats(lstProductFormat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}