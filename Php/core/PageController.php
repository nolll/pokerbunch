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
				$this->notLoggedIn();
			} catch(AccessDeniedException $e){
				$this->accessDenied();
			} catch(Exception $e){
				//throw $e;
			}
		}

		public function view($view, $model = null){
			$viewResult = $this->getViewResult($view, $model);
			if($this->controllerContext != null){
				$this->controllerContext->outputSettings->set();
				$html = $this->getHtmlOutput($viewResult);
				$this->controllerContext->response->setBody($html);
				$this->controllerContext->response->send();
			}
			return $viewResult;
		}

		public function json($model){
			$viewResult = $this->getViewResult(null, $model);
			if($this->controllerContext != null){
				$this->controllerContext->outputSettings->setContentType('application/json');
				$this->controllerContext->outputSettings->set();
				$html = $this->getJsonOutput($viewResult);
				$this->controllerContext->response->setBody($html);
				$this->controllerContext->response->send();
			}
			return $viewResult;
		}

		public function error(HttpError $error){
			if($this->controllerContext != null){
				$this->controllerContext->response->sendHttpError($error->code);
			}
			return $this->view($error->view, $error->code);
		}

		public function redirect(UrlModel $model){
			if($this->controllerContext != null){
				$this->controllerContext->response->redirect($model->url);
			}
			return $model;
		}

		public function getViewResult($view, $model = null){
			return new ViewResult($view, $model);
		}

		public function notLoggedIn(){
			return $this->view('app/Error/NotLoggedIn');
		}

		public function accessDenied(){
			return $this->view('app/Error/AccessDenied');
		}

		public function getControllerDefinition(){
			return new HomegameControllerDefinition($this->controllerContext->request->getUri(), $this->controllerContext->request->isPost());
		}

	}

}