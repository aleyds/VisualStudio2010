using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Upgrade
{
    class Times
    {
        public Times()
        { 
        }

        public enum TimeStatus{
            _TIME_NORMAL,
            _TIME_UNDER,
            _TIME_OVERFLOW,
        };

        public UInt32 ToUnixTime(System.DateTime date)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (UInt32)(date - startTime).TotalSeconds;
        }

        public DateTime ToDateTime(String time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            long lTime = long.Parse(time + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return startTime.Add(toNow);
        }

        public TimeStatus Compare(DateTime start, DateTime end)
        {
            DateTime DateNow = System.DateTime.Now;
            if (DateTime.Compare(start, DateNow) > 0)
            {
                return TimeStatus._TIME_UNDER;
            }
            else if (DateTime.Compare(DateNow, end) > 0)
            {
                return TimeStatus._TIME_OVERFLOW;
            }
            else
            {
                return TimeStatus._TIME_NORMAL;
            }
        }

    }
}
