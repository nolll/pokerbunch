namespace tests\AppTests\Chart{

	use app\Chart\ChartColumnModel;
	use tests\UnitTestCase;

	class ChartColumnModelTests extends UnitTestCase {

		private $type;
		private $label;
		private $pattern;

		function getSut(){
			return new ChartColumnModel(type, label, pattern);
		}

		function test_Type_IsSet(){
			type = 'a';

			$sut = getSut();

			assertEqual('a', $sut.type);
		}

		function test_Label_IsSet(){
			label = 'a';

			$sut = getSut();

			assertEqual('a', $sut.label);
		}

		function test_Pattern_IsSet(){
			pattern = 'a';

			$sut = getSut();

			assertEqual('a', $sut.pattern);
		}

		function test_Pattern_AcceptsNull(){
			pattern = null;

			$sut = getSut();

			assertNull($sut.pattern);
		}

	}

}