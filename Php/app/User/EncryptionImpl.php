<?php
namespace app\User{

	class EncryptionImpl implements Encryption{

		public function encrypt($str, $salt){
			return sha1($str . $salt);
		}

	}

}