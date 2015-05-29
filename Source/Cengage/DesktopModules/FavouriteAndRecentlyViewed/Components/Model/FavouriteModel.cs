/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   FavouriteAndRecentlyViewed
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DotNetNuke.Modules.FavouriteAndRecentlyViewed.Components.Model
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="FavouriteModel" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    FavouriteModel functionality
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    [Serializable]
    public class FavouriteModel
    {
        public string Title { get; set; }
        public char IsFavourite { get; set; }
        public bool IsSeeAll { get; set; }
        public char IsRecent { get; set; }
        public int DisplayCount { get; set; }
        public string Division { get; set; }
        public int UserSK { get; set; }
        public string Content { get; set; }
        public DataTable RecentDataTable { get; set; }
        public string Country { get; set; }
    }
}