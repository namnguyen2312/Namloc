using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Core.Domain.Models.Catalog;

namespace WBrand.Services.Catalog
{
    public interface ICatalogAttributeService : IBaseServices<CatalogAttribute>
    {
        Task<IEnumerable<CatalogAttributeModel>> GetAll();
        Task<CreateCatalogAttributeModel> InsertAsync(CreateCatalogAttributeModel model);
        Task<CatalogAttributeModel> UpdateAsync(CatalogAttributeModel model);
    }
}
