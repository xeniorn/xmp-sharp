using System;
using System.Diagnostics;
using System.Reflection;
using SE.Halligang.CsXmpToolkit.PInvoke;

namespace SE.Halligang.CsXmpToolkit
{
	internal static class XmpDateTime
	{
		public static DateTimeOffset XmpStringToDateTimeOffset(string xmpDateTime)
		{
			PInvoke.XmpDateTime xmpdt;
			XmpUtils.ConvertToDate(xmpDateTime, out xmpdt);
			return XmpDateTimeToDateTime(xmpdt);
		}

		internal static DateTimeOffset XmpDateTimeToDateTime(PInvoke.XmpDateTime xmpDateTime)
		{
			return new DateTimeOffset(xmpDateTime.Year, xmpDateTime.Month, xmpDateTime.Day, xmpDateTime.Hour,
				xmpDateTime.Minute, xmpDateTime.Second, xmpDateTime.NanoSecond / 1_000_000, XmpDateTimeToUtcOffset(xmpDateTime));
		}

        private static TimeSpan XmpDateTimeToUtcOffset(PInvoke.XmpDateTime xmpDateTime)
        {
			int sign;
            switch (xmpDateTime.TZSign)
            {
                case TimeZoneSign.WestOfUtc:
                    sign = -1;
                    break;
                case TimeZoneSign.IsUtc:
                    sign = 0;
                    break;
                case TimeZoneSign.EastOfUtc:
                    sign = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return TimeSpan.FromMinutes(sign * (60 * xmpDateTime.TZHour + xmpDateTime.TZMinute));
        }

        public static string DateTimeToXmpString(DateTimeOffset dateTime)
        {
#if INTERNAL_LOGGING
			LogFile log = LogFile.GetInstance("CsXmpToolkit");
			log.AppendString(TraceLevel.Verbose, MethodInfo.GetCurrentMethod(), "Converting: " + dateTime.ToString("yyyy-MM-dd HH:mm:ss"));
#endif
            PInvoke.XmpDateTime xmpdt = DateTimeToXmpDateTime(dateTime);
            string value;
            XmpUtils.ConvertFromDate(xmpdt, out value);
#if INTERNAL_LOGGING
			log.AppendString(TraceLevel.Verbose, MethodInfo.GetCurrentMethod(), "Result: " + value);
#endif
            return value;
        }

        public static string DateTimeToXmpString(DateTime dateTime, TimeZone localTimeZone)
            => DateTimeToXmpString(new DateTimeOffset(dateTime, localTimeZone.GetUtcOffset(dateTime)));

        internal static PInvoke.XmpDateTime DateTimeToXmpDateTime(DateTime dateTime, TimeZone localTimeZone)
            => DateTimeToXmpDateTime(new DateTimeOffset(dateTime, localTimeZone.GetUtcOffset(dateTime)));

        internal static PInvoke.XmpDateTime DateTimeToXmpDateTime(DateTimeOffset dateTime)
        {
#if INTERNAL_LOGGING
			LogFile log = LogFile.GetInstance("CsXmpToolkit");
#endif
            TimeSpan offset = dateTime.Offset;
            PInvoke.TimeZoneSign sign = PInvoke.TimeZoneSign.IsUtc;
            if (offset.Hours < 0 || offset.Minutes < 0)
            {
                sign = PInvoke.TimeZoneSign.WestOfUtc;
            }
            else if (offset.Hours > 0 || offset.Minutes > 0)
            {
                sign = PInvoke.TimeZoneSign.EastOfUtc;
            }
#if INTERNAL_LOGGING
			log.AppendString(TraceLevel.Verbose, MethodInfo.GetCurrentMethod(), "Sign: " + sign.ToString());
			log.AppendString(TraceLevel.Verbose, MethodInfo.GetCurrentMethod(), "Offset: " + offset.Hours.ToString() + " hours, " + offset.Minutes.ToString() + " minutes");
#endif
            PInvoke.XmpDateTime xmpDateTime = new PInvoke.XmpDateTime
            {
                Year = dateTime.Year,
                Month = dateTime.Month,
                Day = dateTime.Day,
                Hour = dateTime.Hour,
                Minute = dateTime.Minute,
                Second = dateTime.Second,
                NanoSecond = 0, // dateTime.Millisecond;
                TZSign = sign,
                TZHour = Math.Abs(offset.Hours),
                TZMinute = Math.Abs(offset.Minutes),

                HasDate = true,
                HasTime = true,
                HasTimeZone = true
            };

            return xmpDateTime;
        }
    }
}
