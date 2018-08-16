using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Models.Catalog
{
    public class CreateProductModel
    {
        public ProductModel Product { set; get; }

        public List<ProductCategoryModel> ProductCategories { set; get; }

        public List<int> CategoryIds { set; get; }
    }
}
