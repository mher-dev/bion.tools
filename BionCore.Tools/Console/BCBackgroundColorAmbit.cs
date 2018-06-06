using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools.Console
{
    internal class BCBackgroundColorAmbit : BCColorAmbit
    {
        public BCBackgroundColorAmbit(IBCOutputExtended stream, IBCColorExtended color) 
            : base(stream, color)
        {
        }

        protected override void ApplyColor()
        {
            this.OutputStream?.SetBackgroundColor(this.AmbitColor);
        }

        protected override void RestoreColor()
        {
            this.OutputStream?.SetBackgroundColor(this.PreviousColor);
        }

        protected override IBCColorExtended GetStreamColor()
        {
            return this.OutputStream?.GetBackgroundColor();
        }
    }
}
