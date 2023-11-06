using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Common;
using Cit.Apps.Licensing.Persistence.Contexts;
using Cit.Apps.Licensing.Shared.Password;
using Cit.Apps.Shared.Result;
using Microsoft.EntityFrameworkCore;


namespace Cit.Apps.Licensing.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DatabaseContext _databaseContext;
        public GenericRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IQueryable<T> Entities => _databaseContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _databaseContext.Set<T>().AddAsync(entity);
          return entity;  
        }

        public Task UpdateAsync(T entity)
        {
                _databaseContext.Entry(entity).State = EntityState.Modified;
                return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _databaseContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _databaseContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            var item=await _databaseContext.Set<T>().FindAsync(Id);
            return item;

        }

        public ResultModel<bool> ValidateUser(UserCredentialModel userCredentialModel)
        {
            return null;
        }
    }
}
