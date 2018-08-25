using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WBrand.Services.Blog;

namespace WBrand.Web.Api.Controllers.Blog
{
    public class BlogPostController : BaseApiController
    {
        IBlogPostService _blogPostService;
        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }


    }
}
