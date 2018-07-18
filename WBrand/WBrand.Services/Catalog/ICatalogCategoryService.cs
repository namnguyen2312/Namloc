using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common.Page;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Core.Domain.Models.Catalog;

namespace WBrand.Services.Catalog
{
    public interface ICatalogCategoryService: IBaseServices<CatalogCategory>
    {
        Task<CatalogCategoryModel> GetByIdAsync(int id);
        Task<IEnumerable<CatalogCategoryModel>> GetAll();
        Task<IEnumerable<CatalogCategoryModel>> GetAll(bool? isPublish);
        Task<CreateCatalogCategoryModel> InsertAsync(CreateCatalogCategoryModel model);
        Task<CatalogCategoryModel> UpdateAsync(CatalogCategoryModel model);
    }
}
