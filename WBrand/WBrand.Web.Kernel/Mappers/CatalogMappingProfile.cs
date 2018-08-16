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
            CreateMap<CatalogCategory, CatalogCategoryModel>().MaxDepth(10).ReverseMap();
            CreateMap<CreateCatalogCategoryModel, CatalogCategory>();

            CreateMap<CatalogAttributeModel, CatalogAttribute>().ReverseMap();
            CreateMap<CreateCatalogAttributeModel, CatalogAttribute>();

            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<CreateProductModel, Product>();
        }
    }
}