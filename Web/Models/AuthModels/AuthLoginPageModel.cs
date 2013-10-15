using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.AuthModels{

    public class AuthLoginPageModel : AuthLoginPostModel, IPageModel
    {
	    public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public string AddUserUrl { get; set; }
	    public ForgotPasswordUrlModel ForgotPasswordUrl { get; set; }
	}

}