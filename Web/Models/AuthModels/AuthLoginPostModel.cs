namespace Web.Models.AuthModels{

	public class AuthLoginPostModel {

        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

	}

}