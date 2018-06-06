using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    internal sealed class BDictionaryDebuggerView<K, V>
    {
        private IDictionary<K, V> dict;

        public BDictionaryDebuggerView(IDictionary<K, V> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            this.dict = dictionary;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<K, V>[] Items
        {
            get
            {
                KeyValuePair<K, V>[] items = new KeyValuePair<K, V>[dict.Count];
                dict.CopyTo(items, 0);
                return items;
            }
        }
    }
}
