using Cit.Apps.Licensing.Domain.Entities;

namespace Cit.Apps.Licensing.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<ClientSubscription>> GetSubscriptionData();
        Task<ClientSubscription> GetSingleSubscription(int id);
        Task<List<ClientSubscription>> GetSubscriptionDataByClient(int id);
        IEnumerable<T> GetByExpression(string referenceColumnName, int id);


    }
}
