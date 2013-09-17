using System;
using Core.Classes;
using Core.Services;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;

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

        public Homegame GetHomegame(Homegame homegame, HomegameEditPostModel postModel)
        {
            return new Homegame
            {
                Id = homegame.Id,
                Slug = homegame.Slug,
                DisplayName = homegame.DisplayName,
                Description = postModel.Description,
                HouseRules = postModel.HouseRules,
                Currency = new CurrencySettings(postModel.CurrencySymbol, postModel.CurrencyLayout),
                Timezone = TimeZoneInfo.FindSystemTimeZoneById(postModel.TimeZone),
                DefaultBuyin = postModel.DefaultBuyin,
                //CashgamesEnabled = rawHomegame.CashgamesEnabled,
                //TournamentsEnabled = rawHomegame.TournamentsEnabled,
                //VideosEnabled = rawHomegame.VideosEnabled
            };
        }
    }
}