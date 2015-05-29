/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   FavouriteAndRecentlyViewed
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Components.Model;

namespace DotNetNuke.Modules.FavouriteAndRecentlyViewed.Components.FeatureInterfaces
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="IFeatureInterface" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //   Method Declarations
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public interface IFeatureInterface
    {
        SqlDataReader GetFavouriteItems(FavouriteModel favourite);
        bool UpdateFavouriteSaveChanges(int ModuleId, string Save, int UserId);
        IDataReader GetModuleFavouriteSaveChanges(int ModuleId);
        bool InsertFavouriteSaveChanges(int ModuleId, string Save, int UserId);
    }
}
