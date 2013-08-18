namespace Web.Validators{

	class NotNullValidator : SingleValidator {
	    private readonly object _subject;
	    private readonly string _message;

	    public NotNullValidator(object subject, string message)
	    {
	        _subject = subject;
	        _message = message;
	    }

	    protected override void Validate(){
			if(_subject == null){
				AddError(_message);
			}
		}

	}

}