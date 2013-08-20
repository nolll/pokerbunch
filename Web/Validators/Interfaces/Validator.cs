using System.Collections.Generic;

namespace Web.Validators{

	public interface Validator{
	    bool IsValid { get; }
	    List<string> GetErrors();
	}

}