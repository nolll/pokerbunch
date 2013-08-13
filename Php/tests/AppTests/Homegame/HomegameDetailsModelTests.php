namespace tests\AppTests\Homegame{

	use entities\Homegame;
	use Domain\Classes\User;
	use tests\UnitTestCase;
	use app\Homegame\Details\HomegameDetailsModel;

	class HomegameDetailsModelTests extends UnitTestCase {

		private $user;
		/** @var Homegame */
		private $homegame;
		private $isInManagerMode;

		function setUp(){
			$this->user = new User();
			$this->homegame = new Homegame();
			$this->isInManagerMode = false;
		}

		function test_ActionDetails_SetsDisplayName(){
			$this->homegame->setDisplayName('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->displayName);
		}

		function test_ActionDetails_SetsHouseRules(){
			$this->homegame->setHouseRules('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->houseRules);
		}

		function test_ActionDetails_HouseRulesWithLineBreaks_OutputsBrTags(){
			$this->homegame->setHouseRules("a\n\nb");

			$sut = $this->getSut();

			$this->assertIdentical("a<br>\n<br>\nb", $sut->houseRules);
		}

		function test_ActionDetails_SetsEditUrl(){
			$sut = $this->getSut();

			$this->assertIsA($sut->editUrl, 'app\Urls\HomegameEditUrlModel');
		}

		function test_ActionDetails_SetsDescription(){
			$this->homegame->setDescription('a');

			$sut = $this->getSut();

			$this->assertIdentical('a', $sut->description);
		}

		function test_ActionDetails_WithPlayerRights_DoesNotOutputEditLink(){
			$sut = $this->getSut();

			$this->assertFalse($sut->showEditLink);
		}

		function test_ActionDetails_WithManagerRights_ShowsEditLink(){
			$this->isInManagerMode = true;

			$sut = $this->getSut();

			$this->assertTrue($sut->showEditLink);
		}

		private function getSut(){
			return new HomegameDetailsModel($this->user, $this->homegame, $this->isInManagerMode);
		}

	}

}