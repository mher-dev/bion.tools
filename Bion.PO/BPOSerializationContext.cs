using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    public class BPOSerializationContext : IBPOSerializationContext
    {
        protected virtual MemoryStream Compress(MemoryStream dataStream, int level)
        {
            return dataStream;
        }

        protected virtual MemoryStream Decompress(MemoryStream dataStream, int level)
        {
            return dataStream;
        }

        public virtual byte[] Pack(IBPOPackUnpack bpoObject, bool compress, int compressLevel)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, bpoObject);
                if (compress)
                {
                    using (var compressed = this.Compress(stream, compressLevel))
                    {
                        return compressed.ToArray();
                    }
                }
                return stream.ToArray();
            }
        }

        public virtual IBPOPackUnpack Unpack(byte[] bpoData, bool compressed, int compressLevel)
        {

            IFormatter formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(bpoData))
            {
                if (compressed)
                {
                    using (var dStream = this.Decompress(stream, compressLevel))
                    {
                        var rawObject = formatter.Deserialize(stream);
                        return rawObject.ForceAs<IBPOPackUnpack>();
                    }
                }
                else
                {
                    var rawObject = formatter.Deserialize(stream);

                    return rawObject.ForceAs<IBPOPackUnpack>();
                }
            }
        }
    }
}
