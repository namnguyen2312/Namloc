using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WBrand.Common.Page;
using WBrand.Core.Domain.Models.Blog;
using WBrand.Services.Blog;

namespace WBrand.Web.Api.Controllers.Blog
{
    [RoutePrefix("api/blogPostCategory")]
    public class BlogPostCategoryController : BaseApiController
    {
        IBlogPostCategoryService _blogPostCategoryService;
        public BlogPostCategoryController(IBlogPostCategoryService blogPostCategoryService)
        {
            _blogPostCategoryService = blogPostCategoryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public PaginationSet<BlogPostCategoryModel> GetAll(int page, int pageSize, string filter = "")
        {
            return _blogPostCategoryService.GetAll(page + 1, pageSize, filter);
        }

        [HttpGet]
        [Route("GetAllNoPaging")]
        public IEnumerable<BlogPostCategoryModel> GetAllNoPaging()
        {
            return _blogPostCategoryService.GetAll();
        }

        public BlogPostCategoryModel Get(int id)
        {
            return _blogPostCategoryService.GetById(id);
        }

        // POST api/<controller>
        public BlogPostCategoryModel Add([FromBody]BlogPostCategoryModel value)
        {
            return _blogPostCategoryService.Insert(value);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Put")]
        public BlogPostCategoryModel Put([FromBody]BlogPostCategoryModel value)
        {
            //var test = value;

            return _blogPostCategoryService.Update(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            _blogPostCategoryService.DeleteById(id);
        }
    }
}
