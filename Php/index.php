use app\ApplicationModule;
use Mishiin\Application;
use Sharbat\Sharbat;

date_default_timezone_set('UTC');
ob_start();

define('__ROOT__', $_SERVER['DOCUMENT_ROOT']);

function importlib($file){
	require_once(__ROOT__ . '/../lib'. $file);
}

importlib('/AutoLoader/AutoLoader.php');

$autoLoader = new \AutoLoader();

$injector = Sharbat::createInjector(new ApplicationModule());

/** @var Application $application */
$application = $injector->getInstance('Mishiin\Application');
$application->execute();