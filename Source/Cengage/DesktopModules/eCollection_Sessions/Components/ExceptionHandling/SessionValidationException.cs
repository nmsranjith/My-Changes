using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.eCollection_Sessions.Components.Common;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.ExceptionHandling
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="SessionValidationException" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Class to handle the exception
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public class SessionValidationException:Exception
    {
        private string _message;
        private MyEnums.CrudState _errorState;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SessionValidationException(string message)
            : base(message)
        {
            this._message = message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorState"></param>
        public SessionValidationException(string message, MyEnums.CrudState errorState)
            : base(message)
        {
            this._message = message;
            this._errorState = errorState;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getErrorMessage()
        {
            return this._message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MyEnums.CrudState getErrorState()
        {
            return _errorState;
        }
    }
}