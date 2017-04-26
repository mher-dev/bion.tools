using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO.Test
{
    public class TestClassSimple
    {
        public double Double { get; set; }
        public int Int { get; set; }
        public decimal Decimal { get; set; }
        public string String { get; set; }
        public DateTime DateTime { get; set; }

        public bool Equals(TestClassSimple item)
        {
            var result = this.Double != item.Double;
            result = result || this.Int != item.Int;
            result = result || this.Decimal != item.Decimal;
            result = result || this.String != item.String;
            result = result || this.DateTime != item.DateTime;

            return !result;
        }

        public override bool Equals(object obj)
        {
            if (obj is TestClassSimple)
                return this.Equals((TestClassSimple)obj);
            return base.Equals(obj);
        }


    }
}
