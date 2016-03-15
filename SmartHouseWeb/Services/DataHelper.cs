using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouseWeb.Services
{
    public static class DataHelper
    {
        public static long ToUnixTimestamp(this DateTime date)
        {
            return (long) date.ToUniversalTime()
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds;
        }
    }
}