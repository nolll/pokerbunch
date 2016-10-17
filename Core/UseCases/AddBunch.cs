﻿using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class AddBunch
    {
        private readonly IBunchRepository _bunchRepository;

        public AddBunch(IBunchRepository bunchRepository)
        {
            _bunchRepository = bunchRepository;
        }

        public void Execute(Request request)
        {
            var bunch = CreateBunch(request);
            _bunchRepository.Add(bunch);
        }

        private static Bunch CreateBunch(Request request)
        {
            return new Bunch(
                SlugGenerator.GetSlug(request.DisplayName),
                request.DisplayName,
                request.Description,
                string.Empty,
                TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone),
                200,
                new Currency(request.CurrencySymbol, request.CurrencyLayout));
        }

        public class Request
        {
            public string UserName { get; }
            [Required(ErrorMessage = "Display Name can't be empty")]
            public string DisplayName { get; }
            public string Description { get; }
            [Required(ErrorMessage = "Currency Symbol can't be empty")]
            public string CurrencySymbol { get; }
            [Required(ErrorMessage = "Currency Layout can't be empty")]
            public string CurrencyLayout { get; }
            [Required(ErrorMessage = "Timezone can't be empty")]
            public string TimeZone { get; }

            public Request(string userName, string displayName, string description, string currencySymbol, string currencyLayout, string timeZone)
            {
                UserName = userName;
                DisplayName = displayName;
                Description = description;
                CurrencySymbol = currencySymbol;
                CurrencyLayout = currencyLayout;
                TimeZone = timeZone;
            }
        }
    }
}
