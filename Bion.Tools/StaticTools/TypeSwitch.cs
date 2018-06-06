
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bion.Tools
{
    using Bion.Tools.TypeSwitcher;
    using System.Diagnostics;

    namespace TypeSwitcher
    {
        public interface ITypeSwitchCaseGenerator
        {

            void Case<T>(Action<T> @case);
            void Default(Action<object> @default);
        }

        internal class TypeSwitchCaseAction
        {
            public Type Type { get; set; }
            public Delegate Action { get; set; }
        }


        [DebuggerStepThrough]
        internal class TypeSwitchCaseGenerator : ITypeSwitchCaseGenerator
        {

            #region -------- Property: TypeCases --------
            private BFriendlyDictionary<Type, Delegate> _TypeCases;
            public virtual BFriendlyDictionary<Type, Delegate> TypeCases
            {
                get { return this._TypeCases ?? (this._TypeCases = new BFriendlyDictionary<Type, Delegate>()); }
                set { this._TypeCases = value; }
            }
            #endregion [-------- TypeCases --------]

            public Delegate DefaultCase { get; set; }

            public void Case<T>(Action<T> @case)
            {
                var type = typeof(T);
                if (TypeCases.ContainsKey(type))
                    throw new BException<ArgumentException>("The switch statement contains multiple cases with the label value `" + type.FullName + "´");
                TypeCases[type] = @case;

            }

            public void Default(Action<object> @default)
            {
                if (this.DefaultCase != null)
                    throw new BException<ArgumentException>("The switch statement contains multiple cases with the label value `Default´");
                this.DefaultCase = @default;
            }



            public TypeSwitchCaseAction GetCase(Type type)
            {

                foreach (var tcase in this.TypeCases)
                {
                    if (tcase.Key.IsAssignableFrom(type))
                    {
                        return new TypeSwitchCaseAction()
                        {
                            Type = tcase.Key,
                            Action = tcase.Value,
                        };
                    }

                }

                if (this.DefaultCase != null)
                    return new TypeSwitchCaseAction() { Action = DefaultCase, Type = typeof(object) };
                return null;
            }
        }
    }

    [DebuggerNonUserCode, DebuggerStepThrough]
    public static class TypeSwitch
    {
        [DebuggerStepThrough]
        private static TypeSwitchCaseAction _ExecuteSwitch(Type type, Action<ITypeSwitchCaseGenerator> @switch)
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
                var castValue = Convert.ChangeType(@object, caseAction.Type);
                caseAction.Action.DynamicInvoke(castValue);
            }
        }

        [DebuggerNonUserCode]
        public static void For<T>(T @object, Action<ITypeSwitchCaseGenerator> @switch)
        {
            var type = @object?.GetType() ?? typeof(T);

            var caseAction = _ExecuteSwitch(type, @switch);
            _ExecuteCase(caseAction, @object);
        }

    }
}
