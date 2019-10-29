using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(long id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}