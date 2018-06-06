using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    /// <summary>
    /// Indica un objeto IDisposable con bandera de IsDisposed
    /// </summary>
    public interface IBDisposable: IDisposable
    {
        bool IsDisposed { get; }
    }
}
