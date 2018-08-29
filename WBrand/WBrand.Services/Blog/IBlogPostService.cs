using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common.Page;
using WBrand.Core.Domain.Models.Blog;

namespace WBrand.Services.Blog
{
    public interface IBlogPostService
    {
        BlogPostModel GetById(long id);
        PaginationSet<BlogPostModel> GetAll(int pageIndex, int pageSize, string filter = "", int categoryId = 0);
        BlogPostModel Insert(BlogPostModel model);
        BlogPostModel Update(BlogPostModel model);
        void DeleteById(long id);
    }
}
