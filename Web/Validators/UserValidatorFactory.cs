using Core.Classes;
using Infrastructure.Data.Storage.Interfaces;

namespace Web.Validators{

	public class UserValidatorFactory : IUserValidatorFactory
    {
	    private readonly IUserStorage _userStorage;

	    public UserValidatorFactory(IUserStorage userStorage)
	    {
	        _userStorage = userStorage;
	    }

		public Validator GetLoginValidator(User user)
		{
		    const string message = "There was something wrong with your username or password. Please try again.";
		    return new NotNullValidator(user, message);
		}

        //todo: implement the rest of these
        /*
	    public Validator GetAddUserValidator(User user){
			var validator = new CompositeValidator();
			validator = BuildUserValidator(validator, user);
			validator = buildUniqueUserValidator(validator, user);
			return validator;
		}

		public Validator GetEditUserValidator(User user){
			var validator = new CompositeValidator();
			validator = BuildUserValidator(validator, user);
			return validator;
		}

        public Validator GetChangePasswordValidator(string password, string repeatPassword){
			var validator = new CompositeValidator();
			validator.AddValidator(new RequiredValidator(password, "Password can't be empty"));
            validator.AddValidator(new RepeatPasswordValidator(password, repeatPassword, "Password can't be empty"));
			return validator;
		}

		public Validator GetForgotPasswordValidator(string email){
			var validator = new CompositeValidator();
            validator.AddValidator(new RequiredValidator(email, "Email can't be empty"));
            validator.AddValidator(new EmailValidator(email, "The email address is not valid"));
			return validator;
		}

        private CompositeValidator BuildUserValidator(CompositeValidator validator, User user)
        {
			validator.AddValidator(new RequiredValidator(user.UserName, "Login Name can't be empty"));
            validator.AddValidator(new RequiredValidator(user.DisplayName, "Display Name can't be empty"));
            validator.AddValidator(new RequiredValidator(user.Email, "Email can't be empty"));
            validator.AddValidator(new EmailValidator(user.Email, "The email address is not valid"));
			return validator;
		}

		private CompositeValidator buildUniqueUserValidator(CompositeValidator validator, User user){
            validator.AddValidator(new UniqueUserNameValidator(user.UserName, "The user name is already in use", userStorage));
            validator.AddValidator(new UniqueEmailValidator(user.Email, "The email is already in use", userStorage));
			return validator;
		}
        */

    }

}