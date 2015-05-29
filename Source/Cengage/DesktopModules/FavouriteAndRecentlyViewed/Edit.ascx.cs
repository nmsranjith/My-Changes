/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   FavouriteAndRecentlyViewed
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */
using System;
using System.Data;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Controller;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Instrumentation;

namespace DotNetNuke.Modules.FavouriteAndRecentlyViewed
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Edit" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    The EditFavouriteAndRecentlyViewed class is used to manage content
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------

    public partial class Edit : FavouriteAndRecentlyViewedModuleBase
    {

        #region Event Handlers

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Page_Load runs when the control is loaded
        /// </summary>
        /// -----------------------------------------------------------------------------
        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    LoadModuleInfo();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                DnnLog.Error("Exception in Favourite Module is :" + exc.InnerException);
                DnnLog.Error("Exception in Favourite Module is :" + exc.Message);
            }
        }

        #endregion

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            string check = string.Empty;
            string Save = string.Empty;
            try
            {
                check = (favChkbx.Checked && recentChkbx.Checked)
                    ? "both"
                    : ((favChkbx.Checked)
                        ? "fav"
                        : ((recentChkbx.Checked)
                            ? "recent"
                            : string.Empty
                          )
                       );
                Save = (!string.IsNullOrEmpty(titleOfModule.Value.Trim()) ? titleOfModule.Value : "Favorites & Recently Viewed") + "Ñ" + check + "Ñ" + displayCount.Value;
                FavouriteController.Instance.UpdateFavouriteSaveChanges(this.ModuleId, Save, UserId);
                LoadModuleInfo();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
                DnnLog.Error("Exception in Favourite Module is :" + exc.InnerException);
                DnnLog.Error("Exception in Favourite Module is :" + exc.Message);
            }
            
        }
        
        private void LoadModuleInfo()
        {
            string content = string.Empty;
            string[] split = null;
            titleOfModule.Value = "Favorites & Recently Viewed";
            IDataReader reader = null;
            try
            {
                reader = FavouriteController.Instance.GetModuleFavouriteSaveChanges(this.ModuleId);
                if (reader.Read())
                {
                    content = reader["Content"].ToString();
                    if (content != "")
                    {
                        split = content.Split('Ñ');
                        titleOfModule.Value = split[0];
                        switch (split[1].ToLower())
                        {
                            case "both":
                                favChkbx.Checked = true;
                                recentChkbx.Checked = true;
                                break;
                            case "fav":
                                favChkbx.Checked = true;
                                recentChkbx.Checked = false;
                                break;
                            case "recent":
                                favChkbx.Checked = false;
                                recentChkbx.Checked = true;
                                break;
                            default:
                                favChkbx.Checked = false;
                                recentChkbx.Checked = false;
                                break;
                        }

                        displayCount.Value = split[2];
                    }

                }
                else
                {
                    FavouriteController.Instance.InsertFavouriteSaveChanges(this.ModuleId, "ÑÑÑ", UserId);
                }

            }
            catch (Exception exc)
            {
                DnnLog.Error("Exception in Favourite Module is :" + exc.InnerException);
                DnnLog.Error("Exception in Favourite Module is :" + exc.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

        }
    }

}