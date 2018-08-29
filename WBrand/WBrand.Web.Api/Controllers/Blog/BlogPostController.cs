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
    [RoutePrefix("api/blogPost")]
    public class BlogPostController : BaseApiController
    {
        IBlogPostService _blogPostService;
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        [Route("GetAll")]
        public PaginationSet<BlogPostModel> GetAll(int page, int pageSize, string filter = "",int categoryId=0)
        {
            return _blogPostService.GetAll(page + 1, pageSize, filter, categoryId);
        }


        public BlogPostModel Get(int id)
        {
            return _blogPostService.GetById(id);
        }

        // POST api/<controller>
        public BlogPostModel Add([FromBody]BlogPostModel value)
        {
            return _blogPostService.Insert(value);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Put")]
        public BlogPostModel Put([FromBody]BlogPostModel value)
        {
            //var test = value;

            return _blogPostService.Update(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            _blogPostService.DeleteById(id);
        }
    }
}
