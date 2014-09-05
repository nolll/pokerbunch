using Application.Services;
using Application.UseCases.TestEmail;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Tests.Application.UseCases
{
    class TestEmailTests : MockContainer
    {
        [Test]
        public void TestEmail_MessageIsSent()
        {
            Sut.Execute();

            GetMock<IMessageSender>().Verify(o => o.Send("henriks@gmail.com", It.IsAny<string>(), It.IsAny<string>()));
        }

        private TestEmailInteractor Sut
        {
            get { return new TestEmailInteractor(GetMock<IMessageSender>().Object); }
        }
    }
}
