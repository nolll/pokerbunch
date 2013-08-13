namespace app{

	class Version {

		private $major = 4;
		private $minor = 4;
		private $build = 0;

		public function getVersion() {
			return sprintf('{0}.{1}.{2}', $this->major, $this->minor, $this->build);
		}

	}

}