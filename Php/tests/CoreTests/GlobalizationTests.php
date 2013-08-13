<?php
namespace tests\CoreTests{

	use tests\SharbatUnitTestCase;
	use core\Globalization;
	use entities\CurrencySettings;
	use DateTime;
	use tests\TestHelper;

	class GlobalizationTests extends SharbatUnitTestCase {

		function bind(){}

		function testFormatNumber(){
			$input = "1";
			$expected = "1";
			$formatted = Globalization::formatNumber($input);
			$this->assertIdentical($expected, $formatted);

			$input = "12";
			$expected = "12";
			$formatted = Globalization::formatNumber($input);
			$this->assertIdentical($expected, $formatted);

			$input = "123";
			$expected = "123";
			$formatted = Globalization::formatNumber($input);
			$this->assertIdentical($expected, $formatted);

			$input = "1234";
			$expected = "1 234";
			$formatted = Globalization::formatNumber($input);
			$this->assertIdentical($expected, $formatted);

			$input = "1234567890";
			$expected = "1 234 567 890";
			$formatted = Globalization::formatNumber($input);
			$this->assertIdentical($expected, $formatted);
		}

		function testFormatCurrency(){
			$currency = new CurrencySettings("kr", "{AMOUNT} {SYMBOL}");

			$input = "1";
			$expected = "1 kr";
			$formatted = Globalization::formatCurrency($currency, $input);
			$this->assertIdentical($expected, $formatted);

			$input = "1234";
			$expected = "1 234 kr";
			$formatted = Globalization::formatCurrency($currency, $input);
			$this->assertIdentical($expected, $formatted);
		}

		function testFormatWinrate(){
			$currency = new CurrencySettings("kr", "{AMOUNT} {SYMBOL}");

			$input = "1";
			$expected = "1 kr/h";
			$formatted = Globalization::formatWinrate($currency, $input);
			$this->assertIdentical($expected, $formatted);

			$input = "1234";
			$expected = "1 234 kr/h";
			$formatted = Globalization::formatWinrate($currency, $input);
			$this->assertIdentical($expected, $formatted);
		}

		function testFormatResult(){
			$currency = new CurrencySettings("kr", "{AMOUNT} {SYMBOL}");

			$input = "0";
			$expected = "0 kr";
			$formatted = Globalization::formatResult($currency, $input);
			$this->assertIdentical($expected, $formatted);

			$input = "1234";
			$expected = "+1 234 kr";
			$formatted = Globalization::formatResult($currency, $input);
			$this->assertIdentical($expected, $formatted);

			$input = "-1234";
			$expected = "-1 234 kr";
			$formatted = Globalization::formatResult($currency, $input);
			$this->assertIdentical($expected, $formatted);
		}

		function test_FormatDuration(){
			$minutes = 59;
			$string = "59m";
			$formatted = Globalization::formatDuration($minutes);
			$this->assertIdentical($string, $formatted);

			$minutes = 300;
			$string = "5h";
			$formatted = Globalization::formatDuration($minutes);
			$this->assertIdentical($string, $formatted);

			$minutes = 301;
			$string = "5h 1m";
			$formatted = Globalization::formatDuration($minutes);
			$this->assertIdentical($string, $formatted);
		}

		function test_FormatTimespan(){
			$seconds = 1;
			$formatted = Globalization::formatTimespan($seconds);
			$this->assertIdentical('now', $formatted);

			$seconds = 45;
			$formatted = Globalization::formatTimespan($seconds);
			$this->assertIdentical('1 minute', $formatted);

			$seconds = 120;
			$formatted = Globalization::formatTimespan($seconds);
			$this->assertIdentical('2 minutes', $formatted);
		}

		function testFormatShortDate(){
			$dateTime = new DateTime("2010-02-01");

			$str = "Feb 1";
			$formatted = Globalization::formatShortDate($dateTime);
			$this->assertIdentical($str, $formatted);

			$formatted = Globalization::formatShortDate($dateTime, false);
			$this->assertIdentical($str, $formatted);

			$str = "Feb 1 2010";
			$formatted = Globalization::formatShortDate($dateTime, true);
			$this->assertIdentical($str, $formatted);
		}

		function testFormatShortDateTime(){
			$dateTime = new DateTime("2010-02-01 12:28:35");

			$str = "Feb 1 12:28";
			$formatted = Globalization::formatShortDateTime($dateTime);
			$this->assertIdentical($str, $formatted);

			$formatted = Globalization::formatShortDateTime($dateTime, false);
			$this->assertIdentical($str, $formatted);

			$str = "Feb 1 2010 12:28";
			$formatted = Globalization::formatShortDateTime($dateTime, true);
			$this->assertIdentical($str, $formatted);
		}

		function testFormatIsoDate(){
			$dateTime = new DateTime("2010-02-01 12:28:35");

			$str = "2010-02-01";
			$formatted = Globalization::formatIsoDate($dateTime);
			$this->assertIdentical($str, $formatted);
		}

		function testFormatIsoDateTime(){
			$dateTime = new DateTime("2010-02-01 12:28:35");

			$str = "2010-02-01 12:28:35";
			$formatted = Globalization::formatIsoDateTime($dateTime);
			$this->assertIdentical($str, $formatted);
		}

		function testFormatYear(){
			$dateTime = new DateTime("2010-02-01");
			$str = "2010";
			$formatted = Globalization::formatYear($dateTime);
			$this->assertIdentical($str, $formatted);
		}

	}

}