using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Models.Blog
{
    public class BlogPostModel : SeoModel
        
    {
        public long Id { set; get; }

        [MaxLength(128)]
        public string Name { set; get; }

        [MaxLength(1024)]
        public string Image { set; get; }

        [MaxLength(128)]
        public string ShortContent { set; get; }

        [MaxLength(128)]
        public string FullContent { set; get; }

        public bool IsPublish { set; get; }

        public DateTimeOffset PublishDate { set; get; }

        public int CategoryId { set; get; }

        public bool IsDel { set; get; } = false;
    }
}
