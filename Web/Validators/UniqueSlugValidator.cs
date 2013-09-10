using Infrastructure.Data.Storage.Interfaces;

namespace Web.Validators{

	public class UniqueSlugValidator : SimpleValidator
    {
	    private readonly IHomegameStorage _homegameStorage;

	    public UniqueSlugValidator(string subject, string message, IHomegameStorage homegameStorage)
            : base(subject, message)
		{
		    _homegameStorage = homegameStorage;
		}

	    protected override bool ValidateSubject(){
			var existingHomegame = _homegameStorage.GetHomegameByName(Subject);
			if(existingHomegame != null){
				AddError(Message);
				return false;
			}
			return true;
		}

	}

}