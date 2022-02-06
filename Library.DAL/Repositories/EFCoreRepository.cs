using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.DAL.Entities;

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

        // To check necersaryti AsQueryable

        public async Task<IEnumerable<TEntity>> GetAll() // To check necessaryty
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _entities.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task Create(TEntity entity)
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

        public async Task DeleteById(int id)
        {
            var entity = await _entities.FirstOrDefaultAsync(entity => entity.Id == id);

            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }
    }
}
