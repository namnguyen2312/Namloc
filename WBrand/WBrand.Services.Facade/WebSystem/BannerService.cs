using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Entities.System;
using WBrand.Data;
using WBrand.Data.WebSystem;
using WBrand.Services.WebSystem;

namespace WBrand.Services.Facade.WebSystem
{
    public class BannerService : BaseServices<Banner>, IBannerService
    {
        IBannerRepository _bannerRepository;
        public BannerService(IBannerRepository bannerRepository) : base(bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                var entity = _bannerRepository.GetById(id);

                _bannerRepository.Delete(entity);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Banner> GetAll(bool? isPublish = null)
        {
            var query = _bannerRepository.TableNoTracking;
            if (isPublish != null)
                query = query.Where(x => x.IsPublish == isPublish.Value);
            return query.OrderBy(x => x.Order).ToList();
        }
    }
}
