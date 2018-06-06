
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    using BionCore.Tools.TypeSwitcher;
    using System.Diagnostics;


    [DebuggerNonUserCode, DebuggerStepThrough]
    public static class TypeSwitch
    {
        [DebuggerStepThrough]
        private static TypeSwitchCaseAction _ExecuteSwitch(Type type, Action<ITypeSwitchObjectCaseGenerator> @switch)
        {
            var caseGenerator = new TypeSwitchObjectCaseGenerator();

            @switch(caseGenerator);

            return caseGenerator.GetCase(type);
        }

        [DebuggerStepThrough]
        private static TypeSwitchCaseAction _ExecuteSwitch(Type type, Action<TypeSwitchCaseGenerator> @switch)
        {
            var caseGenerator = new TypeSwitchCaseGenerator();

            @switch(caseGenerator);

            return caseGenerator.GetCase(type);
        }

        [DebuggerStepThrough]
        private static void _ExecuteCase(TypeSwitchCaseAction caseAction, object @object)
        {
            if (caseAction != null)
            {
                if (caseAction.Action is Action)
                {
                    caseAction.Action.DynamicInvoke();
                }
                else
                {
                    var castValue = Convert.ChangeType(@object, caseAction.Type);
                    caseAction.Action.DynamicInvoke(castValue);
                }
            }
        }

        [DebuggerNonUserCode]
        public static void For<T>(T @object, Action<ITypeSwitchObjectCaseGenerator> @switch)
        {
            if (@object == null)
                throw new ArgumentNullException(nameof(@object));

            var type = @object.GetType();

            var caseAction = _ExecuteSwitch(type, @switch);
            _ExecuteCase(caseAction, @object);
        }
        [DebuggerNonUserCode]
        public static void For<T>(Action<ITypeSwitchCaseGenerator> @switch)
        {

            var type = typeof(T);

            var caseAction = _ExecuteSwitch(type, @switch);
            _ExecuteCase(caseAction, null);
        }
    }
}
