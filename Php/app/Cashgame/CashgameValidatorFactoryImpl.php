namespace app\Cashgame{

	use app\Cashgame\Action\ActionPostModel;
	use app\Cashgame\Action\BuyinPostModel;
	use core\Validation\PositiveNumberValidator;
	use entities\Cashgame;
	use app\Cashgame\Edit\CashgameEditPostModel;
	use core\Validation\CompositeValidator;
	use app\Cashgame\CashgameValidatorFactory;
	use core\Validation\RequiredValidator;
	use entities\Homegame;

	class CashgameValidatorFactoryImpl implements CashgameValidatorFactory{

		public function getAddCashgameValidator(Homegame $homegame, Cashgame $cashgame){
			$validator = new CompositeValidator();
			$validator.addValidator(new RequiredValidator($cashgame.getLocation(), 'Location can\'t be empty'));
			return $validator;
		}

		public function getEditCashgameValidator(CashgameEditPostModel $postModel){
			$validator = new CompositeValidator();
			$validator.addValidator(new RequiredValidator($postModel.location, 'Location can\'t be empty'));
			return $validator;
		}

		public function getBuyinValidator(BuyinPostModel $postModel){
			$validator = new CompositeValidator();
			$validator.addValidator(new RequiredValidator($postModel.amount, "Amount can't be empty"));
			$validator.addValidator(new PositiveNumberValidator($postModel.amount, 'Amount needs to be positive'));
			$validator.addValidator(new PositiveNumberValidator($postModel.stack, "Stack size can't be negative"));
			return $validator;
		}

		public function getCashoutValidator(ActionPostModel $postModel){
			$validator = new CompositeValidator();
			$validator.addValidator(new RequiredValidator($postModel.stack, "Stack can't be empty"));
			$validator.addValidator(new PositiveNumberValidator($postModel.stack, "Stack size can't be negative"));
			return $validator;
		}

		public function getReportValidator(ActionPostModel $postModel){
			$validator = new CompositeValidator();
			$validator.addValidator(new RequiredValidator($postModel.stack, "Stack can't be empty"));
			$validator.addValidator(new PositiveNumberValidator($postModel.stack, "Stack size can't be negative"));
			return $validator;
		}

	}

}