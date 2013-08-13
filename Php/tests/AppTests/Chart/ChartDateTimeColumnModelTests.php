namespace tests\AppTests\Chart{

	use app\Chart\ChartDateTimeColumnModel;
	use tests\UnitTestCase;

	class ChartDateTimeColumnModelTests extends UnitTestCase {

		private $label;
		private $pattern;

		function getSut(){
			return new ChartDateTimeColumnModel(label, pattern);
		}

		function test_Type_IsSetToDateTime(){
			label = 'any';
			pattern = 'any';

			$sut = getSut();

			assertEqual('datetime', $sut.type);
		}

	}

}