using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools.Console
{
    internal class BCTextColorAmbit : BCColorAmbit
    {
        public BCTextColorAmbit(IBCOutputExtended stream, IBCColorExtended color) 
            : base(stream, color)
        {
        }

        protected override void ApplyColor()
        {
            this.OutputStream?.SetTextColor(this.AmbitColor);
        }

        protected override void RestoreColor()
        {
            this.OutputStream?.SetTextColor(this.PreviousColor);
        }

        protected override IBCColorExtended GetStreamColor()
        {
            return this.OutputStream?.GetTextColor();
        }
    }
}
