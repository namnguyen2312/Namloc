using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities.Catalog
{
    public class CatalogCategory: SeoEntity<int>
    {
        [MaxLength(256)]
        public string Name { set; get; }
        [MaxLength(2048)]
        public string Description { set; get; }
        [MaxLength(2048)]
        public string Image { set; get; }
        public bool IsPublish { set; get; }
        public bool IsDel { set; get; }
        public int? ParentId { set; get; }
        public virtual CatalogCategory Parent { set; get; }
    }
}
