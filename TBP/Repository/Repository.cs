using Microsoft.Extensions.Options;
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

        public Repository(IOptions<MovieDatabaseOptions> settings)
        {
            var options = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

            var client = new MongoClient(options.ConnectionString);
            var database = client.GetDatabase(options.DatabaseName);
            _mongo = database.GetCollection<T>(nameof(T));
        }

        public Repository(MovieDatabaseOptions settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _mongo = database.GetCollection<T>(nameof(T));
        }

        public async Task<bool> Add(T instance)
        {
            try
            {
                await _mongo.InsertOneAsync(instance);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<int> Count()
        {
            try
            {
                return (int)await _mongo.CountDocumentsAsync(item => true);
            }
            catch(Exception e)
            {
                return 0;
            }
        }

        public async Task<bool> Delete(T instance)
        {
            try
            {
                var result = await _mongo.DeleteOneAsync(item => item.Id == instance.Id);

                if (result.IsAcknowledged)
                    return (result.DeletedCount > 0) ? true : false;
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                return await _mongo.Find(item => true).ToListAsync();
            }
            catch(Exception e)
            {
                return new List<T>();
            }
        }

        public async Task<T> GetById(Guid id)
        {
            try
            {
                return await _mongo.Find(item => item.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> Update(T instance)
        {
            try
            {
                var result = await _mongo.ReplaceOneAsync(item => item.Id == instance.Id, instance);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                    return true;
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
