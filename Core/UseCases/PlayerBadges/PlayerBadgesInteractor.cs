﻿using Core.Repositories;

namespace Core.UseCases.PlayerBadges
{
    public static class PlayerBadgesInteractor
    {
        public static PlayerBadgesResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            PlayerBadgesRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgames = cashgameRepository.GetPublished(bunch);

            return new PlayerBadgesResult(request.PlayerId, cashgames);
        }
    }
}