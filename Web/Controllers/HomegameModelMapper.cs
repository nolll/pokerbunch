using System;
using Core.Classes;
using Core.Services;
using Web.Models.HomegameModels.Add;

namespace Web.Controllers
{
    public class HomegameModelMapper : IHomegameModelMapper
    {
        private readonly ISlugGenerator _slugGenerator;

        public HomegameModelMapper(ISlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }

        public Homegame GetHomegame(AddHomegamePageModel model)
        {
            return new Homegame
                {
                    DisplayName = model.DisplayName,
                    Currency = new CurrencySettings(model.CurrencySymbol, model.CurrencyLayout),
                    Timezone = TimeZoneInfo.FindSystemTimeZoneById(model.TimeZone),
                    Description = model.Description,
                    DefaultBuyin = 200,
                    Slug = _slugGenerator.GetSlug(model.DisplayName)
                };
        }
    }
}