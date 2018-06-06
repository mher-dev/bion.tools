using System;

namespace BionCore
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class BionLinealExtensions
    {

        #region ======== AS ========
        /// <summary>
        /// Realiza un cast del objeto al tipo indicado o devuelve el valor de por defecto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T As<T>(this object self)
        {
            if (self is T)
                return (T)self;
            return default(T);
        }

        /// <summary>
        /// Realiza un cast del objeto al tipo indicado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static T ForceAs<T>(this object self)
        {
            return (T)self;
        }

        /// <summary>
        /// Realiza un cast del objeto al tipo indicado o devuelve `defaultValue´
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="defaultValue">Valor de por defecto que devolver en caso de que el objeto no sea de tipo T</param>
        /// <returns></returns>
        public static T As<T>(this object self, T defaultValue)
        {
            if (self is T)
                return (T)self;
            return defaultValue;
        }
        #endregion [======== AS ========]


        #region ======== IfNull ========
        /// <summary>
        /// Si el objeto es nulo, devuelve el valor de  `returnValue´
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="returnValue">Valor que retornar en caso de que self este nulo</param>
        /// <returns></returns>
        public static T IfNull<T>(this T self, T returnValue)
        {
            if (self == null)
                return returnValue;
            return self;
        }

        /// <summary>
        /// Si el objeto es nulo, devuelve el valor de `returnValue´
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="returnValue">Valor que retornar en caso de que self este nulo</param>
        /// <returns></returns>
        public static T IfNull<T>(this T? self, T returnValue)
            where T : struct
        {
            if (!self.HasValue)
                return returnValue;
            return self.Value;
        }

        /// <summary>
        /// Si el objeto es nulo, devuelve el resultado de la función `returnFunction´
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="returnFunction">Valor que retornar en caso de que self este nulo</param>
        /// <returns></returns>
        public static T IfNull<T>(this T self, Func<T> returnFunction)
        {
            if (returnFunction == null)
                throw new ArgumentNullException("returnFunction");

            if (self == null)
                return returnFunction.Invoke();
            return self;
        }
        #endregion [======== IfNull ========]


        #region ======== IfNotNull ========
        /// <summary>
        /// En caso de que el objeto no sea nulo, ejecuta la accion `action´ pasandole como parámetro el objeto en si mismo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="action">Acción a ejecutar</param>
        /// <returns>Devuelve el objeto de origen</returns>
        public static T IfNotNull<T>(this T self, Action<T> action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            if (self != null)
                action.Invoke(self);
            return self;
        }

        /// <summary>
        /// En caso de que el objeto no sea nulo, ejecuta la función `function´ pasandole como parámetro el objeto en si mismo.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="function">Función a ejecutar</param>
        /// <returns>Devuelve el resultado de la función</returns>
        public static T IfNotNull<T>(this T self, Func<T, T> function)
        {
            if (function == null)
                throw new ArgumentNullException("function");

            if (self != null)
                return function.Invoke(self);
            return self;
        }
        #endregion [======== IfNotNull ========]


        #region ======== Is ========
        /// <summary>
        /// Devuelve si el objeto implementa el tipo indicado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool Is<T>(this object self)
        {
            return self is T;
        }

        /// <summary>
        /// Devuelve si el objeto no implementa el tipo indicado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNot<T>(this object self)
        {
            return !self.Is<T>();
        }
        #endregion [======== Is ========]


        #region ======== Extend ========
        /// <summary>
        /// Ejecuta la acción `extendedAction´ pasandole el objeto de origen y tras ello devuelve el objeto de origen.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <param name="extendAction"></param>
        /// <returns></returns>
        public static T Do<T>(this T self, Action<T> extendAction)
        {
            if (extendAction == null)
                throw new ArgumentNullException("extendAction");

            extendAction.Invoke(self);
            return self;
        }

        /// <summary>
        /// Devuelve el mismo valor que ha recibido. Sirve para  invocar propiedades sin tener que almacenarlos en variables temporales
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T Do<T>(this T self)
        {
            return self;
        }
        #endregion [======== Extend ========]

        #region ======== IsNull ========
        /// <summary>
        /// Devuelve si el objeto es nulo
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T self)
            where T : class
        {
            return self == null;
        }

        /// <summary>
        /// Devuelve si el objeto es nulo
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this Nullable<T> self)
            where T : struct
        {
            return !self.HasValue;
        }
        #endregion [======== IsNull ========]

        #region ======== IsNotNull ========
        /// <summary>
        /// Devuelve si el objeto es no nulo
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T self)
            where T : class
        {
            return self != null;
        }

        /// <summary>
        /// Devuelve si el objeto no es nulo
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this Nullable<T> self)
            where T : struct
        {
            return self.HasValue;
        }
        #endregion [======== IsNotNull ========]

        #region ======== NullIfEmpty ========
        /// <summary>
        /// Devuelve un valor nulo en caso de que el string este nulo o vacío
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string NullIfEmpty(this string self)
        {
            if (self == null || self.Length == 0)
                return null;
            return self;
        }

        /// <summary>
        /// Devuelve un valor nulo en caso de que el string este nulo o vacío
        /// </summary>
        /// <param name="self"></param>
        /// <param name="whiteSpaces"></param>
        /// <returns></returns>
        public static string NullIfEmpty(this string self, bool whiteSpaces)
        {
            if (!whiteSpaces)
                return self.NullIfEmpty();
            if (string.IsNullOrWhiteSpace(self))
                return null;

            return self;
        }
        #endregion [======== NullIfEmpty ========]
    }
}
