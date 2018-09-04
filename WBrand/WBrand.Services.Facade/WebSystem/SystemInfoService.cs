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
    public class SystemInfoService : BaseServices<SystemInfo>, ISystemInfoService
    {
        ISystemInfoRepository _systemInfoRepository;
        public SystemInfoService(ISystemInfoRepository systemInfoRepository) : base(systemInfoRepository)
        {
            _systemInfoRepository = systemInfoRepository;
        }

        public SystemInfo GetByFirst()
        {
            Init();
            return _systemInfoRepository.TableNoTracking.FirstOrDefault();
        }

        private void Init()
        {
            if (_systemInfoRepository.TableNoTracking.Count() < 1)
            {
                var newEntity = new SystemInfo
                {
                    CompanyName = "CTY TNHH Nam Lộc",
                    Address = "299/10 Lý Thường Kiệt, Phường 15, Quận 11, Hồ Chí Minh"
                };

                _systemInfoRepository.Insert(newEntity);
            }
        }
    }
}
