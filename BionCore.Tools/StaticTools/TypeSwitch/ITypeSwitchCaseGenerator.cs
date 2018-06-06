
using System;

namespace BionCore.Tools
{
    namespace TypeSwitcher
    {
        public interface ITypeSwitchCaseGenerator
        {
            void Case<T>(Action @case);
            void Default(Action @default);
        }
    }
}
