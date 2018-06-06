using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools
{
    internal interface IBCOutputStalkerManager : IBCOutputExtended
    {
        List<IBCOutputExtended> StalkingOutputs { get; set; }
    }
}
