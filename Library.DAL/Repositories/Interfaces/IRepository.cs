using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task Create(T item);

        void Update(T item);

        Task DeleteById(int id);
    }
}
