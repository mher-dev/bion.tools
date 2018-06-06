
using System;

namespace BionCore.Tools
{
    using System.Diagnostics;

    namespace TypeSwitcher
    {
        [DebuggerStepThrough]
        internal class TypeSwitchObjectCaseGenerator : ITypeSwitchObjectCaseGenerator
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
}
