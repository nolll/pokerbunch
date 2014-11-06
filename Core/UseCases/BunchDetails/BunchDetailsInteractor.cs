using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.BunchDetails
{
    public static class BunchDetailsInteractor
    {
        public static BunchDetailsResult Execute(IBunchRepository bunchRepository, IPlayerRepository playerRepository, IAuth auth, BunchDetailsRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            //var player = playerRepository.GetByUserName(bunch.Id, request.UserName);
            //if(!player.IsInRole(Role.Player))
            //    throw new AccessDeniedException();
            var bunchName = bunch.DisplayName;
            var description = bunch.Description;
            var houseRules = bunch.HouseRules;
            var editBunchUrl = new EditBunchUrl(bunch.Slug);
            var canEdit = auth.IsInRole(bunch.Slug, Role.Manager);

            return new BunchDetailsResult(bunchName, description, houseRules, editBunchUrl, canEdit);
        }
    }
}