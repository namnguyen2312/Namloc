using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities.Catalog
{
    public class ProductCategory: BaseEntity<long>
    {
        public int CategoryId { set; get; }
        public long ProductId { set; get; }
    }
}
