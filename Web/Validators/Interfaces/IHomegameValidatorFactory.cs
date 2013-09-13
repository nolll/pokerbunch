using Core.Classes;
using Web.Models.HomegameModels.Add;

namespace Web.Validators{

	public interface IHomegameValidatorFactory{

        IValidator GetAddHomegameValidator(AddHomegamePageModel model);
		//IValidator GetEditHomegameValidator(Homegame homegame);

	}

}