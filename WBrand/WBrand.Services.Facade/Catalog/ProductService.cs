using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Data;
using WBrand.Services.Catalog;

namespace WBrand.Services.Facade.Catalog
{
    public class ProductService : BaseServices<Product>, IProductService
    {
        public ProductService(IEfRepository<Product> repository) : base(repository)
        {
        }
    }
}
