﻿using System;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.AddBunch
{
    public static class AddBunchInteractor
    {
        public static AddBunchResult Execute(IAuth auth, IBunchRepository bunchRepository, IPlayerRepository playerRepository, AddBunchRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var slug = SlugGenerator.GetSlug(request.DisplayName);
            var existingBunch = bunchRepository.GetBySlug(slug);

            if (existingBunch != null)
                throw new BunchExistsException();

            var bunch = CreateBunch(request);
            var id = bunchRepository.Add(bunch);
            var identity = auth.CurrentIdentity;
            var player = new Player(id, identity.UserId, Role.Manager);
            playerRepository.Add(player);

            var returnUrl = new AddBunchConfirmationUrl();
            return new AddBunchResult(returnUrl);
        }

        private static Bunch CreateBunch(AddBunchRequest request)
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
    }
}
