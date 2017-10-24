using System;

namespace AdPortal.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToTimeSpan(this DateTime dateTime)
        {
            var epoch = new DateTime(1970,1,1,0,0,0);
            var time = dateTime.Subtract(new TimeSpan(epoch.Ticks));

            return time.Ticks/10000;
        }
    }
}