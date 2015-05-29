/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   New and Notable products
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */
using System;
using System.Data;
using DotNetNuke.Instrumentation;
using DotNetNuke.Modules.FavouriteAndRecentlyViewed.Controller;
using DotNetNuke.Services.Exceptions;

namespace DotNetNuke.Modules.NewAndNotable
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="Edit" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    The EditNewAndNotable class is used to manage content
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------

    
    public partial class Edit : NewAndNotableModuleBase
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
                DnnLog.Error("Exception in New Module is :" + exc.InnerException);
                DnnLog.Error("Exception in New Module is :" + exc.Message);
            }
        }

        #endregion


        public void btnNewSave_Click(object sender, System.EventArgs e)
        {
            string isbns = string.Empty, Save = string.Empty;
            try
            {
                isbns = txtIsbn1.Value.Trim() + "," + txtIsbn2.Value.Trim() + "," + txtIsbn3.Value.Trim() + "," + txtIsbn4.Value.Trim() + ","
                            + txtIsbn5.Value.Trim() + "," + txtIsbn6.Value.Trim();
                Save = (!string.IsNullOrEmpty(titleOfModule.Value.Trim()) ? titleOfModule.Value : "New and Notable") + "Ñ" + isbns + "Ñ" + displayCount.Value;
                FavouriteController.Instance.UpdateFavouriteSaveChanges(this.ModuleId, Save, UserId);
                LoadModuleInfo();
            }
            catch (Exception exc)
            {
                DnnLog.Error("Exception in New Module is :" + exc.InnerException);
                DnnLog.Error("Exception in New Module is :" + exc.Message);
            }
           
        }

        private void LoadModuleInfo()
        {
            IDataReader reader = null;
            string[] split = null, isbns = null;
            string content = string.Empty;
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
                        isbns = split[1].Split(',');
                        txtIsbn1.Value = isbns[0];
                        txtIsbn2.Value = isbns[1];
                        txtIsbn3.Value = isbns[2];
                        txtIsbn4.Value = isbns[3];
                        txtIsbn5.Value = isbns[4];
                        txtIsbn6.Value = isbns[5];

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
                DnnLog.Error("Exception in New Module is :" + exc.InnerException);
                DnnLog.Error("Exception in New Module is :" + exc.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            
        }

    }

}