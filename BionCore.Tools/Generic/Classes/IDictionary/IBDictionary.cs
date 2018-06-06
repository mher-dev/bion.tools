using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    public interface IBDictionary
        : IDictionary
        , ICollection
        , IEnumerable
        , ISerializable
        , IDeserializationCallback
    {

    }


    public interface IBDictionary<TKey, TValue>:
        IBDictionary,
        IDictionary<TKey, TValue>, 
        ICollection<KeyValuePair<TKey, TValue>>, 
        IReadOnlyDictionary<TKey, TValue>, 
        IReadOnlyCollection<KeyValuePair<TKey, TValue>>, 
        IEnumerable<KeyValuePair<TKey, TValue>>
    {
    }
}
