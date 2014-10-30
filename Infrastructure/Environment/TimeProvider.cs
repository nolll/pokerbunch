using System;
using Core.Services;

namespace Infrastructure.Environment
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}