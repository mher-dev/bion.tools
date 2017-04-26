using Bion.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    internal static class BPOInternalExtensions
    {
        private static Type[] PrimitiveTypes = new Type[] 
        {
            typeof(decimal),
            typeof(string),
            typeof(DateTime),
        };

        #region ######## Type ########
        /// <summary>
        /// Devuelve el nombre portable del tipo
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetPortableName(this Type self)
        {
            var fullName = self.AssemblyQualifiedName;
            var name = fullName.Substring(0, fullName.LastIndexOf(", Culture"));
            return name;
        }

        public static bool IsGenericAssignableFrom(this Type selfGenericType, Type compareType)
        {
            bool result = false;
            var genericArgs = compareType.GenericTypeArguments;
            if (!genericArgs.Any())
                return result;

            var genericType = selfGenericType.MakeGenericType(genericArgs);
            result = genericType.IsAssignableFrom(compareType);

            return result;
        }

        /// <summary>
        /// Busca en el tipo la primera aparición de ICollection`1 y lo devuelve
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Type AsCollectionType(this Type self)
        {
            if (self.IsValueType || self.IsArray || self.IsEnum)
                throw new ArgumentException("El tipo indicado no es una colección");
            var genericType = typeof(ICollection<>);

            var compareInterfaces = self.GetInterfaces().ToList().Do(d =>
            {
                if (self.IsInterface)
                    d.Insert(0, self);
            });
            foreach (var ci in compareInterfaces)
            {
                Type ciGeneric;
                if (ci.GenericTypeArguments.Any())
                    ciGeneric = ci.GetGenericTypeDefinition();
                else
                    ciGeneric = ci;
                if (ciGeneric == genericType)
                    return ci;
            }

            throw new ArgumentException("El tipo indicado no es una colección");

        }
        public static bool IsNullable(this Type self)
        {
            if (!self.IsClass)
                return false;
            if (self.GenericTypeArguments.Count() != 1)
                return false;
            var genericType = self.GetGenericTypeDefinition();
            if (genericType != typeof(Nullable<>))
                return false;
            return true;

        }
        public static bool IsPrimitveExtended(this Type self)
        {
            if (self.IsPrimitive)
                return true;

            var baseType = self.IsNullable() ? Nullable.GetUnderlyingType(self) : self;
            var result = PrimitiveTypes.Contains(baseType);

            return result;
        }
        #endregion [######## Type ########]

        #region ######## PropertyInfo ########
        /// <summary>
        /// Determinamos los acceso mínimos requeridos tanto por get, como por set
        /// </summary>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        public static BPOPoliticsReadDepth GetMaximalDepth(this PropertyInfo propInfo)
        {
            var setMethod = propInfo.SetMethod;
            var getMethod = propInfo.GetMethod;
            if (setMethod.IsNull() || getMethod.IsNull())
                return BPOPoliticsReadDepth.Inaccessibile;

            BPOPoliticsReadDepth depth;
            var sDepth = GetDepth(setMethod);
            var gDepth = GetDepth(getMethod);
            if (sDepth > gDepth)
                depth = sDepth;
            else
                depth = gDepth;
            return depth;

        }

        private static BPOPoliticsReadDepth GetDepth(MethodInfo method)
        {
            if (method.Attributes.ContainsAny(MethodAttributes.Public, MethodAttributes.Assembly))
                return BPOPoliticsReadDepth.Public;

            if (method.Attributes.ContainsAny(MethodAttributes.Family, MethodAttributes.FamANDAssem))
                return BPOPoliticsReadDepth.Protected;

            if (method.Attributes.ContainsAny(MethodAttributes.Private))
                return BPOPoliticsReadDepth.Private;

            throw new BArgumentException($"No se ha podido determinar `{nameof(BPOPoliticsReadDepth)}´ para la  `{method.Name}´ dentro del tipo `{method.DeclaringType.GetPortableName()}´");


        }
        #endregion [######## PropertyInfo ########]

    }
}
