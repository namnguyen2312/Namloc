using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Data
{
    public interface IEfRepository<T> : IDisposable where T : class
    {
        Task<T> GetByIdAsync(object id);

        T GetById(object id);

        Task<T> InsertAsync(T entity);

        T Insert(T entity);

        int Insert(IEnumerable<T> entities);

        Task<int> InsertAsync(IEnumerable<T> entities);

        T UpdateProperties(T entity, params Expression<Func<T, object>>[] changedProperties);

        Task<T> UpdatePropertiesAsync(T entity, params Expression<Func<T, object>>[] changedProperties);

        Task<T> UpdateAsync(T entity);

        T Update(T entity);
        int Update(IEnumerable<T> entities);

        Task<int> UpdateAsync(IEnumerable<T> entities);

        Task DeleteAsync(T entity);

        T Delete(T entity);

        Task<int> DeleteAsync(IEnumerable<T> entities);

        int Delete(IEnumerable<T> entities);

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }

        void BeginTran(System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.Serializable);
        void CommitTran();
        void RollbackTran();
    }
}
