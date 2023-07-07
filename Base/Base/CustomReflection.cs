using System.Reflection;

namespace wK_Manager.Base {
    public static class CustomReflection {
        public static bool ImplementsOrDerives(this Type @this, Type from) {
            if (from is null)
                return false;
            else if (!from.IsGenericType || !from.IsGenericTypeDefinition)
                return from.IsAssignableFrom(@this);
            else if (from.IsInterface) {
                foreach (Type @interface in @this.GetInterfaces())
                    if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == from)
                        return true;
            }

            return (@this.IsGenericType && @this.GetGenericTypeDefinition() == from)
                || (@this.BaseType?.ImplementsOrDerives(from) ?? false);
        }

        public static T? GetPropValue<T>(this object @instance, string propName)
            => (T?)@instance.GetType().GetProperty(propName)?.GetValue(@instance, null);

        public static bool IsNullable(this PropertyInfo propertyInfo) {
            Type propertyType = propertyInfo.PropertyType;

            return (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                || Nullable.GetUnderlyingType(propertyType) != null;
        }
    }
}
