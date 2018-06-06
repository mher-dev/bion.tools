using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BionCore
{
    public static class BionCoreEnumExtensions
    {

        public static bool ContainsAny<T>(this T self, params T[] values)
            where T : struct, IComparable, IFormattable, IConvertible
        {
            var selfCast = self.ForceAs<int>();
            foreach (var item in values)
            {
                var itemCast = item.ForceAs<int>();
                if ((selfCast & itemCast) == itemCast)
                    return true;
            }

            return false;
        }



    }
}
