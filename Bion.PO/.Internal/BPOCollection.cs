using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    using Core;
    using System.Reflection;
    using IGenericCollection = ICollection<object>;
    using LoopDictionary = ConcurrentDictionary<string, BPOValue>;

    [Serializable]
    internal class BPOCollection : IBPOPackUnpack
    {

        public BPOCollection(ICollection list, IBPOPolitics politics, LoopDictionary alreadyPacked)
        {
            this.AlreadyPacked = alreadyPacked;
            this.BPOPolitics = politics;
            this.RawList = list;
            this.BPOListType = BPOType.GetTypeFrom(list);
            this.UniqueName = BPOTools.GetObjectHashCode(list);
            this.MakePortableList();


        }
        private static bool IsKeyValuePair(object element)
        {
            if (element == null)
                return false;

            var eType = element.GetType();
            if (eType.GenericTypeArguments.Count() != 2)
                return false;

            var genericType = typeof(KeyValuePair<,>);
            var searchType = genericType.MakeGenericType(eType.GenericTypeArguments);
            var result = searchType.IsAssignableFrom(eType);

            return result;
        }
        private void MakePortableList()
        {
            foreach (var item in this.RawList)
            {

                IBPOPackUnpack bpoValue = null;

                bool ap = false;
                if (item != null)
                {
                    var itemName = BPOTools.GetObjectHashCode(item);
                    if (ap = this.AlreadyPacked.ContainsKey(itemName))
                        bpoValue = this.AlreadyPacked[itemName];
                }
                if (!ap)
                {
                    if (IsKeyValuePair(item))
                    {
                        bpoValue = new BPOKeyValuePair(item, this.BPOPolitics, this.AlreadyPacked);
                    }
                    else
                        bpoValue = new BPOValue(item, this.BPOPolitics, this.AlreadyPacked);
                }
                this.BPOItems.Add(bpoValue);
            }

        }

        #region ======== Unpack ========
        public bool IsUnpacked { get; private set; }

        public void Unpack(IBPOInstanceContext context)
        {
            if (this.IsUnpacked)
                return;

            this.IsUnpacked = true;
            this.BPOListType.Unpack(context);
            if (this.BPOListType.IsNullType)
            {
                this.RawList = null;
                return;
            }

            var collectionType = this.BPOListType.RawType.AsCollectionType();
            var method = collectionType.GetMethod(nameof(ICollection<object>.Add));
            this.RawList = context.Instance(this.BPOListType.RawType) as ICollection;
            if (this.RawList == null)
                throw new BArgumentNullException("No se ha podido instanciar el tipo `" + this.BPOListType.PortableName + "´");

            foreach (var bpoItem in this.BPOItems)
            {
                try
                {
                    bpoItem.Unpack(context);
                    method.Invoke(this.RawList, new object[] { bpoItem.RawValue });
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        #endregion [======== Unpack ========]

        #region ======== Pack ========
        public void Pack()
        {
            if (this.IsPacked)
                return;
            this.IsPacked = true;
            this.BPOItems?.ForEach(item => item.Pack());
            //Eliminamos los valores Raw
            this.RawList = null;
            this.AlreadyPacked = null;
        }
        public bool IsPacked { get; set; }
        #endregion [======== Pack ========]

        public LoopDictionary AlreadyPacked { get; set; }
        private string _UniqueName;
        public string UniqueName
        {
            get { return _UniqueName; }
            set { _UniqueName = value; }
        }
        public ICollection RawList { get; private set; }
        public object RawValue { get { return this.RawList; } }

        #region -------- Property: BPOPolitics --------
        private IBPOPolitics _BPOPolitics;
        public virtual IBPOPolitics BPOPolitics
        {
            get { return this._BPOPolitics; }
            set { this._BPOPolitics = value; }
        }
        #endregion [-------- BPOPolitics --------]

        #region -------- Property: BPOListType --------
        private BPOType _BPOListType;
        public virtual BPOType BPOListType
        {
            get { return this._BPOListType; }
            private set { this._BPOListType = value; }
        }
        #endregion [-------- BPOListType --------]


        #region -------- Property: BPOItems --------
        private List<IBPOPackUnpack> _BPOItems;
        public virtual List<IBPOPackUnpack> BPOItems
        {
            get { return this._BPOItems ?? (this._BPOItems = new List<IBPOPackUnpack>()); }
            set { this._BPOItems = value; }
        }


        #endregion [-------- BPOItems --------]


    }
}







//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bion.PO
//{
//    public class BPOList
//    {
//        public BPOList(IList list)
//        {
//            this.RawList = list;
//            this.BPOListType = BPOType.GetTypeFrom(list);

//        }

//        private void MakePortableList()
//        {
//            foreach (var item in this.RawList)
//            {
//                var bpoValue = new BPOValue(item);
//            }
//        }

//        public IList RawList { get; private set; }

//        #region -------- Property: BPOListType --------
//        private BPOType _BPOListType;
//        public virtual BPOType BPOListType
//        {
//            get { return this._BPOListType; }
//            private set { this._BPOListType = value; }
//        }
//        #endregion [-------- BPOListType --------]


//        #region -------- Property: BPOItems --------
//        private List<BPOValue> _BPOItems;
//        public virtual List<BPOValue> BPOItems
//        {
//            get { return this._BPOItems ?? (this._BPOItems = new List<BPOValue>()); }
//            set { this._BPOItems = value; }
//        }
//        #endregion [-------- BPOItems --------]

//    }
//}

