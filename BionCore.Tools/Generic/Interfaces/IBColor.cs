using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    public interface IBColor: IBUID
    {
        string Name { get; }
        string ToRGB();
    }
}
