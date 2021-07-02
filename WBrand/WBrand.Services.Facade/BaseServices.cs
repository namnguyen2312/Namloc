using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common;
using WBrand.Data;

namespace WBrand.Services.Facade
{
    public class BaseServices<T> : DisposableObject, IBaseServices<T> where T : class
    {
        private IEfRepository<T> _repository { get; }

        public BaseServices(IEfRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> InsertAsync(T entity) => await this._repository.InsertAsync(entity);

        public virtual T Insert(T entity) => this._repository.Insert(entity);

        public virtual async Task DeleteAsync(T entity) => await this._repository.DeleteAsync(entity);

        public virtual async Task<T> GetByIdAsync(object id) => await this._repository.GetByIdAsync(id);

        public virtual T GetById(object id) => this._repository.GetById(id);

        public virtual async Task<T> UpdateAsync(T entity) => await this._repository.UpdateAsync(entity);

        public virtual T Update(T entity) => this._repository.Update(entity);

        protected override void DisposeCore()
        {
            _repository.Dispose();
        }
    }
}
