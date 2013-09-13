using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.AuthModels{

	public class AuthLoginPageModel : PageProperties {

	    public string ReturnUrl { get; set; }
	    public UserAddUrlModel AddUserUrl { get; set; }
	    public ForgotPasswordUrlModel ForgotPasswordUrl { get; set; }
	    public string LoginName { get; set; }
        public string Password { get; set; }
	    public bool RememberMe { get; set; }

        public AuthLoginPageModel() : base(null)
	    {
			
	    }

	}

}