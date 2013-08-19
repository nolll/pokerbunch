using Web.Models.Url;

namespace Web.Models{

	public class AuthLoginModel : PageModel {

	    public string ReturnUrl { get; set; }
	    public UserAddUrlModel AddUserUrl { get; set; }
	    public ForgotPasswordUrlModel ForgotPasswordUrl { get; set; }
	    public string LoginName { get; set; }
        public string Password { get; set; }
	    public bool RememberMe { get; set; }

        public AuthLoginModel() : base(null)
	    {
			
	    }

	}

}