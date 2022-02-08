namespace Api.Test.Helpers
{
    using System;

    public static class Any
    {
        static Random _rng = new();

        /// <summary>
        ///    Gets a DateTime a random number of days between a
        ///    given start and end year.
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public static DateTime DateTime(int startYear, int endYear)
        {
            if (startYear > endYear)
            {
                throw new ArgumentException($"{nameof(startYear)} must be before {nameof(endYear)}");
            }

            DateTime s = new DateTime(startYear, 1, 1);
            DateTime e = new DateTime(endYear, 12, 31);

            int days = (int)(e - s).TotalDays;

            return s.AddDays(_rng.Next(0, days));
        }
    }
}
