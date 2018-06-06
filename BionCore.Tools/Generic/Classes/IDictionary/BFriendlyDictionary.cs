using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    [DebuggerTypeProxy(typeof(BDictionaryDebuggerView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class BFriendlyDictionary<TKey, TValue>: BDictionary<TKey, TValue>
    {
        #region ######## Constructores ########

        public BFriendlyDictionary()
        {
            this.DefaultValue = default(TValue);
        }

        public BFriendlyDictionary(int capacity) : base(capacity)
        {
            this.DefaultValue = default(TValue);
        }

        public BFriendlyDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
            this.DefaultValue = default(TValue);
        }

        public BFriendlyDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
        {
            this.DefaultValue = default(TValue);
        }

        public BFriendlyDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer)
        {
            this.DefaultValue = default(TValue);
        }

        public BFriendlyDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
        {
            this.DefaultValue = default(TValue);
        }

        #region ======== Extended ========
        public BFriendlyDictionary(TValue defaultValue)
        {
            this.DefaultValue = defaultValue;
        }

        public BFriendlyDictionary(TValue defaultValue, int capacity) : base(capacity)
        {
            this.DefaultValue = defaultValue;
        }

        public BFriendlyDictionary(TValue defaultValue, IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
            this.DefaultValue = defaultValue;
        }

        public BFriendlyDictionary(TValue defaultValue, IEqualityComparer<TKey> comparer) : base(comparer)
        {
            this.DefaultValue = defaultValue;
        }

        public BFriendlyDictionary(TValue defaultValue, int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer)
        {
            this.DefaultValue = defaultValue;
        }

        public BFriendlyDictionary(TValue defaultValue, IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
        {
            this.DefaultValue = defaultValue;
        }
        #endregion [======== Extended ========]

        #endregion [######## Constructores ########]
        public override TValue this[TKey key]
        {
            get
            {
                if (this.ContainsKey(key))
                    return base[key];
                return this.DefaultValue;
            }

            set
            {
                base[key] = value;
            }
        }

        public virtual TValue DefaultValue { get; protected set; }
    }
}
