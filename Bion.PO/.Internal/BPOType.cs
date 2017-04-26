using Bion.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bion;
namespace Bion.PO
{
    [Serializable]
    internal class BPOType: IBPOPackUnpack
    {
        public static BPOType GetTypeFrom(object element)
        {
            if (element == null)
                return new BPOType(typeof(void));
            return new BPOType(element.GetType());
        }

        public BPOType(Type tipo)
        {
            this.RawType = tipo;
            this.PortableName = this.RawType.GetPortableName();
            if (tipo == typeof(void))
            {
                this.IsNullType = true;
                this.IsSimpleType = true;
            }
            else
            {
                this.IsSimpleType = tipo.IsPrimitveExtended();
                this.IsCollection = BPOTools.IsCollection(tipo);
                    
                    //!this.IsSimpleType && typeof(ICollection<>).IsGenericAssignableFrom(tipo);
            }
        }
        public Type RawType { get; private set; }
        public object RawValue { get { return this.RawType; } }
        #region -------- Property: PortableName --------
        private string _PortableName;
        public virtual string PortableName
        {
            get { return this._PortableName; }
            private set { this._PortableName = value; }
        }
        #endregion [-------- PortableName --------]


        public string Name { get { return this.RawType?.Name; } }




        #region -------- Property: IsNullType --------
        private bool _IsNullType;
        public virtual bool IsNullType
        {
            get { return this._IsNullType; }
            private set { this._IsNullType = value; }
        }
        #endregion [-------- IsNullType --------]

        #region -------- Property: IsSimpleType --------
        private bool _IsSimpleType;
        public virtual bool IsSimpleType
        {
            get { return this._IsSimpleType; }
            set { this._IsSimpleType = value; }
        }
        #endregion [-------- IsSimpleType --------]

        #region -------- Property: IsList --------
        private bool _IsList;
        public virtual bool IsCollection
        {
            get { return this._IsList; }
            private set { this._IsList = value; }
        }
        #endregion [-------- IsList --------]


        #region ======== Unpack ========
        public bool IsUnpacked { get; private set; }

        public void Unpack(IBPOInstanceContext context)
        {
            if (this.IsUnpacked)
                return;
            var type = Type.GetType(this.PortableName);
            if (type == null)
                throw new BUnpackException("No se ha podido restaur el tipo `" + this.PortableName + "´");
            this.IsUnpacked = true;
            this.RawType = type;
        }
        #endregion [======== Unpack ========]


        #region ======== Pack ========
        public void Pack()
        {
            if (this.IsPacked)
                return;
            this.IsPacked = true;
        }
        public bool IsPacked { get; set; }
        #endregion [======== Pack ========]

    }
}
