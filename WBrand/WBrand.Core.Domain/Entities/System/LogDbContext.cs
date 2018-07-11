using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Entities.System
{
    public class LogDbContext
    {
        [Key]
        [MaxLength(128)]
        public string Id { set; get; }
        
        public EntityState Status { set; get; }
        public string DataJsonEntityChange { set; get; }
        public DateTimeOffset CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public string DataJson { get; set; }
    }
}
