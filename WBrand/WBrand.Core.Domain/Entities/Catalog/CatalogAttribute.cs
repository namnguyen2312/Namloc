using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities.Catalog
{
    public class CatalogAttribute:BaseEntity<int>
    {
        [MaxLength(128)]
        public string Name { set; get; }
        [MaxLength(512)]
        public string Description { set; get; }
        public bool IsDel { set; get; } = false;
    }
}
