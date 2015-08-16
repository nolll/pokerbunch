﻿using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddBunch
    {
        private readonly IUserRepository _userRepository;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public AddBunch(IUserRepository userRepository, IBunchRepository bunchRepository, IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var slug = SlugGenerator.GetSlug(request.DisplayName);

            bool bunchExists;
            try
            {
                var b = _bunchRepository.GetBySlug(slug);
                bunchExists = true;
            }
            catch (BunchNotFoundException)
            {
                bunchExists = false;
            }

            if (bunchExists)
                throw new BunchExistsException();

            var bunch = CreateBunch(request);
            var id = _bunchRepository.Add(bunch);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = new Player(id, user.Id, Role.Manager);
            _playerRepository.Add(player);

            var returnUrl = new AddBunchConfirmationUrl();
            return new Result(returnUrl);
        }

        private static Bunch CreateBunch(Request request)
        {
            return new Bunch(
                0,
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
            public string UserName { get; private set; }
            [Required(ErrorMessage = "Display Name can't be empty")]
            public string DisplayName { get; private set; }
            public string Description { get; private set; }
            [Required(ErrorMessage = "Currency Symbol can't be empty")]
            public string CurrencySymbol { get; private set; }
            [Required(ErrorMessage = "Currency Layout can't be empty")]
            public string CurrencyLayout { get; private set; }
            [Required(ErrorMessage = "Timezone can't be empty")]
            public string TimeZone { get; private set; }

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