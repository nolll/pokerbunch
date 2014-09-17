using System.Linq;
using Application.Exceptions;
using Application.Urls;
using Application.UseCases.ForgotPassword;
using NUnit.Framework;

namespace Tests.Application.UseCases
{
    class ForgotPasswordTests
    {
        [Test]
        public void ForgotPassword_WithValidEmail_ReturnUrlIsSet()
        {
            const string email = "a@b.com";
            var request = new ForgotPasswordRequest(email);

            var result = Execute(request);

            Assert.IsInstanceOf<ForgotPasswordConfirmationUrl>(result.ReturnUrl);
        }

        [Test]
        public void ForgotPassword_WithInvalidEmail_ValidationExceptionIsThrown()
        {
            const string email = "";
            var request = new ForgotPasswordRequest(email);

            var ex = Assert.Throws<ValidationException>(() => Execute(request));
            Assert.AreEqual(1, ex.Messages.Count());
        }

        private ForgotPasswordResult Execute(ForgotPasswordRequest request)
        {
            return ForgotPasswordInteractor.Execute(request);
        }
    }
}
