using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WBrand.Core.Domain.Models.Catalog;
using WBrand.Services.Catalog;

namespace WBrand.Web.Api.Controllers.Catalog
{
    [RoutePrefix("api/catalogAttribute")]
    public class CatalogAttributeController : BaseApiController
    {
        ICatalogAttributeService _catalogAttributeService;
        public CatalogAttributeController(ICatalogAttributeService catalogAttributeService)
        {
            _catalogAttributeService = catalogAttributeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<CatalogAttributeModel>> GetAll()
        {
            return await _catalogAttributeService.GetAll();
        }


        public async Task<CatalogAttributeModel> Get(int id)
        {
            return await _catalogAttributeService.GetByIdAsync(id);
        }

        // POST api/<controller>
        public async Task<CreateCatalogAttributeModel> Post([FromBody]CreateCatalogAttributeModel value)
        {
            return await _catalogAttributeService.InsertAsync(value);
        }

        // PUT api/<controller>/5
        public async Task<CatalogAttributeModel> Put([FromBody]CatalogAttributeModel value)
        {
            return await _catalogAttributeService.UpdateAsync(value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}