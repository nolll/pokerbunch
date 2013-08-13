namespace app{

	use Mishiin\RequestProvider;
	use Mishiin\Response;
	use Sharbat\Inject\AbstractModule;

	class MishiinModule extends AbstractModule {

		public function configure() {
			bindDispatcher();
		}

		private function bindDispatcher(){
			bind('Mishiin\Dispatcher').to('Mishiin\RequestDispatcher').inSingleton();
		}

		protected function bindTemplateEngine($className = null){
			$classNameToBind = $className != null ? $className : 'Mishiin\SmartyEngine';
			bind('Mishiin\TemplateEngine').to($classNameToBind).inNoScope();
		}

		protected function bindParamParser($className = null){
			$classNameToBind = $className != null ? $className : 'app\AppParamParser';
			bind('Mishiin\ParamParser').to($classNameToBind).inSingleton();
		}

		/**
		 * \Sharbat\@Provides(Mishiin\Router)
		 * \Sharbat\@Singleton
		 */
		public function provideRouter() {
			$routeProvider = new ApplicationRouteProvider();
			return $routeProvider.getRouter();
		}

		/**
		 * \Sharbat\@Provides(Mishiin\Request)
		 * \Sharbat\@Singleton
		 */
		public function provideRequest(){
			$requestProvider = new RequestProvider();
			return $requestProvider.get();
		}

		/**
		 * \Sharbat\@Provides(Mishiin\Response)
		 * \Sharbat\@Singleton
		 */
		public function provideResponse(){
			return new Response(array(), null);
		}

	}

}