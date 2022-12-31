using System;

namespace Arselon.Utility
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string instance)
        {
            return instance.Length == 0;
        }

    }
}
