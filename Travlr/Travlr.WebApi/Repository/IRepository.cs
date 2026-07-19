using Travlr.WebApi.Dtos;
using Travlr.WebApi.Models;

namespace Travlr.WebApi.Repository
{
    /// <summary>
    /// Repository Interface defines CRUD methods contracts
    /// Using generics for reusability for any type of model
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<IEnumerable<TEntity>> GetAsync();
        // id is BsonId string
        Task<TEntity> GetAsync(string id);
        Task CreateAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(string id, TEntity entity);
        Task RemoveAsync(string id);
    }
}
