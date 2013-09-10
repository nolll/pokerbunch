using Core.Classes;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;
using Web.Models.HomegameModels.Add;

namespace Web.Validators{

	public class HomegameValidatorFactory : IHomegameValidatorFactory
    {
	    private readonly IHomegameStorage _homegameStorage;
	    private readonly ISlugGenerator _slugGenerator;

	    public HomegameValidatorFactory(IHomegameStorage homegameStorage, ISlugGenerator slugGenerator)
	    {
	        _homegameStorage = homegameStorage;
	        _slugGenerator = slugGenerator;
	    }

	    public IValidator GetAddHomegameValidator(HomegameAddModel model)
        {
			IValidator validator = new CompositeValidator();
			validator = BuildHomegameValidator(validator as CompositeValidator, model);
			validator = BuildUniqueHomegameValidator(validator as CompositeValidator, model);
			return validator;
		}

        /*
		public IValidator GetEditHomegameValidator(Homegame homegame){
			IValidator validator = new CompositeValidator();
			validator = BuildHomegameValidator(validator as CompositeValidator, homegame);
			return validator;
		}
        */

        private IValidator BuildHomegameValidator(CompositeValidator validator, HomegameAddModel model)
        {
			validator.AddValidator(new RequiredValidator(model.DisplayName, "Display Name can't be empty"));
			validator.AddValidator(new RequiredValidator(model.CurrencySymbol, "Currency Symbol can't be empty"));
			validator.AddValidator(new RequiredValidator(model.CurrencyLayoutSelectModel.Value, "Currency Layout can't be empty"));
			validator.AddValidator(new RequiredValidator(model.TimezoneSelectModel.Value, "Timezone can't be empty"));
			return validator;
		}

        private IValidator BuildUniqueHomegameValidator(CompositeValidator validator, HomegameAddModel model)
        {
            var slug = _slugGenerator.GetSlug(model.DisplayName);
			validator.AddValidator(new UniqueSlugValidator(slug, "The Homegame name is not available", _homegameStorage));
			return validator;
		}

	}

}