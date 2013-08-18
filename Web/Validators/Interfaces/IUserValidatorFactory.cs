using Core.Classes;

namespace Web.Validators{

	public interface IUserValidatorFactory{

		Validator GetLoginValidator(User user);
        /*
        Validator GetAddUserValidator(User user);
        Validator GetEditUserValidator(User user);
        Validator GetChangePasswordValidator(string password, string repeatPassword);
        Validator GetForgotPasswordValidator(string email);
        */
	}

}