using System.Collections.Generic;

namespace Web.Validators{

	public abstract class BaseValidator : Validator{

        protected List<string> Errors;

		public void AddError(string message){
			EnsureErrorArray();
            Errors.Add(message);
		}

		public virtual List<string> GetErrors(){
			EnsureErrorArray();
			return Errors;
		}

		protected void EnsureErrorArray(){
			if(Errors == null){
				Errors = new List<string>();
			}
		}

		abstract protected void Validate();

        public abstract bool IsValid();

	}

}