using System.Collections.Generic;

namespace DotNetNuke.Modules.eCollection_Students.Components.Interfaces
{
    public interface IDataRepository<T, K>
    {
        #region Abstract Methods

            T Get(K key);

            List<T> GetAll();

            int? Add(T modal);

            int Update(T modal);

            void Delete(K key);

        #endregion
    }

}
