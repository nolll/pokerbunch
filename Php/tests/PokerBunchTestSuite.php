namespace tests{

	importlib('/SimpleTest/test_case.php');

	abstract class PokerBunchTestSuite extends \TestSuite {

		private $selectedTestCase;
		private $testCases;

		public function __construct($label = "All Tests") {
			testCases = array();
			if(isset($_GET["testcase"])){
				selectedTestCase = $_GET["testcase"];
				$label = selectedTestCase;
			} else {
				selectedTestCase = null;
			}
			parent::TestSuite($label);
			addTestCases();
			addFiles();
		}

		abstract function addTestCases();

		public function addTestCase($className){
			testCases[] = getFileName($className);
		}

		private function addTestCaseFile($fileName){
			testCases[] = $fileName;
		}

		protected function addTestCaseNamespace($namespace){
			$folderName = getFolderName($namespace);
			$files = getFileNames($folderName);
			foreach($files as $file){
				$fileName = makeRelativePath($file);
				addTestCaseFile($fileName);
			}
		}

		function getFileNames($folder){
			// create an array to hold directory list
			$items = array();
			// create a handler for the directory
			$handler = opendir($folder);
			// open directory and walk through the filenames
			while ($item = readdir($handler)) {
				// if file isn't this directory or its parent, add it to the results
				if ($item != "." && $item != "..") {
					$items[] = $item;
				}
			}
			closedir($handler);

			$files = array();
			foreach($items as $item){
				$itemPath = $folder . '/' . $item;
				if(is_dir($itemPath)){
					$subFiles = getFileNames($itemPath);
					$files = array_merge($files, $subFiles);
				} else {
					$files[] = $itemPath;
				}
			}
			return $files;
		}

		private function getFolderName($namespace){
			return __ROOT__ . '/' . str_replace('\\', '/', $namespace);
		}

		private function makeRelativePath($absolutePath){
			return str_replace(__ROOT__, '', $absolutePath);
		}

		private function getFileName($className){
			return '/' . str_replace('\\', '/', $className) . '.php';
		}

		public function printNavigationHtml($runIntegration, $runWebTests, $runCoverage, $fullNavigation = true){
			$root = getRoot($runIntegration, $runWebTests);
			$coverageParam = $runCoverage ? '?coverage&' : '?';
			$heading = getHeading($runIntegration, $runWebTests);
			echo("<h1>" . $heading . "</h1>");
			echo("<a href=\"/test/" . $root . $coverageParam . "\">All Tests</a><br/>");
			if($fullNavigation){
				foreach(testCases as $fileName){
					$navName = getNavigationName($fileName);
					$navUrl = $root . $coverageParam . "testcase=" . $fileName;
					echo("<a href=\"$navUrl\">$navName</a><br/>");
				}
			}
		}

		private function getRoot($runIntegration, $runWebTests){
			if($runIntegration){
				return 'int';
			} else if($runWebTests){
				return 'web';
			} else {
				return 'unit';
			}
		}

		private function getHeading($runIntegration, $runWebTests){
			if($runIntegration){
				return 'Integration Tests';
			} else if($runWebTests){
				return 'Web Tests';
			} else {
				return 'Unit Tests';
			}
		}

		private function getNavigationName($fileName){
			$rootFolderRemoved = removeRootNode($fileName);
			$extentionRemoved = removeExtention($rootFolderRemoved);
			$extendedSeparators = extendSeparators($extentionRemoved);
			return $extendedSeparators;
		}

		private function removeRootNode($fileName){
			return str_replace("/tests/", "", $fileName);
		}

		private function removeExtention($fileName){
			return str_replace(".php", "", $fileName);
		}

		private function extendSeparators($navName){
			return str_replace("/", " / ", $navName);
		}

		private function addFiles(){
			foreach(testCases as $fileName){
				if(selectedTestCase === null || selectedTestCase === $fileName){
					$path = __ROOT__ . $fileName;
					parent::addFile($path);
				}
			}
		}

	}

}