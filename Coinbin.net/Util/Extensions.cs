using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Coinbin.net.Util
{
    public static class IEnumerableExtensions
    {
        public static string StrCat(this IEnumerable<string> instance, string separator)
            => string.Join(separator, instance);
    }

    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            string description = null;

            if (e is Enum)
            {
                description = e.ToString();

                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var enumName = type
#if NETSTANDARD1_5
                            .GetTypeInfo()
#endif
                            .GetEnumName(val);

                        var memInfo = type
#if NETSTANDARD1_5
                            .GetTypeInfo()
#endif
                            .GetMember(enumName);

                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).ToArray();
                        if (descriptionAttributes.Length > 0)
                        {
                            // we're only getting the first description we find
                            // others will be ignored
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }

            return description;
        }
    }
}
