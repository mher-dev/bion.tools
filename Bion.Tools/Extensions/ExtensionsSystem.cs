using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion
{
    public static partial class BExtensions
    {
        public static object GetDefaultValue(this Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// BP
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Type GetNotNullType(this Type self)
        {
            var result = Nullable.GetUnderlyingType(self);
            if (result == null)
                return self;
            return result;
        }

        public static bool IsNullable(this Type self)
        {
            return Nullable.GetUnderlyingType(self) != null;
        }






        public static bool SafeEquals(this object self, object compare)
        {
            return (self?.Equals(compare) ?? false);
        }

        public static string ToStringRecursive(this Exception ex, string joinBy = null, bool includeStack = false)
        {
            string result = "";
            if (ex.IsNull())
                return result;
            joinBy = joinBy ?? Environment.NewLine;
            bool firstTime = true;
            Exception inner = ex;
            do
            {
                if (!firstTime) result += joinBy;
                else firstTime = false;

                result += inner.Message;
                if (includeStack)
                {
                    result += "\r\n----------------------- STACK TRACE -----------------------\r\n";
                    result += inner.StackTrace + "\r\n";
                }
                inner = inner.InnerException;
            } while (inner != null);

            return result;
        }
    }
}
