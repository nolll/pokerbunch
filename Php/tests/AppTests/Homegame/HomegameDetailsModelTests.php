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
			user = new User();
			homegame = new Homegame();
			isInManagerMode = false;
		}

		function test_ActionDetails_SetsDisplayName(){
			homegame.setDisplayName('a');

			$sut = getSut();

			assertIdentical('a', $sut.displayName);
		}

		function test_ActionDetails_SetsHouseRules(){
			homegame.setHouseRules('a');

			$sut = getSut();

			assertIdentical('a', $sut.houseRules);
		}

		function test_ActionDetails_HouseRulesWithLineBreaks_OutputsBrTags(){
			homegame.setHouseRules("a\n\nb");

			$sut = getSut();

			assertIdentical("a<br>\n<br>\nb", $sut.houseRules);
		}

		function test_ActionDetails_SetsEditUrl(){
			$sut = getSut();

			assertIsA($sut.editUrl, 'app\Urls\HomegameEditUrlModel');
		}

		function test_ActionDetails_SetsDescription(){
			homegame.setDescription('a');

			$sut = getSut();

			assertIdentical('a', $sut.description);
		}

		function test_ActionDetails_WithPlayerRights_DoesNotOutputEditLink(){
			$sut = getSut();

			assertFalse($sut.showEditLink);
		}

		function test_ActionDetails_WithManagerRights_ShowsEditLink(){
			isInManagerMode = true;

			$sut = getSut();

			assertTrue($sut.showEditLink);
		}

		private function getSut(){
			return new HomegameDetailsModel(user, homegame, isInManagerMode);
		}

	}

}