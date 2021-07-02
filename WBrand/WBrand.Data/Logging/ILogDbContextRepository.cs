using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.System;

namespace WBrand.Data.Logging
{
    public interface ILogDbContextRepository : IEfRepository<LogDbContext>
    {
    }
}
