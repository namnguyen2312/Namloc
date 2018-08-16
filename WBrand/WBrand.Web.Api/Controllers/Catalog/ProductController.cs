using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WBrand.Common.Page;
using WBrand.Core.Domain.Models.Catalog;
using WBrand.Services.Catalog;

namespace WBrand.Web.Api.Controllers.Catalog
{
    [RoutePrefix("api/product")]
    public class ProductController : BaseApiController
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAll")]
        public PaginationSet<ProductModel> GetAll(int page, int pageSize, string filter = "", int categoryId = 0)
        {
            return _productService.GetAll(page + 1, pageSize, filter);
        }

        public ProductModel Get(long id)
        {
            return _productService.GetById(id);
        }

        // POST api/<controller>
        public CreateProductModel Add([FromBody]CreateProductModel value)
        {
            return _productService.Insert(value);
        }

        // PUT api/<controller>/5
        public UpdateProductModel Put([FromBody]UpdateProductModel value)
        {
            return _productService.Update(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(long id)
        {
            _productService.DeleteById(id);
        }
    }
}
