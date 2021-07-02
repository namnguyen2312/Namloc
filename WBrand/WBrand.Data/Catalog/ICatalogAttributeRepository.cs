using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.Catalog;

namespace WBrand.Data.Catalog
{
    public interface ICatalogAttributeRepository: IEfRepository<CatalogAttribute>
    {
    }
}
