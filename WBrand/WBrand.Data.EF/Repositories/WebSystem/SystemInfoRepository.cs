using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.System;
using WBrand.Data.WebSystem;

namespace WBrand.Data.EF.Repositories.WebSystem
{
    public class SystemInfoRepository : BaseRepository<SystemInfo>, ISystemInfoRepository
    {
        public SystemInfoRepository(WBrandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
