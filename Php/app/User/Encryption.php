<?php
namespace app\User{

	interface Encryption {

		public function encrypt($string, $salt);

	}

}