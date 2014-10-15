using System;

namespace Core.Services.Interfaces
{
	public interface ITimeProvider
    {
	    DateTime UtcNow { get; }
    }
}