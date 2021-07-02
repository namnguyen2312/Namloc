using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Models.Catalog
{
    public class CreateCatalogAttributeModel
    {
        [MaxLength(128)]
        [Required]
        public string Name { set; get; }
        [MaxLength(512)]
        public string Description { set; get; }
    }
}
