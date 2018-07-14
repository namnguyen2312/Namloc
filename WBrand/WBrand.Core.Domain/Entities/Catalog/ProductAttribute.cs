using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities.Catalog
{
    public class ProductAttribute : BaseEntity<long>
    {
        public long ProductId { set; get; }
        public int AttributeId { set; get; }
        [MaxLength(512)]
        public string Value { set; get; }
    }
}
