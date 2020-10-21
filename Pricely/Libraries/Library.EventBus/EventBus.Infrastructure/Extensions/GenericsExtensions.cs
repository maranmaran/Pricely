using System;
using System.Linq;

namespace EventBus.Infrastructure.Extensions
{
    public static class GenericsExtensions
    {
        /// <summary>
        /// Gets the name of the generic type through reflection.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Type name</returns>
        public static string GetGenericTypeName(this Type type)
        {
            var typeName = string.Empty;

            if (type.IsGenericType)
            {
                var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            }
            else
            {
                typeName = type.Name;
            }

            return typeName;
        }

        /// <summary>
        /// Gets the name of the generic object through reflection.
        /// </summary>
        /// <param name="object">The object.</param>
        /// <returns>Type name</returns>
        public static string GetGenericTypeName(this object @object)
        {
            return @object.GetType().GetGenericTypeName();
        }
    }
}
