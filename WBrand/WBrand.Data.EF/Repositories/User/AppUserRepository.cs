using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Data.User;

namespace WBrand.Data.EF.Repositories.User
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(WBrandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
