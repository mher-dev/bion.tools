using Bion.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{

    /// <summary>
    /// Cacheo general de propiedades de tipos
    /// </summary>
    internal static class BPOTypeCache
    {
        public static class GenericCache
        {
            #region -------- Attributes --------
            static object _lock_GenericCache = new object();
            #endregion [-------- Attributes --------]


            /// <summary>
            /// Diccionario de cacheo Tipo->ListaPropiedades
            /// </summary>
            private static ConcurrentDictionary<string, List<BPOMemberInfo>> Members { get; } = new ConcurrentDictionary<string, List<BPOMemberInfo>>();



            public static List<BPOMemberInfo> ReadType(Type type)
            {
                if (type.IsAbstract)
                    throw new BArgumentException("No se pueden tramitar típos abstractos");

                if (type.IsInterface)
                    throw new BArgumentException("No se pueden tramitar interfaces");

                var typeKey = type.GetPortableName();
                if (!Members.ContainsKey(typeKey))
                {
                    var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                    lock (_lock_GenericCache)
                    {
                        var list = new List<BPOMemberInfo>();
                        var fieldList = type.GetFields(flags);
                        var propertyList = type.GetProperties(flags);

                        list.AddRange(fieldList.Select(s => new BPOMemberInfo(s)));
                        list.AddRange(propertyList.Select(s => new BPOMemberInfo(s)));
                        Members[typeKey] = list;
                    }
                }

                return Members[typeKey].ToList();
            }
        }

        public static class Cache<T>
        {
            /// <summary>
            /// Leemos de forma estática el tipo
            /// </summary>
            static Cache()
            {
                //Analizamos el tipo
               // GenericCache.ReadType(typeof(T));
            }

            public static void Invoke()
            {

                GenericCache.ReadType(typeof(T));
            }
        }
    }
}
