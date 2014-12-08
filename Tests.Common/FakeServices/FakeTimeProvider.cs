using System;
using Core.Services;

namespace Tests.Common.FakeServices
{
    public class FakeTimeProvider : ITimeProvider
    {
        public DateTime UtcNow
        {
            get { throw new NotImplementedException(); }
        }
    }
}