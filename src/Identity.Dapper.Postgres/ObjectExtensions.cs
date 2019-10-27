using System;

namespace Identity.Dapper.Postgres
{
    internal static class ObjectExtensions
    {
        internal static T ThrowIfNull<T>(this T @object, string paramName)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(paramName, $"Parameter {paramName} cannot be null.");
            }

            return @object;
        }
    }
}
