using System;
using Application.Services.Interfaces;
using Core.Classes;
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
                (
                    0,
                    _slugGenerator.GetSlug(postModel.DisplayName),
                    postModel.DisplayName,
                    postModel.Description,
                    string.Empty,
                    TimeZoneInfo.FindSystemTimeZoneById(postModel.TimeZone),
                    200,
                    new CurrencySettings(postModel.CurrencySymbol, postModel.CurrencyLayout)
                );
        }

        public Homegame GetHomegame(Homegame homegame, HomegameEditPostModel postModel)
        {
            return new Homegame
                (
                    homegame.Id,
                    homegame.Slug,
                    homegame.DisplayName,
                    postModel.Description,
                    postModel.HouseRules,
                    TimeZoneInfo.FindSystemTimeZoneById(postModel.TimeZone),
                    postModel.DefaultBuyin,
                    new CurrencySettings(postModel.CurrencySymbol, postModel.CurrencyLayout)
                );
        }
    }
}