/*
 
 *  Project Name        :   Cengage Higher Education
 *  Module Name         :   New and Notable products
 *  Developer Name      :   Chamala Dargababu
 *  Date Created        :   13-06-2014
 *  Date Modified       :   19-06-2014
  
 */
using System.Data.SqlClient;
using DotNetNuke.Modules.NewAndNotable.Components.Model;

namespace DotNetNuke.Modules.NewAndNotable.Components.NewInterfaces
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="INewAndNotable" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    Method informations
    // </summary>
    // ------------------------------------------------------------------------------------------------------------------------------
    interface INewAndNotable
    {
        SqlDataReader GetNewProducts(NewModel newModel);
        bool IsISBNValid(NewModel newModel);
    }
}
