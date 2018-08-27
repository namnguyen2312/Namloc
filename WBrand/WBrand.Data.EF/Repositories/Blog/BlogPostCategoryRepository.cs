using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Blog;
using WBrand.Data.Blog;

namespace WBrand.Data.EF.Repositories.Blog
{
    public class BlogPostCategoryRepository : BaseRepository<BlogPostCategory>, IBlogPostCategoryRepository
    {
        public BlogPostCategoryRepository(WBrandDbContext dbContext) : base(dbContext)
        {
        }
    }
}
