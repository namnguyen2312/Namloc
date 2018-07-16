using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Models.Catalog
{
    public class CreateCatalogCategoryModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { set; get; }
        [MaxLength(2048)]
        public string Description { set; get; }
        [MaxLength(2048)]
        public string Image { set; get; }
        public bool IsPublish { set; get; }
        [MaxLength(256)]
        public string MetaTitle { set; get; }
        [MaxLength(256)]
        public string MetaKeyword { set; get; }
        [MaxLength(256)]
        public string MetaDescription { set; get; }
        [MaxLength(2048)]
        public string Alias { set; get; }
        public DateTimeOffset? CreatedDate { set; get; }
        [MaxLength(64)]
        public string CreatedBy { set; get; }
        public int? ParentId { set; get; }
    }
}
