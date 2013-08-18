namespace Web.Validators{

	abstract class SimpleValidator : SingleValidator {
	    protected readonly string Subject;
	    protected readonly string Message;

	    public SimpleValidator(string subject, string message)
	    {
	        Subject = subject;
	        Message = message;
	    }

	    protected override void Validate(){
			ValidateSubject();
		}

	    protected abstract bool ValidateSubject();
		
	}

}