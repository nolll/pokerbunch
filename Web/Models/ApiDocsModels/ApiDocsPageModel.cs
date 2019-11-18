using System.Collections.Generic;
using Core.Settings;
using Core.UseCases;
using Web.Components.ApiDocsModels.Section;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.ApiDocsModels
{
    public abstract class ApiDocsPageModel : AppPageModel
    {
        public abstract IList<SectionModel> Sections { get; }

        protected ApiDocsPageModel(AppSettings appSettings, CoreContext.Result appContextResult)
            : base(appSettings, appContextResult)
        {
        }

        public override View GetView()
        {
            return new View("~/Views/Pages/ApiDocs/ApiDocs.cshtml");
        }
    }
}