using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities.System
{
    public class Video : BaseEntity<int>
    {
        [MaxLength(512)]
        public string Name { set; get; }
        [MaxLength(1024)]
        public string URL { set; get; }

        public DateTimeOffset CreatedDate { set; get; } = DateTimeOffset.UtcNow;
        public bool IsShow { set; get; } = true;

        public int Order { set; get; } = 0;
    }
}
