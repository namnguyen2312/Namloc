using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public ActionResult Index(string filter = "", int p = 1, string cat = "")
        {
            var model = _productService.GetAll(p, 9, filter, category: cat);
            return View(model);
        }

        //[Route(Name = "{slug}.html")]
        public ActionResult Detail(string slug)
        {
            var model = _productService.GetByAlias(slug);
            model.ProductRandoms = _productService.GetRandom(8);
            model.Imgs = JsonConvert.DeserializeObject<IEnumerable<string>>(model.Images);
            model.ImgTechs = JsonConvert.DeserializeObject<IEnumerable<string>>(model.ImagesTechnical);
            return View(model);
        }

        public PartialViewResult Category()
        {
            var model = _catalogCategoryService.GetAllNoAsync(true);
            return PartialView("Category", model);
        }
    }
}