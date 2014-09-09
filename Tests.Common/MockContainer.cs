using System;
using System.Collections.Generic;
using Moq;

namespace Tests.Common
{
    public class MockContainer
    {
        private IDictionary<Type, Mock> _mocks;

        public MockContainer()
        {
            Clear();
        }

        public void Clear()
        {
            _mocks = new Dictionary<Type, Mock>();
        }

        public Mock<T> Get<T>() where T : class
        {
            Mock mock;
            var type = typeof(T);
            if (!_mocks.TryGetValue(type, out mock))
            {
                mock = new Mock<T>();
                _mocks.Add(type, mock);
            }
            return mock as Mock<T>;
        }
    }
}