using System;
using System.Collections.Generic;
using System.Data;
using DotNetNuke.Modules.LibraryResources.Components.Modal;
using DotNetNuke.Modules.LibraryResources.Data;
using System.Data.SqlClient;

namespace DotNetNuke.Modules.LibraryResources.Components.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="LibraryResoucesController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    A controller class for Library Resouces business events
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class LibraryResourcesController
    {
        public static readonly LibraryResourcesController libResourceCtrl = new LibraryResourcesController();

        private LibraryResourcesController() { }

        public static LibraryResourcesController Instance
        {
            get { return libResourceCtrl; }
            set { }
        }

        /// <summary>
        /// To get all the Library resources
        /// </summary>
        /// <returns></returns>
        public SqlDataReader GetLibraryResources()
        {
            try
            {
                return DataProvider.Instance().GetLibraryResources();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// To insert/update all the Library resources
        /// </summary>
        /// <returns></returns>
        public int SaveLibraryResources(List<LibraryResource> libResourceList)
        {
            try
            {
                return DataProvider.Instance().SaveLibraryResources(libResourceList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}