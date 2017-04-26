using Bion.PO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion
{
    public static class BPOTools
    {
        public static byte[] ToPortableObject(object value, BPOContext bpoContext)
        {
            var packDictionary = new ConcurrentDictionary<string, BPOValue>();
            var bpo = new BPOValue(value, bpoContext.Politics, packDictionary);
            bpo.Pack();

            var serializationContext = bpoContext.CustomSerializationContext ?? new BPOSerializationContext();

            var result = serializationContext.Pack(bpo, bpoContext.CompressionLevel.HasValue, bpoContext.CompressionLevel.IfNull(0));
            return result;
        }

        public static object FromPortableObject(byte[] bpoArray, BPOContext bpoContext)
        {
            var serializationContext = bpoContext.CustomSerializationContext ?? new BPOSerializationContext();
            var instanceContext = bpoContext.CustomInstanceContext.IfNull(new BPOInstanceContext());
            var bpo = serializationContext.Unpack(bpoArray, bpoContext.CompressionLevel.HasValue, bpoContext.CompressionLevel.IfNull(0)).ForceAs<BPOValue>();
            bpo.Unpack(instanceContext);

            return bpo.RawValue;
        }


        internal static string GetObjectHashCode(object element)
        {
            var type = element.GetType().GetPortableName();
            var hash = element.GetHashCode();
            return hash + "@" + type;
        }

        internal static string GetAtomicName(Type tipo)
        {
            var type = tipo.GetPortableName();
            var hash = "ATOMIC";
            return hash + "@" + type;
        }

        internal static bool IsCollection(Type tipo)
        {
            if (tipo.IsValueType || tipo.IsArray || tipo.IsEnum)
                return false;

            var collectionType = typeof(ICollection<>);

            var compareInterfaces = tipo.GetInterfaces().ToList().Do(d =>
            {
                if (tipo.IsInterface)
                    d.Insert(0, tipo);
            });
            foreach (var ci in compareInterfaces)
            {
                Type ciGeneric;
                if (ci.GenericTypeArguments.Any())
                    ciGeneric = ci.GetGenericTypeDefinition();
                else
                    ciGeneric = ci;
                if (ciGeneric == collectionType)
                    return true;
            }

            return false;

        }




    }
}
