using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Core.Domain.Enum
{
    public enum SlideShowPosition
    {
        [Display(Description = "Tất cả vị trí")]
        All = 0,
        [Display(Description = "Slide header trang chủ")]
        HeaderHome = 10

    }
}
