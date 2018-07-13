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
            Task.Run(() => this.LoggingDb(EntityState.Deleted, entity));
            return entity;
        }

        public int Delete(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                    DbSet.Remove(entity);
                var count = DbContext.SaveChanges();
                Task.Run(() => this.LoggingDb(EntityState.Deleted, entities: entities));
                return count;
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
                await this.LoggingDb(EntityState.Deleted, entity));
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
                var count = await DbContext.SaveChangesAsync();
                await this.LoggingDb(EntityState.Deleted, entities: entities));
                return count;
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
                Task.Run(() => this.LoggingDb(EntityState.Added, entity));
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
                var count = DbContext.SaveChanges();
                Task.Run(() => this.LoggingDb(EntityState.Added, entities: entities));
                return count;
                    
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
                await this.LoggingDb(EntityState.Added, entity);
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
                var count = await DbContext.SaveChangesAsync();
                await this.LoggingDb(EntityState.Added, entities: entities);
                return count;
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
            Task.Run(() => this.LoggingDb(EntityState.Modified, entity));
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
            await this.LoggingDb(EntityState.Modified, entity);
            return entity;
        }

        public T Update(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                DbContext.SaveChanges();
                Task.Run(() => this.LoggingDb(EntityState.Modified, entity));
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
                await this.LoggingDb(EntityState.Modified, entity);
                return entity;
            }
            catch
            {
                throw;
            }
        }
        public int Update(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    DbSet.Attach(entity);
                    DbContext.Entry(entity).State = EntityState.Modified;
                }
                var result = DbContext.SaveChanges();
                Task.Run(() => this.LoggingDb(EntityState.Modified, entities: entities));
                return result;
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
                await this.LoggingDb(EntityState.Modified, entities: entities);
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

        private async Task LoggingDb(EntityState entityState, T entity = null, IEnumerable<T> entities = null)
        {
            try
            {
                var dataJsonString = "";
                if (entity != null)
                    dataJsonString = JsonConvert.SerializeObject(entity).ToString();
                if (entities != null)
                    dataJsonString = JsonConvert.SerializeObject(entities).ToString();

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
