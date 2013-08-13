namespace integration\Avatar{

	use config\Settings;

	class GravatarService implements AvatarService{

		private $settings;

		public function __construct(Settings $settings){
			settings = $settings;
		}

		public function getSmallAvatarUrl($email){
			return getGravatarUrl($email, 40);
		}

		public function getLargeAvatarUrl($email){
			return getGravatarUrl($email, 100);
		}

		private function getGravatarUrl($email, $size){
			$gravatarUrl = 'http://www.gravatar.com/avatar/' .
							md5(strtolower(trim($email))) .
							'?s=' . $size .
							'&d=' . getDefaultImageUrl();
			return $gravatarUrl;
		}

		private function getDefaultImageUrl(){
			return settings.getSiteUrl() . '/core/ui/img/pix.gif';

		}

	}

}