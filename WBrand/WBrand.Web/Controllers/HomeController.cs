using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WBrand.Services.Blog;
using WBrand.Services.Catalog;
using WBrand.Services.WebSystem;
using WBrand.Web.Kernel.ViewModel;

namespace WBrand.Web.Controllers
{
    public class HomeController : Controller
    {
        IBlogPostService _blogPostService;
        IBannerService _bannerService;
        ISlideShowService _slideShowService;
        IVideoService _videoService;
        IProductService _productService;

        public HomeController(IBlogPostService blogPostService,
        IBannerService bannerService,
        ISlideShowService slideShowService,
        IVideoService videoService,
        IProductService productService)
        {
            _blogPostService = blogPostService;
            _bannerService = bannerService;
            _slideShowService = slideShowService;
            _videoService = videoService;
            _productService = productService;
        }
        public ActionResult Index()
        {
            var homeVm = new HomeVm();
            homeVm.BlogPosts = _blogPostService.GetTop4();
            homeVm.SlideShows = _slideShowService.GetAll(Core.Domain.Enum.SlideShowPosition.HeaderHome, true);
            homeVm.Videos = _videoService.GetAll(true);
            homeVm.Products = _productService.GetTop6();
            return View(homeVm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}