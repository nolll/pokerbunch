namespace tests\AppTests\Homegame{

	use tests\SharbatUnitTestCase;
	use app\Homegame\SlugGeneratorImpl;

	class SlugGeneratorTests extends SharbatUnitTestCase {

		function bind(){}

		function test_GetSlug_NameWithSpaces_RemovesSpaces(){
			$generator = new SlugGeneratorImpl();

			$actual = $generator->getSlug("name with spaces");

			$this->assertIdentical($actual, "namewithspaces");
		}

		function test_GetSlug_NameWithCaps_RemovesCaps(){
			$generator = new SlugGeneratorImpl("NameWithCaps");

			$actual = $generator->getSlug("NameWithCaps");

			$this->assertIdentical($actual, "namewithcaps");
		}

	}

}