using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WBrand.Services.WebSystem;

namespace WBrand.Web.Controllers
{
    public class CommonController : BaseController
    {
        IBannerService _bannerService;
        public CommonController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Partner()
        {
            var model = _bannerService.GetAll();
            return PartialView("_Partner", model);
        }
    }
}