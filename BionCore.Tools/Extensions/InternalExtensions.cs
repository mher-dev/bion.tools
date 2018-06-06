using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore
{
    static internal class InternalExtensions
    {

        #region -------- Type --------
        public static string GetPortableName(this Type self)
        {
            var fullName = self.AssemblyQualifiedName;
            var name = fullName.Substring(0, fullName.LastIndexOf(", Culture"));
            return name;
        }


        #endregion [-------- Type --------]

        #region -------- String --------
        public static bool IsEmpty(this string self, bool ignoreWhiteSpace = false)
        {
            if (ignoreWhiteSpace)
                return string.IsNullOrWhiteSpace(self);
            else
                return string.IsNullOrEmpty(self);
        }

        public static bool IsNotEmpty(this string self, bool ignoreWhiteSpace = false)
        {
            return !self.IsEmpty();
        }
        #endregion [-------- String --------]

    }
}
