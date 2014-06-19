﻿using Application.UseCases.BunchContext;
using Core.Repositories;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Add;

namespace Web.ModelFactories.PlayerModelFactories
{
    public class AddPlayerConfirmationPageBuilder : IAddPlayerConfirmationPageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public AddPlayerConfirmationPageBuilder(
            IHomegameRepository homegameRepository,
            IBunchContextInteractor bunchContextInteractor)
        {
            _homegameRepository = homegameRepository;
            _bunchContextInteractor = bunchContextInteractor;
        }

        public AddPlayerConfirmationPageModel Build(string slug)
        {
            var homegame = _homegameRepository.GetBySlug(slug);

            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest {Slug = slug});
            
            return new AddPlayerConfirmationPageModel
                {
                    BrowserTitle = "Player Added",
                    PageProperties = new PageProperties(contextResult),
                    HomegameName = homegame.DisplayName
                };
        }
    }
}