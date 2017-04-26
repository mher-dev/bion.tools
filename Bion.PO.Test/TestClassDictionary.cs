using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO.Test
{
    public class TestClassDictionary
    {
        #region -------- Property: DictionaryInt --------
        private Dictionary<int, int> _DictionaryInt;
        public virtual Dictionary<int, int> DictionaryInt
        {
            get { return this._DictionaryInt ?? (this._DictionaryInt = new Dictionary<int, int>()); }
            set { this._DictionaryInt = value; }
        }
        #endregion [-------- DictionaryInt --------]

        #region -------- Property: DictionaryDouble --------
        private Dictionary<int, double> _DictionaryDouble;
        public virtual Dictionary<int, double> DictionaryDouble
        {
            get { return this._DictionaryDouble ?? (this._DictionaryDouble = new Dictionary<int, double>()); }
            set { this._DictionaryDouble = value; }
        }
        #endregion [-------- DictionaryDouble --------]

        #region -------- Property: DictionaryDecimal --------
        private Dictionary<int, decimal> _DictionaryDecimal;
        public virtual Dictionary<int, decimal> DictionaryDecimal
        {
            get { return this._DictionaryDecimal ?? (this._DictionaryDecimal = new Dictionary<int, decimal>()); }
            set { this._DictionaryDecimal = value; }
        }
        #endregion [-------- DictionaryDecimal --------]

        #region -------- Property: DictionaryDateTime --------
        private Dictionary<int, DateTime> _DictionaryDateTime;
        public virtual Dictionary<int, DateTime> DictionaryDateTime
        {
            get { return this._DictionaryDateTime ?? (this._DictionaryDateTime = new Dictionary<int, DateTime>()); }
            set { this._DictionaryDateTime = value; }
        }
        #endregion [-------- DictionaryDateTime --------]

        #region -------- Property: DictionaryString --------
        private Dictionary<int, string> _DictionaryString;
        public virtual Dictionary<int, string> DictionaryString
        {
            get { return this._DictionaryString ?? (this._DictionaryString = new Dictionary<int, string>()); }
            set { this._DictionaryString = value; }
        }
        #endregion [-------- DictionaryString --------]


        #region -------- Property: DictionarySimple --------
        private Dictionary<int, TestClassSimple> _DictionarySimple;
        public virtual Dictionary<int, TestClassSimple> DictionarySimple
        {
            get { return this._DictionarySimple ?? (this._DictionarySimple = new Dictionary<int, TestClassSimple>()); }
            set { this._DictionarySimple = value; }
        }
        #endregion [-------- DictionarySimple --------]


        public bool Equals(TestClassDictionary item)
        {

            var result = !this.DictionaryInt.SequenceEqual(item.DictionaryInt);
            result = result || !this.DictionaryDateTime.SequenceEqual(item.DictionaryDateTime);
            result = result || !this.DictionaryDouble.SequenceEqual(item.DictionaryDouble);
            result = result || !this.DictionaryDecimal.SequenceEqual(item.DictionaryDecimal);
            result = result || !this.DictionarySimple.SequenceEqual(item.DictionarySimple);

            result = result || !this.DictionaryString.SequenceEqual(item.DictionaryString);

            return !result;
        }

        public override bool Equals(object obj)
        {
            if (obj is TestClassDictionary)
                return this.Equals((TestClassDictionary)obj);
            return base.Equals(obj);
        }

    }
}
