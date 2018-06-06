using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools.Compression
{
    public class BCompressor
    {
        /// <summary>
        /// Marca que el stream de datos ha sido 'Deflate'
        /// </summary>
        public const string ConstDeflateMarker = "{B|DeflateStream}";
        internal static readonly byte[] DeflateMarkerArray = Encoding.ASCII.GetBytes(ConstDeflateMarker);

        /// <summary>
        /// Comprime los datos utilizando DeflateStream
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="markData">Marcar los datos como Deflate</param>
        /// <returns></returns>
        public static byte[] Deflate(byte[] rawData, bool markData = true)
        {
            var origin = rawData;
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(rawData, 0, rawData.Length);
            }

            byte[] result = output.ToArray();
            if (markData)
            {
                var markArray = DeflateMarkerArray;
                result = markArray.Concat(result).ToArray();
            }

            return result;
        }

        /// <summary>
        /// Descomprime los datos utilizando DeflateStream.
        /// </summary>
        /// <param name="deflatedData">Array de datos a descomprimir</param>
        /// <param name="checkMark">Confirma que los datos tengan la marca de Deflate, lo quita y descomprime.</param>
        /// <returns></returns>
        public static byte[] Inflate(byte[] deflatedData, bool checkMark = true)
        {
            int startIndex = 0;
            if (checkMark)
            {
                bool match = IsMarkedAsDeflated(deflatedData);

                if (!match)
                    throw new ArgumentException($"El parámetro recibido no contiene datos que estén marcados como comprimidos por el método `{nameof(Deflate)}´. Intente utlizar {nameof(Inflate)} sin comprobación de marca ({nameof(checkMark)} = false)");

                startIndex = DeflateMarkerArray.Length;
            }

            MemoryStream input = new MemoryStream(deflatedData, startIndex, (deflatedData.Length - startIndex));
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }

        /// <summary>
        /// Indica si el array esta marcado como Deflated
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsMarkedAsDeflated(byte[] data)
        {
            bool match = true;
            var searchArray = DeflateMarkerArray;
            for (int i = 0; i < searchArray.Length; i++)
            {
                if (i >= data.Length)
                {
                    match = false;
                    break;
                }

                if (searchArray[i] != data[i])
                {
                    match = false;
                    break;
                }
            }

            return match;
        }
    }
}
