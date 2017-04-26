using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    using LoopDictionary = ConcurrentDictionary<string, BPOValue>;
    [Serializable]
    internal class BPOValue : IBPOPackUnpack
    {



        public BPOValue(object value, IBPOPolitics politics, LoopDictionary alreadyPacked)
        {
            this.AlreadyPacked = alreadyPacked;
            this.RawValue = value;
            this.BPOPolitics = politics;
            if (value == null)
            {
                this.BPOValueType = new BPOType(typeof(void));
                return;
            }
            this.BPOValueType = new BPOType(value.GetType());

            if (!this.BPOValueType.IsSimpleType)
            {
                this.UniqueName = BPOTools.GetObjectHashCode(value);
                if (this.AlreadyPacked.ContainsKey(UniqueName))
                    throw new Exception($"Ya existe un elemento tramitado con el mismo HASH: `{UniqueName}´");

                this.AlreadyPacked[UniqueName] = this;
            }
            else
            {
                this.UniqueName = BPOTools.GetAtomicName(this.BPOValueType.RawType);
            }

            if (this.BPOValueType.IsSimpleType)
                this.SimpleValue = value;
            else if (this.BPOValueType.IsCollection)
                this.BPOCollectionValue = new BPOCollection(value.ForceAs<ICollection>(), politics, this.AlreadyPacked);
            else
                this.BPOComplexValues = CreateComplexValue(this.BPOValueType.RawType, this.RawValue, politics, this.AlreadyPacked);

        }

        public LoopDictionary AlreadyPacked { get; set; }
        private string _UniqueName;
        public string UniqueName
        {
            get { return _UniqueName; }
            set { _UniqueName = value; }
        }

        #region -------- Property: BPOPolitics --------
        private IBPOPolitics _BPOPolitics;
        public virtual IBPOPolitics BPOPolitics
        {
            get { return this._BPOPolitics; }
            private set { this._BPOPolitics = value; }
        }
        #endregion [-------- BPOPolitics --------]


        #region -------- Property: IsSimpleType --------
        public virtual bool IsSimpleType
        {
            get { return this.BPOValueType.IsSimpleType; }
        }
        #endregion [-------- IsSimpleType --------]

        #region -------- Property: IsNullType --------
        public virtual bool IsNullType
        {
            get { return this.BPOValueType.IsNullType; }
        }
        #endregion [-------- IsNullType --------]

        #region -------- Property: IsCollectionType --------
        public virtual bool IsCollectionType
        {
            get { return this.BPOValueType.IsCollection; }
        }
        #endregion [-------- IsCollectionType --------]

        #region -------- Property: RawValue --------
        private object _RawValue;
        public virtual object RawValue
        {
            get { return this._RawValue; }
            set { this._RawValue = value; }
        }
        #endregion [-------- RawValue --------]

        #region -------- Property: BPOValueType --------
        private BPOType _BPOValueType;
        public virtual BPOType BPOValueType
        {
            get { return this._BPOValueType; }
            set { this._BPOValueType = value; }
        }
        #endregion [-------- BPOValueType --------]


        #region -------- Property: SimpleValue --------
        private object _SimpleValue;
        /// <summary>
        /// Valor simple desempaquetado tal cual
        /// </summary>
        public virtual object SimpleValue
        {
            get { return this._SimpleValue; }
            set { this._SimpleValue = value; }
        }
        #endregion [-------- SimpleValue --------]

        #region -------- Property: BPOMemberInfos --------
        private List<BPOComplexValue> _BPOComplexValues;
        public virtual List<BPOComplexValue> BPOComplexValues
        {
            get { return this._BPOComplexValues ?? (this._BPOComplexValues = new List<BPOComplexValue>()); }
            set { this._BPOComplexValues = value; }
        }

        #region -------- Property: BPOCollectionValue --------
        private BPOCollection _BPOCollectionValue;
        public virtual BPOCollection BPOCollectionValue
        {
            get { return this._BPOCollectionValue; }
            set { this._BPOCollectionValue = value; }
        }
        #endregion [-------- BPOCollectionValue --------]


        #endregion [-------- BPOMemberInfos --------]
        public bool IsUnpacked { get; private set; }

        public void Unpack(IBPOInstanceContext context)
        {
            if (this.IsUnpacked)
                return;
            this.BPOValueType.Unpack(context);
            if (this.IsSimpleType || this.IsNullType)
            {
                this.IsUnpacked = true;
                this.RawValue = this.SimpleValue;
                return;
            }
            else if (this.IsCollectionType)
            {
                this.BPOCollectionValue.Unpack(context);
                this.RawValue = this.BPOCollectionValue.RawList;
                this.IsUnpacked = true;
            }
            else
            {
                var rawInstance = context.Instance(this.BPOValueType.RawType);
                this.RawValue = rawInstance;
                var selfType = this.BPOValueType.RawType;

                foreach (var complexValue in this.BPOComplexValues)
                {
                    if (complexValue.BPOMemberInfo.Depth == BPOPoliticsReadDepth.Inaccessibile)
                        continue;


                    complexValue.Unpack(context);

                    complexValue.UnloadInto(this.RawValue, this.BPOPolitics);

                }
                this.BPOComplexValues?.ForEach(item => item?.Unpack(context));

            }


            //TODO: Asignar al RawValue.Properties las propiedades almacenadas según las políticas del partido
        }




        #region ======== Pack ========
        public void Pack()
        {
            if (this.IsPacked)
                return;
            this.IsPacked = true;

            this.AlreadyPacked = null;
            this.RawValue = null;
            this.BPOCollectionValue?.Pack();
            this.BPOComplexValues?.ForEach(item => item?.Pack());
        }
        public bool IsPacked { get; set; }
        #endregion [======== Pack ========]



        public static List<BPOComplexValue> CreateComplexValue(Type tipo, object valor, IBPOPolitics politics, LoopDictionary alreadyPacked)
        {
            var listaPropiedades = BPOTypeCache.GenericCache.ReadType(tipo).Where(w => w.Depth <= politics.ReadDepth && w.BPOMemberType == politics.MemberType).ToList();
            List<BPOComplexValue> result = new List<BPOComplexValue>();

            foreach (var property in listaPropiedades)
            {
                BPOValue bpoValue = null;
                var rawValue = property.GetValue(valor);
                bool ap = false;
                if (rawValue != null)
                {
                    var uniqueName = BPOTools.GetObjectHashCode(rawValue);
                    if (ap = alreadyPacked.ContainsKey(uniqueName))
                        bpoValue = alreadyPacked[uniqueName];


                }
                if (!ap)
                    bpoValue = new BPOValue(rawValue, politics, alreadyPacked);

                result.Add(new BPOComplexValue
                {
                    BPOPolitics = politics,
                    BPOValue = bpoValue,
                    BPOMemberInfo = property
                });
            }

            return result;
        }
    }
}
