<?php
namespace app\User{

	use Domain\Classes\User;
	use core\Validation\CompositeValidator;
	use Infrastructure\Data\Interfaces\UserStorage;
	use core\Validation\RequiredValidator;
	use core\Validation\NotNullValidator;
	use core\Validation\EmailValidator;
	use app\User\UniqueUserNameValidator;
	use app\User\ChangePassword\RepeatPasswordValidator;

	class UserValidatorFactoryImpl implements UserValidatorFactory{

		private $userStorage;

		public function __construct(UserStorage $userStorage){
			$this->userStorage = $userStorage;
		}

		public function getLoginValidator(User $user = null){
			$message = 'There was something wrong with your username or password. Please try again.';
			return new NotNullValidator($user, $message);
		}

		public function getAddUserValidator(User $user){
			$validator = new CompositeValidator();
			$validator = $this->buildUserValidator($validator, $user);
			$validator = $this->buildUniqueUserValidator($validator, $user);
			return $validator;
		}

		public function getEditUserValidator(User $user){
			$validator = new CompositeValidator();
			$validator = $this->buildUserValidator($validator, $user);
			return $validator;
		}

		public function getChangePasswordValidator($password, $repeatPassword){
			$validator = new CompositeValidator();
			$validator->addValidator(new RequiredValidator($password, 'Password can\'t be empty'));
			$validator->addValidator(new RepeatPasswordValidator($password, $repeatPassword, 'Password can\'t be empty'));
			return $validator;
		}

		public function getForgotPasswordValidator($email){
			$validator = new CompositeValidator();
			$validator->addValidator(new RequiredValidator($email, 'Email can\'t be empty'));
			$validator->addValidator(new EmailValidator($email, 'The email address is not valid'));
			return $validator;
		}

		private function buildUserValidator(CompositeValidator $validator, User $user){
			$validator->addValidator(new RequiredValidator($user->getUserName(), 'Login Name can\'t be empty'));
			$validator->addValidator(new RequiredValidator($user->getDisplayName(), 'Display Name can\'t be empty'));
			$validator->addValidator(new RequiredValidator($user->getEmail(), 'Email can\'t be empty'));
			$validator->addValidator(new EmailValidator($user->getEmail(), 'The email address is not valid'));
			return $validator;
		}

		private function buildUniqueUserValidator(CompositeValidator $validator, User $user){
			$validator->addValidator(new UniqueUserNameValidator($user->getUserName(), 'The user name is already in use', $this->userStorage));
			$validator->addValidator(new UniqueEmailValidator($user->getEmail(), 'The email is already in use', $this->userStorage));
			return $validator;
		}

	}

}