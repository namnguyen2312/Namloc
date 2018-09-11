using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.System;
using WBrand.Data.WebSystem;

namespace WBrand.Data.EF.Repositories.WebSystem
{
    public class BannerRepository : BaseRepository<Banner>, IBannerRepository
    {
        public BannerRepository(WBrandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
