using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Core.Domain.Models.Catalog;

namespace WBrand.Web.Kernel.Mappers
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<CatalogCategory, CatalogCategoryModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name)).ReverseMap()
                ;
            CreateMap<CatalogCategoryModel, CatalogCategory>();
            CreateMap<CreateCatalogCategoryModel, CatalogCategory>();

            CreateMap<CatalogAttributeModel, CatalogAttribute>();
            CreateMap<CreateCatalogAttributeModel, CatalogAttribute>();
        }
    }
}