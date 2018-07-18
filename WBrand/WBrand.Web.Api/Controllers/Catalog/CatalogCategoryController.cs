using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WBrand.Core.Domain.Models.Catalog;
using WBrand.Services.Catalog;
using WBrand.Services.Facade.Catalog;

namespace WBrand.Web.Api.Controllers.Catalog
{
    [RoutePrefix("api/catalogCategory")]
    public class CatalogCategoryController : BaseApiController
    {
        ICatalogCategoryService _catalogCategoryService;
        public CatalogCategoryController(ICatalogCategoryService catalogCategoryService)
        {
            _catalogCategoryService = catalogCategoryService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<CatalogCategoryModel>> GetAll()
        {
            return await _catalogCategoryService.GetAll();
        }

        // GET api/<controller>/5
        public async Task<CatalogCategoryModel> Get(int id)
        {
            return await _catalogCategoryService.GetByIdAsync(id);
        }

        // POST api/<controller>
        public async Task<CreateCatalogCategoryModel> Add([FromBody]CreateCatalogCategoryModel value)
        {
            return await _catalogCategoryService.InsertAsync(value);
        }

        // PUT api/<controller>/5
        public async Task<CatalogCategoryModel> Put([FromBody]CatalogCategoryModel value)
        {
            return await _catalogCategoryService.UpdateAsync(value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
    }
}