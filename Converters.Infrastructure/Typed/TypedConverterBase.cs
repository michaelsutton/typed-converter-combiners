using System;
using Converters.Infrastructure.Utils;

namespace Converters.Infrastructure.Typed
{
    public abstract class TypedConverterBase : ThisExtension
    {
        #region Helpers

        protected static bool IsAssignmentPossible(Type actualTargetType, Type declaredTargetType)
        {
            return ( actualTargetType.IsAssignableFrom(declaredTargetType) ||
                     declaredTargetType.IsAssignableFrom(actualTargetType) || 
                     actualTargetType == typeof(string)
                     );
        }

        #endregion

        #region Mismatch classes

        protected abstract class Mismatch
        {
            public abstract string GetDescription();
        }

        protected class TypeMismatch : Mismatch
        {
            public readonly object ActualValue;
            public readonly Type DeclaredType;

            public TypeMismatch(object actualValue, Type declaredType)
            {
                ActualValue = actualValue;
                DeclaredType = declaredType;
            }

            public override string GetDescription()
            {
                return string.Format(
                    "Expecting the {0} type. The passed value ({1}) is of the {2} type.",
                    DeclaredType,
                    ActualValue,
                    ActualValue.GetType()
                    );
            }
        }

        #endregion
    }
}