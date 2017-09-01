using System;
using System.Collections.Generic;

namespace Coinbin.net.Util
{
    public static class IEnumerableExtensions
    {
        public static string StrCat(this IEnumerable<string> instance, string separator)
            => string.Join(separator, instance);
    }
}
