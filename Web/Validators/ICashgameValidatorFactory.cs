using Core.Classes;
using Web.Models.CashgameModels.Buyin;

namespace Web.Validators{

	public interface ICashgameValidatorFactory{

		IValidator GetAddCashgameValidator(Homegame homegame, Cashgame cashgame);
		//IValidator GetEditCashgameValidator(CashgameEditPostModel postModel);
		//IValidator GetBuyinValidator(BuyinModel postModel);
		//IValidator GetCashoutValidator(ActionPostModel postModel);
		//IValidator GetReportValidator(ActionPostModel postModel);

	}

}