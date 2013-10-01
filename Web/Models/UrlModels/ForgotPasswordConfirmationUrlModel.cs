using Web.Routing;

namespace Web.Models.UrlModels{

	public class ForgotPasswordConfirmationUrlModel : UrlModel{

		public ForgotPasswordConfirmationUrlModel()
            : base(RouteFormats.ForgotPasswordConfirmation)
        {
		}

	}

}