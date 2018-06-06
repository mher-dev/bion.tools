using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    [DebuggerTypeProxy(typeof(BDictionaryDebuggerView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class BCreativeDictionary<TKey, TValue> : BDictionary<TKey, TValue>
    {
        #region ======== Constructors ========

        public BCreativeDictionary(Func<TKey, TValue> instanceCreator)
        {
            this.InitConstructor(instanceCreator);
        }

        public BCreativeDictionary(Func<TKey, TValue> instanceCreator, int capacity) : base(capacity)
        {
            this.InitConstructor(instanceCreator);
        }

        public BCreativeDictionary(Func<TKey, TValue> instanceCreator, IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
            this.InitConstructor(instanceCreator);
        }

        public BCreativeDictionary(Func<TKey, TValue> instanceCreator, IEqualityComparer<TKey> comparer) : base(comparer)
        {
            this.InitConstructor(instanceCreator);
        }

        public BCreativeDictionary(Func<TKey, TValue> instanceCreator, int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer)
        {
            this.InitConstructor(instanceCreator);
        }

        public BCreativeDictionary(Func<TKey, TValue> instanceCreator, IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
        {
            this.InitConstructor(instanceCreator);
        }

        protected BCreativeDictionary(Func<TKey, TValue> instanceCreator, SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.InitConstructor(instanceCreator);
        }

        private void InitConstructor(Func<TKey, TValue> instanceCreator)
        {
            if (instanceCreator == null)
                throw new ArgumentNullException(nameof(instanceCreator));
            this.InstanceCreator = instanceCreator;
        }

        #endregion [======== Constructors ========]

        public virtual Func<TKey, TValue> InstanceCreator { get; protected set; }

        public override TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (!this.ContainsKey(key))
                    value = this.InstanceCreator.Invoke(key);
                else
                    value = base[key];
                return value;
            }

            set
            {
                base[key] = value;
            }
        }


        /// <summary>
        /// Always returns a value for the key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TryGetValue(TKey key, out TValue value)
        {
            value = this[key];
            return true;
        }
    }
}
