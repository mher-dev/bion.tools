using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO.Test
{
    public class TestClassLists
    {
        #region -------- Property: ListInt --------
        private List<int> _ListInt;
        public virtual List<int> ListInt
        {
            get { return this._ListInt ?? (this._ListInt = new List<int>()); }
            set { this._ListInt = value; }
        }
        #endregion [-------- ListInt --------]

        #region -------- Property: ListDouble --------
        private List<double> _ListDouble;
        public virtual List<double> ListDouble
        {
            get { return this._ListDouble ?? (this._ListDouble = new List<double>()); }
            set { this._ListDouble = value; }
        }
        #endregion [-------- ListDouble --------]

        #region -------- Property: ListDecimal --------
        private List<decimal> _ListDecimal;
        public virtual List<decimal> ListDecimal
        {
            get { return this._ListDecimal ?? (this._ListDecimal = new List<decimal>()); }
            set { this._ListDecimal = value; }
        }
        #endregion [-------- ListDecimal --------]

        #region -------- Property: ListDateTime --------
        private List<DateTime> _ListDateTime;
        public virtual List<DateTime> ListDateTime
        {
            get { return this._ListDateTime ?? (this._ListDateTime = new List<DateTime>()); }
            set { this._ListDateTime = value; }
        }
        #endregion [-------- ListDateTime --------]

        #region -------- Property: ListString --------
        private List<string> _ListString;
        public virtual List<string> ListString
        {
            get { return this._ListString ?? (this._ListString = new List<string>()); }
            set { this._ListString = value; }
        }
        #endregion [-------- ListString --------]

        public bool Equals(TestClassLists item)
        {
            var result = !this.ListInt.SequenceEqual(item.ListInt);
            result = result || !this.ListDateTime.SequenceEqual(item.ListDateTime);
            result = result || !this.ListDouble.SequenceEqual(item.ListDouble);
            result = result || !this.ListDecimal.SequenceEqual(item.ListDecimal);

            result = result || !this.ListString.SequenceEqual(item.ListString);

            return !result;
        }

        public override bool Equals(object obj)
        {
            if (obj is TestClassLists)
                return this.Equals((TestClassLists)obj);
            return base.Equals(obj);
        }

    }
}
