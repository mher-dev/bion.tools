using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools
{
    public interface IBObjectCaster
    {
        bool Cast(Type originType, Type destinationType, object originObject, out object destinationObject);
    }
}
