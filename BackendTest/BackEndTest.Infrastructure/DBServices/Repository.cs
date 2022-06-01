using BackEndTest.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Infrastructure.DBServices
{
    public class Repository<TEntity, TDbContext> : IRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        //private bool disposed = false;
        private readonly TDbContext _context;

        public Repository(TDbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAllEntity()
        {
            return _context.Set<TEntity>().ToList();
        }


        public async Task DeleteEntity(int? entityID)
        {
            TEntity foundEntity = await _context.Set<TEntity>().FindAsync(entityID);
            _context.Remove(foundEntity);
        }

        public async Task<TEntity> GetEntityByID(int? entityID)
        {
            return await _context.Set<TEntity>().FindAsync(entityID);
        }

        public void InsertEntity(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
