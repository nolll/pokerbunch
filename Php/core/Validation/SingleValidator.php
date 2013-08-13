<?php
namespace core\Validation{

	abstract class SingleValidator extends BaseValidator{

		public function isValid(){
			$this->ensureErrorArray();
			$this->validate();
			return count($this->errors) == 0;
		}

		protected function isNullOrEmpty($str){
			return $str == null || is_string($str) && strlen($str) == 0;
		}

		protected function isEmail($email){
			return preg_match('/^[a-z0-9&\'\.\-_\+]+@[a-z0-9\-]+\.([a-z0-9\-]+\.)*+[a-z]{2}/is', $email) > 0;
		}

	}

}