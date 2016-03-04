using NUnit.Framework;

namespace Tests.Common
{
    public class ArrangeBase
    {
        protected Mocker Mocker { get; private set; }

        [SetUp]
        public void CreateMocker()
        {
            Mocker = new Mocker();
        }
    }
}