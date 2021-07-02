using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Common.Helper
{
    public class CoreHelper
    {
        public static DateTimeOffset SystemTimeNow { get; } = DateTimeOffset.UtcNow;
    }
}
