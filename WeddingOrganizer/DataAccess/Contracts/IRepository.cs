using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}