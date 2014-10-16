using System;

namespace Core.Services
{
	public interface ITimeProvider
    {
	    DateTime UtcNow { get; }
    }
}