using System;
using Application.Factories;
using Application.Services;
using Core.Classes;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;

namespace Web.ModelMappers
{
    public class HomegameModelMapper : IHomegameModelMapper
    {
        private readonly ISlugGenerator _slugGenerator;
        private readonly IHomegameFactory _homegameFactory;

        public HomegameModelMapper(
            ISlugGenerator slugGenerator,
            IHomegameFactory homegameFactory)
        {
            _slugGenerator = slugGenerator;
            _homegameFactory = homegameFactory;
        }

        public Homegame GetHomegame(AddHomegamePostModel postModel)
        {
            return _homegameFactory.Create(
                    0,
                    _slugGenerator.GetSlug(postModel.DisplayName),
                    postModel.DisplayName,
                    postModel.Description,
                    string.Empty,
                    TimeZoneInfo.FindSystemTimeZoneById(postModel.TimeZone),
                    200,
                    new CurrencySettings(postModel.CurrencySymbol, postModel.CurrencyLayout));
        }

        public Homegame GetHomegame(Homegame homegame, HomegameEditPostModel postModel)
        {
            return _homegameFactory.Create(
                    homegame.Id,
                    homegame.Slug,
                    homegame.DisplayName,
                    postModel.Description,
                    postModel.HouseRules,
                    TimeZoneInfo.FindSystemTimeZoneById(postModel.TimeZone),
                    postModel.DefaultBuyin,
                    new CurrencySettings(postModel.CurrencySymbol, postModel.CurrencyLayout));
        }
    }
}