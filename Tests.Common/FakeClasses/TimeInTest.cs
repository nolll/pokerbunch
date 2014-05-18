using System;
using System.Globalization;
using Core.Entities;

namespace Tests.Common.FakeClasses
{
    public class TimeInTest : Time
    {
        public TimeInTest(TimeSpan timeSpan = default(TimeSpan)) : base(timeSpan)
        {
        }

        public override string ToString()
        {
            return Minutes.ToString(CultureInfo.InvariantCulture);
        }
    }
}