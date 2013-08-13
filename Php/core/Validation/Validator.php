namespace core\Validation{

	interface Validator{

		function isValid();

		function getErrors();

	}

}