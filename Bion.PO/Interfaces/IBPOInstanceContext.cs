using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.PO
{
    public interface IBPOInstanceContext
    {
        T Instance<T>();
        object Instance(Type type);

        T Instance<T>(object[] args);
        object Instance(Type type, object[] args);
    }
}
