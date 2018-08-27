using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Models.Catalog
{
    public class ProductModel : SeoModel
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public decimal Cost { set; get; }
        [MaxLength(256)]
        public string ShortContent { set; get; }
        public string FullContent { set; get; }
        [MaxLength(128)]
        public string SKU { set; get; }
        [MaxLength(128)]
        public string BarCode { set; get; }
        [MaxLength(512)]
        public string Image { set; get; }
        public string Images { set; get; }
        public string ImagesTechnical { set; get; }
        public bool IsDel { set; get; }
        public bool IsNew { set; get; }
        public bool IsPublish { set; get; }
        public List<int> CategoryIds { set; get; }
        public IEnumerable<ProductCategoryModel> ProductCategories { set; get; }
    }
}
