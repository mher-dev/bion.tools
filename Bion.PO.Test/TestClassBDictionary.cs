using Bion.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO.Test
{
    public class TestClassBFriendlyDictionary
    {
        #region -------- Property: BFriendlyDictionaryInt --------
        private BFriendlyDictionary<int, int> _BFriendlyDictionaryInt;
        public virtual BFriendlyDictionary<int, int> BFriendlyDictionaryInt
        {
            get { return this._BFriendlyDictionaryInt ?? (this._BFriendlyDictionaryInt = new BFriendlyDictionary<int, int>()); }
            set { this._BFriendlyDictionaryInt = value; }
        }
        #endregion [-------- BFriendlyDictionaryInt --------]

        #region -------- Property: BFriendlyDictionaryDouble --------
        private BFriendlyDictionary<int, double> _BFriendlyDictionaryDouble;
        public virtual BFriendlyDictionary<int, double> BFriendlyDictionaryDouble
        {
            get { return this._BFriendlyDictionaryDouble ?? (this._BFriendlyDictionaryDouble = new BFriendlyDictionary<int, double>()); }
            set { this._BFriendlyDictionaryDouble = value; }
        }
        #endregion [-------- BFriendlyDictionaryDouble --------]

        #region -------- Property: BFriendlyDictionaryDecimal --------
        private BFriendlyDictionary<int, decimal> _BFriendlyDictionaryDecimal;
        public virtual BFriendlyDictionary<int, decimal> BFriendlyDictionaryDecimal
        {
            get { return this._BFriendlyDictionaryDecimal ?? (this._BFriendlyDictionaryDecimal = new BFriendlyDictionary<int, decimal>()); }
            set { this._BFriendlyDictionaryDecimal = value; }
        }
        #endregion [-------- BFriendlyDictionaryDecimal --------]

        #region -------- Property: BFriendlyDictionaryDateTime --------
        private BFriendlyDictionary<int, DateTime> _BFriendlyDictionaryDateTime;
        public virtual BFriendlyDictionary<int, DateTime> BFriendlyDictionaryDateTime
        {
            get { return this._BFriendlyDictionaryDateTime ?? (this._BFriendlyDictionaryDateTime = new BFriendlyDictionary<int, DateTime>()); }
            set { this._BFriendlyDictionaryDateTime = value; }
        }
        #endregion [-------- BFriendlyDictionaryDateTime --------]

        #region -------- Property: BFriendlyDictionaryString --------
        private BFriendlyDictionary<int, string> _BFriendlyDictionaryString;
        public virtual BFriendlyDictionary<int, string> BFriendlyDictionaryString
        {
            get { return this._BFriendlyDictionaryString ?? (this._BFriendlyDictionaryString = new BFriendlyDictionary<int, string>()); }
            set { this._BFriendlyDictionaryString = value; }
        }
        #endregion [-------- BFriendlyDictionaryString --------]

        #region -------- Property: BFriendlyDictionarySimple --------
        private BFriendlyDictionary<int, TestClassSimple> _BFriendlyDictionarySimple;
        public virtual BFriendlyDictionary<int, TestClassSimple> BFriendlyDictionarySimple
        {
            get { return this._BFriendlyDictionarySimple ?? (this._BFriendlyDictionarySimple = new BFriendlyDictionary<int, TestClassSimple>()); }
            set { this._BFriendlyDictionarySimple = value; }
        }
        #endregion [-------- BFriendlyDictionarySimple --------]
        public bool Equals(TestClassBFriendlyDictionary item)
        {

            var result = !this.BFriendlyDictionaryInt.SequenceEqual(item.BFriendlyDictionaryInt);
            result = result || !this.BFriendlyDictionaryDateTime.SequenceEqual(item.BFriendlyDictionaryDateTime);
            result = result || !this.BFriendlyDictionaryDouble.SequenceEqual(item.BFriendlyDictionaryDouble);
            result = result || !this.BFriendlyDictionaryDecimal.SequenceEqual(item.BFriendlyDictionaryDecimal);

            result = result || !this.BFriendlyDictionaryString.SequenceEqual(item.BFriendlyDictionaryString);

            return !result;
        }

        public override bool Equals(object obj)
        {
            if (obj is TestClassBFriendlyDictionary)
                return this.Equals((TestClassBFriendlyDictionary)obj);
            return base.Equals(obj);
        }

    }
}
