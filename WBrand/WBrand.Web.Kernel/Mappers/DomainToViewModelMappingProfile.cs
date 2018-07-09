using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Core.Domain.Models.User;
using WBrand.Web.Kernel.ViewModel.User;

namespace WBrand.Web.Kernel.Mappers
{
    public class DomainToViewModelMappingProfile: Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AppUser, AppUserVm>();
            CreateMap<AppGroup, AppGroupVm>();
            //CreateMap<OrderItemVm, OrderItem>();
            //CreateMap<OrderVm, Order>();
        }
    }
}