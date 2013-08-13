<?php
namespace integration\Sharing{

	interface SocialServiceFactory {

		/**
		 * @param string $provider
		 * @return SocialService
		 */
		public function makeSocialService($provider);

	}

}