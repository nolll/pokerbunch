<?php
namespace tests\CoreTests{

	use tests\TestHelper;
	use tests\UnitTestCase;

	class GoogleAnalyticsControllerTests extends UnitTestCase {

		// These tests should be rewritten to test the model as soon as possible.
		// The Environment class dependency needs to be addressed

		function test_EnableAnalytics_SiteIsInProduction_EnableAnalyticsIsTrue(){
			/*
			$this->settings->returns("isInProduction", true);

			$viewResult = $this->controller->analytics();

			$this->assertTrue($viewResult->model->enableAnalytics);
			*/
		}

		function test_EnableAnalytics_SiteIsNotInProduction_EnableAnalyticsIsFalse(){
			/*
			$this->settings->returns("isInProduction", false);

			$viewResult = $this->controller->analytics();

			$this->assertFalse($viewResult->model->enableAnalytics);
			*/
		}

	}

}