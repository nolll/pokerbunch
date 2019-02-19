namespace Web.Models.ErrorModels
{
    public class Error404PageModel : ErrorPageModel
    {
        public override string Title => "Page not found";
        public override string Message => "Please check the url for errors";
    }
}