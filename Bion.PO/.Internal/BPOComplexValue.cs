using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bion;
namespace Bion.PO
{
    [Serializable]
    internal class BPOComplexValue : IBPOPackUnpack
    {
        #region -------- Property: BPOMemberInfo --------
        private BPOMemberInfo _BPOMemberInfo;
        public virtual BPOMemberInfo BPOMemberInfo
        {
            get { return this._BPOMemberInfo; }
            set { this._BPOMemberInfo = value; }
        }
        #endregion [-------- BPOMemberInfo --------]

        #region -------- Property: BPOValue --------
        private BPOValue _BPOValue;
        public virtual BPOValue BPOValue
        {
            get { return this._BPOValue; }
            set { this._BPOValue = value; }
        }
        #endregion [-------- BPOValue --------]

        #region -------- Property: BPOPolitics --------
        private IBPOPolitics _BPOPolitics;
        public virtual IBPOPolitics BPOPolitics
        {
            get { return this._BPOPolitics; }
            set { this._BPOPolitics = value; }
        }
        #endregion [-------- BPOPolitics --------]

        #region ======== Unpack ========
        public bool IsUnpacked { get; private set; }

        public void Unpack(IBPOInstanceContext context)
        {
            if (this.IsUnpacked)
                return;

            this.BPOValue?.Unpack(context);
            this.BPOMemberInfo?.Unpack(context);
            this.IsUnpacked = true;
        }
        #endregion [======== Unpack ========]


        #region ======== Pack ========
        public void Pack()
        {
            if (this.IsPacked)
                return;
            this.BPOValue?.Pack();
            this.BPOMemberInfo?.Pack();
            this.IsPacked = true;
        }
        public bool IsPacked { get; set; }
        #endregion [======== Pack ========]

        [Obsolete("Propiedad no disponible", error: true)]
        public object RawValue { get { throw new NotSupportedException("Para los valores complejos, esta propiedad no esta disponible"); } }
        public void UnloadInto(object destination, IBPOPolitics politics)
        {
            var dType = destination.GetType();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            
            if (this.BPOMemberInfo.IsField)
            {
                var mInfo = dType.GetField(this.BPOMemberInfo.MemberName, flags);
                bool isOkay = mInfo.Attributes.ContainsAny(FieldAttributes.Public, FieldAttributes.Assembly);
                isOkay = isOkay || mInfo.Attributes.ContainsAny(FieldAttributes.Private) && politics.ReadDepth.ContainsAny(BPOPoliticsReadDepth.Private);

                if (!isOkay)
                    return;
                mInfo.SetValue(destination, this.BPOValue.RawValue);
            }
            else
            {
                var mInfo = dType.GetProperty(this.BPOMemberInfo.MemberName, flags);

                var mDepth = mInfo.GetMaximalDepth();

                if (mDepth > politics.ReadDepth)
                    return;

                //isOkay = isOkay || mInfo.Attributes.ContainsAny(FieldAttributes.Private) && politics.ReadDepth.ContainsAny(BPOPoliticsReadDepth.Private);

                //if (!isOkay)
                //    return;
                mInfo.SetValue(destination, this.BPOValue.RawValue);
            }
        }
    }
}
