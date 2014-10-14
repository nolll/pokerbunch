using System;
using Core.Services.Interfaces;

namespace Infrastructure.System
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
	}
}