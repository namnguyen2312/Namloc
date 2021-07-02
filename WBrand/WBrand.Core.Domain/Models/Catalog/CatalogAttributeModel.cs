using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Models.Catalog
{
    public class CatalogAttributeModel
        
    {
        public int Id { set; get; }
        [MaxLength(128)]
        [Required]
        public string Name { set; get; }
        [MaxLength(512)]
        public string Description { set; get; }
        public bool IsDel { set; get; }
    }
}
