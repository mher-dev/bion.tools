
using System;

namespace BionCore.Tools
{
    namespace TypeSwitcher
    {
        public interface ITypeSwitchObjectCaseGenerator
        {

            void Case<T>(Action<T> @case);
            void Default(Action<object> @default);
        }
    }
}
