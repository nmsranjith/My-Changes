using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Modules.eCollection_Sessions.Components.Modal;

namespace DotNetNuke.Modules.eCollection_Sessions.Components.Interfaces
{   
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="IDataRepository" company="CENGAGE LEARNING AUSTRALIA PTY LIMITED">
    //    Copyright (c) 2014 CENGAGE LEARNING AUSTRALIA PTY LIMITED
    // </copyright>
    // <summary>
    //    IDataRepository interface
    // </summary>
    // ---------------------------------------------------------------------------------------------------------------------
    public interface IDataRepository<T,K>
    {
        T Get(K key);

        List<T> GetAll(K key, bool sortAsc);

        int? Add(T modal);

        void Update(T modal);

        void Delete(K key);
    }

}