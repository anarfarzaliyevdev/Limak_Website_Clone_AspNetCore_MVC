using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Abstractions
{
    public interface IGeneralRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<bool> Edit(T entity);
        Task<bool> Delete(int id);
    }
}
