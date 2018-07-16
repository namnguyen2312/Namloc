using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common.AutoMapper;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Core.Domain.Models.Catalog;
using WBrand.Data;
using WBrand.Data.Catalog;
using WBrand.Services.Catalog;

namespace WBrand.Services.Facade.Catalog
{
    public class CatalogAttributeService : BaseServices<CatalogAttribute>, ICatalogAttributeService
    {
        ICatalogAttributeRepository _catalogAttributeRepo;
        public CatalogAttributeService(ICatalogAttributeRepository catalogAttributeRepo) : base(catalogAttributeRepo)
        {
            _catalogAttributeRepo = catalogAttributeRepo;
        }

        public async Task<IEnumerable<CatalogAttributeModel>> GetAll()
        {
            return await _catalogAttributeRepo.TableNoTracking.Where(x => !x.IsDel).QueryTo<CatalogAttributeModel>().ToListAsync();
        }

        public async Task<CreateCatalogAttributeModel> InsertAsync(CreateCatalogAttributeModel model)
        {
            try
            {
                var entity = model.MapTo<CatalogAttribute>();
                entity.Name.Trim();
                await _catalogAttributeRepo.InsertAsync(entity);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CatalogAttributeModel> UpdateAsync(CatalogAttributeModel model)
        {
            try
            {
                var entity = model.MapTo<CatalogAttribute>();
                entity.Name.Trim();
                await _catalogAttributeRepo.UpdateAsync(entity);
                return model;
            }
            catch
            {
                throw;
            }
        }
    }
}
