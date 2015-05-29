using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetNuke.Modules.eCollection_Groups.Components.Interfaces
{
    public interface IDataRepository<T, K>
    {
        T Get(K key);

        int? Add(T modal);


        void Delete(K key);
    }
}
