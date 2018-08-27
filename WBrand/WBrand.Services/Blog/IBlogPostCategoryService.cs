using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common.Page;
using WBrand.Core.Domain.Models.Blog;

namespace WBrand.Services.Blog
{
    public interface IBlogPostCategoryService
    {
        BlogPostCategoryModel GetById(int id);

        PaginationSet<BlogPostCategoryModel> GetAll(int pageIndex, int pageSize, string filter = "");

        BlogPostCategoryModel Insert(BlogPostCategoryModel model);

        BlogPostCategoryModel Update(BlogPostCategoryModel model);

        void DeleteById(int id);
    }
}
