using Web.Models.Url;

namespace Web.Models{

	public class AuthLoginModel : PageModel {

	    public string ReturnUrl { get; set; }
	    public UserAddUrlModel AddUserUrl { get; set; }
	    public ForgotPasswordUrlModel ForgotPasswordUrl { get; set; }
	    public string LoginName { get; set; }

        public AuthLoginModel(string returnUrl, string loginName) : base(null)
	    {
			ReturnUrl = returnUrl;
			if(ReturnUrl == null){
				ReturnUrl = new HomeUrlModel().Url;
			}
			AddUserUrl = new UserAddUrlModel();
			ForgotPasswordUrl = new ForgotPasswordUrlModel();
			if(loginName != null){
				LoginName = loginName;
			}
	    }

	}

}