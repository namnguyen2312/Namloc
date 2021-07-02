using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Data.Catalog;

namespace WBrand.Data.EF.Repositories.Catalog
{
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(WBrandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
