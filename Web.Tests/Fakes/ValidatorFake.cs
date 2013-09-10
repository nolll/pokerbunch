using System.Collections.Generic;
using Web.Validators;

namespace Web.Tests.Fakes{

	public class ValidatorFake : IValidator{

	    public bool IsValid { get; set; }
	    public List<string> Errors { get; set; }

		public ValidatorFake()
		{
		    IsValid = true;
			Errors = new List<string>();
		}

        public List<string> GetErrors()
        {
            return Errors;
        }

	}

}