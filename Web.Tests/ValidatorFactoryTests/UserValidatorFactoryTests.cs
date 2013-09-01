using Core.Classes;
using NUnit.Framework;
using Tests.Common;
using Web.Validators;

namespace Web.Tests.ValidatorFactoryTests{

	class UserValidatorFactoryTests : MockContainer {

        [Test]
        public void GetLoginValidator_WithUser_IsValidIsTrue()
        {
			var user = new User();
			var sut = GetSut();
            var result = sut.GetLoginValidator(user);

			Assert.IsTrue(result.IsValid);
		}

		[Test]
        public void GetLoginValidator_WithoutUser_IsValidIsFalse()
        {
			var sut = GetSut();
            var result = sut.GetLoginValidator(null);

			Assert.IsFalse(result.IsValid);
		}

		private IUserValidatorFactory GetSut(){
            return new UserValidatorFactory(UserStorageMock.Object);
		}

	}

}