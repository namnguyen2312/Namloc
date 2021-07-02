using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.System;
using WBrand.Data.Logging;

namespace WBrand.Data.EF.Repositories.Log
{
    public class LogDbContextRepository : BaseRepository<LogDbContext>, ILogDbContextRepository
    {
        public LogDbContextRepository(WBrandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
