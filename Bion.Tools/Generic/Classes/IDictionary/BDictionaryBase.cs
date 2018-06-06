using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools
{
    [DebuggerTypeProxy(typeof(BDictionaryDebuggerView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    internal class BDictionaryBase<TKey, TValue> : Dictionary<TKey, TValue>,  IBUID
    {

        #region -------- Constructors: Dictionary --------
        public BDictionaryBase()
        {
        }

        public BDictionaryBase(IEqualityComparer<TKey> comparer) : base(comparer)
        {
        }

        public BDictionaryBase(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
        }

        public BDictionaryBase(int capacity) : base(capacity)
        {
        }

        public BDictionaryBase(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
        {
        }

        public BDictionaryBase(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer)
        {
        }

        internal BDictionaryBase(SerializationInfo info, StreamingContext context, bool _internal) : base(info, context)
        {
        }

        protected BDictionaryBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endregion [-------- Constructors: Dictionary --------]

        #region ######## IBUID ########
        public int BUID { get; } = BionTools.GenerateBUID();
        #endregion [######## IBUID ########]

    }
}
