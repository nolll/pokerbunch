using System.Collections.Generic;

namespace Web.Validators{

	class CompositeValidator : BaseValidator{

		private List<Validator> _validators;

		private bool _valid;
		private bool _validated;

	    public CompositeValidator()
	    {
	        _validators = new List<Validator>();
			_validated = false;
			_valid = true;
	    }

		public void AddValidator(Validator validator){
			_validators.Add(validator);
		}

	    protected override void Validate(){
			_validated = true;
			foreach(var validator in _validators){
				if(!validator.IsValid){
					_valid = false;
				}
			}
		}

	    public override bool IsValid
	    {
	        get
	        {
	            if (!_validated)
	            {
	                Validate();
	            }
	            EnsureErrorArray();
	            return _valid && Errors.Count == 0;
	        }
	    }

	    public override List<string> GetErrors(){
			EnsureErrorArray();
			foreach(var validator in _validators){
                Errors.AddRange(validator.GetErrors());
			}
			return Errors;
		}

	}

}