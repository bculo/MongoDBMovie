using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;

namespace TBP.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<bool> Add(T instance);
        Task<bool> Update(T instance);
        Task<bool> Delete(T instance);
        Task<int> Count();
    }
}
