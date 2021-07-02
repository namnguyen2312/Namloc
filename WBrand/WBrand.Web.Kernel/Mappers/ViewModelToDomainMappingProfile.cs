using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WBrand.Core.Domain.Entities.Identity;
using WBrand.Web.Kernel.ViewModel.User;

namespace WBrand.Web.Kernel.Mappers
{
    public class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AppUserVm, AppUser>()
                .ForMember(src => src.Address, o => o.MapFrom(t => t.Address))
                .ForMember(src => src.BirthDay, o => o.MapFrom(t => t.BirthDay))
                .ForMember(src => src.CreatedBy, o => o.MapFrom(t => t.CreatedBy))
                .ForMember(src => src.CreatedDate, o => o.MapFrom(t => t.CreatedDate))
                .ForMember(src => src.FullName, o => o.MapFrom(t => t.FullName))
                .ForMember(src => src.UserName, o => o.MapFrom(t => t.UserName))
                .ForMember(src => src.Email, o => o.MapFrom(t => t.Email))
                .ForMember(src => src.Gender, o => o.MapFrom(t => t.Gender))
                .ForMember(src => src.UpdatedBy, o => o.MapFrom(t => t.UpdatedBy))
                .ForMember(src => src.UpdatedDate, o => o.MapFrom(t => t.UpdatedDate))
                .ForMember(src => src.State, o => o.MapFrom(t => t.State))
                .ForMember(src => src.PhoneNumber, o => o.MapFrom(t => t.PhoneNumber))
                ;
        }
    }
}