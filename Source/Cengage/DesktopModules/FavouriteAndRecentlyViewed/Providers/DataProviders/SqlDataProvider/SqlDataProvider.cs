/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   FavouriteAndRecentlyViewed
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */
using System;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Components.Model;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Instrumentation;

namespace DotNetNuke.Modules.FavouriteAndRecentlyViewed.Data
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SqlDataProvider" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    SQL Server implementation of the abstract DataProvider class
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    
    public class SqlDataProvider : DataProvider
    {

        #region Private Members

        private const string ProviderType = "data";
        private const string ModuleQualifier = "FavouriteAndRecentlyViewed_";

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



        public override SqlDataReader GetFavouriteItems(FavouriteModel favourite)
        {
            SqlConnection conn = null;
            SqlParameter attrtvp = null;
            SqlCommand comm = null;
            string ProcedureName = string.Empty;
            try
            {
                attrtvp = new SqlParameter();
                attrtvp.ParameterName = "@ISBNTVP";
                attrtvp.Value = favourite.RecentDataTable;
                attrtvp.SqlDbType = SqlDbType.Structured;
                attrtvp.TypeName = "RECENTLY_VIEWED_TVP";

                conn = new SqlConnection(ConnectionString);
                conn.Open();

                comm = new SqlCommand((!favourite.IsSeeAll) ? "ECOMM_CMS_FAVOURITE_RECENT_PRODUCTS" : "ECOMM_CMS_SEEALL_FAVOURITE_RECENT_PRODUCTS", conn);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@USER_SK", favourite.UserSK));
                comm.Parameters.Add(new SqlParameter("@COUNT", favourite.DisplayCount));
                comm.Parameters.Add(new SqlParameter("@IS_FAVOURITE", favourite.IsFavourite));
                comm.Parameters.Add(new SqlParameter("@IS_RECENT", favourite.IsRecent));
                comm.Parameters.Add(new SqlParameter("@DIVISION", favourite.Division));
                comm.Parameters.Add(new SqlParameter("@COUNTRY", favourite.Country));
                comm.Parameters.Add(attrtvp);
                DnnLog.Fatal("USER_SK : " + favourite.UserSK);
                DnnLog.Fatal("COUNT : " + favourite.DisplayCount);
                DnnLog.Fatal("IS_FAVOURITE : " + favourite.IsFavourite);
                DnnLog.Fatal("IS_RECENT : " + favourite.IsRecent);
                DnnLog.Fatal("DIVISION : " + favourite.Division);
                DnnLog.Fatal("COUNTRY : " + favourite.Country);
                DnnLog.Fatal("ISBN TVP :" + favourite.RecentDataTable.Rows.Count);
                return comm.ExecuteReader();
     
            }
            catch (Exception ex)
            {
                DnnLog.Error("Sql Exception in Favourite Module is :" + ex.InnerException);
                DnnLog.Error("Sql Exception in Favourite Module is :" + ex.Message);
                return null;
            }
            
          
        }

        #endregion


        public override bool UpdateFavouriteSaveChanges(int ModuleId, string Save, int UserId)
        {
            int result = 0;
            try
            {
               result = SqlHelper.ExecuteNonQuery(ConnectionString, "UPDATE_COMERCE_CMS_HTMLTABLE", new SqlParameter("@CONTENT", Save),
                                                    new SqlParameter("@MODULEID", ModuleId),
                                                    new SqlParameter("@USERSK", UserId));
               return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                DnnLog.Error("Sql Exception in Favourite Module is :" + ex.InnerException);
                DnnLog.Error("Sql Exception in Favourite Module is :" + ex.Message);
                return false;
            }
            
        }

        public override bool InsertFavouriteSaveChanges(int ModuleId, string Save, int UserId)
        {
            int result = 0;
            try
            {
                result = SqlHelper.ExecuteNonQuery(ConnectionString, "INSERT_COMERCE_CMS_HTMLTABLE", new SqlParameter("@CONTENT", Save),
                                                    new SqlParameter("@MODULEID", ModuleId),
                                                    new SqlParameter("@USERSK", UserId));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                DnnLog.Error("Sql Exception in Favourite Module is :" + ex.InnerException);
                DnnLog.Error("Sql Exception in Favourite Module is :" + ex.Message);
                return false;
            }
            
        }

        public override IDataReader GetModuleFavouriteSaveChanges(int ModuleId)
        {
            try
            {
                return SqlHelper.ExecuteReader(ConnectionString, "GET_COMERCE_CMS_HTMLTABLE"
                                                , new SqlParameter("@MODULEID", ModuleId));
            }
            catch (Exception ex)
            {
                DnnLog.Error("Sql Exception in Favourite Module is :" + ex.InnerException);
                DnnLog.Error("Sql Exception in Favourite Module is :" + ex.Message);
                return null;
            }
            
        }
    }

}