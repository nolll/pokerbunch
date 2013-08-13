<?php
namespace tests\AppTests\Homegame{

	use entities\Homegame;
	use tests\UnitTestCase;
	use app\Homegame\Listing\HomegameItemModel;
	use tests\TestHelper;

	class HomegameItemModelTests extends UnitTestCase {

		/** @var Homegame */
		private $homegame;

		function setUp(){
			$this->homegame = new Homegame();
		}

		function test_Item_SetsName(){
			$this->homegame->setDisplayName('a');

			$sut = new HomegameItemModel($this->homegame);

			$this->assertIdentical('a', $sut->name);
		}

		function test_Item_SetsDetailsUrl(){
			$sut = new HomegameItemModel($this->homegame);

			$this->assertIsA($sut->urlModel, 'app\Urls\HomegameDetailsUrlModel');
		}

	}

}