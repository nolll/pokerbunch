using System;

namespace Core.Entities
{
    public class Time : IComparable<Time>
    {
        public TimeSpan TimeSpan { get; set; }

        public static Time FromMinutes(int minutes)
        {
            return new Time(TimeSpan.FromMinutes(minutes));
        }

        protected Time(TimeSpan timeSpan)
        {
            TimeSpan = timeSpan;
        }

        public int Minutes
        {
            get { return Convert.ToInt32(Math.Round(TimeSpan.TotalMinutes)); }
        }

        public int CompareTo(Time other)
        {
            return TimeSpan.CompareTo(other.TimeSpan);
        }

        public override string ToString()
        {
            var minutes = Minutes;
            var h = (int)Math.Floor((double)minutes / 60);
            var m = minutes % 60;
            if (h > 0 && m > 0)
            {
                return h + "h " + m + "m";
            }
            if (h > 0)
            {
                return h + "h";
            }
            return m + "m";
        }
    }
}