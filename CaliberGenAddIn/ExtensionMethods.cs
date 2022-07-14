using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAAddIn
{

    public static class ExtensionMethods
    {
        public static string ToFormattedString(this List<string> list)
        {
            var toString = string.Empty;

            if (list.Count == 0) return toString;

            toString = list[0];

            for (int i = 1; i < list.Count; i++)
            {
                toString += ", " + list[i];
            }

            return toString;
        }
        public static DateTime NextWeekDay(this DateTime date)
        {
            while (date.AddDays(1).DayOfWeek == DayOfWeek.Sunday || date.AddDays(1).DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(1);
            }

            return date.AddDays(1);
        }
        public static DateTime PreviousWeekDay(this DateTime date)
        {
            while (date.AddDays(-1).DayOfWeek == DayOfWeek.Sunday || date.AddDays(-1).DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(-1);
            }

            return date.AddDays(-1);
        }
    }
}
