using System.Data;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Components.Model;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Data;
using System.Data.SqlClient;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Components.FeatureInterfaces;

namespace DotNetNuke.Modules.FavouriteAndRecentlyViewed.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="FavouriteController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    FavouriteController functionality
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public sealed class FavouriteController : IFeatureInterface
    {
        public static readonly FavouriteController favCtrl = new FavouriteController();

        private FavouriteController() { }

        public static FavouriteController Instance
        {
            get { return favCtrl; }
            set { }
        }
        

        public SqlDataReader GetFavouriteItems(FavouriteModel favourite)
        {
            return DataProvider.Instance().GetFavouriteItems(favourite);
        }

        public bool UpdateFavouriteSaveChanges(int ModuleId, string Save, int UserId)
        {
            return DataProvider.Instance().UpdateFavouriteSaveChanges(ModuleId, Save, UserId);
        }

        public IDataReader GetModuleFavouriteSaveChanges(int ModuleId)
        {
            return DataProvider.Instance().GetModuleFavouriteSaveChanges(ModuleId);
        }

        public bool InsertFavouriteSaveChanges(int ModuleId, string Save, int UserId)
        {
            return DataProvider.Instance().InsertFavouriteSaveChanges(ModuleId, Save, UserId);
        }

    }
}