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
    public class BlogPostService : IBlogPostService
    {
        IBlogPostRepository _blogPostRepository;
        public BlogPostService(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public void DeleteById(long id)
        {
            try
            {
                var entity = _blogPostRepository.GetById(id);
                _blogPostRepository.Delete(entity);
            }
            catch
            {
                throw;
            }
        }

        public PaginationSet<BlogPostModel> GetAll(int pageIndex, int pageSize, string filter = "", int categoryId = 0)
        {
            var query = _blogPostRepository.TableNoTracking.Where(x => x.IsDel == false);
            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Name.Contains(filter));
            if (categoryId != 0)
                query = query.Where(x => x.CategoryId == categoryId);

            var result = query.OrderBy(x => x.Name).ToPagedList(pageIndex, pageSize);

            return new PaginationSet<BlogPostModel>
            {
                Items = Mapper.Map<IEnumerable<BlogPostModel>>(result.ToList()),
                Page = pageIndex -1,
                PageSize = pageSize,
                TotalCount = result.TotalItemCount,
                TotalPages = result.PageCount
            };
        }

        public BlogPostModel GetById(long id)
        {
            var entity = _blogPostRepository.GetById(id);
            var model = Mapper.Map<BlogPostModel>(entity);
            return model;
        }

        public IEnumerable<BlogPost> GetTop4()
        {
            return _blogPostRepository.TableNoTracking.Where(x => x.IsPublish == true && x.IsDel == false && x.PublishDate <= DateTimeOffset.UtcNow).Take(4).ToList();
        }

        public BlogPostModel Insert(BlogPostModel model)
        {
            try
            {
                var entity = model.MapTo<BlogPost>();
                entity.Name = entity.Name.Trim();
                entity.CreatedDate = CoreHelper.SystemTimeNow;
                entity.Alias = StringHelper.ToUrlFriendlyWithDate(entity.Name, entity.CreatedDate.Value.DateTime);
                _blogPostRepository.Insert(entity);
                return model;
            }
            catch
            {
                throw;
            }
        }

        public BlogPostModel Update(BlogPostModel model)
        {
            try
            {
                var entity = model.MapTo<BlogPost>();
                entity.Name = entity.Name.Trim();
                entity.UpdatedDate = CoreHelper.SystemTimeNow;
                entity.Alias = StringHelper.ToUrlFriendlyWithDate(entity.Name, entity.CreatedDate.Value.DateTime);
                _blogPostRepository.Update(entity);
                return model;
            }
            catch
            {
                throw;
            }
        }
    }
}
