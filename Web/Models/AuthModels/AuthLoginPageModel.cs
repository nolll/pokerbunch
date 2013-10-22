using Web.Models.PageBaseModels;

namespace Web.Models.AuthModels{

    public class AuthLoginPageModel : AuthLoginPostModel, IPageModel
    {
	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public string AddUserUrl { get; set; }
	    public string ForgotPasswordUrl { get; set; }
	}

}