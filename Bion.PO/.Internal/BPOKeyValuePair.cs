using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    using LoopDictionary = ConcurrentDictionary<string, BPOValue>;
    [Serializable]
    internal class BPOKeyValuePair : IBPOPackUnpack
    {
        private static string KeyPropertyName = nameof(KeyValuePair<object, object>.Key);
        private static string ValuePropertyName = nameof(KeyValuePair<object, object>.Value);

        public BPOKeyValuePair(object keyValuePair, IBPOPolitics politics, LoopDictionary alreadyPacked)
        {
            if (keyValuePair == null)
                throw new ArgumentNullException(nameof(keyValuePair));
            this.RawKeyValuePair = keyValuePair;
            this.BPOType = new BPOType(keyValuePair.GetType());
            this.BPOPolitics = politics;
            this.MakePortable(politics, alreadyPacked);
        }

        private void MakePortable(IBPOPolitics politics, LoopDictionary alreadyPacked)
        {
            var rawKey = this.BPOType.RawType.GetProperty(KeyPropertyName).GetValue(this.RawKeyValuePair);
            var rawValue = this.BPOType.RawType.GetProperty(ValuePropertyName).GetValue(this.RawKeyValuePair);

            this.BPOKey = new BPOValue(rawKey, politics, alreadyPacked);
            this.BPOValue = new BPOValue(rawValue, politics, alreadyPacked);


        }

        public void Unpack(IBPOInstanceContext context)
        {
            if (this.IsUnpacked)
                return;
            this.IsUnpacked = true;
            this.BPOType.Unpack(context);
            this.BPOValue.Unpack(context);
            this.BPOKey.Unpack(context);


            this.RawKeyValuePair = context.Instance(this.BPOType.RawType, new object[] { this.BPOKey.RawValue, this.BPOValue.RawValue });

        }

        public void Pack()
        {
            if (this.IsPacked)
                return;
            this.IsPacked = true;

            this.RawKeyValuePair = null;
            this.BPOType.Pack();
            this.BPOValue.Pack();
            this.BPOKey.Pack();
        }

        public object RawKeyValuePair { get; set; }
        public object RawValue { get { return this.RawKeyValuePair; }  }

        #region -------- Property: BPOType --------
        private BPOType _BPOType;
        public virtual BPOType BPOType
        {
            get { return this._BPOType; }
            set { this._BPOType = value; }
        }
        #endregion [-------- BPOType --------]


        #region -------- Property: Key --------
        private BPOValue _BPOKey;
        public virtual BPOValue BPOKey
        {
            get { return this._BPOKey; }
            set { this._BPOKey = value; }
        }
        #endregion [-------- Key --------]

        #region -------- Property: Value --------
        private BPOValue _BPOValue;
        public virtual BPOValue BPOValue
        {
            get { return this._BPOValue; }
            set { this._BPOValue = value; }
        }
        #endregion [-------- Value --------]

        #region -------- Property: BPOPolitics --------
        private IBPOPolitics _BPOPolitics;
        public virtual IBPOPolitics BPOPolitics
        {
            get { return this._BPOPolitics; }
            set { this._BPOPolitics = value; }
        }

        public bool IsUnpacked { get; private set; }


        public bool IsPacked { get; private set; }
        #endregion [-------- BPOPolitics --------]

    }

}
