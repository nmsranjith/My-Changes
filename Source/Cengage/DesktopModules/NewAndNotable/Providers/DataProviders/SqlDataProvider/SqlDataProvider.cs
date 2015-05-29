/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   New and Notable products
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */

using System;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using DotNetNuke.Instrumentation;
using DotNetNuke.Modules.NewAndNotable.Components.Model;
using Microsoft.ApplicationBlocks.Data;

namespace DotNetNuke.Modules.NewAndNotable.Data
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SqlDataProvider" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //   SQL Server implementation of the abstract DataProvider class
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
   
    public class SqlDataProvider : DataProvider
    {

        #region Private Members

        private const string ProviderType = "data";
        private const string ModuleQualifier = "NewAndNotable_";

        private readonly ProviderConfiguration _providerConfiguration = ProviderConfiguration.GetProviderConfiguration(ProviderType);
        private readonly string _connectionString;
        private readonly string _providerPath;
        private readonly string _objectQualifier;
        private readonly string _databaseOwner;

        #endregion

        #region Constructors

        public SqlDataProvider()
        {

            // Read the configuration specific information for this provider
            Provider objProvider = (Provider)(_providerConfiguration.Providers[_providerConfiguration.DefaultProvider]);

            // Read the attributes for this provider

            //Get Connection string from web.config
            _connectionString = Config.GetConnectionString();

            if (string.IsNullOrEmpty(_connectionString))
            {
                // Use connection string specified in provider
                _connectionString = objProvider.Attributes["connectionString"];
            }

            _providerPath = objProvider.Attributes["providerPath"];

            _objectQualifier = objProvider.Attributes["objectQualifier"];
            if (!string.IsNullOrEmpty(_objectQualifier) && _objectQualifier.EndsWith("_", StringComparison.Ordinal) == false)
            {
                _objectQualifier += "_";
            }

            _databaseOwner = objProvider.Attributes["databaseOwner"];
            if (!string.IsNullOrEmpty(_databaseOwner) && _databaseOwner.EndsWith(".", StringComparison.Ordinal) == false)
            {
                _databaseOwner += ".";
            }

        }

        #endregion

        #region Properties

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public string ProviderPath
        {
            get
            {
                return _providerPath;
            }
        }

        public string ObjectQualifier
        {
            get
            {
                return _objectQualifier;
            }
        }

        public string DatabaseOwner
        {
            get
            {
                return _databaseOwner;
            }
        }

        private string NamePrefix
        {
            get { return DatabaseOwner + ObjectQualifier + ModuleQualifier; }
        }

        #endregion

        #region Private Methods

        private static object GetNull(object Field)
        {
            return DotNetNuke.Common.Utilities.Null.GetNull(Field, DBNull.Value);
        }

        #endregion

        #region Public Methods

        //public override IDataReader GetItem(int itemId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItem", itemId);
        //}

        //public override IDataReader GetItems(int userId, int portalId)
        //{
        //    return SqlHelper.ExecuteReader(ConnectionString, NamePrefix + "spGetItemsForUser", userId, portalId);
        //}


        #endregion


        public override SqlDataReader GetNewProducts(NewModel newModel)
        {
            SqlConnection conn = null;
            SqlCommand comm = null;
            SqlParameter attrtvp = null;
            try
            {
                attrtvp = new SqlParameter();
                attrtvp.ParameterName = "@ISBNTVP";
                attrtvp.Value = newModel.IsbnTable;
                attrtvp.SqlDbType = SqlDbType.Structured;
                attrtvp.TypeName = "ISBNTVP";

                conn = new SqlConnection(ConnectionString);
                conn.Open();

                comm = new SqlCommand((!newModel.IsSeeAll) ? "ECOMM_CMS_NEW_NOTABLE_PRODUCTS" : "ECOMM_ALL_CMS_NEW_NOTABLE_PRODUCTS", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@USER_SK", newModel.UserSK));
                comm.Parameters.Add(new SqlParameter("@COUNT", newModel.DisplayCount));
                comm.Parameters.Add(new SqlParameter("@DIVISION", newModel.Division));
                comm.Parameters.Add(new SqlParameter("@COUNTRY", newModel.Country));
                comm.Parameters.Add(attrtvp);
                return comm.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                DnnLog.Error("Sql Exception in NewProduct Module is :" + ex.InnerException);
                DnnLog.Error("Sql Exception in NewProduct Module is :" + ex.Message);
                return null;
            }
        }

        public override bool IsISBNValid(NewModel newModule)
        {
            try
            {
                int result = (int)SqlHelper.ExecuteScalar(ConnectionString, "ECOMM_HE_NEW_NOTABLE_VALIDATE_ISBN"
                                                            , new SqlParameter("@ISBN", newModule.Isbn_13)
                                                            , new SqlParameter("@DIVISION", newModule.Division)
                                                            , new SqlParameter("@COUNTRY", newModule.Country));
                DnnLog.Fatal("Isbn_13 is : " + newModule.Isbn_13);
                DnnLog.Fatal("Division is : " + newModule.Division);
                DnnLog.Fatal("Country is : " + newModule.Country);
                DnnLog.Fatal("RESULT Count :" + result);
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                DnnLog.Error("Sql Exception in Validating Isbn is :" + ex.InnerException);
                DnnLog.Error("Sql Exception in Validating Isbn is :" + ex.Message);
                return false;
            }
        }
    }

}