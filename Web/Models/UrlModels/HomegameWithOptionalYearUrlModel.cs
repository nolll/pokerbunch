using Web.Services;

namespace Web.Models.UrlModels
{
    public abstract class HomegameWithOptionalYearUrlModel : UrlModel
    {
        protected HomegameWithOptionalYearUrlModel(string format, string formatWithYear, string slug, int? year)
            : base(UrlProvider.FormatHomegameWithOptionalYear(format, formatWithYear, slug, year))
        {
        }
    }
}