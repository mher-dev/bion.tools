using Bion.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    /// <summary>
    /// Información sobre un miembro de clase
    /// </summary>
    [Serializable]
    internal class BPOMemberInfo : IBPOPackUnpack
    {
        public BPOMemberInfo(BPOMemberInfo clone)
            : this(clone.RawMemberInfo)
        {

        }

        public BPOMemberInfo(MemberInfo memberInfo)
        {
            if (memberInfo == null)
                throw new ArgumentNullException(nameof(memberInfo));

            this.RawMemberInfo = memberInfo;
            this.MemberName = memberInfo.Name;
            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    this.BPOMemberType = BPOPoliticsMemberTypes.Field;
                    break;

                case MemberTypes.Property:
                    this.BPOMemberType = BPOPoliticsMemberTypes.Property;
                    break;

                default:
                    throw new BArgumentException("No se puede tramitar un miembro de tipo `" + memberInfo.MemberType + "´");
            }

            this.DetectMemberType();
            if (this.IsField)
                this.BPOMemberValueType = new BPOType(this.RawMemberInfo.As<FieldInfo>().FieldType);
            else
                this.BPOMemberValueType = new BPOType(this.RawMemberInfo.As<PropertyInfo>().PropertyType);

        }

        private void DetectMemberType()
        {
            if (this.IsField)
            {
                var cast = this.RawMemberInfo.As<FieldInfo>();


                if (cast.Attributes.ContainsAny(FieldAttributes.Public, FieldAttributes.Assembly))
                    this.Depth = BPOPoliticsReadDepth.Public;
                else if (cast.Attributes.ContainsAny(FieldAttributes.Family, FieldAttributes.FamANDAssem))
                    this.Depth = BPOPoliticsReadDepth.Protected;
                else if (cast.Attributes.ContainsAny(FieldAttributes.Private))
                    this.Depth = BPOPoliticsReadDepth.Private;
                else
                    throw new BArgumentException($"No se ha podido determinar `{nameof(BPOPoliticsReadDepth)}´ (`{cast.Attributes.As<int>()}´) para el miembro `{this.RawMemberInfo.Name}´ dentro del tipo `{this.RawMemberInfo.DeclaringType.GetPortableName()}´");
            }
            else
            {
                var propInfo = this.RawMemberInfo.As<PropertyInfo>();
                this.Depth = propInfo.GetMaximalDepth();

                //var cast1 = this.RawMemberInfo.As<PropertyInfo>().SetMethod;
                //var cast2 = this.RawMemberInfo.As<PropertyInfo>().GetMethod;

                //if (cast1 == null || cast2 == null)
                //{
                //    this.Depth = BPOPoliticsReadDepth.Inaccessibile;
                //    return;
                //}
                //if (cast1.Attributes.ContainsAny(MethodAttributes.Public, MethodAttributes.Assembly) && cast2.Attributes.ContainsAny(MethodAttributes.Public, MethodAttributes.Assembly))
                //    this.Depth = BPOPoliticsReadDepth.Public;
                //else if (cast1.Attributes.ContainsAny(MethodAttributes.Family, MethodAttributes.FamANDAssem) || cast2.Attributes.ContainsAny(MethodAttributes.Family, MethodAttributes.FamANDAssem))
                //    this.Depth = BPOPoliticsReadDepth.Protected;
                //else if (cast1.Attributes.ContainsAny(MethodAttributes.Private) || cast2.Attributes.ContainsAny(MethodAttributes.Private))
                //    this.Depth = BPOPoliticsReadDepth.Private;
                //else
                //    throw new BArgumentException($"No se ha podido determinar `{nameof(BPOPoliticsReadDepth)}´ para la  `{this.RawMemberInfo.Name}´ dentro del tipo `{this.RawMemberInfo.DeclaringType.GetPortableName()}´");
            }

        }

        #region -------- Property: BPOMemberType --------
        private BPOPoliticsMemberTypes? _BPOMemberType;
        public virtual BPOPoliticsMemberTypes? BPOMemberType
        {
            get { return this._BPOMemberType; }
            internal set { this._BPOMemberType = value; }
        }
        #endregion [-------- BPOMemberType --------]


        #region -------- Property: Depth --------
        private BPOPoliticsReadDepth? _Depth;
        public virtual BPOPoliticsReadDepth? Depth
        {
            get { return this._Depth; }
            internal set { this._Depth = value; }
        }
        #endregion [-------- Depth --------]


        #region -------- Property: BPOMemberValueType --------
        private BPOType _BPOMemberValueType;
        public virtual BPOType BPOMemberValueType
        {
            get { return this._BPOMemberValueType; }
            set { this._BPOMemberValueType = value; }
        }
        #endregion [-------- BPOMemberValueType --------]

        public Type MemberValueType
        {
            get
            {
                return this.BPOMemberValueType.RawType;

            }
        }
        public MemberInfo RawMemberInfo { get; set; }
        public object RawValue { get { return this.RawMemberInfo; } }
        /// <summary>
        /// Indica si el campo es un atributo
        /// </summary>
        public bool IsField
        {
            get
            {
                if (BPOMemberType.IsNull())
                    throw new BArgumentNullException("MemberType is no defined.", nameof(BPOMemberType));
                return BPOMemberType == BPOPoliticsMemberTypes.Field;
            }
        }

        /// <summary>
        /// Indica si el campo es una propiedad
        /// </summary>
        public bool IsProperty
        {
            get
            {
                if (BPOMemberType.IsNull())
                    throw new BArgumentNullException("MemberType is no defined.", nameof(BPOMemberType));
                return BPOMemberType == BPOPoliticsMemberTypes.Property;
            }
        }

        #region -------- Property: MemberName --------
        private string _MemberName;
        /// <summary>
        /// Nombre del miembro
        /// </summary>
        public virtual string MemberName
        {
            get { return this._MemberName; }
            set { this._MemberName = value; }
        }
        #endregion [-------- MemberName --------]

        public object GetValue(object obj)
        {
            if (this.IsField)
                return this.RawMemberInfo.As<FieldInfo>().GetValue(obj);
            else
                return this.RawMemberInfo.As<PropertyInfo>().GetValue(obj);
        }


        #region ======== Unpack ========
        public bool IsUnpacked { get; private set; }

        public void Unpack(IBPOInstanceContext context)
        {
            if (this.IsUnpacked)
                return;
            this.IsUnpacked = true;
            this.BPOMemberValueType.Unpack(context);
        }
        #endregion [======== Unpack ========]


        #region ======== Pack ========
        public void Pack()
        {
            if (this.IsPacked)
                return;
            this.IsPacked = true;
            //Eliminamos los valores Raw
            //this.RawMemberInfo = null;
        }
        public bool IsPacked { get; set; }
        #endregion [======== Pack ========]

    }
}
