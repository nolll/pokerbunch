namespace Core.Urls
{
    public abstract class BunchWithOptionalYearUrl : Url
    {
        protected BunchWithOptionalYearUrl(string format, string formatWithYear, string slug, int? year)
            : base(FormatBunchWithOptionalYear(format, formatWithYear, slug, year))
        {
        }

        private static string FormatBunchWithOptionalYear(string format, string formatWithYear, string slug, int? year)
        {
            return year.HasValue ? string.Format(formatWithYear, slug, year.Value) : string.Format(format, slug);
        }
    }
}