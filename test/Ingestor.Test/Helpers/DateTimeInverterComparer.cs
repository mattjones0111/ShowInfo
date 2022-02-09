namespace Ingestor.Test.Helpers
{
    using System;
    using System.Collections;

    public class DateTimeInverterComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            DateTime cx = (DateTime)x;
            DateTime cy = (DateTime)y;

            int compare = cx.CompareTo(cy);

            if (compare > 0)
            {
                return -1;
            }

            if (compare < 0)
            {
                return 1;
            }

            return 0;
        }
    }
}