using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;

namespace TBP.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(ObjectId id);
        Task<bool> Add(T instance);
        Task<bool> AddRange(IEnumerable<T> instances);
        Task<List<T>> GetPaginatedResult(int page, int pagesize);
        Task<bool> Update(T instance);
        Task<bool> Delete(T instance);
        Task<int> Count();
    }
}
