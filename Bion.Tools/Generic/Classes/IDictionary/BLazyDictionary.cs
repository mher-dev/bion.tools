using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools
{
    /// <summary>
    /// Creates new instance of TValue if there is no one asignated
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [DebuggerTypeProxy(typeof(BDictionaryDebuggerView<,>))]
    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class BLazyDictionary<TKey, TValue> : BDictionary<TKey, TValue>
        where TValue : new()
    {
        public override TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (!this.ContainsKey(key))
                {
                    value = new TValue();
                    base.Add(key, value);
                }
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
