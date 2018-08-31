using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities.System
{
    public class SystemInfo:BaseEntity<int>
    {
        [MaxLength(512)]
        public string CompanyName { set; get; }

        [MaxLength(512)]
        public string Address { set; get; }

        [MaxLength(512)]
        public string Phone { set; get; }

        [MaxLength(512)]
        public string TaxCode { set; get; }

        [MaxLength(512)]
        public string Logo { set; get; }

        [MaxLength(512)]
        public string SystemEmail { set; get; }

        [MaxLength(512)]
        public string MetaTitle { set; get; }

        [MaxLength(512)]
        public string MetaDescription { set; get; }

        [MaxLength(512)]
        public string MetaKeyword { set; get; }
    }
}
