using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Common.Extensions
{
    public static class GuidExtensions
    {
        public static string ToCleanString(this Guid? guid)
        {
            if (!guid.HasValue) return string.Empty;

            return guid.ToString().Replace("-", "");
        }
    }
}
