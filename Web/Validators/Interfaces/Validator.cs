using System.Collections.Generic;

namespace Web.Validators{

	public interface Validator{

		bool IsValid();

		List<string> GetErrors();

	}

}