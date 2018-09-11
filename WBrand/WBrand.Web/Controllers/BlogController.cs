using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WBrand.Services.Blog;

namespace WBrand.Web.Controllers
{
    public class BlogController : BaseController
    {
        IBlogPostService _blogPostService;
        IBlogPostCategoryService _blogPostCategoryService;
        public BlogController(IBlogPostService blogPostService,
        IBlogPostCategoryService blogPostCategoryService)
        {
            _blogPostService = blogPostService;
            _blogPostCategoryService = blogPostCategoryService;
        }

        public ActionResult Index(string filter = "", int p = 1, string cat = "")
        {
            var model = _blogPostService.GetAll(p, 9, filter, cat: cat, isPushlish: true);
            return View(model);
        }



        public PartialViewResult Category()
        {
            var model = _blogPostCategoryService.GetAll().Where(x => x.IsPublish == true);
            return PartialView(model);
        }
    }
}