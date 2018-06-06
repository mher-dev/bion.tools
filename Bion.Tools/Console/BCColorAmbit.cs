using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools.Console
{
    public abstract class BCColorAmbit : IBCColorAmbit
    {
        public BCColorAmbit(IBCOutputExtended stream, IBCColorExtended color)
        {
            this.AmbitColor = color;
            this.OutputStream = stream;
            this.PreviousColor = this.GetStreamColor();
            this.ApplyColor();
        }

        #region ######## IBUID ########
        public int BUID { get; } = BionTools.GenerateBUID();
        #endregion [######## IBUID ########]

        #region ######## IBDisposable ########
        public virtual bool IsDisposed { get; private set; }

        public virtual void Dispose()
        {
            if (!this.IsDisposed)
            {
                this.IsDisposed = true;
                this.RestoreColor();
            }
        }
        #endregion [######## IBDisposable ########]


        public virtual IBCColorExtended AmbitColor { get; private set; }
        protected virtual IBCColorExtended PreviousColor { get; private set; }

        protected IBCOutputExtended OutputStream { get; private set; }


        protected abstract void ApplyColor();
        protected abstract void RestoreColor();

        protected abstract IBCColorExtended GetStreamColor();
    }
}
