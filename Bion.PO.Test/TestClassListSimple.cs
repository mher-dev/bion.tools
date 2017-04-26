using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO.Test
{
    public class TestClassListSimple
    {
        #region -------- Property: SingleSimple --------
        private TestClassSimple _SingleSimple;
        public virtual TestClassSimple SingleSimple
        {
            get { return this._SingleSimple ?? (this._SingleSimple = new TestClassSimple()); }
            set { this._SingleSimple = value; }
        }
        #endregion [-------- SingleSimple --------]

        #region -------- Property: SinlgeList --------
        private TestClassLists _SinlgeList;
        public virtual TestClassLists SinlgeList
        {
            get { return this._SinlgeList ?? (this._SinlgeList = new TestClassLists()); }
            set { this._SinlgeList = value; }
        }
        #endregion [-------- SinlgeList --------]


        #region -------- Property: ListSimple --------
        private List<TestClassSimple> _ListSimple;
        public virtual List<TestClassSimple> ListSimple
        {
            get { return this._ListSimple ?? (this._ListSimple = new List<TestClassSimple>()); }
            set { this._ListSimple = value; }
        }
        #endregion [-------- ListSimple --------]

        #region -------- Property: ListList --------
        private List<TestClassLists> _ListList;
        public virtual List<TestClassLists> ListList
        {
            get { return this._ListList ?? (this._ListList = new List<TestClassLists>()); }
            set { this._ListList = value; }
        }
        #endregion [-------- ListList --------]

        public bool Equals(TestClassListSimple item)
        {
            bool result = false;
            result = result || !this.SingleSimple.Equals(item.SingleSimple);
            result = result || !this.SinlgeList.Equals(item.SinlgeList);
            result = result || !this.ListSimple.SequenceEqual(item.ListSimple);
            result = result || !this.ListList.SequenceEqual(item.ListList);

            return !result;
        }

        public override bool Equals(object obj)
        {
            if (obj is TestClassListSimple)
                return this.Equals((TestClassListSimple)obj);
            return base.Equals(obj);
        }
    }
}
