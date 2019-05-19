using System;


namespace ParvazPardaz.Common.Extension
{
    public static class DateTimeExtention
    {
        public static string ToPersianString(this DateTime datetime, PersianDateTimeFormat format = PersianDateTimeFormat.Date)
        {
            return new PersianDateTime(datetime).ToString(format).GetPersianNumber();
        }
        public static string ToPersianTimeString(this DateTime datetime)
        {
            return new PersianDateTime(datetime).ToString("hh:mm").GetPersianNumber();
        }
        public static string ToPersianString(this DateTime? datetime, PersianDateTimeFormat format)
        {
            return datetime != null ? new PersianDateTime(datetime.Value).ToString(format).GetPersianNumber() : string.Empty;
        }
        public static string ToPersianTimeString(this DateTime? datetime)
        {
            return datetime != null ? new PersianDateTime(datetime.Value).ToString("hh:mm:ss tt").GetPersianNumber() : string.Empty;
        }
        public static string ToRemainingDateTime(this DateTime dateTime)
        {
            return RemainingDateTime.Calculate(dateTime).GetPersianNumber();
        }

        public static string GetTimeOfDay(this DateTime dateTime)
        {
            var hour = dateTime.Hour;
            if (hour >= 03 && hour <= 11)
            {
                return "صبح";
            }
            else if (hour >= 12 && hour <= 18)
            {
                return "بعدظهر";
            }
            else if (hour >= 19 && hour <= 23)
            {
                return "شب";
            }
            else
            {
                return "بامداد";
            }
        }

    }
}
