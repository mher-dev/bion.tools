using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion
{
    public static partial class BionExtensionsLinq
    {

        #region -------- IList --------
        private static void _Tack<T>(ICollection<T> self, T item)
        {
            self.Add(item);
        }

        private static void _TackRange<T>(ICollection<T> self, IEnumerable<T> collection)
        {
            foreach (var item in collection)
                self.Add(item);
        }

        /// <summary>
        /// Adds an item to the System.Collections.Generic.ICollection`1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="item">The object to add to the System.Collections.Generic.ICollection`1.</param>
        /// <returns></returns>
        public static IList<T> Tack<T>(this IList<T> self, T item)
        {
            _Tack(self, item);
            return self;
        }


        /// <summary>
        /// Adds an item to the System.Collections.Generic.ICollection`1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="item">The object to add to the System.Collections.Generic.ICollection`1.</param>
        /// <returns></returns>
        public static List<T> Tack<T>(this List<T> self, T item)
        {
            _Tack(self, item);
            return self;
        }


        /// <summary>
        /// Adds an item to the System.Collections.Generic.ICollection`1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="item">The object to add to the System.Collections.Generic.ICollection`1.</param>
        /// <returns></returns>
        public static ICollection<T> Tack<T>(this ICollection<T> self, T item)
        {
            _Tack(self, item);
            return self;
        }



        /// <summary>
        /// Adds the elements of the specified collection to the end of the System.Collections.Generic.List`1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="collection">
        /// The collection whose elements should be added to the end of the System.Collections.Generic.List`1.<para/>
        /// The collection itself cannot be null, but it can contain elements that are null,<para/>
        /// if type T is a reference type.<para/>
        /// </param>
        /// <returns></returns>
        public static IList<T> TackRange<T>(this IList<T> self, IEnumerable<T> collection)
        {
            _TackRange(self, collection);
            return self;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the System.Collections.Generic.List`1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="collection">
        /// The collection whose elements should be added to the end of the System.Collections.Generic.List`1.<para/>
        /// The collection itself cannot be null, but it can contain elements that are null,<para/>
        /// if type T is a reference type.<para/>
        /// </param>
        /// <returns></returns>
        public static List<T> TackRange<T>(this List<T> self, IEnumerable<T> collection)
        {
            _TackRange(self, collection);
            return self;
        }


        /// <summary>
        /// Adds the elements of the specified collection to the end of the System.Collections.Generic.List`1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="collection">
        /// The collection whose elements should be added to the end of the System.Collections.Generic.List`1.<para/>
        /// The collection itself cannot be null, but it can contain elements that are null,<para/>
        /// if type T is a reference type.<para/>
        /// </param>
        /// <returns></returns>
        public static ICollection<T> TackRange<T>(this ICollection<T> self, IEnumerable<T> collection)
        {
            _TackRange(self, collection);
            return self;
        }



        /// <summary>
        /// Adds the elements of the specified collection to the end of the System.Collections.Generic.List`1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="collection">
        /// The collection whose elements should be added to the end of the System.Collections.Generic.List`1.<para/>
        /// The collection itself cannot be null, but it can contain elements that are null,<para/>
        /// if type T is a reference type.<para/>
        /// </param>
        /// <returns></returns>
        public static K TackRange<T, K>(this K self, IEnumerable<T> collection)
            where K : ICollection<T>
        {
            _TackRange(self, collection);
            return self;
        }

        #endregion [-------- IList --------]

        #region -------- List --------
        //public static List<T> Adjoin<T>(this List<T> self, T item)
        //{
        //    self.Adjoin(item);
        //    return self;
        //}

        [DebuggerStepThrough]
        public static List<T> ForAll<T>(this List<T> self, Action<T> action)
        {
            self.ForEach(action);
            return self;
        }

        [DebuggerStepThrough]
        public static K RemoveRange<T, K>(this K self, IEnumerable<T> range)
            where K : List<T>
        {
            foreach (var item in range)
            {
                self.Remove(item);
            }
            return self;
        }
        #endregion [-------- List --------]

        #region -------- IEnumerable --------
        ///// <summary>
        ///// BP
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="e"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static IEnumerable<T> Adjoin<T>(this IEnumerable<T> e, T value)
        //{
        //    foreach (var cur in e)
        //    {
        //        yield return cur;
        //    }
        //    yield return value;
        //}

        //public static IEnumerable<T> Adjoin<T>(this IEnumerable<T> self, IEnumerable<T> items)
        //{
        //    return self.Concat(items);
        //}

        /// <summary>
        /// BP
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool Some<T>(this IEnumerable<T> self)
        {
            return (self != null ? self.Any() : false);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ForAll<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
                action(item);
            return self;
        }

        /// <summary>
        /// Ejecuta la función indicada para cada una de las entidades de la lista.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        /// <returns>En caso de recibir un False sale del ciclo</returns>
        public static IEnumerable<T> ForAll<T>(this IEnumerable<T> self, Func<T, bool> action)
        {
            foreach (var item in self)
            {
                if (!action(item))
                    break;
            }
            return self;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> self)
        {
            return self == null || !self.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> self)
        {
            return !self.IsEmpty();
        }

        public static List<T> ToList<T>(this IEnumerable self)
        {
            var list = new List<T>();
            foreach (var item in self)
            {
                list.Add((T)item);
            }
            return list;
        }

        public static string JoinToString(this IEnumerable<string> self, string separator = ",")
        {
            return string.Join(separator, self);
        }
        #endregion [-------- IEnumerable --------]

    }
}
