using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Enum;

namespace WBrand.Core.Domain.Entities.SlideShow
{
    public class SlideShow : BaseEntity<int>
    {
        [MaxLength(1024)]
        public string Name { set; get; }

        [MaxLength(1024)]
        public string Image { set; get; }

        [MaxLength(2048)]
        public string URL { set; get; }

        public bool IsPublish { set; get; }

        public SlideShowPosition Position { set; get; }
    }
}
