using System;
using System.Reflection;

namespace Codestellation.Quarks.Reflection
{
    public static class TypeExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider self, bool inherited = false)
            where TAttribute : Attribute
        {
            object[] customAttributes = self.GetCustomAttributes(typeof(TAttribute), inherited);

            if (customAttributes.Length > 0)
            {
                return (TAttribute)customAttributes[0];
            }
            return null;
        }
    }
}