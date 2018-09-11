using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Blog;

namespace WBrand.Data.Blog
{
    public interface IBlogPostRepository : IEfRepository<BlogPost>
    {
    }
}
