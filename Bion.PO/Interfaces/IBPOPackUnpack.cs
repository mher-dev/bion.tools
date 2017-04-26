using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    public interface IBPOPackUnpack
    {
        void Unpack(IBPOInstanceContext context);
        bool IsUnpacked { get; }

        void Pack();
        bool IsPacked { get; }

        object RawValue { get; }
    }
}
