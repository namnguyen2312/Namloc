using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities
{
    public class SeoEntity<T> : AudiEntity<T>
    {
        [MaxLength(256)]
        public string MetaTitle { set; get; }
        [MaxLength(256)]
        public string MetaKeyword { set; get; }
        [MaxLength(256)]
        public string MetaDescription { set; get; }
        [MaxLength(2048)]
        public string Alias { set; get; }
    }
}
