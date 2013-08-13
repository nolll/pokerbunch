namespace tests{

	use tests\SharbatUnitTestCase;
	use core\ClassNames;

	class ControllerUnitTestCase extends SharbatUnitTestCase {

		function bind(){
			bindFakeClass(ClassNames::$Request);
			bindFakeClass(ClassNames::$Response);
			bindFakeClass(ClassNames::$TemplateEngine);
		}

	}

}