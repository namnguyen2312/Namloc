using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Enum;

namespace WBrand.Core.Domain.Entities.System
{
    public class Banner : BaseEntity<int>
    {
        [MaxLength(1024)]
        public string Image { set; get; }

        [MaxLength(2048)]
        public string URL { set; get; }

        public int Order { set; get; }

        public BannerPosition Position { set; get; } = BannerPosition.Home;
    }
}
