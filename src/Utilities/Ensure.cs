namespace Utilities
{
    using System;

    public static class Ensure
    {
        public static void IsPositiveInteger(int i, string parameterName)
        {
            if (i < 1)
            {
                throw new ArgumentException($"{parameterName} must be a positive integer");
            }
        }
    }
}
