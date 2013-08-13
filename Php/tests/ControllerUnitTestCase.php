namespace tests{

	use tests\SharbatUnitTestCase;
	use core\ClassNames;

	class ControllerUnitTestCase extends SharbatUnitTestCase {

		function bind(){
			$this->bindFakeClass(ClassNames::$Request);
			$this->bindFakeClass(ClassNames::$Response);
			$this->bindFakeClass(ClassNames::$TemplateEngine);
		}

	}

}