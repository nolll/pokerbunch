﻿namespace Web.Models.UrlModels
{
    public abstract class HomegameWithOptionalYearUrlModel : UrlModel
    {
        protected HomegameWithOptionalYearUrlModel(string format, string formatWithYear, string slug, int? year)
            : base(FormatHomegameWithOptionalYear(format, formatWithYear, slug, year))
        {
        }

        private static string FormatHomegameWithOptionalYear(string format, string formatWithYear, string slug, int? year)
        {
            return year.HasValue ? string.Format(formatWithYear, slug, year.Value) : string.Format(format, slug);
        }
    }
}