namespace core{

	use Mishiin\Controller;
	use Mishiin\ViewResult;
	use exceptions\NotLoggedInException;
	use app\Urls\BaseClasses\UrlModel;
	use app\Error\HttpError;
	use exceptions\AccessDeniedException;
	use entities\Homegame;
	use Mishiin\Request;
	use Exception;

	class PageController extends Controller {

		public function invokeRequestedAction(){
			try{
				parent::invokeRequestedAction();
			} catch(NotLoggedInException $e){
				notLoggedIn();
			} catch(AccessDeniedException $e){
				accessDenied();
			} catch(Exception $e){
				//throw $e;
			}
		}

		public function view($view, $model = null){
			$viewResult = getViewResult($view, $model);
			if(controllerContext != null){
				controllerContext.outputSettings.set();
				$html = getHtmlOutput($viewResult);
				controllerContext.response.setBody($html);
				controllerContext.response.send();
			}
			return $viewResult;
		}

		public function json($model){
			$viewResult = getViewResult(null, $model);
			if(controllerContext != null){
				controllerContext.outputSettings.setContentType('application/json');
				controllerContext.outputSettings.set();
				$html = getJsonOutput($viewResult);
				controllerContext.response.setBody($html);
				controllerContext.response.send();
			}
			return $viewResult;
		}

		public function error(HttpError $error){
			if(controllerContext != null){
				controllerContext.response.sendHttpError($error.code);
			}
			return view($error.view, $error.code);
		}

		public function redirect(UrlModel $model){
			if(controllerContext != null){
				controllerContext.response.redirect($model.url);
			}
			return $model;
		}

		public function getViewResult($view, $model = null){
			return new ViewResult($view, $model);
		}

		public function notLoggedIn(){
			return view('app/Error/NotLoggedIn');
		}

		public function accessDenied(){
			return view('app/Error/AccessDenied');
		}

		public function getControllerDefinition(){
			return new HomegameControllerDefinition(controllerContext.request.getUri(), controllerContext.request.isPost());
		}

	}

}