using System;

namespace GameStore.Foundation
{
    public static class GeneralExtensions
    {
        public static void ShouldNotNull<T>(this T value, string paramName = "value")
            where T: class
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }
    }
}