using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Models.Blog;
using WBrand.Data.Blog;
using WBrand.Services.Blog;

namespace WBrand.Services.Facade.Blog
{
    public class BlogPostService : IBlogPostService
    {
        IBlogPostRepository _blogPostRepository;
        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        public BlogPostModel GetById(long id)
        {
            var entity = _blogPostRepository.GetById(id);
            var model = Mapper.Map<BlogPostModel>(entity);
            return model;
        }
    }
}
