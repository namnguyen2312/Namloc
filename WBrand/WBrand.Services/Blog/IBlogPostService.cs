﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Core.Domain.Models.Blog;

namespace WBrand.Services.Blog
{
    public interface IBlogPostService
    {
        BlogPostModel GetById(long id);
    }
}