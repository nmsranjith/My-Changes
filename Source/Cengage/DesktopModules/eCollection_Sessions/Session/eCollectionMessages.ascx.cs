using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNetNuke.Modules.eCollection_Sessions.Session
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="eCollectionMessages" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    User control is used to display the Success/error/information/warning messages with respective styles
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public partial class eCollectionMessages : System.Web.UI.UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public void ClearMessages()
        {
            ShowError(string.Empty);
            ShowWarning(string.Empty);
            ShowSuccess(string.Empty);
            ShowInfo(string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ShowError(string message)
        {
            pl_error.Visible = !string.IsNullOrEmpty(message);
            lit_error.Text = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ShowWarning(string message)
        {
            pl_warning.Visible = !string.IsNullOrEmpty(message);
            lit_warning.Text = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ShowSuccess(string message)
        {
            pl_success.Visible = !string.IsNullOrEmpty(message);
            lit_success.Text = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ShowInfo(string message)
        {
            pl_info.Visible = !string.IsNullOrEmpty(message);
            lit_info.Text = message;
        }              
    }
}