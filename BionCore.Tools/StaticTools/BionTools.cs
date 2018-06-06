using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    public static partial class BionTools
    {
        #region -------- INTERNAL STATIC FUNCTIONS --------
        #region -------- Serialization --------

        internal static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        internal static object Deserialize(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = (object)binForm.Deserialize(memStream);

            return obj;
        }

        #region ######## BUID ########
        /// <summary>
        /// Genera un BUID unico para objetos IBUID
        /// </summary>
        /// <returns></returns>
        public static string GenerateBUID()
        {
            return new Guid().ToString();
        }
        #endregion [######## BUID ########]


        #endregion [-------- Serialization --------]




        /// <summary>
        /// Lanza una excepción con texto parametrizado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="creator"></param>
        internal static void Throw<T>(string message, Func<string, T> creator)
            where T : Exception
        {
            message = "Bion.Tools: " + message;
            throw creator(message);
        }

        internal static void Throw<T>(string message, Exception innerException, Func<string, Exception, T> creator)
            where T : Exception
        {
            message = "Bion.Tools: " + message;
            throw creator(message, innerException);
        }

        #endregion [-------- INTERNAL STATIC FUNCTIONS --------]



        #region ======== RAndom ========
        static int _rand_seed = Environment.TickCount;

        static readonly ThreadLocal<Random> _rand_randomGenerator =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _rand_seed)));

        /// <summary>
        /// Devuelve un valor int aleatorio
        /// </summary>
        public static int ThreadSafeRandomInt(int from, int to)
        {
            return _rand_randomGenerator.Value.Next(from, to);
        }

        /// <summary>
        /// Devuelve un valor int aleatorio
        /// </summary>
        public static int ThreadSafeRandomInt()
        {
            return _rand_randomGenerator.Value.Next();
        }


        /// <summary>
        /// Devuelve un valor int aleatorio
        /// </summary>
        public static double ThreadSafeRandomDouble(double from, double to)
        {
            var result = _rand_randomGenerator.Value.NextDouble();
            return result * (to - from) + from;
        }

        /// <summary>
        /// Devuelve un valor int aleatorio
        /// </summary>
        public static double ThreadSafeRandomDouble()
        {
            return _rand_randomGenerator.Value.NextDouble();
        }

        #endregion [======== RAndom ========]

    }
}
