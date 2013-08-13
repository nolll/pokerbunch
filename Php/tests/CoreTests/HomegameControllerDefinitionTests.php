namespace tests{

	use core\HomegameControllerDefinition;

	class HomegameControllerDefinitionTests extends SharbatUnitTestCase {

		function bind(){}

		function test_Constructor_EmptyUrl_SetsHomeAndIndex(){
			$url = "";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("home", $def.getName());
			assertIdentical("action_index", $def.getAction());
			assertIdentical(0, count($def.getParams()));
		}

		function test_Constructor_WithoutHomegame_SetsHomeAndIndex(){
			$url = "-";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("home", $def.getName());
			assertIdentical("action_index", $def.getAction());
			assertIdentical(0, count($def.getParams()));
		}

		function test_Constructor_WithoutHomegameWithLeadingSlash_SetsHomeAndIndex(){
			$url = "/-";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("home", $def.getName());
			assertIdentical("action_index", $def.getAction());
			assertIdentical(0, count($def.getParams()));
		}

		function test_Constructor_WithoutHomegameWithTrailingSlash_SetsHomeAndIndex(){
			$url = "-/";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("home", $def.getName());
			assertIdentical("action_index", $def.getAction());
			assertIdentical(0, count($def.getParams()));
		}

		function test_Constructor_WithHomegame_SetsHomegameControllerAndDetailsAndHomegameParam(){
			$url = "homegame";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("game", $def.getName());
			assertIdentical("action_details", $def.getAction());
			assertIdentical(1, count($def.getParams()));
		}

		function test_Constructor_WithHomegameAndController_SetsControllerAndIndexAndHomegameParam(){
			$url = "homegame/controller";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("controller", $def.getName());
			assertIdentical("action_index", $def.getAction());
			assertIdentical(1, count($def.getParams()));
		}

		function test_Constructor_WithControllerAndAction_SetsControllerAndActionAndHomegameParam(){
			$url = "homegame/controller/anyaction";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("controller", $def.getName());
			assertIdentical("action_anyaction", $def.getAction());
			assertIdentical(1, count($def.getParams()));
		}

		function test_Constructor_WithControllerActionAndOneParam_SetsControllerAndIndexAndHomegameParamAndOneExtraParam(){
			$url = "homegame/controller/anyaction/param1";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("controller", $def.getName());
			assertIdentical("action_anyaction", $def.getAction());
			assertIdentical(2, count($def.getParams()));
		}

		function test_Constructor_WithControllerActionAndTwoParams_SetsControllerAndIndexAndHomegameParamAndTwoExtraParams(){
			$url = "homegame/controller/anyaction/param1/param2";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("controller", $def.getName());
			assertIdentical("action_anyaction", $def.getAction());
			assertIdentical(3, count($def.getParams()));
		}

		function test_Constructor_WithControllerActionAndThreeParam_SetsControllerAndIndexAndHomegameParamAndThreeExtraParams(){
			$url = "homegame/controller/anyaction/param/param2/param3";
			$def = new HomegameControllerDefinition($url);
			assertIdentical("controller", $def.getName());
			assertIdentical("action_anyaction", $def.getAction());
			assertIdentical(4, count($def.getParams()));
		}

	}

}