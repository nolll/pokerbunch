﻿using System;

namespace Core.Entities
{
    public class Date
    {
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int Year { get; private set; }

        public Date(int year, int month, int day)
        {
            Month = month;
            Day = day;
            Year = year;
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
    }
}