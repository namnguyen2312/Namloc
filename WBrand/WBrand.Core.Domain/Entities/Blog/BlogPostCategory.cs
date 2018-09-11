using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities.Blog
{
    public class BlogPostCategory : SeoEntity<int>
    {
        [MaxLength(128)]
        public string Name { set; get; }
        public bool IsPublish { set; get; } = true;
        public bool IsDel { set; get; } = false;

    }
}
