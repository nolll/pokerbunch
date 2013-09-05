using Core.Classes;

namespace Web.Validators{

	public class CashgameValidatorFactory : ICashgameValidatorFactory{

		public Validator GetAddCashgameValidator(Homegame homegame, Cashgame cashgame){
			var validator = new CompositeValidator();
			validator.AddValidator(new RequiredValidator(cashgame.Location, "Location can't be empty"));
			return validator;
		}

        /*
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
        */

	}

}