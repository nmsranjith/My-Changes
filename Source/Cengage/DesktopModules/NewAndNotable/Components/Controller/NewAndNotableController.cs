/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   New and Notable products
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */
using System.Data.SqlClient;
using DotNetNuke.Modules.NewAndNotable.Components.Model;
using DotNetNuke.Modules.NewAndNotable.Components.NewInterfaces;
using DotNetNuke.Modules.NewAndNotable.Data;

namespace DotNetNuke.Modules.NewAndNotable.Components.Controller
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="NewAndNotableController" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    To Controll the method callings
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    public class NewAndNotableController : INewAndNotable
    {
        public static readonly NewAndNotableController newCtrl = new NewAndNotableController();
        public static NewAndNotableController Instance
        {
            get { return newCtrl; }
            set { }
        }


        public SqlDataReader GetNewProducts(NewModel newModel)
        {
            return DataProvider.Instance().GetNewProducts(newModel);
        }
        public bool IsISBNValid(NewModel newModel)
        {
            return DataProvider.Instance().IsISBNValid(newModel);
        }
    }
}