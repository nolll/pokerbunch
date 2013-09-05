using Core.Classes;

namespace Web.Validators{

	public interface ICashgameValidatorFactory{

		Validator GetAddCashgameValidator(Homegame homegame, Cashgame cashgame);
		//Validator GetEditCashgameValidator(CashgameEditPostModel postModel);
		//Validator GetBuyinValidator(BuyinPostModel postModel);
		//Validator GetCashoutValidator(ActionPostModel postModel);
		//Validator GetReportValidator(ActionPostModel postModel);

	}

}