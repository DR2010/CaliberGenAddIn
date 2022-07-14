using System;
using System.Globalization;

namespace CICSWeb.Net
{
    public static class Helper
    {
        readonly static string[] DATETIMEFORMATS = new string[] { "yyyy/MM/dd HH:mm:ss" };
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            if (o == null) return null;
            return evaluator(o);
        }
        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator,TResult failureValue)
            where TResult : class
            where TInput : class
        {
            return o != null ? evaluator(o) : failureValue;
        }
        public static TResult ReturnDb<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
            where TResult : class
            where TInput : class
        {
            return o != null ? o.GetType().Equals(typeof(System.DBNull)) ? failureValue : evaluator(o) : failureValue;
        }
        public static string CicsDateTimeToString(DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }

        /// <summary>
        /// Converts passed time to cics time format
        /// </summary>
        /// <param name="time">time</param>
        /// <returns>A string representing the cics time format</returns>
        public static string CicsTimeToString(DateTime time)
        {
            return time.ToString("HHmmss");
        }
        public static DateTime StringToDate(string date, string time)
        {
            return DateTime.ParseExact(string.Format("{0} {1}", date, time), DATETIMEFORMATS, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);
        }
        public static DateTime StringToDate(string datetime)
        {
            return DateTime.ParseExact(datetime, DATETIMEFORMATS, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces);
        }
    }
}
