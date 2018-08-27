using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common.AutoMapper;
using WBrand.Common.Helper;
using WBrand.Common.Page;
using WBrand.Core.Domain.Entities.Blog;
using WBrand.Core.Domain.Models.Blog;
using WBrand.Data.Blog;
using WBrand.Services.Blog;

namespace WBrand.Services.Facade.Blog
{
    public class BlogPostCategoryService : IBlogPostCategoryService
    {
        IBlogPostCategoryRepository _blogPostCategoryRepository;
        public BlogPostCategoryService(IBlogPostCategoryRepository blogPostCategoryRepository)
        {
            _blogPostCategoryRepository = blogPostCategoryRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                var entity = _blogPostCategoryRepository.GetById(id);
                entity.IsDel = true;
                _blogPostCategoryRepository.Update(entity);
            }
            catch
            {
                throw;
            }
        }

        public PaginationSet<BlogPostCategoryModel> GetAll(int pageIndex, int pageSize, string filter = "")
        {
            var query = _blogPostCategoryRepository.TableNoTracking.Where(x => x.IsDel == false);

            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Name.Contains(filter));

            var result = query.OrderBy(x => x.Name).ToPagedList(pageIndex, pageSize);
            return new PaginationSet<BlogPostCategoryModel>
            {
                Items = Mapper.Map<IEnumerable<BlogPostCategoryModel>>(result.ToList()),
                Page = pageIndex,
                PageSize = pageSize,
                TotalCount = result.TotalItemCount,
                TotalPages = result.PageCount
            };
        }

        public BlogPostCategoryModel GetById(int id)
        {
            return _blogPostCategoryRepository.GetById(id).MapTo<BlogPostCategoryModel>();
        }

        public BlogPostCategoryModel Insert(BlogPostCategoryModel model)
        {
            try
            {
                var entity = model.MapTo<BlogPostCategory>();
                entity.Name = entity.Name.Trim();
                entity.Alias = StringHelper.ToUrlFriendly(entity.Name);
                _blogPostCategoryRepository.Insert(entity);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public BlogPostCategoryModel Update(BlogPostCategoryModel model)
        {
            try
            {
                var entity = model.MapTo<BlogPostCategory>();
                entity.Name = entity.Name.Trim();
                entity.Alias = StringHelper.ToUrlFriendly(entity.Name);
                _blogPostCategoryRepository.Update(entity);
                return model;
            }
            catch
            {
                throw;
            }
        }
    }
}
