using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.SlideShow;
using WBrand.Data.WebSystem;

namespace WBrand.Data.EF.Repositories.WebSystem
{
    public class SlideShowRepository : BaseRepository<SlideShow>, ISlideShowRepository
    {
        public SlideShowRepository(WBrandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
