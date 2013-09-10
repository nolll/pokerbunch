using System.Collections.Generic;

namespace Web.Validators{

	public interface IValidator{
	    bool IsValid { get; }
	    List<string> GetErrors();
	}

}