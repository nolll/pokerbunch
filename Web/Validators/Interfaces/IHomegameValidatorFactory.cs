using Core.Classes;
using Web.Models.HomegameModels.Add;

namespace Web.Validators{

	public interface IHomegameValidatorFactory{

        IValidator GetAddHomegameValidator(HomegameAddModel model);
		//IValidator GetEditHomegameValidator(Homegame homegame);

	}

}