using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    internal interface IBCOutputStalkerManager : IBCOutputExtended
    {
        List<IBCOutputExtended> StalkingOutputs { get; set; }
    }
}
