<?php
namespace app\Cashgame{

	use app\Cashgame\Action\ActionPostModel;
	use app\Cashgame\Action\BuyinPostModel;
	use core\Validation\Validator;
	use entities\Cashgame;
	use app\Cashgame\Edit\CashgameEditPostModel;
	use entities\Homegame;

	interface CashgameValidatorFactory{

		/**
		 * @param Homegame $homegame
		 * @param Cashgame $cashgame
		 * @return Validator
		 */
		public function getAddCashgameValidator(Homegame $homegame, Cashgame $cashgame);

		/**
		 * @param CashgameEditPostModel $postModel
		 * @return Validator
		 */
		public function getEditCashgameValidator(CashgameEditPostModel $postModel);

		/**
		 * @param BuyinPostModel $postModel
		 * @return Validator
		 */
		public function getBuyinValidator(BuyinPostModel $postModel);

		/**
		 * @param ActionPostModel $postModel
		 * @return Validator
		 */
		public function getCashoutValidator(ActionPostModel $postModel);

		/**
		 * @param ActionPostModel $postModel
		 * @return Validator
		 */
		public function getReportValidator(ActionPostModel $postModel);

	}

}