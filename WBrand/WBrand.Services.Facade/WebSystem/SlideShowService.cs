using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common.Helper;
using WBrand.Core.Domain.Entities.SlideShow;
using WBrand.Core.Domain.Enum;
using WBrand.Data;
using WBrand.Data.WebSystem;
using WBrand.Services.WebSystem;

namespace WBrand.Services.Facade.WebSystem
{
    public class SlideShowService : BaseServices<SlideShow>, ISlideShowService
    {
        ISlideShowRepository _slideShowRepository;
        public SlideShowService(ISlideShowRepository slideShowRepository) : base(slideShowRepository)
        {
            _slideShowRepository = slideShowRepository;
        }

        public void DeleteById(int id)
        {
            try
            {
                var entity = _slideShowRepository.GetById(id);
                _slideShowRepository.Delete(entity);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<SlideShow> GetAll(SlideShowPosition position = 0, bool? isPublish = null)
        {
            var query = _slideShowRepository.TableNoTracking;
            if (position != 0)
                query = query.Where(x => x.Position == position);
            if (isPublish != null)
                query = query.Where(x => x.IsPublish == isPublish.Value);

            return query.ToList();
        }

        public IEnumerable<dynamic> GetAllPosition()
        {
            var list = EnumHelper.ToDictionary(typeof(SlideShowPosition)).Select(x => new { Id = x.Key, Name = x.Value });
            return list;
        }
    }
}
