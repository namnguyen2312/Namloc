using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities.Blog
{
    public class BlogPost : SeoEntity<long>
    {
        [MaxLength(512)]
        public string Name { set; get; }

        [MaxLength(1024)]
        public string Image { set; get; }

        [MaxLength(128)]
        public string ShortContent { set; get; }

        public string FullContent { set; get; }

        public bool IsPublish { set; get; } = true;

        public DateTimeOffset PublishDate { set; get; }

        public int CategoryId { set; get; }

        public bool IsDel { set; get; } = false;

    }
}
