using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Services
{
    public interface IBaseServices<T>: IDisposable where T : class
    {
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        Task<T> InsertAsync(T entity);
        T Insert(T entity);
        Task<T> UpdateAsync(T entity);
        T Update(T entity);
        Task DeleteAsync(T entity);
    }
}
