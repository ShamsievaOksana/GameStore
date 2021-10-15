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
        
        public static void ShouldBeGreaterThanZero(this int value, string paramName = "value")
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(paramName);
        }
    }
}