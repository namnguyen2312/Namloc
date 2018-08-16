using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common;
using WBrand.Common.AutoMapper;
using WBrand.Common.Helper;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Core.Domain.Models.Catalog;
using WBrand.Data;
using WBrand.Data.Catalog;
using WBrand.Services.Catalog;


namespace WBrand.Services.Facade.Catalog
{
    public class CatalogCategoryService : BaseServices<CatalogCategory>, ICatalogCategoryService
    {
        ICatalogCategoryRepository _catalogCategoryRepo;
        public CatalogCategoryService(ICatalogCategoryRepository catalogCategoryRepo) : base(catalogCategoryRepo)
        {
            _catalogCategoryRepo = catalogCategoryRepo;
        }

        public async Task<CatalogCategoryModel> GetByIdAsync(int id)
        {
            var query = await _catalogCategoryRepo.GetByIdAsync(id);

            return Mapper.Map<CatalogCategoryModel>(query);
        }
        public async Task<IEnumerable<CatalogCategoryModel>> GetAll()
        {
            try
            {
                var query =  _catalogCategoryRepo.TableNoTracking.Where(x => !x.IsDel).Include(x=>x.Parent).OrderBy(o => o.ParentId).ToListAsync();
                var model = AutoMapper.Mapper.Map<IEnumerable<CatalogCategoryModel>>(query);
                foreach (var item in model)
                {
                    var parentCategory = item.Parent;

                    while (parentCategory != null)
                    {
                        item.Name = $"{parentCategory.Name} >> {item.Name}";
                        parentCategory = parentCategory.Parent;
                    }
                }
                return model;
            }
            catch
            {
                throw;
            }
            
        }

        public async Task<IEnumerable<CatalogCategoryModel>> GetAll(bool? isPublish)
        {
            var query = await _catalogCategoryRepo.TableNoTracking.Where(x => !x.IsDel).OrderBy(o => o.ParentId).ToListAsync();
            
            if (isPublish != null)
                query = query.Where(x => x.IsPublish == isPublish.Value).ToList();
            var queryModel = Mapper.Map<IEnumerable<CatalogCategoryModel>>(query);
            return queryModel;
        }

        public async Task<CreateCatalogCategoryModel> InsertAsync(CreateCatalogCategoryModel model)
        {
            try
            {
                var entity = model.MapTo<CatalogCategory>();

                entity.Alias = StringHelper.ToUrlFriendly(entity.Name);
                entity.CreatedDate = SystemUtils.SystemTimeNow;

                await _catalogCategoryRepo.InsertAsync(entity);
                return model;
            }
            catch
            {
                throw;
            }
            
        }

        public async Task<CatalogCategoryModel> UpdateAsync(CatalogCategoryModel model)
        {
            try
            {
                var entity = model.MapTo<CatalogCategory>();

                entity.Alias = StringHelper.ToUrlFriendly(entity.Name);
                entity.UpdatedDate = SystemUtils.SystemTimeNow;

                await _catalogCategoryRepo.UpdateAsync(entity);
                return model;
            }
            catch
            {
                throw;
            }
            
        }

        public void DeleteById(int id)
        {
            try
            {
                var entity = _catalogCategoryRepo.GetById(id);
                entity.IsDel = true;
                _catalogCategoryRepo.Update(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
