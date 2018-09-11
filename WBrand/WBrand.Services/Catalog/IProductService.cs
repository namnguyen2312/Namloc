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
    public interface IProductService : IBaseServices<Product>
    {
        ProductModel GetById(long id);

        ProductModel GetByAlias(string alias);

        CreateProductModel Insert(CreateProductModel model);

        UpdateProductModel Update(UpdateProductModel model);

        PaginationSet<ProductModel> GetAll(int pageIndex, int pageSize, string filter = "", int categoryId = 0,string category="");

        IEnumerable<Product> GetTop6();

        IEnumerable<ProductModel> GetRandom(int amountItem);

        void DeleteById(long id);
    }
}
