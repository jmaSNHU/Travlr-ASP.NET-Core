using MongoDB.Driver;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Models;

namespace Travlr.WebApi.Repository
{

    /// <summary>
    /// Generic Repository for database CRUD operations
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        /// <summary>
        /// Initalizes the collection for the provided type T
        /// </summary>
        /// <param name="database"></param>
        /// <param name="collection"></param>
        public GenericRepository(IMongoDatabase database, string collection)
        {
            _collection = database.GetCollection<TEntity>(collection);
        }

        /// <summary>
        /// Returns all objects of type T
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Returns the object of type T with matching Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Saves a new object of type T to the database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task CreateAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Updates an existing object of type T
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns>Returns the updated object of type T</returns>
        public async Task<TEntity?> UpdateAsync(string id, TEntity entity)
        {
            var result = await _collection.ReplaceOneAsync(x => x.Id == id, entity);
            if (result.MatchedCount != 0)
                return await GetAsync(entity.Id!);
            else
                return default;
        }

        /// <summary>
        /// Deletes an existing object of type T that matches Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string id) => 
            await _collection.DeleteOneAsync(x => x.Id == id);
    }
}
