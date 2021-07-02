using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Models
{
    public class AudiModel
    {
        public DateTimeOffset? CreatedDate { set; get; }
        public DateTimeOffset? UpdatedDate { set; get; }
        [MaxLength(64)]
        public string CreatedBy { set; get; }
        [MaxLength(64)]
        public string UpdatedBy { set; get; }
    }
}
