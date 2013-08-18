namespace Web.Validators{

	class RequiredValidator : SimpleValidator {
	    
        public RequiredValidator(string subject, string message) : base(subject, message)
	    {
	    }

	    protected override bool ValidateSubject(){
			if(string.IsNullOrEmpty(Subject)){
				AddError(Message);
				return false;
			}
			return true;
		}
		
	}

}