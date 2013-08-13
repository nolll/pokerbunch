<?php
use tests\UnitTestSuite;
use tests\WebTestSuite;
use tests\IntegrationTestSuite;
use tests\TestTimer;

ini_set("error_reporting", E_ALL);
ini_set("display_errors", "On");
ini_set("display_startup_errors", "On");
ini_set('xdebug.collect_params', 4);
ini_set('max_execution_time', 300);

define("__ROOT__", $_SERVER["DOCUMENT_ROOT"]);

function importlib($file){
	require_once(__ROOT__ . '/../lib'. $file);
}

importlib('/AutoLoader/AutoLoader.php');
importlib('/SimpleTest/mock_objects.php');
importlib('/SimpleTest/simpletest.php');
importlib('/SimpleTest/reporter.php');
importlib('/SimpleCoverage/CoverageReporter.php');

function getTestSuite($runIntegration, $runWebTests, $title){
	if($runIntegration){
		return new IntegrationTestSuite($title);
	} else if($runWebTests){
		return new WebTestSuite($title);
	} else {
		return new UnitTestSuite($title);
	}
}

$autoLoader = new \AutoLoader();

$timer = new TestTimer();

$verbose = isset($_GET["verbose"]);

SimpleTest::ignore('UnitTestCase');
SimpleTest::ignore('tests\ControllerUnitTestCase');
SimpleTest::ignore('tests\PokerBunchControllerUnitTestCase');
SimpleTest::ignore('tests\PageControllerUnitTestCase');

$reporter = null;
$title = null;

$runCoverage = isset($_GET["coverage"]) && function_exists("xdebug_start_code_coverage");
$runIntegration = isset($_GET["integration"]);
$runWebTests = isset($_GET["web"]);
$showNavigation = isset($_GET["navigation"]);

$title = getTestTitle($runIntegration, $runWebTests);

function getTestTitle($runIntegration, $runWebTests){
	if($runIntegration){
		return "Integration Tests";
	} else if($runWebTests){
		return "Web Tests";
	} else {
		return "All Tests";
	}
}

$reporter = new CustomReporter();

if($runCoverage){
    $coverage = new Coverage(__ROOT__);
	$coverage->excludeFolder("/../lib");
	$coverage->excludeFolder("/tests");
	$coverage->start();
    $reporter = new CoverageReporter($coverage);
    $title .= " With Coverage";
}

$tests = getTestSuite($runIntegration, $runWebTests, $title);

if($showNavigation){
	$showFullNavigation = !$runCoverage;
	$tests->printNavigationHtml($runIntegration, $runWebTests, $runCoverage, $showFullNavigation);
} else {
	$tests->run($reporter);
}

echo('Test time: ' . round($timer->measure(), 2) . ' seconds');