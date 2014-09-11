using System;

namespace Core.Entities
{
    public class Date : IComparable<Date>
    {
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int Year { get; private set; }

        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public Date(DateTime dateTime)
            : this(dateTime.Year, dateTime.Month, dateTime.Day)
        {
        }

        public string IsoString
        {
            get { return string.Format("{0}-{1}-{2}", Year.ToString("D4"), Month.ToString("D2"), Day.ToString("D2")); }
        }

        public DateTime UtcMidninght
        {
            get
            {
                return new DateTime(Year, Month, Day, 0, 0, 0, DateTimeKind.Utc);
            }
        }

        public static Date Parse(string s)
        {
            var d = DateTime.Parse(s);
            return new Date(d.Year, d.Month, d.Day);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Date;
            return other != null && Year == other.Year && Month == other.Month && Day == other.Day;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(Date other)
        {
            if (Equals(other))
                return 0;
            if (Year.CompareTo(other.Year) < 0)
                return -1;
            if (Month.CompareTo(other.Month) < 0)
                return -1;
            if (Day.CompareTo(other.Day) < 0)
                return -1;
            return 0;
        }
    }
}