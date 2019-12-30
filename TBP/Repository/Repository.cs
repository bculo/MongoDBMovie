using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Options;

namespace TBP.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly IMongoCollection<T> _mongo;
        protected readonly IMongoDatabase _mongoDatabase;

        [ActivatorUtilitiesConstructor]
        public Repository(IOptions<MovieDatabaseOptions> settings)
        {
            var options = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

            var client = new MongoClient(options.ConnectionString);
            _mongoDatabase = client.GetDatabase(options.DatabaseName);
            _mongo = _mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public Repository(MovieDatabaseOptions settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _mongoDatabase = client.GetDatabase(settings.DatabaseName);
            _mongo = _mongoDatabase.GetCollection<T>(typeof(T).Name);
        }

        public virtual async Task<bool> Add(T instance)
        {
            try
            {
                await _mongo.InsertOneAsync(instance);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<bool> AddRange(IEnumerable<T> instances)
        {
            try
            {
                await _mongo.InsertManyAsync(instances);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<int> Count()
        {
            try
            {
                return (int)await _mongo.CountDocumentsAsync(item => true);
            }
            catch(Exception)
            {
                return 0;
            }
        }

        public virtual async Task<bool> Delete(T instance)
        {
            try
            {
                var result = await _mongo.DeleteOneAsync(item => item.Id == instance.Id);

                if (result.IsAcknowledged)
                    return (result.DeletedCount > 0) ? true : false;
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public virtual async Task<List<T>> GetAll()
        {
            try
            {
                return await _mongo.Find(item => true).ToListAsync();
            }
            catch(Exception)
            {
                return new List<T>();
            }
        }

        public virtual async Task<T> GetById(ObjectId id)
        {
            try
            {
                return await _mongo.Find(item => item.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual async Task<List<T>> GetPaginatedResult(int page, int pagesize)
        {
            try
            {
                return await _mongo.Find(item => true)
                    .Skip((page - 1) * pagesize)
                    .Limit(pagesize)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public virtual async Task<bool> Update(T instance)
        {
            try
            {
                var result = await _mongo.ReplaceOneAsync(item => item.Id == instance.Id, instance);
           
                if (result.IsAcknowledged && result.ModifiedCount > 0)
                    return true;
                return false;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
