<?php
namespace Infrastructure\Data\Interfaces {

	use Domain\Classes\User;

	interface UserStorage{

		/**
		 * @param string $email
		 * @return User
		 */
		public function getUserByEmail($email);

		/**
		 * @param string $userName
		 * @return User
		 */
		public function getUserByName($userName);

		/**
		 * @param string $token
		 * @return User
		 */
		public function getUserByToken($token);

		/**
		 * @param string $userNameOrEmail
		 * @param string $password
		 * @return User
		 */
		public function getUserByCredentials($userNameOrEmail, $password);

		/**
		 * @return User[]
		 */
		public function getUsers();

		/**
		 * @param User $user
		 * @return bool
		 */
		public function updateUser($user);

		/**
		 * @param User $user
		 * @return int
		 */
		public function addUser(User $user);

		/**
		 * @param User $user
		 * @return bool
		 */
		public function deleteUser(User $user);

		public function getSalt($userNameOrEmail);

		public function setSalt(User $user, $salt);

		public function setEncryptedPassword(User $user, $encryptedPassword);

		public function getToken(User $user);

		public function setToken(User $user, $token);

	}

}