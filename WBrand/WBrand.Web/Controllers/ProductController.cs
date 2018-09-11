using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WBrand.Services.Catalog;

namespace WBrand.Web.Controllers
{
    public class ProductController : BaseController
    {
        IProductService _productService;
        ICatalogCategoryService _catalogCategoryService;
        public ProductController(IProductService productService,
            ICatalogCategoryService catalogCategoryService)
        {
            _productService = productService;
            _catalogCategoryService = catalogCategoryService;
        }
        public ActionResult Index(string filter = "", int p = 1, int cat = 0)
        {
            var model = _productService.GetAll(p, 9, filter, cat);
            return View(model);
        }

        public ActionResult Detail(string slug)
        {
            return View();
        }

        public PartialViewResult Category()
        {
            var model = _catalogCategoryService.GetAll(true);
            return PartialView();
        }
    }
}