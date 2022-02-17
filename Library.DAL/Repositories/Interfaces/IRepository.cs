using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetByIdAsync(long id);

        Task<bool> DoesExistByIdAsync(long id);

        Task CreateAsync(T item);

        void Update(T item);

        Task DeleteById(long id);

        void UpdateManyToMany(IEnumerable<T> currentItems, IEnumerable<T> newItems);
    }
}
