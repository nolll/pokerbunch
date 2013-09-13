using System;
using Core.Classes;
using Core.Services;
using Web.Models.HomegameModels.Add;

namespace Web.ModelMappers
{
    public class HomegameModelMapper : IHomegameModelMapper
    {
        private readonly ISlugGenerator _slugGenerator;

        public HomegameModelMapper(ISlugGenerator slugGenerator)
        {
            _slugGenerator = slugGenerator;
        }

        public Homegame GetHomegame(AddHomegamePostModel postModel)
        {
            return new Homegame
                {
                    DisplayName = postModel.DisplayName,
                    Currency = new CurrencySettings(postModel.CurrencySymbol, postModel.CurrencyLayout),
                    Timezone = TimeZoneInfo.FindSystemTimeZoneById(postModel.TimeZone),
                    Description = postModel.Description,
                    DefaultBuyin = 200,
                    Slug = _slugGenerator.GetSlug(postModel.DisplayName)
                };
        }
    }
}