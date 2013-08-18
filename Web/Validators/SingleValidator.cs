namespace Web.Validators{

	abstract class SingleValidator : BaseValidator{

		public override bool IsValid(){
			EnsureErrorArray();
			Validate();
			return Errors.Count == 0;
		}

		protected bool IsNullOrEmpty(string str){
			return string.IsNullOrEmpty(str);
		}

		protected bool IsEmail(string email)
		{
            //todo: fix the regex
            /*
		    var regex = new Regex("/^[a-z0-9&\'\.\-_\+]+@[a-z0-9\-]+\.([a-z0-9\-]+\.)*+[a-z]{2}/is");
		    return regex.Match(email).Success;
            */
		    return true;
		}

	}

}