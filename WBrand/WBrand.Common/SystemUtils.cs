using NCuid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBrand.Common
{
    public static class SystemUtils
    {
        public static string GenaratorSringId => Cuid.Generate(RandomSource.Secure);
        public static DateTimeOffset SystemTimeNow => DateTimeOffset.UtcNow;
    }
}
