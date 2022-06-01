using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndTest.Infrastructure.Interfaces
{
    public interface IRepository<TEntity, TDbContext> where TEntity : class where TDbContext : DbContext
    {
        IEnumerable<TEntity> GetAllEntity();
        Task<TEntity> GetEntityByID(int? entityId);
        void InsertEntity(TEntity entity);
        Task DeleteEntity(int? entityId);
        void UpdateEntity(TEntity entity);
        Task Save();
    }
}
