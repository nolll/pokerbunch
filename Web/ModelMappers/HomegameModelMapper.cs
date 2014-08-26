using System;
using Application.Factories;
using Application.Services;
using Core.Entities;
using Web.Models.HomegameModels.Add;
using Web.Models.HomegameModels.Edit;

namespace Web.ModelMappers
{
    public static class HomegameModelMapper
    {
        public static Homegame GetHomegame(AddHomegamePostModel postModel)
        {
            return HomegameFactory.Create(
                    0,
                    SlugGenerator.GetSlug(postModel.DisplayName),
                    postModel.DisplayName,
                    postModel.Description,
                    string.Empty,
                    TimeZoneInfo.FindSystemTimeZoneById(postModel.TimeZone),
                    200,
                    new Currency(postModel.CurrencySymbol, postModel.CurrencyLayout));
        }

        public static Homegame GetHomegame(Homegame homegame, HomegameEditPostModel postModel)
        {
            return HomegameFactory.Create(
                    homegame.Id,
                    homegame.Slug,
                    homegame.DisplayName,
                    postModel.Description,
                    postModel.HouseRules,
                    TimeZoneInfo.FindSystemTimeZoneById(postModel.TimeZone),
                    postModel.DefaultBuyin,
                    new Currency(postModel.CurrencySymbol, postModel.CurrencyLayout));
        }
    }
}