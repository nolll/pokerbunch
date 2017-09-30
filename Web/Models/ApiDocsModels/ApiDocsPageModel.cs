using System.Collections.Generic;
using Core.UseCases;
using Web.Components.ApiDocsModels.Section;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.ApiDocsModels
{
    public abstract class ApiDocsPageModel : AppPageModel
    {
        public abstract IList<SectionModel> Sections { get; }

        protected ApiDocsPageModel(CoreContext.Result appContextResult)
            : base(appContextResult)
        {
        }

        public override View GetView()
        {
            return new View("~/Views/Pages/ApiDocs/ApiDocs.cshtml");
        }
    }
}