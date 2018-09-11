using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WBrand.Core.Domain.Entities.Blog;
using WBrand.Core.Domain.Models.Blog;

namespace WBrand.Web.Kernel.Mappers
{
    public class BlogMappingProfile: Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<BlogPost, BlogPostModel>().ReverseMap();

            CreateMap<BlogPostCategory, BlogPostCategoryModel>().ReverseMap();
            
        }
    }
}