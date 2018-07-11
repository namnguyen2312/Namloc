using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common;
using WBrand.Core.Domain.Entities.System;

namespace WBrand.Data.EF
{
    public class BaseRepository<T> : DisposableObject, IEfRepository<T> where T : class
    {

        private DbContextTransaction _tranContext;
        protected WBrandDbContext DbContext { get; }
        protected DbSet<T> DbSet => this.DbContext.Set<T>();
        protected DbQuery<T> DbQueryNotracking => this.DbSet.AsNoTracking();

        public IQueryable<T> Table => this.DbSet;

        public IQueryable<T> TableNoTracking => this.DbQueryNotracking;
        public BaseRepository(WBrandDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public void BeginTran(IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            _tranContext = DbContext.Database.BeginTransaction(isolationLevel);
        }

        public void CommitTran()
        {
            _tranContext.Commit();
            _tranContext.Dispose();
        }

        public T Delete(T entity)
        {
            entity = DbSet.Remove(entity);
            DbContext.SaveChanges();
            return entity;
        }

        public int Delete(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                    DbSet.Remove(entity);

                return DbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                DbSet.Remove(entity);
                await DbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                    DbSet.Remove(entity);

                return await DbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public T Insert(T entity)
        {
            try
            {
                DbSet.Add(entity);
                DbContext.SaveChanges();
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public int Insert(IEnumerable<T> entities)
        {
            try
            {
                DbSet.AddRange(entities);
                return DbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                var newEntity = DbSet.Add(entity);
                await DbContext.SaveChangesAsync();
                return newEntity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(IEnumerable<T> entities)
        {
            try
            {
                DbSet.AddRange(entities);
                return await DbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public void RollbackTran()
        {
            _tranContext.Rollback();
            _tranContext.Dispose();
        }
        public T UpdateProperties(T entity, params Expression<Func<T, object>>[] changedProperties)
        {
            if (changedProperties?.Any() == true)
            {
                foreach (var property in changedProperties)
                {
                    DbContext.Entry(entity).Property(property).IsModified = true;
                }
            }
            else
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
            DbContext.SaveChanges();
            return entity;
        }

        public async Task<T> UpdatePropertiesAsync(T entity, params Expression<Func<T, object>>[] changedProperties)
        {
            if (changedProperties?.Any() == true)
            {
                foreach (var property in changedProperties)
                {
                    DbContext.Entry(entity).Property(property).IsModified = true;
                }
            }
            else
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public T Update(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                DbContext.SaveChanges();
                Task.Run(() => this.LoggingDb(entity, EntityState.Modified));
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                await this.LoggingDb(entity, EntityState.Modified);
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    DbSet.Attach(entity);
                    DbContext.Entry(entity).State = EntityState.Modified;
                }
                var result = await DbContext.SaveChangesAsync();
                return result;
            }
            catch
            {
                throw;
            }
        }

        protected override void DisposeCore()
        {
            this.DbContext.Dispose();
        }

        private async Task LoggingDb(T entity, EntityState entityState)
        {
            try
            {
                var dataJsonString = JsonConvert.SerializeObject(entity).ToString();
                //var dataJsonTracker = JsonConvert.SerializeObject(DbContext.ChangeTracker.Entries().ToList());
                var changeTracker = DbContext.ChangeTracker.Entries();
                var dataJsonTracker = new List<ValueTuple<string, EntityState>>();
                foreach (var item in changeTracker)
                {
                    var entityName = item.GetType().FullName;
                    var state = item.State;
                    dataJsonTracker.Add(new ValueTuple<string, EntityState>(entityName, state));
                }

                var newLogDb = new LogDbContext
                {
                    Id = SystemUtils.GenaratorSringId,
                    CreatedDate = SystemUtils.SystemTimeNow,
                    DataJson = dataJsonString,
                    Status = entityState,
                    DataJsonEntityChange = JsonConvert.SerializeObject(dataJsonTracker)
                };
                using (var db = new WBrandDbContext())
                {
                    db.LogDbContexts.Add(newLogDb);
                    await db.SaveChangesAsync();
                }
                   
            }
            catch (Exception ex)
            {

                
                throw;

            }
            
        }
    }
}
