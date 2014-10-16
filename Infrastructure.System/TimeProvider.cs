using System;
using Core.Services;

namespace Infrastructure.System
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}