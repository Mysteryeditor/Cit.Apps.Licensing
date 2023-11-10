using Cit.Apps.Licensing.Application.Interfaces.Repositories;
using Cit.Apps.Licensing.Application.ReadModels;
using Cit.Apps.Licensing.Domain.Common;
using Cit.Apps.Licensing.Domain.Entities;
using Cit.Apps.Licensing.Persistence.Contexts;
using Cit.Apps.Licensing.Shared.Password;
using Cit.Apps.Shared.Result;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            var item = await _databaseContext.Set<T>().FindAsync(Id);
            return item;

        }

        public ResultModel<bool> ValidateUser(UserCredentialModel userCredentialModel)
        {
            return null;
        }

        public async Task<List<ClientSubscription>> GetSubscriptionData()
        {
            var data = await _databaseContext
                .Set<ClientSubscription>().Include(c => c.Client).Include(p => p.SubscriptionPlan)
                .ToListAsync();
            return data;

        }

        public IEnumerable<T> GetByExpression(string referenceColumnName, int id)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, referenceColumnName);
            var value = Expression.Constant(id);
            var equality = Expression.Equal(property, value);
            var entity = _databaseContext.Set<T>().Where(Expression.Lambda<Func<T, bool>>(equality, parameter));
            return entity;
        }

        public async Task<List<ClientSubscription>> GetSubscriptionDataByClient(int id)
        {
            var data = await _databaseContext
                  .Set<ClientSubscription>().Where(c=>c.ClientId==id).Include(c => c.Client).Include(p => p.SubscriptionPlan)
                  .ToListAsync();
            return data;
        }

        public async Task<ClientSubscription> GetSingleSubscription(int id)
        {
            return await _databaseContext.Set<ClientSubscription>().Where(c => c.Id == id).Include(c => c.Client).Include(p => p.SubscriptionPlan).FirstOrDefaultAsync();
        }
    }
}
