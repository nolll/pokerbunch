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

        public Homegame GetHomegame(HomegameAddModel model)
        {
            var homegame = new Homegame();
            homegame.DisplayName = model.DisplayName;
            homegame.Currency = new CurrencySettings(model.CurrencySymbol, model.CurrencyLayoutSelectModel.Value);
            homegame.Timezone = TimeZoneInfo.FindSystemTimeZoneById(model.TimezoneSelectModel.Value);
            homegame.Description = model.Description;
            homegame.DefaultBuyin = 200;
            homegame.Slug = _slugGenerator.GetSlug(homegame.DisplayName);
            return homegame;
        }
    }
}