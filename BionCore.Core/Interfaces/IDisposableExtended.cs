using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BionCore.Core
{
    /// <summary>
    /// Interfaz para gestionar los objetos deshechables
    /// </summary>
    public interface IDisposableExtended: IDisposable
    {
        /// <summary>
        /// Indica si el elemento ha sido deshechado
        /// </summary>
        bool IsDisposed { get; }
    }
}
