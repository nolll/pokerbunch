﻿using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditBunch
    {
        private readonly IBunchRepository _bunchRepository;

        public EditBunch(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var postedHomegame = CreateBunch(bunch, request);
            _bunchRepository.Save(postedHomegame);

            var returnUrl = new BunchDetailsUrl(request.Slug);
            return new Result(returnUrl);
        }

        private static Bunch CreateBunch(Bunch bunch, Request request)
        {
            return new Bunch(
                    bunch.Id,
                    bunch.Slug,
                    bunch.DisplayName,
                    request.Description,
                    request.HouseRules,
                    TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone),
                    request.DefaultBuyin,
                    new Currency(request.CurrencySymbol, request.CurrencyLayout));
        }

        public class Request
        {
            public string Slug { get; private set; }
            public string Description { get; private set; }
            [Required(ErrorMessage = "Currency Symbol can't be empty")]
            public string CurrencySymbol { get; private set; }
            [Required(ErrorMessage = "Currency Layout can't be empty")]
            public string CurrencyLayout { get; private set; }
            [Required(ErrorMessage = "Timezone can't be empty")]
            public string TimeZone { get; private set; }
            public string HouseRules { get; private set; }
            public int DefaultBuyin { get; private set; }

            public Request(string slug, string description, string currencySymbol, string currencyLayout, string timeZone, string houseRules, int defaultBuyin)
            {
                Slug = slug;
                Description = description;
                CurrencySymbol = currencySymbol;
                CurrencyLayout = currencyLayout;
                TimeZone = timeZone;
                HouseRules = houseRules;
                DefaultBuyin = defaultBuyin;
            }
        }

        public class Result
        {
            public Url ReturnUrl { get; private set; }

            public Result(Url returnUrl)
            {
                ReturnUrl = returnUrl;
            }
        }
    }
}