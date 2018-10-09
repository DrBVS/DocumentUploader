using System.Collections.Generic;

namespace DocumentUploader.DataAccess
{
    public interface IDataRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        IEnumerable<T> Insert(IEnumerable<T> item);
        T Update(T item);
        void Delete(string id);
    }
}
