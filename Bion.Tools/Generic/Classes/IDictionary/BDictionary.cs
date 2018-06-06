using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools
{
    [DebuggerTypeProxy(typeof(BDictionaryDebuggerView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class BDictionary<TKey, TValue>: IBDictionary<TKey, TValue>
    {

        #region ######## Self ########
        protected Dictionary<TKey, TValue> WrappedDictionary { get; set; }

        
        #region ======== Constructores ========
        public BDictionary()
        {
            this.WrappedDictionary = new BDictionaryBase<TKey, TValue>();
        }

        public BDictionary(IEqualityComparer<TKey> comparer)
        {
            this.WrappedDictionary = new BDictionaryBase<TKey, TValue>(comparer);

        }

        public BDictionary(IDictionary<TKey, TValue> dictionary) 
        {
            this.WrappedDictionary = new BDictionaryBase<TKey, TValue>(dictionary);
        }

        public BDictionary(int capacity)
        {
            this.WrappedDictionary = new BDictionaryBase<TKey, TValue>(capacity);

        }

        public BDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            this.WrappedDictionary = new BDictionaryBase<TKey, TValue>(dictionary, comparer);
        }

        public BDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            this.WrappedDictionary = new BDictionaryBase<TKey, TValue>(capacity, comparer);
        }

        protected BDictionary(SerializationInfo info, StreamingContext context)
        {
            this.WrappedDictionary = new BDictionaryBase<TKey, TValue>(info, context, _internal: true);

        }
        #endregion [======== Constructores ========]

        #region ======== Override ========
        #endregion [======== Override ========]

        #endregion [######## Self ########]



        #region ######## Interface: IDictionary ########
        public virtual TValue this[TKey key]
        {
            get
            {
                return ((IDictionary<TKey, TValue>)WrappedDictionary)[key];
            }

            set
            {
                ((IDictionary<TKey, TValue>)WrappedDictionary)[key] = value;
            }
        }

        public virtual int Count
        {
            get
            {
                return ((IDictionary<TKey, TValue>)WrappedDictionary).Count;
            }
        }

        public virtual bool IsFixedSize
        {
            get
            {
                return ((IDictionary)WrappedDictionary).IsFixedSize;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return ((IDictionary<TKey, TValue>)WrappedDictionary).IsReadOnly;
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return ((IDictionary)WrappedDictionary).IsSynchronized;
            }
        }

        public virtual ICollection<TKey> Keys
        {
            get
            {
                return ((IDictionary<TKey, TValue>)WrappedDictionary).Keys;
            }
        }

        public virtual object SyncRoot
        {
            get
            {
                return ((IDictionary)WrappedDictionary).SyncRoot;
            }
        }

        public virtual ICollection<TValue> Values
        {
            get
            {
                return ((IDictionary<TKey, TValue>)WrappedDictionary).Values;
            }
        }


        ICollection IDictionary.Keys
        {
            get
            {
                return ((IDictionary)WrappedDictionary).Keys;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return ((IDictionary)WrappedDictionary).Values;
            }
        }

        public virtual void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>)WrappedDictionary).Add(item);
        }

        public virtual void Add(object key, object value)
        {
            ((IDictionary)WrappedDictionary).Add(key, value);
        }

        public virtual void Add(TKey key, TValue value)
        {
            ((IDictionary<TKey, TValue>)WrappedDictionary).Add(key, value);
        }

        public virtual void Clear()
        {
            ((IDictionary<TKey, TValue>)WrappedDictionary).Clear();
        }

        public virtual bool Contains(object key)
        {
            return ((IDictionary)WrappedDictionary).Contains(key);
        }

        public virtual bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)WrappedDictionary).Contains(item);
        }

        public virtual bool ContainsKey(TKey key)
        {
            return ((IDictionary<TKey, TValue>)WrappedDictionary).ContainsKey(key);
        }

        public virtual void CopyTo(Array array, int index)
        {
            ((IDictionary)WrappedDictionary).CopyTo(array, index);
        }

        public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>)WrappedDictionary).CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>)WrappedDictionary).GetEnumerator();
        }



        public virtual void Remove(object key)
        {
            ((IDictionary)WrappedDictionary).Remove(key);
        }

        public virtual bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)WrappedDictionary).Remove(item);
        }

        public virtual bool Remove(TKey key)
        {
            return ((IDictionary<TKey, TValue>)WrappedDictionary).Remove(key);
        }

        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            return ((IDictionary<TKey, TValue>)WrappedDictionary).TryGetValue(key, out value);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return ((IDictionary)WrappedDictionary).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IDictionary<TKey, TValue>)WrappedDictionary).GetEnumerator();
        }
        object IDictionary.this[object key]
        {
            get
            {
                return ((IDictionary)WrappedDictionary)[key];
            }

            set
            {
                ((IDictionary)WrappedDictionary)[key] = value;
            }
        }
        #endregion [######## Interface: IDictionary ########]


        #region ######## Interface: ISerializable ########
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)WrappedDictionary).GetObjectData(info, context);
        }

        public virtual void OnDeserialization(object sender)
        {
            ((IDeserializationCallback)WrappedDictionary).OnDeserialization(sender);
        }
        #endregion [######## Interface: ISerializable ########]

        #region ######## IReadOnlyDictionary ########

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return ((IReadOnlyDictionary<TKey, TValue>)WrappedDictionary).Keys;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return ((IReadOnlyDictionary<TKey, TValue>)WrappedDictionary).Values;
            }
        }



        #endregion [######## IReadOnlyDictionary ########]

    }
}
