using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.DAL.Entities;
using System.Linq;

namespace Library.DAL.Repositories
{
    public abstract class EfCoreRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        protected readonly TContext _context;
        private readonly DbSet<TEntity> _entities;

        public EfCoreRepository(TContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll() // To check necessaryty
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _entities.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<bool> DoesExistByIdAsync(long id) // For faster checks, then FirstOrDefault.
        {
            return await _entities.AnyAsync(entity => entity.Id == id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _entities.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Update(entity);
        }

        public async Task DeleteById(long id)
        {
            var entity = await _entities.FirstOrDefaultAsync(entity => entity.Id == id);

            _entities.Remove(entity);
        }

        public void UpdateManyToMany(IEnumerable<TEntity> currentItems, IEnumerable<TEntity> newItems)
        {
            _entities.RemoveRange(currentItems);
            _entities.AddRange(newItems);
        }
    }
}
