namespace integration\Avatar{

	use config\Settings;

	class GravatarService implements AvatarService{

		private $settings;

		public function __construct(Settings $settings){
			$this->settings = $settings;
		}

		public function getSmallAvatarUrl($email){
			return $this->getGravatarUrl($email, 40);
		}

		public function getLargeAvatarUrl($email){
			return $this->getGravatarUrl($email, 100);
		}

		private function getGravatarUrl($email, $size){
			$gravatarUrl = 'http://www.gravatar.com/avatar/' .
							md5(strtolower(trim($email))) .
							'?s=' . $size .
							'&d=' . $this->getDefaultImageUrl();
			return $gravatarUrl;
		}

		private function getDefaultImageUrl(){
			return $this->settings->getSiteUrl() . '/core/ui/img/pix.gif';

		}

	}

}