
using System;

namespace BionCore.Tools
{
    namespace TypeSwitcher
    {
        internal class TypeSwitchCaseAction
        {
            public Type Type { get; set; }
            public Delegate Action { get; set; }
        }
    }
}
